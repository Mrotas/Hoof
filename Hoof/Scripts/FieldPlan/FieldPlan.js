var FieldPlan = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;
    var previousTableCellValue;

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function (row) {
        var isValid = true;

        var type = row.find('td:nth-child(2)').find('select');
        if (type.val() === '') {
            type.addClass('has-error');
            isValid = false;
        }

        var plan = row.find('td:nth-child(3)').find('input');
        if (plan.val() === '') {
            plan.addClass('has-error');
            isValid = false;
        }

        if (isValid) {
            type.removeClass('has-error');
            plan.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getFieldPlanModel = function (row, isNew) {
        var plan;
        if (isNew) {
            plan = row.find('td:nth-child(3)').find('input').val();
        } else {
            plan = row.find('td:nth-child(3)').text();
        }

        return {
            Hectare: plan.replace(',', '.')
        };
    };

    var makeRowEditable = function (button) {
        var row = $(button).closest('tr');
        $(row).addClass('editable');

        var tdsToEdit = $(row).find('td.canEdit');
        $.each(tdsToEdit, function (index, td) {
            previousTableCellValue = $(td).text();
            $(td).attr('contenteditable', true);
            $(td).addClass('editable');
            td.focus();
        });
    }

    var clearTable = function (setPreviousValue) {
        var rowInEditProcess = $('#planTable').find('tr.editable');
        rowInEditProcess.removeClass('editable');
        $.each($(rowInEditProcess).find('td.editable'), function (index, cell) {
            if (setPreviousValue) {
                $(cell).text(previousTableCellValue);
            }
            $(cell).attr('contenteditable', false);
            $(cell).removeClass('editable');
        });

        $('#save').attr('disabled', true);
        $('#cancel').attr('disabled', true);
    }

    var clearRow = function (row) {
        var tdsToClear = row.find('td');
        $.each(tdsToClear, function (index, td) {
            var select = $(td).find('select');
            if (select.length > 0) {
                select.val(null);
                return true; // continue
            }
            var input = $(td).find('input');
            if (input.length > 0) {
                input.val('');
            }
        });
    }

    var enableButtons = function (isEditMode) {
        $('#addPlan').attr('disabled', isEditMode);
        var buttons = [];
        buttons.push($('#planTable').find('.editPlan'));
        buttons.push($('#planTable').find('.deletePlan'));
        $.each(buttons,
            function (index, button) {
                $(button).attr('disabled', isEditMode);
            });

        $('#save').attr('disabled', !isEditMode);
        $('#cancel').attr('disabled', !isEditMode);

    }

    $('.clearSearch').on('click', function () {
        var row = $(this).closest('tr');
        clearRow(row);
    });

    $('#closeDangerAlert').on('click', function () {
        $('#dangerAlert').hide();
    });

    $("td.canEdit").keypress(function (e) {
        var typedCharacter = String.fromCharCode(e.which);
        if (typedCharacter === ',' || typedCharacter === '.') {
            var inputText = $(this).text();
            if (inputText.indexOf(',') === -1 && inputText.indexOf('.') === -1) {
                return typedCharacter;
            }
        }

        if (isNaN(typedCharacter)) {
            e.preventDefault();
        }
    });

    $('#cancel').on('click', function () {
        $('#warningAlert').hide();
        clearTable(true);
        enableButtons(false);
    });

    $('#save').on('click', function () {
        var rowInEditProcess = $('#planTable').find('tr.editable');

        var isValid = validateForm(rowInEditProcess);
        if (isValid) {
            $.ajax({
                type: "POST",
                url: '/' + controller + '/Edit',
                data: {
                    model: getFieldPlanModel(rowInEditProcess, false),
                    marketingYearId: marketingYearId
                },
                dataType: 'json',
                success: function (data) {
                    if (data.message !== '') {
                        showDangerAlert(data.message);
                    } else {
                        location.reload();
                    }
                },
                error: function () {
                    alert('Coś poszło nie tak, proszę odświeżyć stronę.');
                }
            });

            clearTable(false);
        }
    });

    $('#addPlan').on('click', function () {
        var row = $(this).closest('tr');
        var isFormValid = validateForm(row);

        if (isFormValid) {
            $.ajax({
                type: "POST",
                url: '/' + controller + '/Add',
                data: {
                    model: getFieldPlanModel(row, true),
                    marketingYearId: marketingYearId
                },
                dataType: 'json',
                success: function (data) {
                    if (data.message !== '') {
                        showDangerAlert(data.message);
                    } else {
                        location.reload();
                    }
                },
                error: function () {
                    alert('Coś poszło nie tak, proszę odświeżyć stronę.');
                }
            });
        }
    });

    $('.editPlan').on('click', function () {
        makeRowEditable(this);
        enableButtons(true);
    });

    $('.deletePlan').on('click', function () {
        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć plan Powierzchnia zagospodarowanych przez dzierżawcę albo zarządcę obwodu łowieckiego łąk śródleśnych i przyleśnych?');
        $('#confirmDeleteModal').modal('show');
    });

    $('#confirmDelete').on('click', function () {

        $.ajax({
            type: "POST",
            url: '/' + controller + '/Delete',
            data: {
                marketingYearId: marketingYearId
            },
            dataType: 'json',
            success: function (data) {
                if (data.message !== '') {
                    showDangerAlert(data.message);
                } else {
                    location.reload();
                }
            },
            error: function () {
                alert('Coś poszło nie tak, proszę odświeżyć stronę.');
            }
        });

        $('#confirmDeleteModal').modal('hide');
    });
}
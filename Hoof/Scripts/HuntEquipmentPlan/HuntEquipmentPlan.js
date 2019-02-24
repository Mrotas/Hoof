var HuntEquipmentPlan = function(config) {

    var marketingYearModel = config.MarketingYearModel;
    var previousTableCellValue;

    var showDangerAlert = function (message) {
        
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var showWarningAlert = function(message) {

        $('.alert-warning').find('.alert-body').text(message);
        $('.alert-warning').show();
    }

    var validateForm = function (row) {
        var isValid = true;

        var huntEquipmentType = row.find('td:nth-child(2)').find('select');
        if (huntEquipmentType.val() === '') {
            huntEquipmentType.addClass('has-error');
            isValid = false;
        }

        var huntEquipmentCountPlan = row.find('td:nth-child(3)').find('input');
        if (huntEquipmentCountPlan.val() === '') {
            huntEquipmentCountPlan.addClass('has-error');
            isValid = false;
        }

        if (isValid) {
            huntEquipmentType.removeClass('has-error');
            huntEquipmentCountPlan.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getHuntEquipmentPlanModel = function (row, isNew) {
        var huntEquipmentType;
        var huntEquipmentTypeName;
        var huntEquipmentCountPlan;
        if (isNew) {
            huntEquipmentType = row.find('td:nth-child(2)').find('select').val();
            huntEquipmentTypeName = row.find('td:nth-child(2)').find('select').text();
            huntEquipmentCountPlan = row.find('td:nth-child(3)').find('input').val();
        } else {
            huntEquipmentType = row.find('td:nth-child(1)').text();
            huntEquipmentTypeName = row.find('td:nth-child(2)').text();
            huntEquipmentCountPlan = row.find('td:nth-child(3)').text();
        }

        return {
            Type: huntEquipmentType,
            TypeName: huntEquipmentTypeName,
            Count: huntEquipmentCountPlan
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

    var clearTable = function(setPreviousValue) {
        var rowInEditProcess = $('#huntEquipmentPlanTable').find('tr.editable');
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

    var enableButtons = function(isEditMode) {

        $('#addHuntEquipmentPlan').attr('disabled', isEditMode);
        var buttons = [];
        buttons.push($('#huntEquipmentPlanTable').find('.editHuntEquipmentPlan'));
        buttons.push($('#huntEquipmentPlanTable').find('.deleteHuntEquipmentPlan'));
        $.each(buttons,
            function(index, button) {
                $(button).attr('disabled', isEditMode);
            });

        $('#save').attr('disabled', !isEditMode);
        $('#cancel').attr('disabled', !isEditMode);
        
    }

    $('#closeDangerAlert').on('click', function() {
        $('#dangerAlert').hide();
    });

    $('#closeWarningAlert').on('click', function () {
        $('#warningAlert').hide();
    });

    $("td.canEdit").keypress(function (e) {
        if (isNaN(String.fromCharCode(e.which))) {
            e.preventDefault();
        }
    });

    $('#cancel').on('click', function () {
        $('#warningAlert').hide();
        clearTable(true);
        enableButtons(false);
    });

    $('#save').on('click', function () {
        var rowInEditProcess = $('#huntEquipmentPlanTable').find('tr.editable');

        var isValid = validateForm(rowInEditProcess);
        if (isValid) {
            $.ajax({
                type: "POST",
                url: '/HuntEquipmentPlan/Edit',
                data: {
                    model: getHuntEquipmentPlanModel(rowInEditProcess, false),
                    marketingYearId: marketingYearModel.Id
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

    $('#addHuntEquipmentPlan').on('click', function () {

        var row = $(this).closest('tr');
        var isFormValid = validateForm(row);

        if (isFormValid) {
            $.ajax({
                type: "POST",
                url: '/HuntEquipmentPlan/Add',
                data: {
                    model: getHuntEquipmentPlanModel(row, true),
                    marketingYearId: marketingYearModel.Id
                },
                dataType: 'json',
                success: function(data) {
                    if (data.message !== '') {
                        showDangerAlert(data.message);
                    } else {
                        location.reload();
                    }
                },
                error: function() {
                    alert('Coś poszło nie tak, proszę odświeżyć stronę.');
                }
            });
        }
    });

    $('.editHuntEquipmentPlan').on('click', function () {

        var rowInEditProcess = $('#huntEquipmentPlanTable').find('tr.editable');
        if (rowInEditProcess.length > 0) {
            showWarningAlert('Aby móc edytować kolejny plan, proszę zakończyć proces edycji poprzedniego.');
            return;
        }

        makeRowEditable(this);

        enableButtons(true);
    });

    $('.deleteHuntEquipmentPlan').on('click', function () {

        var row = $(this).closest('tr');
        var model = getHuntEquipmentPlanModel(row, false);
        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć plan dla urządzeń łowieckich ' + model.TypeName);
        $('#confirmDeleteModal').data('huntEquipmentType', model.Type).modal('show');
    });

    $('#confirmDelete').on('click', function () {

        var huntEquipmentType = $('#confirmDeleteModal').data('huntEquipmentType');

        $.ajax({
            type: "POST",
            url: '/HuntEquipmentPlan/Delete',
            data: {
                huntEquipmentType: huntEquipmentType,
                marketingYearId: marketingYearModel.Id
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
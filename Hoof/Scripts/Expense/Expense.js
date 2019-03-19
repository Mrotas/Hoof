var Expense = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function () {
        var isValid = true;

        var description = $('#description');
        if (description.val() === '') {
            description.addClass('has-error');
            isValid = false;
        }

        var cost = $('#cost');
        if (cost.val() === '') {
            cost.addClass('has-error');
            isValid = false;
        }

        var note = $('#note');
        if (note.val() === '') {
            note.addClass('has-error');
            isValid = false;
        }

        var date = $("#date");
        if (date.val() === '') {
            date.addClass('has-error');
            isValid = false;
        }

        if (isValid) {
            description.removeClass('has-error');
            cost.removeClass('has-error');
            note.removeClass('has-error');
            date.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getExpenseModel = function () {
        var id = $('#addExpenseModal').data('id');
        var description = $('#description').val();
        var cost = $('#cost').val();
        var note = $('#note').val();
        var date = $('#date').val();

        return {
            Id: id,
            Description: description,
            Cost: cost,
            Note: note,
            Date: date
        };
    };

    var getDateFromCell = function (date) {
        var day = date.substr(0, 2);
        var month = date.substr(3, 2);
        var year = date.substr(6, 4);

        return year + '-' + month + '-' + day;
    }

    var clearModal = function () {
        $('#description').val('');
        $('#description').removeClass('has-error');
        $('#cost').val('');
        $('#cost').removeClass('has-error');
        $('#note').val('');
        $('#note').removeClass('has-error');
        $('#date').val('');
        $('#date').removeClass('has-error');

        $('.alert-danger').hide();
    }

    $('#closeDangerAlert').on('click', function () {
        $('#dangerAlert').hide();
    });

    $('#cancel').on('click', function () {
        clearModal();
    });

    $('#closeModalXButton').on('click', function () {
        clearModal();
    });

    $("#addExpenseModal").on("hidden.bs.modal", function () {
        clearModal();
    });
    
    $('#save').on('click', function () {
        var isFormValid = validateForm();
        var url;
        var id = $('#addExpenseModal').data('id');
        if (typeof (id) === 'undefined') {
            url = '/' + controller + '/Add';
        } else {
            url = '/' + controller + '/Edit';
        }

        if (isFormValid) {
            $.ajax({
                type: "POST",
                url: url,
                data: {
                    model: getExpenseModel(),
                    marketingYearId: marketingYearId
                },
                dataType: 'json',
                success: function (data) {
                    if (data.message !== '') {
                        showDangerAlert(data.message);
                    } else {
                        clearModal();
                        location.reload();
                    }
                },
                error: function () {
                    alert('Coś poszło nie tak, proszę odświeżyć stronę.');
                }
            });
            $.removeData($('#addExpenseModal'), 'id');
        }
    });

    $('.editExpense').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var description = row.find('td:nth-child(2)').text().trim();
        var cost = row.find('td:nth-child(3)').text().trim();
        var note = row.find('td:nth-child(4)').text().trim();
        var date = row.find('td:nth-child(5)').text().trim();
        date = getDateFromCell(date);
        
        $('#description').val(description);
        $('#cost').val(cost);
        $('#note').val(note);
        $('#date').val(date);

        $('#addExpenseModal').data('id', id).modal('show');
    });

    $('.deleteExpense').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var description = row.find('td:nth-child(2)').text().trim();
        var cost = row.find('td:nth-child(3)').text().trim();

        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć wydatek ' + description + ', ' + cost + 'zł?');
        $('#confirmDeleteModal').data('id', id).modal('show');
    });

    $('#confirmDelete').on('click', function () {
        var id = $('#confirmDeleteModal').data('id');

        $.ajax({
            type: "POST",
            url: '/' + controller + '/Delete',
            data: {
                id: id,
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
    });
}
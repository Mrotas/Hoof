var Fodder = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function () {
        var isValid = true;

        var date = $('#date');
        if (date.val() === '') {
            date.addClass('has-error');
            isValid = false;
        }

        var type = $('#fodderType');
        if (type.val() === '') {
            type.addClass('has-error');
            isValid = false;
        }

        var kilograms = $('#kilograms');
        if (kilograms.val() === '') {
            kilograms.addClass('has-error');
            isValid = false;
        }

        var owner = $("#owner");
        if (owner.val() === '') {
            owner.addClass('has-error');
            isValid = false;
        }

        var description = $('#description');
        if (description.val() === '') {
            description.addClass('has-error');
            isValid = false;
        }

        if (isValid) {
            date.removeClass('has-error');
            type.removeClass('has-error');
            kilograms.removeClass('has-error');
            owner.removeClass('has-error');
            description.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getFodderModel = function () {
        var id = $('#addFodderModal').data('id');
        var type = $('#fodderType').val();
        var kilograms = $('#kilograms').val();
        var owner = $("#owner").val();
        var description = $('#description').val();
        var date = $('#date').val();

        return {
            Id: id,
            Type: type,
            Kilograms: kilograms,
            Owner: owner,
            Description: description,
            Date: date
        };
    };

    var getDateFromCell = function (date) {
        var day = date.substr(0, 2);
        var month = date.substr(3, 2);
        var year = date.substr(6, 4);

        return year + '-' + month + '-' + day;
    }

    var clearModal = function() {
        $('#fodderType').val('');
        $('#kilograms').val('');
        $("#owner").val('');
        $('#description').val('');
        $('#date').val('');
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

    $('#save').on('click', function () {
        var isFormValid = validateForm();
        var url;
        var id = $('#addFodderModal').data('id');
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
                    model: getFodderModel(),
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
            $.removeData($('#addFodderModal'), 'id');
        }
    });

    $('.editFodder').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var owner = row.find('td:nth-child(2)').text().trim();
        var type = row.find('td:nth-child(3)').text().trim();
        var kilograms = row.find('td:nth-child(5)').text().trim();
        var description = row.find('td:nth-child(6)').text().trim();
        var date = row.find('td:nth-child(7)').text().trim();
        date = getDateFromCell(date);

        $('#fodderType').val(type);
        $('#kilograms').val(kilograms);
        $("#owner").val(owner);
        $('#description').val(description);
        $('#date').val(date);

        $('#addFodderModal').data('id', id).modal('show');
    });

    $('.deleteFodder').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var typeName = row.find('td:nth-child(4)').text().trim();
        var kilograms = row.find('td:nth-child(5)').text().trim();

        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć zagospodarowanie karmy ' + typeName + ' w ilości ' + kilograms + ' kilogramów?');
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
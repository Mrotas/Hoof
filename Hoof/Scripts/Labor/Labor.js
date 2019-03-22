var Labor = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function () {
        var isValid = true;

        var huntsman = $('#huntsman');
        if (huntsman.val() === '') {
            huntsman.addClass('has-error');
            isValid = false;
        }

        var description = $('#description');
        if (description.val() === '') {
            description.addClass('has-error');
            isValid = false;
        }
        
        var date = $("#date");
        if (date.val() === '') {
            date.addClass('has-error');
            isValid = false;
        }

        if (isValid) {
            huntsman.removeClass('has-error');
            description.removeClass('has-error');
            date.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getLaborModel = function () {
        var id = $('#addLaborModal').data('id');
        var huntsmanId = $('#huntsman').val();
        var description = $('#description').val();
        var date = $('#date').val();

        return {
            Id: id,
            HuntsmanId: huntsmanId,
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

    var fillHuntsmanSelect = function (data) {
        $.each(data, function (index, huntsman) {
            $('#huntsman')
                .append($("<option></option>")
                    .attr("value", huntsman.Id)
                    .text(huntsman.Name + ' ' + huntsman.LastName));
        });
    };

    var clearModal = function () {
        $('#huntsman').val('');
        $('#huntsman').removeClass('has-error');
        $('#description').val('');
        $('#description').removeClass('has-error');
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

    $("#addLaborModal").on("hidden.bs.modal", function () {
        clearModal();
    });

    $(document).ready(function () {
        if ($('#huntsman option').length <= 1) {
            $.ajax({
                type: "GET",
                url: '/Huntsman/GetAll',
                dataType: 'json',
                success: function (data) {
                    if (typeof (data) === 'undefined') {
                        showDangerAlert(data.message);
                    } else {
                        fillHuntsmanSelect(data);
                    }
                },
                error: function () {
                    alert('Coś poszło nie tak, proszę odświeżyć stronę.');
                }
            });
        }
    });

    $('#save').on('click', function () {
        var isFormValid = validateForm();
        var url;
        var id = $('#addLaborModal').data('id');
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
                    model: getLaborModel(),
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
            $.removeData($('#addLaborModal'), 'id');
        }
    });

    $('.editLabor').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var huntsmanId = row.find('td:nth-child(2)').text().trim();
        var description = row.find('td:nth-child(4)').text().trim();
        var date = row.find('td:nth-child(5)').text().trim();
        date = getDateFromCell(date);
        
        $('#huntsman').val(huntsmanId);
        $('#description').val(description);
        $('#date').val(date);

        $('#addLaborModal').data('id', id).modal('show');
    });

    $('.deleteLabor').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var huntsmanName = row.find('td:nth-child(3)').text().trim();
        var description = row.find('td:nth-child(4)').text().trim();

        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć pracę gospodarczą ' + description + ', gospodarza ' + huntsmanName + '?');
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
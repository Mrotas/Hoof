var DeerLicker = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function () {
        var isValid = true;

        var section = $('#section');
        if (section.val() === '') {
            section.addClass('has-error');
            isValid = false;
        }

        var district = $('#district');
        if (district.val() === '') {
            district.addClass('has-error');
            isValid = false;
        }

        var forestry = $('#forestry');
        if (forestry.val() === '') {
            forestry.addClass('has-error');
            isValid = false;
        }

        var count = $('#count');
        if (count.val() === '') {
            count.addClass('has-error');
            isValid = false;
        }

        if (isValid) {
            section.removeClass('has-error');
            district.removeClass('has-error');
            forestry.removeClass('has-error');
            count.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getDeerLickerModel = function () {
        var id = $('#addDeerLickerModal').data('id');
        var section = $('#section').val();
        var district = $('#district').val();
        var forestry = $("#forestry option:selected").text();
        var count = $('#count').val();
        var description = $('#description').val();

        return {
            Id: id,
            Section: section,
            District: district,
            Forestry: forestry,
            Description: description,
            Count: count
        };
    };
    
    var clearModal = function () {
        $('#section').val('');
        $('#section').removeClass('has-error');
        $('#district').val('');
        $('#district').removeClass('has-error');
        $("forestry:selected").prop("selected", false);
        $("#forestry").removeClass('has-error');
        $('#description').val('');
        $('#description').removeClass('has-error');
        $('#count').val('');
        $('#count').removeClass('has-error');

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

    $("#addDeerLickerModal").on("hidden.bs.modal", function () {
        clearModal();
    });
    
    $('#save').on('click', function () {
        var isFormValid = validateForm();
        var url;
        var id = $('#addDeerLickerModal').data('id');
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
                    model: getDeerLickerModel(),
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
            $.removeData($('#addDeerLickerModal'), 'id');
        }
    });

    $('.editDeerLicker').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var section = row.find('td:nth-child(2)').text().trim();
        var district = row.find('td:nth-child(3)').text().trim();
        var forestry = row.find('td:nth-child(4)').text().trim();
        var count = row.find('td:nth-child(5)').text().trim();
        var description = row.find('td:nth-child(6)').text().trim();
        
        $('#section').val(section);
        $('#district').val(district);
        $('#forestry option:contains(' + forestry + ')').attr('selected', 'selected');
        $('#description').val(description);
        $('#count').val(count);

        $('#addDeerLickerModal').data('id', id).modal('show');
    });

    $('.deleteDeerLicker').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var section = row.find('td:nth-child(2)').text().trim();
        var district = row.find('td:nth-child(3)').text().trim();
        var forestry = row.find('td:nth-child(4)').text().trim();

        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć lizawki z oddziału ' + section + ', rewir ' + district + ', leśnictwo ' + forestry + '?');
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
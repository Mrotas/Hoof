var Pulpit = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function () {
        var isValid = true;

        var number = $('#number');
        if (number.val() === '') {
            number.addClass('has-error');
            isValid = false;
        }

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

        var hasRoof = $('#hasRoof');
        if (hasRoof.val() === '') {
            hasRoof.addClass('has-error');
            isValid = false;
        }

        var createdDate = $("#createdDate");
        if (createdDate.val() === '') {
            createdDate.addClass('has-error');
            isValid = false;
        }

        if (isValid) {
            number.removeClass('has-error');
            section.removeClass('has-error');
            district.removeClass('has-error');
            forestry.removeClass('has-error');
            hasRoof.removeClass('has-error');
            createdDate.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getPulpitModel = function () {
        var id = $('#addPulpitModal').data('id');
        var number = $('#number').val();
        var section = $('#section').val();
        var district = $('#district').val();
        var forestry = $("#forestry option:selected").text();
        var hasRoof = $('#hasRoof').val();
        var description = $('#description').val();
        var createdDate = $('#createdDate').val();
        var removedDate = $('#removedDate').val();

        return {
            Id: id,
            Number: number,
            Section: section,
            District: district,
            Forestry: forestry,
            HasRoof: hasRoof,
            Description: description,
            CreatedDate: createdDate,
            RemovedDate: removedDate
        };
    };

    var getDateFromCell = function (date) {
        var day = date.substr(0, 2);
        var month = date.substr(3, 2);
        var year = date.substr(6, 4);

        return year + '-' + month + '-' + day;
    }

    var clearModal = function () {
        $('#number').val('');
        $('#number').removeClass('has-error');
        $('#section').val('');
        $('#section').removeClass('has-error');
        $('#district').val('');
        $('#district').removeClass('has-error');
        $("forestry:selected").prop("selected", false);
        $("#forestry").removeClass('has-error');
        $("#hasRoof").val('');
        $("#hasRoof").removeClass('has-error');
        $('#description').val('');
        $('#description').removeClass('has-error');
        $('#createdDate').val('');
        $('#createdDate').removeClass('has-error');
        $('#removedDate').val('');
        $('#removedDate').removeClass('has-error');

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

    $("#addPulpitModal").on("hidden.bs.modal", function () {
        clearModal();
    });

    $('#add').on('click', function () {
        $('#removedDateSection').attr('hidden', true);
    });

    $('#save').on('click', function () {
        var isFormValid = validateForm();
        var url;
        var id = $('#addPulpitModal').data('id');
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
                    model: getPulpitModel(),
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
            $.removeData($('#addPulpitModal'), 'id');
        }
    });

    $('.editPulpit').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var number = row.find('td:nth-child(2)').text().trim();
        var section = row.find('td:nth-child(3)').text().trim();
        var district = row.find('td:nth-child(4)').text().trim();
        var forestry = row.find('td:nth-child(5)').text().trim();
        var hasRoof = row.find('td:nth-child(6)').find('input').val();
        var description = row.find('td:nth-child(7)').text().trim();
        var createdDate = row.find('td:nth-child(8)').text().trim();
        createdDate = getDateFromCell(createdDate);
        var removedDate = row.find('td:nth-child(9)').text().trim();
        removedDate = getDateFromCell(removedDate);

        $('#removedDateSection').attr('hidden', false);

        $('#number').val(number);
        $('#section').val(section);
        $('#district').val(district);
        $('#forestry option:contains(' + forestry + ')').attr('selected', 'selected');
        $('#hasRoof').prop('checked', hasRoof);
        $('#description').val(description);
        $('#createdDate').val(createdDate);
        $('#removedDate').val(removedDate);

        $('#addPulpitModal').data('id', id).modal('show');
    });

    $('.deletePulpit').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var number = row.find('td:nth-child(2)').text().trim();
        var section = row.find('td:nth-child(3)').text().trim();
        var district = row.find('td:nth-child(4)').text().trim();
        var forestry = row.find('td:nth-child(5)').text().trim();

        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć ambonę numer ' + number + ' z oddziału ' + section + ', rewir ' + district + ', leśnictwo ' + forestry + '?');
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
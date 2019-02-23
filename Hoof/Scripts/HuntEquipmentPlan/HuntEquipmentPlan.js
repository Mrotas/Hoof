var HuntEquipmentPlan = function(config) {

    var marketingYearModel = config.MarketingYearModel;

    var showAlert = function (message) {
        
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function (button) {
        var isValid = true;
        var row = $(button).closest('tr');

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
            showAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getHuntEquipmentPlanModel = function (button) {
        var row = $(button).closest('tr');
        var huntEquipmentType = row.find('td:nth-child(2)').find('select').val();
        var huntEquipmentCountPlan = row.find('td:nth-child(3)').find('input').val();

        return {
            Type: huntEquipmentType,
            Count: huntEquipmentCountPlan
        };
    };

    $('#closeAlert').on('click', function() {
        $('#alert').hide();
    });

    $('#addHuntEquipmentPlan').on('click', function () {

        var isFormValid = validateForm(this);

        if (isFormValid) {
            $.ajax({
                type: "POST",
                url: '/HuntEquipmentPlan/Add',
                data: {
                    model: getHuntEquipmentPlanModel(this),
                    marketingYearId: marketingYearModel.Id
                },
                dataType: 'json',
                success: function(data) {
                    if (data.message !== '') {
                        showAlert(data.message);
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
}
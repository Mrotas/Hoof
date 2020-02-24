var AnnualPlan = function(config) {

    var marketingYearId = config.MarketingYearId;

    $('#approveAnnualPlan').on('click', function() {
        $.ajax({
            type: 'POST',
            url: '/AnnualPlanStatus/ApproveToNextStatus',
            data: {
                'marketingYearId': marketingYearId
            },
            success: function(data) {
                location.reload();
            },
            error: function(data) {
                alert('Wystąpił błąd podczas aktualizacji statusu Rocznego Planu. Proszę spróbować ponownie.');
            }
        });
    });

    $('#rejectAnnualPlan').on('click', function () {
        $.ajax({
            type: 'POST',
            url: '/AnnualPlanStatus/RejectAnnualPlan',
            data: {
                'marketingYearId': marketingYearId
            },
            success: function (data) {
                if (data !== '') {
                    alert(data);
                } else {
                    location.reload();
                }
            },
            error: function (data) {
                alert('Wystąpił błąd podczas aktualizacji statusu Rocznego Planu. Proszę spróbować ponownie.');
            }
        });
    });
};
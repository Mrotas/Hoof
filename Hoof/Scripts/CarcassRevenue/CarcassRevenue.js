var CarcassRevenue = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function () {
        var isValid = true;

        var isHuntedGameSelected = $("input[name='huntedGame']:checked").val();
        if (!isHuntedGameSelected) {
            showDangerAlert('Proszę zaznaczyć upolowaną zwierzynę');
            return false;
        } else {
            $('#alert').hide();
        }

        var carcassWeight = $('#carcassWeight');
        if (carcassWeight.val() === '') {
            carcassWeight.addClass('has-error');
            isValid = false;
        } else {
            carcassWeight.removeClass('has-error');
        }

        var revenue = $('#revenue');
        if (revenue.val() === '') {
            revenue.addClass('has-error');
            isValid = false;
        } else {
            revenue.removeClass('has-error');
        }

        if (isValid) {
            carcassWeight.removeClass('has-error');
            revenue.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getCarcassRevenueModel = function () {
        var id = $('#addCarcassRevenueModal').data('id');
        var huntedGameId = $("input[name='huntedGame']:checked").attr('value');
        var carcassWeight = $('#carcassWeight').val();
        var revenue = $('#revenue').val();

        return {
            Id: id,
            HuntedGameId: huntedGameId,
            CarcassWeight: carcassWeight,
            Revenue: revenue
        };
    };
    
    var dateTimeReviver = function (value) {
        var a;
        if (typeof value === 'string') {
            a = /\/Date\((\d*)\)\//.exec(value);
            if (a) {
                return new Date(+a[1]);
            }
        }
        return value;
    }

    var populateHuntedGameTable = function () {
        $.ajax({
            type: 'GET',
            url: '/Hunt/GetByMarketingYear',
            data: {
                marketingYearId: marketingYearId
            },
            dataType: 'json',
            async: false,
            success: function (data) {
                $('#huntedGameTable').dataTable({
                    'scrollY': '300px',
                    'scrollCollapse': true,
                    'paging': false,
                    'info': false,
                    "language": {
                        "lengthMenu": "Pokaż _MENU_ wierszy na stronę",
                        "zeroRecords": "Nie znaleziono żadnej upolowanej zwierzyny",
                        "info": "Strona _PAGE_ z _PAGES_",
                        "infoEmpty": "Brak upolowanych zwierzyn",
                        "infoFiltered": "(znaleziono z _MAX_ upolowanych zwierzyn)",
                        "search": "Szukaj:",
                        "paginate": {
                            "first": "Pierwsza",
                            "last": "Ostatnia",
                            "next": "Następna",
                            "previous": "Poprzednia"
                        }
                    },
                    "aaData": data,
                    "aoColumns": [
                        {
                            "sTitle": "",
                            "mData": "HuntedGameId",
                            "mRender": function (data) {
                                return '<input type="radio" name="huntedGame" value="' + data + '" style="width: 100%">';
                            },
                            "bSortable": false,
                            'width': '5%'
                        },
                        {
                            "sTitle": "Myśliwy",
                            "mData": "Huntsman",
                            'width': '25%'
                        },
                        {
                            "sTitle": "Zwierzyna",
                            "mData": "GameKindName",
                            'width': '20%'
                        },
                        {
                            "sTitle": "Rodzaj",
                            "mData": "GameSubKindName",
                            'width': '10%'
                        },
                        {
                            "sTitle": "Klasa",
                            "mData": "GameClass",
                            'width': '15%'
                        },
                        {
                            "sTitle": "Waga",
                            "mData": "GameWeight",
                            'width': '10%'
                        },
                        {
                            "sTitle": "Polowanie",
                            "mData": "Date",
                            "mRender": function (data) {
                                var jsonDate = dateTimeReviver(data);
                                return jsonDate.getDate() + '/' + (jsonDate.getMonth() + 1) + '/' + jsonDate.getFullYear();
                            },
                            'width': '15%'
                        }
                    ],
                    "destroy": true
                });
            },
            error: function (data) {
                showDangerAlert(data.statusText);
            }
        });
    };

    var clearModal = function () {
        $('#carcassWeight').val('');
        $('#carcassWeight').removeClass('has-error');
        $('#revenue').val('');
        $('#revenue').removeClass('has-error');
        $.removeData($('#addCarcassRevenueModal'), 'id');

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

    $("#addCarcassRevenueModal").on("hidden.bs.modal", function () {
        clearModal();
    });

    $('#add').on('click', function () {
        $('#addCarcassRevenueModal').modal('show');
        populateHuntedGameTable();
    });

    $('#save').on('click', function () {
        var isFormValid = validateForm();
        var url;
        var id = $('#addCarcassRevenueModal').data('id');
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
                    model: getCarcassRevenueModel(),
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
            $.removeData($('#addCarcassRevenueModal'), 'id');
        }
    });

    $('.editCarcassRevenue').on('click', function () {
        $('#addCarcassRevenueModal').modal('show');
        populateHuntedGameTable();

        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var huntedGameId = row.find('td:nth-child(2)').text().trim();
        var carcassWeight = row.find('td:nth-child(6)').text().trim();
        var revenue = row.find('td:nth-child(8)').text().trim();

        $('#carcassWeight').val(carcassWeight);
        $('#revenue').val(revenue);

        var rows = $('#huntedGameTable').find('tr');
        $.each(rows, function(index, row) {
            var huntedGameCheckbox = $(row).find('td:nth-child(1)').find('input');
            if (huntedGameCheckbox.attr('value') === huntedGameId) {
                huntedGameCheckbox.prop("checked", true);
                $('.dataTables_scrollBody').animate({
                    scrollTop: $('#huntedGameTable tbody tr').eq(index).offset().top - 400
                }, 1000);
                $(row).css('background-color', "rgba(222, 247, 222, 0.6)");
                return;
            }
        });

        $('#addCarcassRevenueModal').data('id', id);
    });

    $('.deleteCarcassRevenue').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var gameKindName = row.find('td:nth-child(3)').text().trim();
        var gameSubKindName = row.find('td:nth-child(4)').text().trim();
        var gameClass = row.find('td:nth-child(5)').text().trim();
        var revenue = row.find('td:nth-child(8)').text().trim();

        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć przychód ze sprzedaży tuszy ' + gameKindName + ' - ' + gameSubKindName + ' - ' + gameClass + ', ' + revenue + 'zł?');
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
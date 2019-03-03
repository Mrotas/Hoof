var GameHuntPlan = function (config) {

    var marketingYearId = config.MarketingYearId;
    var controller = config.Controller;
    var previousTableCellValue = [];

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function (row) {
        var isValid = true;

        var kind = row.find('td:nth-child(2)').find('select');
        if (kind.val() === '') {
            kind.addClass('has-error');
            isValid = false;
        } else {
            kind.removeClass('has-error');
        }

        var subKind = row.find('td:nth-child(3)').find('select');
        if (subKind.children('option').length > 1 && subKind.val() === '') {
            subKind.addClass('has-error');
            isValid = false;
        } else {
            subKind.removeClass('has-error');
        }

        var gameClass = row.find('td:nth-child(4)').find('select');
        if (gameClass.children('option').length > 1 && gameClass.val() === '') {
            gameClass.addClass('has-error');
            isValid = false;
        } else {
            gameClass.removeClass('has-error');
        }

        var cull = row.find('td:nth-child(5)').find('input');
        if (cull.val() === '') {
            cull.addClass('has-error');
            isValid = false;
        } else {
            cull.removeClass('has-error');
        }

        var gameCatch = row.find('td:nth-child(6)').find('input');
        if (gameCatch.val() === '') {
            gameCatch.addClass('has-error');
            isValid = false;
        } else {
            gameCatch.removeClass('has-error');
        }

        if (isValid) {
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getGameHuntPlanModel = function (row, isNew) {
        var id, gameType, gameId, kind, kindName, subKind, subKindName, gameClass, gameClassName, cull, gameCatch;
        if (isNew) {
            kind = row.find('td:nth-child(2)').find('select').val();
            kindName = row.find('td:nth-child(2)').find('select option:selected').text();
            subKind = row.find('td:nth-child(3)').find('select').val();
            subKindName = row.find('td:nth-child(3)').find('select option:selected').text();
            gameClass = row.find('td:nth-child(4)').find('select').val();
            gameClassName = row.find('td:nth-child(4)').find('select option:selected').text();
            cull = row.find('td:nth-child(5)').find('input').val();
            gameCatch = row.find('td:nth-child(6)').find('input').val();
        } else {
            id = row.find('td:nth-child(1)').text();
            gameType = row.find('td:nth-child(2)').text();
            gameId = row.find('td:nth-child(3)').text();
            kindName = row.find('td:nth-child(4)').text();
            subKindName = row.find('td:nth-child(5)').text();
            gameClassName = row.find('td:nth-child(6)').text();
            cull = row.find('td:nth-child(7)').text();
            gameCatch = row.find('td:nth-child(8)').text();
        }

        return {
            Id: id,
            GameType: gameType,
            GameId: gameId,
            GameKind: kind,
            GameKindName: kindName,
            GameSubKind: subKind,
            GameSubKindName: subKindName,
            Class: gameClass,
            ClassName: gameClassName,
            Cull: cull,
            Catch: gameCatch
        };
    };

    var makeRowEditable = function (button) {
        var row = $(button).closest('tr');
        $(row).addClass('editable');

        var tdsToEdit = $(row).find('td.canEdit');
        $.each(tdsToEdit, function (index, td) {
            previousTableCellValue.push($(td).text());
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
                $(cell).text(previousTableCellValue[index]);
            }
            $(cell).attr('contenteditable', false);
            $(cell).removeClass('editable');
        });

        previousTableCellValue = [];
        $('#save').attr('disabled', true);
        $('#cancel').attr('disabled', true);
    }

    var clearRow = function (row) {
        var tdsToClear = row.find('td');
        $.each(tdsToClear, function (index, td) {
            var select = $(td).find('select');
            if (select.length > 0) {
                if (index === 1) {
                    select.val(null);
                } else {
                    select.empty().append("<option></option>");
                }
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
        $.each(buttons, function (index, button) {
            $(button).attr('disabled', isEditMode);
        });

        $('#save').attr('disabled', !isEditMode);
        $('#cancel').attr('disabled', !isEditMode);

    }

    var fillGameSubKindSelect = function(data) {
        $.each(data, function (index, game) {
            if (game.SubKind !== null) {
                $('#gameSubKind')
                    .append($("<option></option>")
                        .attr("value", game.SubKind)
                        .text(game.SubKindName)); 
            }
        });
    };
    
    $('.clearSearch').on('click', function () {
        var row = $(this).closest('tr');
        clearRow(row);
    });

    $('#closeDangerAlert').on('click', function () {
        $('#dangerAlert').hide();
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

    $("input[name='gameType']").on('change', function() {
        var gameType = $(this).val();
        sessionStorage.setItem("FilteredGameType", $(this).attr('id'));
        var rows = $('#planTableBody').find('tr');
        if (gameType === '0') {
            $.each(rows, function(index, row) {
                $(row).attr('hidden', false);
            });
        } else {
            $.each(rows, function (index, row) {
                var filteredGameType = $(row).find('td:nth-child(2)').text();
                filteredGameType = filteredGameType.replace(/\s/g, '');
                $(row).attr('hidden', !(gameType === filteredGameType));
            });
        }
    });

    $("#planTableBody").ready(function () {
        var checkedGameTypeFilterName = sessionStorage.getItem("FilteredGameType");
        $('#' + checkedGameTypeFilterName).prop('checked', true).change();
    });

    $('#gameKind').on('change', function () {
        $('#gameSubKind').empty().append("<option></option>");
        $('#gameClass').empty().append("<option></option>");
        var gameKindName = $(this).find(":selected").text();
        if (gameKindName !== '') {
            $.ajax({
                type: "GET",
                url: '/Game/GetGameModelsByGameKindName',
                data: {
                    gameKindname: gameKindName
                },
                dataType: 'json',
                success: function (data) {
                    if (typeof (data) === 'undefined') {
                        showDangerAlert(data.message);
                    } else {
                        fillGameSubKindSelect(data);
                    }
                },
                error: function () {
                    alert('Coś poszło nie tak, proszę odświeżyć stronę.');
                }
            });
        }
    });

    $('#gameSubKind').on('change', function () {
        $('#gameClass').empty().append("<option></option>");
        var gameSubKindName = $(this).find(":selected").text();
        if (gameSubKindName === 'Byk' || gameSubKindName === 'Kozioł') {
            $('#gameClass')
                .append($("<option></option>")
                    .attr("value", 4)
                    .text('Selekcyjne'))
                .append($("<option></option>")
                    .attr("value", 5)
                    .text('Łowne'));;
        }
    });

    $('#save').on('click', function () {
        var rowInEditProcess = $('#planTable').find('tr.editable');

        var isValid = validateForm(rowInEditProcess);
        if (isValid) {
            $.ajax({
                type: "POST",
                url: '/' + controller + '/Edit',
                data: {
                    model: getGameHuntPlanModel(rowInEditProcess, false),
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
                    model: getGameHuntPlanModel(row, true),
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
        var row = $(this).closest('tr');
        var model = getGameHuntPlanModel(row, false);
        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć plan ' + model.GameKindName + ' - ' + model.GameSubKindName + ' - ' + model.ClassName + '?');
        $('#confirmDeleteModal').data('id', model.Id).modal('show');
    });

    $('#confirmDelete').on('click', function () {
        var id = $('#confirmDeleteModal').data('id');

        $.ajax({
            type: "POST",
            url: '/' + controller + '/Delete',
            data: {
                id: id
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
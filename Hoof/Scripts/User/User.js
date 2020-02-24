var User = function (config) {
    
    var controller = config.Controller;

    var showSuccessAlert = function (message) {
        $('.alert-success').find('.success-alert-body').text(message);
        $('.alert-success').show();
    }

    var showDangerAlert = function (message) {
        $('.alert-danger').find('.alert-body').text(message);
        $('.alert-danger').show();
    }

    var validateForm = function () {
        var isValid = true;

        var name = $('#name');
        if (name.val() === '') {
            name.addClass('has-error');
            isValid = false;
        }

        var lastName = $('#lastName');
        if (lastName.val() === '') {
            lastName.addClass('has-error');
            isValid = false;
        }

        var role = $('#role');
        if (role.val() === '') {
            role.addClass('has-error');
            isValid = false;
        }

        var email = $("#email");
        if (email.val() === '') {
            email.addClass('has-error');
            isValid = false;
        }
        
        if (isValid) {
            name.removeClass('has-error');
            lastName.removeClass('has-error');
            role.removeClass('has-error');
            email.removeClass('has-error');
            $('#alert').hide();
        } else {
            showDangerAlert('Proszę wypełnić wymagane pola !');
        }

        return isValid;
    };

    var getUserModel = function (isNew) {
        var id = $('#addUserModal').data('id');
        var name = $('#name').val();
        var lastName = $('#lastName').val();
        var role = $("#role option:selected").text();
        var email = $('#email').val();

        var model = {
            Name: name,
            LastName: lastName,
            Role: role,
            Email: email
        };

        if (isNew) {
            model.Id = id;
        }

        return model;
    };
    
    var clearModal = function () {
        $('#name').val('');
        $('#lastName').val('');
        $("#role").val('');
        $('#email').val('');
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
        var id = $('#addUserModal').data('id');
        var isNew = typeof (id) === 'undefined';
        if (isNew) {
            url = '/' + controller + '/Create';
        } else {
            url = '/' + controller + '/Edit';
        }

        if (isFormValid) {
            $.ajax({
                type: "POST",
                url: url,
                data: {
                    model: getUserModel(isNew)
                },
                dataType: 'json',
                success: function (data) {
                    if (url.includes('Create') && data.created) {
                        showSuccessAlert("Pomyślnie utworzono konto użytkownika!");
                        return;
                    } 
                    else if (data.message !== '') {
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
            $.removeData($('#addUserModal'), 'id');
        }
    });

    $('.editUser').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var name = row.find('td:nth-child(3)').text().trim();
        var lastName = row.find('td:nth-child(4)').text().trim();
        var role = row.find('td:nth-child(5)').text().trim();
        var email = row.find('td:nth-child(6)').text().trim();

        $('#name').val(name);
        $('#lastName').val(lastName);
        $('#role option:contains(' + role + ')').attr('selected', 'selected');
        $('#email').val(email);

        $('#addUserModal').data('id', id).modal('show');
    });

    $('.deleteUser').on('click', function () {
        var row = $(this).closest('tr');
        var id = row.find('td:nth-child(1)').text().trim();
        var name = row.find('td:nth-child(3)').text().trim();
        var lastName = row.find('td:nth-child(4)').text().trim();

        $('#confirmDeleteModalBody').text('Czy na pewno chcesz usunąć użytkownika ' + name + ' ' + lastName + '?');
        $('#confirmDeleteModal').data('id', id).modal('show');
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
    });
}
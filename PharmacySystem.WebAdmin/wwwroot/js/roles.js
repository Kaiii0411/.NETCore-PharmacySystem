$(document).ready(function () {
    $('#btnSubmitCreateRole').click(function () {

        //declare
        var rolename = $('#RoleName').val();
        var description = $('#Description').val();

        //form
        var request = {
            RoleName: rolename,
            Description: description
        }

        $.ajax({
            url: "/Roles/Create",
            type: 'POST',
            data: request,
            success: function (rs) {
                if (rs == 0) {
                    $('#roles-create')[0].reset();
                    alertify.success("Done!");
                }
                else {
                    alertify.error("Error!");
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        })
    });

    $('#btnConfirmUpdateUsers').click(function () {

        //declare
        var id = $('#Roles').val();
        var rolename = $('#RoleName').val();
        var description = $('#Description').val();

        //form
        var request = {
            Id: id,
            RoleName: rolename,
            Description: description
        }

        $.ajax({
            url: "/Roles/Edit",
            type: 'POST',
            data: request,
            success: function (rs) {
                if (rs == 0) {
                    alertify.success("Done!");
                    DisableUpdate();
                }
                else {
                    alertify.error("Error!");
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        })
    });
});

function EnableUpdate() {
    document.getElementById("RoleName").disabled = false;
    document.getElementById("Description").disabled = false;

    document.getElementById("btnConfirmUpdateRoles").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateRoles").style.display = 'inline-block';
    document.getElementById("btnUpdateRoles").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("RoleName").disabled = true;
    document.getElementById("Description").disabled = true;

    document.getElementById("btnConfirmUpdateRoles").style.display = 'none';
    document.getElementById("btnCancelUpdateRoles").style.display = 'none';
    document.getElementById("btnUpdateRoles").style.display = 'inline-block';
}
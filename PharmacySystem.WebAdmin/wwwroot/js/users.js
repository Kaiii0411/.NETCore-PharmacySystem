$(document).ready(function () {
    $('#btnSubmitCreateUser').click(function () {

        //declare
        var username = $('#UserName').val();
        var password = $('#Password').val();
        var confirmpassword = $('#ConfirmPassword').val();
        var idstaff = $('#IdStaff').val();
        var idaccount = $('#IdAccount').val();

        //form
        var request = {
            UserName: username,
            Password: password,
            ConfirmPassword: confirmpassword,
            IdStaff: idstaff,
            IdAccount: idaccount 
        }

        $.ajax({
            url: "/Users/Create",
            type: 'POST',
            data: request,
            success: function (rs) {
                if (rs == 0) {
                    $('#users-create')[0].reset();
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
        var id = $('#Users').val();
        var idstaff = $('#IdStaff').val();
        var idaccount = $('#IdAccount').val();

        //form
        var request = {
            Id: id,
            IdStaff: idstaff,
            IdAccount: idaccount
        }

        $.ajax({
            url: "/Users/Edit",
            type: 'POST',
            data: request ,
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

    $('#btnConfirmDeleteUsers').click(function () {

        //declare
        var id = $('#Users').val();


        //form
        var request = {
            Id: id
        }

        $.ajax({
            url: "/Users/Delete",
            type: 'POST',
            data: request,
            success: function (rs) {
                if (rs.result == 'Redirect') {
                    alertify.success("Done!");
                    window.location = rs.url;
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        })
    });
});

function EnableUpdate() {
    document.getElementById("IdAccount").disabled = false;
    document.getElementById("IdStaff").disabled = false;

    document.getElementById("btnConfirmUpdateUsers").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateUsers").style.display = 'inline-block';
    document.getElementById("btnUpdateUsers").style.display = 'none';
    document.getElementById("btnBackToList").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("IdAccount").disabled = true;
    document.getElementById("IdStaff").disabled = true;

    document.getElementById("btnConfirmUpdateUsers").style.display = 'none';
    document.getElementById("btnCancelUpdateUsers").style.display = 'none';
    document.getElementById("btnUpdateUsers").style.display = 'inline-block';
    document.getElementById("btnBackToList").style.display = 'inline-block';
}
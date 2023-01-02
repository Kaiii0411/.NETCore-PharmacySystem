$(document).ready(function () {

    $('#btnSubmitCreateStaff').click(function () {

        //declare
        var staffname = $('#StaffName').val();
        var dateofbirth = $('#DateOfBirth').val();
        var store = $('#Store').val();
        var phone = $('#Phone').val();
        var email = $('#Email').val();

        //form
        var CreateStaffForm = {
            StaffName: staffname,
            DateOfBirth: dateofbirth,
            Phone: phone,
            Email: email,
            IdStore: store,
            Status: status
        }

        $.ajax({
            url: "/Staff/Create",
            type: 'POST',
            data: CreateStaffForm,
            success: function (rs) {
                if (rs == 0) {
                    $('#staff-create')[0].reset();
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

    $('#btnConfirmUpdateStaff').click(function () {

        //declare
        var staff = $('#Staff').val();
        var staffname = $('#StaffName').val();
        var dateofbirth = $('#DateOfBirth').val();
        var store = $('#Store').val();
        var phone = $('#Phone').val();
        var email = $('#Email').val();

        //form
        var UpdateStaffForm = {
            IdStaff: staff,
            StaffName: staffname,
            DateOfBirth: dateofbirth,
            Phone: phone,
            Email: email,
            IdStore: store,
            Status: status
        }

        $.ajax({
            url: "/Staff/Edit",
            type: 'PUT',
            data: UpdateStaffForm,
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

    $('#btnConfirmDeleteStaff').click(function () {

        //declare
        var staff = $('#Staff').val();


        //form
        var DeleteStaffForm = {
            IdStaff: staff
        }

        $.ajax({
            url: "/Staff/Delete",
            type: 'POST',
            data: DeleteStaffForm,
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

    $('#staff-create').parsley();
});

function EnableUpdate() {
    document.getElementById("StaffName").disabled = false;
    document.getElementById("DateOfBirth").disabled = false;
    document.getElementById("Store").disabled = false;
    document.getElementById("Phone").disabled = false;
    document.getElementById("Email").disabled = false;

    document.getElementById("btnConfirmUpdateStaff").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateStaff").style.display = 'inline-block';
    document.getElementById("btnUpdateStaff").style.display = 'none';
    document.getElementById("btnBackToList").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("StaffName").disabled = true
    document.getElementById("DateOfBirth").disabled = true;
    document.getElementById("Store").disabled = true;
    document.getElementById("Phone").disabled = true;
    document.getElementById("Email").disabled = true;

    document.getElementById("btnConfirmUpdateStaff").style.display = 'none';
    document.getElementById("btnCancelUpdateStaff").style.display = 'none';
    document.getElementById("btnUpdateStaff").style.display = 'inline-block';
    document.getElementById("btnBackToList").style.display = 'inline-block';
}
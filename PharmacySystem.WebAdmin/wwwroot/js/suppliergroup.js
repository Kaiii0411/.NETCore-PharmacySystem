$(document).ready(function () {
    $('#btnSubmitCreateSupplierGroup').click(function () {
        //declare
        var suppliergroupname = $('#SupplierGroupName').val();
        var note = $('#Note').val();

        //form
        var CreateSupplierGroupForm = {
            SupplierGroupName: suppliergroupname,
            Note: note
        }

        $.ajax({
            url: "/SupplierGroup/Create",
            type: 'POST',
            data: CreateSupplierGroupForm,
            success: function (rs) {
                if (rs == 0) {
                    $('#suppliergroup-create')[0].reset();
                    alertify.success("Done!");
                    location.reload(true);
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

    $('#btnConfirmUpdateSupplierGroup').click(function () {

        //declare
        var suppliergroupid = $('#SupplierGroup').val();
        var suppliergroupname = $('#SupplierGroupName').val();
        var note = $('#Note').val();

        //form
        var UpdateSupplierGroupForm = {
            IdSupplierGroup: suppliergroupid,
            SupplierGroupName: suppliergroupname,
            Note: note
        }

        $.ajax({
            url: "/SupplierGroup/Edit",
            type: 'PUT',
            data: UpdateSupplierGroupForm,
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

    $('#btnConfirmDeleteSupplierGroup').click(function () {

        //declare
        var suppliergroupid = $('#SupplierGroup').val();

        //form
        var DeleteSupplierGroupForm = {
            IdSupplierGroup: suppliergroupid
        }

        $.ajax({
            url: "/SupplierGroup/Delete",
            type: 'POST',
            data: DeleteSupplierGroupForm,
            success: function (rs) {
                if (rs.result == 'Redirect') {
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
    document.getElementById("SupplierGroupName").disabled = false;
    document.getElementById("Note").disabled = false;

    document.getElementById("btnConfirmUpdateSupplierGroup").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateSupplierGroup").style.display = 'inline-block';
    document.getElementById("btnUpdateSupplierGroup").style.display = 'none';
    document.getElementById("btnBackToList").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("SupplierGroupName").disabled = true;
    document.getElementById("Note").disabled = true;

    document.getElementById("btnConfirmUpdateSupplierGroup").style.display = 'none';
    document.getElementById("btnCancelUpdateSupplierGroup").style.display = 'none';
    document.getElementById("btnUpdateSupplierGroup").style.display = 'inline-block';
    document.getElementById("btnBackToList").style.display = 'inline-block';
}
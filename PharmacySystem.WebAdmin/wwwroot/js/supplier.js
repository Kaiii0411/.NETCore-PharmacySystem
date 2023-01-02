$(document).ready(function () {
    $('#btnSubmitCreateSupplier').click(function () {
        //declare
        var suppliername = $('#SupplierName').val();
        var suppliergroup = $('#SupplierGroup').val();
        var phone = $('#Phone').val();
        var email = $('#Email').val();
        var address = $('#Address').val();

        //form
        var CreateSupllierForm = {
            IdSupplierGroup: suppliergroup,
            SupplierName: suppliername,
            Phone : phone,
            Email : email,
            Address : address
        }

        $.ajax({
            url: "/Supplier/Create",
            type: 'POST',
            data: CreateSupllierForm,
            success: function (rs) {
                if (rs == 0) {
                    $('#supplier-create')[0].reset();
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
    })

    $('#btnConfirmUpdateSupplier').click(function () {

        //declare
        var supplierid = $('#Supplier').val();
        var suppliername = $('#SupplierName').val();
        var suppliergroup = $('#SupplierGroup').val();
        var phone = $('#Phone').val();
        var email = $('#Email').val();
        var address = $('#Address').val();

        //form
        var UpdateSupplierForm = {
            IdSupplier: supplierid,
            SupplierName: suppliername,
            Address: address,
            Phone: phone,
            Email: email,
            IdSupplierGroup: suppliergroup,
        }

        $.ajax({
            url: "/Supplier/Edit",
            type: 'PUT',
            data: UpdateSupplierForm,
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

    $('#btnConfirmDeleteSupplier').click(function () {

        //declare
        var supplierid = $('#Supplier').val();

        //form
        var DeleteSupplierForm = {
            IdSupplier: supplierid
        }

        $.ajax({
            url: "/Supplier/Delete",
            type: 'POST',
            data: DeleteSupplierForm,
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

    $("#ApproveInvoice").click(function () {

        //declare
        var invoiceid = $('#InvoiceId').val();
        var status = $('#Status').val();

        //form
        var ProcessForm = {
            IdInvoice: invoiceid,
            Status: status
        }

        $.ajax({
            url: "/Supplier/UpdateProcess",
            type: 'PUT',
            data: ProcessForm,
            success: function (rs) {
                if (rs == 0) {
                    alertify.success("Done!");
                    window.location.reload();
                }
                else {
                    alertify.error("Error!");
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        });
    });
});

function EnableUpdate() {
    document.getElementById("SupplierName").disabled = false;
    document.getElementById("SupplierGroup").disabled = false;
    document.getElementById("Phone").disabled = false;
    document.getElementById("Email").disabled = false;
    document.getElementById("Address").disabled = false;

    document.getElementById("btnConfirmUpdateSupplier").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateSupplier").style.display = 'inline-block';
    document.getElementById("btnUpdateSupplier").style.display = 'none';
    document.getElementById("btnBackToList").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("SupplierName").disabled = true;
    document.getElementById("SupplierGroup").disabled = true;
    document.getElementById("Phone").disabled = true;
    document.getElementById("Email").disabled = true;
    document.getElementById("Address").disabled = true;

    document.getElementById("btnConfirmUpdateSupplier").style.display = 'none';
    document.getElementById("btnCancelUpdateSupplier").style.display = 'none';
    document.getElementById("btnUpdateSupplier").style.display = 'inline-block';
    document.getElementById("btnBackToList").style.display = 'inline-block';
}

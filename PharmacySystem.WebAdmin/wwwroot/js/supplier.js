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
});
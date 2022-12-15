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

});
$(document).ready(function () {
    $('#btnAddEInvoiceItems').click(function () {
        //declare
        var idmedicine = $('#idMedicine').val();
        var quantity = $('#Quantity').val();

        $.ajax({
            url: "/ExportInvoice/AddToInvoice",
            type: 'POST',
            data: { id: idmedicine, quantity: quantity },
            success: function (res) {
                alertify.success("Done!");
                $("#datatable-buttons").load(window.location + " #datatable-buttons");
            },
            error: function (err) {
                alertify.error("Not receiving data!");
            }
        })
    });

    $(function () {
        $('#idMedicine').select2();
    });

    $('#btnCreateEInvoice').click(function () {
        $.ajax({
            url: "/ExportInvoice/Create",
            type: 'POST',
            success: function (res) {
                alertify.success("Done!");
            },
            error: function (err) {
                alertify.error("Not receiving data!");
            }
        })
    });
});
function LoadInvoice() {
    $.ajax({
        url: "/ImportInvoice/GetListItems",
        type: 'GET',
        success: function (res) {

        }
    })
}

$(document).ready(function () {
    $('#btnAddIInvoiceItems').click(function () {
        //declare
        var idmedicine = $('#idMedicine').val();
        var quantity = $('#Quantity').val();

        $.ajax({
            url: "/ImportInvoice/AddToInvoice",
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
});
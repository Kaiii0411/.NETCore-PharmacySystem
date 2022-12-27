$(document).ready(function () {
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
            url: "/MainInvoice/UpdateProcess",
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
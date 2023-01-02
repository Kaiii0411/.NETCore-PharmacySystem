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

    $("#ConfirmRejectInvoice").click(function () {
        console.log('test');
        //declare
        var invoiceid = $('#InvoiceId').val();
        var status = $('#Status').val();
        var note = $('#Note').val();

        //form
        var ProcessForm = {
            IdInvoice: invoiceid,
            Status: status,
            Note: note
        }

        $.ajax({
            url: "/MainInvoice/RejectProcess",
            type: 'PUT',
            data: ProcessForm,
            success: function (rs) {
                if (rs == 0) {
                    alertify.success("Done!");
                    DisableReject();
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

function EnableReject() {
    document.getElementById("Note").disabled = false;
    document.getElementById("ConfirmRejectInvoice").style.display = 'inline-block';
    document.getElementById("CancelRejectInvoice").style.display = 'inline-block';
    document.getElementById("ApproveInvoice").style.display = 'none';
    document.getElementById("PrintInvoice").style.display = 'none';
    document.getElementById("RejectInvoice").style.display = 'none';
}

function DisableReject() {
    document.getElementById("Note").disabled = true;
    document.getElementById("ConfirmRejectInvoice").style.display = 'none';
    document.getElementById("CancelRejectInvoice").style.display = 'none';
    document.getElementById("ApproveInvoice").style.display = 'inline-block';
    document.getElementById("PrintInvoice").style.display = 'inline-block';
    document.getElementById("RejectInvoice").style.display = 'inline-block';
}
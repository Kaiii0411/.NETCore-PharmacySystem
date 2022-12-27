function ShowUpdate() {
    document.getElementById("editButton").style.display = 'none';
    document.getElementById("newQuantity").style.display = 'revert';
    document.getElementById("regularQuantity").style.display = 'none';
    document.getElementById("updateButton").style.display = 'inline-block';
    document.getElementById("cancelButton").style.display = 'inline-block';
    document.getElementById("deleteButton").style.display = 'none';
}

function CancelUpdate() {
    document.getElementById("editButton").style.display = 'inline-block';
    document.getElementById("regularQuantity").style.display = 'revert';
    document.getElementById("newQuantity").style.display = 'none';
    document.getElementById("updateButton").style.display = 'none';
    document.getElementById("cancelButton").style.display = 'none';
    document.getElementById("deleteButton").style.display = 'inline-block';
}

function UpdateInvoice() {
    var idmedicine = $('#idMedicine').val();
    var quantity = $('#newQuantity').val();

    $.ajax({
        url: "/ImportInvoice/UpdateInvoice",
        type: 'POST',
        data: { id: idmedicine, quantity: quantity },
        success: function (res) {
            alertify.success("Done!");
            $("#datatablecreateiinvoice").load(window.location + " #datatablecreateiinvoice");
        },
        error: function (err) {
            alertify.error("Not receiving data!");
        }
    })
}

function DeleteItemsInvoice() {
    var idmedicine = $('#idMedicine').val();

    $.ajax({
        url: "/ImportInvoice/RemoveItemsInvoice",
        type: 'POST',
        data: { id: idmedicine},
        success: function (res) {
            alertify.success("Done!");
            $("#datatablecreateiinvoice").load(window.location + " #datatablecreateiinvoice");
        },
        error: function (err) {
            alertify.error("Not receiving data!");
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
                $("#datatablecreateiinvoice").load(window.location + " #datatablecreateiinvoice");
            },
            error: function (err) {
                alertify.error("Not receiving data!");
            }
        })
    });

    $(function () {
        $('#idMedicine').select2();
    });

    $('#btnCreateIInvoice').click(function () {
        //declare
        var idsupllier = $('#idSupllier').val();

        var CreateIInvoiceForm = {
            IdSupplier: idsupllier
        }

        $.ajax({
            url: "/ImportInvoice/Create",
            type: 'POST',
            data: CreateIInvoiceForm,
            success: function (res) {
                alertify.success("Done!");
            },
            error: function (err) {
                alertify.error("Not receiving data!");
            }
        })
    });

    $("#idSupllier").on("change", function () {
        $.ajax({
            type: "POST",
            url: "/ImportInvoice/GetListMedicineByGroupId",
            data: { idSupplier: $("#idSupllier").val() },
            success: function (d) {
                var items = "";
                $(d).each(function () {
                    items += "<option value=" + this.value + ">" + this.text + "</option>";
                })
                $("#idMedicine").html(items);
            },
            failure: function (d) {
                alert(d.responseText);
            },
            error: function (d) {
                alert(d.responseText);
            }
        });
    })
});
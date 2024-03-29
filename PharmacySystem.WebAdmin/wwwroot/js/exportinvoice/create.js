﻿function ShowUpdate() {
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
        url: "/ExportInvoice/UpdateInvoice",
        type: 'POST',
        data: { id: idmedicine, quantity: quantity },
        success: function (res) {
            alertify.success("Done!");
            $("#datatablecreateeinvoice").load(window.location + " #datatablecreateeinvoice");
        },
        error: function (err) {
            alertify.error("Not receiving data!");
        }
    })
}

function DeleteItemsInvoice() {
    var idmedicine = $('#idMedicine').val();

    $.ajax({
        url: "/ExportInvoice/RemoveItemsInvoice",
        type: 'POST',
        data: { id: idmedicine },
        success: function (res) {
            alertify.success("Done!");
            $("#datatablecreateeinvoice").load(window.location + " #datatablecreateeinvoice");
        },
        error: function (err) {
            alertify.error("Not receiving data!");
        }
    })
}

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
                $("#datatablecreateeinvoice").load(window.location + " #datatablecreateeinvoice");
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

    $('#btnCheckOutEInvoice').click(function () {
        $.ajax({
            url: "/ExportInvoice/CheckOut",
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
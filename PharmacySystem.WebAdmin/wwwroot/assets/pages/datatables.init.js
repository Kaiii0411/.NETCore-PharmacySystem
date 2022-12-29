/*
 Template Name: Drixo - Bootstrap 4 Admin Dashboard
 Author: Themesdesign
 Website: www.themesdesign.in
 File: Datatable js
 */

$(document).ready(function() {
    $('#datatable').DataTable();

    //CreateMedicine
    var table = $('#datatable-buttons').DataTable({
        lengthChange: false,
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'MedicinesList'
            },
            {
                extend: 'pdfHtml5',
                title: 'MedicinesList'
            },
        ]
    });

    table.buttons().container()
        .appendTo('#datatable-buttons_wrapper .col-md-6:eq(0)');

    $('#datatablemedicine').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Medicines-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'Medicines-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
    });

    $('#datatablesupplier').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Supplier-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'Supplier-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
    });

    $('#datatablesuppliergroup').DataTable({
        dom: 'frtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true
    });

    $('#datatablemedicinegroup').DataTable({
        dom: 'frtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true
    });

    $('#datatableimportinvoice').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'ImportInvoice-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'ImportInvoice-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
    });

    $('#datatableexportinvoice').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'ExportInvoice-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'ExportInvoice-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
    });

    $('#datatablestore').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Store-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'Store-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
    });

    $('#datatablestaff').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Staff-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'Staff-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
    });

    $('#datatablecreateiinvoice').DataTable({
        dom: 'frtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true
    });

    $('#datatableinvoicedetails').DataTable({
        dom: 'frtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true
    });
    $('#datatablecreateeinvoice').DataTable({
        dom: 'frtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true
    });

    $('#datatableusers').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Users-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'Users-List',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
    });
} );
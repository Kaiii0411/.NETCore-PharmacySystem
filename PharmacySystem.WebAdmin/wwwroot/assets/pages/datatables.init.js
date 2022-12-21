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
        //buttons: ['copy', 'excel', 'pdf', 'colvis']
    });

    table.buttons().container()
        .appendTo('#datatable-buttons_wrapper .col-md-6:eq(0)');

    var tablemedicine = $('#datatablemedicine').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        "bDestroy": true,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'MedicinesList',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
            {
                extend: 'pdfHtml5',
                title: 'MedicinesList',
                className: 'btn btn-primary btn-lg waves-effect waves-light'
            },
        ]
        //buttons: ['copy', 'excel', 'pdf', 'colvis']
    });
} );
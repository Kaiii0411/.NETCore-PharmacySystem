/*
 Template Name: Drixo - Bootstrap 4 Admin Dashboard
 Author: Themesdesign
 Website: www.themesdesign.in
 File: Datatable js
 */

$(document).ready(function() {
    $('#datatable').DataTable();

    //Buttons examples
    var table = $('#datatable-buttons').DataTable({
        lengthChange: false,
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Medicines List'
            },
            {
                extend: 'pdfHtml5',
                title: 'Medicines List'
            },
        ]
        //buttons: ['copy', 'excel', 'pdf', 'colvis']
    });

    table.buttons().container()
        .appendTo('#datatable-buttons_wrapper .col-md-6:eq(0)');
} );
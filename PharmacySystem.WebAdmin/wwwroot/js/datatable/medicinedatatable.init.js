$(document).ready(function () {

    //CreateMedicine
    var table = $('#datatablemedicine').DataTable({
        dom: 'Bfrtip',
        lengthChange: false,
        searching: false,
        "showNEntries": true,
        "info": false,
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
});
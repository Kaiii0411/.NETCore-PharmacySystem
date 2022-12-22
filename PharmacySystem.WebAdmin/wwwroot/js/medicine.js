
function ShowDetails(id) {
    var Id = id;
    $.ajax({
        type: "POST",
        url: "/Medicine/Edit",
        data: { id: Id },

        success: function (response) {
            $("#partialModal").find(".modal-body").html(response);
            $("#partialModal").modal('show');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};

$(document).ready(function () {

    var table = $('#datatablemedicine').DataTable({
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

    $("refreshButton").on("click", function () {
        table.ajax.reload(null, false);
    });

    $('#btnSubmitCreateMedicine').click(function () {

        //declare
        var medicinegroup = $('#MedicineGroup').val();
        var medicinename = $('#MedicineName').val();
        var expirydate = $('#ExpiryDate').val();
        var supplier = $('#Supplier').val();
        var quantity = $('#Quantity').val();
        var unit = $('#Unit').val();
        var importprice = $('#ImportPrice').val();
        var sellprice = $('#SellPrice').val();
        var description = $('#Description').val();

        //form
        var CreateMedicineForm = {
            IdMedicineGroup: medicinegroup,
            MedicineName: medicinename,
            ExpiryDate: expirydate,
            IdSupplier: supplier,
            Quantity: quantity,
            Unit: unit,
            ImportPrice: importprice,
            SellPrice: sellprice,
            Description: description
        }

        $.ajax({
            url: "/Medicine/Create",
            type: 'POST',
            data: CreateMedicineForm,
            success: function (rs) {
                if (rs == 0) {
                    $('#medicine-create')[0].reset();
                    alertify.success("Done!");
                }
                else {
                    alertify.error("Error!");
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        })
    });

    $('#btnConfirmUpdateMedicine').click(function () {

        //declare
        var medicineid = $('#Medicine').val();
        var medicinegroup = $('#MedicineGroup').val();
        var medicinename = $('#MedicineName').val();
        var expirydate = $('#ExpiryDate').val();
        var supplier = $('#Supplier').val();
        var quantity = $('#Quantity').val();
        var unit = $('#Unit').val();
        var importprice = $('#ImportPrice').val();
        var sellprice = $('#SellPrice').val();
        var description = $('#Description').val();

        //form
        var UpdateMedicineForm = {
            IdMedicine: medicineid,
            IdMedicineGroup: medicinegroup,
            MedicineName: medicinename,
            ExpiryDate: expirydate,
            IdSupplier: supplier,
            Quantity: quantity,
            Unit: unit,
            ImportPrice: importprice,
            SellPrice: sellprice,
            Description: description
        }

        $.ajax({
            url: "/Medicine/Edit",
            type: 'PUT',
            data: UpdateMedicineForm,
            success: function (rs) {
                if (rs == 0) {
                    alertify.success("Done!");
                    DisableUpdate();
                }
                else {
                    alertify.error("Error!");
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        })
    });

    $('#btnConfirmDeleteMedicine').click(function () {

        //declare
        var medicineid = $('#Medicine').val();


        //form
        var DeleteMedicineForm = {
            IdMedicine: medicineid
        }

        $.ajax({
            url: "/Medicine/Delete",
            type: 'POST',
            data: DeleteMedicineForm,
            success: function (rs) {
                if (rs.result == 'Redirect') {
                    alertify.success("Done!");
                    window.location = rs.url;
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        })
    });

    $('#medicine-create').parsley();
});

function EnableUpdate() {
    document.getElementById("MedicineGroup").disabled = false;
    document.getElementById("MedicineName").disabled = false;
    document.getElementById("ExpiryDate").disabled = false;
    document.getElementById("Supplier").disabled = false;
    document.getElementById("Quantity").disabled = false;
    document.getElementById("Unit").disabled = false;
    document.getElementById("ImportPrice").disabled = false;
    document.getElementById("SellPrice").disabled = false;
    document.getElementById("Description").disabled = false;

    document.getElementById("btnConfirmUpdateMedicine").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateMedicine").style.display = 'inline-block';
    document.getElementById("btnUpdateMedicine").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("MedicineGroup").disabled = true;
    document.getElementById("MedicineName").disabled = true;
    document.getElementById("ExpiryDate").disabled = true;
    document.getElementById("Supplier").disabled = true;
    document.getElementById("Quantity").disabled = true;
    document.getElementById("Unit").disabled = true;
    document.getElementById("ImportPrice").disabled = true;
    document.getElementById("SellPrice").disabled = true;
    document.getElementById("Description").disabled = true;

    document.getElementById("btnConfirmUpdateMedicine").style.display = 'none';
    document.getElementById("btnCancelUpdateMedicine").style.display = 'none';
    document.getElementById("btnUpdateMedicine").style.display = 'inline-block';
}


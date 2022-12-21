function Refresh() {
    table = $("#datatablemedicine").DataTable();
    table.ajax.reload(null, false);
};

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

$(document).ready(function () {


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
});
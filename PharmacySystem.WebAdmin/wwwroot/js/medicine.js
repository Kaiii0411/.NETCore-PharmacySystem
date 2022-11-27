$(document).ready(function () {
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
    }
    )
});
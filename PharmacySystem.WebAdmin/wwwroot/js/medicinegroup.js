$(document).ready(function () {
    $('#btnSubmitCreateMedicineGroup').click(function () {
        //declare
        var medicinegroupname = $('#MedicineGroupName').val();
        var note = $('#Note').val();

        //form
        var CreateMedicineGroupForm = {
            MedicineGroupName: medicinegroupname,
            Note: note
        }

        $.ajax({
            url: "/MedicineGroup/Create",
            type: 'POST',
            data: CreateMedicineGroupForm,
            success: function (rs) {
                if (rs == 0) {
                    $('#medicinegroup-create')[0].reset();
                    alertify.success("Done!");
                    location.reload(true);
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
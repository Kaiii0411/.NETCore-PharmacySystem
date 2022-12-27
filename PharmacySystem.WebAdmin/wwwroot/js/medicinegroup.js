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

    $('#btnConfirmUpdateMedicineGroup').click(function () {

        //declare
        var medicinegroupid = $('#MedicineGroup').val();
        var medicinegroupname = $('#MedicineGroupName').val();
        var note = $('#Note').val();

        //form
        var UpdateMedicineGroupForm = {
            IdMedicineGroup: medicinegroupid,
            MedicineGroupName: medicinegroupname,
            Note: note
        }

        $.ajax({
            url: "/MedicineGroup/Edit",
            type: 'PUT',
            data: UpdateMedicineGroupForm,
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

    $('#btnConfirmDeleteMedicineGroup').click(function () {

        //declare
        var medicinegroupid = $('#MedicineGroup').val();

        //form
        var DeleteMedicineGroupForm = {
            IdMedicineGroup: medicinegroupid
        }

        $.ajax({
            url: "/MedicineGroup/Delete",
            type: 'POST',
            data: DeleteMedicineGroupForm,
            success: function (rs) {
                if (rs.result == 'Redirect') {
                    window.location = rs.url;
                }
            },
            error: function () {
                alertify.error("Not receiving data!");
            }
        })
    });
});

function EnableUpdate() {
    document.getElementById("MedicineGroupName").disabled = false;
    document.getElementById("Note").disabled = false;

    document.getElementById("btnConfirmUpdateMedicineGroup").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateMedicineGroup").style.display = 'inline-block';
    document.getElementById("btnUpdateMedicineGroup").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("MedicineGroupName").disabled = true;
    document.getElementById("Note").disabled = true;

    document.getElementById("btnConfirmUpdateMedicineGroup").style.display = 'none';
    document.getElementById("btnCancelUpdateMedicineGroup").style.display = 'none';
    document.getElementById("btnUpdateMedicineGroup").style.display = 'inline-block';
}
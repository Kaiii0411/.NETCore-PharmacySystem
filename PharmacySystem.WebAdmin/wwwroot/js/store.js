$(document).ready(function () {

    $('#btnSubmitCreateStore').click(function () {

        //declare
        var storename = $('#StoreName').val();
        var address = $('#Address').val();
        var storeowner = $('#Owner').val();
        var phone = $('#Phone').val();

        //form
        var CreateStoreForm = {
            StoreName: storename,
            Address: address,
            StoreOwner: storeowner,
            Phone: phone
        }

        $.ajax({
            url: "/Store/Create",
            type: 'POST',
            data: CreateStoreForm,
            success: function (rs) {
                if (rs == 0) {
                    $('#store-create')[0].reset();
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

    $('#btnConfirmUpdateStore').click(function () {

        //declare
        var store = $('#Store').val();
        var storename = $('#StoreName').val();
        var address = $('#Address').val();
        var storeowner = $('#Owner').val();
        var phone = $('#Phone').val();

        //form
        var UpdateStoreForm = {
            IdStore: store,
            StoreName: storename,
            Address: address,
            StoreOwner: storeowner,
            Phone: phone
        }

        $.ajax({
            url: "/Store/Edit",
            type: 'PUT',
            data: UpdateStoreForm,
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

    $('#btnConfirmDeleteStore').click(function () {

        //declare
        var store = $('#Store').val();


        //form
        var DeleteStoreForm = {
            IdStore: store
        }

        $.ajax({
            url: "/Store/Delete",
            type: 'POST',
            data: DeleteStoreForm,
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

    $('#store-create').parsley();
});



function EnableUpdate() {
    document.getElementById("StoreName").disabled = false;
    document.getElementById("Address").disabled = false;
    document.getElementById("Owner").disabled = false;
    document.getElementById("Phone").disabled = false;

    document.getElementById("btnConfirmUpdateStore").style.display = 'inline-block';
    document.getElementById("btnCancelUpdateStore").style.display = 'inline-block';
    document.getElementById("btnUpdateStore").style.display = 'none';
}

function DisableUpdate() {
    document.getElementById("StoreName").disabled = true;
    document.getElementById("Address").disabled = true;
    document.getElementById("Owner").disabled = true;
    document.getElementById("Phone").disabled = true;

    document.getElementById("btnConfirmUpdateStore").style.display = 'none';
    document.getElementById("btnCancelUpdateStore").style.display = 'none';
    document.getElementById("btnUpdateStore").style.display = 'inline-block';
}
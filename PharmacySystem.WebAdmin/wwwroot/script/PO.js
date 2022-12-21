

$(document).ready(function () {
    $('.select').select2(
        {
            minimumResultsForSearch: 2,
            placeholder: "Please choose...",
            theme: "classic",
            dropdownAutoWidth: true,
            allowClear: true
        }
    );
    $('.default-date-picker').datepicker(
        {
            container: '.container',
            format: "d/m/yyyy",
            autoclose: true,
            todayHighlight: true
        }
    ).on('change', function () {
        $('.default-date-picker').hide();
    });
    // SmartWizard initialize
    $('#smartwizard').smartWizard(
        {
            selected: 0,
            justified: true,
            autoAdjustHeight: false,
            theme: 'arrows',
            transition: {
                animation: 'none', // Effect on navigation, none/fade/slide-horizontal/slide-vertical/slide-swing
                speed: '400', // Transion animation speed
                easing: '', // Transition animation easing. Not supported without a jQuery easing plugin
                theme: 'arrows'
            },
            lang: {
                next: 'Bước tiếp theo / Next',
                previous: 'Bước trước / Previous'
            },
            onLeaveStep: leaveAStepCallback,
        }
    );
    $("#btnsavepo").click(function (e) {
        AddOrUpdatePO('/PO/Create');
    });
    $("#btneditpo").click(function (e) {
        AddOrUpdatePO('/PO/Edit');
    });
    //$(document).on('click', "#btnsaveoverview", function (e) {
    //    AddOrUpdatePoOverview("/PO/CreateOverView");
    //});
    $(document).on('click', "#btnsaveoverview", function (e) {
        var PoOvID = $("#PoOvID").val();
        if (PoOvID != "") {
            AddOrUpdatePoOverview("/PO/EditOverView")
        }
        else {
            AddOrUpdatePoOverview("/PO/CreateOverView");
        }
    });
    $(document).on('click', "#btnedititemdetail", function (e) {
        AddOrUpdatePoItemDetail("/PO/EditItemDetail")
    });
    $(document).on('click', "#btnsaveitemdetail", function (e) {
        AddOrUpdatePoItemDetail("/PO/CreateItemDetail")
    });
    $(document).on('click', "#save-all", function () {
        var PoID = $("#PoID").val();
        $.ajax({
            url: '/PO/SaveAll',
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ PoID: PoID }),
            success: function (data) {
                if (data.success) {
                    window.location.href = "/PO";
                    alert("Gửi duyệt thành công. PO sẽ được đưa vào quy trình duyệt.");
                }
                if (data.HasErrors) {
                    alert(data.message);
                }
            },
            error: function (xhr, status, err) { }
        });
    });
    $(document).on('click', '#po-approve', function (e) {
        var PoID = $("#PoID").val();
        var PcaKey = $('#PcaKey').val();
        $.ajax({
            url: '/PO/Approved',
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ PcaKey: PcaKey }),
            success: function (data) {
                if (data.success) {
                    alert("Approval success");
                    window.location.href = "/PO/ListApprove";
                }
                if (data.HasErrors) {
                    alert(data.message);
                }
            },
            error: function (xhr, status, err) { }
        });
    });
    $(document).on('click', '#btnclearoverview', function (e) {
        var form = $('#form-po-overview');
        var PoOvID = $('#PoOvID').val();
        if (PoOvID.length > 0) {
            $('#PoOvID').val('');
            ClearFormOververView(form);
        }
        $('#btncancelcreateoverview').show();
        $('.saveoverview').show();
        $('#btnclearoverview').hide();
    });
    $(document).on('click', '#btncancelcreateoverview', function (e) {
        var form = $('#form-po-overview');
        $('#PoOvID').val('');
        $('#btnclearoverview').show();
        $('.saveoverview').hide();
        $(this).hide();
        ClearFormOververView(form);
    });
    $(document).on('click', '#Save-Reject', function (e) {
        var PoID = $("#PoID").val();
        var PcaKey = $('#PcaKey').val();
        var message = $('#Content-Reject').val();
        if (message == '') {
            alert("Please enter the reason for reject");
        }
        $.ajax({
            url: '/PO/Reject',
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ id: parseInt(PoID), PcaKey: PcaKey, Message: message }),
            success: function (data) {
                if (data.success) {
                    alert("Rejected success");
                    window.location.href = "/PO";
                }
                if (data.HasErrors) {
                    alert(data.message);
                }
            },
            error: function (xhr, status, err) { }
        });
    });
    $(document).on('click', '#delete-overview-itm', function (e) {
        debugger;
        var PoOvID = $('#PoOvID-del').val();
        var PoItmID = $('#PoItmID-del').val();
        if (parseInt(PoOvID) > 0) {
            DeleteOverView(PoOvID);
        }
        else if (parseInt(PoItmID) > 0) {
            DeleteItemDetail(PoItmID);
        }
    });
    function DeleteOverView(PoOvID) {
        var PoID = $("#ID").val();
        $.ajax({
            url: "/PO/DeleteOverview",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ PoOvID: PoOvID }),
            success: function (data) {
                if (data.success) {
                    $('#Modal-Delete').modal('hide');
                    $('.list-over-view').load('/PO/RenderListOverView', { PoID: PoID });
                }
                if (data.HasErrors) {
                    alert(data.message);
                    //showErrorMessages(data.Errors, $(form));
                }
            },
            error: function (xhr, status, err) { }
        });
    }
    function DeleteItemDetail(PoItmID) {
        $.ajax({
            url: "/PO/DeleteItemDetail",
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            data: JSON.stringify({ ItmID: PoItmID }),
            success: function (data) {
                if (data.success) {
                    debugger;
                    $('#Modal-Delete').modal('hide');
                    $('.table-item-detail').load('/PO/RenderTableItemDetail', { PoOvID: data.PoOvID });
                }
                if (data.HasErrors) {
                    alert(data.message);
                    //showErrorMessages(data.Errors, $(form));
                }
            },
            error: function (xhr, status, err) { }
        });
    }

    function ClearFormOververView(form) {
        $(form).find('input')
            .filter(':text, :password, :file').val('')
            .end()
            .filter(':checkbox, :radio')
            .removeAttr('checked')
            .end()
            .end()
            .find('textarea').val('')
            .end()
            .find('select').val("").trigger('change');
        ;
        //var elements = $(form).elements;
    }
    function AddOrUpdatePoOverview(url) {
        $("[data-valmsg-for='MainOVV']", form).text('');
        var form = $('#form-po-overview');
        $.validator.unobtrusive.parse(form);
        var PoID = $("#PoID").val();
        form.validate();
        if (form.valid()) {
            //Serialize the form datas.   
            var valdata = form.serialize();
            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: valdata,
                success: function (data) {
                    if (data.success) {
                        if (data.isnew) {
                            //Reload Table
                            //$('.list-over-view').load('/PO/RenderInputOverView', { ID: PoOvID, PoID: PoID });
                        }
                        $('.list-over-view').load('/PO/RenderListOverView', { PoID: PoID });
                        ClearFormOververView(form);
                        alert("Lưu thành công");
                    }
                    if (data.HasErrors) {
                        showErrorMessages(data.Errors, $(form));
                    }
                },
                error: function (xhr, status, err) { }
            });
        }
    }
    function AddOrUpdatePoItemDetail(url) {
        var form = $('#form-po-item-detail');
        $("[data-valmsg-for='Main']", form).text('');
        $.validator.unobtrusive.parse(form);
        var PoOvID = $("#PoOvID").val();
        form.validate();
        if (form.valid()) {
            //Serialize the form datas.   
            var valdata = form.serialize();
            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: valdata,
                success: function (data) {
                    if (data.success) {
                        if (data.isnew) {
                            //Reload Table
                            //$('.list-over-view').load('/PO/RenderInputOverView', { ID: PoOvID, PoID: PoID });
                        }
                        $('.table-item-detail').load('/PO/RenderTableItemDetail', { PoOvID: PoOvID });
                        if (data.message != null) {
                            alert(data.message);
                        }
                        else
                            alert("Lưu thành công");
                    }
                    if (data.HasErrors) {
                        showErrorMessages(data.Errors, $(form));
                    }
                },
                error: function (xhr, status, err) { }
            });
        }
    }
    function AddOrUpdatePO(Url) {
        var form = $('#form-po-create');
        $.validator.unobtrusive.parse(form);
        form.validate();
        if (form.valid()) {
            //Serialize the form datas.   
            var valdata = form.serialize();
            $.ajax({
                url: Url,
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: valdata,
                success: function (data) {
                    if (data.success) {
                        if (data.isnew) {
                            window.location.href = data.redirectToUrl;
                        }
                        alert("Lưu thành công");
                    }
                    if (data.HasErrors) {
                        showErrorMessages(data.Errors, $(form));
                    }
                },
                error: function (xhr, status, err) { }
            });
        }
    }
    function showErrorMessages(errors, context) {
        $.each(errors, function (i, error) {
            $("[data-valmsg-for='" + error.Key + "']", context).text(error.Value)
                .removeClass("field-validation-valid")
                .addClass("field-validation-error");
        });
    }
    $('#exchangerate').on('change', function () {
        this.value = parseFloat(this.value).toFixed(3);
    });
    addRowHandlers();
});
function leaveAStepCallback(obj, context) {
    alert("Leaving step " + context.fromStep + " to go to step " + context.toStep);
    // return false to stay on step and true to continue navigation 
    if ($('form').valid()) {
        return true;
    } else {
        return false;
    }
}
function DeleteOverView(PoOvID, itemNo) {
    $('#ModalLabel-delete').html("Xóa OverView");
    $('#PoOvID-del').val(PoOvID);
    $('#PoItmID-del').val('');
    $('#message-delete').html("Xác nhận xóa Item No <strong>" + itemNo + "?");
    $('#Modal-Delete').modal('show');
}
function ConfirmDeletePoItemDetail(PoItmID, LineNo) {
    $('#ModalLabel-delete').html("Xóa Item Details");
    $('#PoOvID-del').val('');
    $('#PoItmID-del').val(PoItmID);
    $('#message-delete').html("Xác nhận xóa Service Line No <strong>" + LineNo + "?");
    $('#Modal-Delete').modal('show');
}
function parseJsonDateToString(jsonDateString) {
    try {
        var datetime = new Date(parseInt(jsonDateString.replace('/Date(', '')));
        var date = datetime.getDate();
        var month = datetime.getMonth() + 1;
        var year = datetime.getFullYear();
        return date.toString().padStart(2, '0') + '/' + month.toString().padStart(2, '0') + '/' + year;
    }
    catch{ };
    return null;
}
function UpdateOverview(OvvData) {
    $("#PoOvID").val(OvvData.PoOvID);
    $("#PoID").val(OvvData.PoID);
    $("#Material").val(OvvData.Material);
    $("#ShortText").val(OvvData.ShortText);
    $("#Batch").val(OvvData.Batch);
    $("#InfoRecord").val(OvvData.InfoRecord);
    $("#GL").val(OvvData.GL);
    $("#AssetNo").val(OvvData.AssetNo);
    $("#CostCenter").val(OvvData.CostCenter);
    $("#ProfitCenter").val(OvvData.ProfitCenter);
    $("#OrderInteral").val(OvvData.OrderInteral);
    $("#MaterialGrpAcc").val(OvvData.MaterialGrpAcc);
    $("#MaterialGrpAcc").val(OvvData.MaterialGrpAcc);
    $("#DeliveryDate").val(parseJsonDateToString(OvvData.DeliveryDate));
    UpdateEleSelect($("#AssignmentID"), OvvData.AssignmentID);
    UpdateEleSelect($("#ItemCatID"), OvvData.ItemCatID);
    UpdateEleSelect($("#BusinessAreaID"), OvvData.BusinessAreaID);
    UpdateEleSelect($("#UomID"), OvvData.UomID);
    UpdateEleSelect($("#PlantID"), OvvData.PlantID);
    UpdateEleSelect($("#TaxCodeID"), OvvData.TaxCodeID);
    UpdateEleSelect($("#MaterialGroupID"), OvvData.MaterialGroupID);
    UpdateEleSelect($("#CountryID"), OvvData.CountryID);
    UpdateEleSelect($("#DivisionID"), OvvData.DivisionID);
    UpdateEleSelect($("#DivHeadID"), OvvData.DivHeadID);
    UpdateEleSelect($("#SalesGrpID"), OvvData.SalesGrpID);
    UpdateEleSelect($("#StorageLocID"), OvvData.StorageLocID);
}
function UpdateEleSelect(ele, value) {
    ele.val(value);
    ele.trigger('change');
}
function LoadPoOverView(PoID, PoOvID, edit) {
    var pcakey = $('#PcaKey').val();
    var form = $('#form-po-overview');
    var isApprove = true;
    if (pcakey === null || typeof pcakey === "undefined") {
        isApprove = false;
    }
    $.ajax({
        url: '/PO/GetDataOverview',
        type: "Get",
        dataType: 'json',
        async: true,
        contentType: 'application/json; charset=UTF-8',
        data: { PoOvID: PoOvID },
        success: function (data) {
            if (data.success) {
                //$(form).jsonToForm(data.data);
                //$(form).deserialize(data.data);
                UpdateOverview(data.data);
                if (edit) {
                    $("#btnclearoverview").hide();
                    $("#btnsaveoverview").show();
                    $("#btncancelcreateoverview").show();
                }
                else {
                    $("#btnclearoverview").show();
                    $("#btnsaveoverview").hide();
                    $("#btncancelcreateoverview").hide();
                }
            }
            if (data.HasErrors) {
                alert(data.message);
            }
        },
        error: function (xhr, status, err) { }
    });
    //var data = $.ajax({
    //    url: '/PO/RenderTableItemDetail',
    //    type: "Get",
    //    dataType: 'json',
    //    async: true,
    //    contentType: 'application/json; charset=UTF-8',
    //    data: { PoOvID: PoOvID, isApprove }
    //}).responseText;
    //$('.table-item-detail').innerHTML(data);
    $('.table-item-detail').load('/PO/RenderTableItemDetail', { PoOvID: PoOvID, isApprove });
    //$('.input-over-view').load('/PO/RenderInputOverView', { ID: PoOvID, PoID: PoID, isApprove });
    $('.input-item-detail').load('/PO/RenderInPutItemDetail', { ID: 0, PoOvID: PoOvID, isApprove });

}
function LoadPoItemDetail(PoOvID, PoItmID) {
    $('.input-item-detail').load('/PO/RenderInPutItemDetail', { ID: PoItmID, PoOvID: PoOvID });
}
function DeletePoItemDetail(PoItmID) {
    $('#ModalLabel-delete').html("Xóa Chi tiết");
    $('#PoOvID-del').val('');
    $('#PoItmID-del').val(PoItmID);
    $('#message-delete').html("Xác nhận xóa chi tiết của Item No <strong>" + itemNo + "?");
    $('#Modal-Delete').modal('show');
}
function addRowHandlers() {
    var table = document.getElementById("table-list-po-overview");
    var rows = table.getElementsByTagName("tr");
    for (i = 0; i < rows.length; i++) {
        var currentRow = table.rows[i];
        var createClickHandler =
            function (row) {
                return function () {
                    var cell = row.getElementsByTagName("td")[0];
                    var id = cell.innerHTML;
                    cell = row.getElementsByTagName("td")[1];
                    var Poid = cell.innerHTML;
                    LoadPoOverView(Poid, id, false);
                };
            };
        currentRow.ondblclick = createClickHandler(currentRow);
    }
}
$.fn.deserialize = function (serializedString) {
    var $form = $(this);
    $form[0].reset();
    serializedString = serializedString.replace(/\+/g, '%20');
    var formFieldArray = serializedString.split("&");
    $populateFeedback.slideDown().html('');
    $.each(formFieldArray, function (i, pair) {
        var nameValue = pair.split("=");
        var name = decodeURIComponent(nameValue[0]);
        var value = decodeURIComponent(nameValue[1]);
        // Find one or more fields
        var $field = $form.find('[name=' + name + ']');
        console.log(name, value);
        $populateFeedback.append('<li>' + name + ' = ' + value + '</li>');

        if ($field[0].type == "radio"
            || $field[0].type == "checkbox") {
            var $fieldWithValue = $field.filter('[value="' + value + '"]');
            var isFound = ($fieldWithValue.length > 0);
            if (!isFound && value == "on") {
                $field.first().prop("checked", true);
            } else {
                $fieldWithValue.prop("checked", isFound);
            }
        } else {
            $field.val(value);
        }
    });
}

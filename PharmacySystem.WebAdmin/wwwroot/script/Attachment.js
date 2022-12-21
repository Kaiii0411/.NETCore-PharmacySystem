$(document).ready(function () {
    document.getElementById("file").addEventListener("change", UploadFile, false);
    var selDiv = document.querySelector("#selectedFiles");

    function UploadFile() {
        var data = new FormData();
        var ID = $('#ID').val();
        var files = $("#file").get(0).files;
        data.append("HelpSectionImages", files[0]);
        data.append("GrID", ID);
        $(function () {
            $.ajax({
                type: "POST",
                url: '/Attachment/Add',
                processData: false,
                data: data,
                dataType: 'JSON',
                contentType: false,
                async: true,
                success: function (response) {
                    if (response.Filename === 1) {
                        return;
                    }
                    if (response.Filename != null || response.Filename != '') {
                        selDiv.innerHTML += "<div class=\"list-group-item d-flex justify-content-between align-items-center row\" id=" + "\"" + (response.AtmID) + "\"" + "name=\"itemob\">" +
                            "<div class=\"row col-11\">"+
                            "<div class=\"column left\">" +
                            "<span id=\"hiden\" class=\"info-box-icon bg-aqua\">" + response.Extend + "</span></div>" +
                            "<div class=\"column left\"><a href=\"https://www.adobe.com/prodlist.pdf\" title=\"https://www.adobe.com/prodlist.pdf\">" +
                            response.Filename + "</a></div></div>" + "<div class=\"row col-1\"><div class=\"column right\">" +
                            "<button onclick =\"removeIndex(" + (response.AtmID) + ")\"type=\"button\" class=\"close\" aria-label=\"Close\">"
                            + "<span aria-hidden=\"true\">&times;</span></button></div></div>"
                        var elem = document.getElementById('selectedFiles');
                        elem.scrollTop = elem.scrollHeight;
                        //window.setInterval(function () {
                        //    var $z = document.querySelector("#wait");
                        //    $z.remove();
                        //    var $a = document.querySelector("#hiden");
                        //    $a.removeAttribute('id');
                        //}, 40000);

                        //getNumLegnth();

                    }
                    $("#file").val('');
                },
                error: function (er) { },
            });
        })
    }
});
function removeIndex(id) {
    $.ajax({
        type: "POST",
        url: '/Attachment/Delete',
        data: JSON.stringify({ ID: id }),
        dataType: 'json',
        contentType: 'application/json; charset=UTF-8',
        success: function (response) {
            if (response.success) {
                var myobj = document.getElementById(id);
                myobj.remove();
                getNumLegnth();
            }
            else {
                alert("Xóa không thành công. Vui lòng thử lại.");
            }
        }
    });

}
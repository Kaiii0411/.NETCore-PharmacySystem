function ViewFile(ID) {
    var win = window.open("/Attachment/Download?AtmID=" + ID, "Document Support", 'location=1,status=1,scrollbars=1, resizable=1, directories=1, toolbar=1, titlebar=1');
    if (win) {
        //Browser has allowed it to be opened
        win.focus();
    } else {
        //Browser has blocked it
        alert('Please allow popups for this website');
    }
}
function ViewFileMT(ID) {
    window.open("/Attachment/DownloadMT?AtmID=" + ID, "_blank");
    //var win = window.open("/Attachment/DownloadMT?AtmID=" + ID, "Document Support", 'location=1,status=1,scrollbars=1, resizable=1, directories=1, toolbar=1, titlebar=1');
    //if (win) {
    //    //Browser has allowed it to be opened
    //    win.focus();
    //} else {
    //    //Browser has blocked it
    //    alert('Please allow popups for this website');
    //}
}
function Notifications() {
    "use strict";

    // tooltip demo
    $('.tooltip_bvtm').tooltip({
        selector: "[data-toggle=tooltip]",
        container: "body"
    })

    // popover demo
    $("[data-toggle=popover]")
        .popover()
};
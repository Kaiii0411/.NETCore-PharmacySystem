/*====================================
 Free To Use For Personal And Commercial Usage
Author: http://binarytheme.com
 License: Open source - MIT
 Please visit http://opensource.org/licenses/MIT for more Full Deatils of license.
 Share Us if You Like our work 
 Enjoy Our Codes For Free always.
======================================*/
//horizontal wizrd code section
$(function () {
    $("#wizard").steps({
        headerTag: "h2",
        bodyTag: "section",
        transitionEffect: "slideLeft"
    });
});
//vertical wizrd  code section
$(function () {
    $("#wizardV").steps({
        headerTag: "h2",
        bodyTag: "section",
        //transitionEffect: "slideLeft",
        //stepsOrientation: "vertical",
        // Disables the finish button (required if pagination is enabled)
        //enableFinishButton: false,
        // Disables the next and previous buttons (optional)
        enablePagination: false,
        // Enables all steps from the begining
        enableAllSteps: true,
    });
});
$(document).ready(function () {

});

var box;
function GoToRates() {
    box = $("#dialog");
    box.dialog({ autoOpen: false, maxHeight: 500, width: 650, modal: true });
    box.parent().appendTo($("form"));
    box.dialog("open");
}
function CloseDialog() {
    box.dialog("close");
   box.parent().remove();
}

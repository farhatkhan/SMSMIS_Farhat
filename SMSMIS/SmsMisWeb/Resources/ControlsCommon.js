var __formInputIds = new Array();
function handleReturn(e) {
    if (e.which == 13) {
        var id = $(this)[0].id;
        var controlType = 'text';
        if (id != '') {
            var nextControl = __formInputIds[__formInputIds[id].index + 1];
            controlType = __formInputIds[id].type;
            if (typeof nextControl != 'undefined') {
                $('#' + nextControl.id).focus();
            }
            if (controlType != 'submit') {
                e.preventDefault();
            }
            else {
                $('#' + id).click();
            }
        }
        if (typeof $(this).id == 'undefined' || $(this.id) == '') e.preventDefault();
    }
}
function attachCommonEvents() {
    var arr = $('*').find('[dosbox=true]');
    if (typeof arr != 'undefined' && arr != null && arr.length > 0) {
        for (x = 0; x < arr.length; x++) {
            if (x == 0) $(arr[x]).focus();
            $(arr[x]).on("keydown", handleReturn);
            __formInputIds[__formInputIds.length] = __formInputIds[arr[x].id] = { 'id': arr[x].id, 'index': x, 'type': arr[x].type };
        }
    }
    $('body').on("keypress", handleReturn);
}
function focusOnFirstAvailableControl() {
    try { $('#' + __formInputIds[0].id).focus() } catch (e) { }
}
function filterArray($filter, allList, selectedList, filterPropertyName, selectedPropertyName) {
    object[selectedPropertyName] = true;
}
$(document).ready(attachCommonEvents);
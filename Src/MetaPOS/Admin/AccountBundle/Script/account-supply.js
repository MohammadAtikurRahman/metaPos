
$(function() {

    $('#contentBody_txtCashInAmount,#contentBody_txtOpeinginDueDate').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnSave").trigger("click");
            event.preventDefault();
        }
    });

    $('#contentBody_ddlCashInTypeSup').change(function() {
        $("#btnSave").trigger("click");
        event.preventDefault();
    });
})
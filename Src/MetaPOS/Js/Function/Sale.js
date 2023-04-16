// Save button click event
$("#contentBody_btnSave #contentBody_btnUpdate").click(function() {
    if ($("#contentBody_txtPayCash").val() == "") {
        $("#contentBody_txtPayCash").val("0");
    }
});
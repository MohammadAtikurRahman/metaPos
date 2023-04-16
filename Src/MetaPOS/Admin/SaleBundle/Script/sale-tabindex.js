

//$(function() {
//    $('#contentBody_txtSearchNameCode').attr('TabIndex', 1);
//    $('#contentBody_ddlCustomer').attr('TabIndex', 3);
//    $('#contentBody_txtVatAmt').attr('TabIndex', 4);
//    $('#contentBody_txtDiscAmt').attr('TabIndex', 5);
//    $('#contentBody_txtPayCash').attr('TabIndex', 6);
//    $('#btnSaveInvoice').attr('TabIndex', 7);
//});


$(document).on('keypress', function (e) {
    if (e.which == 13) {

        if (document.activeElement.tabIndex == 7) {
            document.getElementById("btnSaveInvoice").click();
        }
        else {
            event.preventDefault();
        }
        //alert(document.activeElement.tabIndex);
    }
});
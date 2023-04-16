

$('#btnSuspend').click(function () {
    var totalBalance = $('#contentBody_txtGiftAmt').val();
    var totalGrandTotal = $('#contentBody_txtGrossAmt').val();
    $('#lblGrandTotalSusspend').text(totalGrandTotal);
    $('#lblBalanceSusspend').text(totalBalance);

    var returnAmtSusspend = 0;
    if (parseFloat(totalBalance) < 0) {
        returnAmtSusspend = parseFloat(totalGrandTotal) + parseFloat(Math.abs(totalBalance));
    }
    else {
        returnAmtSusspend = parseFloat(totalGrandTotal) - parseFloat(Math.abs(totalBalance));
    }

    $('#lblReturnAmtSusspend').text(returnAmtSusspend);
    $('#txtSuspendReturn').val(returnAmtSusspend);

    $('#suspendInvoice').modal('toggle');
});



$('#btnSuspendPopUp').click(function () {
    console.log("Suspend new");
    suspendInvoice();
});



function suspendInvoice() {
    
    $('#suspendInvoice').modal('toggle');

    var billNo = $('#contentBody_lblBillingNo').text();
    var returnAmt = $('#txtSuspendReturn').val() == "" ? "0" : $('#txtSuspendReturn').val();
    var balance = $('#lblGrandTotalSusspend').text() == "" ? 0 : $('#lblGrandTotalSusspend').text();

    if (billNo == null || billNo == "" || billNo == NaN) {
        showMessage("Invalid Bill No!!", "Warning");
        return;
    }
    
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/suspendInvoiceAction",
        data: JSON.stringify({ "billNo": billNo, "returnAmt": returnAmt, "balance": balance }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8"
    }).done(function (data) {
        if (data.d == true) {
            showMessage("Suspended successfully.", "Success");

            resetInvoice();
        }
        else {
            showMessage("Suspended failed.", "Warning");

            resetInvoice();
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        showMessage("Suspended fail!!", "Warning");

        resetInvoice();

        console.log(textStatus + ': ' + errorThrown);
    });
}


$('#btnExtraDiscount').click(function () {


    var getTotalDue = $('#contentBody_txtGiftAmt').val() == "" ? 0 : $('#contentBody_txtGiftAmt').val();
    var payCash = $('#contentBody_txtPayCash').val() == "" ? 0 : $('#contentBody_txtPayCash').val();

    var moreDiscount = parseFloat(getTotalDue) - parseFloat(payCash) < 0 ? 0 : parseFloat(getTotalDue) - parseFloat(payCash);

    // set value
    $('#contentBody_txtExtraDiscount').val(moreDiscount.toFixed(2));
    $('#divMoreDisc').removeClass("disNone");

   

    //var setTotalDue = parseFloat(getTotalDue) - parseFloat(getDue);
    //if (setTotalDue < 0)
    //    setTotalDue = 0;

    //$('#contentBody_txtGiftAmt').val(setTotalDue.toFixed(2));

   

    // update account
    updateAccount();
    // reset value
    $('#contentBody_txtDueChange').val("0.00");
    
});


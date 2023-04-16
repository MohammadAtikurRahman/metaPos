// Customer name replace label by textbox
$(function () {
    var e = $.Event('keydown', { keyCode: 32 }); // right arrow key
    $('#contentBody_txtCusName').trigger(e);

    changePayTypeDdl();


});





$('#contentBody_txtDiscAmt, #contentBody_txtPayCash, #contentBody_txtInterestRate, #contentBody_txtPayTwo').keyup(function () {

    updateCartAccountSection();

    // reset more discount
    //$('#contentBody_txtExtraDiscount').val("0.00");

});

$('#contentBody_rblInterestType').change(function () {

    updateCartAccountSection();

    changeCurrentBalanceLanguage();
    changeLanguage();
});




$('#contentBody_ddlDiscType, #contentBody_txtVa, #btnAdvance,#contentBody_ddlVatAmt,#contentBody_txtVatAmt').change(function () {
    changePayTypeDdl();
    updateCartAccountSection();
});


$('.ddlPayType').change(function () {
   
    changePayTypeDdl(this);
});


$('#contentBody_txtCardNo').keypress(function (evt) {
    var val = String.fromCharCode(evt.which);
    if (!(/[0-9]/.test(val))) {
        evt.preventDefault();
    }
});


$('#contentBody_emiCardNo').keypress(function (evt) {
    var val = String.fromCharCode(evt.which);
    if (!(/[0-9]/.test(val))) {
        evt.preventDefault();
    }
});


$('#btnQuickPayOne').click(function () {
    var due = $('#contentBody_txtGiftAmt').val();
    $('#contentBody_txtPayCash').val(due);
    $('#contentBody_txtDueChange').val("0.00");
});



$('#contentBody_txtLoadingCost, #contentBody_txtUnloading, #contentBody_txtShippingCost, #contentBody_txtCarryingCost, #contentBody_txtServiceCharge').change(function () {

    updateCartAccountSection();

});



function updateCartAccountSection() {
    totalNetAmt = cartTotal;
    grossAmt = 0;

    setCartDetail();

    getInterestCalculation();

    getGrandTotal();

    getTotalDue();

    payCashSaleAccount();

    advancePayment();

    installment();


}



function setCartDetail() {
    //console.log("cartTotal set:", cartTotal);
    $("#contentBody_txtNetAmt").val(cartTotal.toFixed(2));
}



function getGrandTotal() {
    var totalMiscCostAmt = getTotalMiscCostAmt();
    $('#contentBody_lblMiscAmt').val(totalMiscCostAmt.toFixed(2));

    var discAmt = $('#contentBody_txtDiscAmt').val();
    var discType = $('#contentBody_ddlDiscType').val();
    var totalAmt = parseFloat($('#contentBody_txtNetAmt').val());
    totalDiscAmt = commissionCalculation(totalAmt, discAmt, discType);
    var moreDisc = $('#contentBody_txtExtraDiscount').val() == "" ? 0 : $('#contentBody_txtExtraDiscount').val();
    totalDiscAmt = parseFloat(totalDiscAmt) + parseFloat(moreDisc);
    //$('#contentBody_txtDiscAmt').val(totalDiscAmt);

    // vat
    var totalVatAmt = getTotalVatAmt();

    // interest
    var interestAmt = $('#contentBody_lblInterestAmt').text() == "" ? 0 : $('#contentBody_lblInterestAmt').text();

    grossAmt = parseFloat(totalNetAmt) + parseFloat(totalVatAmt) + parseFloat(totalMiscCostAmt) + parseFloat(interestAmt) - parseFloat(totalDiscAmt);

    $('#contentBody_txtGrossAmt').val(grossAmt.toFixed(2));

    return grossAmt;
}




function getTotalMiscCostAmt() {
    var loadingCost = $('#contentBody_txtLoadingCost').val() == "" ? 0 : $('#contentBody_txtLoadingCost').val();
    var unloadingCost = $('#contentBody_txtUnloading').val() == "" ? 0 : $('#contentBody_txtUnloading').val();
    var shipingCost = $('#contentBody_txtShippingCost').val() == "" ? 0 : $('#contentBody_txtShippingCost').val();
    var carrayCost = $('#contentBody_txtCarryingCost').val() == "" ? 0 : $('#contentBody_txtCarryingCost').val();
    var serviceCharge = $('#contentBody_txtServiceCharge').val() == "" ? 0 : $('#contentBody_txtServiceCharge').val();

    return parseFloat(loadingCost) + parseFloat(unloadingCost) + parseFloat(shipingCost) + parseFloat(carrayCost) + parseFloat(serviceCharge);

}




function getTotalVatAmt() {

    var vatType = $('#contentBody_ddlVatAmt').val();
    var vatAmt = $('#contentBody_txtVatAmt').val();
    var totalNetAmt = $('#contentBody_txtNetAmt').val();


    if (vatAmt == "")
        vatAmt = "0";
    if (totalNetAmt == "")
        totalNetAmt = "0";

    var totalVatAmt = 0;
    if (vatType == "%") {
        totalVatAmt = (parseFloat(vatAmt) * parseFloat(totalNetAmt) / 100);
    }
    else {
        totalVatAmt = vatAmt;
    }

    return totalVatAmt;
}




function changePayTypeDdl(ddl) {
    var ddlVal = $(ddl).val();

    // Credit Cart maturity date
    //if (ddlVal == 6) {
    //    if ($("#MaturityDate").hasClass("disNone")) {

    //        $("#MaturityDate").removeClass("disNone");
    //        $("#MaturityDate").addClass("disBlock");
    //    }
    //    else {
    //        $("#MaturityDate").addClass("disNone");
    //        $("#MaturityDate").removeClass("disBlock");
    //    }
    //}
    //else {
    //    $("#MaturityDate").addClass("disNone");
    //}

    // pay type Card
    //if (ddlVal == 1) {
    //    if ($("#divCardType").hasClass("disNone")) {

    //        $("#divCardType").removeClass("disNone");
    //        $("#divCardType").addClass("disBlock");
    //    }
    //    else {
    //        $("#divCardType").addClass("disNone");
    //        $("#divCardType").removeClass("disBlock");
    //    }
    //}
    //else {
    //    $("#divCardType").addClass("disNone");
    //}


    var ddlPayTypeOne = $('#contentBody_ddlPayType').val();
    var ddlPayTypeTwo = $('#contentBody_ddlPayTypeTwo').val();

    if (ddlPayTypeOne == "1" || ddlPayTypeTwo == "1") {

        $("#divCardType").addClass("disBlock").removeClass("disNone");
    }
    else {
        $("#divCardType").addClass("disNone").addClass("disBlock");
    }

    if (ddlPayTypeOne == "2" || ddlPayTypeTwo == "2") {
       
        $("#MaturityDate").addClass("disBlock").removeClass("disNone");
    }
    else {
        $("#MaturityDate").addClass("disNone").addClass("disBlock");
    }

    if (ddlPayTypeOne == "3" || ddlPayTypeTwo == "3") {

        $("#divMobileBankType").addClass("disBlock").removeClass("disNone");
    }
    else {
        $("#divMobileBankType").addClass("disNone").addClass("disBlock");
    }

    if (ddlPayTypeOne == "4" || ddlPayTypeTwo == "4") {

        $("#divEmiType").addClass("disBlock").removeClass("disNone");
    }
    else {
        $("#divEmiType").addClass("disNone").addClass("disBlock");
    }
}




function commissionCalculation(totalAmt, discAmt, discType) {

    if (discAmt == "")
        discAmt = 0;

    var totalDisc = 0;

    if (discType == "%")
        totalDisc = (totalAmt * discAmt) / 100;
    else
        totalDisc = discAmt;


    return totalDisc;
}



function payCashSaleAccount() {

    var giftAmt = $.trim($('#contentBody_txtGiftAmt').val()) == "" ? 0 : $('#contentBody_txtGiftAmt').val();
    var paycash = $.trim($('#contentBody_txtPayCash').val()) == "" ? 0 : $.trim($('#contentBody_txtPayCash').val());
    var paycashTwo = $.trim($('#contentBody_txtPayTwo').val()) == "" ? 0 : $.trim($('#contentBody_txtPayTwo').val());

    paycash = parseFloat(paycash) + parseFloat(paycashTwo);

    if (isUpdate == true) {
        if (giftAmt < 0)
            paycash = 0;
    }

    var currentBal = parseFloat(giftAmt) - parseFloat(paycash);
    var orginalGiftAmt = currentBal;

    var advanceChecked = $('#btnAdvance').is(":checked");
    if (parseFloat(currentBal) < 0 && advanceChecked == false) {

        changeLanguage();
     
       // $('#contentBody_lblDueChage').text("Change");
       
        currentBal = Math.abs(currentBal);
        if (isUpdate == true) {
            
            
            $('#contentBody_txtDueChange').val(currentBal.toFixed(2));

            if (parseFloat(giftAmt) < 0) {
                $('#contentBody_txtPayCash').val(currentBal.toFixed(2));
                $('#contentBody_txtDueChange').val("0.00");

                $('#contentBody_lblPayCash').text("Return");
                $('#contentBody_lblPayCash').removeClass("disNone");
                $('#contentBody_ddlPayType').addClass("disNone");
                $('#contentBody_ddlPayType').addClass("disNone");
                $('#btnQuickPayOne').addClass("disNone");
                $('#divPayOne').addClass("disNone");
            }
            else {
                $('#contentBody_lblPayCash').addClass("disNone");
                $('#contentBody_ddlPayType').removeClass("disNone");
                $('#contentBody_ddlPayType').removeClass("disNone");
                $('#btnQuickPayOne').removeClass("disNone");
                $('#divPayOne').removeClass("disNone");

            }
        }
        else {

            $('#contentBody_txtDueChange').val(currentBal.toFixed(2));
        }

    }
    else {

        changeCurrentBalanceLanguage();

        $('#contentBody_lblPayCash').text("Pay");
        $('#contentBody_txtDueChange').val(currentBal.toFixed(2));
  
    }


}




function getTotalDue() {

    var previousDue = $('#contentBody_txtPreviousDue').val() == "" ? 0 : $('#contentBody_txtPreviousDue').val();
    var grandTotal = $('#contentBody_txtGrossAmt').val() == "" ? 0 : $('#contentBody_txtGrossAmt').val();
    var returnPaid = $('#contentBody_txtReturnPaid').val() == "" ? 0 : $('#contentBody_txtReturnPaid').val();
    var returnTotal = $('#contentBody_txtReturnTotal').val() == "" ? 0 : $('#contentBody_txtReturnTotal').val();

    if (isUpdate == true && getUrlParameter("status") != 'draft') {
        grandTotal = 0;
    }

    var returnTotalAmt = parseFloat(returnTotal) - parseFloat(returnPaid);
    var totalDueAmt = (parseFloat(previousDue) + parseFloat(grandTotal)) - returnTotalAmt;

    if (parseFloat(returnTotalAmt) < 0) {
        if (isUpdate)
            $('#contentBody_lblPayCash').text("Return");
        changeLanguage();
        // $('#contentBody_lblDueChage').text("Change");
    }
    else {
        $('#contentBody_lblPayCash').text("Pay");
        changeCurrentBalanceLanguage();
    }
    $('#contentBody_txtGiftAmt').val(totalDueAmt.toFixed(2));
}



function productReturnDiscount() {
    var disc = $('#contentBody_txtDiscAmt').val();
    var discType = $('#contentBody_lblDiscType').text();

    if (parseFloat(disc) <= 0)
        return 0;

    var discReturnAmt = 0;
    var totalQty = 0;
    var totalReturnQty = 0;
    $("#contentBody_divShoppingList tr").each(function (index) {

        if (discType == "(%)") {
            var price = $(this).find('.txtUnitPrice').val() == undefined ? 0 : $(this).find('.txtUnitPrice').val();
            var returnQty = $(this).find('.txtReturn').val() == undefined ? 0 : $(this).find('.txtReturn').val();
            
            var returnTotalAmt = price * returnQty;
            discReturnAmt += (disc * returnTotalAmt) / 100;
            console.log("discReturnAmt::", discReturnAmt);
        }
        else {
            var qtyVal = $(this).find('.ddlQty').val() == undefined ? 0 : $(this).find('.ddlQty').val();
            totalQty += parseFloat(qtyVal);
            var returnQtyForAmt = $(this).find('.txtReturn').val() == undefined ? 0 : $(this).find('.txtReturn').val();
            totalReturnQty += parseFloat(returnQtyForAmt);
        }

    });

    if (discType != "(%)") {
        discReturnAmt += (parseFloat(disc) / parseFloat(totalQty)) * totalReturnQty;
    }

    return discReturnAmt;

}


function productReturnDiscount() {
    var disc = $('#contentBody_txtDiscAmt').val();
    var discType = $('#contentBody_lblDiscType').text();

    var discReturnAmt = 0;
    var totalQty = 0;
    var totalReturnQty = 0;
    $("#contentBody_divShoppingList tr").each(function(index) {
        
        if (discType == "(%)") {
            var price = $(this).find('.txtUnitPrice').val() == undefined ? 0 : $(this).find('.txtUnitPrice').val();
            var returnQty = $(this).find('.txtReturn').val() == undefined ? 0 : $(this).find('.txtReturn').val();

            var returnTotalAmt = price * returnQty;
            discReturnAmt = (disc * returnTotalAmt) / 100;
        }
        else {
            var qtyVal = $(this).find('.ddlQty').val() == undefined ? 0 : $(this).find('.ddlQty').val();
            totalQty += parseFloat(qtyVal);
            var returnQtyForAmt = $(this).find('.txtReturn').val() == undefined ? 0 : $(this).find('.txtReturn').val();
            totalReturnQty += parseFloat(returnQtyForAmt);
        }

    });

    if (discType != "(%)") {
        discReturnAmt = (parseFloat(disc) / parseFloat(totalQty)) * totalReturnQty;
    }

    return discReturnAmt;

}




function getInterestCalculation() {

    // cart total
    var cartTotal = $.trim($('#contentBody_txtNetAmt').val()) == "" ? 0 : $.trim($('#contentBody_txtNetAmt').val());
    // disc
    var discountAmt = $('#contentBody_txtDiscAmt').val() == "" ? 0 : $('#contentBody_txtDiscAmt').val();
    var discType = $('#contentBody_ddlDiscType').val();
    var discTotal = commissionCalculation(cartTotal, discountAmt, discType);

    var grandTotal = parseFloat(cartTotal) - parseFloat(discTotal);

    var paycash = $.trim($('#contentBody_txtPayCash').val()) == "" ? 0 : $.trim($('#contentBody_txtPayCash').val());
    var invoiceDue = parseFloat(grandTotal) - parseFloat(paycash);


    var interestRate = $('#contentBody_txtInterestRate').val() == "" ? "0" : $.trim($('#contentBody_txtInterestRate').val());
    var baseInterestAmt = 0;
    var interestType = $("#contentBody_rblInterestType input[type='radio']:checked").val();
    if (interestType == "1") {
        baseInterestAmt = cartTotal;
    }
    else {
        baseInterestAmt = invoiceDue;
    }

    var totalInterestAmt = (parseFloat(baseInterestAmt) * parseFloat(interestRate)) / 100;

    $('#contentBody_lblInterestAmt').text(totalInterestAmt.toFixed(2));


}


$('#btnAddPay').click(function () {
    $('#divPaymentTwo').removeClass("disNone");
});

$('#btnRemovePayTwo').on("click", function () {

    $('#divPaymentTwo').addClass("disNone");
    $('#contentBody_txtPayTwo').val(" ");

    var ddlPayTypeTwo = $('#contentBody_ddlPayTypeTwo').val();
    if (ddlPayTypeTwo == "1") {

        $('#divCardType').addClass("disNone").removeClass("disBlock");

    } else if (ddlPayTypeTwo == "2") {

        $('#MaturityDate').addClass('disNone').removeClass("disBlock");

    }
});



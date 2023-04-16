$('#btnNew').click(function() {
    resetInvoice();

    // Search box reset 
    $('#contentBody_txtBillingNo').val("");
    $('#contentBody_txtBillingNoSearch').val("");
});


// Reset all invoice
function resetInvoice() {
    //$('#contentBody_divShoppingList').empty();

    isUpdate = false;
    iCounter = 0;

    $('#contentBody_txtPayTwo').val("");
    $('#divPaymentTwo').addClass("disNone");
    $('#MaturityDate').addClass("disNone");
    $('#divCardType').addClass("disNone");

    $('#contentBody_txtCustomerRemainder').val("");
    $('#contentBody_txtCustomerRemainder').text("");
    $('#contentBody_txtExtraDiscount').val("0.00");

    $('#contentBody_txtNetAmt').val("0.00");

    totalNetAmt = cartTotal = returnTotal = 0;

    $("#contentBody_divShoppingList").find("tr").remove();

    $('#contentBody_lblBillingNo').text("");
    $('#contentBody_ddlCustomer').select2("val", "0");
    $("#contentBody_ddlCustomer").removeAttr("disabled");
    //$('#contentBody_ddlReferredBy').select2("val", "0");

    $("#btnCustomerModal").removeAttr("disabled");
    $('#contentBody_txtAdditionalDue').val("0.00");
    
    $('#contentBody_txtGiftAmt').val("0.00");
    $('#contentBody_ddlPayType').selectedIndex = $('contentBody_ddlDiscType').selectedIndex = $('#contentBody_ddlCardType').selectedIndex = $('#contentBody_ddlBankName').selectedIndex = $('#contentBody_ddlStaff').selectedIndex = 0;
    $('#contentBody_ddlStaff').selectedIndex = 0;
    $('#contentBody_txtDiscAmt').val("");
    $('#contentBody_txtPayCash').val("");
    $('#contentBody_txtGrossAmt').val("0.00");
    $('#contentBody_txtGrossAmt').text("0.00");
    $('#contentBody_txtVatAmt').val("0.00");
    $('#contentBody_txtReturn').val("0.00");
    $('#contentBody_txtInterestAmt').val("0.00");
    $('#contentBody_txtMaturityDate').val(new Date($.now()));
    $('#contentBody_txtCheckNo').val("");
    $('#contentBody_txtPayCard').val("");
    $('#contentBody_txtToken').val("");
   $('#contentBody_txtDiscAmt').val("0.00");
    $('#contentBody_txtPayDate').val(moment().format('DD-MMM-YYYY'));
    $('#contentBody_txtBalance').val("0.00");
    $('#contentBody_lblMiscAmt').text("0.00");
    $('#contentBody_lblMiscAmt').val("0.00");

    $('#contentBody_chkAdvance').checked = false;
    // Display/Hide
    $('#btnSaveInvoice').removeClass("disNone");
    $('#btnUpdateInvice').addClass("disNone");
    $('#btnSuspend').addClass("disNone");

    // Misc. cost reset
    $('#contentBody_txtLoadingCost').val("");
    $('#contentBody_txtShippingCost').val("");
    $('#contentBody_txtCarryingCost').val("");
    $('#contentBody_txtServiceCharge').val("");
    $('#contentBody_txtUnloading').val("");
    $('#contentBody_txtPreviousDue').val("0.00");

    // Referal reset
    $('#contentBody_txtRefName').val("");
    $('#contentBody_txtRefPhone').val("");
    $('#contentBody_txtRefAddress').val("");

    // Notes
    $('#contentBody_txtComment').val("");

    // Search box reset 
    //$('#contentBody_txtBillingNo').val("");
    //$('#contentBody_txtBillingNoSearch').val("");

    deleteCartProductSaleId = "";
    deleteCartProductSotckStatusId = "";

    $('#btnSaveInvoice').attr("disabled", false);

    $('#contentBody_txtSearchNameCode').attr("disabled", false);
    $('#contentBody_txtSearchNameCode').focus();
    $('#btnAddToCart').attr("disabled", false);

    $('#divSearchBox').removeClass("disNone");
    $('#divSaleUpdateTitle').addClass("disNone");
    $('#thReturn').addClass("disNone");

    $('#divDiscountOption').removeClass("disNone");
    $('#contentBody_lblDiscType').addClass("disNone");
    $('#contentBody_txtVatAmt').removeClass("aspNetDisabled txtboxBg TxtboxToLabel");
    $('#contentBody_txtVatAmt').attr("disabled", false);
    $('#contentBody_txtDiscAmt').removeClass("aspNetDisabled txtboxBg TxtboxToLabel");
    $('#contentBody_txtVatAmt').attr("disabled", false);
    $('#contentBody_txtDiscAmt').attr("disabled", false);

    $('#contentBody_lblInterestAmt').text("0");
    $('#contentBody_txtInterestRate').val("");

    $('#ddlDateSeletor').val("");
    $('#contentBody_txtInstalmentNumber').val("");
    $('#contentBody_txtDownPayment').val("");
    $('#contentBody_txtDueChange').val("0.00");

    $('#contentBody_txtInstalmentNumber').attr("disabled", false);
    $('#contentBody_txtCustomerRemainder').attr("disabled", false);
    $('#contentBody_txtDownPayment').attr("disabled", false);
    $('#ddlDateSeletor').attr("disabled", false);

    $('#contentBody_txtReturnTotal').val("0.00");
    $('#contentBody_txtReturnPaid').val("0.00");

    $('#divAdvance').removeClass('disNone');

    $('#btnAdvance').prop("checked", false);

}
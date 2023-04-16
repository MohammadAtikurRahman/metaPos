$('#btnSaveInvoice').click(function () {

    var isSaved = checkInvoiceValidation();

    if (!isSaved) {
        document.body.scrollTop = 0; // For Safari
        document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
        return;
    }

    $("#btnSaveInvoice").attr("disabled", true);
    var billNo = $('#contentBody_lblBillingNo').text();
    upsertInvoice(billNo, "sale");
});


$('#btnConfirmSale').click(function () {

    var isSaved = checkInvoiceValidation();

    if (!isSaved) {
        document.body.scrollTop = 0; // For Safari
        document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
        return;
    }

    $("#btnConfirmSale").attr("disabled", true);
    var billNo = $('#contentBody_lblBillingNo').text();
    upsertInvoice(billNo, "sale");
});



$('#btnDraftInvoice').click(function () {

    var isSaved = checkInvoiceValidation();

    if (!isSaved) {
        document.body.scrollTop = 0; // For Safari
        document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
        return;
    }

    var payCash1 = $('#contentBody_txtPayCash').val() == "" ? "0" : $('#contentBody_txtPayCash').val();
    var payCash2 = $('#contentBody_txtPayTwo').val() == "" ? "0" : $('#contentBody_txtPayTwo').val();
    var totalPayCash = parseFloat(payCash1) + parseFloat(payCash2);
    if (totalPayCash > 0) {
        showMessage("You can't pay in Draft/Quatation.", "Warning");
        document.body.scrollTop = 0; // For Safari
        document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
        return;
    }

    $("#btnDraftInvoice").attr("disabled", true);

    upsertInvoice("", "draft");


});


function checkInvoiceValidation() {
    
    var cusId = $('#contentBody_ddlCustomer').val();
    var isCustomerRequired = columnAccess[0]["isCustomerRequired"];
    if (isCustomerRequired == "1") {
        if (cusId == null || cusId == 0) {
            showMessage("You have to select or create a Customer", "Warning");
            return false;
        }
    }

    if (columnAccess[0]["isCustomerReminder"] == "1" && $('#contentBody_txtCustomerRemainder').val() != "") {

        var reminderDate = $('#contentBody_txtCustomerRemainder').val();
        var m = moment(reminderDate);
        if (!m.isValid()) {
            showMessage("Reminder is not date format", "Warning");
            return false;
        }
    }

    if (columnAccess[0]["displayInstalment"] == "1") {

        if ($('#contentBody_txtInstalmentNumber').val() != "" && $('#contentBody_txtCustomerRemainder').val() == "") {
            showMessage("Next pay date is not valid", "Warning");
            return false;
        }
        if ($('#contentBody_txtInstalmentNumber').val() != "" && $('#contentBody_txtDownPayment').val() == "") {
            showMessage("Next pay date is not valid", "Warning");
            return false;
        }
    }
    
    if (isImeiStatusOpen == true) {
        showMessage("Select an IMEI", "Warning");
        return false;
    }

    if (iCounter <= 0) {
        showMessage("Shopping list empty.", "Warning");
        return false;
    }

    return true;
}






var isSaveConfirm = true;


$('#btnUpdateInvice').click(function () {
    if (iCounter > 0) {

        // check return amount
        var returnTotal = $('#contentBody_txtReturnTotal').val() == "" ? 0 : $('#contentBody_txtReturnTotal').val();
        var returnPaid = $('#contentBody_txtReturnPaid').val() == "" ? 0 : $('#contentBody_txtReturnPaid').val();
        var paycash = $('#contentBody_txtPayCash').val() == "" ? 0 : $('#contentBody_txtPayCash').val();
        var preDue = $('#contentBody_txtPreviousDue').val() == "" ? 0 : $('#contentBody_txtPreviousDue').val();

        var returnAmt = parseFloat(preDue) - (parseFloat(returnTotal) - parseFloat(returnPaid));

        if (parseFloat(Math.abs(returnAmt.toFixed(2))) != parseFloat(paycash) && $('#contentBody_lblPayCash').text() == "Return") {
            showMessage("change store is not allowed because Advance is not active", "Warning");
            return;
        }
        else {

            var billNo = $('#contentBody_lblBillingNo').text();
            var opt = "update";

            upsertInvoice(billNo, opt);
        }
    }
    else {
        showMessage("Shopping list empty.", "Warning");
    }
});




function upsertInvoice(billNo, opt) {

    var paidAmt = 0, isPackage = false;

    var currentdate = new moment().tz('Asia/Dhaka').format('DD-MMM-YYYY HH:mm:ss a');
    var currentdate1 = new moment().tz('Asia/Dhaka').format('DD-MMM-YYYY');
    // var entrytime = currentdate.getHours() + ":" + currentdate.getMinutes() + ":" + currentdate.getSeconds();

    var entryDate = $('#contentBody_txtBillingDate').val().trim() == currentdate1 ? currentdate : $('#contentBody_txtBillingDate').val().trim();



    console.log("entryDated:", entryDate);
    var entryDated = moment().tz('Asia/Dhaka').format('DD-MMM-YYYY');
    console.log("entryDate:", entryDated);
    var discType = $('#contentBody_ddlDiscType').val();

    var previousDue = $('#contentBody_txtPreviousDue').val() == "" ? 0 : $('#contentBody_txtPreviousDue').val();
    var totalDue = $('#contentBody_txtGiftAmt').val() == "" ? 0 : $('#contentBody_txtGiftAmt').val();
    var returnTotalAmt = $('#contentBody_txtReturnTotal').val() == "" ? 0 : $('#contentBody_txtReturnTotal').val();
    var returnSaleAmt = $('#contentBody_txtReturnTotal').val() == "" ? 0 : $('#contentBody_txtReturnTotal').val();
    var paidReturnAmt = $('#contentBody_txtReturnPaid').val() == "" ? 0 : $('#contentBody_txtReturnPaid').val();
    var changeAmt = $('#contentBody_txtDueChange').val() == "" ? 0 : $('#contentBody_txtDueChange').val();
    var due = $('#contentBody_txtDueChange').val() == "" ? 0 : $('#contentBody_txtDueChange').val();
    var cusId = ($('#contentBody_ddlCustomer').val() == null) || ($('#contentBody_ddlCustomer').val() == "") ? 0 : $('#contentBody_ddlCustomer').val();
    var loadingCost = $('#contentBody_txtLoadingCost').val() == "" ? 0 : $('#contentBody_txtLoadingCost').val();
    var unloadingCost = $('#contentBody_txtUnloading').val() == "" ? 0 : $('#contentBody_txtUnloading').val();
    var shippingCost = $('#contentBody_txtShippingCost').val() == "" ? 0 : $('#contentBody_txtShippingCost').val();
    var carryingCost = $('#contentBody_txtCarryingCost').val() == "" ? 0 : $('#contentBody_txtCarryingCost').val();
    var serviceCharge = $('#contentBody_txtServiceCharge').val() == "" ? 0 : $('#contentBody_txtServiceCharge').val();
    var comment = $('#contentBody_txtComment').val();
    var token = $('#contentBody_txtToken').val();
    var cusType = $('#contentBody_rblCusType').val() == "" ? 0 : $('#contentBody_rblCusType').val(); 
    var vatAmt = $('#contentBody_txtVatAmt').val() == "" ? 0 : $('#contentBody_txtVatAmt').val();
    var vatType = $('#contentBody_ddlVatAmt').val() == "" ? 0 : $('#contentBody_ddlVatAmt').val();
    var preDue = $('#contentBody_txtPreviousDue').val() == "" ? 0 : $('#contentBody_txtPreviousDue').val();
    var refName = $('#contentBody_txtRefName').val() == "" ? 0 : $('#contentBody_txtRefName').val();
    var refPhone = $('#contentBody_txtRefPhone').val() == "" ? 0 : $('#contentBody_txtRefPhone').val();
    var refAddress = $('#contentBody_txtRefAddress').val() == "" ? 0 : $('#contentBody_txtRefAddress').val();
    var instalmentNumber = $('#contentBody_txtInstalmentNumber').val() == "" ? 0 : $('#contentBody_txtInstalmentNumber').val();
    var downPayment = $('#contentBody_txtDownPayment').val() == "" ? 0 : $('#contentBody_txtDownPayment').val();
    var interestRate = $('#contentBody_txtInterestRate').val() == "" ? 0 : $('#contentBody_txtInterestRate').val();
    var interestAmt = $('#contentBody_lblInterestAmt').text() == "" ? 0 : $('#contentBody_lblInterestAmt').text();
    var instalmentIncrement = $('#ddlDateSeletor').text() == "" ? 0 : $('#ddlDateSeletor').val();
    var nextPayDate = $('#contentBody_txtCustomerRemainder').val() == "" ? new Date(0) : $('#contentBody_txtCustomerRemainder').val();
    var salePersonById = $('#contentBody_ddlStaff').val();
    var referredBy = $('#contentBody_ddlReferredBy').val();
    var isAdvance = $('#btnAdvance').is(":checked");
    var payMethod = $('#contentBody_ddlPayType').val() == "" ? "0" : $('#contentBody_ddlPayType').val();
    var payMethodTwo = $('#contentBody_ddlPayTypeTwo').val();
    var payCashTwo = $('#contentBody_txtPayTwo').val() == "" ? 0 : $('#contentBody_txtPayTwo').val();
    var cardId = $('#contentBody_ddlCardType').val() == "" ? 0 : $('#contentBody_ddlCardType').val();
    var bankId = $('#contentBody_ddlBankName').val() == "" ? 0 : $('#contentBody_ddlBankName').val();
    var payCash = $.trim($('#contentBody_txtPayCash').val()) == "" ? 0 : $.trim($('#contentBody_txtPayCash').val());
    var maturityDate = $('#contentBody_txtMaturityDate').val() == "" ? moment().format('L') : $('#contentBody_txtMaturityDate').val();
    var checkNo = $('#contentBody_txtCheckNo').val();
    var trxId = $('#contentBody_txtTrxId').val() == "" ? 0 : $('#contentBody_txtTrxId').val();
    var mobileBankingId = $("#contentBody_ddlMobileBankType").val();
    var cardNo = $('#contentBody_txtCardNo').val();
    var emiId = $("#contentBody_ddlEmiType").val();
    var emiCardNo = $("#contentBody_emiCardNo").val();
    var invoiceTypeCus = $('#contentBody_rblCusType input:checked').val()=="" ? "0" : $('#contentBody_rblCusType input:checked').val();

    var payType;
    var payDescr;
    if (payMethod == "1") {
        payType = cardId;
        payDescr = cardNo;
    }
    else if (payMethod == "2") {
        payType = bankId;
        payDescr = checkNo;
    }
    else if (payMethod == "3") {
        payType = mobileBankingId;
        payDescr = trxId;
    }
    else if (payMethod == "4") {
        payType = emiId;
        payDescr = emiCardNo;
    }


    var invoiceType = "0"; 
    if (invoiceTypeCus == "0") {
        invoiceType = "0";
    }else {
        invoiceType = "1";
    }
    if (payMethod == "4") {
        invoiceType = "2";
    }
   
    var salePersonType = "0";
    if (columnAccess[0]["autoSalesPerson"] == "1") {
        salePersonType = "1";
        salePersonById = $('#contentBody_lblSaleById').text() == "" ? 0 : $('#contentBody_lblSaleById').text();
    }

    var returnAmt = 0;

    if (($('#contentBody_lblDueChage').text() == "Change" || $('#contentBody_lblDueChage').text() == "ফেরত") && $('#contentBody_lblPayCash').text() == "Pay") {
        due = 0;
        returnAmt = $('#contentBody_txtDueChange').val();
        payCash = payCash - changeAmt;
        if (payCash < 0)
            payCash = 0;
    }

    var returnCash = parseFloat(previousDue) - (parseFloat(returnTotal) - parseFloat(paidReturnAmt));
    var balance = payCash;

    if (($('#contentBody_lblDueChage').text() == "Change" || $('#contentBody_lblDueChage').text() == "ফেরত") && $('#contentBody_lblPayCash').text() == "Return") {
        if (payCash > returnCash) {
            payCash = Math.abs(returnCash);
            due = 0;
        }

        if (returnCash < 0) {
            returnTotalAmt = Math.abs(returnCash);
            balance = -payCash;
            payCash = 0;
        }
    }



    // get additional due amount
    var additionDue = getAdditionalDueAmt();
    if (due < 0) {
        due = 0;
    }

    var saleDataList = [];
    var jsonData = {};
    var executeCounter = 0, jsExeCounter = 0;
    $("#contentBody_divShoppingList tr").each(function(index) {

        if ($(this).find('.ddlQty').val() == "" || $(this).find('.ddlQty').val() == null) {
            showMessage("Qty is invalid.", "Warning");
            isSaveConfirm = false;
            return;
        }
        var testName = $(this).find('.lblProdName').val()
        console.log('testName:', testName);
        var returnQty = $(this).find('.txtReturn').val() == "" ? 0 : $(this).find('.txtReturn').val();
        var unitPrice = $(this).find('.txtUnitPrice').val() == "" ? 0 : $(this).find('.txtUnitPrice').val();
        var prodId = $(this).find('.lblItemId').text() == "" ? 0 : $(this).find('.lblItemId').text();
        var qty = $(this).find('.ddlQty').val() == "" ? 0 : $(this).find('.ddlQty').val();

        var serialNo = $(this).find('.serialNo').val() == "" ? 0 : $(this).find('.serialNo').val();
        if (serialNo == undefined)
            serialNo = "";
        var prodCodes = $(this).find('.lblProdCodes').text();
        var engineNumber = $(this).find('.lblEngineNumber').text();
        var cecishNumber = $(this).find('.lblcecishNumber').text();
        var imei = $(this).find('.lblImei').text() == 'N/A' ? "" : $(this).find('.lblImei').text();
        var searchType = $(this).find('.lblSearchType').text();
        var storeId = $(this).find('.lblStore').text();
        var removeImei = $(this).find('.lblRemoveImei').text();
        var extraDiscount = $('#contentBody_txtExtraDiscount').val() == "" ? 0 : $('#contentBody_txtExtraDiscount').val();
        var fieldRecord = $(this).find('.lblFieldRecord').text();
        var attributeRecord = $(this).find('.lblAttributeRecord').text();

        var discountType = "", offer = 0;
        if (columnAccess[0]["offer"] == "1") {
            discountType = $(this).find('.lblDiscountType').text() == "XXXXXXXX" ? "0" : $(this).find('.lblDiscountType').text();
            offer = $(this).find('.lblFree').text() == "" ? 0 : $(this).find('.lblFree').text();
        }


        // package status
        if (prodCodes != "") {
            isPackage = true;
        } else {
            isPackage = false;
        }

        if (returnQty == undefined)
            returnQty = 0;

        //
        var urlParams = new URLSearchParams(window.location.search);
        var reminderId = urlParams.get('reminderId');
        if (reminderId == null)
            reminderId = "";
        
        jsonData = {
            "saleId": $(this).find('.lblsaleId').text(),
            "stockStatusId": $(this).find('.lblStockStatusId').text(),
            "billNo": billNo,
            "cusId": cusId,
            "name": "",
            "phone": "",
            "address": "",
            "email":"",
            "notes": "",
            "accountNo": "",
            "installmentStatus": false,
            "prodID": prodId,
            "qty": qty,
            "serialNo": serialNo,
            "invoiceType": invoiceType,
            "netAmt": totalNetAmt,
            "discAmt": totalDiscAmt,
            "vatAmt": vatAmt,
            "vatType": vatType,
            "grossAmt": grossAmt,
            "payMethod": payMethod,
            "payCash": payCash,
            "giftAmt": due,
            "return_": returnAmt,
            "balance": balance,
            "sPrice": unitPrice,
            "discType": discType,
            "comment": comment,
            "currentCash": payCash,
            "salesPersonId": salePersonById,
            "referredBy": referredBy,
            "cardId": cardId,
            "brankId": bankId,
            "token": token,
            "cusType": cusType,
            "maturityDate": maturityDate,
            "checkNo": checkNo,
            "warranty": ' ',
            "loadingCost": loadingCost,
            "unloadingCost": unloadingCost,
            "shippingCost": shippingCost,
            "carryingCost": carryingCost,
            "paidAmt": paidAmt,
            "deleteCartProductSaleId": deleteCartProductSaleId,
            "deleteCartProductSotckStatusId": deleteCartProductSotckStatusId,
            "salePersonType": salePersonType,
            "additionDue": additionDue,
            "returnQty": returnQty,
            "returnAmt": returnSaleAmt,
            "paidReturnAmt": paidReturnAmt,
            "preDue": preDue,
            "entryDate": entryDate,
            "prodCodes": prodCodes,
            "isPackage": isPackage,
            "engineNumber": engineNumber,
            "cecishNumber": cecishNumber,
            "nextPayDate": nextPayDate,
            "executeCounter": jsExeCounter,
            "interestRate": interestRate,
            "interestAmt": interestAmt,
            "instalmentNumber": instalmentNumber,
            "instalmentIncrement": instalmentIncrement,
            "downPayment": downPayment,
            "imei": imei,
            "searchType": searchType,
            "opt": opt,
            "storeId": storeId,
            "removeImei": removeImei,
            "serviceCharge": serviceCharge,
            "extraDiscount": extraDiscount,
            "refName": refName,
            "refPhone": refPhone,
            "refAddress": refAddress,
            "isAdvance": isAdvance,
            "discountType": discountType,
            "offer": offer,
            "source": "Invoice",
            "fieldRecord": fieldRecord,
            "attributeRecord": attributeRecord,
            "payMethodTwo": payMethodTwo,
            "payCashTwo": payCashTwo,
            "reminderId": reminderId,
            "payType": payType,
            "payDescr": payDescr
        };
        jsExeCounter++;
        saleDataList.push(jsonData);
    });

    console.log("jsonData::", jsonData);

    if (opt == "update")
        url = baseUrl + "Admin/SaleBundle/View/Invoice.aspx/updateSaleDataListAction";
    else
        url = baseUrl + "Admin/SaleBundle/View/Invoice.aspx/saveSaleDataListAction";

    $.ajax({
        url: url,
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(saleDataList) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function(data) {
            console.log("sales data Query:", data.d);
            var message = data.d.split('|')[0]; //message.Split('|')[0]
            billNo = data.d.split('|')[1]; 
            if (message == "True" || message == "true") {
                showMsgOutput(true, "save");

                // Set Print value
                searchSaleOptionNext(billNo, false);

            } else {
                showMsgOutput(false, data.d);
            }

            // Search box reset 
            $('#contentBody_txtBillingNo').val("");
            $('#contentBody_txtBillingNoSearch').val("");

            // Set last BillNo for print
            $('#contentBody_lblBillNoHidden').val(billNo);


            // Reset
            resetInvoice();
        },
        error: function(data) {
            showMessage(data.responseText, "Error");
        },
        failure: function(data) {
            showMessage(data.responseText, "Error");
        }
    });
}


function sendSMSToCustomerAndOwner(cusSelectedData, billNo, payCash, due, totalNetAmt, grossAmt) {

    if (cusSelectedData.length > 0) {
        var cusSelectedText = cusSelectedData[0].text;
        cusSelectedText = cusSelectedText.slice(0, -1);
        var splitCusSelectedText = cusSelectedText.split(",");
        // get customer name 
        var tempCustomerName = $.trim(splitCusSelectedText[0]);
        var splitTempCusName = tempCustomerName.split("(");
        var customerName = splitTempCusName[1];
        // get phone number
        var phoneNumberSms = $.trim(splitCusSelectedText[1]);

        if (columnAccess[0]["sendInvoiceBySms"] == "1") {
            sendInvoiceBySMS(phoneNumberSms, customerName, billNo, payCash, due, totalNetAmt, grossAmt, false);
        }

        if (columnAccess[0]["isSendSmsOwnerNumber"] == "1") {
            sendInvoiceBySMS(phoneNumberSms, customerName, billNo, payCash, due, totalNetAmt, grossAmt, true);
        }
    }

}



function updateDueAdjustment(cusId, changeAmt, payCash, grossAmt, returnTotalAmt, paidReturnAmt, preDue) {

    var url = baseUrl + "Admin/SaleBundle/View/Invoice.aspx/updateDueAdjustmentAction";

    var jsonData = {
        "cusId": cusId,
        "changeAmt": changeAmt,
        "payCash": payCash,
        "grossAmt": grossAmt,
        "returnAmt": returnTotalAmt,
        "paidReturnAmt": paidReturnAmt,
        "preDue": preDue
    };

    $.ajax({
        url: url,
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var advanceAmt = 0;
            if (data.d == "false") {
                showMessage("Due adjustment fail", "Warning");
            }
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}




function updateAdvanceSaleCustomer(cusId, advanceAmt) {

    var url = baseUrl + "Admin/SaleBundle/View/Invoice.aspx/updateCustomerAdvanceAction";

    var jsonData = {
        "cusId": cusId,
        "advanceAmt": advanceAmt
    };

    $.ajax({
        url: url,
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d != true) {
                showMessage("Advance save fail", "Warning");
            }

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });

}




function getAdditionalDueAmt() {
    var payCash = $('#contentBody_txtPayCash').val();
    var preDue = $('#contentBody_txtPreviousDue').val();
    var grandAmt = $('#contentBody_txtGrossAmt').val();
    var additionalDue = 0;

    if (payCash == null || payCash == "")
        payCash = 0;
    if (preDue == null || preDue == "")
        payCash = 0;
    if (grandAmt == null || grandAmt == "")
        payCash = 0;

    if (parseFloat(payCash) > parseFloat(grandAmt)) {
        additionalDue = parseFloat(payCash) - parseFloat(grandAmt);
    }
    else {
        additionalDue = 0;
    }
    return additionalDue;
};
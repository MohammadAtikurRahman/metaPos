
var getForm;
function getParameterFromUrl() {
    // get value form URL Parameter
    var getSaleId = getUrlParameter("id");
    var getCustomerId = getUrlParameter("cusid");
   // var getDisc = getUrlParameter("disamt");
    getForm = getUrlParameter("from");


    if (getSaleId != undefined && getCustomerId != undefined && getForm != undefined) {
        // Calling setting access value  function
        getSettingAccessValue();

        // Reset Invoice
        resetInvoice();

        // Sale search

        var billNo = getSaleId;
        if (billNo == "") {
            billNo = getUrlParameter("id");
            if (billNo != "")
                isUpdate = true;
        }


        $('#contentBody_lblBillingNo').text(billNo);
       // $('#contentBody_txtDiscAmt').val(totalDiscAmt);///disc amt show when slip page action click
        $('#btnSaveInvoice').addClass("disNone");
        $('#btnUpdateInvice').removeClass("disNone");
        $('#btnSuspend').removeClass("disNone");

        if (getUrlParameter("status") == "draft") {
            $('#btnDraftInvoice').addClass("disNone");
            $('#btnConfirmSale').removeClass("disNone");
            $('#btnUpdateInvice').addClass("disNone");
            
        }
        else if (getUrlParameter("status") == "suspended" || getUrlParameter("status") == "Suspended") {
            $('#btnDraftInvoice').addClass("disNone");
            $('#btnConfirmSale').addClass("disNone");
            $('#btnUpdateInvice').addClass("disNone");
            $('#btnSuspend').addClass("disNone");
        }
        else {
            $('#btnDraftInvoice').addClass("disNone");
            $('#btnConfirmSale').addClass("disNone");
        }
        //now call line 50
        searchSaleOption(false);
        //searchSaleOptionNext(billNo, true);

        
    }
    else if (getSaleId == undefined && getCustomerId != undefined && getForm == undefined) {
        // Customer Search

    }

    // Load Customer Info
    if (getCustomerId != undefined)
        getCustomerListSelectedByCusID(getCustomerId);

}



function searchSaleOptionNext(billNo, isUpdateStatus) {
        
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getInvoicePrintDataAction",
        data: JSON.stringify({ "billNo": billNo }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var slipData = JSON.parse(data.d);
            var jsonPrintGlobalData = slipData;

            localStorage.setItem("jsonPrintGlobalData", JSON.stringify(jsonPrintGlobalData));

            if (isUpdateStatus) {

                for (var i = 0; i < slipData.length; i++) {
                    setProductToCart(slipData[i]);
                }

                changeReturn();
                updateCartAccountSection();
            }
            else {
                if (columnAccess[0]["printWithAutoSave"] == "1") {
                    printInvoiceDiv();
                }
            }

        }
    });

}




function setProductToCart(jsonSlipData) {
    isUpdate = true;
    // Set Cart Data
    var prodCode = jsonSlipData.prodCode;
    var div = $("<tr class='dynamicContentUnit' id='dynamicContentUnit'/>");
    var itemData = itemAddToShoppingCart(prodCode);
    div.html(itemData);
    $("#contentBody_divShoppingList").append(div);

    // inilize value
    var prodId = jsonSlipData.prodID;
    var billNo = jsonSlipData.billNo;
    var itemName = jsonSlipData.prodPackName;
    var maxQty = 0;
    var qty = jsonSlipData.qty;
    var returnQty = jsonSlipData.returnQty;
    var category = jsonSlipData.CategoryName;
    var supplier = jsonSlipData.supcompany;
    var bPrice = jsonSlipData.bPrice;
    var sPrice = jsonSlipData.sPrice;
    var commission = jsonSlipData.commission;
    var dealerPrice = jsonSlipData.dealerPrice;
    var unitId = jsonSlipData.unitId;
    var engineNumber = jsonSlipData.engineNumber;
    var cecishNumber = jsonSlipData.cecishNumber;
    var unitRatio = jsonSlipData.unitRatio;

    var storeName = jsonSlipData.storeName;
    var locationName = jsonSlipData.locationName;
    var storeId = jsonSlipData.storeId;
    var strImei = jsonSlipData.imei;
    var serialNo = jsonSlipData.serialNo;

    var imeiList = "";
    // split IMEI as a single value
    if (strImei != "" && strImei != null) {
        var singImei = strImei.split(',');

        // make list for html view
        imeiList = "<ul id='ddlImei'>";
        for (var i = 0; i < singImei.length - 1; i++) {
            imeiList += '<li>' + singImei[i] + ' <span class="btnCloseImei" imeiVal="' + singImei[i] + '">×</span></li>';
        };
        imeiList += "</ul>";

    }

    // check validation
    if (prodId == null || prodId == "") {
        showMessage("ProductId is not valid", "Warning");
        return;
    }

    if (maxQty == null || maxQty == "" || maxQty == undefined)
        maxQty = 0;

    if ((maxQty <= 0) && (billNo == "0")) {
        if (columnAccess[0]["stayAfterSearchProduct"] == "0")
            $('#contentBody_txtSearchNameCode').val();
        showMessage("Qty is not avabilable", "Warning");
        return;
    }



    if (category == null || category == "" || category == undefined)
        category = "0";
    if (supplier == null || supplier == "" || supplier == undefined)
        supplier = "0";
    if (bPrice == null || bPrice == "" || bPrice == undefined)
        bPrice = "0";
    if (sPrice == null || sPrice == "" || sPrice == undefined)
        sPrice = "0";
    if (dealerPrice == null || dealerPrice == "" || dealerPrice == undefined)
        dealerPrice = "0";
    if (serialNo == null || serialNo == "" || serialNo == undefined)
        serialNo = "0";


    // get the current row
    var currentRow = $(div).closest("tr");
    currentRow.find(".lblItemId").text(prodId);

    currentRow.find(".lblProdName").text(itemName);
    currentRow.find(".lblProdCategory").text(category);
    currentRow.find(".lblProdSupplier").text(supplier);
    currentRow.find(".txtUnitPrice").val(sPrice);
    currentRow.find(".lblUnitPrice").text(sPrice);
    currentRow.find(".lblBuyPrice").text(bPrice);
    currentRow.find(".supCommission").text(commission + "%");
    currentRow.find(".lblDealerPrice").text(dealerPrice);
    currentRow.find(".ddlQty").val(qty);
    currentRow.find(".lblUnitId").text(unitId);
    currentRow.find(".lblUnitValue").text(unitRatio);
    currentRow.find(".lblTotalQty").text(maxQty);
    
    currentRow.find(".txtReturn").val(returnQty);
    if (parseFloat(returnQty) > 0) {
        currentRow.find(".txtReturn").attr("disabled", true);
    }

    currentRow.find(".lblEngineNumber").text(engineNumber);
    currentRow.find(".lblcecishNumber").text(cecishNumber);
    currentRow.find(".lblImei").html(imeiList == "" ? 'N/A' : imeiList);
    currentRow.find(".lblSearchType").text("product");
    currentRow.find(".lblStore").text(storeId);
    currentRow.find(".lblStoreName").text(storeName);
    currentRow.find(".lblLocation").text(locationName);
    currentRow.find(".lblSerialNo").text(serialNo);
    var returnAmt = jsonSlipData.returnAmt;


    if (billNo != "0") {
        currentRow.find(".ddlQty").attr("disabled", true);
        currentRow.find(".txtUnitPrice").attr("disabled", true);
        currentRow.find(".remove").addClass("disNone");

        currentRow.find(".serialNo").attr("disabled", true);
    }



    var dealerStatusActive = $("[id*=contentBody_divCusType] input:checked").val();
    var price = 0;
    if (dealerStatusActive == "0") {
        currentRow.find(".txtUnitPrice").val(sPrice);
        price = sPrice;
    }
    else {
        currentRow.find(".txtUnitPrice").val(dealerPrice);
        price = dealerPrice;
    }

    // offer
    if (columnAccess[0]["offer"] == "1") {
        var offer = jsonSlipData.offer;
        var offerType = jsonSlipData.offerType;
        currentRow.find(".lblFree").text(offer);
        currentRow.find(".lblOfferType").text(offerType);

        currentRow.find(".offerValue").addClass("disNone");
        currentRow.find(".discountType").addClass("disNone");
        currentRow.find(".discountValue").addClass("disNone");

        if (parseFloat(offer) > 0) {

            currentRow.find(".txtReturn").addClass("disNone");
        }

    }

    var attributeRecord = jsonSlipData.attributeRecord;
    console.log("attributeRecord:", attributeRecord);
    if (attributeRecord != "0") {
        $.ajax({
            url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getAttributeNameDataAction",
            data: JSON.stringify({ "attributeRecord": attributeRecord }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (dataAttr) {
                console.log("Data Attribute:", dataAttr.d);

                if (dataAttr.d != "") {
                    variantValue = dataAttr.d;
                }

                var currentRow = $(div).closest("tr");
                currentRow.find(".lblAttributeValue").text(variantValue);
                currentRow.find(".attribute-section").removeClass('disNone');

            }

        });
    }


    $('#contentBody_txtReturnPaid').val(returnAmt.toFixed(2));
    $('#contentBody_txtReturnTotal').val(returnAmt.toFixed(2));


    // Reset value
    if (columnAccess[0]["stayAfterSearchProduct"] == "0")
        $.trim($('#contentBody_txtSearchNameCode').val(""));

}













$('#btnSearch').on("click", function () {


    // Reset Invoice
    resetInvoice();

    var searchType = $('.toggle').hasClass("off");
    console.log("searchType:", searchType);
    searchSaleOption(searchType);


});


// Invoice next opt
function nextInvoice() {

}


// Invoice Previous
function previousInvoice() {

}


function searchSaleOption(searchType) {
    isUpdate = true;

    var searchOption = $('#contentBody_chkSearchOption').val();
    var searchTxt = $('#contentBody_txtBillingNo').val();
    if (searchTxt == "" || searchTxt == null) {
        searchTxt = getUrlParameter("id");
    }


    if (isException(searchOption)) {
        showMessage("Search option is not valid", "Warning");
        return;
    }

    if (isException(searchTxt)) {
        showMessage("Invoice no is not valid", "Warning");
        return;
    }

    // Set form saleInfo
    var isError = searchSale(searchTxt, searchType);

    // Load Customer Info
    changeCustomerUpdateInfo();

    if (isError) {
        $('#divSpecialSearch').removeClass("open");
        resetInvoice();
        return;
    }

    // add item search block 
    $('#contentBody_txtSearchNameCode').attr("disabled", true);
    $('#btnAddToCart').attr("disabled", true);
    $('#thReturn').removeClass("disNone");

    var billNo = searchTxt;
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getStockStatusDataListAction",
        data: JSON.stringify({ "billNo": billNo }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsonData = $.parseJSON(data.d);
            var leng = jsonData.length;

            for (var i = 0; i < leng; i++) {

                var stockJsonData = jsonData[i];
                searchType = jsonData[i].searchType;

                if (searchType == "product") {
                    updateCartItemData(billNo, stockJsonData.prodCode);
                }
                else if (searchType == "salePackage") {
                    updatePackageItem(billNo, stockJsonData.prodCode, stockJsonData.sPrice, stockJsonData.qty);
                }
                else {
                    searchCartItemByService(billNo, stockJsonData.prodCode);
                }

                // Set last BillNo for print
                $('#contentBody_lblBillNoHidden').val(searchTxt);


            }
            console.log("test action");

            updateCartAccountSection();
        },
        failure: function (response) {
            console.log(response);
            alert(response);
        },
        error: function (response) {
            console.log(response);
            alert(response);
        }
    });



    // Reminder date 
    getReminderDateDataDisplayInInvoice(searchTxt);

    // Display
    $('#divReturnTotal').removeClass("disNone");
    $('#divReturnPaid').removeClass("disNone");



    //$('#').removeClass("disNone");

    // Assaign to another searchbox
    $('#contentBody_txtBillingNoSearch').val(searchTxt);
    // Close overly
    $('#divSpecialSearch').removeClass("open");

    $('#divPaid').removeClass("disNone");

    // discount, vat textbox to label
    $('#contentBody_txtVatAmt').addClass("aspNetDisabled form-control txtboxBg TxtboxToLabel").attr("disabled", true);;
    $('#contentBody_txtDiscAmt').addClass("aspNetDisabled form-control txtboxBg TxtboxToLabel").attr("disabled", true);;
    $('#divVat').addClass("no-padding-important");
    $('#divDiscountOption').addClass("disNone");
    $('#btnEdit').addClass("disNone");

    $('#divSearchBox').addClass("disNone");
    $('#divSaleUpdateTitle').removeClass("disNone");

}


function getReminderDateDataDisplayInInvoice(billNo) {

    $.ajax({
        url: baseUrl + "Admin/InstallmentBundle/View/Default.aspx/getReminderInfoAction",
        data: JSON.stringify({ "billNo": billNo }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d == "")
                return;

            var jsonData = JSON.parse(data.d);

            var jsonDate = jsonData[0].nextPayDate;

            var date = new Date(parseInt(jsonDate.substr(6)));
            //var month = date.getMonth() + 1;
            //var Nextdate = date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();



            $('#contentBody_txtCustomerRemainder').val(moment(date).format("DD-MMM-YYYY"));
            $('#contentBody_txtInstalmentNumber').val(jsonData[0].instalmentNumber);
            $('#contentBody_txtDownPayment').val(jsonData[0].downPayment);

            $('#contentBody_txtInstalmentNumber').attr("disabled", true);
            $('#contentBody_txtCustomerRemainder').attr("disabled", true);
            $('#contentBody_txtDownPayment').attr("disabled", true);
            $('#ddlDateSeletor').attr("disabled", true);

            $('#contentBody_txtInstalmentAmount').val(jsonData[0].downPayment);

            $('#divInstalmentAmtShow').removeClass('disNone');
            $('#divInstalment').addClass('disNone');

        },
        failure: function (response) {
            console.log(response);
            alert(response.statusText);
        },
        error: function (response) {
            console.log("Error:", response);
            $('.errorMsgTitle').text(response.statusText);
            $('#errorMsgTxt').html(response.responseText);
            $('#errorMsgModal').modal("show");
            $('#errorMsgModal').modal({ backdrop: 'static', keyboard: false });
        }

    });
};


function invoiceCustomerSearch(searchTxt) {

    if (searchTxt.indexOf('(') > 0) {
        var splitSearchTxt = searchTxt.split('(');
        searchTxt = splitSearchTxt[1].slice(0, -1);
    }

    getCustomerListSelectedByCusID(searchTxt.trim());

    updateCartAccountSection();
}



function searchSale(searchTxt, searchType) {

    if (!searchType) {

        invoiceBillNoSearch(searchTxt);
    }
    else {

        invoiceCustomerSearch(searchTxt);
    }

    updateCartAccountSection();

};




function invoiceBillNoSearch(searchTxt) {


    var isError = false;
    returnTotal = 0;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getInvoiceSaleDataListAction",
        data: JSON.stringify({ "searchTxt": searchTxt }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var jsonData = $.parseJSON(data.d);

            //var cusId = jsonData[0].Id;
            var cusId = jsonData[0].cusID;
            var billNo = searchTxt;
            var netAmt = jsonData[0].netAmt;
            var vatAmt = jsonData[0].vatAmt;
            var vatType = jsonData[0].vatType;
            var grossAmt = jsonData[0].grossAmt;
            var paidAmt = jsonData[0].balance;
            var giftAmt = jsonData[0].giftAmt;
            var returnAmt = 0;
            var payMethod = jsonData[0].payMethod;
            var staffId = jsonData[0].salesPersonId;
            var referredBy = jsonData[0].referredBy;
            var cartType = jsonData[0].cardId;
            var token = jsonData[0].token;
            var discAmt = jsonData[0].discAmt;
            var discType = jsonData[0].discType;
            var balance = jsonData[0].balance;
            var loadCost = jsonData[0].loadingCost;
            var unloadCost = jsonData[0].unloadingCost;
            var shipCost = jsonData[0].shippingCost;
            var carryCost = jsonData[0].carryingCost;
            var salePersonType = jsonData[0].salePersonType;
            var status = jsonData[0].status;
            var additionalDue = jsonData[0].additionalDue;
            var retrunAmt = jsonData[0].returnAmt;
            var serviceCharge = jsonData[0].serviceCharge;

            var refName = jsonData[0].refName;
            var refPhone = jsonData[0].refPhone;
            var refAddress = jsonData[0].refAddress;


            // suspend check 
            if (status == "0") {
                $('#btnSaveInvoice').addClass("disNone");
                $('#btnSaveInvoice').attr("disable", true);
                $('#btnSaveInvoice').addClass("disNone");
                $('#btnUpdateInvice').attr("disable", true);
                $('#btnSuspend').addClass("disNone");
                showMessage("This invoice is suspended!!", "Warning");
            }
            else {
                $('#btnSuspend').removeClass("disNone");
                $('#btnSaveInvoice').addClass("disNone");
                $('#btnUpdateInvice').removeClass("disNone");
            }


            if (isException(cusId)) {
                isError = true;
                showMessage("Customer is invalid", "Warning");
                return;
            }

            if (isException(billNo)) {
                isError = true;
                showMessage("BillNo is invalid", "Warning");
                return;
            }

            if (isException(netAmt)) {
                isError = true;
                showMessage("Net amount is invalid", "Warning");
                return;
            }

            if (isException(grossAmt)) {
                isError = true;
                showMessage("Grand amount is invalid", "Waning");
                return;
            }


            if (isException(payMethod)) {
                showMessage("Pay method is invalid", "Warning");
                payMethod = 0;
            }

            if (isException(cartType)) {
                showMessage("Cart payment is invalid", "Warning");
                cartType = 0;
            }

            if (isException(vatAmt)) {
                vatAmt = 0;
            }

            if (isException(token)) {
                token = "";
            }

            if (isException(discType)) {
                discType = "";
            }

            if (isException(balance)) {
                balance = 0;
            }

            if (isException(loadCost)) {
                loadCost = 0;
            }

            if (isException(unloadCost)) {
                unloadCost = 0;
            }

            if (isException(shipCost)) {
                shipCost = 0;
            }

            if (isException(carryCost)) {
                carryCost = 0;
            }

            // discount calculate
            discAmt = discountCalculate(netAmt, discAmt, discType);


            // parse to float
            netAmt = parseFloat(netAmt);
            grossAmt = parseFloat(grossAmt);
            vatAmt = parseFloat(vatAmt);
            paidAmt = parseFloat(paidAmt);
            giftAmt = parseFloat(giftAmt);
            returnAmt = parseFloat(returnAmt);
            additionalDue = parseFloat(additionalDue);
            retrunAmt = parseFloat(retrunAmt);

            $('#contentBody_ddlCustomer').val(cusId).change();
            $("#contentBody_ddlCustomer").attr("disabled", "disabled");
            $("#btnCustomerModal").attr("disabled", "disabled");
            $('#contentBody_lblBillingNo').text(billNo);
            $("#contentBody_txtNetAmt").val(netAmt.toFixed(2));
            $('#contentBody_txtVatAmt').val(vatAmt.toFixed(2));
            $('#contentBody_ddlVatAmt').val(vatType);
            $("#contentBody_ddlVatAmt").attr("disabled", "disabled");
            $('#contentBody_txtGrossAmt').val(grossAmt.toFixed(2));
            //$('#contentBody_txtGiftAmt').val(giftAmt.toFixed(2));
            $('#contentBody_ddlPayType').val(payMethod);
            $('#contentBody_txtReturnPaid').val(retrunAmt.toFixed(2));
            $('#contentBody_txtReturnTotal').val(retrunAmt.toFixed(2));
            $('#contentBody_ddlCardType').val(cartType);
            $('#contentBody_txtToken').val(token);

            if (discType == "" ? discType = "tk" : discType)
                $('#contentBody_txtDiscAmt').val(discAmt.toFixed(3));

            $('#contentBody_lblDiscType').text("(" + discType + ")");
            $('#contentBody_lblDiscType').removeClass("disNone");



            $('#contentBody_ddlDiscType').val(discType);
            $('#contentBody_txtPayDate').val("");
            $('#contentBody_txtBalance').val(balance);
            $('#contentBody_txtLoadingCost').val(loadCost);
            $('#contentBody_txtUnloading').val(unloadCost);
            $('#contentBody_txtShippingCost').val(shipCost);
            $('#contentBody_txtCarryingCost').val(carryCost);
            $('#contentBody_txtServiceCharge').val(serviceCharge);


            $('#contentBody_txtRefName').val(refName);
            $('#contentBody_txtRefPhone').val(refPhone);
            $('#contentBody_txtRefAddress').val(refAddress);

            $('#btnEditDate').addClass('disNone');
            $('#dateEdited').removeClass('billing-invoice').addClass('padding-top-four');


            if (salePersonType == "1") {
                $('#contentBody_lblSaleById').text(staffId);
                var nameAsLogin = getSalesPersonByRoleId(staffId);
                $('#contentBody_lblSaleBy').text(nameAsLogin);
            }
            else {
                $('#contentBody_ddlStaff').val(staffId);
            }

            //$('#contentBody_ddlReferredBy').val(referredBy).change();;

            trackGrossAmt = jsonData[0].grossAmt;

            // hide advance pay option because of advance not completed when invoice update
            $('#divAdvance').addClass('disNone');
        },
        failure: function (response) {
            console.log(response);
            alert(response);
        },
        error: function (response) {
            console.log(response);
            alert(response);
        }
    });
    return isError;
}





function getSaleInfoData(searchTxt, billNo) {
    var jsonData;
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getItemSaleDataListAction",
        data: JSON.stringify({ "billNo": billNo }),
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            jsonData = $.parseJSON(data.d);
        },
        failure: function (response) {
            console.log(response);
            alert(response);
        },
        error: function (response) {
            console.log(response);
            alert(response);
        }
    });

    return jsonData;
}


// Get URL Parameter GO Action
function getUrlParameter(sParam) {
    var sPageUrl = decodeURIComponent(window.location.search.substring(1)),
        sUrlVariables = sPageUrl.split('&'), sParameterName, i;

    for (i = 0; i < sUrlVariables.length; i++) {
        sParameterName = sUrlVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1] == undefined ? true : sParameterName[1];
        }
    }
};


function discountCalculate(netAmt, discAmt, discType) {
    if (discType == "%") {
        discAmt = (parseFloat(discAmt) * 100) / parseFloat(netAmt);
    }
    return discAmt;
}

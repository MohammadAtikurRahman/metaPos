//global Variable 
var supComm = 0,
    isOffer,
    isUnit,
    isCartCategory,
    isCartSuppilier,
    isSupplierCommission,
    globalTotalNetAmt = 0,
    trackGrossAmt = 0,
    deleteCartProductSaleId = "",
    deleteCartProductSotckStatusId = "",
    isEngineNumber = "",
    isCecishNumber = "",
    iCounter = 0,
    lastProdCodeForImei = "",
    isDisplayLocation = "";

var isUpdate = false,
    isImeiStatusOpen = false;

var imeiListShopCart = [];


//
var totalNetAmt = 0, totalDiscAmt = 0, grossAmt = 0, totalMiscAmt = 0, totalVatAmt = 0, totalDueAmt = 0, cartTotal = 0, returnTotal = 0;

// On event load
function eventChange() {

    // action focus
    $("#contentBody_txtSearchNameCode").focus();


    initOptionVisibility();

    loadBtn();

    getParameterFromUrl();

    var roleId = $('#contentBody_lblSaleById').text();
    getSalesPersonByRoleId(roleId);



    $(document).ready(function () {
        // Stop user to press enter in textbox
        $("input:text").keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });
    });
}



// barcode -> add product 
var barcode = "", bacodeCounter = 0;
$("#contentBody_txtSearchNameCode").keyup(function (e) {
    console.log("action by code");
    var searchVal = $(this).val();
    if (searchVal == "") {
        return;
    }

    productId = getProductIdByProductCode(searchVal);
    var searchType = $("#contentBody_rblSearchIn input:checked").val();
    var code = (e.keyCode ? e.keyCode : e.which);
    console.log("code::", code);

    if (code == 13)// Enter key hit
    {
        AddToCartSalesItem(productId, searchType)
    }
    else if (code == 9)// Tab key hit
    {
        AddToCartSalesItem(productId, searchType)
    }


    $('#contentBody_txtSearchNameCode').focus();

});


$('#btnAddToCart').click(function () {

    var searchVal = $.trim($('#contentBody_txtSearchNameCode').val());
    if (searchVal == null || searchVal == '') {
        alert("Please select a product");
        return;
    }


    if (isImeiStatusOpen == true) {
        showMessage("Select an IMEI", "Warning");
        return;
    }


    var productId = "";
    if (!searchVal.includes('[')) {
        productId = getProductIdByProductCode(searchVal);
    }
    else
    {
        var splitSearchVal = searchVal.split('[');
        if (splitSearchVal.length > 1) {
            productId = $.trim(splitSearchVal[splitSearchVal.length - 1].slice(0, -1));
        }
    }


    var searchType = $("#contentBody_rblSearchIn input:checked").val();
    AddToCartSalesItem(productId, searchType)

});




function AddToCartSalesItem(itemId, searchType) {

    if (searchType == "1") {
        AddToCartItenByPackage(itemId);
    }
    else if (searchType == "2")
    {
        AddToCartItenByService(itemId);
    }
    else if (searchType == "3")
    {
        AddToCartItemByIMEI(itemId);
    }
    else
    {
        AddToCartItemByProduct(itemId);
    }

    if (columnAccess[0]["imei"] == "1") {
        loadIMEIByProductCode(itemId);
    }
}




function loadBtn() {

    // fist time load save and update button
    $('#btnSaveInvoice').removeClass("disNone");
    $('#btnUpdateInvice').addClass("disNone");


    $("#contentBody_ddlCustomer").select2({
        placeholder: Select_a_customer,
        closeOnSelect: false,
        allowClear: true
    });
}


// Save button click event
$("#contentBody_btnSave #contentBody_btnUpdate").click(function () {
    if ($("#contentBody_txtPayCash").val() == "") {
        $("#contentBody_txtPayCash").val("0");
    }
});


// Option visibility
function initOptionVisibility() {

    var miscCost = columnAccess[0]["isMiscCost"];
    if (miscCost == "1") {
        $('#tabMiscCost').removeClass("disNone");
        $('#divMisc').removeClass("disNone");
    }

    var referredBy = columnAccess[0]["isReferredBy"];
    if (referredBy == "1") {
        $('#divReferredBy').removeClass("disNone");
        $('#divReferredBy').removeClass("disNone");

        //
        $('#liRefferal').addClass("disNone");
    }

    var btnGodownInvoice = columnAccess[0]["isGodownInvoice"];
    if (btnGodownInvoice == "1") {
        $('#contentBody_btnPrintGodown').removeClass("disNone");
    }


    var payDate = columnAccess[0]["paydate"];
    if (payDate == "1") {
        $('#divPayDate').removeClass("disNone");
    }

    var token = columnAccess[0]["token"];
    if (token == "1") {
        $('#divToken').removeClass("disNone");
    }

    var advance = columnAccess[0]["isAdvanced"];
    if (advance == "0") {
        $('#divAdvanced').addClass("disNone");
        $('#divCheckAdvanced').removeClass('disNone');
    }
    else {
        $('#divAdvanced').removeClass("disNone");
    }

    if (columnAccess[0]["isDisplaySerialNo"] == "1") {
        $('#thSerialNo').removeClass('disNone');
    }

    isCartCategory = [0]["isCartCategory"];


    isCartSuppilier = columnAccess[0]["isCartSupplier"];

    isDisplayLocation = columnAccess[0]["displayLocation"];

    isSupplierCommission = columnAccess[0]["isSupplierCom"];


    isEngineNumber = columnAccess[0]["isEngineNumber"];


    isCecishNumber = columnAccess[0]["isCecishNumber"];


    isUnit = columnAccess[0]["isUnit"];


    //if (columnAccess[0]["isDueAdjustment"] == "1") {
    //    $('#divDue').addClass("disNone").removeClass("disBlock");
    //    $('#divChange').removeClass("disBlock").addClass("disNone");
    //}

    if (columnAccess[0]["displayDiscountInSale"] === "1") {
        $('#divDiscount').removeClass("disNone");
    }

    if (columnAccess[0]["displayCartBuyPrice"] == "1") {
        $('#thBuyPrice').removeClass("disNone");
    }
    if (columnAccess[0]["displayCartWholesalePrice"] == "1") {
        $('#thWholeSalePrice').removeClass("disNone");
    }

    if (columnAccess[0]["displayCartRetailPrice"] == "1") {
        $('#thRetailPrice').removeClass("disNone");
    }


    if (columnAccess[0]["autoSalesPerson"] == "1") {
        $('#divSaleByLabel').removeClass("disNone");
    }
    else {
        $('#contentBody_ddlStaff').removeClass("disNone");
    }

    if (columnAccess[0]["displayCartTotalQty"] == "1") {
        $('#thTotalQty').removeClass("disNone");
    }


    if (columnAccess[0]["isWholeSeller"] == "1") {
        $('#contentBody_divCusTypeInvoice').removeClass("disNone");

    }

    if (columnAccess[0]["searchBy"] == "1") {
        $('#contentBody_divSearchBy').removeClass("disNone");
    }

    if (columnAccess[0]["isVatSale"] == "1") {
        $('#divVat').removeClass("disNone");
    }

    if (columnAccess[0]["displayPaperSizeSelectOption"] == "1") {
        $('#divDisplayPaperSize').removeClass("disNone");
    }

    if (columnAccess[0]["isCustomerReminder"] == "1") {
        $('#divCustomerRemainder').removeClass("disNone");
    }


    if (columnAccess[0]["displayInterest"] == "1") {
        $('#divInterestAmt').removeClass("disNone");
        $('#divInterestRate').removeClass("disNone");
    }

    if (columnAccess[0]["displayExtraDiscount"] == "1") {
        $('#divExtraDiscount').removeClass("disNone");
    }

    if (columnAccess[0]["displayInstalment"] == "1") {
        $('#divInstallmentStatus').removeClass("disNone");
        $('#divAccountNo').removeClass("disNone");
        $('#divInstalment').removeClass("disNone");
    }
    else {
        $('#divInstalment').addClass("disNone");
    }



    if (columnAccess[0]["displayGuarantor"] == "1") {
        $('#liRefferal').text("Guarantor");
    }

    //if (columnAccess[0]["displaySearchCustomer"] == "0") {
    //    $("#contentBody_ddlCustomer").attr("disabled", "disabled");
    //}

    var recipt = columnAccess[0]["receipt"];
    console.log("recipt::::", recipt);

    if (columnAccess[0]["receipt"] == "1") {
        $('#btnPrintReceipt').removeClass("disNone");
    }

    if (columnAccess[0]["tokenRcpt"] == "1") {
        $('#btnPrintTokenRcpt').removeClass("disNone");
    }

    console.log("display Draft:", columnAccess[0]["displayDraft"]);
    if (columnAccess[0]["displayDraft"] == "1") {
        $('#btnDraftInvoice').removeClass("disNone");
    }


    if (columnAccess[0]["isGodownInvoice"] != "0") {
        $('#btnPrintChallan').removeClass("disNone");
    }


    if (columnAccess[0]["isMultipay"] == "1") {
        $('#divAddButton').removeClass("disNone");

        //$('#btnQuickPayOne').addClass('disNone');
        $('#divPayOne').addClass('disNone');
        $('#btnQuickPayTwo').addClass('disNone');

        $('#divPayCashOneTextbox').addClass('col-sm-6').removeClass('col-sm-4');
        $('#divPayCashTowTextbox').addClass('col-sm-6').removeClass('col-sm-4');
    }
    else {
        $('#divPayCashOneTextbox').addClass('col-sm-6').removeClass('col-sm-4');
    }


    var CartDefaultProduct = columnAccess[0].CartDefaultProduct;
    if (CartDefaultProduct != "") {
        $('#btnLoadDefaultProduct').removeClass("disNone");
    }

    // Operation

    checkActionPermission("Edit");

    checkActionPermission("Delete");

}


// After Postback  / Page end call
try {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function (s, e) {
        initOptionVisibility();

        $("#contentBody_txtSearchNameCode").on("keyup paste", function () {
            SearchCartAllItem();
        });

    });
}
catch (e) {

}


var actionAccess = "";
function checkActionPermission(pageName) {
    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/checkPagePermissionAction",
        dataType: "json",
        data: JSON.stringify({ "pageName": pageName }),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {


            actionAccess = data.d;
            if (actionAccess == false && pageName == "Edit") {
                $('#btnUpdateInvice').attr("disabled", 'disabled');
                $('#btnUpdateInvice').addClass("disNone");
            }

            if (actionAccess == false && pageName == "Delete") {
                $('#btnSuspend').attr("disabled", 'disabled');
                $('#btnSuspend').addClass("disNone");
            }

            actionAccess = data.d;
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}

function getSalesPersonByRoleId(roleId) {
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getCurrentLoginUsernameAction",
        data: '{roleId:"' + roleId + '"}',
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#contentBody_lblSaleBy').text(data.d);
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

}



function getProductIdByProductCode(productCode) {

    if (productCode.indexOf('"') >= 0) {
        productCode = productCode.split('"').join('\\"')
    }

    var prodId = "";
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getProductIdByProductCodeAction",
        data: '{productCode:"' + productCode + '"}',
        dataType: 'json',
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            prodId = data.d;
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

    return prodId;
}



$(document).bind("ajaxSend", function () {

    try {
        initRequest();
    }
    catch (e) {

    }

}).bind("ajaxComplete", function () {

    try {
        endRequest();
    }
    catch (e) {

    }

});


$(document).keypress(
    function (event) {
        if (event.which == '13') {
            event.preventDefault();
        }
    });



/*** Cart Auto loader ***/

$('#btnLoadDefaultProduct').click(function () {
    var cartDefaultProduct = columnAccess[0].CartDefaultProduct;

    var splitCartDefaultProduct = cartDefaultProduct.split(',');

    for (var i = 0; i < splitCartDefaultProduct.length; i++) {

        var productId = splitCartDefaultProduct[i];
        if (productId != "")
            AddToCartItemByProduct(productId);
    }

});


function AddToCartItemByIMEI(IMEI) {
    var obj = {};
    obj.IMEI = IMEI;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getProductIDByIMEIAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var prodId = data.d;

            AddToCartItemByProduct(prodId);


            //  Enable IMEI
            if (columnAccess[0]["imei"] == "1") {
                loadIMEIByProductCode(prodId);
            }
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
}




function changeLanguage() {
    if (columnAccess[0]["language"] == "bn")
        $('#contentBody_lblDueChage').text("ফেরত");
    else
        $('#contentBody_lblDueChage').text("Change");
}



function changeCurrentBalanceLanguage() {
    if (columnAccess[0]["language"] == "bn")
        $('#contentBody_lblDueChage').text("বর্তমান হিসাব");
    else
        $('#contentBody_lblDueChage').text("Current Balance");
}

var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";

var billNo = getUrlParameter('billNo');
var type = getUrlParameter('type');
var rcpt = getUrlParameter('rcpt');

console.log("rcpt=", rcpt);


$(function () {
    getSettingAccessValue();

    var width = columnAccess[0]["smallPrintPaperWidth"];
    width = parseFloat(width) * 3.77;
    console.log("width:", width);

    if (type == "0") {
        $('#invoiceBody').css({ width: width });
        $('.invoice-text').css({ width: "60%" });
        $('.billing-detail td').css({ padding: "0" });
        $('.pay-type').text('Type');

        $('.details').text('Details');
        $('.unit-cost').text('Cost');
        $('.signature-area').addClass('disNone');
        $('#brandLogo').addClass('disNone');
    }

    printInvoiceAction();

});


var variantValue = "";
var couterSL = 1;
var totalReturnQty = 0;


function printInvoiceAction() {
    var jsonPrintGlobalData = localStorage.getItem("jsonPrintGlobalData");
    var jsonData = $.parseJSON(jsonPrintGlobalData);

    // Header
    var invoiceTitle = jsonData[0].invoiceType;
    var payType = jsonData[0].payMethod;
    console.log("payMethod:" + jsonData[0].payMethod);
    var payTypeName = "";
    if (payType == 0)
        payTypeName = "Cash";
    else if (payType == 1)
        payTypeName = "Card";
    else if (payType == 2)
        payTypeName = "Cheque";
    else if (payType == 3)
        payTypeName = "Mobile Banking";
    else if (payType == 4)
        payTypeName = "EMI Plan (" + getEmiPlanMonth(jsonData[0].payType) + ")";
    else if (payType == 5)
        payTypeName = "Cash on delivery";

    $('#lblPayType').text(payTypeName);

    $('#lblBillNo').text(billNo);

    if (jsonData[0].status == "draft") {
        $('.invoice-text').text("Quotation");
        $('.invoice-text').css({ width: "60%" });
    }


    if (columnAccess[0]["displayBrandNamePrint"] == "1") {
        $('#lblShopTitle').removeClass('disNone');
        $('#lblShopTitle').text(jsonData[0].branchName == null ? "" : jsonData[0].branchName);
    }

    var phone, mobile;
    if (jsonData[0].branchPhone != null || "")
        phone = jsonData[0].branchPhone == null ? "" : "Phone: " + jsonData[0].branchPhone;

    if (jsonData[0].branchMobile != "" || null)
        mobile = jsonData[0].branchMobile == null ? "" : " Mobile: " + jsonData[0].branchMobile;

    $('#lblShopPhone').text(phone);
    $('#lblShopMobile').text(mobile);
    $('#lblShopAddress').html(jsonData[0].branchAddress == null ? "" : jsonData[0].branchAddress);

    //for diagnostic
    var conditionalId = "", conditionalName = "", conditionalAddress = "", conditionalEmail = "", conditionalPhone = "";
    if (columnAccess[0]["businessType"].trim() == "diagnostic") {

        conditionalId = "<td>ID</td><td>:</td>";
        conditionalName = "<td>Patient Name</td><td>:</td>";
        conditionalPhone = "<td>Phone</td><td>:</td>";
        conditionalEmail = "<td>Email</td><td>:</td>";
        conditionalAddress = "<td>Address</td><td>:</td>";

        $('#lblSex').html("<td>Sex</td><td>:</td><td>" + jsonData[0].sex + "</td>");
        $('#lblAge').html("<td>Age</td><td>:</td><td>" + jsonData[0].age + "</td>");
        $('#lblBloodGroup').html("<td>Blood Group</td><td>:</td><td>" + jsonData[0].bloodGroup + "</td>");

        $('.details').text("Name of tests");
        $('.product-qty').text("Items");

        $('.unit-txtbox').addClass('disNone');

    }

    //for gadget accesories
    if (columnAccess[0]["businessType"].trim() == "gadget") {
        conditionalId = "<td>Cus Id</td><td>:</td>";
        conditionalName = "<td>Name</td><td>:</td>";
        conditionalPhone = "<td>Phone</td><td>:</td>";
        conditionalEmail = "<td>Email</td><td>:</td>";
        conditionalAddress = "<td>Address</td><td>:</td>";

        $('.unit-txtbox').addClass('disNone');

    }

    $('#lblCusId').html(conditionalId + "<td>#" + jsonData[0].cusID + "</td>");
    $('#lblCusName').html(conditionalName + "<td>" + jsonData[0].name + "</td>");
    $('#lblPhone').html(conditionalPhone + "<td>" + jsonData[0].phone + "</td>");
    if (jsonData[0].mailInfo.trim() != "")
        $('#lblEmail').html(conditionalEmail + "<td>" + jsonData[0].mailInfo + "</td>");
    $('#lblAddress').html(conditionalAddress + "<td>" + jsonData[0].address + "</td>");


    var dateFormat;
    if (type == "0")
        dateFormat = "DD-MM-YY";
    else
        dateFormat = "DD-MMM-YYYY";
    console.log("DateTime::", moment(Date(jsonData[0].entryDate.substr(6))));
    $('#lblDate').text(moment(Date(jsonData[0].entryDate.substr(6))).format(dateFormat));
    $('#lblTime').text(moment(Date(jsonData[0].entryDate.substr(6))).format('h:mm a'));

    var saleBy = "";
    if (jsonData[0].isAutoSalesPerson == false)
        saleBy = jsonData[0].staffName == null ? "" : jsonData[0].staffName;
    else
        saleBy = jsonData[0].title == null ? "" : jsonData[0].title;

    if (saleBy == "")
        $('.sale-by').addClass('disNone');
    else
        $('#lblSaleBy').text(saleBy);


//logo

    if (columnAccess[0]["displayLogoPrint"] == "1") {
        $('#brandLogo').removeClass('disNone');
        if (jsonData[0].branchLogoPath != null)
            $('.brand-logo img').attr("src", baseUrl + "Img/Logo/" + jsonData[0].branchLogoPath);
    }


    // account 
    var miscCostAmt = parseFloat(jsonData[0].loadingCost) + parseFloat(jsonData[0].shippingCost) + parseFloat(jsonData[0].carryingCost) + parseFloat(jsonData[0].unloadingCost) + parseFloat(jsonData[0].serviceCharge);

    var previousDue = 0;
    var totalDue = 0;
    if (jsonData[0].status == "Sold") {

        previousDue = (parseFloat(jsonData[0].cusCurrentDue) + parseFloat(jsonData[0].balance)) - parseFloat(jsonData[0].grossAmt);

        totalDue = parseFloat(jsonData[0].grossAmt) + parseFloat(previousDue);
    }
    else {
        previousDue = (parseFloat(jsonData[0].cusCurrentDue) + parseFloat(jsonData[0].balance));

        totalDue = parseFloat(previousDue);
    }

    if (isNaN(previousDue))
        previousDue = 0;

    previousDue += parseFloat(jsonData[0].extraDiscount);

    if (parseFloat(jsonData[0].extraDiscount) > 0) {
        $('#trMoreDisc').removeClass('disNone');
        $('#lblMoreDisc').text(parseFloat(jsonData[0].extraDiscount).toFixed(2));
    }


    if (parseFloat(totalDue) < 0) {
        $('#lblTotalAmountLabel').text("Total Advance");
    }
    totalDue = Math.abs(totalDue);
    if (isNaN(totalDue))
        totalDue = 0;
    $('#lblTotalDue').text(ins1000Sep(parseFloat(totalDue).toFixed(2)));

    var currentDue = parseFloat(jsonData[0].cusCurrentDue);


    var discAmtWithoutMoreDisc = parseFloat(jsonData[0].discAmt) - parseFloat(jsonData[0].extraDiscount);
    $('#lblTotalAmt').text(ins1000Sep(parseFloat(jsonData[0].netAmt).toFixed(2)));


    if (columnAccess[0]["isMiscCost"] == "1" && parseFloat(miscCostAmt) > 0) {
        $('#divMiscCostPrint').removeClass("disNone");
        $('#lblMiscCost').text(ins1000Sep(miscCostAmt.toFixed(2)));
    }

    if (columnAccess[0]["isVatPrint"] == "1" && parseFloat(vatAmt) > 0) {
        $('#trVatAmt').removeClass('disNone');
        var vatType = jsonData[0].vatType;

        var vatAmt = jsonData[0].vatAmt;

        if (vatType == "%") {
            vatType = "(" + vatType + ")";
        }
        else {
            vatType = "";
            vatAmt = vatAmt.toFixed(2);
        }

        $('#lblVatAmt').text(vatAmt + vatType);
    }

    if (columnAccess[0]["displayDiscountPrint"] == "1" && parseFloat(discAmtWithoutMoreDisc) > 0) {
        $('#trDiscount').removeClass('disNone');
        $('#lblDiscount').text(ins1000Sep(discAmtWithoutMoreDisc.toFixed(2)));
    }

    var grandTotal = parseFloat(jsonData[0].grossAmt) + parseFloat(jsonData[0].extraDiscount);
    if (parseFloat(discAmtWithoutMoreDisc) + parseFloat(vatAmt) + parseFloat(miscCostAmt) > 0) {
        $('#lblGrandTotal').text(ins1000Sep(grandTotal.toFixed(2)));
        $('#divGrandTotal').removeClass('disNone');
    }
    $('#lblPreviousDue').text(ins1000Sep((previousDue.toFixed(2))));


    $('#lblCurrentPay').text(ins1000Sep(jsonData[0].payCash.toFixed(2)));

    if (currentDue < 0) {
        $('#lblBalanceLabel').text("Advance");
    }

    if (currentDue == 0) {
        var change = jsonData[0].return_;
        if (isNaN(change))
            change = 0;
        $('#lblBalanceLabel').text("Change");
        $('#lblCurrentDue').text(ins1000Sep(formatNum(Math.abs(parseFloat(change).toFixed(2)))));

    }
    else {
        if (isNaN(currentDue))
            currentDue = 0;
        $('#lblBalanceLabel').text("Due");
        $('#lblCurrentDue').text(ins1000Sep(formatNum(Math.abs(parseFloat(currentDue).toFixed(2)))));
    }

    //invoice header

    var headerInvoice = '';

    if (invoiceTitle == 0) {
        headerInvoice += '<h2 class="invoice-text">INVOICE</h2>';
        
    } else if (invoiceTitle == 1) {
        headerInvoice += '<h2 class="invoice-text">PARTNER INVOICE</h2>';
    }
    else if (invoiceTitle == 2) {
        headerInvoice += '<h2 class="invoice-text">HIRE RECIPT</h2>';
    }
   $('#invoiceHeaderContent').html(headerInvoice);



    // In word
    var currentPay = jsonData[0].balance == null || "" ? "0" : jsonData[0].balance;
    if (parseFloat(currentPay) > 0)
        $('#lblInWord').text("In Word: " + inWords(currentPay) + " Taka Only");


    //footer
    $('#lblFooterNote').html(jsonData[0].invoiceFooterNote == null ? "" : jsonData[0].invoiceFooterNote);



    $.each(jsonData, function (index, value) {
        totalReturnQty = parseFloat(value.returnQty);
    });




    $.each(jsonData, function (index, value) {

        var attributeRecord = value.attributeRecord;

        console.log("attributeRecord:", attributeRecord);
        if (attributeRecord != 0 && attributeRecord != null) {
            $.ajax({
                url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getAttributeNameDataAction",
                data: JSON.stringify({ "attributeRecord": attributeRecord }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (dataAttr) {

                    if (dataAttr.d != "") {
                        variantValue += " - " + dataAttr.d;
                    }

                    setDataToPrint(value);
                }

            });
        }
        else {
            setDataToPrint(value);
        }


        if (columnAccess[0]["dbranding"] == "1") {
            $('#lblDbranding').html("Developed by <b>MetaKave Bangladesh</b> | <b>Contact:</b> 01723393456 / 01738169709 | <b>Web:</b> www.metaposbd.com");
        }

    });

    window.print();

}



function getEmiPlanMonth(month) {
    if (month == "3month") {
        return " 3 Months";
    } else if (month == "6month") {
        return "6 Months";
    } else if (month == "9month") {
        return "9 Months";
    } else if (month == "12month") {
        return "12 Months";
    } else if (month == "18month") {
        return "18 Months";
    }

}


function setDataToPrint(value) {

    var productDesc = "";
    var tar = "";
    var unitName = "";
    var qty = "0";
    var returnQty = "0";
    var salePrice = "0";
    var salePriceOriginal = "0";
    var tdUnit = "";

    if (value.searchType == "product") {
        if (value.unitId == "0")
            unitName = "Pcs";
        else
            unitName = value.unitName;


        if (columnAccess[0]["isCategoryProduct"] == "0")
            productDesc = "<b>" + value.prodName + variantValue + "</b>";
        else if (columnAccess[0]["isCategoryProduct"] == "1")
            productDesc = "<b>" + value.CategoryName + variantValue + "</b>";
        else if (columnAccess[0]["isCategoryProduct"] == "2")
            productDesc += "<p>" + value.CategoryName + " " + value.prodName + variantValue + "</p>";
    }
    else if (value.searchType == "salePackage") {
        unitName = "Pcs";
        productDesc += value.packName;

    }
    else if (value.searchType == "service") {
        productDesc += value.serviceName;
        unitName = "Pcs";
    }


    if (columnAccess[0]["isPrintCategory"] == "1")
        productDesc += "<p> Category: " + value.CategoryName + "</p>";

    if (columnAccess[0]["isPrintSupplier"] == "1")
        productDesc += "<p>Supplier: " + value.supCompany + "</p>";

    if (columnAccess[0]["isPrintImei"] == "1") {
        if (value.imei != "") {
            value.imei = value.imei.substring(0, value.imei.length - 1);
            productDesc += "<p>IMEI: " + value.imei + "</p>";
        }
    }



    if (columnAccess[0]["isPrintWarranty"] == "1") {

        if (value.warranty != "0" && value.warranty != null) {

            var splitWarranty = value.warranty.split('-');

            if (splitWarranty[0] != "" && splitWarranty[1] != "" && splitWarranty[1] != "")
                productDesc += "<p>Warranty: " + splitWarranty[0] + "y " + splitWarranty[1] + "m " + splitWarranty[2] + "d</p>";
        }
    }

    if (columnAccess[0]["isDisplaySerialNo"] == "1" && value.serialNo != "")
        productDesc += "<p>Serial No: " + value.serialNo + "</p>";


    if (type != "0" && columnAccess[0]["businessType"].trim() != "diagnostic") {
        tdUnit = "<td>" + unitName + "</td>";
        $('.unit-txtbox').removeClass('disNone');
    }

    qty = value.qty;
    returnQty = value.returnQty;
    salePriceOriginal = value.sPrice;
    salePrice = ins1000Sep(formatNum(parseFloat(value.sPrice).toFixed(2)));


    
    tar = "<tr>" +
        "<td>" + couterSL + "</td>" +
        "<td>" + productDesc + " </td>" + tdUnit +
        "<td>" + qty + "</td>";

    if (totalReturnQty > 0) {
        $('#thReturn').removeClass('disNone');
        tar += "<td>" + returnQty + "</td>";
    }

   
    //tar += "<td id='saleAmount'>" + salePrice + "</td>" +
    //    "<td id='totolAmount'>" + ins1000Sep(formatNum(((parseFloat(qty) * parseFloat(salePriceOriginal)) - (parseFloat(returnQty) * parseFloat(salePriceOriginal))).toFixed(2))) + "</td>" +
    //    "</tr>";
    couterSL++;
    $('#cartItemList tr:last').after(tar);

    if (columnAccess[0]["tokenRcpt"] == "1" && columnAccess[0]["receipt"] == "0" && rcpt == "false") {
        $('#thUnitCost').addClass('disNone');
        $('#totalCost').addClass('disNone');
        //$('#totolAmount').addClass('disNone');
        //$('#saleAmount').addClass('disNone');
        $('#invoiceAmount').addClass('disNone');
    }


    
}


function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};



$.formattedDate = function (dateTime, dateFormat) {
    return moment(dateTime).format(dateFormat);
};


var columnAccess;
// Check page permission avaialable or not
function getSettingAccessValue() {
    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getSettingAccessValueAction",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            columnAccess = JSON.parse(data.d);

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}


// Numaric to word conversation
var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
var b = ['', '', 'Twenty', 'Thirty', 'forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

function inWords(num) {
    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + ' ' : '';
    return str;
}



////Cancel button click action
//$(function () {

//    var afterPrint = function () {
//        this.window.close();
//    };

//    if (window.matchMedia) {
//        var mediaQueryList = window.matchMedia('print');

//        mediaQueryList.addListener(function (mql) {
//            //alert($(mediaQueryList).html());
//            if (!mql.matches) {
//                afterPrint();
//            }
//        });
//    }
//    window.onafterprint = afterPrint;
//});



function ins1000Sep(val) {

    val = val.split(".");
    val[0] = val[0].split("").reverse().join("");
    val[0] = val[0].replace(/(\d{3})/g, "$1,");
    val[0] = val[0].split("").reverse().join("");
    val[0] = val[0].indexOf(",") == 0 ? val[0].substring(1) : val[0];
    return val.join(".");
}
function rem1000Sep(val) {
    return val.replace(/,/g, "");
}
function formatNum(val) {
    val = Math.round(val * 100) / 100;
    val = ("" + val).indexOf(".") > -1 ? val + "00" : val + ".00";
    var dec = val.indexOf(".");
    return dec == val.length - 3 || dec == 0 ? val : val.substring(0, dec + 3);
}
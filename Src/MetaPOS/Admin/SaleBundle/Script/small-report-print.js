
var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";


$(function () {
    getSettingAccessValue();
});




//
var billNo = getUrlParameter('billNo');

$.ajax({
    url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getInvoicePrintDataAction",
    data: JSON.stringify({ "billNo": billNo }),
    dataType: "json",
    type: "POST",
    contentType: "application/json; charset=utf-8",
    success: function (data) {

        var jsonData = $.parseJSON(data.d);

        // Header
        var payType = jsonData[0].payMethod;
        var payTypeName = "";
        if (payType == 0)
            payTypeName = "Cash";
        else if (payType == 1)
            payTypeName = "Card";
        else if (payType == 2)
            payTypeName = "Cheque";
        else if (payType == 3)
            payTypeName = "bKash";
        else if (payType == 4)
            payTypeName = "Deposit";
        else if (payType == 5)
            payTypeName = "Cash on delivery";
        else if (payType == 6)
            payTypeName = "Credit";

        $('#lblPayType').text("Pay Type: "+payTypeName);

        $('#lblBillNo').text("Invoice: " + billNo);

        if (columnAccess[0]["displayBrandNamePrint"] == "1") {
            $('#lblShopTitle').removeClass('disNone');
            $('#lblShopTitle').text(jsonData[0].branchName == null ? "" : jsonData[0].branchName);
        }

        $('#lblShopAddress').html(jsonData[0].branchAddress == null ? "" : jsonData[0].branchAddress);
        if (jsonData[0].branchPhone != null || "")
            $('#lblShopPhone').text(jsonData[0].branchPhone == null ? "" : "Phone: " + jsonData[0].branchPhone);

        if (jsonData[0].branchMobile != "" || null)
            $('#lblShopMobile').text(jsonData[0].branchMobile == null ? "" : " Mobile: " + jsonData[0].branchMobile);

        $('#lblCusId').text("#"+jsonData[0].cusID);
        $('#lblCusName').text(jsonData[0].name);
        $('#lblAddress').text(jsonData[0].address);
        $('#lblEmail').text(jsonData[0].mailInfo);
        $('#lblDate').text("Date: "+moment(Date(jsonData[0].entryDate.substr(6))).format('DD MMM YYYY'));
        //$('#lblDate').text($.formattedDate(new Date(parseInt(jsonData[0].entryDate.substr(6))), 'DD MMMM YYYY'));
        $('#lblTime').text("Time: " + moment(Date(jsonData[0].entryDate.substr(6))).format('h:mm:ss a'));



        
        if (jsonData[0].isAutoSalesPerson == false)
            $('#lblSaleBy').text(jsonData[0].staffName == null ? "" : "Sold By: " + jsonData[0].staffName);
        else
            $('#lblSaleBy').text(jsonData[0].title == null ? "" : "Sold By: " + jsonData[0].title);

        $('#lblPhone').text(jsonData[0].phone);
        //logo

        if (columnAccess[0]["displayLogoPrint"] == "1") {
            $('#brandLogo').removeClass('disNone');
            $('.brand-logo img').attr("src", baseUrl + "Img/Logo/" + jsonData[0].branchLogoPath);
        }


        // account 
        var misc = parseFloat(jsonData[0].loadingCost) + parseFloat(jsonData[0].shippingCost) + parseFloat(jsonData[0].carryingCost) + parseFloat(jsonData[0].unloadingCost) + parseFloat(jsonData[0].serviceCharge);

        var previousDue = 0;
        var totalDue = 0;
        if (jsonData[0].status == "Sold") {

            previousDue = (parseFloat(jsonData[0].cusCurrentDue) + parseFloat(jsonData[0].payCash)) - parseFloat(jsonData[0].grossAmt);

            totalDue = parseFloat(jsonData[0].grossAmt) + parseFloat(previousDue) + parseFloat(price);
        }
        else {
            previousDue = (parseFloat(jsonData[0].cusCurrentDue) + parseFloat(jsonData[0].payCash));

            totalDue = parseFloat(previousDue);
        }

        previousDue += parseFloat(jsonData[0].extraDiscount);

        if (parseFloat(jsonData[0].extraDiscount) > 0) {
            $('#trMoreDisc').removeClass('disNone');
            $('#lblMoreDisc').text(parseFloat(jsonData[0].extraDiscount).toFixed(2));
        }


        if (parseFloat(totalDue) < 0) {
            $('#lblTotalAmountLabel').text("Total Advance");
        }
        totalDue = Math.abs(totalDue);
        $('#lblTotalDue').text(ins1000Sep(parseFloat(totalDue).toFixed(2)));

        var currentDue = parseFloat(jsonData[0].cusCurrentDue);


        // vat
        if (columnAccess[0]["isVatSale"] == "1") {
            $('#trVatAmt').removeClass("disNone");

            var vat = jsonData[0].vatAmt + "(" + jsonData[0].vatType + ")";
            $('#lblVat').text(vat);
        }

        var discAmtWithoutMoreDisc = parseFloat(jsonData[0].discAmt) - parseFloat(jsonData[0].extraDiscount);
        $('#lblTotalAmt').text(ins1000Sep(parseFloat(jsonData[0].netAmt).toFixed(2)));
        $('#lblMiscCost').text(ins1000Sep(misc.toFixed(2)));

        if (columnAccess[0]["displayDiscountPrint"] == "1") {
            $('#trDiscount').removeClass('disNone');
            $('#lblDiscount').text(ins1000Sep(discAmtWithoutMoreDisc.toFixed(2)));
        }

        var grandTotal = parseFloat(jsonData[0].grossAmt) + parseFloat(jsonData[0].extraDiscount);
        $('#lblGrandTotal').text(ins1000Sep(grandTotal.toFixed(2)));
        $('#lblPreviousDue').text(ins1000Sep((previousDue.toFixed(2))));


        $('#lblCurrentPay').text(ins1000Sep(jsonData[0].payCash.toFixed(2)));

        if (currentDue < 0) {
            $('#lblBalanceLabel').text("Advance");
        }

        if (currentDue == 0) {
            //var payCash = jsonData[0].payCash.toFixed(2);
            //var balance = jsonData[0].balance.toFixed(2);
            var change = jsonData[0].return_;
            $('#lblBalanceLabel').text("Change");
            $('#lblCurrentDue').text(ins1000Sep(formatNum(Math.abs(parseFloat(change).toFixed(2)))));

        }
        else {
            $('#lblCurrentDue').text(ins1000Sep(formatNum(Math.abs(parseFloat(currentDue).toFixed(2)))));
        }


        // In word
        var currentPay = jsonData[0].balance == null || "" ? "0" : jsonData[0].balance;
        if (parseFloat(currentPay) > 0)
            $('#lblInWord').text("In Word: " + inWords(currentPay) + " Taka Only");


        //footer
        $('#lblFooterNote').html(jsonData[0].invoiceFooterNote == null ? "" : jsonData[0].invoiceFooterNote);


        // Cart section
        var cartLen = jsonData.length;
        var couterSL = 1;

        // get total return qty
        var totalReturnQty = 0;
        $.each(jsonData, function (index, value) {
            totalReturnQty = parseFloat(value.returnQty);
        });


        var unitName = "";
        var productDesc = "";
        var returnQty = "0";
        var salePrice = "0";
        var qty = "0";
        var tar = "";

        $.each(jsonData, function (index, value) {

            // get Unit
            var prodId = value.prodID;
            $.ajax({
                url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getProductUnitRatioAction",
                data: JSON.stringify({ "prodId": prodId }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (dataUnit) {

                    var jsonUnitData = $.parseJSON(dataUnit.d);
                    var attributeRecord = value.attributeRecord;

                    $.ajax({
                        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getAttributeNameDataAction",
                        data: JSON.stringify({ "attributeRecord": attributeRecord }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (dataAttr) {

                            if (value.searchType == "product") {
                                unitName = "Pcs";

                                var variantValue = "";
                                if (dataAttr.d != "") {
                                    variantValue += " - " + dataAttr.d;
                                }

                                if (columnAccess[0]["isCategoryProduct"] == "0")
                                    productDesc = "<b>" + value.prodName.trim() + variantValue + "</b>";
                                else if (columnAccess[0]["isCategoryProduct"] == "1")
                                    productDesc = "<b>" + value.CategoryName.trim() + variantValue + "</b>";
                                else if (columnAccess[0]["isCategoryProduct"] == "2")
                                    productDesc += "<p>" + value.CategoryName.trim() + " " + value.prodName.trim() + variantValue + "</p>";
                            }
                            else if (value.searchType == "salePackage") {
                                //unitName = jsonUnitData[0].unitName;
                                unitName = "Pcs";
                                productDesc += value.packName;

                            }
                            else if (value.searchType == "service") {
                                productDesc += value.serviceName;
                                unitName = "Pcs";
                            }

                            returnQty = value.returnQty == "" ? "0" : value.returnQty;
                            if (parseFloat(returnQty) > 0) {
                                productDesc += "<p> Return: " + value.returnQty + "</p>";
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

                            if (columnAccess[0]["isPrintWarrantywarranty"] == "1") {

                                if (value.warranty != "0" && value.warranty != null) {

                                    var splitWarranty = value.warranty.split('-');

                                    productDesc += "<p>Warranty: " + splitWarranty[0] + "y " + splitWarranty[1] + "m " + splitWarranty[2] + "d</p>";
                                }
                            }

                            qty = value.qty;
                            salePrice = value.sPrice;

                            tar = "<tr>" +
                                "<td>" + couterSL + "</td>" +
                                "<td>" + productDesc + " </td>" +
                                "<td class='disNone'>" + unitName + "</td>" +
                                "<td>" + (parseFloat(qty) - parseFloat(returnQty)) + "</td>";
                            tar += "<td>" + ins1000Sep(formatNum(parseFloat(salePrice).toFixed(2))) + "</td>" +
                                "<td>" + ins1000Sep(formatNum(((parseFloat(qty) * parseFloat(salePrice)) - (parseFloat(returnQty) * parseFloat(salePrice))).toFixed(2))) + "</td>" +
                                "</tr>";

                            $('#cartItemList tr:last').after(tar);

                            couterSL++;

                        }

                    }).done(function () {

                        document.querySelector('.demo').innerHTML = ' ';

                        if (couterSL == cartLen + 1)
                            window.print();

                        /* barcode */
                        var invoice_num = document.getElementById("lblBillNo").innerHTML;
                        JsBarcode("#barcode", invoice_num, {
                            width: 1.2,
                            height: 40,
                            displayValue: false
                        });


                    });

                    if (columnAccess[0]["dbranding"] == "1") {
                        $('#lblDbranding').html("Developed by <b>MetaKave</b> | <b>Contact:</b> 01723393456 / 01738169709 | <b>Web:</b> www.metaposbd.com");
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



        });

    }
});


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

console.log("start");
var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";
var type = getUrlParameter('type');

var isPrintReady = false;
var db;
var sale, customer, config, product, storeCustomersListPrint, storeProductsListPrint;


var dbReq = indexedDB.open('metaPOSDatabase', 1);

dbReq.onsuccess = function (event) {
    console.log("Database initilized ");
    db = event.target.result;

    $('.metapos-loading').removeClass('disNone');

    configurationInit();

    productInit();

    checkAndInitailizeDataPrint(db);

};



$(function () {

    if (type == "0") {
        $('#invoiceBody').css({ width: "300px" });
        $('.invoice-text').css({ width: "30%" });
        $('.billing-detail td').css({ padding: "0" });
        $('.pay-type').text('Type');

        $('.details').text('Details');
        $('.unit-cost').text('Cost');
        $('.signature-area').addClass('disNone');
        $('#brandLogo').addClass('disNone');
    }

});


var txProduct, storeProducts, txCustomer, storeCustomers, txSlip, storeSlips, txConfig, storeConfigs;

function checkAndInitailizeDataPrint(db) {

    try {

        var lastInvoice;

        txSlip = db.transaction(['metaposSales'], 'readwrite');
        storeSlips = txSlip.objectStore('metaposSales');
        var req = storeSlips.openCursor();

        req.onsuccess = function (event) {
            var cursor = event.target.result;
            if (cursor != null) {
                console.log("last invoice cursor.value:", cursor.value.timestamp);
                lastInvoice = cursor.value.timestamp;
                cursor.continue();
            }
            else {

                initializeData(lastInvoice, db);
            }
        };
        req.onerror = function (event) {
            alert('error in cursor request ' + event.target.errorCode);
        };


    }
    catch (e) {

        console.log("warning", "Database is not initilization, please login your account.");

        indexedDB.deleteDatabase('metaPOSDatabase');

        setTimeout(function () { window.location.href = "/login"; }, 5000);
    }
}


function initializeData(timestamp, db) {

    txSlip = db.transaction(['metaposSales'], 'readwrite');
    storeSlips = txSlip.objectStore('metaposSales');
    var reqSlips = storeSlips.openCursor();
    reqSlips.onsuccess = function (eventSlip) {

        var cursorSlip = eventSlip.target.result;

        if (cursorSlip != null) {
            if (cursorSlip.value.timestamp == timestamp) {
                sale = cursorSlip.value.text;

                invoiceCustomerDataToPrint();
            }

            cursorSlip.continue();
        }
    };
}




function invoiceCustomerDataToPrint() {
    txCustomer = db.transaction(['metaposCustomers'], 'readwrite');
    storeCustomers = txCustomer.objectStore('metaposCustomers');
    var reqCustomer = storeCustomers.openCursor();
    var allCustomers = [];
    reqCustomer.onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor != null) {
            allCustomers.push(cursor.value.text);
            cursor.continue();
        }
        else {
            storeCustomersListPrint = allCustomers;

            var cusId = sale[0].cusId;
            console.log("cusId:", cusId);
            customer = _.find(storeCustomersListPrint, {
                'cusID': cusId.toString()
            });

            if (customer == undefined) {
                customer = _.find(storeCustomersListPrint, {
                    'id': cusId.toString()
                });
            }

            if (customer == undefined) {
                customer = _.find(storeCustomersListPrint, {
                    'id': parseInt(cusId.toString())
                });
            }

            setInvoiceDataToPrint();

        }
    };
}



function configurationInit() {

    txConfig = db.transaction(['metaposConfigs'], 'readwrite');
    storeConfigs = txConfig.objectStore('metaposConfigs');

    var reqConfig = storeConfigs.openCursor();
    var allConfigs = [];
    reqConfig.onsuccess = function (evnt) {
        var cursorConfig = evnt.target.result;
        if (cursorConfig != null) {

            allConfigs.push(cursorConfig.value.text);
            cursorConfig.continue();
        }
        else {
            config = allConfigs;

        }
    };

}


var productItemCounter = 0;
function productInit() {
    var allProducts = [];
    txProduct = db.transaction(['metaposProducts'], 'readwrite');
    storeProducts = txProduct.objectStore('metaposProducts');
    var reqProduct = storeProducts.openCursor();

    reqProduct.onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor != null) {
            allProducts.push(cursor.value.text);
            cursor.continue();
        }
        else {
            console.log("productItemCounter:", productItemCounter);
            if (productItemCounter == 0) {
                productItemCounter++;

                storeProductsListPrint = allProducts;

                setProductItemToPrint();
            }
        }
    };

}




function setProductItemToPrint() {
    var i = 0, couterSL = 1;
    var unitName, returnQty, productDesc, qty, salePrice, tar;

    $.each(sale, function (index, value) {

        // get Unit
        var prodId = value.prodID;
        product = _.find(storeProductsListPrint, {
            'prodId': parseInt(prodId)
        });


        if (value.searchType == "product") {
            unitName = "Pcs";
            var variantValue = "";
            productDesc = "<b>" + product.name + variantValue + "</b>";
        }


        returnQty = value.returnQty == "" ? "0" : value.returnQty;
        if (parseFloat(returnQty) > 0) {
            productDesc += "<p> Return: " + value.returnQty + "</p>";
        }

        //productDesc += "<p> Category: " + product.catName + "</p>";


        //productDesc += "<p>Supplier: " + product.supName + "</p>";

        //if (value.imei != "") {
        //    value.imei = value.imei.substring(0, value.imei.length - 1);
        //    productDesc += "<p>IMEI: " + value.imei + "</p>";
        //}


        //if (value.warranty != "0" && value.warranty != null) {

        //    var splitWarranty = value.warranty.split('-');

        //    productDesc += "<p>Warranty: " + splitWarranty[0] + "y " + splitWarranty[1] + "m " + splitWarranty[2] + "d</p>";
        //}


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
        i++;


        $('#lblDbranding').html("Developed by <b>MetaKave</b> | <b>Contact:</b> 01723393456 / 01738169709 | <b>Web:</b> www.metaposbd.com");


    });

    $('.metapos-loading').addClass('disNone');

    if (isPrintReady)
        window.print();
    else
        isPrintReady = true;
}



function setInvoiceDataToPrint() {
    var payTypeName = "Cash";
    productInit();

    $('#lblPayType').text(payTypeName);

    $('#lblBillNo').text("...");

    console.log("config:", config);
    if (config == undefined)
        configurationInit();

    console.log("B:", config[0].branchName);
    $('#lblShopTitle').html(config[0].branchName);

    $('#lblShopAddress').html(config[0].branchAddress);

    $('#lblShopPhone').text(config[0].branchPhone);


    $('#lblShopMobile').text(config[0].branchMobile);
    console.log("customer:", customer);
    if (customer)
        $('#lblCusId').text("#" + customer.cusID);
    $('#lblCusName').text(customer.name);
    $('#lblAddress').text(customer.address);
    $('#lblEmail').text(customer.email);

    var dateFormat;
    if (type == "0")
        dateFormat = "DD-MM-YY";
    else
        dateFormat = "DD-MMM-YYYY";

    $('#lblDate').text(moment(Date(sale[0].entryDate)).format(dateFormat));
    //$('#lblDate').text($.formattedDate(new Date(parseInt(jsonData[0].entryDate.substr(6))), 'DD MMMM YYYY'));
    $('#lblTime').text(moment(Date(sale[0].entryDate)).format('h:mm:ss'));

    $('#lblSaleBy').text("...");


    $('#lblPhone').text(customer.phone);
    //logo

    //if (columnAccess[0]["displayLogoPrint"] == "1") {
    //    $('#brandLogo').removeClass('disNone');
    //    $('.brand-logo img').attr("src", baseUrl + "Img/Logo/" + jsonData[0].branchLogoPath);
    //}


    $('#trMoreDisc').removeClass('disNone');
    $('#lblMoreDisc').text(parseFloat("0.00"));

    $('#lblTotalDue').text(ins1000Sep(parseFloat(sale[0].netAmt).toFixed(2)));

    // vat
    //$('#lblVat').text(vat);

    var discAmtWithoutMoreDisc = parseFloat(sale[0].discAmt) - parseFloat(sale[0].extraDiscount);
    $('#lblTotalAmt').text(ins1000Sep(parseFloat(sale[0].netAmt).toFixed(2)));
    $('#lblMiscCost').text(ins1000Sep("0.00"));


    $('#trDiscount').removeClass('disNone');
    $('#lblDiscount').text(ins1000Sep(discAmtWithoutMoreDisc.toFixed(2)));


    var grandTotal = parseFloat(sale[0].grossAmt) + parseFloat(sale[0].extraDiscount);
    $('#lblGrandTotal').text(ins1000Sep(grandTotal.toFixed(2)));
    $('#lblPreviousDue').text(ins1000Sep((sale[0].preDue.toFixed(2))));


    $('#lblCurrentPay').text(ins1000Sep(sale[0].payCash));
    var currentDue = parseFloat(sale[0].balance);
    if (currentDue == 0) {
        var change = sale[0].return_;
        $('#lblBalanceLabel').text("Change");
        $('#lblCurrentDue').text(ins1000Sep(formatNum(Math.abs(parseFloat(change).toFixed(2)))));

    }
    else {
        $('#lblCurrentDue').text(ins1000Sep(formatNum(Math.abs(parseFloat(currentDue).toFixed(2)))));
    }


    // In word
    var currentPay = sale[0].payCash == null || "" ? "0" : sale[0].payCash;
    currentPay = parseFloat(currentPay).toFixed(2);
    console.log("currentPay:", currentPay);
    if (parseFloat(currentPay) > 0)
        $('#lblInWord').text("In Word: " + inWords(currentPay) + " Taka Only");


    //footer
    $('#lblFooterNote').html(config[0].invoiceFooterNote);


    // Cart section

    console.log("sale:", sale);

    // get total return qty
    var totalReturnQty = 0;
    $.each(sale, function (index, value) {
        totalReturnQty = parseFloat(value.returnQty);
    });



    if (isPrintReady)
        window.print();
    else
        isPrintReady = true;
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
    if (!n) return;
    var str = '';
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



// Get customer list
function getCustomersPrint(storeCustomers) {

    var req = storeCustomers.openCursor();
    var allCustomers = [];

    req.onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor != null) {
            allCustomers.push(cursor.value.text);
            cursor.continue();
        }
        else {


            storeCustomersListPrint = allCustomers;
        }
    };
    req.onerror = function (event) {
        alert('error in cursor request ' + event.target.errorCode);
    };

};



//window.onafterprint = function () {
//    console.log("Printing completed...");
//    window.close();
//}
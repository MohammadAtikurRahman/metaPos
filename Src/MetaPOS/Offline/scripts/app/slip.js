var app = window.app || {};

var storeSlipSales;

// Base url
var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";


var db;
var dbReq = indexedDB.open('metaPOSDatabase', 1);

dbReq.onsuccess = function (event) {
    db = event.target.result;
    getAndDisplaySalesSlip(db);
};


$(function () {
    'use strict';


    // load slip

    // Load slip data
    app.submitToSale = function (index) {
        console.log("index:", index);
        $(this).attr("disabled", 'disabled');





        // start loading
        $('.metapos-loading').removeClass('disNone');

        var xhr = new XMLHttpRequest();
        var file = "https://cors-anywhere.herokuapp.com/http://web.metaposbd.com/Img/logo.png";
        var randomNum = Math.round(Math.random() * 10000);

        xhr.open('HEAD', file + "?rand=" + randomNum, true);
        xhr.send();

        xhr.addEventListener("readystatechange", processRequest, false);


        function processRequest(e) {
            if (xhr.readyState == 4) {
                if (xhr.status >= 200 && xhr.status < 304) {

                    syncDataTo(index);

                }
                else {
                    $('.metapos-loading').addClass('disNone');
                    $('.msg-internet-connection').modal('show');

                    if (this.hasAttribute("disabled")) {
                        this.prop("disabled", false);;
                    }
                }
            }
        }


    };





});


function syncDataToOnline(jsonStrData, index) {


    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/syncSaleDataListAction",
        //url: "http://web.metaposbd.com/Admin/SaleBundle/View/Invoice.aspx/syncSaleDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonStrData) + "' }",
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        type: 'POST',
        success: function (data) {
            var message = data.d.split('|')[0].trim();
            if (message == "True" || message == "true") {
                $('.metapos-loading').addClass('disNone');
                showNotification("success", "Sync data successfully.");

                // delete slip
                deleteResult(index);


            }
            else {
                $('.metapos-loading').addClass('disNone');
                console.log(data.d);
                showNotification("warning", data.d.split('|')[1].trim());

            }


        },
        error: function (xhr, ajaxOptions, thrownError) {
            if (xhr.status == 500) {
                $('.metapos-loading').addClass('disNone');

                showNotification("warning", "Please login the application");
            }
            console.log("error", xhr.status);
            console.log(thrownError);
            console.log(xhr.responseText);
        }
    });
}


function loadSlipData(metaposSales) {
    console.log("metaposSales:", metaposSales);
    var i = 0;
    var tableRow = "";
    var salesData = metaposSales;
    if (salesData == null) {
        tableRow += "<tr><td colspan='10' class='text-center'>" + "No Sales Data found" + "</td></tr>";
        $('#offlineSlip').append(tableRow);
        return;
    }

    var length = metaposSales.length;
    for (i = length - 1; i <= length; i--) {


        if (i < 0)
            break;;

        tableRow += "<tr>" +
            "<td>" + (i + 1) + "</td>" +
            "<td>" + salesData[i].text[0].cusName + "</td>" +
            "<td>" + salesData[i].text[0].billNo + "</td>" +
            "<td>" + salesData[i].text[0].grossAmt + "</td>" +
            "<td>" + (parseFloat(salesData[i].text[0].shippingCost) + parseFloat(salesData[i].text[0].serviceCharge) + parseFloat(salesData[i].text[0].carryingCost)) + "</td>" +
            "<td>" + salesData[i].text[0].balance + "</td>" +
            "<td>" + salesData[i].text[0].salesPersonName + "</td>" +
            "<td>" + moment(salesData[i].text[0].entryDate).format('DD-MMM-YYYY') + "</td>" +
            "<td>" + salesData[i].text[0].status + "</td>" +
            "<td>" + salesData[i].text[0].store + "</td>" +
            "<td><button class='btn btn-primary btn-outline-accent' onclick='app.submitToSale(" + salesData[i].timestamp + ")'><i class=\"material-icons\">sync</i> Sync </button></td>" +
            "</tr>";
    }

    $('#offlineSlip').html(" ");
    $('#offlineSlip').append(tableRow);

    var rowCount = $('#tblSlip tr').length - 1;
    $('.lblTotalCartQty').attr("totalqty", rowCount);
    $(".lblTotalCartQty").html(rowCount);
};



function getAndDisplaySalesSlip(db) {
    var tx = db.transaction(['metaposSales'], 'readonly');
    var store = tx.objectStore('metaposSales');

    var  req = store.openCursor();
    var allSales = [];

    req.onsuccess = function (event) {
        let
        cursor = event.target.result;
        console.log("cursor:", cursor);
        if (cursor != null) {
            allSales.push(cursor.value);
            cursor.continue();
        }
        else {
            storeSlipSales = allSales;
            loadSlipData(allSales);
        }
    };
    req.onerror = function (event) {
        alert('error in cursor request ' + event.target.errorCode);
    };
}


function deleteResult(index) {

    var tx = db.transaction(['metaposSales'], 'readwrite');
    var store = tx.objectStore('metaposSales');

    var req = store.openCursor();


    req.onsuccess = function (event) {
        var cursor = event.target.result;

        if (cursor != null) {

            if (cursor.value.timestamp == index) {
                var request = cursor.delete();
                request.onsuccess = function () {
                    console.log('Deleted that slip.');
                };
            }

            cursor.continue();
        }
        else {
            getAndDisplaySalesSlip(db);
        }

    };

};



function syncDataTo(index) {

    var tx = db.transaction(['metaposSales'], 'readwrite');
    var store = tx.objectStore('metaposSales');

    var req = store.openCursor();


    req.onsuccess = function (event) {
        var cursor = event.target.result;
        console.log("cursor:", cursor);

        if (cursor != null) {
            console.log("timestamp:", cursor.value.timestamp, "//", index);
            if (cursor.value.timestamp == index) {

                syncDataToOnline(cursor.value.text, index);
            }

            cursor.continue();
        }

    };

};


function doesConnectionExist() {
    var xhr = new XMLHttpRequest();
    var file = "https://cors-anywhere.herokuapp.com/http://web.metaposbd.com/Img/logo.png";
    var randomNum = Math.round(Math.random() * 10000);

    xhr.open('HEAD', file + "?rand=" + randomNum, false);
    xhr.send();

    xhr.addEventListener("readystatechange", processRequest, false);

    var isExit = false;
    function processRequest(e) {
        if (xhr.readyState == 4) {
            if (xhr.status >= 200 && xhr.status < 304) {
                isExit = true;
                //alert("connection exists!");
            } else {
                isExit = false;
                //alert("connection doesn't exist!");
            }
        }
    }
    console.log("isExit:", isExit);
}
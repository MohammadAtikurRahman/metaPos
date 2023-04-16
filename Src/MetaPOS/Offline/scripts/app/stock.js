var app = window.app || {};

let db;
let dbReq = indexedDB.open('metaPOSDatabase', 1);

dbReq.onsuccess = function (event) {
    db = event.target.result;
    getAndDisplay(db);
}



function getAndDisplay(db) {
    let tx = db.transaction(['metaposProducts'], 'readonly');
    let store = tx.objectStore('metaposProducts');

    let req = store.openCursor();
    let allProducts = [];

    req.onsuccess = function (event) {
        let
        cursor = event.target.result;
        if (cursor != null) {
            allProducts.push(cursor.value);
            cursor.continue();
        }
        else {
            displayStock(allProducts);
        }
    };
    req.onerror = function (event) {
        alert('error in cursor request ' + event.target.errorCode);
    };
}



function displayStock(Product) {
    
    var i = 0;
    var tableRow = "";

    if (Product == null) {
        tableRow += "<tr><td colspan='10' class='text-center'>" + "No Stock Data found" + "</td></tr>";
        $('#offlineSlip').append(tableRow);
        return;
    }

    var length = Product.length;
    for (i = 0; i < length; i++) {

        //console.log(Product[i]);

        tableRow += "<tr>" +
            "<td>" + Product[i].text.prodId + "</td>" +
            "<td>" + Product[i].text.name + "</td>" +
            "<td>" + Product[i].text.stockQty + "</td>" +
            "<td class=\"item_code\">" + Product[i].text.code + "</td>" +
            "<td>" + parseFloat(Product[i].text.buyPrice).toFixed(2) + "</td>" +
            "<td>" + parseFloat(Product[i].text.salePrice).toFixed(2) + "</td>" +
            "<td>" + parseFloat(Product[i].text.dealerPrice).toFixed(2) + "</td>" +
            "<td>" + Product[i].text.storeName + "</td>" +
            // "<td><button class='btn btn-primary btn-outline-accent' onclick='app.submitToSale(" + i + ")'><i class=\"material-icons\">sync</i> Action </button></td>" +
            "<td> " +
            '<div class="dropdown">' +
            ' <button class="btn btn-primary btn-outline-accent dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" onclick="app.submitToSale(" + i + ")">Action</button>' +
            ' <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
            ' <a class="dropdown-item btnViewCusotmer" href="javascript:void(0)">View</a>' +
            '</div>' +
            '</div>' +
            "</td>" +
            "</tr>";
    }


    $('#offlineSlip').append(tableRow);

    var rowCount = $('#tblSlip tr').length;
    $('.lblTotalCartQty').attr("totalqty", rowCount);
    $(".lblTotalCartQty").html(rowCount);

}



(function ($) {
    'use strict';


    // Load slip data
    app.submitToSale = function(index) {

        // alert(index);

        var salesData = JSON.parse(localStorage.getItem("metaposSales"));

        var jsonStrData = salesData[index];

        console.log("jsonStrData:", jsonStrData);

        $.ajax({
            url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/syncSaleDataListAction",
            data: "{ 'jsonStrData' : '" + JSON.stringify(jsonStrData) + "' }",
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            type: 'POST',
            success: function(data) {
                console.log("data:", data.d);

            },
            failure: function(response) {
                console.log(response);
                alert(response);
            },
            error: function(response) {
                console.log(response);
                alert(response);
            }
        });
    };


    // int doc here
    $(document).ready(function () {



        // Create customer  <Open Modal>
        $(document).on("click", ".btnViewCusotmer", function () {


            var pCode = $(this).parents("tr").find('.item_code').text().trim();


            var products = JSON.parse(localStorage.getItem('metaposProduct')),
                product = _.find(products, {
                    'code': pCode
                });

            console.log(product);
            product.imei = product.imei.split(',');

            //var imei = product.imei_record.split(",");

            console.log(product.imei);

            // slip imei
            if (product.imei.length > 0) {

                var imei = product.imei;
                var attrList = "";

                for (var i = 0; i < imei.length; i++) {
                    attrList += '<li class="singAttr">' + imei[i] + '</li>';
                }
                $('#attrList').html(attrList);

            }

            // Warranty date formatter
            // if (product.warranty !== "0-0-0") {
            //     console.log("i am here");
            //     var waranty = product.warranty.split("-");
            //     console.log(waranty);

            //     var year = waranty[0];
            //     var month = waranty[1];
            //     var day = waranty[2];

            //     var isYear, isMonth, isDay = false;

            //     if (year !== "0") {
            //         console.log("yes! year");
            //         isYear = true;
            //         product.waranty = year + " Years ";

            //         console.log(product.waranty);
            //     }

            //     if (month !== "0") {
            //         console.log("yes! month");
            //         isMonth = true;
            //         if (isYear) {
            //             product.waranty += " & " + month + " Month";


            //             console.log(product.waranty);
            //         } else {
            //             product.waranty = month + " Month";


            //             console.log(product.waranty);
            //         }
            //     }


            //     if (day !== "0") {
            //         console.log("yes! day");
            //         if (isYear && isMonth) {
            //             product.waranty += " & " + day + " Days";
            //         } else {
            //             product.waranty = day + " Days";
            //         }
            //     }
            //     var abc = product.warranty;
            //     console.log(abc);
            //     $('#pWarranty').html(abc);

            // } else {
            //     product.warranty = "None";
            // }


            //console.log(product.warranty);



            $('.mdlProductName').text(product.name);
            $('#pCode').text(product.code);
            $('#pSupplier').text(product.supName);
            $('#pCategory').text(product.catName);
            $('#pManufacturer').text(product.manufacturerName);
            $('#pStockQty').text(product.qty);
            $('#pWarningQty').text(product.Warning);
            $('#pWholesalePrice').text(product.dealerPrice);
            $('#pWeight').text(product.weight);
            $('#pSize').text(product.size);
            // $('#pWarranty').text(product.warranty);
            //$('#pIMEI').text(product.imei_record);
            $('#pReceivedDate').text(product.recivedDate);
            $('#pExpiryDate').text(product.ExpiryDate);
            $('#pBatchNo').text(product.batchNo);
            $('#pShipmentStatus').text(product.shipmentStatus);
            $('#pUnit').text(product.unitName);
            $('#pWarehouse').text(product.warehouse);
            $('#pCommission').text(product.Commission);
            $('#pNotes').text(product.note);





            $('#mdlViewProduct').modal({
                keyboard: false
            })


        });

    })

})(jQuery)
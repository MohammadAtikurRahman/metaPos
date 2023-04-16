
var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";

var type = getUrlParameter('type');
console.log("type:" + type);

$(function () {
    getSettingAccessValue();


    var width = columnAccess[0]["smallPrintPaperWidth"];
    width = parseFloat(width) * 3.77;


    if (type == "0") {

        $('#challanBody').css({ width: width });
        $('.body-item').addClass('body-item');
        //$('.main-body').css({ width: "60%" });
        //$('.challan-text').css({ width: "60%" });
        //$('#small-signature').removeClass('signature-area hr').addClass('small-signature-area hr');

        $('#small-signature hr').css({ margin: "0px 0px", width: "145px" });

       
    }

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


        var arrLocation = [];
        var arrLocationName = [];

        var i = 0;
        $.each(jsonData, function (key, item) {

            var valExist = arrLocation.indexOf(item.locationId);

            if (valExist < 0) {
                arrLocation[i] = item.locationId;
                arrLocationName[i] = item.locationName;
                i++;
            }
        });


        var godownInvoice = '';
        for (var locCounter = 0; locCounter < arrLocation.length; locCounter++) {
            var locationId = arrLocation[locCounter];

            var locationName = "";
            if (columnAccess[0]["displayChallanLocation"] == "1") {
                locationName = arrLocationName[locCounter];

                if (locationName == null) {
                    locationName = "NONE";
                }

                locationName = "<h3> Location: " + locationName + "</h3>";
            }

            var addressName = "";
            if (columnAccess[0]["displayChallanLocation"] == "1") {
                addressName = "<h3> " + jsonData[0].branchAddress + "</h3>";
            }
            
            console.log("displayChallanLocation::", columnAccess[0]["displayChallanLocation"]);
            console.log("displayChallanAddress::", columnAccess[0]["displayChallanAddress"]);

            godownInvoice += '<div class="invoice-header">' +
                '<div class="brand-logo">'
                + '<img src="logo.png" alt="">'
                + '</div>'
                + '<div class="brand-info">'
                + '<div class="brand-name">'
                + '<h1>' + jsonData[0].branchName + '</h1>'
                + '</div>'
                + '<div class="address" style="display: none">' +
                +'<p class="MsoNormal lblShopAddress" style="text-align: center; display: none"></p>'
                + '<p  class="MsoNormal lblShopNumber" style="text-align: center;display: none"><span class="lblShopPhone"></span><span class="lblShopMobile"></span></p>'
                + '</div>'
                + '<div class="contact"></div>'
                + '</div>'
                + '<div class="invoice-title">'
                + locationName
                + addressName
                + '<p>Phone: ' + jsonData[0].branchPhone + '</h1>'
                + '</div>'
                + '<div><h3 class="challan-text">CHALLAN</h3></div>'
                + '</div>';
                



            // START HEADER DETAILS     style="border: 1px solid #000;width: 12%;margin: 0 auto;"
            godownInvoice += '<div class="invoice-content">' +
                '<div class="customer-detail">' +
                '<table class="content-table">' +
                '<tr style="display: none">' +
                '<td>Customer Id</td>' +
                '<td>:' + jsonData[0].cusID + '</td>' +
                '<td></td>' +
                '</tr>';

            var cusName = jsonData[0].name;
            if (cusName != "")
                godownInvoice += '<tr>' +
                    '<td>Name</td>' +
                    '<td>:</td>' +
                    '<td>' + cusName + '</td>' +
                    '<tr>';

            var cusAddress = jsonData[0].address;
            if (cusAddress != "")
                godownInvoice += '<tr>' +
                    '<td>Address</td>' +
                    '<td>:</td>' +
                    '<td>' + cusAddress + '</td>' +
                    '<tr>';

            var cusPhone = jsonData[0].phone;
            if (cusPhone != "")
                godownInvoice += '<tr>' +
                    '<td>Phone</td>' +
                    '<td>:</td>' +
                    '<td>' + cusPhone + '</td>' +
                    '<tr>';

            //godownInvoice += '<tr>' +
            //    '<td>Email</td>' +
            //    '<td>:</td>' +
            //    '<td>' + jsonData[0].mailInfo + '</td>' +
            //    '<tr>';

            godownInvoice += '<tr></tr>';

            godownInvoice += '<tr>' +
                '<td>Bill No</td>' +
                '<td>:</td>' +
                '<td>' + jsonData[0].billNo + '</td>' +
                '<tr>';

            godownInvoice += '<tr>' +
                '<td>Date</td>' +
                '<td>:</td>' +
                '<td>' + moment(Date(jsonData[0].entryDate.substr(6))).format('DD MMMM YYYY') + '</td>' +
                '<tr>';

            godownInvoice += '<tr>' +
                '<td>Time</td>' +
                '<td>:</td>' +
                '<td>' + moment(Date(jsonData[0].entryDate.substr(6))).format('h:mm:ss a') + '</td>' +
                '<tr>';
            var saleBy = "";
            if (jsonData[0].isAutoSalesPerson == false)
                saleBy = jsonData[0].staffName == null ? "" : jsonData[0].staffName;
            else
                saleBy += jsonData[0].title == null ? "" : jsonData[0].title;

            if (saleBy != "")
                godownInvoice += '<tr>' +
                    '<td>Sale By</td>' +
                    '<td>:</td>' +
                    '<td> ' + saleBy + '</td>' +
                    '<tr>';

            godownInvoice += '</table>';
            // END HEADER DETAILS 

            // START PRODUCT LOOP
            godownInvoice += '<table><tr>';

            godownInvoice += '<div class="product-details">' +
                '<table class="item" id="cartItemList">' +
                '<tr>' +
                '<th>SL</th>' +
                '<th>Product Details</th>' +
                '<th>Unit</th>' +
                '<th style="padding-left: 10px;">Qty.</th>' +
                '</tr>';

            var sl = 1, totalQty = 0;
            $.each(jsonData, function (key, item) {

                if (item.locationId == locationId) {

                    var productDesc = '', unitName = '';
                    if (item.searchType == "product") {
                        unitName = "Pcs";
                        if (columnAccess[0]["isCategoryProduct"] == "0")
                            productDesc = "<b>" + item.prodName + "</b>";
                        else if (columnAccess[0]["isCategoryProduct"] == "1")
                            productDesc = "<b>" + item.CategoryName + "</b>";
                        else if (columnAccess[0]["isCategoryProduct"] == "2")
                            productDesc += "<p>" + item.CategoryName + " " + item.prodName + "</p>";

                        if (columnAccess[0]["isDisplaySerialNo"] == "1" && item.serialNo != "")
                            productDesc += "<p>Serial No: " + item.serialNo + "</p>";
                    }
                    else if (item.searchType == "salePackage") {
                        //unitName = jsonUnitData[0].unitName;
                        unitName = "Pcs";
                        productDesc += item.packName;

                    } else if (item.searchType == "service") {
                        productDesc += item.serviceName;
                        unitName = "Pcs";
                    }

                    godownInvoice += "<tr>" +
                       "<td>" + sl + "</td>" +
                       "<td>" + productDesc + " </td>" +
                       "<td>" + unitName + "</td>" +
                       "<td>" + item.qty + "</td>" +
                       "</tr>";
                }

                totalQty += parseFloat(item.qty);


                sl++;

            });




            godownInvoice += '</table>';
            godownInvoice += "<p style='float: right;padding-top: 15px;'> Total Qty: " + totalQty + "</p>";

            godownInvoice += '</div>';



            godownInvoice += '</tr></table>';
            // END PRODUCT LOOP

            godownInvoice += '<div class="signature-area" id="small-signature">'
                + '<div class="customer-area">'
               // + '<hr>'
                + '<p style="border-top: 1px solid #333; width:50%"></p>'
                + '<p>Authorized</p>'
                + '</div>'
                + '<div class="authorized-area">'
                //+ '<hr>'
                + '<p style="border-top: 1px solid #333; width:50%"></p>'
                + '<p>Signature</p>'
                + '</div>'
                + '</div>';

            godownInvoice += '<div class="developer">' +
                                '<p style="text-align:center">Developed by MetaKave Bangladesh </p>' +
                              '</div>';


        }


        $('#godownContent').html(godownInvoice);


    },
    failure: function (response) {
        console.log(response);
        alert(response);
    },
    error: function (response) {
        console.log(response);
        alert(response);
    }
}).done(function () {
    window.print();
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
var a = ['', 'one ', 'two ', 'three ', 'four ', 'five ', 'six ', 'seven ', 'eight ', 'nine ', 'ten ', 'eleven ', 'twelve ', 'thirteen ', 'fourteen ', 'fifteen ', 'sixteen ', 'seventeen ', 'eighteen ', 'nineteen '];
var b = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];

function inWords(num) {
    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
    return str;
}



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
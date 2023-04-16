

function eventChange() {

    // default datetime
    var dateEntered = Date.now();
    var toDayDate = moment.utc(dateEntered).format("DD-MMM-YYYY");
    $('#txtDateFrom').val(toDayDate);
    $('#txtDateTo').val(toDayDate);
    //console.log("date:", moment.utc(dateEntered).format("DD-MMM-YYYY"));

}


$(function () {
    var searchType = $("#contentBody_rblSearchByType input:checked").val();
    if (searchType == undefined)
        searchType = "product";
    geInventoryDataList(searchType);

    getCategoryDataList(searchType);


});


$('#contentBody_rblSearchByType').change(function () {
    var searchType = $("#contentBody_rblSearchByType input:checked").val();
    geInventoryDataList(searchType);

    getCategoryDataList(searchType);
});


$('#ddlCategoryWiseReport, #txtDateFrom, #txtDateTo,#ddlStatusFilter,#contentBody_ddlStoreList,#contentBody_ddlUserList').change(function () {

    var searchType = $("#contentBody_rblSearchByType input:checked").val();
    console.log("searchType:", searchType);
    geInventoryDataList(searchType);

});


function getCategoryDataList(searchType) {

    if (searchType == "salePackage") {
        $('#ddlCategoryWiseReport').attr("disabled", true);
        $('#ddlCategoryWiseReport').val(0);
        return;
    }
    else {
        $('#ddlCategoryWiseReport').attr("disabled", false);
    }

    $.ajax({
        url: baseUrl + "Admin/ReportBundle/View/InventoryReport.aspx/getCategoryDataListAction",
        dataType: "json",
        type: "POST",
        data: "{searchType:'" + searchType + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('#ddlCategoryWiseReport').empty();

            $('#ddlCategoryWiseReport').empty().append('<option selected="selected" value="0">' + _SelectCategory + '</option>');

            $.each(data.d, function () {
                $('#ddlCategoryWiseReport').append($("<option></option>").val(this['Value']).html(this['Text']));
            });
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


function geInventoryDataList(searchType) {

    var category = $('#ddlCategoryWiseReport').val();
    var dateFrom = $('#txtDateFrom').val();
    var dateTo = $('#txtDateTo').val();
    var prodId = getUrlParameter("prodId");
    var status = $('#ddlStatusFilter').val();
    var storeId = $('#contentBody_ddlStoreList').val();
    var userId = $('#contentBody_ddlUserList').val();

    if (category == null)
        category = 0;
    if (dateTo == "")
        dateTo = moment().format('YYYY MM DD');

    if (dateFrom == "")
        dateFrom = moment().format('YYYY MM DD');

    var jsonData = {
        "searchType": searchType,
        "category": category,
        "dateFrom": dateFrom,
        "dateTo": dateTo,
        "prodId": prodId,
        "status": status,
        "storeId": storeId,
        "userId": userId
    };
    console.log("jsonData:", jsonData);


    $.ajax({
        "type": "Post",
        "url": baseUrl + "Admin/ReportBundle/View/InventoryReport.aspx/getInventoryReportDataListAction",
        "data": "{jsonData: '" + JSON.stringify(jsonData) + "'}",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var jsonGetData = JSON.parse(data.d);

            $('#dataListTable').DataTable().destroy();
            $('#dataListTable').empty();

            var isDeliveryStatus = false;
            var deliveryStatusWidth = "10%";
            var deliveryStatusWidthDisNone = "";

            var isLastQtyWidth = "10%";
            var isLastQtyDisNone = "";

            var isBalanceQtyWidth = "10%";
            var isBalanceDisNone = "";

            var isProductCodeWith = "10%";
            var isProductCodeDisNone = "";



            if (columnAccess[0]["displayDeliveryStatus"] == "0") {
                deliveryStatusWidth = "0%";
                deliveryStatusWidthDisNone = " disNone";
            }

            console.log("LastIS", columnAccess[0]["isLastQty"]);

            if (columnAccess[0]["isLastQty"] == "0") {
                isLastQtyWidth = "0%";
                isLastQtyDisNone = " disNone";
            }

            if (columnAccess[0]["isBalanceQty"] == "0") {
                isBalanceQtyWidth = "0%";
                isBalanceDisNone = " disNone";
            }

            if (columnAccess[0]["isBarcodeShowInStock"] == "0") {
                isProductCodeWith = "0%";
                isProductCodeDisNone = " disNone";
            }


            var table = $('#dataListTable').DataTable({
                "aaData": JSON.parse(data.d),
                "order": [[0, "desc"]],
                "columnDefs": [
                    {
                        "width": "0%",
                        "className": "id disNone",
                        "targets": [0]
                    },
                    {
                        "width": "10%",
                        "className": "prodId ",
                        "targets": [1]
                    },
                    {
                        "className": "itemName",
                        "width": "20%",
                        "targets": [2]
                    },
                    {
                        "className": "prodCode" + isProductCodeDisNone,
                        "width": isProductCodeWith,
                        "targets": [3]
                    },
                    {
                        "className": "details",
                        "width": "20%",
                        "targets": [4]
                    },
                    {
                        "className": "Attributes",
                        "width": "10%",
                        "targets": [5]
                    },
                    {
                        "className": "qty",
                        "width": "10%",
                        "targets": [6]
                    },
                    {
                        "className": "lastQty" + isLastQtyDisNone,
                        "width": isLastQtyWidth,
                        "targets": [7]
                    },
                    {
                        "className": "BalanceQty" + isBalanceDisNone,
                        "width": isBalanceQtyWidth,
                        "targets": [8]
                    },
                    {
                          "className": "bPrice disNone",
                          "width": "0%",
                          "targets": [9]
                    },
                    {
                        "className": "inventoryAmount disNone",
                        "width": "0%",
                        "targets": [10]
                    },
                    {
                        "className": "Date",
                        "width": "10%",
                        "targets": [11]
                    },
                    {
                        "className": "status",
                        "width": "10%",
                        "targets": [12]

                    },
                    {
                        "className": "storeName",
                        "width": "10%",
                        "targets": [13]
                    },
                    {
                        "className": "searchType disNone",
                        "width": "0%",
                        "targets": [14]
                    },
                    {
                        "className": "deliveryStatus" + deliveryStatusWidthDisNone,
                        "width": deliveryStatusWidth,
                        "targets": [15]
                    }
                ],
                "columns": [
                    {
                        "title": "Id",
                        "data": "Id"
                    },
                    {
                        "title": ID,
                        "data": "prodId"
                    },
                    {
                        "title": ItemName,
                        "data": "itemName"
                    },
                    {
                        "title": Product_code,
                        "data": "prodCode"
                    },
                    {
                        "title": Details,
                        "data": "details",
                        "render": function (details) {
                            if (details != "")
                                return "Invoice No: " + details;
                            else
                                return details;
                        }
                    },
                    {
                        "title": Attributes,
                        "data": "attributeRecord",
                        "render": function (attributeRecord, type, full, meta) {
                            if (attributeRecord == "0" || attributeRecord == "")
                                return "N/A";
                            console.log("attributeRecord:", attributeRecord);

                            var attrRecord = attributeRecord;
                            var attrFieldName = "";

                            $.ajax({
                                url: baseUrl + "Admin/InventoryBundle/View/Stock.aspx/getFieldAttributeNameDataAction",
                                data: JSON.stringify({ "attributeRecord": attrRecord }),
                                dataType: "json",
                                type: "POST",
                                /*async: false,*/
                                contentType: "application/json; charset=utf-8",
                                success: function (dataAttr) {
                                    attrFieldName = dataAttr.d.toString();
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

                            return attrFieldName;
                        }
                    },
                    {
                        "title": Qty,
                        "data": "qty"
                    },
                    {
                        "title": _LastStock,
                        "data": "lastQty"
                    },
                    {
                        "title": _BalanceStock,
                        "data": "balanceQty"
                    },
                    {
                        "title": _BuyPrice,
                        "data": "bPrice"
                    },
                    {
                        "title": _InventoryAmount,
                        "data": "inventoryAmount"
                    },
                    {
                        "title": _Date,
                        "data": "entryDate",
                        "render": function (entryDate) {
                            var date = new Date(parseInt(entryDate.substr(6)));
                            return moment(date).format("DD-MMM-YYYY");;
                        }
                    },
                    {
                        "title": _Status,
                        "data": "status",
                        "render": function (dataStatus, type, full, meta) {
                            console.log("dataStatus:", dataStatus);
                            isDeliveryStatus = false;
                            if (dataStatus == 'stock')
                                return "Stocked";
                            else if (dataStatus == 'stockReceive')
                                return "Received";
                            else if (dataStatus == 'stockReturn')
                                return "Returned";
                            else if (dataStatus == 'sale')
                                return "Sold";
                            else if (dataStatus == 'saleReturn')
                                return "Sold Back";
                            else if (dataStatus == 'Damage')
                                return "Damaged";
                            else if (dataStatus == 'stockPending')
                                return ' <button type="button" class="btn btn-sm btn-warning" value=' + full.Id + ' onclick="changeStockStatus(' + full.Id + ')">Pending</button>';
                            else if (dataStatus == 'stockTransfer') {
                                isDeliveryStatus = true;
                                return 'Transferred';
                            }
                            else
                                return dataStatus;
                        }
                    },
                    {
                        "title": _Store,
                        "data": "storeName"
                    },

                    {
                        "title": "searchType",
                        "data": "searchType"
                    },
                    {
                        "title": "Delivery",
                        "data": "deliveryStatus",
                        "render": function (deliveryStatus, type, full, meta) {

                            if (isDeliveryStatus) {
                                if (deliveryStatus == "0") {
                                    return '<button type="button" class="btn btn-sm btn-info" value=' + full.Id + ' onclick="changeDeliveryStatus(' + full.Id + ')">Delivery</button>';
                                }
                                else if (deliveryStatus == "1") {
                                    return "Delivered";
                                }
                                else {
                                    return "N/A";
                                }
                            }
                            else {
                                return 'N/A';
                            }
                        }
                    }
                ],
                "lengthMenu": [
                    [10, 25, 50, 100, -1],
                    [10, 25, 50, 100, "All"]
                ],
                "pageLength": 10,
                "paging": true,
                "searching": true
            });

            var buttons = new $.fn.dataTable.Buttons(table,
               {
                   "buttons": [
                       {
                           "extend": 'print',
                           "exportOptions": {
                               "columns": [1, 2, 3, 4, 5, 6, 7, 8]
                           },
                           "text": '',
                           "autoPrint": true,
                           "className": 'glyphicon glyphicon-print datatable-button',
                           "customize": function (win) {


                               $('h1').addClass('disNone');
                               $(win.document.body).find('h1').addClass('disNone').css('font-size', '9px');


                               $(win.document.body)
                                   .css('text-align', 'center');


                               var companyName = $('#contentBody_lblHiddenCompanyName').val();
                               var companyAddress = $('#contentBody_lblHiddenCompanyAddress').val();
                               var companyPhone = $('#contentBody_lblHiddenCompanyPhone').val();

                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Inventory Report List</b></p>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + companyPhone + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25; margin-top: 5">' + companyName + '</h3>');


                           }
                       },
                       {
                           "extend": 'collection',

                           "text": '',
                           "className": 'glyphicon glyphicon-export datatable-button',
                           "buttons": [
                               {
                                   "extend": 'pdf',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 4, 6, 7, 8, 9]
                                   }
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 6, 7, 8, 9, 10, 11, 12, 13]
                                   }
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 6, 7, 8, 9, 10, 11, 12, 13]
                                   }
                               }
                           ]
                       }
                   ]
               }).container().appendTo($('#filterPanel'));


            // Parameter Search
            if (prodId != "")
                $('.search').text(prodId);


        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });


}



var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
        else
            return "";
    }
};



function changeStockStatus(deliveryId) {

    $.ajax({
        "type": "Post",
        "url": baseUrl + "Admin/ReportBundle/View/InventoryReport.aspx/changeStatusDataAction",
        "data": "{deliveryId: '" + JSON.stringify(deliveryId) + "'}",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            searchType = "product";
            geInventoryDataList(searchType);
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




function changeDeliveryStatus(deliveryId) {
    $.ajax({
        "type": "Post",
        "url": baseUrl + "Admin/ReportBundle/View/InventoryReport.aspx/changeDeliveryStatusDataAction",
        "data": "{deliveryId: '" + JSON.stringify(deliveryId) + "'}",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        success: function (dataDeliveryStatus) {

            showMessage("Deliveried successfully", "Success");

            searchType = "product";
            geInventoryDataList(searchType);
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
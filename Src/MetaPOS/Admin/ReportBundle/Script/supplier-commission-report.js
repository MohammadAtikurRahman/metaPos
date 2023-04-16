function eventChange() {

    // default datetime
    var dateEntered = Date.now();
    var toDayDate = moment.utc(dateEntered).format("DD-MMM-YYYY");
    $('#txtDateFrom').val(toDayDate);
    $('#txtDateTo').val(toDayDate);
    //console.log("date:", moment.utc(dateEntered).format("DD-MMM-YYYY"));

    var searchType = "product";
    getCategoryDataList(searchType);

    getSupplierCommissionDataList();

}


$('#ddlCategoryWiseReport, #txtDateFrom, #txtDateTo,#contentBody_ddlStoreList,#contentBody_ddlUserList').change(function () {

    getSupplierCommissionDataList();

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

            $('#ddlCategoryWiseReport').empty().append('<option selected="selected" value="0">' + select_category + '</option>');

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


function getSupplierCommissionDataList() {
    console.log("Ok");

    var category = $('#ddlCategoryWiseReport').val();
    var dateFrom = $('#txtDateFrom').val();
    var dateTo = $('#txtDateTo').val();
    var prodId = getUrlParameter("prodId");
    var storeId = $('#contentBody_ddlStoreList').val();
    var userId = $('#contentBody_ddlUserList').val();

    if (category == null)
        category = 0;
    if (dateTo == "")
        dateTo = moment().format('YYYY MM DD');

    if (dateFrom == "")
        dateFrom = moment().format('YYYY MM DD');

    var jsonData = {
        "category": category,
        "dateFrom": dateFrom,
        "dateTo": dateTo,
        "prodId": prodId,
        "storeId": storeId,
        "userId": userId
    };

    $.ajax({
        "type": "Post",
        "url": baseUrl + "Admin/ReportBundle/View/SupplierCommission.aspx/getSupplierCommissionReportDataListAction",
        "data": "{jsonData: '" + JSON.stringify(jsonData) + "'}",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var jsonGetData = JSON.parse(data.d);

            $('#dataListTable').DataTable().destroy();
            $('#dataListTable').empty();


            var isProductCodeWith = "10%";
            var isProductCodeDisNone = "";

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
                        "className": "details disNone",
                        "width": "20%",
                        "targets": [4]
                    },
                    {
                        "className": "qty",
                        "width": "10%",
                        "targets": [5]
                    },
                    {
                        "className": "commission",
                        "width": "18%",
                        "targets": [6]
                    },
                    {
                        "className": "commissionAmt",
                        "width": "10%",
                        "targets": [7]
                    },

                    {
                        "className": "Date",
                        "width": "10%",
                        "targets": [8]
                    },
                    {
                        "className": "status disNone",
                        "width": "10%",
                        "targets": [9]

                    },
                    {
                        "className": "storeName",
                        "width": "10%",
                        "targets": [10]
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
                        "title": Item_Name,
                        "data": "itemName"
                    },
                    {
                        "title": P_Code,
                        "data": "prodCode"
                    },
                    {
                        "title": "Details",
                        "data": "details",
                        "render": function (details) {
                            if (details != "")
                                return "Invoice No: " + details;
                            else
                                return details;
                        }
                    },

                    {
                        "title": Qty,
                        "data": "qty"
                    },
                    {
                        "title": Commission,
                        "data": "commission"
                    },
                    {
                        "title": Amount,
                        "data": "commissionAmt"
                    },
                    {
                        "title": cDate,
                        "data": "entryDate",
                        "render": function (entryDate) {
                            var date = new Date(parseInt(entryDate.substr(6)));
                            return moment(date).format("DD MMM YYYY");;
                        }
                    },
                    {
                        "title": "Status",
                        "data": "status",
                        "render": function (dataStatus, type, full, meta) {
                            if (dataStatus == 'supplierCommission')
                                return "Supplier Commission";
                            else
                                return dataStatus;
                        }
                    },
                    {
                        "title": Store,
                        "data": "storeName"
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
                           "title": function () {
                               var printTitle = "<span class='com-name'>" + $('.company-name').text() + "<span><br/> <span class='title'>" + $('.title').text() + "</span> <br/>  <span class='date-time'> Print Date:" + moment().format("DD-MMM-YYYY") + "</span>";
                               return printTitle;
                           },
                           "exportOptions": {
                               "columns": [1, 2, 3, 5, 6, 7, 8, 10]
                           },
                           "text": '',
                           "autoPrint": true,
                           "className": 'glyphicon glyphicon-print datatable-button',
                           "customize": function (win) {


                               var companyName = $('#contentBody_lblHiddenCompanyName').val();
                               var companyAddress = $('#contentBody_lblHiddenCompanyAddress').val();
                               var companyPhone = $('#contentBody_lblHiddenCompanyPhone').val();


                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Supplier Commission List</b></p>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + companyPhone + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25; margin-top: 5">' + companyName + '</h3>');



                               $(win.document.body)
                                   .css('text-align', 'center');

                               $(win.document.body).find('.title')
                                 .css('font-size', '20px');

                               $(win.document.body).find('.com-name')
                                 .css('font-size', '25px');

                               $(win.document.body).find('.date-time')
                                 .css('font-size', '16px');

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
                                       "columns": [1, 2, 3, 4, 5, 6, 7, 8]
                                   }
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 4]
                                   }
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 4]
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




$(function () {

    displayDataTable(true);


});


// On page load
function eventChange() {

    activeModule = "sale";

    checkPagePermission("DueReminder");

    displayDataTable(false);
}


$('#txtTo, #txtFrom').change(function () {
    displayDataTable(false);
});



$('#chkWithDueCustomer, #ddlActiveStatus,#chkAllHistory').change(function () {
    displayDataTable(false);
});


// Display datatable list
var totalDueFooter=0;
function displayDataTable(footerStatus) {

    var withDueCustomer = $('#chkWithDueCustomer').is(':checked');
    var chkAllHistory = $('#chkAllHistory').is(':checked');

    var reminderBetween = "";
    if (withDueCustomer == true) {
        reminderBetween = " ";
    }
    else {
        reminderBetween = " AND convert(date, tbl1.nextPayDate, 101) BETWEEN @" + $('#txtFrom').val() + "@ AND @" + $('#txtTo').val() + "@ ";
    }

    var reportHistory = chkAllHistory;
    if (reportHistory)
        reportHistory = "0,1";

    var urlParams = getUrlParameterSingle("customer");

    var jsonData = {
        "select": "distinct tbl1.nextPayDate,tbl1.Id,tbl1.customerId,tbl1.status,tbl1.downPayment,tbl1.paidAmt,tbl2.name,tbl2.phone,tbl2.accountNo,tbl2.address,tbl3.refName,tbl3.refPhone,tbl3.refAddress,tbl3.grossAmt,tbl3.giftAmt,tbl3.balance,tbl3.billNo",
        "from": "CustomerReminderInfo tbl1 INNER JOIN CustomerInfo tbl2 ON tbl1.customerId = tbl2.cusID INNER JOIN saleInfo tbl3 ON tbl1.billNo = tbl3.billNo ",
        "where": {
            "tbl1.active": '1',
            "AND": '',
            "tbl1.payStatus": reportHistory,
            "between": reminderBetween

        },
        "column": "tbl1.nextPayDate",
        "dir": "ASC"
    };

    getSettingAccessValue();
    var sendMessage = "";
    if (columnAccess[0]["displayInstallmentMessage"] == "0")
        sendMessage = "disNone";

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataJoinListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            //var actionEditHtml =
            //    "<a id='btnUpdateModal' href='javascript:void(0)' class='glyIconPosition' data-toggle='modal' data-backdrop='static' data-toggle='modal' data-target='#myModal'>" +
            //        "<span class='glyphicon glyphicon-edit'>" +
            //        "</span></a>";
            
            if (footerStatus)
                totalDueFooter = 0;
            console.log("totalDueFooter:", totalDueFooter);
            console.log("footerStatus:", footerStatus);


            var actionEditHtml =
                "<a id='btnUpdateModal' href='javascript:void(0)' class='glyIconPosition'> <span class='glyphicon glyphicon glyphicon-edit'></span></a>";


            var actionHtml = actionEditHtml;

            var actionSendMessageHtml =
                "<a id='btnSendMessageModal' href='javascript:void(0)' class='glyIconPosition'> <span class='glyphicon glyphicon glyphicon-send'></span></a>";


            if ($.fn.DataTable.isDataTable('#dataListTable')) {
                $('#dataListTable').DataTable().destroy();
            }

            $('#dataListTable').empty();
            console.log("start database");

            var table = $('#dataListTable').DataTable({
                "aaData": JSON.parse(data.d),
                "order": [[12, "desc"]],
                "columnDefs": [
                    {
                        "width": "5%",
                        "className": "id",
                        "targets": [0]
                    },
                    {
                        "className": "BillNo ",
                        "width": "15%",
                        "targets": [1]
                    },
                    {
                        "className": "accountNo disNone",
                        "width": "10%",
                        "targets": [2]
                    },
                    {
                        "className": "cusName",
                        "width": "20%",
                        "targets": [3]
                    },
                    {
                        "className": "phone",
                        "width": "10%",
                        "targets": [4]
                    },
                    {
                        "className": "address",
                        "width": "10%",
                        "targets": [5]
                    },
                    {
                        "className": "refName disNone",
                        "width": "10%",
                        "targets": [6]
                    },
                    {
                        "className": "refPhone disNone",
                        "width": "10%",
                        "targets": [7]
                    },
                    {
                        "className": "refAddress disNone",
                        "width": "10%",
                        "targets": [8]
                    },
                    {
                        "className": "Total",
                        "width": "10%",
                        "targets": [9],
                        "render": function (data, type, row) {
                            return '&#2547; ' + data;
                        }
                    },
                    {
                        "className": "Paid ",
                        "width": "10%",
                        "targets": [10]
                    },
                    {
                        "className": "giftAmt ",
                        "width": "10%",
                        "targets": [11]
                    },

                    {
                        "className": "Instalment",
                        "width": "10%",
                        "targets": [12],
                        "render": function (data, type, row) {
                            return '&#2547; ' + data;
                        }
                    },
                    {
                        "className": "paid",
                        "width": "10%",
                        "targets": [13],
                        "render": function (data, type, row) {
                            return '&#2547; ' + data;
                        }
                    },
                    {
                        "className": "Due",
                        "width": "10%",
                        "targets": [14],
                        "render": function (dataDue, type, row) {
                            var downPayment = row["downPayment"];
                            //var due = parseFloat(downPayment) - parseFloat(dataDue);
                            var due = parseInt(downPayment) - parseInt(dataDue);

                            if (footerStatus) {
                                totalDueFooter += due;
                            }
                            return '&#2547; ' + due.toFixed(2);
                        },
                    },
                    {

                        "className": "PayDate",
                        "width": "20%",
                        "targets": [15],
                        render: function (dataNextPay) {

                            var date = new Date(parseInt(dataNextPay.substr(6)));
                            var inputDate = $('#txtTo').val();

                            var selectedDate = $('#txtTo').val() == "" ? new Date() : new Date(inputDate);
                            if (date < selectedDate) {


                                return '<span class="past">' + moment(date).format("DD MMM YYYY") + '</span>';

                            }
                            else {
                                return '<span class="current">' + moment(date).format("DD MMM YYYY") + '</span>';

                            }
                        }

                    },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action ",
                        "width": "10%",
                        "targets": [16]
                    },
                    {
                        "orderable": false,
                        "defaultContent": actionSendMessageHtml,
                        "className": "Send " + sendMessage,
                        "width": "10%",
                        "targets": [17]
                    }
                ],
                "columns": [
                    {
                        "title": "",
                        "data": "Id"
                    },
                    {
                        "title": Bill_no,
                        "data": "billNo"
                    },
                    {
                        "title": "Acc No",
                        "data": "accountNo"
                    },
                    {
                        "title": Name,
                        "data": "name"
                    },
                    {
                        "title": Phone,
                        "data": "phone"
                    },
                    {
                        "title": Address,
                        "data": "address"
                    },
                    {
                        "title": "Garanter",
                        "data": "refName"
                    },
                    {
                        "title": "G. Phone",
                        "data": "refPhone"
                    },
                    {
                        "title": "G. Address",
                        "data": "refAddress"
                    },

                    {
                        "title": Total,
                        "data": "grossAmt"
                    },
                    {
                        "title": Total_Paid,
                        "data": "balance"
                    },
                    {
                        "title": Due,
                        "data": "giftAmt"
                    },
                    {
                        "title": Instalment,
                        "data": "downPayment"
                    },
                    {
                        "title": Paid,
                        "data": "paidAmt"
                    },
                    {
                        "title": Due,
                        "data": "paidAmt"
                    },
                    {
                        "title": Pay_Date,
                        //"data": null
                        "data": "nextPayDate",
                        //"render": function (dataNextPay) {
                        //    var date = new Date(parseInt(dataNextPay.substr(6)));

                        //    return moment(date).format("DD MMM YYYY");;
                        //}
                    },

                    {
                        "title": Action,
                        "data": null
                    },

                    {
                        "title": "Send",
                        "data": null
                    }

                ],
                "lengthMenu": [
                    [-1],//10, 25, 50, 100,
                    ["All"]// 10, 25, 50, 100, 
                ],
                //"pageLength": 10,
                "paging": true,
                "searching": true,
                "footer": true,
            });



            $('input[type="search"]').val(urlParams);


            // datatable footer
            if (footerStatus) {
                $('#dataListTable').append(
                    '<tfoot> ' +
                    '<tr>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th><strong>TOTAL</strong></th>' +
                    '<th><strong>&#2547;<span id="tfFooter">' + totalDueFooter + '</span></strong></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '<th></th>' +
                    '</tr>' +
                    '</tfoot>'
                );
            }


            // serach event
            var iDueCounter = 0;
         

            $('#dataListTable').on('order.dt search.dt page.dt', function () {
                console.log('search', $('.dataTables_filter input').val());
                //var random_color = colors[Math.floor(Math.random() * colors.length)];
                $('#dataListTable tbody').find('tr').each(function (i) {

                    // $(this).css('background-color', '#0000cd');
                    var d = $(this).find('.Due').text();
                });
            });

            // click action     
            $('#dataListTable tbody').on('click', '#btnUpdateModal', function () {
                var index = $(this).closest('tr').index();

                //console.log("index:", index);

                var dataStore = table.rows({ filter: 'applied' }).data();
                console.log("dataStore:", dataStore);
                var id = dataStore[index]["Id"];
                console.log("index id:", id);
                var cusId = dataStore[index]["customerId"];

                var billNo = dataStore[index]["billNo"];

                window.location.href = "invoice-next?id=" + billNo + "&cusid=" + cusId + "&from=reminder&reminderId=" + id + "";


                //
                //$('#lblCustomerId').text(dataStore["customerId"]);
                //$('#lblCustomerName').text(dataStore["name"]);
                //$('#lblTotalPaid').text(dataStore["totalPaid"].toFixed(2));
                //$('#lblTotalDue').text(dataStore["totalDue"].toFixed(2));

                //var date = new Date(parseInt(dataStore["nextPayDate"].substr(6)));
                //var month = date.getMonth() + 1;
                //var originalDate = date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                //console.log(originalDate);
                //$('#txtNextPayDate').val(originalDate);


            });

            // Send Message
            $('#dataListTable tbody').on('click', '#btnSendMessageModal', function () {

                var index = $(this).closest('tr').index();
                var dataStore = table.rows({ filter: 'applied' }).data();

                var cusId = dataStore[index]["customerId"];
                var customer = dataStore[index]["name"];
                var invoiceNo = dataStore[index]["billNo"];

                var date = new Date(parseInt(dataStore[index]["nextPayDate"].substr(6)));
                var nextPayDate = moment(date).format("DD MMM YYYY");

                var installmentAmt = dataStore[index]["downPayment"];
                var phoneNumber = dataStore[index]["phone"];
                installmentMessageSend(customer, invoiceNo, nextPayDate, installmentAmt, phoneNumber);

            });

            var buttons = new $.fn.dataTable.Buttons(table,
               {
                   "buttons": [
                       {
                           "extend": 'print',
                           "exportOptions": {
                               "columns": [0,2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                           },
                           orientation: 'landscape',
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
                               var comPhone = $('#contentBody_lblHiddenCompanyPhone').val();

                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Customer Instalment List</b></p>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + comPhone + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25;margin-top: 5">' + companyName + '</h3>'); //before the table
                               
                               
                           }
                       },
                       {
                           "extend": 'collection',
                           "exportOptions": {
                               "columns": [1]
                           },
                           "text": '',
                           "className": 'glyphicon glyphicon-export datatable-button',
                           "buttons": [
                               {
                                   "extend": 'pdf',
                                   "exportOptions": {
                                       "columns": [2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                                   },
                                   orientation: 'landscape',
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                                   },
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                                   },
                               },
                           ]
                       }
                   ]
               }).container().appendTo($('#filterPanel'));
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });


}






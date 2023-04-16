
$(function () {

    loadCustomerDatatable();
});


$('#contentBody_ddlCusType, #contentBody_ddlActiveStatus').change(function () {
    loadCustomerDatatable();
});



function loadCustomerDatatable() {

    var jsonData = {
        "cusType": $('#contentBody_ddlCusType').val(),
        "payStatus": $('#contentBody_ddlSearchByPayStatus').val(),
        "activeStatus": $('#contentBody_ddlActiveStatus').val()
    };


    $.ajax({
        "type": "Post",
        "url": baseUrl + "/Admin/CustomerBundle/View/Customer.aspx/getCustomerDataListAction",
        "data": "{jsonData: '" + JSON.stringify(jsonData) + "'}",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var actionHtml = '<div class="dropdown customer-dropdown">' +
                '<button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">' +
                Action +
                '<span class="caret"></span>' +
                '</button>' +
                '<ul class="dropdown-menu" aria-labelledby="dropdownMenu1">' +
                '<li><a id="viewCustomer">' + View + '</a></li>';

            if ($('#contentBody_ddlActiveStatus').val() != "0") {
                actionHtml += '<li><a id="addPayment">' + Add_Payment + '</a></li>' +
                    '<li><a id="addAdvanceCustomer">' + Add_Advance + '</a></li>' +
                    '<li><a id="addOpeningDue">' + Add_Opening_Due + '</a></li>' +
                    '<li><a id="invoiceStatus">' + Invoices + '</a></li>' +
                    '<li><a id="fullLedgerReport" runat="server">' + Full_Ledger + '</a></li>' +
                    '<li><a id="editCustomer">' + Edit + '</a></li>' +
                    '<li><a id="deleteCustomer">' + Delete + '</a></li>';
            }
            else {
                actionHtml += '<li><a id="restoreCustomer">Restore</a></li>';
            }

            actionHtml += '</ul>' +
            '</div>';

            if ($.fn.DataTable.isDataTable('#dataListTable')) {
                $('#dataListTable').DataTable().destroy();
            }

            $('#dataListTable').empty();

            $("#dataListTable").append('<tfoot><hr/><tr><th></th><th></th><th></th><th></th><th></th><th>' + Total + '</th><th id="dueTotal"></th><th></th></tr></tfoot>');

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
                        "className": "cusID",
                        "targets": [1]
                    },
                    {
                        "width": "10%",
                        "className": "AccountNo disNone",
                        "targets": [2]
                    },
                    {
                        "width": "20%",
                        "className": "name",
                        "targets": [3]
                    },
                    {
                        "width": "20%",
                        "className": "address",
                        "targets": [4]
                    },
                    {
                        "width": "15%",
                        "className": "phone",
                        "targets": [5]
                    },
                    {
                        "width": "10%",
                        "className": "totalDue",
                        "targets": [6]
                    },
                    {
                        "width": "0%",
                        "className": "isInstalment disNone",
                        "targets": [7]
                    },
                    {
                        "width": "0%",
                        "className": "disNone",
                        "targets": [8]
                    },
                    {
                        "width": "0%",
                        "className": "disNone",
                        "targets": [9]
                    },
                    {
                        "width": "0%",
                        "className": "disNone",
                        "targets": [10]
                    },
                    {
                        "width": "0%",
                        "className": "disNone",
                        "targets": [11]
                    },
                    {
                        "width": "0%",
                        "className": "disNone",
                        "targets": [12]
                    },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action ",
                        "width": "10%",
                        "targets": [13]
                    }
                ],
                "columns": [
                    {
                        "title": "",
                        "data": "Id"
                    },
                    {
                        "title": ID,
                        "data": "cusID"
                    },
                    {
                        "title": "Account No",
                        "data": "AccountNo"
                    },
                    {
                        "title": Name,
                        "data": "name"
                    },
                    {
                        "title": Address,
                        "data": "address"
                    },
                    {
                        "title": Phone,
                        "data": "phone"
                    },
                    {
                        "title": Total_Balance,
                        "data": "balance", render: function (balanceData) {
                            return balanceData == null ? 0 : balanceData;
                        }
                    },
                    {
                        "title": "Instalment",
                        "data": "installmentStatus"
                    },
                    //{
                    //    "title": Address,
                    //    "data": "address"
                    //},
                    {
                        "title": Email,
                        "data": "mailInfo"
                    },
                    {
                        "title": "Notes",
                        "data": "notes"
                    },
                    {
                        "title": "CusType",
                        "data": "CusType"
                    },
                    {
                        "title": "designation",
                        "data": "designation"
                    },
                    {
                        "title": Action,
                        "data": null
                    }
                    

                ],
                "lengthMenu": [
                    [10, 25, 50, 100, -1],
                    [10, 25, 50, 100, "All"]
                ],
                "pageLength": 10,
                "paging": true,
                "searching": true,
                "deferRender": true,

                //
                "footerCallback": function (row, datad, start, end, display) {
                    var api = $('#dataListTable').DataTable();


                    var intVal = function (i) {
                        return typeof i === 'string' ?
                            i.replace(/[\$,]/g, '') * 1 :
                            typeof i === 'number' ?
                            i : 0;
                    };

                    // This is for the Total text
                    var col0 = api
                        .column(6)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);
                    //First, please note var name col1 and we use it then
                    var col1 = api
                        .column(6)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);
                    console.log("col1:", col1);
                    $(api.column(9).footer()).html('Total');
                    // Here you can add the rows
                    $('#dueTotal').html(col1);
                },
                
            });
            var buttons = new $.fn.dataTable.Buttons(table,
               {
                   "buttons": [
                       {
                           "extend": 'print',
                           "exportOptions": {
                               "columns": [1, 3, 4, 5, 6, 8, 9]
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

                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Customer List</b></p>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + companyPhone + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25; margin-top: 5">' + companyName + '</h3>');


                           }
                       },
                       {
                           "extend": 'collection',
                           "exportOptions": {
                               "columns": [1, 3, 4, 5, 6, 8, 9]
                           },
                           "text": '',
                           "className": 'glyphicon glyphicon-export datatable-button',
                           "buttons": [
                               {
                                   "extend": 'pdf',
                                   "exportOptions": {
                                       "columns": [1, 3, 4, 5, 6, 8, 9]
                                   },
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 3, 4, 5, 6, 8, 9]
                                   },
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 3, 4, 5, 6, 8, 9]
                                   },
                               }
                           ]
                       }
                   ]
               }).container().appendTo($('#filterPanel'));

            ////futytuty
            //cusPayStatus();

             //customerGridColumnHide();

        }
    });
}

$('#dataListTable').on('draw.dt', function () {
    cusPayStatus();
});





function Clear() {
    $('#dataListTable tr').show();
}


$('#contentBody_ddlSearchByPayStatus').change(function () {

    Clear();

    cusPayStatus();

});


function cusPayStatus() {

    var payStatus = $('#contentBody_ddlSearchByPayStatus').val();
    var table = $('#dataListTable').DataTable();
    $('#dataListTable tr > td:nth-child(7)').each(function () {

        if (payStatus == "0") {
            if ($(this).html() != "0") {
                $(this).parent().hide();
            }
        }
        else if (payStatus == "1") {
            if ($(this).html() == "0") {
                $(this).parent().hide();
            }
        }
        else {
            $('#dataListTable tr').show();
        }

    });



//$.each($("#dataListTable tr"), function () { //get each tr which has selected class
    //    console.log("dataa:", $(this).find('td').eq(6).text());
    //    var totalDue = $(this).find('td').eq(6).text();

    //    var column = table.column($(this).attr('data-column'));

    //    console.log("AAA:", table.column(6));

    //    console.log("payStatus b:", payStatus);
    //    if (payStatus == 0) {
    //        console.log("payStatus:", payStatus);
    //        table.column(6).search(0).draw();
    //    }
    //    else if (totalDue == payStatus) {
    //        table.column(6).search(payStatus).draw();
    //    }
    //});

    //table
    //    .search(payStatus)
    //    .draw();

}




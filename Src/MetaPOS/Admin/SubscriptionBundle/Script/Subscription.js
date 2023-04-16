

var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";
var invoice = getInvoiceFormUrl();
console.log("invoice:", invoice);

$(function () {

    $('#contentBody_txtMonth').keyup(function () {
        var fee = $('#contentBody_lblFee').text();

        var month = $(this).val();
        if (month == null || month == "") {
            month = 1;
        }

        var totalFee = parseFloat(month) * parseFloat(fee);

        $('#contentBody_lblTotalFee').text(totalFee.toFixed(2));

    });

    getSubscriptionData();

    loadCurrentBalance();

    //paymentModal();


    //if (invoice != "") {
    //    confirmationPayment();
    //}

});


$('#divCheckedSendEmail input[type="checkbox"]').click(function () {
    if ($(this).is(":checked")) {
        $('#divEmailOption').removeClass('disNone')
    } else {
        $('#divEmailOption').addClass('disNone')
        $('#contentBody_txtEmailAddress').val("")
    }
});


$('#divTerms input[type="checkbox"]').click(function () {
    if ($(this).is(":checked")) {
        $('#contentBody_btnPayment').removeAttr('disabled');
        
        console.log("checked");
    } else {
        $('#contentBody_btnPayment').attr('disabled', 'true');
        console.log("not checked");
    }
})


function getSubscriptionData() {

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/SubscriptionBundle/View/Subscription.aspx/getSubscriptionDataListAction",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#dataListTable').DataTable().destroy();
            $('#dataListTable').empty();


            var table = $('#dataListTable').DataTable({
                "aaData": JSON.parse(data.d),
                "order": [[1, "desc"]],
                "columnDefs": [
                    {
                        "width": "0%",
                        "className": "id disNone",
                        "targets": [0]
                    },
                    {
                        "className": "invoice-no",
                        "width": "10%",
                        "targets": [1]
                    },
                    {
                        "className": "name",
                        "width": "10%",
                        "targets": [2]
                    },
                    {
                        "className": "description",
                        "width": "10%",
                        "targets": [3]
                    },
                    {
                        "className": "cashin",
                        "width": "10%",
                        "targets": [4]
                    },
                    {
                        "className": "cashout",
                        "width": "10%",
                        "targets": [5]
                    },
                    {
                        "className": "type",
                        "width": "10%",
                        "targets": [6]
                    },
                    {
                        "className": "status",
                        "width": "10%",
                        "targets": [7]
                    },
                    {
                        "className": "createDate",
                        "width": "10%",
                        "targets": [8]
                    }
                ],
                "columns": [
                    {
                        "title": "",
                        "data": "Id"
                    },
                    {
                        "title": InvoiceNo,
                        "data": "invoiceNo"
                    },
                    {
                        "title": Title,
                        "data": "name"
                    },
                    {
                        "title": Description,
                        "data": "description"
                    },
                    {
                        "title": CashIn,
                        "data": "cashin"
                    },
                    {
                        "title": CashOut,
                        "data": "cashout"
                    },
                    {
                        "title": Type,
                        "data": "type"
                    },
                    {
                        "title": Status,
                        "data": "status",
                        "render": function (statusData) {
                            if (statusData == "0")
                                return "Pending";
                            else if (statusData == "1")
                                return "Accepted";
                            else
                                return "Unkown";
                        }
                    },
                    {
                        "title": sDate,
                        "data": "createDate",
                        "render": function (entryDate) {
                            return moment(entryDate).format('DD-MMM-YYYY h:mm a');
                        }
                    }
                ],
                "lengthMenu": [
                    [10, 25, 50, 100, -1],
                    [10, 25, 50, 100, "All"]
                ],
                "pageLength": 10,
                "paging": true,
                "searching": true,
            });

            var buttons = new $.fn.dataTable.Buttons(table,
               {
                   "buttons": [
                       {
                           "extend": 'print',
                           "exportOptions": {
                               "columns": [1, 2, 3, 4, 5, 6, 7]
                           },
                           "text": '',
                           "autoPrint": true,
                           "className": 'glyphicon glyphicon-print datatable-button',
                           "customize": function (win) {
                               $(win.document.body)
                                   .css('text-align', 'center');


                           }
                       },
                       {
                           "extend": 'collection',
                           "exportOptions": {
                               "columns": [1, 2, 3, 4, 5, 6, 7]
                           },
                           "text": '',
                           "className": 'glyphicon glyphicon-export datatable-button',
                           "buttons": [
                               {
                                   "extend": 'pdf',
                                   "exportOptions": {
                                       "columns": [1,2,3,4,5,6,7]
                                   },
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 4, 5, 6, 7]
                                   },
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 4, 5, 6, 7]
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


function confirmationPayment() {

    $.ajax({
        url: baseUrl + "Admin/SubscriptionBundle/View/Subscription.aspx/confirmPaymentAction",
        data: '{ "invoice": "' + invoice + '" }',
        dataType: "json",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var dataSplit = data.d.split('|');
            showMessage(dataSplit[1], dataSplit[0]);

            loadCurrentBalance();
            getSubscriptionData();
        }
    });
    
}


function loadCurrentBalance() {

    $.ajax({
        url: baseUrl + "Admin/SubscriptionBundle/View/Subscription.aspx/loadCurrentBalanceAction",
        data: '',
        dataType: "json",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var balance = data.d;
            var calBalance = data.d;
            console.log("calBalance:", calBalance);

            if (parseFloat(balance) < 0)
                balance = 0;
            $('#contentBody_lblBalance').text(balance);

            $('#contentBody_lblCurrentBalance').text(balance);

            calculatePaymentAmout(calBalance);
        }
    });

}




function calculatePaymentAmout(currentBal) {
    console.log("currentBal:", currentBal);

    var duePayment = 0;
    var duePaymentTxt = 0;
    if (parseFloat(currentBal) > 0)
    {
        duePayment = 0;
    }
    else
    {
        duePaymentTxt = Math.abs(currentBal)
    }

    if (isNaN(duePaymentTxt))
        duePaymentTxt = 0;

    $('#contentBody_txtPaymentAmount').val(duePaymentTxt.toFixed(2));
    $('#contentBody_lblDueAmout').text(duePaymentTxt.toFixed(2));
}



function paymentModal() {
 
    $("#PaymentConfirmModal").on("shown.bs.modal", function () {
        $(this).find('iframe').attr('src', 'https://sandbox.portwallet.com/payment/?invoice=85D6D0B97DD1CA36');
        $(this).find('iframe').attr('width', '100%');
    });
    $("#PaymentConfirmModal").modal('show');

}


function getInvoiceFormUrl() {
    try {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
       sURLVariables = sPageURL.split('&');

        return sURLVariables[2].split('=')[1];
    }
    catch (e) {
        return "";
    }

};
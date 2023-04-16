

$('#btnSaveReminder').on("click", function() {
    var lblCusId = $('#lblCustomerId').text();

    var findjsonData = {
        "select": "totalPaid,totalDue",
        "from": "CustomerInfo",
        "where": {
            "cusId": lblCusId
        },

    };

    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataAction",
        dataType: "json",
        data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsonData = JSON.parse(data.d);

            updateAction(jsonData[0].totalPaid, jsonData[0].totalDue);
        },
        error: function (data) {
            console.log(data.responseText, "Error");
        },
        failure: function (data) {
            console.log(data.responseText, "Error");
        }
    });
   


});



function updateAction(totalPaid,totalDue) {

    var jsonDataCustomer = {
        "from": "CustomerInfo",
        "set": {
            "totalPaid": parseFloat(totalPaid) + parseFloat($('#txtPay').val() == "" ? 0 : $('#txtPay').val()),
            "totalDue": parseFloat(totalDue) - parseFloat($('#txtPay').val() == "" ? 0 : $('#txtPay').val()),

            //"totalPaid": parseInt(totalPaid) + parseInt($('#txtPay').val() == "" ? 0 : $('#txtPay').val()),
            //"totalDue": parseInt(totalDue) - parseInt($('#txtPay').val() == "" ? 0 : $('#txtPay').val()),

        },
        "where": {
            cusId: $('#lblCustomerId').text()
        }
    };

    var jsonDataReminder = {
        "from": "CustomerReminderInfo",
        "set": {
            "nextPayDate": $('#txtNextPayDate').val() == "" ? datetime.now : $('#txtNextPayDate').val()

        },
        "where": {
            customerId: $('#lblCustomerId').text()
        }
    };

    $.ajax({
        url: baseUrl + "Admin/InstalmentBundle/View/Default.aspx/updateCustomerReminderDataAction",
        dataType: "json",
        data: "{ 'jsonDataCustomer' : '" + JSON.stringify(jsonDataCustomer) + "','jsonDataReminder' : '" + JSON.stringify(jsonDataReminder) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#myModal").modal("hide");

            displayDataTable();

            showOutput(data.d);

            resetFormValue();
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}


function resetFormValue() {
    
}
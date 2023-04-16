

function installmentMessageSend(customer, invoiceNo, nextPayDate, installmentAmt, phoneNumber) {
    
    var branchId;
    if (userRightGlobal == "Regular") {
        branchId = branchIdGlobal;
    }
    else {
        branchId = roleIdGlobal;
    }


    var jsonDataField = {
        "select": "installmentTemplate ",
        "from": "BranchInfo",
        "where": {
            "Id": branchId
        },
        "column": "id",
        "dir": "desc"
    };

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/RecordBundle/View/Record.aspx/getBranchInfoAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonDataField) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (branchData) {
            var messageData = JSON.parse(branchData.d);

            // sms 
            var tempMessage = messageData[0].installmentTemplate;
          

            //Hello customer Your invoice No @billNo .You pay @paid and Due @due, thanks for shopping.- MetaPOS

            var message = "";
            if (tempMessage != null) {
                message = tempMessage.replace("@invoiceNo", invoiceNo).replace("@customer", customer).replace("@nextPayDate", nextPayDate).replace("@installmentAmt", installmentAmt);
            }


            phoneNumber = "+88" + phoneNumber;
            var jsonData = {
                "phoneList": phoneNumber,
                "message": message,
                "smsCost": 0,
                "messageCount": 1,
                "branchId": branchId,
                "customer": customer
            };
            $.ajax({
                url: baseUrl + "Admin/AppBundle/View/Operation.aspx/sendSmsDataAction",
                dataType: "json",
                data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //console.log("Msg Status:", data.d);
                    var jsonDataSuccess = JSON.parse(data.d);
                    console.log("jsonDataSuccess:", jsonDataSuccess);
                    if (jsonDataSuccess.Code == '200') {
                        showMessage("Message send successfully.", "Success");
                    }
                    else {
                        showMessage("Message can not send.", "Warning");
                    }
                    
                },
                failure: function (response) {
                    console.log(response);
                },
                error: function (response) {
                    console.log(response);
                }
            });
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });


}
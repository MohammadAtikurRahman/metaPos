
function sendInvoiceBySMS(phoneNumber, customerName, billNo, PaidAmt, DueAmt, cartAmt, grandAmt, isSendToOwner) {

    var branchId;
    if (userRightGlobal == "Regular") {
        branchId = branchIdGlobal;
    }
    else {
        branchId = roleIdGlobal;
    }


    var jsonDataField = {
        "select": "invoiceSmsTemplate,ownerNumber ",
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
            var tempMessage = messageData[0].invoiceSmsTemplate;
            var businessOwnerNumber = messageData[0].ownerNumber;

            //Hello customer Your invoice No @billNo .You pay @paid and Due @due, thanks for shopping.- MetaPOS

            var message = "";
            if (tempMessage != null) {
                message = tempMessage.replace("@billNo", billNo).replace("@paid", PaidAmt).replace("@due", DueAmt).replace("@customer", customerName).replace("@cartAmt", cartAmt).replace("@grandAmt", grandAmt);
            }

            if (isSendToOwner) {
                phoneNumber = businessOwnerNumber;
                

                if (businessOwnerNumber == "")
                    return;
            }

            phoneNumber = "+88" + phoneNumber;
            var jsonData = {
                "phoneList": phoneNumber,
                "message": message,
                "smsCost": 0,
                "messageCount": 1,
                "branchId": branchId,
                "customer": customerName
            };

            $.ajax({
                url: baseUrl + "Admin/AppBundle/View/Operation.aspx/sendSmsDataAction",
                dataType: "json",
                data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    console.log("Msg Status:", data.d);
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
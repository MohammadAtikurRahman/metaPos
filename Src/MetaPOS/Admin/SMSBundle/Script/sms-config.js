function eventChange() {

    activeModule = "config";

}




// Select Group
//$('#ddlBranch').on('change', function () {
//    loadConfig($('#'));
//    console.log("a===", a);
//});



// Save Action
$(document).on("click",

    "#btnSaveChanges",
    function () {

        var findjsonData = {
            "select": "roleId",
            "from": "SmsConfigInfo",
            "where": {
                "roleId": $('#contentBody_lblHiddenSMSConfig').val() //ddBranch
            }
            
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.d !== "success") {
                    saveAction();
                }
                else if (data.d === "success") {
                    updateAction();
                }
                else {
                    updateAction();
                }
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
    });


// Save Action
function saveAction() {

    if (validateForm() === false)
        return;

    var jsonData = {
        "from": "SmsConfigInfo",
        "values":
            {
                "roleID": $('#contentBody_lblHiddenSMSConfig').val(), //ddBranch
                "apiKey": $('#contentBody_txtApiKey').val(), //txtApiKey
                "senderKey": $('#contentBody_txtSenderKey').val(),
                "username": $("#contentBody_txtUsername").val(),
                "password": $("#contentBody_txtPassword").val(),
                "medium": '-',
                "senderId": '-',
                "cost": '0',
                "balance": '0',
                "entryDate": currentDatetimeGlobal,
                "updateDate": currentDatetimeGlobal,
                "active": '1'
            }

};
    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/saveDataAction",
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            showOutput(data.d);
           // showMessage(data.responseText,"Success");
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}


// Update Action
function updateAction() {

    if (validateForm() === false)
        return;

    var jsonData = {
        "from": "SmsConfigInfo",
        "set": {
            "apiKey": $('#contentBody_txtApiKey').val(),  ////txtApiKey
            "senderKey": $('#contentBody_txtSenderKey').val(),
            "username": $("#contentBody_txtUsername").val(),
            "password": $("#contentBody_txtPassword").val(),
        },
        "where": {
            "roleId": $.trim($('#contentBody_lblHiddenSMSConfig').val()) // ddBranch
        }
    };

    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/updateDataAction",
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            showOutput(data.d);
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}


// Load Config
function loadConfig(roleId) {
    var jsonData = {
        "select": "apiKey,senderKey,username,password",
        "from": "SmsConfigInfo",
        "where": {
            "roleId": roleId
        },

        "column": "id",
        "dir": "asc"
    };

    $.ajax({
        type: "post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{'jsonStrData' : '" + JSON.stringify(jsonData) + "'}",
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        success: function (data) {

            var configData = JSON.parse(data.d);

            $('#contentBody_txtApiKey').val(configData[0].apiKey); //txtApiKey
            $('#contentBody_txtSenderKey').val(configData[0].senderKey); //txtSenderKey
            $('#contentBody_txtUsername').val(configData[0].username);
            $('#contentBody_txtPassword').val(configData[0].password);

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


// Validation

function validateForm() {

    var validate = true;
    var group = $('#contentBody_lblHiddenSMSConfig').val();  // ddBranch
    var apiKey = $('#contentBody_txtApiKey').val(); //txtApiKey
    var senderKey = $('#contentBody_txtSenderKey').val();
    var username = $("#contentBody_txtUsername").val();
    var password = $("#contentBody_txtPassword").val();

    if (group === "0") {
        validate = false;
        showMessage("Plese select a group.", "warning");
    }

    else if (username === "") {
        validate = false;
        showMessage("User name can't empty.", "warning");
    }

    else if (password === "") {
        validate = false;
        showMessage("Password can't empty.", "warning");
    }

    else if (apiKey === "") {
        validate = false;
        showMessage("Api key can't empty.", "warning");
    }

    else if (senderKey === "") {
        validate = false;
        showMessage("Sender key can't empty.", "warning");
    }

    return validate;
}


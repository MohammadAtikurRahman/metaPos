
function eventChange() {
    activeModule = "config";
    loadConfig();
}

// email save in db

$(document).on("click",
    "#btnSaveChanges",
    function () {
        if (validateFrom() === false)
            return;


        var findjsonData = {
            "select": "medium",
            "from": "EmailConfigInfo",
            "where": {
                "roleId": roleIdGlobal
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
            error: function (data) {
                console.log(data.responseText, "Error");
            },
            failure: function (data) {
                console.log(data.responseText, "Error");
            }
        });
    });

// save action

function saveAction() {
    var jsonData = {
        "from": "EmailConfigInfo",

        "values":
            {
                "medium": $('#ddlMedium').val(),
                "sender": $('#txtSender').val(),
                "apiKey": $('#txtApiKey').val(),
                "cost": $('#txtCost').val(),
                "entryDate": currentDatetimeGlobal,
                "updateDate": currentDatetimeGlobal,
                "roleId": roleIdGlobal,
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

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });

}




// update Action

function updateAction() {

    var jsonData = {
        "from": "EmailConfigInfo",
        "set": {
            "medium": $('#ddlMedium').val(),
            "sender": $('#txtSender').val(),
            "apiKey": $('#txtApiKey').val(),
            "cost": $("#txtCost").val()

        },
        "where": {
            "roleId": roleIdGlobal
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

// load Config

function loadConfig() {
    var jsonData = {
        "select": "medium,sender,apiKey,cost",
            "from": "EmailConfigInfo",
            "where": {
                    "roleId": roleIdGlobal
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

                $('#ddlMedium').val(configData[0].medium);
                $('#txtSender').val(configData[0].sender);
                $('#txtApiKey').val(configData[0].apiKey);               
                $('#txtCost').val(configData[0].cost);

            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }

        });
}

function validateFrom() {

    var validate = true;
    var medium = $('#ddlMedium').val();
    var apiKey = $('#txtApiKey').val();
    var sender = $('#txtSender').val();
    var cost = $("#txtCost").val();
    if (medium === "none") {
        validate = false;
        showMessage("Plese select a medium", "warning");
    }
    if (apiKey === "") {
        validate = false;
        showMessage("Api key can't empty!", "warning");
    }
    if (sender === "") {
        validate = false;
        showMessage("Please provide your email !", "warning");
    }
    if (!cost.match(/\d+/)) {
        validate = false;
        showMessage("Invalid cost!", "warning");
    }
    return validate;
}
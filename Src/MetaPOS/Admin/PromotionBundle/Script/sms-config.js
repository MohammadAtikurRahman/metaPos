function eventChange() {
    activeModule = "config";

    //loadBranch();
    //loadConfig();
}

// Select Group
$('#ddlGroup').on('change', function () {
    loadConfig($(this).val());
});


// Select Medium

$('#ddlMedium').on('change', function () {
    resetMedium();
    displayMediumControl();
});

$(document).ready(function () {
    pageLoad();
});


function displayMediumControl() {


    var selectedMedium = $('#ddlMedium').val();

    if (selectedMedium === "elitbuzz") {

        showMedium();

    }
    else if (selectedMedium === "infobip") {

        showMedium();

    }
    else if (selectedMedium === "modem") {

        modem();

    }
    else {

        emptyMedium();
    }
}


// Save Action

$(document).on("click",

    "#btnSaveChanges",
    function () {

        var findjsonData = {
            "select": "medium",
            "from": "SmsConfigInfo",
            "where": {
                "roleId": $('#ddlGroup').val()
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


// Save Action

function saveAction() {
    var jsonData = {
        "from": "SmsConfigInfo",

        "values":
            {
                "roleID": $('#ddlGroup').val(),
                "medium": $('#ddlMedium').val(),
                "apiKey": $('#txtApiKey').val(),
                "senderId": $('#txtSenderId').val(),
                "cost": $("#txtSmsCost").val(),
                "balance": $("#txtSmsBalance").val(),
                "username": $("#txtUsername").val(),
                "password": $("#txtPassword").val(),
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

    if (validateFrom() === false)
        return;
    var jsonData = {
        "from": "SmsConfigInfo",
        "set": {
            "medium": $('#ddlMedium').val(),
            "apiKey": $('#txtApiKey').val(),
            "senderId": $("#txtSenderId").val(),
            "cost": $("#txtSmsCost").val(),
            "balance": $("#txtSmsBalance").val(),
            "username": $("#txtUsername").val(),
            "password": $("#txtPassword").val(),
        },
        "where": {
            "roleId": $.trim($('#ddlGroup').val())
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

function loadConfig(roleId) {
    var jsonData = {
        "select": "medium,apiKey,senderId,cost,balance,username,password",
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

            $('#ddlMedium').val(configData[0].medium);
            $('#txtApiKey').val(configData[0].apiKey);
            $('#txtSenderId').val(configData[0].senderId);
            $('#txtSmsCost').val(configData[0].cost);
            $('#txtSmsBalance').val(configData[0].balance);
            $('#txtUsername').val(configData[0].username);
            $('#txtPassword').val(configData[0].password);
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }

    });
}





// Load 



// Check Medium

function showMedium() {

    $('#divMedium').show();

}

function modem() {
    $('#divMedium').hide();
    $('#showMessage').html("<h2><i>This medium is not ready yet...</i></h2>");
}

function emptyMedium() {

    $('#divMedium').hide();
    $('#showMessage').html("<h2><i>Please select a medium...</i></h2>");
}

// 

function resetMedium() {

    $('#txtSmsBalance').val("");
    $('#txtSmsCost').val("");
    $('#selectApiKey').val("");
    $('#selectSenderId').val("");
    $('#showMessage').html("");
    $("#txtUsername").html("");
    $("#txtPassword").html("");
}

// Page  Load

function pageLoad(roleId) {

    var jsonData = {
        "select": "medium,apiKey,senderId,cost,balance,username,password",
        "from": "SmsConfigInfo",
        "where": {
            "roleId": roleId
        },
        "column": "id",
        "dir": "asc"
    };


    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var formatData = JSON.parse(data.d);
            if (formatData[0].medium === "elitbuzz") {
                showMedium();
            }
            else if (formatData[0].medium === "infobip") {
                showMedium();
            }
            else if (formatData[0].medium === "modem") {
                modem();
            }

            else {
                emptyMedium();
            }

            // load data


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

function validateFrom() {

    var validate = true;
    var medium = $('#ddlMedium').val();
    var apiKey = $('#txtApiKey').val();
    var senderId = $("#txtSenderId").val();
    var cost = $("#txtSmsCost").val();
    var balance = $("#txtSmsBalance").val();
    var username = $("#txtUsername").val();
    var password = $("#txtPassword").val();


    if (medium === '0') {
        validate = false;
        showMessage("Plese select a medium", "warning");
    }

    if (senderId === "") {
        validate = false;
        showMessage("Sender id can't empty!", "warning");
    }

    //if (username === "") {
    //    validate = false;
    //    showMessage("User name can't empty!", "warning");
    //}

    //if (password === "") {
    //    validate = false;
    //    showMessage("Password can't empty!", "warning");
    //}

    //if (!cost.match(/\d+/)) {
    //    validate = false;
    //    showMessage("Invalid cost!", "warning");
    //}

    return validate;
}




function loadBranch() {
    var jsonData = {
        "select": "roleID,title",
        "from": "RoleInfo",
        "where": {
            "userRight": 'Branch',
            "active":'1'
        },
        "column": "title",
        "dir": "asc"
    };


    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $('#ddlGroup')
                   .append($("<option></option>")
                              .attr("value", 0)
                              .text("Select"));

            var valueSelect = JSON.parse(data.d);
            $.each(valueSelect, function (key, value) {
                $('#ddlGroup')
                    .append($("<option></option>")
                               .attr("value", value.roleID)
                               .text(value.title));
            });

            $('#ddlGroup option:first-child').attr("selected", "selected");
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



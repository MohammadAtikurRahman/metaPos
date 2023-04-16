
function eventChange() {

    activeModule = "config";

    loadConfig();

}


//  Save Action

$(document).on("click",
 "#btnSaveFacebookConfig",function() {
     var findjsonData = {
         "select": "accessToken",
         "from": "FacebookConfigInfo",
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
         failure: function (response) {
             console.log(response);
         },
         error: function (response) {
             console.log(response);
         }
     });
 });

function saveAction() {
    var jsonData = {
        "from": "FacebookConfigInfo",

        "values":
            {
                "pageId": $('#txtPageId').val(),
                "accessToken":$('#txtAccessToken').val(),
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

    if (validateFrom() === false)
        return;
    var jsonData = {
        "from": "FacebookConfigInfo",
        "set": {
            "pageId": $('#txtPageId').val(),
            "accessToken": $('#txtAccessToken').val()

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
        "select": "pageId,accessToken",
        "from": "FacebookConfigInfo",
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

            $('#txtPageId').val(configData[0].pageId);
            $('#txtAccessToken').val(configData[0].accessToken);           
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }

    });
}

// Form Validation

function validateFrom() {

    var validate = true;
    var pageId = $('#txtPageId').val();
    var accessToken = $('#txtAccessToken').val();
    
    if (pageId === "") {
        validate = false;
        showMessage("Plese provide your facebook page id", "warning");
    }
    if (accessToken === "") {
        validate = false;
        showMessage("Please provide access token", "warning");
    }
   
    return validate;
}


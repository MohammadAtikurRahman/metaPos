

$(function() {

    loadServiceType();

});



$('#btnSave').click(function () {

    var type = $('#ddlServiceType').val();
    var name = $('#txtServiceName').val();
    var retailPrice = $('#txtRetailPrice').val();
    var wholePrice = $('#txtWholePrice').val();

    if (type == "0") {
        showModalOutput("Please select service type!", "warning");
        return;
    }

    if (name == "") {
        showModalOutput("Please enter service name!", "warning");
        return;
    }


    if (retailPrice == "")
        retailPrice = 0;
    if (wholePrice == "")
        wholePrice = 0;
    
   var  jsonStrData = {
        "type": type,
        "name": name,
        "retailPrice": retailPrice,
        "wholePrice": wholePrice
    };

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Service.aspx/saveServiceDataAction",
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonStrData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            //Close modal
            $('#addServiceFormModal').modal('hide');

            if (data.d == true) {
                showMsgOutput(true, "save");

                displayDataTableService();
            }
            else {
                showMsgOutput(false, data.d);
            }

            // reset
            resetService();
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




$('#btnUpdate').click(function () {

    var type = $('#ddlServiceType').val();
    var name = $('#txtServiceName').val();
    var retailPrice = $('#txtRetailPrice').val();
    var wholePrice = $('#txtWholePrice').val();
    var lblId = $('#lblId').text();

    if (type == "0") {
        showModalOutput("Please select service type!", "warning");
        return;
    }

    if (name == "") {
        showModalOutput("Please enter service name!", "warning");
        return;
    }


    if (retailPrice == "")
        retailPrice = 0;
    if (wholePrice == "")
        wholePrice = 0;

    var jsonStrData = {
        "type": type,
        "name": name,
        "retailPrice": retailPrice,
        "wholePrice": wholePrice,
        "id": lblId
    };

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Service.aspx/updateServiceDataAction",
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonStrData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            //Close modal
            $('#addServiceFormModal').modal('hide');

            if (data.d == true) {
                showMsgOutput(true, "save");

                loadServiceType();
            }
            else {
                showMsgOutput(false, data.d);
            }

            // reset
            resetService();
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




// Load Service Type

function loadServiceType() {
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Service.aspx/getServiceTypeListAction",
        dataType: "json",
        data: "",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            var ddlServiceType = $('#ddlServiceType');

            ddlServiceType.empty().append('<option selected="selected" value="0">' + SelectService + '</option>');
            $.each(data.d, function (i, item) {
                ddlServiceType.append($("<option></option>").val(this['Value']).html(this['Text']));
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




function resetService() {
    $('#ddlServiceType').val("");
    $('#txtServiceName').val("");
    $('#txtRetailPrice').val("");
    $('#txtWholePrice').val("");
}



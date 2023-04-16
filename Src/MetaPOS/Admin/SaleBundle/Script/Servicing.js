
function eventChange() {

    nextServiceID();

    getCustomerList();

    loadServiceParameterFromUrl();


    $('#contentBody_txtProdName,#contentBody_txtImei,#contentBody_txtDescription,#contentBody_txtSupplier,#contentBody_txtDeliveryDate,#contentBody_txtTotalAmt,#contentBody_txtPayCost').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnSaveService").trigger("click");
            event.preventDefault();
        }
    });

}


// Get URL Parameter GO Action
function getUrlParameter(sParam) {
    var sPageUrl = decodeURIComponent(window.location.search.substring(1)),
        sUrlVariables = sPageUrl.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sUrlVariables.length; i++) {
        sParameterName = sUrlVariables[i].split('=');

        if (sParameterName[0] == sParam) {
            return sParameterName[1] == undefined ? true : sParameterName[1];
        }
    }
};

function loadServiceParameterFromUrl() {

    // get value form URL Parameter
    var getCusid = getUrlParameter("cusid");
    var getProdID = getUrlParameter("prodID");
    var getProdName = getUrlParameter("prodName");
    var getSupCompany = getUrlParameter("supCompany");
    var getImei = getUrlParameter("imei");
    var getSupID = getUrlParameter("supID");

    if (getCusid != undefined) {

        getCustomerListSelectedByCusID(getCusid);

        $("#contentBody_ddlCustomer").attr("disabled", "disabled");
        $("#contentBody_txtSupplier").attr("disabled", "disabled");

        $('#contentBody_txtProdName').val(getProdName);
        $('#contentBody_txtImei').val(getImei);
        $('#contentBody_txtSupplier').val(getSupCompany);
        $('#contentBody_lblProdId').text(getProdID);
        $('#contentBody_lblSupplier').text(getSupID);
    }

}

var iService = 0;
$('#btnSaveService').click(function () {


    $("#btnSaveService").attr("disabled", true);
    $("#contentBody_ddlCustomer").attr("disabled", true);


    var customerId = $('#contentBody_ddlCustomer').val();
    var serviceId = $('#contentBody_txtServiceId').val();
    var prodId = $('#contentBody_lblProdId').text();
    var prodName = $('#contentBody_txtProdName').val();
    var imei = $('#contentBody_txtImei').val();
    var supplier = $('#contentBody_txtSupplier').val();
    var deliveryDate = $('#contentBody_txtDeliveryDate').val();
    var description = $('#contentBody_txtDescription').val();
    var totalAmt = $('#contentBody_txtTotalAmt').val() == "" ? 0 : $('#contentBody_txtTotalAmt').val();
    var paidAmt = $('#contentBody_txtPayCost').val() == "" ? 0 : $('#contentBody_txtPayCost').val();
    var supplierId = $('#contentBody_supplierId').text();

    if (customerId == 0 || customerId == null) {
        showMessage("Please select a customer.", "Warning");
        resetService();
        return;
    }

    var serviceData = {
        customerId: customerId,
        serviceId: serviceId,
        prodId: prodId,
        prodName: prodName,
        imei: imei,
        supplier: supplier,
        deliveryDate: deliveryDate,
        description: description,
        totalAmt: totalAmt,
        paidAmt: paidAmt,
        active: "0",
        supplierId: supplierId
    };
    
    if (iService == 0) {
        
        iService++;



        $.ajax({
            url: baseUrl + "Admin/SaleBundle/View/Servicing.aspx/saveServiceInfoDataAction",
            dataType: "json",
            data: "{ 'serviceData' : '" + JSON.stringify(serviceData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                resetService();

                getCustomerList();

                showMessage("Successfully saved.", "Success");

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

});


function nextServiceID() {

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Servicing.aspx/genarateNextServiceIdAction",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#contentBody_txtServiceId').val(data.d);
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


$('#btnResetService').click(function() {
    resetService();
});

function resetService() {
    $('#contentBody_ddlCustomer').val(0);
    $('#contentBody_txtServiceId').val("");
    $('#contentBody_txtProdName').val("");
    $('#contentBody_txtDescription').val("");
    $('#contentBody_txtImei').val("");
    //$('#contentBody_txtDeliveryDate').date();
    $('#contentBody_txtTotalAmt').val("");
    $('#contentBody_txtPayCost').val("");
    $('#contentBody_txtSupplier').val("");

    $("#btnSave").attr("disabled", false);
    $("#contentBody_ddlCustomer").removeAttr("disabled");

    $("#btnSaveService").attr("disabled", false);
    $("#contentBody_ddlCustomer").attr("disabled", false);

    nextServiceID();

}
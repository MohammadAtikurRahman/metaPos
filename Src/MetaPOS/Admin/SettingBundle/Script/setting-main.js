// On load event
function eventChange() {
}


// Change event for input changes
$("input[type='radio'],input[type='text']").on("change", function () {

    var column = (this.id.split("_"))[1];
    var value = this.value;
    var branchId = $('#contentBody_lblHiddenBranchId').val();  //contentBody_ddlBranchSetting

    var subscriptionAmt = $(this).closest('.addons-price').text();
    console.log("Sub:", subscriptionAmt);

    updateSettingValue(column, value, branchId);
});


$("input[type='checkbox']").on("change", function () {
    var isChecked = $(this).prop('checked');
    var value = 1;
    var sign = "+";
    if (!isChecked) {
        value = 0;
        sign = "-";
    }

    var column = (this.id.split("_"))[1];

    console.log("column,value:", column, "/" + value);
    var branchId = $('#contentBody_lblHiddenBranchId').val();  // contentBody_ddlBranchSetting

    updateSettingValue(column, value, branchId);

    var addonsPrice = $(this).closest('.setting-section').find('.addons-price').text();

    updateSubscription(addonsPrice, sign, branchId);
});




//$("input[type='text']").on("change", function() {
//    var value = $(this).val();

//    var column = (this.id.split("_"))[1];

//    console.log("column,value:", column, "/" + value);
//    var branchId = $('#contentBody_ddlBranchSetting').val();

//    updateSettingValue(column, value, branchId);
//});


$("#contentBody_language, #contentBody_businessType").on("change", function () {
    var value = $(this).val();

    var column = (this.id.split("_"))[1];

    var branchId = $('#contentBody_lblHiddenBranchId').val();

    updateSettingValue(column, value, branchId);
    console.log("work it", column, "/" + value);
});


// Update setting value
function updateSettingValue(column, value, branchId) {

    var jsonData = {
        "column": column,
        "value": value,
        "branchId": branchId

    };

    $.ajax({
        url: baseUrl + "Admin/SettingBundle/View/Setting.aspx/updateSettingInfoAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //showMessage(data.d, "Success");
            messageTostr(data.d, "Success");
        },
        error: function (data) {
            messageTostr(data.responseText, "Error");
        },
        failure: function (data) {
            messageTostr(data.responseText, "Error");
        }
    });

}



// Update Subscription
function updateSubscription(addonsPrice, sign, branchId) {

    var jsonData = {
        "addonsPrice": addonsPrice,
        "branchId": branchId,
        "sign": sign
    };

    $.ajax({
        url: baseUrl + "Admin/SettingBundle/View/User.aspx/updateSubscriptionAction",
        data: "{'jsonData':'" + JSON.stringify(jsonData) + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log("Sub:s:", data.d);
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



$(document).ready(function () {
    $('#btn25mm,#btn50mm,#btn80mm').click(function () {
        $('#contentBody_smallPrintPaperWidth').val($(this).val());
        $('#contentBody_smallPrintPaperWidth').trigger("change");
    });
});


$(document).ready(function () {
    $('#contentBody_monthWaseSalesReport').click(function () {
        $(this).val();
    });
});

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    var height = $(".setting-section-height").height();
    height -= 70;
    $(".setting-nav-tabs").height(height);
});
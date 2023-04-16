

// date edit
$('#btnEditDate').on("click", function () {
    $('#contentBody_txtBillingDate').addClass("BillingDateEnable datepickerCSS");
    $('#contentBody_txtBillingDate').attr("disabled", false);
    $('#contentBody_txtBillingDate').removeClass("BillingDate");
    $('#btnUpdateDate').removeClass("disNone");
    $('#btnEditDate').addClass("disNone");

    $("#contentBody_txtBillingDate").datepicker({
        dateFormat: "dd-M-yy"
    });
});


$('#btnUpdateDate').on("click", function () {
    $('#contentBody_txtBillingDate').addClass("BillingDate");
    $('#contentBody_txtBillingDate').attr("disabled", true);
    $('#contentBody_txtBillingDate').removeClass("BillingDateEnable datepickerCSS");
    $('#btnUpdateDate').addClass("disNone");
    $('#btnEditDate').removeClass("disNone");
});



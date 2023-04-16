
$("#contentBody_txtSearchNameCode").on("keyup paste", function () {
    SearchCartAllItem();
});


function SearchCartAllItem() {

    var itemType = $("#contentBody_rblSearchIn input:checked").val();

    itemType = itemType.replace('(', ' ').replace(')', ' ').replace('[', ' ').replace(']', ' ');



    $("[id$=txtSearchNameCode]").autocomplete({
        source: function (request, response) {
            var serachVal = request.term;
            $.ajax({
                url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getSearchProductListAction",
                data: "{ 'search': '" + serachVal + "', 'itemType': '" + itemType + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split('<>')[0].trim() + item.split('<>')[1].trim() + ' [' + item.split('<>')[2] + ']',
                            val: item.split('<>')[2]
                        };
                    }));
                },
                error: function (data) {
                    showMessage(data.responseText, "Error");
                },
                failure: function (data) {
                    showMessage(data.responseText, "Error");
                }
            });
        },
        select: function (e, i) {
            $("[id$=hfProductDetails]").val(i.item.val);
            $("#contentBody_txtSearchNameCode").focus();
        },
        minLength: 1
    });
}


// Search Invoice or Customer
$("#contentBody_txtBillingNo").on("keyup paste", function () {

    var searchOption = "";
    if ($("#divSearchOptionPanel .toggle").hasClass("off") == true)
        searchOption = "customer";
    else
        searchOption = "invoice";

    $("[id$=txtBillingNo]").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getInvoiceByCustomerOrBill",
                data: "{ 'prefix': '" + request.term + "', 'searchOption': '" + searchOption + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split(',')[0] + '-' + item.split(',')[1] + '[' + item.split(',')[2] + ']',
                            val: item.split(',')[1]
                        };
                    }));
                },
                failure: function (responseSe) {
                    console.log(responseSe);
                    alert(responseSe);
                },
                error: function (responseSe) {
                    console.log(responseSe);
                    alert(responseSe);
                }
            });
        },
        select: function (e, i) {
            $("[id$=hfProductDetails]").val(i.item.val);
        },
        minLength: 1
    });
});




// Toggle Search Type
$("#contentBody_txtBillingNoSearch").on("focus", function () {
    if ($("#divSearchOptionPanel .toggle").hasClass("off"))
        $("#divSearchOptionPanel .toggle").removeClass("off");
});




// Quick pay button 
$('#btnQuickPay').click(function () {
    var dueChangeVal = $('#contentBody_txtGiftAmt').val();
    $('#contentBody_txtPayCash').val(Math.abs(dueChangeVal));
    $('#contentBody_txtDueChange').val("0.00");
    $('#contentBody_txtExtraDiscount').val("0.00");
});
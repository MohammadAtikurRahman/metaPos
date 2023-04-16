
$(function() {
    $('#contentBody_ddlSup').change(function() {
        var supId = $(this).val();
        console.log("supId:", supId);

        $.ajax({
            url: baseUrl + "Admin/InventoryBundle/View/StockOpt.aspx/getSupplierCommisionAction",
            dataType: "json",
            data: "{ 'supId': '" + supId + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#contentBody_txtSuplierCommission').val(data.d);
            },
            error: function (data) {
                console.log(data.responseText, "Error");
            },
            failure: function (data) {
                console.log(data.responseText, "Error");
            }
        });

    });
});
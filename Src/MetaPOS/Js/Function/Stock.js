// Click on forgot password link
$("#contentBody_txtQty").keyup(function() {
    if (parseInt($(this).val()) < 0) {
        var qty = $("#contentBody_txtQty").val();
        var bPrice = $("#contentBody_txtBPrice").val();
        var amount = parseInt(qty) * parseInt(bPrice);
        $("#contentBody_lblStockLessQtyAlert").text(amount + " will be dedecucted.");
    }
    else
        $("#contentBody_lblStockLessQtyAlert").text("");
});
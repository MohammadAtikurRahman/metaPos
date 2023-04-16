$('#btnLoadNewPopUp').click(function () {

    // Get Invoice Data
    var soldProductList = [];
    $("#contentBody_divShoppingList tr:not(:eq(0))").each(function (index) {
        var prodId = $(this).find('.lblItemId').text();

        soldProductList.push(prodId);
    }).promise()
        .done(function () {
            // suspend 
            suspendInvoice();
        });



    // Set Invoic Data
    for (var i = 0; i < soldProductList.length; i++) {
        var prodIdArray = soldProductList[i];
        AddToCartItemByProduct(prodIdArray);
    }

    $('#suspendInvoice').modal('hide');

});
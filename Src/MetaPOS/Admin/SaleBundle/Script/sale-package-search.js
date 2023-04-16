


function searchPackageOption() {
    isUpdate = true;

    var searchOption = $('#contentBody_chkSearchOption').val();
    var searchTxt = $('#contentBody_txtBillingNo').val();
    if (searchTxt == "" || searchTxt == null) {
        searchTxt = getUrlParameter("id");
    }


    if (isException(searchOption)) {
        showMessage("Search option is not valid", "Warning");
        return;
    }

    if (isException(searchTxt)) {
        showMessage("Invoice no is not valid", "Warning");
        return;
    }

    // Set form saleInfo
    var isError = searchSale(searchTxt, searchOption);

    changeCustomerUpdateInfo();


    if (isError) {
        $('#divSpecialSearch').removeClass("open");
        resetInvoice();
        return;
    }

    // add item search block 
    $('#contentBody_txtSearchNameCode').attr("disabled", true);
    $('#btnAddToCart').attr("disabled", true);


    $('#thReturn').removeClass("disNone");

    var billNo = searchTxt;
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getStockStatusDataListAction",
        data: JSON.stringify({ "billNo": billNo }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var jsonData = $.parseJSON(data.d);
            var leng = jsonData.length;

            for (var i = 0; i < leng; i++) {

                var stockJsonData = jsonData[i];
                updateCartItemData(billNo, stockJsonData.prodCode);
            }
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


    // Display
    $('#divReturnTotal').removeClass("disNone");
    $('#divReturnPaid').removeClass("disNone");

    //$('#').removeClass("disNone");
    //$('#').removeClass("disNone");

    // Assaign to another searchbox
    $('#contentBody_txtBillingNoSearch').val(searchTxt);

    // Close overly
    $('#divSpecialSearch').removeClass("open");
    $('#btnSaveInvoice').addClass("disNone");
    $('#btnUpdateInvice').removeClass("disNone");
    $('#divPaid').removeClass("disNone");
}
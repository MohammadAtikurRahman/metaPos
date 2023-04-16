

function SearchCartProductByPackage() {
}


function AddToCartItenByPackage(prodCode) {

    var obj = {};
    obj.prodCode = prodCode;

    $.ajax({
        url: baseUrl + "Admin/PackageBundle/View/Package.aspx/getPackageDataListAddToCartAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "") {
                showMessage("Package not found.", "Warning");
                return;
            }

            var jsonData = JSON.parse(data.d)[0];
            var prodCodes = jsonData.prodCode;
            $.ajax({
                url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getStockMinQtyDataAction",
                data: JSON.stringify({ prodCodes: prodCodes }),
                dataType: 'json',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (dataQty) {

                    var itemName, maxQty = 0, prodId, category, supplier, bPrice, sPrice, commission, dealerPrice, unitId;
                    var qty = 1;
                    var returnQty = 0;

                    if (JSON.stringify(jsonData) != undefined) {
                        qty = jsonData.qty;
                    }



                    maxQty = JSON.parse(dataQty.d);

                    itemName = jsonData.packageName;

                    prodId = parseInt(jsonData.Id);
                    var storeId = jsonData.storeId;
                    console.log("jsonData.:", storeId);
                    category = "";
                    supplier = "";
                    bPrice = 0;
                    sPrice = parseFloat(jsonData.price);
                    commission = 0;
                    dealerPrice = parseFloat(jsonData.dealerPrice);
                    var storeId = jsonData.storeId;
                    unitId = 0;



                    // check validation
                    if (prodId == null || prodId == "") {
                        showMessage("ProductId is not valid", "Warning");
                        return;
                    }

                    if (maxQty == null || maxQty == "" || maxQty == undefined)
                        maxQty = 0;

                    if (maxQty <= 0) {
                        $('#contentBody_txtSearchNameCode').val();
                        showMessage("Qty is not avabilable", "Warning");
                        return;
                    }


                    if (category == null || category == "" || category == undefined)
                        category = "0";
                    if (supplier == null || supplier == "" || supplier == undefined)
                        supplier = "0";
                    if (bPrice == null || bPrice == "" || bPrice == undefined)
                        bPrice = "0";
                    if (sPrice == null || sPrice == "" || sPrice == undefined)
                        sPrice = "0";
                    if (dealerPrice == null || dealerPrice == "" || dealerPrice == undefined)
                        dealerPrice = "0";

                    var isDuplicate = false;
                    $("#contentBody_divShoppingList tr").each(function (index) {
                        var cartProdCode = $(this).find('.lblProdCode').text();

                        if (prodCode == cartProdCode) {
                            isDuplicate = true;
                            AddToCartExistProduct(prodCode);
                        }
                    });

                    if (!isDuplicate) {

                        // Set Cart Data
                        var div = $("<tr class='dynamicContentUnit' id='dynamicContentUnit'/>");
                        var itemData = itemAddToShoppingCart(prodCode);
                        div.html(itemData);
                        $("#contentBody_divShoppingList").append(div);


                        // get the current row
                        var currentRow = $(div).closest("tr");
                        currentRow.find(".lblItemId").text(prodId);
                        currentRow.find(".lblProdName").text(itemName);
                        currentRow.find(".lblProdCategory").text(category);
                        currentRow.find(".lblProdSupplier").text(supplier);
                        currentRow.find(".txtUnitPrice").val(sPrice);
                        currentRow.find(".lblUnitPrice").text(sPrice);
                        currentRow.find(".lblBuyPrice").text(bPrice);
                        currentRow.find(".supCommission").text(commission + "%");
                        currentRow.find(".lblDealerPrice").text(dealerPrice);
                        currentRow.find(".ddlQty").val(qty);
                        currentRow.find(".lblUnitId").text(unitId);
                        currentRow.find(".lblTotalQty").text(maxQty);
                        currentRow.find(".txtReturn").val(returnQty);
                        currentRow.find(".lblProdCodes").text(prodCodes);
                        currentRow.find(".lblSearchType").text("salePackage");
                        currentRow.find(".lblStore").text(storeId);




                        var dealerStatusActive = $("[id*=contentBody_divCusType] input:checked").val();
                        if (dealerStatusActive == "0") {
                            currentRow.find(".txtUnitPrice").val(sPrice);
                        }
                        else {
                            currentRow.find(".txtUnitPrice").val(dealerPrice);
                            sPrice = dealerPrice;
                        }

                        var selectedQty = currentRow.find(".ddlQty").val();
                        var totalQty = parseFloat(selectedQty) - parseFloat(returnQty);
                        if (totalQty < 0)
                            totalQty = 0;

                        var subTotal = parseFloat(totalQty) * parseFloat(sPrice);
                        currentRow.find(".totalPrice").text(subTotal.toFixed(2));

                        cartTotal += parseFloat(subTotal);
                        totalNetAmt = parseFloat(cartTotal);

                        returnTotal += parseFloat(returnQty) * parseFloat(sPrice);

                        // Reset value
                        $.trim($('#contentBody_txtSearchNameCode').val(""));

                        updateCartAccountSection();
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





function updatePackageItem(billNo, prodCode, packPrice, packQty) {


    if (billNo != "0")
        cartTotal = 0;

    // saleInfo, stockInfo, stockStatusInfo, UnitInfo

    var obj = {};
    obj.billNo = billNo;
    obj.prodCode = prodCode;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getPackageDataListAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "") {
                showMessage("Product not found.", "Warning");
                return;
            }

            var getPackageData = JSON.parse(data.d);
            var saleData = getPackageData[0].saleData;
            var packageData = getPackageData[1].packageData;

            var jsonData = JSON.parse(packageData)[0];
            var prodCodes = jsonData.prodCode;
            var salePackageData = JSON.parse(saleData)[0];

            $.ajax({
                url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getStockMinQtyDataAction",
                data: JSON.stringify({ prodCodes: prodCodes }),
                dataType: 'json',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (dataQty) {

                    var itemName, maxQty = 0, prodId, category, supplier, bPrice, sPrice, commission, dealerPrice, unitId;
                    var qty = 1;
                    var returnQty = 0;

                    if (JSON.stringify(salePackageData) != undefined) {
                        qty = salePackageData.qty;
                    }

                    maxQty = JSON.parse(dataQty.d);
                    itemName = jsonData.prodName;
                    prodId = parseInt(jsonData.prodId);
                    category = "";
                    supplier = "";
                    bPrice = 0;
                    sPrice = parseFloat(jsonData.sPrice);
                    commission = 0;
                    dealerPrice = parseFloat(jsonData.dealerPrice);
                    unitId = 0;
                    returnQty = salePackageData.returnQty;

                    // check validation
                    if (prodId == null || prodId == "") {
                        showMessage("ProductId is not valid", "Warning");
                        return;
                    }

                    if (maxQty == null || maxQty == "" || maxQty == undefined)
                        maxQty = 0;

                    if (maxQty <= 0) {
                        $('#contentBody_txtSearchNameCode').val("");
                        showMessage("Qty is not avabilable", "Warning");
                        return;
                    }


                    if (category == null || category == "" || category == undefined)
                        category = "0";
                    if (supplier == null || supplier == "" || supplier == undefined)
                        supplier = "0";
                    if (bPrice == null || bPrice == "" || bPrice == undefined)
                        bPrice = "0";
                    if (sPrice == null || sPrice == "" || sPrice == undefined)
                        sPrice = "0";
                    if (dealerPrice == null || dealerPrice == "" || dealerPrice == undefined)
                        dealerPrice = "0";

                    var isDuplicate = false;
                    $("#contentBody_divShoppingList tr").each(function (index) {
                        var cartProdCode = $(this).find('.lblProdCode').text();

                        if (prodCode == cartProdCode) {
                            isDuplicate = true;
                            //AddToCartExistProduct(prodCode);
                        }
                    });

                    if (isDuplicate == true) {
                        AddToCartExistProduct(prodCode);
                    }
                    else {

                        // Set Cart Data
                        var div = $("<tr class='dynamicContentUnit' id='dynamicContentUnit'/>");
                        var itemData = itemAddToShoppingCart(prodCode);
                        div.html(itemData);
                        $("#contentBody_divShoppingList").prepend(div);


                        // get the current row
                        var currentRow = $(div).closest("tr");
                        currentRow.find(".lblItemId").text(prodId);
                        currentRow.find(".lblProdName").text(itemName);
                        currentRow.find(".lblProdCategory").text(category);
                        currentRow.find(".lblProdSupplier").text(supplier);
                        currentRow.find(".txtUnitPrice").val(sPrice);
                        currentRow.find(".lblUnitPrice").text(sPrice);
                        currentRow.find(".lblBuyPrice").text(bPrice);
                        currentRow.find(".supCommission").text(commission + "%");
                        currentRow.find(".lblDealerPrice").text(dealerPrice);
                        currentRow.find(".ddlQty").val(qty);
                        currentRow.find(".lblUnitId").text(unitId);
                        currentRow.find(".lblTotalQty").text(maxQty);
                        currentRow.find(".txtReturn").val(returnQty);
                        currentRow.find(".lblProdCodes").text(prodCodes);
                        currentRow.find(".lblSearchType").text("salePackage");

                        var dealerStatusActive = $("[id*=contentBody_divCusType] input:checked").val();
                        if (dealerStatusActive == "0") {
                            currentRow.find(".txtUnitPrice").val(sPrice);
                        }
                        else {
                            currentRow.find(".txtUnitPrice").val(dealerPrice);
                            sPrice = dealerPrice;
                        }

                        if (billNo != "0") {
                            currentRow.find(".ddlQty").attr("disabled", true);
                            currentRow.find(".txtUnitPrice").attr("disabled", true);
                            currentRow.find(".remove").attr("disabled", true);

                            currentRow.find(".txtUnitPrice").val(packPrice);
                            currentRow.find(".lblUnitPrice").text(packPrice);
                            sPrice = packPrice;

                            // qty
                            currentRow.find(".ddlQty").val(packQty);
                        }

                        var selectedQty = currentRow.find(".ddlQty").val();

                        var totalQty = parseFloat(selectedQty) - parseFloat(returnQty);
                        if (totalQty < 0)
                            totalQty = 0;

                        var subTotal = parseFloat(totalQty) * parseFloat(sPrice);
                        currentRow.find(".totalPrice").text(subTotal.toFixed(2));

                        cartTotal += parseFloat(subTotal);
                        totalNetAmt = parseFloat(cartTotal);

                        returnTotal += parseFloat(returnQty) * parseFloat(sPrice);

                        // Reset value
                        $.trim($('#contentBody_txtSearchNameCode').val(""));

                        updateCartAccountSection();
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
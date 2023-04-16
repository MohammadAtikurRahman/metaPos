


//function AddToCartItem() {
//    console.log("AddToCart Action");

//    isUpdate = false;

//    var prodItem = $.trim($('#contentBody_txtSearchNameCode').val());
//    if (prodItem == null || prodItem == '') {
//        alert("Please select a product");
//        return;
//    }

//    if (isImeiStatusOpen == true) {
//        showMessage("Select an IMEI", "Warning");
//        return;
//    }

//    var prodId = "";
//    var splitProductCodeAndName = prodItem.split('[');
//    if (splitProductCodeAndName.length > 1) {

//        prodId = $.trim(splitProductCodeAndName[splitProductCodeAndName.length - 1].slice(0, -1));
//    }
//    else {
//        prodId = prodItem;
//    }

//    var codeExist = false;

//    // add to cart using product code 
//    if (!prodItem.includes('[')) {
//        codeExist = true;

//        itemType = $("#contentBody_rblSearchIn input:checked").val();
//        if (itemType == "3") {
//            AddToCartItemByIMEI(prodItem);
//        }
//        else {
//            addToCartByBarcodeReader(prodItem);
//        }
//    }

//    // Item Type
//    if (!codeExist) {
//        //prodCode = prodCode.substring(0, prodCode.length - 1);
//        // Item Type
//        var itemType = $("#contentBody_rblSearchIn input:checked").val();
//        if (itemType == "0") {
//            AddToCartItemByProduct(prodId);
//        }
//        else if (itemType == "1") {
//            AddToCartItenByPackage(prodId);

//        } else if (itemType == "2") {
//            AddToCartItenByService(prodId);
//        }

//    }

//    //  Enable IMEI
//    if (columnAccess[0]["imei"] == "1") {
//        loadIMEIByProductCode(prodId);
//    }

//}




function AddToCartItemByProduct(prodId) {
    var obj = {};
    obj.prodId = prodId;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getProductDataListAddToCartAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "[]") {
                showMessage("Product not found.", "Warning");
                return;
            }

            var jsonData = JSON.parse(data.d)[0];
            if (jsonData == undefined)
                return;
            var objStock = {};
            objStock.prodId = prodId;


            var qty = 1;
            var returnQty = 0;


            var maxQty = parseFloat(jsonData.stockQty);

            var commission = jsonData.commission;
            var buyPrice = jsonData.bPrice;

            var itemName = jsonData.prodName;
            var category = jsonData.catName;
            var supplier = jsonData.supCompany;
            var bPrice = getBuyPriceWithoutCommission(buyPrice, commission);
            var sPrice = jsonData.sPrice;

            var dealerPrice = jsonData.dealerPrice;
            var unitId = jsonData.unitId;
            var engineNumber = jsonData.engineNumber;
            var cecishNumber = jsonData.cecishNumber;
            var imei = jsonData.imei;
            var store = jsonData.storeName;
            var location = jsonData.locationName;
            var comPrice = jsonData.comPrice;
            var offerType = jsonData.offerType;
            var offerValue = jsonData.offerValue;
            var discountType = jsonData.discountType;
            var discountValue = jsonData.discountValue;
            var unitRatio = jsonData.unitRatio;
            var fieldRecord = jsonData.fieldRecord;
            var attributeRecord = jsonData.attributeRecord;
            // check validation
            if (prodId == null || prodId == "") {
                showMessage("ProductId is not valid", "Warning");
                return;
            }

            if (maxQty == null || maxQty == "" || maxQty == undefined)
                maxQty = 0;

            if (maxQty <= 0) {
                if (columnAccess[0]["stayAfterSearchProduct"] == "0")
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
                var cartProdId = $(this).find('.lblItemId').text();

                if (prodId == cartProdId) {
                    isDuplicate = true;
                    AddToCartExistProduct(cartProdId);
                }
            });

            if (!isDuplicate) {

                // Set Cart Data
                var div = $("<tr class='dynamicContentUnit' id='dynamicContentUnit'/>");
                var itemData = itemAddToShoppingCart(prodId);
                div.html(itemData);
                //append() for assignding prepend() for dessending 
                $("#contentBody_divShoppingList").prepend(div);
                


                var attributeRecord = jsonData.attributeRecord;
                if (attributeRecord != "0") {
                    $.ajax({
                        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getAttributeNameDataAction",
                        data: JSON.stringify({ "attributeRecord": attributeRecord }),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (dataAttr) {
                            console.log("Data Attribute:", dataAttr.d);

                            if (dataAttr.d != "") {
                                variantValue = dataAttr.d;
                            }

                            var currentRow = $(div).closest("tr");
                            currentRow.find(".lblAttributeValue").text(variantValue);
                            currentRow.find(".attribute-section").removeClass('disNone');

                        }

                    });
                }

                // get the current row
                var currentRow = $(div).closest("tr");
                currentRow.find(".lblItemId").text(prodId);
                currentRow.find(".lblProdName").text(itemName);
                currentRow.find(".lblProdCategory").text(category);
                currentRow.find(".lblProdSupplier").text(supplier);
                currentRow.find(".txtUnitPrice").val(parseFloat(sPrice).toFixed(2));
                currentRow.find(".lblUnitPrice").text(parseFloat(sPrice).toFixed(2));
                currentRow.find(".lblBuyPrice").text(parseFloat(bPrice).toFixed(2));
                currentRow.find(".supCommission").text(commission + "%");
                currentRow.find(".lblDealerPrice").text(parseFloat(dealerPrice).toFixed(2));
                currentRow.find(".ddlQty").val(qty);
                currentRow.find(".lblUnitId").text(unitId);
                currentRow.find(".lblTotalQty").text(maxQty);
                currentRow.find(".txtReturn").val(returnQty);
                currentRow.find(".lblEngineNumber").text(engineNumber);
                currentRow.find(".lblcecishNumber").text(cecishNumber);
                currentRow.find(".lblImei").text(imei == "" ? 'N/A' : "");
                currentRow.find(".lblSearchType").text("product");
                var storeId = $('#storeIdGlobal').val();
                currentRow.find(".lblStore").text(storeId);
                currentRow.find(".lblStoreName").text(store);
                currentRow.find(".lblLocation").text(location);
                currentRow.find(".lblUnitValue").text(unitRatio);
                currentRow.find(".lblAttributeRecord").text(attributeRecord);
                currentRow.find(".lblFieldRecord").text(fieldRecord);


                if ((parseFloat(comPrice) > 0) && (columnAccess[0]["displayComPrice"] == "1")) {
                    currentRow.find(".lblComPricelabel").removeClass('disNone');
                    currentRow.find(".lblComPrice").text(parseFloat(comPrice).toFixed(2));
                }

                if (columnAccess[0]["offer"] == "1") {
                    if (offerType == "0")
                        offerType = "qty";
                    else if (offerType == "1")
                        offerType = "amount";

                    if (discountType == "0")
                        discountType = "qty";
                    else if (discountType == "1")
                        discountType = "amount";
                    else if (discountType == "2")
                        discountType = "point";

                    currentRow.find(".lblFree").text(0);
                    currentRow.find(".lblOfferType").text(offerType);
                    currentRow.find(".lblOfferValue").text(offerValue);
                    currentRow.find(".lblDiscountType").text(discountType);
                    currentRow.find(".lblDiscountValue").text(discountValue);

                    //
                    if (offerType == null) {
                        currentRow.find('.offerInfo').addClass('disNone');
                    }
                }


                if (columnAccess[0]["imei"] == "1") {
                    currentRow.find(".txtReturn").attr("disabled", true);
                }

                var dealerStatusActive = $("[id*=contentBody_divCusType] input:checked").val();
                var price = 0;
                if (dealerStatusActive == "0") {
                    currentRow.find(".txtUnitPrice").val(sPrice);
                    price = sPrice;
                }
                else {
                    currentRow.find(".txtUnitPrice").val(parseFloat(dealerPrice).toFixed(2));
                    price = dealerPrice;
                }

                if (columnAccess[0]["displayVariant"] == "1") {

                    var variantValue = getVariantNameForDisplay(attributeRecord);
                    currentRow.find(".lblVariant").text(variantValue);
                }

                // Commission Price Set
                getSupplierCommionValue(div);

                var selectedQty = currentRow.find(".ddlQty").val();
                var totalQty = parseFloat(selectedQty) - parseFloat(returnQty);
                if (totalQty < 0)
                    totalQty = 0;

                var subTotal = parseFloat(totalQty) * parseFloat(price);
                currentRow.find(".totalPrice").text(parseFloat(subTotal).toFixed(2));

                cartTotal += parseFloat(subTotal);
                totalNetAmt = parseFloat(cartTotal);


                returnTotal += parseFloat(returnQty) * parseFloat(price);

                // Reset value
                if (columnAccess[0]["stayAfterSearchProduct"] == "0")
                    $('#contentBody_txtSearchNameCode').val("");

                updateCartAccountSection();
            }

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }

    });


}


function getBuyPriceWithoutCommission(buyPrice, commission) {

    if (buyPrice == null || buyPrice == "" || buyPrice == undefined)
        buyPrice = "0";

    if (commission == null || commission == "" || commission == undefined)
        commission = "0";

    var commissionAmt = (buyPrice * commission) / 100;

    return buyPrice - commissionAmt;
}


var codeExist = false;
function addToCartByBarcodeReader(prodCode) {
    barcode = "";
    bacodeCounter = 0;

    //check product is exist

    //cartTotal = 0;
    AddToCartExistProduct(prodCode);


    if (!codeExist) {
        var prodId = getProductIdByProductCode(prodCode);
        AddToCartItemByProduct(prodId);
    }
    else {
        codeExist = false;
    }
}




function AddToCartExistProduct(prodId) {

    $("#contentBody_divShoppingList tr").each(function (index) {

        var cartProdId = $(this).find('.lblItemId').text();
        if (prodId == cartProdId) {
            codeExist = true;
            var currentCartQty = $(this).find(".ddlQty").val();
            if (currentCartQty == undefined || currentCartQty == "") {
                currentCartQty = 0;
            }
            if (columnAccess[0]["isUnit"] == "1") {
                $(this).find(".ddlQty").val(parseFloat(currentCartQty) + 1);
            }
            else {
                $(this).find(".ddlQty").val(parseInt(currentCartQty) + 1);
            }

            changeItemBoxData(this);

            if (columnAccess[0]["stayAfterSearchProduct"] == "0")
                $('#contentBody_txtSearchNameCode').val("");
        }

    });
}




function itemAddToShoppingCart(prodCode) {
    // check cart empty
    iCounter++;

    var content = '<td class="disNone"><label class="lblStockStatusId">0</label> </td>';
    content += '<td class="disNone"><label class="lblsaleId"></label>0</td>';
    content += '<td class="disNone"><label class="lblItemId"></label> </td>';
    content += '<td class="disNone"><label class="lblProdCode cart-code">' + prodCode + '</label></td>';
    content += '<td><label class="cart-serial">' + parseInt(iCounter) + '</label> </td>';

    content += '<td>';

    content += '<label class="lblProdName">' + "XXXXXXXXX" + '</label><div class="description">';

    if (columnAccess[0]["offer"] == "1") {
        content += '<span class="offerInfo"><p class="offer">Offer: <label class="lblFree">' + "XXXXXXXX" + '</label></p>';
        content += '<p class="offerType">Offer Type: <label class="lblOfferType">' + "XXXXXXXX" + '</label></p>';
        content += '<p class="offerValue">Offer Value: <label class="lblOfferValue">' + "XXXXXXXX" + '</label></p>';
        content += '<p class="discountType">Discount Type: <label class="lblDiscountType">' + "XXXXXXXX" + '</label></p>';
        content += '<p class="discountValue">Discount Value: <label class="lblDiscountValue">' + "XXXXXXXX" + '</label></p> </span>';

    }

    content += '<p class="supplier-section disNone">Store: <label class="lblStoreName">' + "XXXXXXXX" + '</label></p>';
    content += '<p class="supplier-section disNone">Store: <label class="lblStore">' + "XXXXXXXX" + '</label></p>';

    if (isDisplayLocation == "1") {
        content += '<p class="supplier-section">Location: <label class="lblLocation">' + "XXXXXXXX" + '</label></p>';
    }

    content += '<p class="attribute-section disNone">Attributes: <label class="lblAttributeValue">' + "XXXXXXXX" + '</label></p>';
    
    if (isCartCategory == "1") {
        content += '<p class="category-section">Category: <label class="lblProdCategory">' + "XXXXXXXX" + '</label></p>';
    }

    if (isCartSuppilier == "1") {
        content += '<p class="supplier-section">Supplier: <label class="lblProdSupplier">' + "XXXXXXXX" + '</label></p>';
    }

    if (columnAccess[0]["displayCartTotalQty"] == "1") {
        content += '<p class="total-Qty-section">Total Qty: <label class="lblTotalQty"></label></p>';
    }
    else {
        content += '<p class="disNone"><label class="lblTotalQty"></label></p>';
    }


    var buyPriceClass = "";
    if (columnAccess[0]["displayCartBuyPrice"] == "0") {
        buyPriceClass = " disNone";
    }
    content += '<p class="' + buyPriceClass + '">Buy Price: <label class="lblBuyPrice "/></p>';

    if (columnAccess[0]["displayCartWholesalePrice"] == "1") {
        content += '<p>Wholesale Price: <label class="lblDealerPrice"/></p>';
    }



    if (columnAccess[0]["displayCartRetailPrice"] == "1") {
        content += '<p>Retail Price: <label class="lblUnitPrice"></label></p>';
    }

    if (columnAccess[0]["displayComPrice"] == "1") {
        content += '<p class="lblComPricelabel disNone">Company Price: <label class="lblComPrice"/></p>';
    }

    if (isSupplierCommission == "1") {
        content += '<p class="commission-section">Commission: <label class="supCommission"></label></p>';
        content += '<p class="disNone"><label class="lblSupCommissionAmt cart-price"></label></p>';
    }

    if (isEngineNumber == "1") {
        content += '<p class="engine-section">Engine Number: <label class="lblEngineNumber"></label></p>';
    }

    if (isCecishNumber == "1") {
        content += '<p class="ceceish-section">Ceceish Number: <label class="lblcecishNumber"></label></p>';
    }
    if (columnAccess[0]["isDisplaySerialNo"] == "1") {
        if (isUpdate) {
            content += '<p>Serial No: <label class="lblSerialNo"></label></p>';
        }
    }

    content += '<p class="disNone">Type: <label class="lblSearchType"></label></p>';



    if (columnAccess[0]["imei"] == "1") {
        content += '<p class="imei-section"> Imei: <label class="lblImei"></label> </P>';
        content += '<label class="lblRemoveImei disNone" text=""></label>';
    }

    if (columnAccess[0]["displayVariant"] == "1") {
        content += '<p class="imei-section disNone"> Variant: <label class="lblVariant"></label> </P>';
    }
    content += '</div></td>';

    if (editAccess == "False") {
        content += '<td><input type="text" class="txtUnitPrice" disabled="disabled" onkeypress="javascript:return isNumber(event)" onchange="changeItemBoxData(this)" tabindex="2"/></td>';
    }
    else {
        content += '<td><input type="text" class="txtUnitPrice" onkeypress="javascript:return isNumber(event)" onchange="changeItemBoxData(this)" tabindex="2"/></td>';
    }
    content += '<td><spam class="cartqty unit-txtbox"><input type="text" class="ddlQty" onkeypress="javascript:return isNumber(event)"  onkeyup="changeItemBoxData(this)"  tabindex="2"/></spam></td>';



    if (columnAccess[0]["isDisplaySerialNo"] == "1") {
       
        if (isUpdate)
        {
            $('#thSerialNo').addClass('disNone');
            content += '<td class="disNone"><label class="serial"><input type="text" class="serialNo"  tabindex="2"/></label></td>';
        }
        if (isUpdate == false) {
            $('#thSerialNo').removeClass('disNone');
            content += '<td><label class="serial"><input type="text" class="serialNo"  tabindex="2"/></label></td>';
        }
        
    }


    if (isUpdate) {
        $('#thReturn').removeClass('disNone');
        content += '<td><spam class="cartqty unit-txtbox"><input type="text" class="txtReturn"  onkeyup="changeReturn(this)"/></spam></td>';
    }






    content += '<td><label class="totalPrice"></label></td>';
    content += '<td class="remove-area"><input type="button" value="X" class="remove label-danger" onclick="removeItem(' + iCounter + ')" /></td>';
    content += '<td class="disNone"><label class="lblUnitId"/></td>';
    content += '<td class="disNone"><label class="lblProdCodes"/></td>';
    content += '<td class="disNone"><label class="lblUnitValue"/></td>';
    content += '<td class="disNone"><label class="lblAttributeRecord"/></td>';
    content += '<td class="disNone"><label class="lblFieldRecord"/></td>';

    return content;
}




function updateCartItemData(billNo, prodCode) {
    if (billNo != "0")
        cartTotal = 0;  

    // saleInfo, stockInfo, stockStatusInfo, UnitInfo

    var obj = {};
    obj.billNo = billNo;
    obj.prodCode = prodCode;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getSaleDataDataListAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d == "") {
                showMessage("Product not found.", "Warning");
                return;
            }

            var jsonData = JSON.parse(data.d);
            var saleData = jsonData[0].saleData;
            var stockStatusData = jsonData[1].stockStatusData;
            var unitData = jsonData[2].unitData;
            var stockData = jsonData[3].stockData;


            var itemName, maxQty = 0, storeId, prodId, category, supplier, bPrice, sPrice, commission, dealerPrice, unitId, engineNumber, cecishNumber,serialNo, imei;
            var qty = 1;
            var returnQty = 0;


            if (saleData.length > 2) {
                saleData = JSON.parse(jsonData[0].saleData)[0];
                qty = saleData.qty;
                returnQty = saleData.returnQty;
                var strImei = saleData.imei;

                // select customer
                getCustomerListSelectedByCusID(saleData.cusID);


                additionalDue = saleData.additionalDue;
                if (additionalDue < 0)
                    additionalDue = 0;

                // get current invoice due becase of update invoice when due adjustment on
                currentInvoiceDue = saleData.giftAmt;

            }


            stockData = JSON.parse(jsonData[3].stockData)[0];
            if (stockData != "" && stockStatusData == "") {
                qty = 1;
                jsonData = stockData;
                // get data
            }

            if (stockStatusData != "") {

                stockStatusData = JSON.parse(jsonData[1].stockStatusData)[0];

                jsonData = stockStatusData;

            }
            if (jsonData == undefined)
                return;

            itemName = jsonData.prodName;
            maxQty += parseFloat(jsonData.stockQty);
            prodId = parseInt(jsonData.prodId);
            category = jsonData.catName.toString();
            supplier = jsonData.supcompany;
            bPrice = jsonData.bPrice.toString();
            sPrice = jsonData.sPrice.toString();
            commission = jsonData.commission.toString();
            dealerPrice = jsonData.dealerPrice.toString();
            unitId = jsonData.unitId.toString();
            engineNumber = jsonData.engineNumber;
            cecishNumber = jsonData.cecishNumber;
            var unitRatio = jsonData.unitRatio;



            var jsonStockData = stockData;
            var storeName = jsonStockData.warehouseName;
            var locationName = jsonStockData.locationName;

            storeId = jsonData.storeId;


            var imeiList = "";
            // split IMEI as a single value
            if (strImei != "" && strImei != null) {
                var singImei = strImei.split(',');

                // make list for html view
                imeiList = "<ul id='ddlImei'>";
                for (var i = 0; i < singImei.length - 1; i++) {
                    imeiList += '<li>' + singImei[i] + ' <span class="btnCloseImei" imeiVal="' + singImei[i] + '">×</span></li>';
                };
                imeiList += "</ul>";

            }

            // check validation
            if (prodId == null || prodId == "") {
                showMessage("ProductId is not valid", "Warning");
                return;
            }

            if (maxQty == null || maxQty == "" || maxQty == undefined)
                maxQty = 0;

            if ((maxQty <= 0) && (billNo == "0")) {
                if (columnAccess[0]["stayAfterSearchProduct"] == "0")
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
                currentRow.find(".lblDealerPrice").text(stockData.dealerPrice);
                currentRow.find(".ddlQty").val(qty);
                currentRow.find(".lblUnitId").text(unitId);
                currentRow.find(".lblUnitValue").text(unitRatio);
                currentRow.find(".lblTotalQty").text(maxQty);
                currentRow.find(".txtReturn").val(returnQty);
                currentRow.find(".lblEngineNumber").text(engineNumber);
                currentRow.find(".lblcecishNumber").text(cecishNumber);
                currentRow.find(".lblImei").html(imeiList == "" ? 'N/A' : imeiList);
                currentRow.find(".lblSearchType").text("product");
                currentRow.find(".lblStore").text(storeId);
                currentRow.find(".lblStoreName").text(storeName);
                currentRow.find(".lblLocation").text(locationName);
                


                if (billNo != "0") {
                    currentRow.find(".ddlQty").attr("disabled", true);
                    currentRow.find(".txtUnitPrice").attr("disabled", true);
                    currentRow.find(".remove").addClass("disNone");
                }



                var dealerStatusActive = $("[id*=contentBody_divCusType] input:checked").val();
                var price = 0;
                if (dealerStatusActive == "0") {
                    currentRow.find(".txtUnitPrice").val(sPrice);
                    price = sPrice;
                }
                else {
                    currentRow.find(".txtUnitPrice").val(stockData.dealerPrice);
                    price = dealerPrice;
                }

                // offer
                if (columnAccess[0]["offer"] == "1") {
                    var offer = jsonData.offer;
                    var offerType = jsonData.offerType;
                    currentRow.find(".lblFree").text(offer);
                    currentRow.find(".lblOfferType").text(offerType);

                    currentRow.find(".offerValue").addClass("disNone");
                    currentRow.find(".discountType").addClass("disNone");
                    currentRow.find(".discountValue").addClass("disNone");

                    if (parseFloat(offer) > 0) {

                        currentRow.find(".txtReturn").addClass("disNone");
                    }

                }


                // Commission Price Set
                getSupplierCommionValue(div);

                var selectedQty = currentRow.find(".ddlQty").val();
                var totalQty = parseFloat(selectedQty) - parseFloat(returnQty);
                if (totalQty < 0)
                    totalQty = 0;

                var subTotal = parseFloat(totalQty) * parseFloat(price);
                currentRow.find(".totalPrice").text(parseFloat(subTotal).toFixed(2));

                cartTotal += parseFloat(subTotal);
                totalNetAmt = parseFloat(cartTotal);


                returnTotal += parseFloat(returnQty) * parseFloat(price);

                // Reset value
                if (columnAccess[0]["stayAfterSearchProduct"] == "0")
                    $.trim($('#contentBody_txtSearchNameCode').val(""));

                updateCartAccountSection();
            }

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }

    });

}




function removeItem(id) {
    // Item Remove form cart
    $("body").on("click", ".remove", function () {
        var lblsaleId = $(this).closest("tr").find('.lblsaleId').text();
        var lblStockStatusId = $(this).closest("tr").find('.lblStockStatusId').text();
        var prodCode = $(this).closest("tr").find('.lblProdCode').text();

        // add to variable 
        deleteCartProductSaleId += lblsaleId + ";";
        deleteCartProductSotckStatusId += lblStockStatusId + ";";

        $(this).closest("tr").remove();

        changeItemBoxData(this);

        updateCartAccountSection();

        // remove product imei
        for (var key in imeiListShopCart) {
            if (imeiListShopCart[key][prodCode] == imeiListShopCart[parseInt(id) - 1][prodCode]) {
                imeiListShopCart.splice(key);
            }
        }

        isImeiStatusOpen = false;
        $('#contentBody_divFieldControl').addClass("disNone");

    });

}




function getSupplierCommionValue(div) {
    var currentRow = $(div).closest("tr");

    $("#contentBody_divShoppingList tr").each(function (index) {
        var dealerPrice = $(this).find('.lblDealerPrice').text() == "" ? 0 : $(this).find('.lblDealerPrice').text();
        var buyPrice = $(this).find('.lblBuyPrice').text() == "" ? 0 : $(this).find('.lblBuyPrice').text();

        var commission = $(this).find('.supCommission').text() == "" ? 0 : $(this).find('.supCommission').text();
        var totalCommission = (parseFloat(dealerPrice) * parseFloat(commission)) / 100;

        if (commission <= 0) {
            currentRow.find(".lblSupCommissionAmt").text(parseFloat(buyPrice));

        }
        else {
            currentRow.find(".lblSupCommissionAmt").text(parseFloat(buyPrice) - parseFloat(totalCommission).toString());
        }

    });
}




function changeReturn() {
    returnTotal = 0, cartTotal = 0;
    $("#contentBody_divShoppingList tr").each(function (index) {

        var unitPrice = $(this).find(".txtUnitPrice").val() == "" ? 0 : $(this).find(".txtUnitPrice").val();
        var soldQty = $(this).find(".ddlQty").val() == "" ? 0 : $(this).find(".ddlQty").val();
        var returnQty = $(this).find(".txtReturn").val() == "" ? 0 : $(this).find(".txtReturn").val();
        var prevousDue = $('#contentBody_txtPreviousDue').val() == "" ? 0 : $('#contentBody_txtPreviousDue').val();

        if (unitPrice == undefined) {
            unitPrice = 0;
        }

        if (soldQty == undefined) {
            soldQty = 0;
        }

        if (returnQty == undefined) {
            returnQty = 0;
        }
        var totalQty = parseFloat(soldQty) - parseFloat(returnQty);
        if (parseFloat(soldQty) < parseFloat(returnQty)) {
            showMessage("You can not return more than sold qty", "Warning");
            $(this).find(".txtReturn").val("0.00");
        }

        var unitRatio = $(this).find(".lblUnitValue").text();
        if (unitRatio == "")
            unitRatio = "1";

        var inputQty = returnQty.toString();
        var piece = 0;
        if (inputQty.indexOf(".") > -1) {
            piece = inputQty.split('.')[1];
        }

        if (parseFloat(piece) >= parseFloat(unitRatio)) {
            showMessage("You can not input more than unit ratio", "Warning");
            $(this).find(".txtReturn").val(0);
            return;
        }

        returnTotal += parseFloat(unitPrice) * parseFloat(returnQty);
        var subTotal = parseFloat(unitPrice) * parseFloat(totalQty);

        //
        var discAmtReturn = productReturnDiscount();
        var returnAmt = returnTotal - discAmtReturn;

        $(this).find(".totalPrice").text(subTotal.toFixed(2));
        $('#contentBody_txtReturnTotal').val(returnAmt.toFixed(2));


        cartTotal += parseFloat(subTotal);

        $('#divReturnTotal').removeClass("disNone");
        $('#divReturnPaid').removeClass("disNone");

    });

    // rest paycash
    $('#contentBody_txtPayCash').val(" ");

    updateCartAccountSection();

}




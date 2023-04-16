




function AddToCartItenByService(prodCode) {

    var obj = {};
    obj.prodCode = prodCode;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Service.aspx/getServiceDataListAddToCartAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "") {
                showMessage("Product not found.", "Warning");
                return;
            }


            var serviceData = JSON.parse(data.d);

            // Set Cart Data
            var div = $("<tr class='dynamicContentUnit' id='dynamicContentUnit'/>");
            var itemData = itemAddToShoppingCart(prodCode);
            div.html(itemData);
            $("#contentBody_divShoppingList").prepend(div);

            sPrice = serviceData[0].retailPrice;


            // get the current row
            var currentRow = $(div).closest("tr");
            currentRow.find(".lblItemId").text(serviceData[0].Id);
            currentRow.find(".lblProdName").text(serviceData[0].name);
            currentRow.find(".lblProdCategory").text();
            currentRow.find(".lblProdSupplier").text();
            currentRow.find(".txtUnitPrice").val(serviceData[0].name);
            currentRow.find(".lblUnitPrice").text(serviceData[0].retailPrice);
            currentRow.find(".lblBuyPrice").text(0);
            currentRow.find(".supCommission").text();
            currentRow.find(".lblDealerPrice").text(serviceData[0].wholePrice);
            currentRow.find(".ddlQty").val(1);
            currentRow.find(".lblUnitId").text();
            currentRow.find(".lblTotalQty").text();
            currentRow.find(".txtReturn").val();
            currentRow.find(".lblProdCodes").text();
            currentRow.find(".lblSearchType").text("service");

            currentRow.find(".supplier-section").hide();
            currentRow.find(".category-section").hide();
            currentRow.find(".engine-section").hide();
            currentRow.find(".ceceish-section").hide();
            currentRow.find(".total-Qty-section").hide();
            currentRow.find(".imei-section").hide();
            currentRow.find(".commission-section").hide();

            

            var dealerStatusActive = $("[id*=contentBody_divCusType] input:checked").val();
            if (dealerStatusActive == "0") {
                currentRow.find(".txtUnitPrice").val(serviceData[0].retailPrice);
            }
            else {
                currentRow.find(".txtUnitPrice").val(serviceData[0].wholePrice);
                sPrice = serviceData[0].wholePrice;
            }

            var totalQty = currentRow.find(".ddlQty").val();


            var subTotal = parseFloat(totalQty) * parseFloat(sPrice);
            currentRow.find(".totalPrice").text(subTotal.toFixed(2));

            cartTotal += parseFloat(subTotal);
            totalNetAmt = parseFloat(cartTotal);


            // Reset value
            $.trim($('#contentBody_txtSearchNameCode').val(""));

            updateCartAccountSection();
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





function searchCartItemByService(billNo, Id) {
    
    var jsonString = {};
    jsonString.billNo = billNo;
    jsonString.Id = Id;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Service.aspx/searchServiceDataListAddToCartAction",
        data: JSON.stringify(jsonString),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {


            if (data.d == "") {
                showMessage("Product not found.", "Warning");
                return;
            }

            var jsonDataService = JSON.parse(data.d);





            var itemName = jsonDataService[0].prodName;
            var qty = parseFloat(jsonDataService[0].qty);
            var prodId = parseInt(jsonDataService[0].prodID);
            var prodCode = jsonDataService[0].prodID;
            var sPrice = jsonDataService[0].sPrice;
            var dealerPrice = jsonDataService[0].dealerPrice;
            var searchType = jsonDataService[0].searchType;
            returnQty = jsonDataService[0].returnQty;
            
            if (returnQty == null || returnQty == undefined)
                returnQty = 0;

            
            // check validation
            if (prodId == null || prodId == "") {
                showMessage("ProductId is not valid", "Warning");
                return;
            }

            var isDuplicate = false;
            $("#contentBody_divShoppingList tr").each(function (index) {

                var cartProdCode = $(this).find('.lblProdCode').text();

                if (prodCode == cartProdCode) {
                    isDuplicate = true;
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
                currentRow.find(".txtUnitPrice").val(sPrice);
                currentRow.find(".lblUnitPrice").text(sPrice);
                currentRow.find(".lblDealerPrice").text(dealerPrice);
                currentRow.find(".ddlQty").val(qty);
                currentRow.find(".txtReturn").val(returnQty);
                currentRow.find(".lblSearchType").text(searchType);
                currentRow.find('.lblProdCode').text(prodId);

                currentRow.find(".supplier-section").hide();
                currentRow.find(".category-section").hide();
                currentRow.find(".engine-section").hide();
                currentRow.find(".ceceish-section").hide();
                currentRow.find(".total-Qty-section").hide();
                currentRow.find(".imei-section").hide();
                currentRow.find(".commission-section").hide();

                // disable 
                currentRow.find(".ddlQty").attr("disabled", true);
                currentRow.find(".txtUnitPrice").attr("disabled", true);
                currentRow.find(".remove").addClass("disNone");

                


                var dealerStatusActive = $("[id*=contentBody_divCusType] input:checked").val();
                var price = 0;
                if (dealerStatusActive == "0") {
                    currentRow.find(".txtUnitPrice").val(sPrice);
                    price = sPrice;
                }
                else {
                    currentRow.find(".txtUnitPrice").val(dealerPrice);
                    price = dealerPrice;
                }

                var selectedQty = currentRow.find(".ddlQty").val();

                var totalQty = parseFloat(selectedQty) - parseFloat(returnQty);
                if (totalQty < 0)
                    totalQty = 0;

                var subTotal = parseFloat(totalQty) * parseFloat(price);
                currentRow.find(".totalPrice").text(subTotal.toFixed(2));

                cartTotal += parseFloat(subTotal);
                totalNetAmt = parseFloat(cartTotal);


                returnTotal += parseFloat(returnQty) * parseFloat(price);
                

                updateAccount();
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

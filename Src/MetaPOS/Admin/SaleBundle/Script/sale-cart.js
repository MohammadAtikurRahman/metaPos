


function updateCartProduct(billNo) {
    var rowIndex = 0;
    var unitMagerment = columnAccess[0]["isUnit"];

    totalNetAmt = cartTotal = 0;

    var productIdList = [];
    $("#contentBody_divShoppingList tr").each(function (index) {//.not(":first")
        var prodId = $(this).find('.lblItemId').text();

        if (!productIdList.indexOf(prodId)) {
            productIdList.push(prodId);
        }
    });

    // push billNo
    productIdList.push(billNo);



    $("#contentBody_divShoppingList tr").each(function (index) {

        rowIndex++;

        var qty = $(this).find('.ddlQty').val();

        var uPrice = $(this).find('.txtUnitPrice').val();
        if (uPrice == null || uPrice == "")
            uPrice = 0;
        if (qty == "") {
            //$(this).find('.ddlQty').val("1");
            qty = "1";
        }

        var qtyOnly = 0;
        var pieceTotalAmt = 0;
        if (qty.indexOf('.') > 0) {
            qtyOnly = qty.split('.')[0];

            var unitRatio = $(this).find(".lblUnitValue").text();
            var unitPrice = $(this).find(".txtUnitPrice").val();

            pieceTotalAmt = getUnitPrice(qty, unitPrice, unitRatio);
        }
        else {
            qtyOnly = qty;
        }

        subTotal = (parseFloat(uPrice) * parseFloat(qtyOnly));
        subTotal += pieceTotalAmt;
        $(this).find('.totalPrice').text(subTotal.toFixed(2));

        cartTotal += parseFloat(subTotal);


        updateCartAccountSection();

    });
}




function changeItemBoxData() {
    cartTotal = totalNetAmt = 0;
    var billNo = $('#contentBody_lblBillingNo').text();

    $("#contentBody_divShoppingList tr").each(function (index) {



        var maxQty = $(this).find(".lblTotalQty").text();
        var inputQty = $(this).find(".ddlQty").val();
        if (inputQty == "" || inputQty == null) {
            //$(this).find(".ddlQty").val("1");
        }

        // except service
        var searchType = $(this).find(".lblSearchType").text();
        if (searchType == "service") {
            return;
        }





        if (maxQty == "" || maxQty == undefined)
            maxQty = 0;
        if (inputQty == "" || inputQty == undefined)
            inputQty = 0;
        //console.log("maxQty:", maxQty, "//" + inputQty);
        if (parseFloat(maxQty) < parseFloat(inputQty)) {
            showMessage("You can not more than max qty", "Warning");
            $(this).find(".ddlQty").val(0);
        }

        // Check unit
        if (columnAccess[0]["isUnit"] == "1") {
            var piece = 0;
            inputQty = inputQty.toString();
            if (inputQty.indexOf(".") > -1) {
                piece = inputQty.split('.')[1];
            }

            var unitRatio = $(this).find(".lblUnitValue").text();
            if (unitRatio == "")
                unitRatio = "1";
            if (parseFloat(piece) >= parseFloat(unitRatio)) {
                showMessage("You can not input more than unit ratio", "Warning");
                $(this).find(".ddlQty").val(0);
                return;
            }
        }
        else {
            inputQty = inputQty.toString();
            if (inputQty.indexOf(".") > -1) {
                showMessage("Unit measurement is not active", "Warning");
                $(this).find(".ddlQty").val(0);
                return;
            }
        }

        var unitPrice = $(this).find(".txtUnitPrice").val();
        var commissionAmt = $(this).find(".lblSupCommissionAmt").text();
        var buyPrice = $(this).find(".lblBuyPrice").text();

        if (buyPrice == "" || buyPrice == undefined)
            buyPrice = 0;

        if (unitPrice == "" || unitPrice == undefined)
            unitPrice = 0;

        if (commissionAmt == "" || commissionAmt == undefined)
            commissionAmt = 0;


        if (commissionAmt == 0) {
            commissionAmt = $(this).find(".lblBuyPrice").text();
        }

        if (parseFloat(unitPrice) < parseFloat(buyPrice)) {
            showMessage("You can not pay less then unit price", "Warning");
            $(this).find(".txtUnitPrice").val(parseFloat(buyPrice) + 0.01);
        }

        var qtyOnly = 0;
        var pieceTotalAmt = 0;
        if (inputQty.indexOf('.') > 0) {
            qtyOnly = inputQty.split('.')[0];

            unitRatio = $(this).find(".lblUnitValue").text();
            unitPrice = $(this).find(".txtUnitPrice").val();
            pieceTotalAmt = getUnitPrice(inputQty, unitPrice, unitRatio);
        }
        else {
            qtyOnly = inputQty;
        }

        var subTotal = parseFloat(unitPrice) * parseFloat(qtyOnly);
        subTotal += pieceTotalAmt;

        $(this).find(".totalPrice").text(subTotal.toFixed(2));
        $('#contentBody_txtReturnTotal').val(returnTotal.toFixed(2));

        cartTotal += parseFloat(subTotal);
  
        // Offer 
        if (columnAccess[0]["offer"] == "1")
            getValidOffer(this);

        updateCartProduct(billNo);

    });


}




function getUnitPrice(inputQty, unitPrice, unitRatio) {
    var pieceOnly = 0;
    var pieceTotalAmt = 0;
    if (inputQty.indexOf('.') > 0) {
        pieceOnly = inputQty.split('.')[1];

        var piecePrice = unitPrice / unitRatio;
        pieceTotalAmt = piecePrice * pieceOnly;
    }

    return pieceTotalAmt;
}

//

function copyText(evt, val) {
    $("#contentBody_txtBarcodeScaner").value = val.value + String.fromCharCode(evt.which);
}




function confirmGiftAmount() {
    var giftAmt = parseFloat(document.getElementById("<% =txtGiftAmt.ClientID %>").value).toFixed(2);
    if (parseFloat(giftAmt) > 0) {
        if (confirm("Allow Due : " + giftAmt + " Tk. ?")) {
            return true;
        }
        else
            return false;
    }
    return true;
}



function getStockCurrentQty(prodId) {

    var jsonData = "";
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getStockCurrentQtyAction",
        data: '{prodId:"' + prodId + '"}',
        type: "POST",
        dataType: 'json',
        contentType: 'application/json;charset-utf=8',
        success: function (data) {
            jsonData = $.parseJSON(data.d);
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

    return jsonData;
}


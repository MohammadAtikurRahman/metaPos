


function getValidOffer(div) {

    var prodId = $(div).find(".lblItemId").text();
    var totalAdditionalDisc = 0;

    if (prodId == "")
        return;

    var jsonData = {
        "prodId": prodId
    };

    $.ajax({
        url: baseUrl + "Admin/PromotionBundle/View/Offer.aspx/getValidOfferAction",
        data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var jsonData = JSON.parse(data.d)[0];

            if (jsonData == undefined)
                return;

            var offerType = jsonData.offerType;
            var offerValue = jsonData.offerValue;
            var discountType = jsonData.discountType;
            var discountValue = jsonData.discountValue;
            var offerEnd = jsonData.offerEnd;


            if (offerType == "0") {
                // Offer type qty

                var qty = $(div).find(".ddlQty").val() == "" ? "0" : $(div).find(".ddlQty").val();
                var baseQty = parseFloat(qty) / parseFloat(offerValue);

                var totalPiece = 0;
                if (discountType == "0") {
                    // qty
                    var freeQty = parseInt(baseQty) * parseInt(discountValue);

                    discountValue = discountValue.toString();
                    if (discountValue.indexOf('.') > 0) {

                        var piece = discountValue.split('.')[1];
                        totalPiece = parseInt(baseQty) * parseInt(piece);

                        var unitRatio = $(div).find(".lblUnitValue").text();

                        if (parseFloat(totalPiece) >= parseFloat(unitRatio)) {
                            freeQty += parseFloat(totalPiece) / parseFloat(unitRatio);
                            totalPiece = parseFloat(totalPiece) % parseFloat(unitRatio);
                        }
                    }
                    

                    $(div).find(".lblFree").text(parseInt(freeQty).toString() + "." + parseInt(totalPiece));


                } else if (discountType == "1") {
                    // amount
                    var discAmt = parseInt(baseQty) * parseFloat(discountValue);
                    totalAdditionalDisc += discAmt;

                    $('#divMoreDisc').removeClass("disNone");
                    $('#contentBody_txtExtraDiscount').val(parseFloat(totalAdditionalDisc.toFixed(2)));
                    $(div).find(".lblFree").text(totalAdditionalDisc);

                } else if (discountType == "2") {
                    // point
                    var frePoint = parseInt(baseQty) * parseInt(discountValue);
                    $(div).find(".lblFree").text(parseInt(frePoint));
                }


            }
            else if (offerType == "1") {
                // Offer type amount

                var subTotal = $(div).find(".totalPrice").text() == "" ? "0" : $(div).find(".totalPrice").text();
                var baseSubTotal = parseFloat(subTotal) / parseFloat(offerValue);

                if (discountType == "0") {
                    // qty
                    var freeQtyBaseAmt = parseInt(baseSubTotal) * parseInt(discountValue);
                    $(div).find(".lblFree").text(parseInt(freeQtyBaseAmt));

                } else if (discountType == "1") {
                    // amount
                    var discAmtBaseAmt = parseInt(baseSubTotal) * parseFloat(discountValue);
                    totalAdditionalDisc += discAmtBaseAmt;

                    $('#divMoreDisc').removeClass("disNone");
                    $('#contentBody_txtExtraDiscount').val(totalAdditionalDisc);
                    $(div).find(".lblFree").text(totalAdditionalDisc);


                } else if (discountType == "2") {
                    // point
                    var freePointBaseAmt = parseInt(baseSubTotal) * parseInt(discountValue);
                    $(div).find(".lblFree").text(parseInt(freePointBaseAmt));
                }

            }

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



// On event load
function eventChange() {

    initOptionVisibility();

}


document.onreadystatechange = function () {

    var storeId = $('#contentBody_lblStoreId').text();
    if (document.readyState === "complete") {

        //LoadQty(storeId);
    }
    else {
        window.onload = function () {
           // LoadQty(storeId);
        };
    };
};



$(document).ready(function () {

    $('#contentBody_ddlWarehouseList, #contentBody_ddlSupplierList').change(function () {
        
        $.xhrPool.abortAll = function () {
            //console.log("xhrPool:", $.xhrPool);
            $.each(this, function (jqXHR) {
                jqXHR.abort();
            });
        };

        isRequest = false;

        $("#contentBody_grdStock tr").each(function () {

            $(this).find(".grd-product-qty").text(" ");
            $(this).find(".grd-product-qty").addClass("stock-qty-loader grd-product-qty");

        });

        var storeId = $(this).val();
        //LoadQty(storeId);
    });

    $('#contentBody_ddlFieldAddVariant').change(function () {

        var type = 0;
        var value = $(this).val();

        $.ajax({
            url: baseUrl + "Admin/InventoryBundle/View/StockBulkOpt.aspx/getVariantDataAction",
            data: JSON.stringify({ "type": type, "value": value }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var ddlVariant = $('#contentBody_ddlAttrAddVariant');
                ddlVariant.empty();

                $.each(data.d, function () {
                    ddlVariant.append($("<option></option>").val(this['Value']).html(this['Text']));

                });
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });

    });



});


//var xhr = null;
//var isRequest = true;
//function LoadQty(storeId) {
//    var i = 0;
//    $.xhrPool = [];
//    $("#contentBody_grdStock tr").each(function () {

//        //Skip first(header) row
//        if (!this.rowIndex) return;
//        var prodId = $(this).find(".grd-product-Id").text();
//        var qty = $(this).find("td:eq(5)").text();
//        var unitId = $(this).find(".grd-unit-id").text();
//        var gridRow = $(this);
//        if (prodId.trim() != "") {
//            var obj = {
//                "prodId": prodId.trim(),
//                "storeId": storeId,
//                "unitId": unitId.trim()
//            };

//            var currentQty = '';
//            //if (xhr != null) {
//            //    xhr.abort();
//            //}
//            xhr = $.ajax({
//                url: baseUrl + "Admin/InventoryBundle/View/Stock.aspx/getProductQtyDataAction",
//                data: JSON.stringify(obj),
//                dataType: 'json',
//                type: "POST",
//                async: true,
//                contentType: "application/json; charset=utf-8",
//                success: function (data, responseCode, jqxhr) {
//                    currentQty = data.d;
//                    gridRow.find(".grd-product-qty").html(currentQty);
//                    gridRow.find(".grd-product-qty").removeClass("stock-qty-loader");
//                    if (!isRequest)
//                        xhr.abort();

//                },
//                failure: function (response) {
//                    console.log(response);
//                },
//                error: function (response) {
//                    console.log(response);
//                }
//            });


//            $.ajaxSetup({
//                beforeSend: function (xhr) {
//                    $.xhrPool.push(xhr);
//                }
//            });

//        }

//    });
//}





// Option visibility
function initOptionVisibility() {

    // Stock Action enable 
    if (columnAccess[0]["displayTransfer"] == "1") {
        $('.transfer-opt').removeClass('disNone');
    }
}


$(".btn-default").on("click", function () {
    // Stock Action enable 
    if (columnAccess[0]["displayTransfer"] == "1") {
        $('.transfer-opt').removeClass('disNone');
    }
});



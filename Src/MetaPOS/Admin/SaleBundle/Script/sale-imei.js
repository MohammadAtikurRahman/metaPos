



$(function () {
    $('#contentBody_ddlIMEI').select2();
});


$('#contentBody_ddlIMEI').on('select2:opening select2:closing', function (event) {
    var $searchfield = $(this).parent().find('.select2-search__field');
    $searchfield.prop('disabled', true);
});



function loadIMEIByProductCode(prodID) {
    console.log("prodID =>", prodID);
    lastProdCodeForImei = prodID;

    var obj = {};
    obj.prodID = prodID;

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getProductImeiListAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d != '') {

                // enable Imei dropdown
                $('#contentBody_divFieldControl').removeClass('disNone');
                isImeiStatusOpen = true;

                var ddlImei = $('#contentBody_ddlIMEI');
                $('#contentBody_ddlIMEI').empty();
                var splitImei = data.d.split(',');
                for (var i = 0; i < splitImei.length; i++) {

                    ddlImei.append($("<option></option>").val(splitImei[i]).html(splitImei[i]));


                    for (var key in imeiListShopCart) {

                        if (imeiListShopCart[key][prodID] == splitImei[i]) {

                            $("#contentBody_ddlIMEI option[value='" + imeiListShopCart[key][prodID] + "']").remove();
                        }
                    }


                }
            }
            else {

                //$('#dynamicContentUnit').closest("tr").find('.lblImei').text('N/A');
                $('#dynamicContentUnit').siblings('tr').last().find('.lblImei').text('N/A');
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


}


var sellImeiList = "";
$('#btnAddAttr').click(function () {

    var selectedImei = $('#contentBody_ddlIMEI').val();

    if (selectedImei.length == 0) {
        showMessage("Select a IMEI", "Warning");
        return;
    }


    $('#contentBody_divFieldControl').addClass('disNone');
    isImeiStatusOpen = false;

    prodFindingShoppingList(selectedImei, lastProdCodeForImei);


    // push imei to array
    for (var i = 0; i < selectedImei.length; i++) {
        var obj = {};
        obj[lastProdCodeForImei] = selectedImei[i];
        imeiListShopCart.push(obj);
    }

    $("option[value='" + selectedImei + "']").remove();

    changeItemBoxData(this);

});






function prodFindingShoppingList(selectedImei, lastProdCodeForImei) {

    $("#contentBody_divShoppingList tr").each(function (index) {

        var prodCode = $(this).find(".lblProdCode").text();
        if (lastProdCodeForImei == prodCode) {

            sellImeiList = $(this).find('.lblImei').text().replace("×", ",");
            if (sellImeiList == "N/A")
                sellImeiList = "";

            sellImeiList = sellImeiList.replace("×", ",");
            sellImeiList += selectedImei + ',';

            var imeiList = "";
            // split IMEI as a single value
            if (sellImeiList != "" && sellImeiList != null) {
                var singImei = sellImeiList.split(',');


                // make list for html view
                imeiList = "<ul id='ddlImei'>";
                for (var i = 0; i < singImei.length - 1; i++) {
                    imeiList += '<li>' + singImei[i] + ' <span class="btnCloseImeiSave"imeiVal="' + singImei[i] + '">×</span></li>';
                };
                imeiList += "</ul>";
                $(this).find('.lblImei').html(imeiList);

            }

            // Update Qty 
            var imeiLength = $('#contentBody_ddlIMEI').val().length;
            var oldQty = $(this).find('.ddlQty').val();
            $(this).find('.ddlQty').val((parseFloat(oldQty) + parseFloat(imeiLength)) - 1);
        }



    });

}



$(document).on('click', '.btnCloseImei', function () {

    var removeImei = $(this).attr("imeiVal");

    var currentRow = $(this).closest("tr");

    var removeQty = currentRow.find(".txtReturn").val();
    if (removeQty == null || removeQty == "")
        removeQty = 0;
    removeQty = parseFloat(removeQty) + 1;

    // hide curent imei
    var currentImei = currentRow.find(".lblRemoveImei").text();
    currentRow.find(".lblRemoveImei").text(currentImei.length > 0 ? removeImei + "," + currentImei : removeImei + ",");
    currentRow.find(".txtReturn").val(removeQty);

    changeReturn(this);

    $(this).parent().hide();


});



$(document).on('click', '.btnCloseImeiSave', function () {

    var removeImei = $(this).attr("imeiVal");

    var currentRow = $(this).closest("tr");

    var removeQty = currentRow.find(".ddlQty").val();
    if (removeQty == null || removeQty == "")
        removeQty = 0;
    removeQty = parseFloat(removeQty) - 1;

    // hide curent imei
    var currentImei = currentRow.find(".lblRemoveImei").text();
    currentRow.find(".lblRemoveImei").text(currentImei.length > 0 ? removeImei + "," + currentImei : removeImei + ",");
    currentRow.find(".ddlQty").val(removeQty);

    changeReturn(this);

    $(this).parent().remove();


});


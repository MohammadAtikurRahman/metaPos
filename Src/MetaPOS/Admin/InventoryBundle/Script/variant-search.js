

$(function () {

});



function variantSearch(variantField) {

    getVariantSearchData('1', '', variantField);

}




function getVariantSearchData(type, value, ddlVariant) {

    $.ajax({
        url: baseUrl + "Admin/InventoryBundle/View/StockBulkOpt.aspx/getVariantDataAction",
        data: JSON.stringify({ "type": type, "value": value }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            ddlVariant = $('#' + ddlVariant + '');
            ddlVariant.empty();
            if (type == "1") {
                ddlVariant.append($("<option></option>").val("0").html("Select"));
                totalField = 0;
            }

            $.each(data.d, function () {
                ddlVariant.append($("<option></option>").val(this['Value']).html(this['Text']));

                //field count
                totalField++;


            });


            if (type == "1") {
                $(ddlVariant).select2({
                    placeholder: "Select a attribute",
                    allowClear: true
                });
            }
            else {
                $(ddlVariant).select2({
                    placeholder: "Select a attribute",
                    multiple: true
                });
            }
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}



function fieldVariantSearch(ddlField) {
    // Field Search
    getVariantSearchData('1', '', ddlField);

}




function attributeVariantSearch(fieldValue, ddlAttribute) {
    // Attribute search

    getVariantSearchData('0', fieldValue, ddlAttribute);

}
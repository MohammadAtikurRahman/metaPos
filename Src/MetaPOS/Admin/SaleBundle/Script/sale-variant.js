

function getVariantNameForDisplay(attributeRecord) {

    var obj = {};
    obj.attributeRecord = attributeRecord;
    var variant = "";
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/getAttributeNameDataAction",
        data: JSON.stringify(obj),
        dataType: 'json',
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            variant = data.d;
            return data.d;
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

    return variant;
}
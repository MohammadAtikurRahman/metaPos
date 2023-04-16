


$(document).on("keyup paste", function () {

    searchPackage();

});


function searchPackage() {


    //if ($(document.activeElement).attr("id") == "<% = txtSearchNameCode.ClientID %>") {
    $("[id$=txtSearchNameCode]").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: baseUrl + 'Admin/InventoryBundle/View/Stock.aspx/GetProducts',
                data: "{ 'prefix': '" + request.term.trim() + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        var itemWithoutUnexpected = item.replace(',', ' ');
                        return {
                            label: itemWithoutUnexpected.split('<>')[0] + ' [' + itemWithoutUnexpected.split('<>')[1] + ']',
                            val: itemWithoutUnexpected.split('<>')[1]
                        };
                    }));
                },
                error: function (res) {
                    alert(res.responseText);
                },
                failure: function (res) {
                    alert(res.responseText);
                }
            });
        },
        select: function (e, i) {
            $("[id$=hfProductDetails]").val(i.item.val);
        },
        minLength: 1
    });
    //}
}
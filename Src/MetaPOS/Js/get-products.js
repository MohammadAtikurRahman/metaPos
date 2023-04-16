
$(document).keyup(function() {

    if ($(document.activeElement).attr("id") == "<% =txtSearchNameCode.ClientID %>") {

        $("[id$=txtSearchNameCode]").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/Admin/Stock.aspx/GetProducts") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                label: item.split(',')[0] + ' (' + item.split(',')[1] + ' )',
                                val: item.split(',')[1]
                            };
                        }));
                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function(e, i) {
                $("[id$=hfProductDetails]").val(i.item.val);
            },
            minLength: 1
        });
    }
});
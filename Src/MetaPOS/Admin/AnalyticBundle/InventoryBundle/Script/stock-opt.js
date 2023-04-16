
$(document).ready(function () {
    $('[id$=tagsinput]').tagsinput({
        itemValue: 'value',
        itemText: 'text',
        typeahead: {
            source: function (query) {
                var str;
                $.ajax({
                    type: "POST",
                    url: "CityNamesData.asmx/GetCities",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        str = data.d;
                    }
                });
                var str1 = $.getJSON(str);
                //var str = $.getJSON('assets/cities.json');
                return str1;
            }
        }
    });
    $('[id$=tagsinput]').tagsinput('add', { "value": 1, "text": "Amsterdam" });
    $('[id$=tagsinput]').tagsinput('add', { "value": 4, "text": "Washington" });
    $('[id$=tagsinput]').tagsinput('add', { "value": 7, "text": "Sydney" });
    $('[id$=tagsinput]').tagsinput('add', { "value": 10, "text": "Beijing" });
    $('[id$=tagsinput]').tagsinput('add', { "value": 13, "text": "Cairo" });
})



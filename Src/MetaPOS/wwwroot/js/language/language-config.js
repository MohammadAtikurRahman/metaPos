

$(function () {

    var language;
    try {
        language = columnAccess[0]["language"];
        if (language == "bn") {
            banglaLocalization();
        }
       
    }
    catch (e) {
        getSettingAccessValue();
    } 
    
});




function getSettingAccessValue() {
    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getSettingAccessValueAction",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            columnAccess = JSON.parse(data.d);

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}


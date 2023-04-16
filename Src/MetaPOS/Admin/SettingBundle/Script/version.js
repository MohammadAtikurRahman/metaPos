

$('#btnUpdateVersion').click(function () {
    $.ajax({
        url: baseUrl + "Admin/SettingBundle/View/Version.aspx/updateSystemVersion",
        dataType: 'json',
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data.d);
        },
        error: function (xmlhttprequest, textstatus, message) {
            if (textstatus === "timeout") {
                console.log("got timeout");
            }
            else {
                console.log(textstatus);
            }
        },
        timeout: 3600000
    });
});
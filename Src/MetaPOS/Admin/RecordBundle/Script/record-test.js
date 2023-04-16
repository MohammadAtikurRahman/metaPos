$.ajax({
    url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
    data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
    dataType: 'json', 
    type: "POST",
    cache: false,
}).done(function (data) {
    if (data.d !== "success") {
        showModalOutput("Unit name is exist! Try different name.", "warning");
    }
    else {
        saveAction();
    }
}).fail(function (jqXHR, textStatus, errorThrown) {
    console.log(textStatus + ': ' + errorThrown);
});







$(function () {


    $('#btnSaveProfile').click(function () {

        var company = $('#txtCompany').val();
        var phone = $('#txtPhone').val();
        var mobile = $('#txtMobile').val();
        var ownerNumber = $('#txtBusinessOwnerNumber').val();
        var vat = $('#txtVat').val();
        var tax = $('#txtTax').val();
        var url = $('#txtUrlPath').val();
        var header = $('#txtInvoiceHeader').val();
        var footer = $('#txtInvoiceFooter').val();

        var storeId = $('#contentBody_ddlStore').val();

        var jsonStrData = {
            "company": company,
            "phone": phone,
            "mobile": mobile,
            "ownerNumber": ownerNumber,
            "vat": vat,
            "tax": tax,
            "url":url,
            "header": header,
            "footer": footer,
            "storeId":storeId
        };
        console.log("jsonStrData:", jsonStrData);

        $.ajax({
            type: "Post",
            url: baseUrl + "Admin/ProfileBundle/Views/Profile.aspx/SaveProfileDataAction",
            data: "{ 'jsonStrData' : '" + JSON.stringify(jsonStrData) + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                showModalOutput(data.d, "success");

                reset();

                var changeStore = $('#contentBody_ddlStore').val();
                loadProfileData(changeStore);

            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });

    });


    function reset() {
        $('#txtCompany').val("");
        $('#txtPhone').val("");
        $('#txtMobile').val("");
        $('#txtBusinessOwnerNumber').val("");
        $('#txtVat').val("");
        $('#txtTax').val("");
        $('#txtUrlPath').val("");
        $('#txtInvoiceHeader').val("");
        $('#txtInvoiceFooter').val("");
    }



    function showModalOutput(msg, optType) {
        $('#msgOutput').removeAttr("style");
        $("#msgOutput").html("<div class='alert alert-" + optType + "' role='alert'><span class='fa fa-exclamation-triangle' aria-hidden='true'></span>&nbsp;<strong>Message!&nbsp;</strong>" + msg + "</div>");
        $("#msgOutput").delay(3200).fadeOut(300);
    }
});
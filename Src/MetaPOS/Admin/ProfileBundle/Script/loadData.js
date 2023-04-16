

$(function () {

    var currentStoreId = $('#contentBody_lblStoreId').text();
    console.log("currentStoreId:", currentStoreId);

    loadStore(currentStoreId);


    loadProfileData(currentStoreId);


    $('#contentBody_ddlStore').change(function () {

        var changeStore = $('#contentBody_ddlStore').val();

        loadProfileData(changeStore);

    });

});


function loadStore(storeId) {

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/ProfileBundle/Views/Profile.aspx/getStoreListDataAction",
        data: '{"storeId":' + storeId + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var ddlStores = $('#contentBody_ddlStore');

            ddlStores.empty().append('<option selected="selected" value="0">Select Store</option>');

            $.each(data.d, function () {
                ddlStores.append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            ddlStores.val(storeId);

        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });

};


function loadProfileData(storeId) {

    

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/ProfileBundle/Views/Profile.aspx/loadProfileDataAction",
        data: "{ 'storeId' : '" + storeId + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log("data:", data.d);

            var comapny = "", phone = "", mobile = "", ownerNumber = "", vat = "", tax = "", url = "", heaer = "", footer = "";
            if (data.d != "")
            {
                var jsonData = JSON.parse(data.d);

                comapny = jsonData[0].branchName;
                phone = jsonData[0].branchPhone;
                mobile = jsonData[0].branchMobile;
                ownerNumber = jsonData[0].ownerNumber;
                vat = jsonData[0].branchVatRegNo;
                tax = jsonData[0].branchTaxIdNo;
                url = jsonData[0].branchWebsite;
                heaer = jsonData[0].branchAddress;
                footer = jsonData[0].invoiceFooterNote;
            }

            $('#txtCompany').val(comapny);
            $('#txtPhone').val(phone);
            $('#txtMobile').val(mobile);
            $('#txtBusinessOwnerNumber').val(ownerNumber);
            $('#txtVat').val(vat);
            $('#txtTax').val(tax);
            $('#txtUrlPath').val(url);
            $('#txtInvoiceHeader').val(heaer);
            $('#txtInvoiceFooter').val(footer);
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

};



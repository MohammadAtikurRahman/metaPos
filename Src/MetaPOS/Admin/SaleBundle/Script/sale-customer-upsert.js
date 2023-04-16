// Base url
var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";
var pageName = location.pathname.split('/').slice(-1)[0];


$('#btnCustomerModalSale').click(function () {
    resetCustomerForm();

    $('#lblTitle').text('Add New Customer');
    $('#btnSaveCustomer').show();
    $('#btnUpdateCustomer').hide();

    // Set Page 
    $('#lblActionPage').text("sale");

    change();

    $('#modalCustomer').modal('show');
});


$('#contentBody_ddlCustomer').on('select2:close', function (e) {

    if (columnAccess[0]["displayInstalment"] == "0") {
        changeCustomerUpdateInfo();
    }

    if (pageName != "Servicing")
        updateCartAccountSection();
    console.log("closee...");

    changeCustomerUpdateInfo();
});


$('#contentBody_ddlCustomer').change(function () {
    if (columnAccess[0]["displayInstalment"] == "0") {
        changeCustomerUpdateInfo();
    }
    if (pageName != "Servicing")
        updateCartAccountSection();

    console.log("change...");
});


$(document).on('keyup', '.select2-search__field', function (ev) {

    var searchTxt = $(this).val();
    searchCustomerListForLoad(searchTxt);

    if (columnAccess[0]["displayInstalment"] == "0") {
        changeCustomerUpdateInfo();
    }

});


function getCustomerListSelectedByCusID(searchTxt) {
    if (searchTxt == "") {
        return;
    }

    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/searchCustomerListForLoadAction",
        dataType: "json",
        type: "POST",
        data: JSON.stringify({ searchTxt: searchTxt }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var ddlCustomers = $('#contentBody_ddlCustomer');

            ddlCustomers.empty().append('<option selected="selected" value="0">select customer</option>');

            $.each(data.d, function () {
                ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
            });

            // select current customer
            $('#contentBody_ddlCustomer').val(searchTxt).change();

            changeCustomerUpdateInfo();
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
}


// get customer list
function getCustomerList() {
    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/getCustomerListAction",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var ddlCustomers = $('#contentBody_ddlCustomerServiceing');

            ddlCustomers.empty().append('<option selected="selected" value="0">Select Customer</option>');

            $.each(data.d, function () {
                ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
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
}


function searchCustomerListForLoad(searchTxt) {
    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/searchCustomerListForLoadAction",
        dataType: "json",
        type: "POST",
        data: JSON.stringify({ searchTxt: searchTxt }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('#contentBody_ddlCustomer').empty();

            $.each(data.d, function () {
                $('#contentBody_ddlCustomer').append($("<option></option>").val(this['Value']).html(this['Text']));
            });
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
}


// customer modal close
$('#btnCloseCusModal').click(function () {
    $.trim($('#contentBody_txtCustomerName').val(" "));
    $.trim($('#contentBody_txtCustomerPhone').val(" "));
    $.trim($('#contentBody_txtCustomerAddress').val(" "));
    $.trim($('#contentBody_txtCustomerEmail').val(" "));
});



function changeCustomerUpdateInfo() {
    var cusId = $('#contentBody_ddlCustomer').val();

    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/changeCustomerUpdateInfoAction",
        dataType: "json",
        data: "{ 'cusId' : '" + cusId + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var jsonData = JSON.parse(data.d);
            if (jsonData != "") {
                //if (jsonData[0].installmentStatus == true && columnAccess[0]["displayInstalment"] == "1") {
                //    $('#divInstalment').removeClass("disNone");
                //}
            }
            else {
                $('#contentBody_txtPreviousDue').val("0.00");
            }

            // Load Prevoius due
            loadCustomerBalance();
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
}





function loadCustomerBalance() {

    var cusId = $('#contentBody_ddlCustomer').val();

    if (cusId == "0")
        return;

    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/loadCustomerBalanceDataAction",
        dataType: "json",
        data: "{ 'cusId' : '" + cusId + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var dueCustomer = data.d;
            if (dueCustomer == "")
                dueCustomer = 0;
            console.log("dueCustomer:", dueCustomer);

            $('#contentBody_txtPreviousDue').val(parseFloat(dueCustomer).toFixed(2));
            if (parseFloat(dueCustomer) > 0) {
                $('#divPreviousDue').removeClass("disNone");
                $('#divTotalBal').removeClass("disNone");
            }
            else {
                $('#divPreviousDue').addClass("disNone");
                $('#divTotalBal').addClass("disNone");
            }

            updateCartAccountSection();

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
}




/**** Validation ****/
$(function () {
    $("#contentBody_txtCustomerPhone").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
});

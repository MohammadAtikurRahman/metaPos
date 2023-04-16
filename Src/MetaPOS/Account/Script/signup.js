
var isFormValid = false;
var loading;

$(function () {

    var url = location.href;
    var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";


    $('#btnSaveStore').on("click", function () {

        var storeName = $('#txtStoreName').val();
        var email = $('#txtEmail').val();
        var password = $('#txtPassword').val();
        var phone = $('#txtPhone').val();


        // Validation
        var isValidate = true;
        if (storeName.length == 0) {
            $('.account-store-name-warning').removeClass("disNone");
            isValidate = false;
        }

        if (email.length == 0) {
            $('.account-email-warning').removeClass("disNone");
            isValidate = false;
        }

        if (password.length == 0) {
            $('.account-password-warning').removeClass("disNone");
            isValidate = false;
        }

        if (phone.length == 0) {
            $('.account-mobile-warning').removeClass("disNone");
            isValidate = false;
        }

        if (!isValidate) {
            return;
        }

        // start loading
        loading = new Loading({
            title: ' Please wait ',
            direction: 'hor',
            discription: 'Loading...',
            defaultApply: true,
        });

        var jsonData = {
            "storeName": storeName,
            "email": email,
            "password": password,
            "phone": phone
        };

        console.log("validated");

        $.ajax({
            url: baseUrl + "account/view/signup.aspx/signupToMetaposAction",
            data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var resultData = data.d;
                console.log("resultData::", resultData);
                var splitResult = resultData.split('|');
                if (splitResult[0] == 'false') {
                    loading.out();
                    showNotificationToast(splitResult[1], "warning");
                }
                else {
                    loading.out();
                    showNotificationToast(splitResult[1], "success");

                    window.location.href = "/admin/dashboard";
                }

                resetSignupForm();
            },
            error: function (data) {
                console.log("data error:", data);
                loading.out();
                showNotificationToast("Request is not compete, contact to support team.", "error");

                resetSignupForm();

            }

        });
    });



    function resetSignupForm() {

        $('#txtStoreName').val("");
        $('#metaposUrl').text("metaposbd.com/yourstore");
        $('#txtEmail').val("");
        $('#txtPassword').val("");
        $('#txtPassword').val("");
        $('#txtPhone').val("");
        $("#ddlPackage").val("");
        $("#ddlDomainNo").val("");
    }


    function showNotificationToast(message, type) {

        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        switch (type) {
            case "Success":
            case "success":
                toastr.success(message);
                break;
            case "Warning":
            case "warning":
                toastr.warning(message);
                break;
            case "Info":
            case "info":
                toastr.info(message);
                break;
            case "Error":
            case "error":
                toastr.error(message);
                break;

            default:
                toastr.info("Something is wrong");
        }
    }




    $('#txtStoreName').on("keyup", function () {

        var inputTxt = $(this).val();
        var inputLength = inputTxt.length;
        console.log("inputLength:", inputLength);
        if (inputLength <= 0) {
            $('.account-store-name-warning').removeClass("disNone");
            $('#txtStoreName').addClass("account-textbox-warning");
            $('#metaposUrl').text("metaposbd.com/yourstore");
            return;
        }

        $('.account-store-name-warning').addClass("disNone");

        var inputDbname = $('#txtStoreName').val();
        var subdomainChekcer = {
            url: baseUrl + "account/view/signup.aspx/checkSubdomainDataAction",
            data: "{ 'subdomain' : '" + inputDbname + "' }",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
        };

        $.ajax(subdomainChekcer).done(function (response) {

            $('#metaposUrl').text("metaposbd.com/" + inputTxt);
            if (response.d == true) {
                $('.account-store-name-warning').removeClass("disNone");
                $('.store-name-warning-text').text("Your store name already exists.");
                $('#metaposUrl').css("color", "red");
            }
            else {
                $('#metaposUrl').css("color", "black");
            }
        });


    });


    $('#txtEmail').on("focusout", function () {
        var txtEmail = $(this).val();
        var emailLen = txtEmail.length;
        if (emailLen == 0) {
            $('.account-email-warning').removeClass("disNone");
        }
        else {
            $('.account-email-warning').addClass("disNone");
        }
    });

    $('#txtPassword').on("focusout", function () {
        var txtinput = $(this).val();
        var inputLen = txtinput.length;
        if (inputLen == 0) {
            $('.account-password-warning').removeClass("disNone");
        }
        else {
            $('.account-password-warning').addClass("disNone");
        }
    });

    $('#txtPhone').on("focusout", function () {
        var txtinput = $(this).val();
        var inputLen = txtinput.length;
        if (inputLen == 0) {
            $('.account-warning-message').text("Please enter a phone number");
            $('.account-mobile-warning').removeClass("disNone");
            return;
        }
        else {
            $('.account-mobile-warning').addClass("disNone");
        }

        if (inputLen < 11) {
            $('.account-mobile-warning').removeClass("disNone");
            $('.account-warning-message').text("Please enter a valid phone number");
            return;
        }
        else {
            $('.account-mobile-warning').addClass("disNone");
        }
    });

    

    //$('#txtPhone').keypress(function (evt) {
    //    var val = String.fromCharCode(evt.which);
    //    if (!(/[0-9]/.test(val))) {
    //        evt.preventDefault();
    //    }
    //});




    $("#txtStoreName").keypress(function (event) {
        var ew = event.which;
        if (ew == 32)
            return false;
        if (48 <= ew && ew <= 57)
            return true;
        if (65 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;
    });

    $("#txtBusinessName").keypress(function (event) {
        var ew = event.which;
        if (ew == 32)
            return false;
        if (48 <= ew && ew <= 57)
            return true;
        if (65 <= ew && ew <= 90)
            return true;
        if (97 <= ew && ew <= 122)
            return true;
        return false;
    });

    $("#txtPhone").keypress(function (event) {
        var ew = event.which;
        if (ew == 32)
            return false;
        if (48 <= ew && ew <= 57)
            return true;
        if (65 <= ew && ew <= 90)
            return false;
        if (97 <= ew && ew <= 122)
            return false;
        return false;
    });

});

$("#btnSignup").click(function () {
    //$(this).prop("disabled", true);
    //$(this).css("cursor", "not-allowed");
    //$(this).attr("disabled", true);
    //var self = $(this),//'submit',
    //    button = self.find('input[type = "submit"], button');
    //    button.attr('disabled', 'disabled');

    $('.account-warning-signup').removeClass("disNone");
    //$('#btnSignup').attr('disabled', 'disabled');
});
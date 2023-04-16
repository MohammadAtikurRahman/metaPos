$('document').ready(function () {




    //FIX MODALS
    $('.modal-trigger').leanModal({
        dismissible: false
    });

    (function ($) {
        $(function () {

            $('.button-collapse').sideNav();

        }); // end of document ready
    })(jQuery); // end of jQuery name space


    //Login & For Gotin Password Box Hideing
    $(".forgot_box").hide();

    $("#forgePass").click(function () {
        $(".log_box").hide("slide", function () {
            $(".forgot_box").show("slide");
        });
    });

    $("#backToLogin").click(function () {
        $(".forgot_box").hide("slide", function () {
            $(".log_box").show("slide");
        });
    });

    //$(".btnFeatures").click(function() {
    //    window.open('http://metaposbd.com/web/metaposbd/features.html', '_blank');
    //});

    $("#btnLogIn").click(function () {
        document.getElementById("loader").style.display = "block";
        document.getElementById("login-body").style.display = "none";
        $("body").removeClass("backstretch");
    });

});

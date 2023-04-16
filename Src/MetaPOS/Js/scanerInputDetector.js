$(document).ready(function() {

    var keyupFiredCount = 0;


    function DelayExecution(f, delay) {
        var timer = null;
        return function() {
            var context = this, args = arguments;

            clearTimeout(timer);
            timer = window.setTimeout(function() {
                    f.apply(context, args);
                },
                delay || 10);
        };
    }


    $.fn.ConvertToBarcodeTextbox = function() {

        $(this).focus(function() {
            $(this).select();
            $("#msg").html("");
        });

        $(this).keydown(function(event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);

            if ($(this).val() == '') {
                keyupFiredCount = 0;
            }
            if (keycode == 13) { //enter key
                $(".nextcontrol").focus();
                return false;
                event.stopPropagation();
            }
        });

        $(this).keyup(DelayExecution(function(event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            keyupFiredCount = keyupFiredCount + 1;
        }));

        $(this).blur(function(event) {
            if ($(this).val() == '')
                return false;

            if (keyupFiredCount <= 1) {
                ;
                //$("#msg").html("<span style='color:green'>Its Scanner!</span>");
                //__doPostBack('<%=btnPrint.ClientID%>', "");
                //document.getElementById("<%= btnClearSession.ClientID %>")
                //alert('Its Scanner');
            }
            else {
                ;
                //$("#msg").html("<span style='color:red'>Its Manually Typed!</span>");
                //alert('Its Manual Entry');
            }

            keyupFiredCount = 0;
        });
    };
    try {
        $(".barcodeinput").ConvertToBarcodeTextbox();
        if ($(".barcodeinput").val() == '')
            $(".barcodeinput").focus();
        else
            $(".nextcontrol").focus();
    }
    catch (e) {
    }

});


$(function() {

    $('.float-number-validate').on('keypress', function (event) {
        var value = $(this).val();

        if (event.keyCode == 46)
            return true;

        if (event.keyCode < 48 || event.keyCode > 57)
            return false;
    });

    $('.int-number-validate').on('keypress', function (event) {

        if (event.keyCode < 48 || event.keyCode > 57)
            return false;
    });

    $('.char-input-validate').on('keypress', function(event) {
        var keyCode = event.which ? event.which : event.keyCode;
        //Small Alphabets
        if (parseInt(keyCode) >= 97 && parseInt(keyCode) <= 122) {
            return true;
        }
        //Caps Alphabets
        if (parseInt(keyCode) >= 65 && parseInt(keyCode) <= 90) {
            return true;
        }
        if (parseInt(keyCode) == 32 || parseInt(keyCode) == 13 || parseInt(keyCode) == 46 || keyCode == 9 /*TAB*/ || keyCode == 8 /*BCKSPC*/ || keyCode == 37 /*LFT ARROW*/ || keyCode == 39 /*RGT ARROW*/) {
            return true;
        }
        return false;
    });

});
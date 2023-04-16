/**
 * Input Validation
 * Author: sayedbrur@gmail.com
 */

$(document).ready(function() {

    $('#zoneForm').validate({
        rules: {
            'zone_form[name]': {
                required: true,
                maxlength: 100
            },
            'zone_form[latitude]': {
                required: true,
                number: true,
                maxlength: 12
            },
            'zone_form[longitude]': {
                required: true,
                number: true,
                maxlength: 12
            },
            'zone_form[radius]': {
                required: true,
                integer: true,
                maxlength: 5
            }
        },
        highlight: function(element) {
            //$(element).closest('.control-group').removeClass('success').addClass('with-errors');
        },
        success: function(element) {
            //element.text('OK!').addClass('valid').closest('.control-group').removeClass('error').addClass('success');
        },
        submitHandler: function(form) {
            upsertZone();
            return false;
        }
    });
});
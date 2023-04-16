
// Fire page load functions
function eventChange() {

    
    

}


// Show alert in modal operation
function showModalOutputOpeningDue(msg, optType) {
    $('#msgOutputOpeningDue').removeAttr("style");
    $("#msgOutputOpeningDue").html("<div class='alert alert-" + optType + "' role='alert'><span class='fa fa-exclamation-triangle' aria-hidden='true'></span>&nbsp;<strong>Warning!&nbsp;</strong>" + msg + "</div>");
    $("#msgOutputOpeningDue").delay(3200).fadeOut(300);
}


// Show alert in modal operation Advance
function showModalOutputAdvance(msg, optType) {
    $('#msgOutputAdvance').removeAttr("style");
    $("#msgOutputAdvance").html("<div class='alert alert-" + optType + "' role='alert'><span class='fa fa-exclamation-triangle' aria-hidden='true'></span>&nbsp;<strong>Warning!&nbsp;</strong>" + msg + "</div>");
    $("#msgOutputAdvance").delay(3200).fadeOut(300);
}


// Show alert in modal operation Advance
function showModalOutputPayment(msg, optType) {
    $('#msgOutputPayment').removeAttr("style");
    $("#msgOutputPayment").html("<div class='alert alert-" + optType + "' role='alert'><span class='fa fa-exclamation-triangle' aria-hidden='true'></span>&nbsp;<strong>Warning!&nbsp;</strong>" + msg + "</div>");
    $("#msgOutputPayment").delay(3200).fadeOut(300);
}


// document ready
$(function() {

});


$('#example-getting-started').change(function() {
    var brands = $('#example-getting-started option:selected');
    var selected = [];
    $(brands).each(function(index, brand) {
        selected.push([$(this).val()]);
    });


    if (selected[0] == "account") {
        customerGridColumnShow();
    }
    else {
        customerGridColumnHide();
    }
});



function customerGridColumnShow() {

    $('#dataListTable tr > td:nth-child(3)').show();
    $('#dataListTable tr > th:nth-child(3)').show();
}


function customerGridColumnHide() {

    $('#dataListTable tr > td:nth-child(3)').addClass('disNone');
    $('#dataListTable tr > th:nth-child(3)').addClass('disNone');
}

$(function () {
    // changeUnitOption();

});


//$('#contentBody_ddlUnit').change(function () {
//  changeUnitOption();
//});

//function changeUnitOption() {

//    // Default div piece
//    var unitSelect = $('#contentBody_ddlUnit').val();

//    if (unitSelect == '0') {
//        $('#contentBody_divPiece').removeClass("disBlock").addClass("disNone");
//        $('#contentBody_txtPiece').val("");
//    }
//    else {
//        $('#contentBody_divPiece').removeClass("disNone").addClass("disBlock");
//        $('#contentBody_txtPiece').val("");
//    }
//}


// Check IMEI Enable and disable when add product

$('#contentBody_chkIMEIEnable').change(function () {
    if ($(this).prop('checked') == false) {
        $('#contentBody_txtIMEI').text(" ");
        $(".bootstrap-tagsinput span").remove();
        //$(".bootstrap-tagsinput input[type='text']").prop({
        //    disabled: true
        //});
    }
    else {
        $('#contentBody_txtIMEI').text(" ");
        $('#contentBody_txtIMEI').class("enable");
    }
});






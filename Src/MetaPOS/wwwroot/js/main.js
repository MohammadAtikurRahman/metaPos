
var accessPagesGlobal;
$(function () {
    
});

// On load event
function mainChange() {

    $.when($.ajax(findActiveSubMenu())).then(function () {
        findActiveMainMenu();
    });

    loadOperationControl();

    loadDatePicker();

    loadMenuCollapseDesign();

    getSettingAccessValue();

    if (typeof eventChange !== 'undefined' && $.isFunction(eventChange)) {
        eventChange();
    }
}


// Init request
function initRequest(sender, args) {

    accessPagesGlobal = $("#accessPagesGlobal").val();

    initProgressbar();
}


// End request
function endRequest(sender, args) {

    endProgressbar();

    loadOperationControl();

    loadDatePicker();
}


// Load date picker
function loadDatePicker() {
    $('.datepickerCSS').each(function () {
        $(this).datepicker({
            dateFormat: "dd-M-yy"
        });
    });
}


// Init progressbar 
function initProgressbar() {

    $('#metaposloading').removeClass('disNone');

    //$get("progressDivWithGif").style.display = "block";
    //$('body').append('<div class="for-ajax modal-backdrop fade in"></div>');
    //$('.ajax-loader').removeClass('hidden');
}


// End progressbar 
function endProgressbar() {
    $('#metaposloading').addClass('disNone');

    //$get("progressDivWithGif").style.display = "none";
    //$('.for-ajax').remove();
}


function reloadThisPage() {
    location.reload(true);
}

// Operation Control for add, edit and delete
function loadOperationControl() {

    if (addAccess == "False") {
        $('.btnAddOpt').addClass("disNone");
        //$('.btnAddOpt').attr('disabled', 'disabled');
    }

    if (editAccess == "False") {
        $('.btnEditOpt').addClass('disNone');
        //$('.btnEditOpt').attr('disabled', 'disabled');
    }

    if (deleteAccess == "False") {
        $('.btnDeleteOpt').addClass('disNone');
        //$('.btnDeleteOpt').attr('disabled', 'disabled');
    }
}


// Dropdown visiblity
$("#btnSettings").on("click", function () {
    if ($(".dropdown-menu").hasClass("disBlock")) {
        $(".dropdown-menu").removeClass("disBlock");
        $(".dropdown-menu").addClass("disNone");
    }
    else {
        $(".dropdown-menu").addClass("disBlock");
        $(".dropdown-menu").removeClass("disNone");
    }
});


// Menu expansion
$('#main-menu a').each(function (index) {
    if (this.href.trim() == window.location) {
        $(this).addClass("selected");
        $(this).parent().parent().attr('aria-expanded', true);
        $(this).parent().parent().addClass('in');
        $(this).parent().parent().parent().addClass('active');
    }
});

$('#main-menu li ul').each(function (index) {
    if ($(this).has("li").length == 0)
        $(this).parent().addClass('hide');
});


// Bind dropdown
function BindEvents() {
    $('[data-toggle=dropdown]').dropdown();
}


// Print
function printDiv(divName) {
    var printContents = document.getElementById(divName).innerHTML,
        originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}


// Show alert in modal operation
function showModalOutput(msg, optType) {
    $('#msgOutput').removeAttr("style");
    $("#msgOutput").html("<div class='alert alert-" + optType + "' role='alert'><span class='fa fa-exclamation-triangle' aria-hidden='true'></span>&nbsp;<strong>Warning!&nbsp;</strong>" + msg + "</div>");
    $("#msgOutput").delay(3200).fadeOut(300);
}





// Show opeation messages
function showMsgOutput(status, operation) {

    if (status == true && operation == "save") {
        showMessage("Data saved successfully.", "Success");
    }
    else if (status == false && operation == "save") {
        showMessage("Opearation failed!", "Error");
    }
    else if (status == true && operation == "update") {
        showMessage("Data updated successfully.", "Success");
    }
    else if (status == false && operation == "save") {
        showMessage("Opearation failed!", "Error");
    }
    else if (status == true && operation == "delete") {
        showMessage("Data deleted successfully.", "Success");
    }
    else if (status == false && operation == "delete") {
        showMessage("Opearation failed!", "Error");
    }
    else if (status == true && operation == "restore") {
        showMessage("Data restored successfully.", "Success");
    }
    else if (status == false && operation == "restore") {
        showMessage("Opearation failed!", "Error");
    }
    else {
        showMessage(operation, "Error");
    }
}




// Show opeation messages
function showOutput(status) {

    if (status === "success") {
        showMessage("Operation successful.", "Success");
    } else {
        showMessage(status, "Error");
    }
}




// Check page permission avaialable or not
function checkPagePermission(pageName) {
    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/checkPagePermissionAction",
        dataType: "json",
        data: JSON.stringify({ "pageName": pageName }),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data.d == false)
                window.location.replace(baseUrl + "Admin/404");
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}







// Check page permission avaialable or not
function getSettingAccessValue() {
    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getSettingAccessValueAction",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            columnAccess = JSON.parse(data.d);

            //setInvoiceUrl();

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}




/***** Check exception error handling *************/

function isEmptyVal(value) {
    if (value == "")
        return true;
    else
        return false;
}


function isNull(value) {
    if (value == null)
        return true;
    else
        return false;
}


function isUndefined(value) {
    if (value == undefined)
        return true;
    else
        return false;
}


function isException(value) {
    var error = false;
    if (isEmptyVal(value)) {
        //console.log("isEmpty");
        error = true;
    }
    if (isNull(value)) {
        //console.log("isNull");
        error = true;
    }
    if (isUndefined(value)) {
        //console.log("isUndefined");
        error = true;
    }

    return error;
}

function getUrlParameterSingle(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1));
    console.log("sPageURL:", sPageURL);
    var sParameterName = sPageURL.split('=');
    return sParameterName[1];

    //for (i = 0; i < sURLVariables.length; i++) {
    //    sParameterName = sURLVariables[i].split('=');

    //    if (sParameterName[0] === sParam) {
    //        return sParameterName[1] === undefined ? true : sParameterName[1];
    //    }
    //}
};


function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode;
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
        return false;

    return true;
}

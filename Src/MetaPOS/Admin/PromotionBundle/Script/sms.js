// Global variable
var medium = "";
var smsCurrentBalance;
var countNumber;
var messageCounter = "";
var countCheckboxCheckedNumber = "";
var totalCost;


// On page load
function eventChange() {
    activeModule = "promotion";

    medium = loadMediumUsingGroupData();
    //initialPageLoad();
}




// Show Medium

function loadMedium(groupId) {

    var jsonData = {
        "select": "medium,cost,balance",
        "from": "SmsConfigInfo",
        "where": {
            "roleId": groupId
        },
        "column": "id",
        "dir": "asc"
    };

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var formatData = JSON.parse(data.d);
            
            if (formatData[0].medium && formatData[0].medium != "") {
                $('#showMedium').html("<h3>Medium : " + formatData[0].medium + "</h3>").css("font-family", "New Times Roman");
            }
            else {
                showMessage("Medium not found!");
            }

            if (formatData[0].cost && formatData[0].cost != "") {
                $('#contentBody_lblSmSRate').text(formatData[0].cost);
                $('#contentBody_getMsgCost').text(formatData[0].cost);
                
            }


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




// Checkbox 

$(document).ready(function () {

    initialPageLoad();
    // Selected Number Count

    $("input[type=checkbox]").each(function () {
        $(this).change(checkedNumberCount);

        cssStyle();

    });



    // Start Page 

    function initialPageLoad() {

        $('#checkedPhoneCount').html("<b><i>Contacts Count:</i><b>" + 0);
        $('#contentBody_lblShowCost').html("<b><i>Message Cost:</i></b>" + 0);
        $('#lblCount').html(0 + "/" + 0);
    }


    // Message Count

    $('textarea').keyup(messageCount);
    $('textarea').keydown(messageCount);

    var msgCount;

    function messageCount() {
        //getSmsBalance();
        var charLength = $(this).val().length;
        if (/^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\s\\/]+$/.test($(this).val())) {
            msgCount = charLength / 160;
        }
        else {
            msgCount = charLength / 70;
        };

        messageCounter = Math.ceil(msgCount);

        $('#lblCount').html(messageCounter + "/" + charLength);


        // Total Cost   

        sendSmsCost();
    }



});
// Total Cost
$('input').change(function () {

    sendSmsCost();
});



// Select Phone Number

function checkedNumberCount() {

    countCheckboxCheckedNumber = $("input[type=checkbox]:checked").length;

    $('#checkedPhoneCount').html("<b><i>Contacts Count:</i><b>" + " " + countCheckboxCheckedNumber);
    sendSmsCost();

};



// Sms Cost

function sendSmsCost() {
    var msgCost = parseFloat($("#contentBody_getMsgCost").val());
    totalCost = msgCost * countCheckboxCheckedNumber * messageCounter;

    $('#contentBody_lblShowCost').html("<b><i>Message Cost:</i></b>" + " " + totalCost.toFixed(2));






}

// CSS Styles

function cssStyle() {

    $('#lblShowCost').css('font-family', 'Times New Roman');
    $('#lblShowCost').css('font-size', '18px');
    $('#checkedPhoneCount').css('font-family', 'Times New Roman');
    $('#checkedPhoneCount').css('font-size', '18px');
    $('#lblCharacterCount').css('font-family', 'Times New Roman');
    $('#lblCharacterCount').css('font-size', '18px');
    $('#lblCount').css('font-family', 'Times New Roman');
    $('#lblCount').css('font-size', '18px');
    $('#contentBody_lblShowCost').css('font-size', '18px'),
    $('#contentBody_lblShowCost').css('font-family', 'Times New Roman');
}




function loadMediumUsingGroupData() {

    var jsonData = {
        "select": "groupId",
        "from": "RoleInfo",
        "where": {
            "roleId": roleIdGlobal
        },
        "column": "groupId",
        "dir": "asc"
    };

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var jsonParseData = JSON.parse(data.d);
            if (jsonParseData[0].groupId != undefined)
                loadMedium(jsonParseData[0].groupId);
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
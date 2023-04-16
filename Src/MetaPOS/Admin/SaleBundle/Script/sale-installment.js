

$(function () {
    var datetimeNow = moment();
    var currentDateTime = datetimeNow.tz('Asia/Dhaka').format('DD-MMM-YYYY');
    console.log("currentDateTime:", currentDateTime);
    $('#contentBody_txtStartPymentDate').val(currentDateTime);
    $('#contentBody_txtCustomerRemainder').val(currentDateTime);
});


$('#contentBody_txtInstalmentNumber, #contentBody_txtPayCash,#contentBody_txtInterestRate').keyup(function () {
    installment();

});



function installment() {
    var totalBal = $('#contentBody_txtGiftAmt').val();
    var payment = $('#contentBody_txtPayCash').val() == "" ? "0" : $('#contentBody_txtPayCash').val();
    var dueAmt = parseFloat(totalBal) - parseFloat(payment);
    //var dueAmt = parseInt(totalBal) - parseInt(payment);
    if (dueAmt > 0) {

        var insNum = $('#contentBody_txtInstalmentNumber').val() == "" ? "0" : $('#contentBody_txtInstalmentNumber').val();
        var downPayment = 0;
        if (insNum > 0)
             downPayment = parseFloat(dueAmt) / parseFloat(insNum);
            //downPayment = parseInt(dueAmt) / parseInt(insNum);
        $('#contentBody_txtDownPayment').val(downPayment.toFixed(2));

    }
}
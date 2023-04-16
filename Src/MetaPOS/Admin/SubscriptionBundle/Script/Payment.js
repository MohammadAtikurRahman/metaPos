

$(function() {
    $('#contentBody_txtNumberOfMonth').keyup(function () {
        console.log("logo");
        var fee = $('#contentBody_lblFee').text();

        var month = $(this).val();
        if (month == null || month == "") {
            month = 1;
        }


        var totalFee = parseFloat(month) * parseFloat(fee);

        $('#contentBody_lblTotalAmt').text(totalFee.toFixed(2));
    });
})


$(function() {
    $('#contentBody_txtCustomerRemainder').attr('disabled', 'disabled');
})


$('#ddlDateSeletor,#contentBody_txtStartPymentDate').change(function () {
    var increment = $(this).val();

    var startDate = $('#contentBody_txtStartPymentDate').val();
    var futureDate = startDate;

    if (increment != "") {

        if (increment == "1m") {
            futureDate = moment(startDate).add(1, 'months');
        } else if (increment == "3m") {
            futureDate = moment(startDate).add(3, 'months');
        }
        else if (increment == "6m") {
            futureDate = moment(startDate).add(6, 'months');
        }
        else if (increment == "1y") {
            futureDate = moment(startDate).add(1, 'year');
        }
        else {
            futureDate = moment(startDate).add(increment, 'days');
        }
    }

    //var new_date = moment(Date.now()).add(dateIncrementor, 'days');

    var reminder_date = moment(futureDate).format("DD-MMM-YYYY");

    $('#contentBody_txtCustomerRemainder').val(reminder_date);

});
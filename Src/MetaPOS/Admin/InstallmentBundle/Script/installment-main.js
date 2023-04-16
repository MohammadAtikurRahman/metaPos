


$(function() {
    // $('#txtFrom').val(moment(Date.now()).format("DD-MMM-YYYY"));
    var datetimeAgo = moment(new Date()).add(-1, 'days').format("DD-MMM-YYYY");
    $('#txtFrom').val(datetimeAgo);
    var datetimeNow = moment(new Date()).add(7, 'days').format("DD-MMM-YYYY");

    $('#txtTo').val(datetimeNow);

})
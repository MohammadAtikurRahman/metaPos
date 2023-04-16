


$(function () {
    
    //var isafter = moment(moment(expiryDate).format("MM-DD-YYYY")).isAfter(moment().format("MM-DD-YYYY")); // true

    //if (!isafter) {

    //}

    $('#btnCloseNotify').click(function() {
        $('#expiryNotificatonBar').addClass('disNone');
        $('#expiryNotificatonBar').addClass('disNone');
        $('#sidebar').removeClass('side-bar-adjust-after-notification');
        $('#invoiceBody').removeClass('padding-top-30');
        $('#menuHeader').removeClass('padding-0');
    });


    if (expiryNotify == "1") {
        $('#lblSubscriptionDate').text(moment(expiryDate).format("DD-MMM-YYYY"));
        $('#expiryNotificatonBar').removeClass('disNone');
        $('#sidebar').addClass('side-bar-adjust-after-notification');
        $('#invoiceBody').addClass('padding-top-30');
        $('#menuHeader').addClass('padding-0');
        
    } else if (expiryNotify == "2") {
        $('#expiryModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    }
});
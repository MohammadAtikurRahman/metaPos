function showMessage(message, messagetype) {

    var cssclass;

    switch (messagetype) {
    case 'Success':
        cssclass = 'alert-success';
        break;
    case 'Error':
        cssclass = 'alert-danger';
        break;
    case 'Warning':
        cssclass = 'alert-warning';
        break;
    default:
        cssclass = 'alert-info';
    }

    $("#alert_div").remove();
    $('#alert_container').append('<div id="alert_div" style="margin: 10px 0px 0px; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

    setTimeout(function() {
        $("#alert_div").fadeTo(2000, 500).slideUp(500, function() {
            $("#alert_div").remove();
        });
    }, 3000);

}
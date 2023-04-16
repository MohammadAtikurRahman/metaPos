function showNotificationToast(message, type) {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    switch (type) {
        case "Success":
            toastr.success(message);
            break;
        case "Warning":
            toastr.warning(message);
            break;
        case "Info":
            toastr.info(message);
            break;
        case "Error":
            toastr.error(message);
            break;

        default:
            toastr.info("Something is wrong");
    }
}
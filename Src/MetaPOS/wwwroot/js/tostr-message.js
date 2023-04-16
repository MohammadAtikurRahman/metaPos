function messageTostr(message, type) {

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
        case "success":
            toastr.success(message);
            break;
        case "Warning":
        case "warning":
            toast.warning(message);
            break;
        case "Info":
        case "info":
            toastr.info(message);
            break;
        case "Error":
        case "error":
            toastr.error(message);
            break;

        default:
            toastr.info("Something is wrong");
    }
}
//if ('serviceWorker' in navigator) {
//    window.addEventListener('load', () => {
//        navigator.serviceWorker
//            .register('sw_pages.js')
//            .then(reg => console.log('Service Worker: Registered (Pages)'))
//            .catch(err => console.log(`Service Worker: Error: ${err}`));
//});
//}


console.log("app");

function showNotification(type,message) {

    var title = "";
    if (type == "success")
        title = "Success";
    else if (type == "warning")
        title = "Warning";

    $.toast({
        heading: title,
        text: message,
        icon: type,
        position: 'top-right',
    });
}
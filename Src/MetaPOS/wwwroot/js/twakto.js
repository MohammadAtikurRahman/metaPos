//<!--Start of Tawk.to Script-->
var tawkUrl = location.href;
var tawkBaseUrl = tawkUrl.substring(7, tawkUrl.indexOf('/', 10)) + "";

var companyName = $("#companyName").val();
var companyDomain = $("#companyDomain").val();
var companyEmail = $("companyEmail").val();

var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
(function () {
    var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
    s1.async = true;
    s1.src = 'https://embed.tawk.to/5d91b5446c1dde20ed04210b/default';
    s1.charset = 'UTF-8';
    s1.setAttribute('crossorigin', '*');
    s0.parentNode.insertBefore(s1, s0);
})();

Tawk_API.onLoad = function () {
    Tawk_API.setAttributes({
        'name': companyName + ' ' + '(' + tawkBaseUrl + ')',
        'email': companyEmail,
        'hash': 'hash value'
    }, function (error) { });
};
    //<!--End of Tawk.to Script-->
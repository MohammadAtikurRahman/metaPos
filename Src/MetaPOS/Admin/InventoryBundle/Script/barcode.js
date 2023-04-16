



function customBarcodePrint(prodName, prodCode, prodPrice) {
    // Base url
    var url = location.href;
    var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";

    window.open("" + baseUrl + "Admin/BarcodeTool/barcode.html?name=" + prodName + "&code=" +
             prodCode + "&price=" + prodPrice + "", '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes');

    

    //window.open("" + baseUrl + "Admin/BarcodeTool/barcode.html?name=" + prodName + "&code=" +
    //              prodCode + "&price=" + prodPrice + "", '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes');

}


function barcodePrint(prodName, prodCode, prodPrice) {
    // Base url
    var url = location.href;
    var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";

    window.open("" + baseUrl + "Admin/BarcodeTool/barcode.html?name=" + prodName + "&code=" +
                  prodCode + "&price=" + prodPrice + "", '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes');

}
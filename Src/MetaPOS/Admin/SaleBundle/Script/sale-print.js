// Print Invoice
$('#btnPrintCrystalViewer').on('click', function () {
    window.frames["InvoiceIframe"].focus();
    window.frames["InvoiceIframe"].print();
    window.frames["InvoiceIframe"].close();
});

// Print Receipt 
$('#btnPrintReceipt').on('click', function () {
    window.frames["ReceiptIframe"].focus();
    window.frames["ReceiptIframe"].print();
});


// Print Receipt for resturent
$('#btnPrintTokenRcpt').on('click', function () {
    window.frames["TokenReceiptIframe"].focus();
    window.frames["TokenReceiptIframe"].print();
});


function myFunction() {
    var objDiv = $("#contentBody_divShoppingList").val();

    if (objDiv != null) {
        objDiv.scrollTop = objDiv.scrollHeight;
    }
}




/*
    0 = main invoice, 1 = short Invoice, 2 = godown Invoice, 3 = bangla Invoice,  
*/

function printInvoiceDiv() {
    var invoiceType = columnAccess[0]["isUnicode"];
    //var printPageType = columnAccess[0]["SalesPrintPageType"];
    var printPageType = $("#contentBody_rblPaperSizeOption input[type='radio']:checked").val();
    var billNo = $('#contentBody_lblBillNoHidden').val();

    var type = 0;
    if (invoiceType == 0 && printPageType == 0) {
        // small invocie - eng
        type = 0;
    }
    else if (invoiceType == 0 && printPageType == 1) {
        // large invoice - eng
        type = 1;
    }
    else if (invoiceType == 1 && printPageType == 0) {
        // small invocie - bng
        type = 2;
    }
    else if (invoiceType == 1 && printPageType == 1) {
        // large invocie - bng
        type = 3;
    }


    window.open("SaleBundle/Report/Invoice.html?billNo=" + billNo + "&type=" + type + "", '_blank', 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
}




function printTokenRcptDiv() {
    var invoiceType = columnAccess[0]["isUnicode"];
    //var printPageType = columnAccess[0]["SalesPrintPageType"];
    var printPageType = $("#contentBody_rblPaperSizeOption input[type='radio']:checked").val();
    var billNo = $('#contentBody_lblBillNoHidden').val();

    var type = 0;
    if (invoiceType == 0 && printPageType == 0) {
        // small invocie - eng
        type = 0;
    }
    else if (invoiceType == 0 && printPageType == 1) {
        // large invoice - eng
        type = 1;
    }
    else if (invoiceType == 1 && printPageType == 0) {
        // small invocie - bng
        type = 2;
    }
    else if (invoiceType == 1 && printPageType == 1) {
        // large invocie - bng
        type = 3;
    }

    var isTokenRcpt =false;


    window.open("SaleBundle/Report/TokenRcpt.html?billNo=" + billNo + "&type=" + type + "&rcpt="+isTokenRcpt+"", '_blank', 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
}



function printGodownDiv() {


    // New code goes here ***********
    var printWindow;
    var invoiceType = columnAccess[0]["isUnicode"];
    var printPageType = columnAccess[0]["challanPaperSize"];
    var billNo = $('#contentBody_lblBillNoHidden').val();
    

    var type = 0;
    if (invoiceType == 0 && printPageType == 0) {
        // small invocie - eng
        type = 0;
    }
    else if (invoiceType == 0 && printPageType == 1) {
        // large invoice - eng
        type = 1;
    }
    console.log("printPageType::", printPageType);

    printWindow = window.open("SaleBundle/Report/challan.html?billNo=" + billNo + "&type=" + type + "", '_blank', 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
    //printWindow.print();

}



function printReceiptDiv() {
    //var printWindow = window.open("" + baseUrl + "Admin/Print/InvoiceLoader.aspx?type=0", 'height=200,width=400');
    var printWindow = window.open("" + baseUrl + "Admin/Print/InvoiceLoader.aspx?type=3", '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes');
    printWindow.document.close();
    printWindow.print();
    printWindow.document.close();
}




$(function () {

    var afterPrint = function () {
        this.window.close();
    };

    if (window.matchMedia) {
        var mediaQueryList = window.matchMedia('print');

        mediaQueryList.addListener(function (mql) {
            //alert($(mediaQueryList).html());
            if (!mql.matches) {
                afterPrint();
            }
        });
    }
    window.onafterprint = afterPrint;
});
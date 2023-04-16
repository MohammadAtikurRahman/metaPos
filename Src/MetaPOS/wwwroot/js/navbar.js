$(function () {
    // Offline 

    console.log("accessPagesGlobal: " + accessPagesGlobal);

});


// Default visibility
function findActiveSubMenu() {

    // Set access page global
    if (userRightGlobal == "Super") {
        $(".btnLinkUser #dynamicLinkUser").text("Reseller");
        accessPagesGlobal = "Dashboard; User; Security; Version; Docs;";
    }
    else if (userRightGlobal == "Group") {
        $(".btnLinkUser #dynamicLinkUser").text("Company");
        accessPagesGlobal = "Dashboard; User; Security; Support; Web; Version; Docs; Payment;";
    }
    else if (userRightGlobal == "Branch") {
        //$(".btnLinkUser #dynamicLinkUser").text("User");
        accessPagesGlobal = "User; Profile; Security; SmsConfig; Version; Docs; " + accessPagesGlobal;
    }

    

    // Active selected menu
    console.log('active==' + activeModule);
    $(".navbar-nav ." + activeModule + " a:first-child").addClass('selected-nav-module');

    // Check invoice be active
    if (~accessPagesGlobal.indexOf("Sale;")) {
        $(".btnLinkInvoice").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Invoice;")) {
        $(".btnLinkInvoice").removeClass('disNone');
    }

    moduleVisibility();
}


// Check module visibility 
function moduleVisibility() {
    if (activeModule == "default")
        visibleDefaultNav("");
    else if (activeModule == "inventory")
        visibleInventoryModule("");
    else if (activeModule == "sale")
        visibleSaleModule("");
    else if (activeModule == "accounting")
        visibleAccountingModule("");
    else if (activeModule == "report")
        visibleReportModule("");
    else if (activeModule == "promotion")
        visiblePromotionModule("");
    else if (activeModule == "record")
        visibleRecordModule("");
    else if (activeModule == "settings")
        visibleSettingsModule("");
    else if (activeModule == "public")
        visibleConfigurationModule("");
    else if (activeModule == "website")
        visibleShopModule("");
}


// Default visibility
function visibleDefaultNav() {
    if (~accessPagesGlobal.indexOf("Slip;")) {
        $(".btnLinkSlip").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Customer;")) {
        $(".btnLinkCustomer").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("StockReport;")) {
        $(".btnLinkStockReport").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Transaction;")) {
        $(".btnLinkTransaction").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Summary;")) {
        $(".btnLinkSummary").removeClass('disNone');
    }
}


// Inventory visiblity
$("#linkInventory").on("click", function () {
    visibleInventoryModule("#linkInventory ");
});


function visibleInventoryModule(prefix) {

    if (~accessPagesGlobal.indexOf("Purchase;")) {
        $(prefix + ".btnLinkPurchase").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Stock;")) {
        $(prefix + ".btnLinkStock").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Package;")) {
        $(prefix + ".btnLinkPackage").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Warranty;")) {
        $(prefix + ".btnLinkWarranty").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Return;")) {
        $(prefix + ".btnLinkReturn").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Damage;")) {
        $(prefix + ".btnLinkDamage").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Cancel;")) {
        $(prefix + ".btnLinkCancel").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Warning;")) {
        $(prefix + ".btnLinkWarning").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Expiry;")) {
        $(prefix + ".btnLinkExpiry").removeClass('disNone');
    }

}


// Sales visiblity
$("#linkSale").on("click", function () {
    visibleSaleModule("#linkSale ");
});


function visibleSaleModule(prefix) {

    if (~accessPagesGlobal.indexOf("Sale;")) {
        $(prefix + ".btnLinkSaleInvoice").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Invoice;")) {
        $(prefix + ".btnLinkSaleInvoice").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Customer;")) {
        $(prefix + ".btnLinkCustomer").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Quotation;")) {
        $(prefix + ".btnLinkQuotation").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Servicing;")) {
        $(prefix + ".btnLinkServicing").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("DueReminder;")) {
        $(prefix + ".btnLinkDueReminder").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Token;")) {
        $(prefix + ".btnLinkToken").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Service;")) {
        $(prefix + ".btnLinkService").removeClass('disNone');
    }

}


// Accounting visiblity
$("#linkAccounting").on("click", function () {
    visibleAccountingModule("#linkAccounting ");
});


function visibleAccountingModule(prefix) {

    if (~accessPagesGlobal.indexOf("Supply;")) {
        $(prefix + ".btnLinkSupply").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Receive;")) {
        $(prefix + ".btnLinkReceive").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Expense;")) {
        $(prefix + ".btnLinkExpense").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Salary;")) {
        $(prefix + ".btnLinkSalary").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Banking;")) {
        $(prefix + ".btnLinkBanking").removeClass('disNone');
    }
}


// Reports visiblity
$("#linkReport").on("click", function () {
    visibleReportModule("#linkReport ");
});


function visibleReportModule(prefix) {

    if (~accessPagesGlobal.indexOf("Transaction;")) {
        $(prefix + ".btnLinkTransaction").removeClass('disNone');
    }
    if (~accessPagesGlobal.indexOf("ProfitLoss;")) {
        $(prefix + ".btnLinkProfitLoss").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Summary;")) {
        $(prefix + ".btnLinkSummary").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Analytic;")) {
        $(prefix + ".btnLinkAnalytic").removeClass('disNone');
    }
    if (~accessPagesGlobal.indexOf("SupplierCommission;")) {
        $(prefix + ".btnLinkSupplierCommission").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("PurchaseReport;")) {
        $(prefix + ".btnLinkPurchaseReport").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("InventoryReport;")) {
        $(prefix + ".btnLinkInventoryReport").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("StockReport;")) {
        $(prefix + ".btnLinkStockReport").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Slip;")) {
        $(prefix + ".btnLinkSlip").removeClass('disNone');
    }
}




// Promotion visiblity
$("#linkPromotion").on("click", function () {
    visiblePromotionModule("#linkPromotion ");
});


function visiblePromotionModule(prefix) {

    if (~accessPagesGlobal.indexOf("Offer;")) {
        $(prefix + ".btnLinkOffer").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("SMS;")) {
        $(prefix + ".btnLinkSMS").removeClass('disNone');
    }
}


// Records visiblity
$("#linkRecord").on("click", function () {
    visibleRecordModule("#linkRecord ");
});


function visibleRecordModule(prefix) {

    if (~accessPagesGlobal.indexOf("Manufacturer;")) {
        $(prefix + ".btnManufacturer").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Location;")) {
        $(prefix + ".btnLocation").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Supplier;")) {
        $(prefix + ".btnLinkSupplier").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Category;")) {
        $(prefix + ".btnLinkCategory").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Particular;")) {
        $(prefix + ".btnLinkParticular").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Staff;")) {
        $(prefix + ".btnLinkStaff").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Bank;")) {
        $(prefix + ".btnLinkBank").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Card;")) {
        $(prefix + ".btnLinkCard").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("UnitMeasurement;")) {
        $(prefix + ".btnLinkUnitMeasurement").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Store;")) {
        $(prefix + ".btnLinkWarehouse").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Field;")) {
        $(prefix + ".btnLinkField").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Attribute;")) {
        $(prefix + ".btnLinkAttribute").removeClass('disNone');
    }
    if (~accessPagesGlobal.indexOf("ServiceType;")) {

        $(prefix + ".btnLinkServiceType").removeClass('disNone');
    }
}


// Shop visiblity
$("#linkWebsite").on("click", function () {
    visibleShopModule("#linkWebsite ");
});

function visibleShopModule(prefix) {

    if (~accessPagesGlobal.indexOf("Ecommerce;")) {
        $(prefix + ".btnLinkEcommerce").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Web;")) {
        $(prefix + ".btnLinkWeb").removeClass('disNone');
    }
}


// Settings visiblity
$("#linkSettings").on("click", function () {
    visibleSettingsModule("#linkSettings ");
});

function visibleSettingsModule(prefix) {


    if (~accessPagesGlobal.indexOf("User;")) {
        $(prefix + ".btnLinkUser").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Profile;")) {
        $(prefix + ".btnLinkProfile").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Security;")) {
        $(prefix + ".btnLinkSecurity").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("ImportCustomer;")) {
        $(prefix + ".btnLinkCustomerImport").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Support;")) {
        $(prefix + ".btnLinkSupport").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Setting;")) {
        $(prefix + ".btnLinkSetting").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("SmsConfig;")) {
        $(prefix + ".btnLinkSmsConfig").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Sync;")) {
        $(prefix + ".btnLinkSync").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Subscription;")) {
        $(prefix + ".btnLinkSubscription").removeClass('disNone');
    }
    if (~accessPagesGlobal.indexOf("Payment;")) {
        $(prefix + ".btnLinkPayment").removeClass('disNone');
    }

}


// public visiblity
$("#linkPublic").on("click", function () {
    visibleConfigurationModule("#linkPublic ");
});

function visibleConfigurationModule(prefix) {

    if (~accessPagesGlobal.indexOf("Version;")) {
        $(prefix + ".btnLinkVersion").removeClass('disNone');
    }

    if (~accessPagesGlobal.indexOf("Docs;")) {
        $(prefix + ".btnLinkDocs").removeClass('disNone');
    }
}




$("#LinkOffline").on("click", function () {

    var prefix = "#LinkOffline ";

    if (~accessPagesGlobal.indexOf("Offline;")) {
        $(prefix + ".btnLinkOffline").removeClass('disNone');
        $(prefix + ".btnLinkLoadOffline").removeClass('disNone');
    }

});
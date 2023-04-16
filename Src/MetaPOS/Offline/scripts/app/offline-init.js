
var url = location.href;
var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";
var isReadyOfflineCustomer = false, isReadyOfflineProduct = false, isReadyOfflineConfig = false;

let configs;
let customers;
let products;
let sales;
var db;
let dbReq = indexedDB.open('metaPOSDatabase', 1);
dbReq.onupgradeneeded = function(event) {
    // Set the db variable to our database so we can use it!  
    db = event.target.result;

    configs = db.createObjectStore('metaposConfigs', { autoIncrement: true });
    customers = db.createObjectStore('metaposCustomers', { autoIncrement: true });
    products = db.createObjectStore('metaposProducts', { autoIncrement: true });
    sales = db.createObjectStore('metaposSales', { autoIncrement: true });
};

dbReq.onsuccess = function(event) {
    db = event.target.result;


    var txProduct, storeProducts, txCustomer, storeCustomers, txSlip, storeSlips, txConfig, storeConfigs;

    try {
        txProduct = db.transaction(['metaposProducts'], 'readonly');
        storeProducts = txProduct.objectStore('metaposProducts');

        txCustomer = db.transaction(['metaposCustomers'], 'readonly');
        storeCustomers = txCustomer.objectStore('metaposCustomers');

        txConfig = db.transaction(['metaposConfigs'], 'readonly');
        storeConfigs = txConfig.objectStore('metaposConfigs');


        // Products
        var reqProduct = storeProducts.openCursor();
        reqProduct.onsuccess = function (evnt) {
            var cursor = evnt.target.result;
            if (cursor == null) {

                //initProducts();
            }
        };
        reqProduct.onerror = function (evnt) {
            alert('error in Products cursor request ' + evnt.target.errorCode);
        };


        // Customers
        var reqCustomer = storeCustomers.openCursor();
        reqCustomer.onsuccess = function (evnt) {
            var cursor = evnt.target.result;
            if (cursor == null) {

                //initCustomers();
            }
        };
        reqCustomer.onerror = function (evnt) {
            alert('error in Customers cursor request ' + evnt.target.errorCode);
        };

        

        

        // Configs
        var reqConfig = storeConfigs.openCursor();
        reqConfig.onsuccess = function (evnt) {
            var cursor = evnt.target.result;
            if (cursor == null) {

               // initConfigs();
            }
        };
        reqConfig.onerror = function (evnt) {
            alert('error in Configs cursor request ' + evnt.target.errorCode);
        };


        
    }
    catch (e) {
        
    }
    
};



function initProducts() {

    $('.metapos-loading').removeClass("disNone");

    $.ajax({
        url: baseUrl + "offline/offline.aspx/initializeProductAction",
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        type: 'POST',
        success: function (data) {
            console.log("offline data x:", data.d);
            var metaposConfig = JSON.stringify(data.d);
            var configsData = JSON.parse(metaposConfig);
            configsData = JSON.parse(configsData);

            var tx = db.transaction(['metaposProducts'], 'readwrite');
            var products = tx.objectStore('metaposProducts');

            var objectStoreRequest = products.clear();

            objectStoreRequest.onsuccess = function (event) {
                // report the success of our request
                console.log("clear Request successful.");
            };

            for (var i = 0; i < configsData.length; i++) {

                products.add({ text: configsData[i], timestamp: Date.now() });
            }
            tx.oncomplete = function () {
                $('.metapos-loading').addClass("disNone");
                console.log('product initialization completed');
                if (isReadyOfflineCustomer && isReadyOfflineConfig)
                    window.location.href = "/offline";
                else
                    isReadyOfflineProduct = true;
            };


        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("error", xhr.status);
            console.log(thrownError);
            console.log(xhr.responseText);

            exitOnlineWithoutDataLoad();
        }
    });
}


function initCustomers() {
    
    
    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/initializeCustomerAction",
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        type: 'POST',
        success: function (data) {
            var metaposConfig = JSON.stringify(data.d);
            var configsData = JSON.parse(metaposConfig);
            configsData = JSON.parse(configsData);

            var tx = db.transaction(['metaposCustomers'], 'readwrite');
            var customers = tx.objectStore('metaposCustomers');
            for (var i = 0; i < configsData.length; i++) {
                customers.add({ text: configsData[i], timestamp: Date.now() });
            }
            tx.oncomplete = function () {
                console.log('Customer initialization completed');
                if (isReadyOfflineProduct && isReadyOfflineConfig)
                    window.location.href = "/offline";
                else
                    isReadyOfflineCustomer = true;
            };


        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("error", xhr.status);
            console.log(thrownError);
            console.log(xhr.responseText);

            exitOnlineWithoutDataLoad();

        }
    });

}



function initConfigs() {
    

    $.ajax({
        url: baseUrl + "Admin/SaleBundle/View/Invoice.aspx/initializeConfigAction",
        dataType: 'json',
        contentType: "application/json;charset=utf-8",
        type: 'POST',
        success: function (data) {
            var metaposConfig = JSON.stringify(data.d);
            var configsData = JSON.parse(metaposConfig);
            configsData = JSON.parse(configsData);

            var  tx = db.transaction(['metaposConfigs'], 'readwrite');
            var store = tx.objectStore('metaposConfigs');
            for (var i = 0; i < configsData.length; i++) {
                store.add({ text: configsData[i], timestamp: Date.now() });
            }
            tx.oncomplete = function () {
                console.log('Configuration initialization completed');
                if (isReadyOfflineProduct && isReadyOfflineCustomer)
                    window.location.href = "/offline";
                else
                    isReadyOfflineConfig = true;
            };


        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log("status", xhr.status);
            console.log(thrownError);
            console.log(xhr.responseText);

            exitOnlineWithoutDataLoad();
        }
    });

}


function exitOnlineWithoutDataLoad() {
    if (confirm('Sorry, Data is not Sync, Are you want to continue offline? ')) {
        window.location.href = "/offline";
    } else {
        $('.metapos-loading').addClass("disNone");
    }
}


$(function () {
    $('.btnLinkLoadOffline a').on('click', function () {
        console.log("start sync");
        var dbReq = indexedDB.open('metaPOSDatabase', 1.1);
        console.log("dbReq:", dbReq);
        dbReq.onupgradeneeded = function (event) {
            console.log("deleted");
            var db = event.target.result;
            db.deleteObjectStore("metaposProducts");
            db.deleteObjectStore("metaposCustomers");
        };

        initProducts();
        initCustomers();
        initConfigs();
        console.log("end sync");
    })


    $('.btnLinkOffline a').on('click', function () {
        console.log("offline clicked");
        window.location.href = "/offline";
    });
})
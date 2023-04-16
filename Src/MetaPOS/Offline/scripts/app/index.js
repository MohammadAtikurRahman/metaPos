var db;

var dbReq = indexedDB.open('metaPOSDatabase', 1);

var datetimeNow = moment();
var currentDateTime = datetimeNow.tz('Asia/Dhaka').format();
console.log("DhakaTime:", currentDateTime);
var timestampTemp = moment.unix(datetimeNow);
var timestamp = timestampTemp._i;

dbReq.onsuccess = function (event) {
    console.log("Database initilized ");
    db = event.target.result;

    checkAndInitailizeData(db);
};

var txProduct, storeProducts, txCustomer, storeCustomers, txSlip, storeSlips, txConfig, storeConfigs;
function checkAndInitailizeData(db) {

   

    try {
        txProduct = db.transaction(['metaposProducts'], 'readwrite');
        storeProducts = txProduct.objectStore('metaposProducts');

        txCustomer = db.transaction(['metaposCustomers'], 'readwrite');
        storeCustomers = txCustomer.objectStore('metaposCustomers');

        txSlip = db.transaction(['metaposSales'], 'readwrite');
        storeSlips = txSlip.objectStore('metaposSales');

        txConfig = db.transaction(['metaposConfigs'], 'readwrite');
        storeConfigs = txConfig.objectStore('metaposConfigs');

        configSetting(db);



        getProducts(db);

        getCustomers(db);

    }
    catch (e) {
        console.log("Error", e);
        showNotification("warning", "Database is not initilization, please login your account.");

        indexedDB.deleteDatabase('metaPOSDatabase');

        //setTimeout(function () { window.location.href = "/login"; }, 5000);
    }



}


$(function() {
    getProducts(db);

    getCustomers(db);
});


function configSetting(db) {

    console.log("config");
    // Configations
    txConfig = db.transaction(['metaposConfigs'], 'readwrite');
    storeConfigs = txConfig.objectStore('metaposConfigs');
    var req = storeConfigs.openCursor();
    var allConfigs = [];

    req.onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor != null) {

            allConfigs.push(cursor.value);
            cursor.continue();
        }
        else {
            displayConfig(allConfigs);
        }
    };
    req.onerror = function (event) {
        alert('error in cursor request ' + event.target.errorCode);
    };
}


function displayConfig(config) {
    $('#lblCompany').text(config[0].text.branchName);
    $('#lblStoreName').text(config[0].text.name);
}

var config = "";
var storeProductsList = "", storeCustomersList = "";


function getProducts(db) {

    txProduct = db.transaction(['metaposProducts'], 'readwrite');
    storeProducts = txProduct.objectStore('metaposProducts');

    var req = storeProducts.openCursor();
    var allProducts = [];

    req.onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor != null) {
            allProducts.push(cursor.value.text);
            cursor.continue();
        }
        else {
            console.log("allProducts:", allProducts);
            intDropdownProduct(allProducts);

            storeProductsList = allProducts;
            console.log("storeProductsList:", storeProductsList);
        }
    };
    req.onerror = function (event) {
        alert('error in cursor request ' + event.target.errorCode);
    };

};



// Get customer list
function getCustomers(db) {

    txCustomer = db.transaction(['metaposCustomers'], 'readwrite');
    storeCustomers = txCustomer.objectStore('metaposCustomers');
    var req = storeCustomers.openCursor();
    var allCustomers = [];

    req.onsuccess = function (event) {
        var cursor = event.target.result;
        if (cursor != null) {
            allCustomers.push(cursor.value.text);
            cursor.continue();
        }
        else {

            intDropdownCustomer(allCustomers);

            storeCustomersList = allCustomers;
            console.log("storeCustomersList:", storeCustomersList);
        }
    };
    req.onerror = function (event) {
        alert('error in cursor request ' + event.target.errorCode);
    };

};



function intDropdownProduct(products) {

    $('.txtProduct').flexdatalist({
        searchContain: true,
        textProperty: '{name} [{code}]',
        valueProperty: 'code',
        minLength: 1,
        focusFirstResult: true,
        selectionRequired: true,
        //groupBy: 'catName',
        visibleProperties: ["name", "qty", "salePrice"],
        searchIn: ["name", "code", "catName", "salePrice"],
        //url: 'product.json',
        data: products,
        relatives: '#relative',
        noResultsText: 'No product found  "{keyword}"'
    });

};



function intDropdownCustomer(customers) {


    var inputCustAccount = $('.txtCustomer').flexdatalist({
        searchContain: true,
        textProperty: '{name}',
        valueProperty: '{cusID}',
        minLength: 1,
        focusFirstResult: true,
        selectionRequired: true,
        //groupBy: 'type',
        visibleProperties: ["name", "phone", "address"],
        searchIn: ["name", "phone", "address", "email"],
        //url: 'product.json',
        data: customers,
        relatives: '#relative',
        noResultsText: 'No customer found  "{keyword}"'
    });
    inputCustAccount.on('select:flexdatalist', function (event, data) {
        console.log("selected cusssst");
        $('#txtCustomerTmpId').val(data.id);
        $('#txtCustomerId').val(data.cusID);
        $('#txtCustomerName').val(data.name);
    });

};


document.body.addEventListener('keydown', function (e) {
    if (e.which === 83 && e.ctrlKey && e.altKey) {
        //alert('CTRL & ALT + S'); 
        alert("Your Invoice is saved");
    }
});




var app = window.app || {};


$('#btnSaveInvoice').on("click", function () {

    if ($('#tblCart tr').length > 0) {
        app.saveInvoice();
        showNotification("success", "Invoice created successfully.");
    } else {
        showNotification("warning", "Cart can not be emty.");
    }

});



//Detect When the Enter Key Pressed
$('#txtProduct').keypress(function (event) {
    console.log("txtProduct keypress");
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        app.addToCart($("#txtProduct").val());
    }

});



(function ($) {



    // Invoice accounting 
    app.calculateCart = function () {

        var unitPrice = 0;
        var itemQty = 0;
        var CartTotal = 0;
        var discountType = "%";
        var discountAmount = 0;
        var GrandTotal = 0;

        $("#cart_body tr").each(function (index) {

            unitPrice = parseFloat($(this).find('.itemUnitPrice').val().trim()).toFixed(2);
            itemQty = parseInt($(this).find('.txtItemQty').val().trim());

            CartTotal += unitPrice * itemQty;
        });


        // discount calculation 
        var discountAmt = $('#txtDiscount').val();
        //console.log(discountAmt);

        if ($('#txtDiscountType').prop("checked") == true) {

            discountAmount = parseFloat(CartTotal * discountAmt / 100).toFixed(2);
            discountType = "%";
        }
        else {
            discountAmount = parseFloat(discountAmt).toFixed(2);
            discountType = "৳";
        }

        GrandTotal = parseFloat(CartTotal - discountAmount).toFixed(2);


        // Pay
        var payAmt = parseFloat($('#txtPayToday').val()).toFixed(2);

        if (payAmt == "NaN") {
            payAmt = 0;
        }

        console.log(GrandTotal);
        console.log(payAmt);


        $('#txtCartTotal').val(parseFloat(CartTotal).toFixed(2));
        $('#txtGrandTotal').val(parseFloat(GrandTotal).toFixed(2));
        $('#txtBalance').val(parseFloat(GrandTotal - payAmt).toFixed(2));
        $('#txtDisAmt').text('(' + parseFloat(discountAmount).toFixed(2) + ')');


    };


    // add to cart 
    app.addToCart = function (code) {


        // first check product exit OR not in CART 
        var IsDup = false;

        // product code from text box
        var Product_code = code;


        $("#cart_body tr").each(function (index) {

            var cartProdId = $(this).find('.item_code').text().trim();

            if (Product_code == cartProdId) {


                // add qty exiting product
                $("#cart_body tr").each(function (indexCart) {

                    if ($(this).find('.item_code').text().trim() == Product_code) {
                        var cur_qty = $(this).find(".txtItemQty").val();

                        $(this).find(".txtItemQty").val(parseInt(cur_qty) + 1);

                        $("#txtProduct").val("");
                        app.calculateCart();
                    }

                });
                IsDup = true;
            }
        });

        if (IsDup == false) {
            // app.addToCart(Product_code);

            var products = storeProductsList;

            var product = _.find(products, {
                'code': code
            }),

              count = 1;
            console.log("product:", product);

            if (undefined != product) {

                if (count > 0) {

                    // var qtyIndex = $(".lblTotalCartQty").html();
                    //var qtyIndex = $('.lblTotalCartQty').attr("totalqty");

                    var qtyIndex = $('#tblCart tr').length;

                    // if(qtyIndex == 0)
                    // {
                    //     qtyIndex = 1;
                    // }

                    //console.log(qtyIndex);

                    var cart = "";


                    cart += '<tr class="row_dynamic">';
                    cart += '<td class="itemSL">' + ++qtyIndex + '</td>';
                    cart += '<td>';
                    cart += '<span class="item_name">' + product.name + '</span>';
                    cart += '<div class="item_details">';
                    cart += '<p>Category <span class="item_cat">: ' + product.catName + '</span></p>';
                    cart += '<p>Supplier <span class="item_sup">: ' + product.supName + '</span></p>';
                    cart += '<p>Total Qty <span class="item_tq">: ' + product.qty + '</span></p>';
                    cart += '<p class="disNone">Code <span class="item_code"> ' + product.code + '</span></p>';
                    cart += '<p class="disNone">Id <span class="item_id "> ' + product.prodId + '</span></p>';
                    cart += '</div>';
                    cart += '</td>';
                    cart += '<td>';
                    cart += '<input type="text" value="' + product.salePrice + '" class="form-control itemUnitPrice hasAccount">';
                    cart += '</td>';
                    cart += '<td>';
                    cart += '<input type="text" value="1" class="form-control txtItemQty hasAccount">';
                    cart += '</td>';
                    cart += '<td><span class="sub_total"> ' + parseFloat(product.salePrice * 1).toFixed(2) + '</td>';
                    cart += '<td>';
                    cart += '<button class="btn btn-danger btn-sm btnRemoveItem"> <i class="material-icons">close</i></button>';
                    //cart += '<button class="btn btn-danger btn-sm"> X </button>';
                    cart += '</td>';
                    cart += '</tr>';


                    $('#cart_body').append(cart);
                    $("#txtProduct").val("");

                    var rowCount = $('#tblCart tr').length;
                    $('.lblTotalCartQty').attr("totalqty", rowCount);
                    $(".lblTotalCartQty").html(rowCount);

                    app.calculateCart();


                }
                else {
                    $("#txtProduct").val("");
                    alert('Available qty is o');
                }
            }
            else {
                alert('Oops! Somethings went wrong');
            }
        }
    };


    // Save Product
    app.saveInvoice = function () {
        $("#cart_body tr").each(function (index) {

            var p_code = $(this).find('.item_code').text().trim();
            var saleQty = $(this).find(".txtItemQty").val();


            // get all products
            var products = storeProductsList,
                product = _.find(products, {
                    'code': p_code
                });

            // remove the selected product
            var remove_product = _.remove(products, {
                'code': p_code
            });


            // update the qty 
            product.qty -= saleQty;


            // push obj as new value
            products.push(product);


            // update local storage data
            //localStorage.setItem('metaposProduct', JSON.stringify(products));

        });

        app.saveSlip();
        resetInvoiceOffline();
        console.log("Invoice Created");

    };


    // Save Slip
    var newCustId = "";
    app.saveSlip = function () {

        var salesData = [];

        var executeCounter = 0;
        var email = "", phone = "", address = "", type = "0";
        $("#cart_body tr").each(function (index) {
            var qty = $(this).find('.txtItemQty').val().trim();
            var disAmt = $('#txtDisAmt').text().replace("(", "").replace(")", "");
            var sPrice = $(this).find('.itemUnitPrice').val().trim();
            var cusName = $('#txtCustomer').val();
            var txtCustomerId = $('#txtCustomerId').val();
            var tempId = $('#txtCustomerTmpId').val().trim();
            var paycash = $('#txtPayToday').val().trim() == "" ? "0" : $('#txtPayToday').val().trim();

            var discountType = "%";
            if ($('#txtDiscountType').prop("checked") == true) {
                discountType = "%";
            }
            else {
                discountType = "৳";
            }


            if (index == 0) {
                console.log("tempId:", tempId, "//", txtCustomerId);

                tempId = parseInt(tempId.trim());
                var customers = storeCustomersList;
                console.log("customers__:", customers);
                var customer = _.find(customers, {
                    'id': tempId
                });

                if (customer == undefined) {
                    customer = _.find(customers, {
                        'id': tempId.toString()
                    });
                }
                console.log("cust__", customer);


                if (customer == undefined) {
                    var generator = new IDGenerator();
                    var id = generator.generate();
                    var customerInsert = {
                        "id": id,
                        "cusID": "",
                        "name": "",
                        "phone": "",
                        "address": "",
                        "email": "",
                        "balance": "0",
                        "type": "0",
                        "status": true
                    };

                    // push data in LS
                    var txCustomer = db.transaction(['metaposCustomers'], 'readwrite');
                    var customerDataStore = txCustomer.objectStore('metaposCustomers');
                    customerDataStore.add({ text: customerInsert, timestamp: timestamp });

                    newCustId = id;
                    txtCustomerId = id;
                    cusName = "";
                    email = "";
                    phone = "";
                    address = "";
                    type = "0";
                }
                else {
                    email = customer.email;
                    phone = customer.phone;
                    address = customer.address;
                    type = customer.type;
                }
                console.log("email:", email, "/address:", address);
                if (email == undefined)
                    email = "";

                if (phone == undefined)
                    phone = "";

                if (address == undefined)
                    address = "";

                if (type == undefined)
                    type = "0";

            }
            else {
                txtCustomerId = newCustId;
            }


            var salesObj = {
                "saleId": '',
                "stockStatusId": '',
                "billNo": '',
                "cusId": txtCustomerId,
                "cusName": cusName,
                "email": email,
                "phone": phone,
                "address": address,
                "type": type,
                "prodID": $(this).find('.item_id').text().trim(),
                "qty": qty == "" ? "1" : qty,
                "netAmt": $('#txtCartTotal').val(),
                "discAmt": disAmt == "" ? "0" : disAmt,
                "vatAmt": 0,
                "vatType": "Tk",
                "grossAmt": $('#txtGrandTotal').val(),
                "payMethod": 0,
                "payCash": paycash,
                "giftAmt": $('#txtBalance').val(),
                "return_": 0,
                "balance": $('#txtBalance').val(),
                "sPrice": sPrice == "" ? "0" : sPrice,
                "discType": discountType,
                "comment": '',
                "currentCash": paycash,
                "salesPersonId": 0,
                "salesPersonName": 'Admin',
                "referredBy": "0",
                "cardId": 1,
                "brankId": 1,
                "token": 0,
                "cusType": 0,
                "maturityDate": moment.tz(Date.now(), "Asia/Dhaka").format(),
                "checkNo": "",
                "warranty": ' ',
                "loadingCost": 0,
                "unloadingCost": 0,
                "shippingCost": 0,
                "carryingCost": 0,
                "paidAmt": 0,
                "deleteCartProductSaleId": '',
                "deleteCartProductSotckStatusId": '',
                "salePersonType": 0,
                "additionDue": 0,
                "returnQty": 0,
                "returnAmt": 0,
                "paidReturnAmt": 0,
                "preDue": 0,
                "entryDate": currentDateTime,
                "prodCodes": '',
                "isPackage": 0,
                "engineNumber": '',
                "cecishNumber": '',
                "nextPayDate": currentDateTime,
                "executeCounter": executeCounter,
                "interestRate": 0,
                "interestAmt": 0,
                "instalmentNumber": 0,
                "instalmentIncrement":0,
                "downPayment": 1000,
                "imei": '',
                "searchType": "product",
                "opt": 'sale',
                "storeId": 1,
                "removeImei": '',
                "serviceCharge": 0,
                "extraDiscount": 0,
                "refName": '',
                "refPhone": '',
                "refAddress": '',
                "isAdvance": 0,
                "discountType": discountType,
                "offer": 0,
                "source": "Invoice",
                "fieldRecord": '',
                "attributeRecord": '',
                "payMethodTwo": 1000,
                "payCashTwo": 0,
                "status": 'Sold',
                "store": 'Main Store',
                "reminderId": "",
                "serialNo": "",
                "invoiceType":"0"
            };
            salesData.push(salesObj);
            executeCounter++;

            console.log("salesDataStore:", salesObj);
        });


        var tx = db.transaction(['metaposSales'], 'readwrite');
        var salesDataStore = tx.objectStore('metaposSales');
        console.log("timestamp x:", timestamp);
        lastInvoice = timestamp;
        salesDataStore.add({ text: salesData, timestamp: lastInvoice });


        // remove all data
        $("#tblCart").find("tr").remove();
        app.reloadCart();
    };


    // int row index
    app.reIntSL = function () {
        var sl = 1;

        $("#cart_body tr").each(function (index) {

            $(this).find('.itemSL').text(sl);
            sl++;
        });
    };


    // Create customer 
    function CreateCustomer(db) {
        var generator = new IDGenerator();
        var id = generator.generate();
        console.log("Id:", id);
        var customer = {
            "id": id,
            "cusID": "",
            "name": $('#txtCustomerNameMdl').val().trim(),
            "phone": $('#txtCustomerMobileMdl').val().trim(),
            "address": $('#txtCustomerAddressMdl').val().trim(),
            "email": $('#txtCustomerEmailMdl').val().trim(),
            "balance": parseFloat($('#txtCustomerBalanceMdl').val().trim()).toFixed("2"),
            "type": "0",
            "status": true
        };

        // Start a database transaction and get the notes object store
        let tx = db.transaction(['metaposCustomers'], 'readwrite');
        let store = tx.objectStore('metaposCustomers');
        // Put the sticky note into the object store
        let customerData = { text: customer, timestamp: timestamp };
        store.add(customerData);


        // Wait for the database transaction to complete
        tx.oncomplete = function () { console.log('stored note!'); }
        tx.onerror = function (event) {
            alert('error storing note ' + event.target.errorCode);
        };

        $('#mdlAddCustomer').modal('hide');

        getCustomers(db);


        //$('#txtCustomer').val(id);
    };


    // reload shoping cart 

    app.reloadCart = function () {
        app.reIntSL();
        app.calculateCart();


        var rowCount = $('#tblCart tr').length;
        $('.lblTotalCartQty').attr("totalqty", rowCount);
        $(".lblTotalCartQty").html(rowCount);
    };


    // get curent date
    app.getCurrentDate = function () {

        var datetimeNow = moment();
        var currentDateTime = datetimeNow.tz('Asia/Dhaka').format('dd-MMM-yyyy');

        return currentDateTime;
    };


    // get curent date
    app.getCurrentTime = function () {

        var datetimeNow = moment();
        var currentDateTime = datetimeNow.tz('Asia/Dhaka').format('hh:mm:ss');

        return currentDateTime;
    };


    // int doc here
    $(document).ready(function () {

        console.log(app.getCurrentDate());
        console.log(app.getCurrentTime());


        // calculate account when text box has changed 
        $(document).on('input', '.hasAccount', function () {

            var parent = $(this).parent().parent();

            var itemPrice = parent.find('.itemUnitPrice').val();
            var itemQty = parent.find('.txtItemQty').val();

            parent.find('.sub_total').text(parseFloat(itemPrice * itemQty).toFixed(2));


            app.calculateCart();
        });


        // discount type tracker
        $("#txtDiscountType").change(function () {
            app.calculateCart();
        });


        // remove item form cart
        $(document).on("click", ".btnRemoveItem", function () {
            var row_number = $(this).closest("tr")[0].rowIndex;

            //$(this).parents("tr").fadeOut("slow");


            $(this).parents("tr").fadeOut("slow", function () {

                $("#cart_body tr:eq(" + row_number + ")").remove();
                app.reloadCart();
            });
        });


        // Create customer  <Open Modal>
        $(document).on("click", "#btnAddCustomer", function () {

            $('#mdlAddCustomer').modal({
                keyboard: false
            });


        });


        // Create new customer <Same Local DB>
        $(document).on("click", "#btnCreateCustomer", function () {
            CreateCustomer(db);
        });


        // Empty product Cart 
        $(document).on("click", "#btnEmptyCart", function () {

            if (confirm("Are you sure!")) {
                $("#tblCart").find("tr").remove();
                app.reloadCart();
            }


        });


        // Truck Dropdown Product
        //$('input.txtProduct').on('select:flexdatalist', function (event, set, options) {
        //    //console.log(set.value);
        //    //console.log(set.text);

        //    console.log("loggg..");

        //    app.addToCart(set.code);


        //});


        // truck chnaged 
        $('input.flexdatalist').flexdatalist();
        $('input.flexdatalist.txtProduct').on('change:flexdatalist', function (event, set, options) {
            alert(set.value);
        });




        // Truck Droupdown Customer
        $('input.txtCustomer').on('select:flexdatalist', function (event, set, options) {

            console.log("customer selected");
            $('#txtCustomerTmpId').val(set.id);
            $('#txtCustomerId').val(set.cusID);
            $('#txtCustomerName').val(set.name);
        });


        console.log(navigator.onLine);
        //app.createCustomers();
        //app.createProducts();

        window.addEventListener('offline', function (e) {
            console.log('offline');
        }, false);
        window.addEventListener('online', function (e) {
            alert('online');
        }, false);





    });





})(jQuery);



function resetInvoiceOffline() {

    $('.txtCustomer').val();
    $('#txtCartTotal').val("0.00");
    $('#txtDiscount').val("0");
    $('#txtGrandTotal').val("0.00");
    $('#txtPayToday').val("");
    $('#txtBalance').val("0.00");

};

function IDGenerator() {

    this.length = 8;
    this.timestamp = +new Date;

    var _getRandomInt = function (min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    this.generate = function () {
        var ts = this.timestamp.toString();
        var parts = ts.split("").reverse();
        var id = "";

        for (var i = 0; i < this.length; ++i) {
            var index = _getRandomInt(0, parts.length - 1);
            id += parts[index];
        }

        return id;
    };
}


$('#btnAddTo').click(function () {
    console.log("clikedd:", $("#txtProductCode").val(), ":", $("#txtProduct").text());
    app.addToCart($("#txtProduct").val());
});



function printSmallInvoice() {
    window.open("invoice.html?type=0", '', 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
}

function printNormalInvoice() {
    window.open("invoice.html?type=1", '_blank', 'location=yes,height=570,width=800,scrollbars=yes,status=yes');
}



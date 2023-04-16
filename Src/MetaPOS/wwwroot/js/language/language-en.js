

function englishLocalization() {


    var businessType = columnAccess[0]["businessType"];
    console.log("businessType:", businessType);



    $('.btnLinkInvoice').text("Create Invoice");
    $('#linkInventory a span').text("Inventory");
    $('.btnLinkPurchase a span').text("Purchase");
    $('.btnLinkStock a span').text("Stock");
    $('.btnLinkPackage a span').text("Package");
    $('.btnLinkWarranty a span').text("Warranty");
    $('.btnLinkReturn a span').text("Return");
    $('.btnLinkDamage a span').text("Damage");
    $('.btnLinkWarning a span').text("Warning");
    $('.btnLinkExpiry a span').text("Expiry");


    $('#linkSale a span').text("Sales");
    $('.btnLinkSaleInvoice a span').text("Invoice");
    $('.btnLinkCustomer a span').text("Customer");
    $('.btnLinkService a span').text("Service");
    $('.btnLinkQuotation a span').text("Quotation");
    $('.btnLinkServicing a span').text("Servicing");
    $('.btnLinkDueReminder a span').text("Installment");
    $('.btnLinkToken a span').text("Token");


    $('#linkAccounting a span').text("Accounting");
    $('.btnLinkSupply a span').text("Supply");
    $('.btnLinkReceive a span').text("Receive");
    $('.btnLinkExpense a span').text("Expense");
    $('.btnLinkSalary a span').text("Salary");
    $('.btnLinkBanking a span').text("Banking");


    $('#linkReport a span').text("Reports");
    $('.btnLinkPurchaseReport a span').text("Purchase");
    $('.btnLinkSlip a span').text("Invoice");
    $('.btnLinkInventoryReport a span').text("Inventory");
    $('.btnLinkTransaction a span').text("Transactions");
    $('.btnLinkProfitLoss a span').text("Profit/Loss");
    $('.btnLinkSummary a span').text("Summary");
    $('.btnLinkAnalytic a span').text("Analytic");
    $('.btnLinkSupplierCommission a span').text("Supplier Commission");


    $('#linkPromotion a span').text("Promotion");
    $(".btnLinkOffer a span").text("Offer");
    $(".btnLinkSMS a span").text("SMS");
    $(".btnLinkEmail a span").text("Email");


    $('#linkWebsite a span').text("Website");
    $('.btnLinkEcommerce a span').text("Ecommerce");
    $('#btnLinkWeb a span').text("Website");


    $('#linkRecord a span').text("Records");
    $('.btnLinkWarehouse a span').text("Store");
    $('.btnLocation a span').text("Location");
    $('.btnManufacturer a span').text("Manufacturer");
    $('.btnLinkSupplier a span').text("Supplier");
    $('.btnLinkCategory a span').text("Category");
    $('.btnLinkUnitMeasurement a span').text("Measurement");
    $('.btnLinkField a span').text("Field");
    $('.btnLinkAttribute a span').text("Attribute");
    $('.btnLinkBank a span').text("Bank");
    $('.btnLinkCard a span').text("Card");
    $('.btnLinkParticular a span').text("Particular");
    $('.btnLinkStaff a span').text("Staff");
    $('.btnLinkServiceType a span').text("ServiceType");

    $('#linkSettings a span').text("Settings");
    $('.btnLinkUser a span').text("User");
    $('.btnLinkProfile a span').text("Profile");
    $('.btnLinkSubscription a span').text("Subscription");
    $('.btnLinkPayment a span').text("Payment");
    $('.btnLinkSecurity a span').text("Security");
    $('.btnLinkSupport a span').text("Support");
    $('.btnLinkSetting a span').text("Setting ");
    $('.btnLinkSmsConfig a span').text("SMS Config");


    $('.btnOffline').text("Offline");


    $('.sale-amount').text("Today Sale");
    $('.new-invice').text("New Invoice");
    $('.sale-item').text("Sale Qty");
    $('.new-customer').text("New Customer");
    $('.last-four-month-sale').text("Last "+ columnAccess[0]["monthWaseSalesReport"]+" month Sales");
    $('.last-month-popular-product').text("This month popular product");


    //$('.update-status').text(" Updated");
    $('.accounts').text("Accounts");
    $('.lang-customers').text("New customer");
    $('.lang-misc').text("Misc. cost");
    $('.lang-referal-tab').text("Referrel");
    $('.lang-notes').text("Notes");
    $('.lang-cart-total').text("Cart total");
    $('.lang-misc-cost').text("+ Misc cost");
    $('.lang-vat').text("+ Vat");
    $('.lang-discount').text("- Discount");
    $('.lang-more-discount').text("More disc");
    $('.lang-interest').text("+ Interest");
    $('.lang-total').text("Total");
    $('.lang-previous-balance').text("+ Pre Balance");
    $('.lang-return-amount').text("- Return");
    $('.lang-return-paid').text("+ Paid");
    $('.lang-total-balance').text("Total Balance");
    $('.lang-sales-person').text("Sales Person");
    $('.lang-referal').text("Referrel");
    $('.lang-pay-card').text("Pay");
    $('.lang-token').text("Token");
    $('.lang-pay-date').text("Pay Date");
    $('.lang-pay-type').text("Pay Type");
    $('.lang-pay').text("Pay");
    $('.lang-maturity-date').text("Maturity Date");
    $('.lang-next-pay-date').text("Next Date");
    $('.lang-next-start-date').text("Start Date");
    $('.lang-installment-number').text("Number");
    $('.lang-installment-amount').text("Payment");
    $('.lang-interest-rate').text("Interest Rate");
    $('.lang-due-change').text("Due");
    $('.lang-add-as-disc').text("Add as Disc");
    $('.lang-paper-size').text("Paper size");
    $('.lang-current-balance').text("Current Balance");
    $('.lang-pay-advance').text("Pay Advance");
    $('.lang-save').text("Save");
    $('.lang-update').text("Update");
    $('.lang-suspend').text("Suspend");
    $('.lang-print-invoice').text("Print");
    $('.lang-print-challan').text("Challan");
    $('.lang-print-receipt').text("Receipt");


    $('.lang-loading').text("Loading");
    $('.lang-unloading').text("Unloading");
    $('.lang-shipping').text("Shipping");
    $('.lang-carrying').text("Carrying");
    $('.lang-service').text("Service");
    $('.lang-referral-name').text("Name");
    $('.lang-referral-phone').text("Phone");
    $('.lang-referrel-address').text("Address");
    $('.lang-invoice-notes').text("Write notes");

    /*Purchase*/
    $('.lang-total-price').text("Total Price");
    $('.lang-qty').text("Qty");
    $('.lang-Price').text("Price");
    $('.lang-product-title').text("Product Title");
    $('.lang-purchase').text("Purchase");
    $('.lang-add-update-product').text("Add / Update Product");
    $('.lang-store').html("Store <span class='required'>*</span>");
    $('.lang-location').text("Location");
    $('.lang-manufacturer-brand').text("Manufacturer/Brand");
    $('.lang-supplier-vendor').html("Supplier / Vendor <span class='required'>*</span>");
    $('.lang-category').html("Category <span class='required'>*</span>");

    if (businessType.trim() == 'electronics')
        $('.lang-product-name').html("Product Model <span class='required'>*</span>");
    else {
        $('.lang-product-name').html("Product Name <span class='required'>*</span>");
    }

    //if (businessType.trim() == 'electronics')
    //    $('.lang-product-code').html("Serial Number <span class='required'>*</span>");
    //else
        $('.lang-product-code').html("Product Code <span class='required'>*</span>");
    

    $('.lang-unit').text("Unit");
    $('.lang-add-exist-qty').html("Add Qty + Exists <span class='required'>*</span>");
    $('.lang-free-qty').text("Free Qty");
    $('.lang-total-purchase-amount').text("Total Purchase Amount");
    $('.lang-warning-qty').text("Warning Qty");
    $('.lang-set-cost-price').html("Buy Price <span class='required'>*</span>");
    $('.lang-set-company-price').text("Company Price");
    $('.lang-wholesale-price').text("Wholesale Price");
    $('.lang-sale-price').html("Retail Price <span class='required'>*</span>");
    /*$('.lang-total-purchase-amount').text("Total Purchase Amount");*/
    $('.lang-total-stock-amount').text("Total Stock Amount");
    $('.lang-size').text("Size");
    $('.lang-warranty').text("Warranty");

    if (businessType.trim() == 'gadget')
        $('.lang-imei').html("Serial <span class='required'>*</span>");
    else {
        $('.lang-imei').html("IMEI <span class='required'>*</span>");
    }
    //$('.lang-imei').text("IMEI");
    $('.lang-sku').text("SKU");
    $('.lang-engine-number').text("Engine Number");
    $('.lang-cecish-number').text("Cecish Number");
    $('.lang-tax').text("Tax");
    $('.lang-received-date').text("Received Date");
    $('.lang-expiry-date').text("Expiry Date");
    $('.lang-batch-no').text("Batch No");
    $('.lang-serial-no').text("Serial No");
    $('.lang-supplier-payment').text("Supplier Payment");
    $('.lang-shipment-status').text("Shipment Status");
    $('.lang-upload-file').text("Upload File");
    $('.lang-type').text("Type");
    $('.lang-inventory-field').text("Field");
    $('.lang-inventory-attribute').text("Attribute");
    $('.lang-import-products').text("Import New Products");
    $('.lang-product-list').text("Product List");

    if (businessType.trim() == 'gadget')
        $('.lang-purchase-code').text("Purchase No.");
    else {
        $('.lang-purchase-code').text("Purchase Code");
    }
    
    $('.lang-purchase-date').text("Purchase Date");
    $('.lang-purchase-comment').text("Purchase Comment");
    $('.lang-schedule-payment').text("Schedule Payment");
    $('.lang-schedule-comment').text("Schedule Comment");
    $('.total-received').text("Total Received");
    $('.lang-track-received-comment').text("Track Received Comment");

    /* Customer */
    $('.lang-customer-title').text("Customers");
    $('.lang-add-customer').text("Add Customer");
    $('.lang-customer-name').text("Name");
    $('.lang-add-new-customer').text("New Customer");
    $('.lang-customer-type').text("Customer Type");
    $('.lang-account-no').text("Account No");
    $('.lang-customer-phone').text("Phone");
    $('.lang-customer-address').text("Address");
    $('.lang-email-email').text("Email");
    $('.lang-customer-designation').text("Designation");
    $('.lang-customer-notes').text("Notes");
    $('.lang-customer-pay-with-installment').text("Installment");
    $('.lang-customer-add-advance').text("Add Advance");
    $('.lang-customer-advance-amount').text("Advance Amount");
    $('.lang-customer-date').text("Date");
    $('.lang-customer-add-opening-due').text("Add Opening Due");
    $('.lang-customer-opening-due-amount').text("Opening Due Amount");
    $('.lang-customer-payment').text("Payment");
    $('.lang-customer-amount').text("Amount");
    $('.lang-customer-retail').text("Retailer");

    if (businessType.trim() == 'electronics')
        $('.lang-customer-wholesale').text("Dealer");
    else {
        $('.lang-customer-wholesale').text("Wholesaler");

    }




    /** Service **/
    $('.lang-service-title').text("Service");
    $('.lang-add-service').text("Add Service");
    $('.lang-service-list').text("Service List");
    $('.lang-service-type').text("Type");
    $('.lang-service-name').text("Name");
    $('.lang-service-wholesale-price').text("Wholesale Price");
    $('.lang-service-retail-price').text("Retail Price");


    /** Servicing **/
    $('.lang-servicing-customer').text("Customer");
    $('.lang-servicing-id').text("ID");
    $('.lang-servicing-product-name').text("Product Name");
    $('.lang-servicing-imei').text("IMEI");
    $('.lang-servicing-description').text("Description");
    $('.lang-servicing-supplier').text("Supplier");
    $('.lang-servicing-delivery-date').text("Delivery Date");
    $('.lang-servicing-total-amount').text("Total Amount");
    $('.lang-servicing-pay-cost').text("Pay Cost");
    $('.lang-servicing-servicing-report').text("Servicing Report");
    $('.lang-servicing-servicing-Id').text("Servicing ID");

    /* Installment */
    $('.lang-instllment-installemts').text("Installments");
    $('.lang-installment-installment-list').text("Instalment List");
    $('.lang-installment-all-installment').text("All Installment");
    $('.lang-installment-all-hostory').text("All Hostory");
    $('.lang-intallment-total-paid').text("Total Paid");
    $('.lang-total-balance').text("Total Balance");
    $('.lang-installment-pay-date').text("Pay Date");
    $('.lang-installment-pay').text("Pay Installment");
    $('.lang-installment-to').text("To");

    /* Supply */
    $('.lang-accounting-supply').text("Supplier Payment");
    $('.btn-supply-add-supply').val("Add Supplier");
    $('.lang-accounting-opening-balance').text("Opening Balance");
    $('.lang-supply-particular').text("Particular");
    $('.lang-supply-supplier-payment').text("Supplier Payment");
    $('.lang-accounting-supplier').text("Supplier");
    $('.lang-supply-description').text("Description");
    $('.lang-supply-amount').text("Amount");
    $('.lang-supply-give-payment').text("Give Payment");
    $('.lang-supply-date').text("Date");
    $('.lang-supply-supply-reprot').text("Reprot");

    $('.lang-accounting-any-date').val("Any Date");
    $('.lang-accounting-by-supplier').text("By Supplier");

    /* Receive */
    $('.lang-accounting-receive').text("Receive");
    $('.lang-accounting-add-receive').text("Add Receive");
    $('.lang-accounting-staff').text("Staff");
    $('.lang-accounting-descrition').text("Descrition");
    $('.lang-accounting-reived-date').text("Recived Date");
    $('.lang-accounting-add-expense').text("Add Expense");
    $('.lang-accounting-supplier').text("Supplier");
    $('.lang-accounting-report').text("Receive Report");

    /* Token */
    $('.lang-token-token').text("Token");
    $('.lang-token-token-report').text("Report");


    /* Action Button */
    $('.btn-close').text("Close");
    $('.btn-save').text("Save");
    $('.btn-update').text("Update");
    $('.btn-reset').text("Reset");

    /* Expense */
    $('.lang-accounting-particular').text("Particular");
    $('.lang-accounting-description').text("Description");
    $('.lang-accounting-amount').text("Amount");
    $('.lang-accounting-expenseDate').text("Expense Date");
    $('.lang-accounting-store').text("Store");
    $('.lang-accounting-user').text("User");
    $('.lang-accounting-particular').text("Particular");
    $('.lang-accounting-from').text("From");
    $('.lang-accounting-to').text("To");
    $('.lang-accounting-search').text("Search");
    $('.lang-accounting-expense').text("Expense");
    $('.lang-accounting-add-expense').text("Add Expense");
    $('.lang-accounting-expense-report').text("Expense Report");



    /*Salary*/
    $('.lang-accounting-select-staff').text("Select Staff");
    $('.lang-accounting-details-of-salary').text("Details of Salary");
    $('.lang-accounting-salary-amount').text("Salary Amount");
    $('.lang-accountig-salary-date').text("Salary Date");
    $('.lang-accounting-by-btaff').text("By Staff");
    $('.lang-accounting-date-from').text("Date From");
    $('.lang-accounting-date-to').text("Date To");
    $('.lang-accounting-salary').text("Salary");
    $('.lang-accounting-add-salary').text("Add Salary");
    $('.lang-accounting-salary-report').text("Salary Report");

    /*Banking*/
    $('.lang-accounting-bank-name').text("Bank Name");
    $('.lang-accounting-withdraw-date').text("Withdraw Date");
    $('.lang-accounting-diposit-date').text("Diposit Date");
    $('.lang-accounting-add-bank-diposit').text("Add Bank Diposit");
    $('.lang-accounting-add-bank-withdraw').text("Add Bank Withdraw");
    $('.lang-accounting-banking').text("Banking");
    $('.lang-accounting-banking-report').text("Banking Report");



    /*Setting*/
    $('.lang-setting-security').text("Security");
    $('.lang-setting-edit-user-settings').text("Edit User Settings");
    $('.lang-setting-edit-privacy-settings').text("Edit Privacy Settings");
    $('.lang-setting-store-name').text("Store Name");
    $('.lang-setting-user-name').text("User Name");
    $('.lang-setting-user-email').text("User Email");
    $('.lang-setting-current-password').text("Current Password");
    $('.lang-setting-new-password').text("New Password");
    $('.lang-setting-confirm-password').text("Confirm Password");
    $('.lang-setting-update-your-profile').text("Update Your Profile");
    $('.lang-setting-company-name').text("Company Name");
    $('.lang-setting-profile').text("Profile");
    $('.lang-setting-phone').text("Phone");
    $('.lang-setting-mobile').text("Mobile");
    $('.lang-setting-business-owner-number').text("Business Owner Number");
    $('.lang-setting-vat-reg-no').text("Vat Reg No.");
    $('.lang-setting-tax-id-no').text("Tax-Id-No");
    $('.lang-setting-url-path').text("Url Path");
    $('.lang-setting-invoice-header').text("Invoice Header");
    $('.lang-setting-invoice-footer').text("Invoice Footer");

    /*
   $('.lang-setting-invoice-footer').text("Business Owner Number");
   $('.lang-setting-eidt-company-logo').text("EDIT COMPANY LOGO");
   */

    /*Return & Damage*/
    $('.lang-inventory-product-code').text("Product Code");
    $('.lang-inventory-add-product').text("Add a Product");
    $('.lang-inventory-print-your-barcode').text("Print Your Barcode");
    $('.lang-inventory-show-all-products').val("Show all products");
    $('.lang-inventory-print-all-stock').val("Print All Stock");
    $('.lang-inventory-print-barcode').val("Print Barcode");
    $('.lang-inventory-export').text("Export");
    $('.lang-inventory-import').text("Import");
    $('.lang-inventory-store').text("Store");
    $('.lang-inventory-supplier').text("Supplier");
    $('.lang-inventory-category').text("Category");
    $('.lang-inventory-from').text("From");
    $('.lang-inventory-to').text("To");
    $('.lang-inventory-qty').text("Qty");
    $('.lang-inventory-imei').text("IMEI");
    $('.lang-inventory-package-name').text("Package Name");
    $('.lang-inventory-wholesale-price').text("Wholesale Price");
    $('.lang-inventory-sale-price').text("Sale price");
    $('.lang-inventory-package').text("Package");
    $('.lang-inventory-package-list').text("Package List");
    $('.lang-inventory-add-update-package').text("Add/Update Package");
    $('.lang-inventory-stock').text("Stock");
    $('.lang-inventory-stock-report').text("Stock Report");
    $('.lang-inventory-product-title').text("Product Title");
    $('.lang-inventory-price').text("Price");
    $('.lang-inventory-warranty').text("Warranty");
    $('.lang-inventory-warranty-history').text("Warranty History");
    $('.lang-inventory-return').text("Return");
    $('.lang-inventory-back-to-return').text("Back To Return");
    $('.lang-inventory-search-by-date').text("Search By Date");
    $('.lang-inventory-damage').text("Damage");
    $('.lang-inventory-add-damage').text("Add Damage");
    $('.lang-inventory-stock-warning-qty').text("Stock Warning Qty");
    $('.lang-invenroty-warning-stock-history').text("Warning Stock History");
    $('.lang-inventory-expiry').text("Expiry");
    $('.lang-inventory-expired-product-history').text("Expired Product History");

    $('.lang-sales-quotation').text("Quotation");
    $('.lang-sales-quotation-list').text("Quotation List");
    $('.lang-sales-servicing').text("Servicing");
    $('.lang-sales-to').text("To");

    $('.lang-reports-purchase-report').text("Purchase Report");
    $('.lang-reports-invoice-report').text("Invoice Report");
    $('.lang-reports-inventory-report').text("Inventory Report");
    $('.lang-reports-transaction').text("Transaction");
    $('.lang-reports-supplier-commission').text("Supplier Commission");
    $('.lang-reports-profit-loss').text("Profit/Loss");
    $('.lang-reports-loss-profit-histroy').text("LOSS/PROFIT HISTORY");
    $('.lang-reports-summary').text("Summary");
    $('.lang-reports-sales-sammary').text("Sales Summary");
    $('.lang-reports-cash-summary').text("Cash Summary");
    $('.lang-reports-accounts-summary').text("Accounts Summary");
    $('.lang-reports-analytic-reports').text("Analytic Reports");
    $('.lang-reports-store').text("Store");
    $('.lang-reports-from').text("From");
    $('.lang-reports-to').text("To");


    $('.lang-record-store').text("Store");
    $('.lang-record-store-list').text("Store List");
    $('.lang-record-store-name').text("Store Name");
    $('.lang-record-show').text("Show");
    $('.lang-record-entries').text("Entries");
    $('.lang-record-add-store').text("Add Store");

    $('.lang-record-location').text("Location");
    $('.lang-record-add-location').text("Add Location");
    $('.lang-record-manufacturer').text("Manufacturer");
    $('.lang-record-add-manufacturer').text("Add Manufacturer");
    $('.lang-record-manufacturer-list').text("Manufacturer List");
    $('.lang-record-supplier').text("Supplier");
    $('.lang-record-add-supplier').text("Add Supplier");
    $('.lang-record-supplier-list').text("Supplier List");
    $('.lang-record-company-name').text("Company Name");
    $('.lang-record-contact-name').text("Contact Name");
    $('.lang-record-supplier-code').text("Supplier Code");
    $('.lang-record-contact-designation').text("Contact Designation");
    $('.lang-record-contact-phone').text("Contact Phone");
    $('.lang-record-contact-email').text("Contact Email");
    $('.lang-record-discount').text("Discount (in %)");
    $('.lang-record-address').text("Address");
    $('.lang-record-category').text("Category");
    $('.lang-record-add-category').text("Add Category");
    $('.lang-record-category-list').text("Category List");
    $('.lang-record-category-name').text("Category Name");
    $('.lang-record-unit-of-measurement').text("Unit of Measurement");
    $('.lang-record-add-unit').text("Add Unit");
    $('.lang-record-unit-list').text("Unit List");
    $('.lang-record-name').text("Name");
    $('.lang-record-ratio').text("Ratio");
    $('.lang-record-field').text("Field");
    $('.lang-record-add-new-field').text("Add New Field");
    $('.lang-record-field-list').text("Field List");
    $('.lang-record-attribute').text("Attribute");
    $('.lang-record-add-attribute').text("Add Attribute");
    $('.lang-record-attribute-list').text("Attribute List");
    $('.lang-record-field-name').text("Field Name");
    $('.lang-record-attribute-name').text("Attribute Name");
    $('.lang-record-card-name').text("Card Name");
    $('.lang-record-add-card').text("Add Card");
    $('.lang-record-card-discount').text("Card Discount");
    $('.lang-record-bank-name').text("Bank Name");
    $('.lang-record-add-bank').text("Add Bank");
    $('.lang-record-bank-name-list').text("Bank Name List");
    $('.lang-record-card').text("Card");
    $('.lang-record-card-list').text("Card List");
    $('.lang-record-particular').text("Particular");
    $('.lang-record-add-particular').text("Add Particular");
    $('.lang-record-particular-list').text("Particular List");
    $('.lang-record-staff').text("Staff");
    $('.lang-record-add-new-staff').text("Add New Staff");
    $('.lang-record-staff-list').text("Staff List");
    $('.lang-record-phone').text("Phone");
    $('.lang-record-sex').text("Sex");
    $('.lang-record-date-of-birth').text("Date of birth");
    $('.lang-record-department').text("Department");
    $('.lang-record-service-type').text("Service Type");
    $('.lang-record-add-service-type').text("Add Service Type");
    $('.lang-record-service-type-list').text("Service Type List");
    $('.lang-record-service-type-name').text("Service Type Name");

}
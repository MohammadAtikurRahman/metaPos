


function banglaLocalization() {
    console.log("bangla loaded");

    var businessType = columnAccess[0]["businessType"];



    


    $('.sale-amount').text("আজকে বিক্রি ");
    $('.new-invice').text("নতুন ইনভয়েস");
    $('.sale-item').text("বিক্রি আইটেম");
    $('.new-customer').text("নতুন কাস্টমার");
    $('.last-four-month-sale').text("গত " + columnAccess[0]["monthWaseSalesReport"]  + " মাসে বিক্রির গ্রাফ চিত্র");
    $('.last-month-popular-product').text("এই মাসে সবচেয়ে বেশি বিক্রিত পণ্য");


    $('.update-status').text(" আপডেট");
    $('.accounts').text("একাউন্টস");
    $('.lang-customers').text("নতুন কাস্টমার");
    $('.lang-misc').text("");
    $('.lang-referal-tab').text("রেফারেল");
    $('.lang-notes').text("নোটস");

    $('.lang-cart-total').text("কার্ট টোটাল");
    $('.lang-misc-cost').text("+ বিবিধ খরচ");
    $('.lang-vat').text("+ ভ্যাট");
    $('.lang-discount').text("- ডিসকাউন্ট");
    $('.lang-more-discount').text("আরো ডিসকাউন্ট");
    $('.lang-interest').text("+ ইন্টারেস্ট");
    $('.lang-total').text("সর্বমোট");
    $('.lang-previous-balance').text("+ পূর্বের ব্যালান্স");
    $('.lang-return-amount').text("- ফেরত টাকা");
    $('.lang-return-paid').text("+ পরিশোধিত টাকা");
    $('.lang-total-balance').text("মোট ব্যালান্স");
    $('.lang-sales-person').text("বিক্রেতা");
    $('.lang-referal').text("রেফেরাল");
    $('.lang-pay-card').text("পে কার্ড");
    $('.lang-token').text("টোকেন");
    $('.lang-pay-date').text("প্রদানের তারিখ");

    $('.lang-pay-type').text("বেতনের ধরণ");
    $('.lang-pay').text("বেতন");
    $('.lang-maturity-date').text("ম্যাচুরিটি তারিখ");
    $('.lang-next-pay-date').text("পরবর্তী তারিখ");
    $('.lang-next-start-date').text("শুরুর তারিখ");
    $('.lang-installment-number').text("কিস্তির সংখ্যা");
    $('.lang-installment-amount').text("কিস্তির পরিমাণ");
    $('.lang-interest-rate').text("ইন্টারেস্ট রেট");
    $('.lang-due-change').text("বাকি টাকা");
    $('.lang-add-as-disc').text("অতিরিক্ত ছাড়");
    $('.lang-paper-size').text("পেপার সাইজ");
    $('.lang-current-balance').text("বর্তমান হিসাব");
    $('.lang-pay-advance').text("অগ্রিম জমা");
    $('.lang-save').text("সেভ");
    $('.lang-update').text("আপডেট");
    $('.lang-suspend').text("স্থগিত");
    $('.lang-print-invoice').text("প্রিন্ট");
    $('.lang-print-challan').text("চালান");
    $('.lang-print-receipt').text("রসিদ");

    $('.lang-loading').text("মালামাল খালাস");
    $('.lang-unloading').text("মালামাল বোঝাই");
    $('.lang-shipping').text("শিপিং");
    $('.lang-carrying').text("পরিবহণ খরচ");
    $('.lang-service').text("সার্ভিস");
    $('.lang-referral-name').text("নাম");
    $('.lang-referral-phone').text("ফোন");
    $('.lang-referrel-address').text("ঠিকানা");
    $('.lang-invoice-notes').text("নোট লিখুন");

    /*Purchase*/
    $('.lang-total-price').text("মোট মূল্য");
    $('.lang-qty').text("সংখ্যা");
    $('.lang-price').text("মূল্য");
    $('.lang-product-title').text("পণ্যর নাম");
    $('.lang-purchase').text("পণ্য কিনুন");
    $('.lang-add-update-product').text("পণ্য যুক্ত / আপডেট করুন");
    $('.lang-store').html("দোকান <span class='required'>*</span>");
    $('.lang-location').text("অবস্থান");
    $('.lang-manufacturer-brand').text("উৎপাদক/ব্র্যান্ড");
    $('.lang-supplier-vendor').html("সরবরাহকারী / বিক্রেতা <span class='required'>*</span>");
    $('.lang-category').html("ক্যাটাগরি <span class='required'>*</span>");

    if (businessType.trim() == 'electronics')
        $('.lang-product-name').html("পণ্যর মডেল <span class='required'>*</span>");
    else {
        $('.lang-product-name').html("পণ্যর নাম <span class='required'>*</span>");
    }

    //if (businessType.trim() == 'electronics')
    //    $('.lang-product-code').html("সিরিয়াল নম্বর <span class='required'>*</span>");
    //else
        $('.lang-product-code').html("পণ্য কোড <span class='required'>*</span>");
    

    $('.lang-unit').text("ইউনিট/একক");
    $('.lang-add-exist-qty').html("পরিমাণ যুক্ত করুন + উপস্থিত আছে <span class='required'>*</span>");
    $('.lang-free-qty').text("ফ্রি পণ্যর সংখ্যা");
    $('.lang-total-purchase-amount').text("মোট ক্রয়কৃত মূল্য");
    $('.lang-warning-qty').text("সতর্কীকরণ পরিমাণ");
    $('.lang-set-cost-price').html("পণ্যটির ক্রয়মূল্য <span class='required'>*</span>");
    $('.lang-set-company-price').text("পণ্যটির কোম্পানি মূল্য");
    $('.lang-wholesale-price').text("পণ্যটির পাইকারি মূল্য");
    $('.lang-sale-price').html("পণ্যটির বিক্রয় মূল্য <span class='required'>*</span>");
    /*$('.lang-total-purchase-amount').text("মোট ক্রয়কৃত টাকা");*/
    $('.lang-total-stock-amount').text("মোট স্টকের পরিমাণ");
    $('.lang-size').text("সাইজ");
    $('.lang-warranty').text("ওয়ারেন্টি");

    if (businessType.trim() == 'electronics')
        $('.lang-imei').html("সিরিয়াল <span class='required'>*</span>");
    else {
        $('.lang-imei').html("আইএমইআই <span class='required'>*</span>");
    }

    $('.lang-sku').text("SKU");
    $('.lang-engine-number').text("ইঞ্জিন নাম্বার");
    $('.lang-cecish-number').text("চেসিস নাম্বার");
    $('.lang-tax').text("ট্যাক্স");
    $('.lang-received-date').text("রিসিভ তারিখ");
    $('.lang-expiry-date').text("মেয়াদ শেষের তারিখ");
    $('.lang-batch-no').text("ব্যাচ নং");
    $('.lang-serial-no').text("সিরিয়াল নং");
    $('.lang-supplier-payment').text("সাপ্লায়ার পেমেন্ট");
    $('.lang-shipment-status').text("চালানের অবস্থা");
    $('.lang-upload-file').text("ফাইল আপলোড করুন");
    $('.lang-type').text("টাইপ");
    $('.lang-inventory-field').text("ফিল্ড");
    $('.lang-inventory-attribute').text("এট্রিবিউট");
    $('.lang-import-products').text("পণ্য ইমপোর্ট করুন");
    $('.lang-product-list').text("পণ্যর লিস্ট");

    if (businessType.trim() == 'gadget')
        $('.lang-purchase-code').text("ক্রয়কৃত নং");
    else {
        $('.lang-purchase-code').text("ক্রয়কৃত কোড");
    }

    $('.lang-purchase-code').text("ক্রয়কৃত কোড");

    $('.lang-purchase-date').text("ক্রয়ের তারিখ");
    $('.lang-purchase-comment').text("এই ক্রয় সম্পর্কে মন্তব্য");
    $('.lang-schedule-payment').text("পরবর্তী পরিশোধের তারিখ");
    $('.lang-schedule-comment').text("পরবর্তী পরিশোধের মন্তব্য");
    $('.total-received').text("মোট প্রাপ্ত মূল্য ট্র্যাক রাখুন");
    $('.lang-track-received-comment').text("ট্র্যাক রাখুন প্রাপ্ত মন্তব্য");

    /* Customer */
    $('.lang-customer-title').text("কাস্টমার");
    $('.lang-add-customer').text("নতুন কাস্টমার");
    $('.lang-customer-name').text("নাম");
    $('.lang-add-new-customer').text("নতুন কাস্টমার");
    $('.lang-customer-type').text("কাস্টমার ধরণ");
    $('.lang-account-no').text("একাউন্ট নং");
    $('.lang-customer-phone').text("ফোন");
    $('.lang-customer-address').text("অ্যাড্রেস");
    $('.lang-email-email').text("ইমেইল");
    $('.lang-customer-designation').text("পদবী");
    $('.lang-customer-notes').text("নোটস");
    $('.lang-customer-pay-with-installment').text("কিস্তি");
    $('.lang-customer-add-advance').text("অ্যাডভান্স যোগ করুন");
    $('.lang-customer-advance-amount').text("অগ্রিম টাকা");
    $('.lang-customer-date').text("তারিখ");
    $('.lang-customer-add-opening-due').text("ওপেনিং ডিউ যুক্ত করুন");
    $('.lang-customer-opening-due-amount').text("ওপেনিং ডিউ");
    $('.lang-customer-payment').text("পেমেন্ট :");
    $('.lang-customer-amount').text("টাকা");
    $('.lang-customer-retail').text("খুচরা");

    if (businessType.trim() == 'gadget')
        $('.lang-customer-wholesale').text("ব্যবসায়ী");
    else {
        $('.lang-customer-wholesale').text("পাইকারি");
    }




    /** Service **/
    $('.lang-service-title').text("সার্ভিস");
    $('.lang-add-service').text("সার্ভিস যুক্ত করুন");
    $('.lang-service-list').text("সার্ভিসের তালিকা");
    $('.lang-service-type').text("সার্ভিসের ধরণ");
    $('.lang-service-name').text("সার্ভিস নাম");
    $('.lang-service-wholesale-price').text("পাইকারি মূল্য");
    $('.lang-service-retail-price').text("খুচরা মূল্য");



    /** Servicing **/
    $('.lang-servicing-customer').text("কাস্টমার");
    $('.lang-servicing-id').text("আইডি");
    $('.lang-servicing-product-name').text("পণ্যর নাম");
    $('.lang-servicing-imei').text("আইএমইআই");
    $('.lang-servicing-description').text("সার্ভিসের বর্ণনা");
    $('.lang-servicing-supplier').text("সরবরাহকারী নাম");
    $('.lang-servicing-delivery-date').text("বিতরণ তারিখ");
    $('.lang-servicing-total-amount').text("মোট টাকা");
    $('.lang-servicing-pay-cost').text("খরচ");
    $('.lang-servicing-servicing-report').text("সার্ভিসিং রিপোর্ট");
    $('.lang-servicing-servicing-Id').text("সার্ভিসিং আইডি");

    /* Instalment */
    $('.lang-instllment-installemts').text("কিস্তি");
    $('.lang-installment-installment-list').text("কিস্তির তালিকা");
    $('.lang-installment-all-installment').text("সমস্ত কিস্তি");
    $('.lang-installment-all-hostory').text("সমস্ত তথ্য");
    $('.lang-intallment-total-paid').text("মোট পরিশোধ");
    $('.lang-total-balance').text("মোট টাকা");
    $('.lang-installment-pay-date').text("পরিশোধের তারিখ");
    $('.lang-installment-pay').text("কিস্তি পরিশোধ করুন");
    $('.lang-installment-to').text("হতে");

    /* Supply */
    $('.lang-accounting-supply').text("সাপ্লায়ার পেমেন্ট");
    $('.btn-supply-add-supply').val("সাপ্লায়ারযুক্ত করুন");
    $('.lang-accounting-opening-balance').text("ওপেনিং ব্যালেন্স");
    $('.lang-supply-particular').text("বিশেষ খাতসমূহ");
    $('.lang-supply-supplier-payment').text("সাপ্লায়ার পেমেন্ট");
    $('.lang-accounting-supplier').text("সাপ্লায়ার");
    $('.lang-supply-description').text("বিস্তারিত বর্ণনা");
    $('.lang-supply-amount').text("সাপ্লায়ারের টাকার পরিমাণ");
    $('.lang-supply-give-payment').text("সাপ্লায়ারের টাকা প্রদান করুন");
    $('.lang-supply-date').text("তারিখ");
    $('.lang-supply-supply-reprot').text("রিপোর্ট");

    $('.lang-accounting-any-date').val("যেকোন তারিখ");
    $('.lang-accounting-by-supplier').text("বাই সাপ্লায়ার");

    /* Receive */
    $('.lang-accounting-receive').text("আয়কৃত");
    $('.lang-accounting-add-receive').text("রিসিভ যুক্ত করুন");
    $('.lang-accounting-staff').text("কর্মচারিবর্গ");
    $('.lang-accounting-descrition').text("বিস্তারিত বর্ণনা");
    $('.lang-accounting-reived-date').text("প্রাপ্ত তারিখ");
    $('.lang-accounting-add-expense').text("খরচ সংযুক্ত করুন");
    $('.lang-accounting-supplier').text("সাপ্লায়ার");
    $('.lang-accounting-report').text("রিসিভ রিপোর্ট");

    /* Token */
    $('.lang-token-token').text("টোকেন");
    $('.lang-token-token-report').text("টোকেন রিপোর্ট");

    /* Expences */
    $('.lang-accounting-particular').text("বিশেষ খাত");
    $('.lang-accounting-description').text("বিস্তারিত বর্ণনা");
    $('.lang-accounting-amount').text("পরিমাণ");
    $('.lang-accounting-expenseDate').text("খরচ তারিখ");
    $('.lang-accounting-store').text("দোকান");
    $('.lang-accounting-user').text("ব্যবহারকারী");
    $('.lang-accounting-particular').text("বিশেষ খাত");
    $('.lang-accounting-from').text("শুরুর তারিখ");
    $('.lang-accounting-to').text("শেষের তারিখ");
    $('.lang-accounting-search').text("খুজুন");
    $('.lang-accounting-expense').text("এক্সপেনস");
    $('.lang-accounting-add-expense').text("এক্সপেনস যোগ করুন");
    $('.lang-accounting-expense-report').text("এক্সপেনস রিপোর্ট");


    /*Salary*/
    $('.lang-accounting-select-staff').text("সিলেক্ট স্টাফ");
    $('.lang-accounting-details-of-salary').text("বিস্তারিত বেতন");
    $('.lang-accounting-salary-amount').text("মোট বেতন ");
    $('.lang-accountig-salary-date').text("বেতন তারিখ");
    $('.lang-accounting-by-btaff').text("স্টাফ");
    $('.lang-accounting-date-from').text("শুরুর তারিখ");
    $('.lang-accounting-date-to').text("শেষের তারিখ");
    $('.lang-accounting-salary').text("বেতন");
    $('.lang-accounting-add-salary').text("বেতন যোগ করুন");
    $('.lang-accounting-salary-report').text("বেতন রিপোর্ট");
    

    /*Banking*/
    $('.lang-accounting-bank-name').text("ব্যাংক নাম");
    $('.lang-accounting-withdraw-date').text("প্রত্যাহার তারিখ");
    $('.lang-accounting-diposit-date').text("জমার তারিখ");
    $('.lang-accounting-add-bank-diposit').text("ব্যাংক ডিপোজিট যুক্ত করুন");
    $('.lang-accounting-add-bank-withdraw').text("ব্যাংক প্রত্যাহার করুন");
    $('.lang-accounting-banking').text("ব্যাংকিং");
    $('.lang-accounting-banking-report').text("ব্যাংকিং রিপোর্ট");

    /*Setting*/
    $('.lang-setting-security').text("নিরাপত্তা");
    $('.lang-setting-edit-user-settings').text("ব্যবহারকারীর সেটিংস সম্পাদনা করুন");
    $('.lang-setting-edit-privacy-settings').text("গোপনীয়তা সেটিংস সম্পাদনা করুন");
    $('.lang-setting-store-name').text("দোকান নাম");
    $('.lang-setting-user-name').text("ব্যবহারকারীর নাম");
    $('.lang-setting-user-email').text("ব্যবহারকারীর ইমেইল");
    $('.lang-setting-current-password').text("বর্তমান পাসওয়ার্ড");
    $('.lang-setting-new-password').text("নতুন পাসওয়ার্ড");
    $('.lang-setting-confirm-password').text("নিশ্চিত পাসওয়ার্ড");
    $('.lang-setting-update-your-profile').text("আপনার প্রোফাইল আপডেট করুন");
    $('.lang-setting-company-name').text("কোম্পানির নাম");
    $('.lang-setting-profile').text("প্রোফাইল");
    $('.lang-setting-phone').text("ফোন");
    $('.lang-setting-mobile').text("মোবাইল");
    $('.lang-setting-business-owner-number').text("ব্যবসায়ের মালিকের সংখ্যা");
    $('.lang-setting-vat-reg-no').text("ভ্যাট রেজ নং");
    $('.lang-setting-tax-id-no').text("ট্যাক্স আইডি নং");
    $('.lang-setting-url-path').text("ইউআরএল পথ");
    $('.lang-setting-invoice-header').text("ইনভয়েস হেডার");
    $('.lang-setting-invoice-footer').text("ইনভয়েস ফুটার");


    /*Return & Damage*/
    $('.lang-inventory-product-code').text("প্রোডাক্ট কোড"); 
    $('.lang-inventory-add-product').text("নতুন প্রোডাক্ট");
    $('.lang-inventory-print-your-barcode').text("একটি বারকোড প্রিন্ট");
    $('.lang-inventory-show-all-products').val("সমস্ত পণ্য দেখুন");
    $('.lang-inventory-print-all-stock').val("সমস্ত পণ্য প্রিন্ট");
    $('.lang-inventory-print-barcode').val("প্রিন্ট বারকোড");
    $('.lang-inventory-export').text("এক্সপোর্ট");
    $('.lang-inventory-import').text("ইম্পোর্ট");
    $('.lang-inventory-store').text("দোকান");
    $('.lang-inventory-supplier').text("সরবরাহকারী");
    $('.lang-inventory-category').text("ক্যাটাগরি");
    $('.lang-inventory-from').text("শুরুর তারিখ");
    $('.lang-inventory-to').text("শেষের তারিখ");
    $('.lang-inventory-qty').text("পণ্য সংখ্যা");
    $('.lang-inventory-imei').text("আইএমইআই");
    $('.lang-inventory-package-name').text("প্যাকেজ নাম");
    $('.lang-inventory-wholesale-price').text("পাইকারি মূল্য");
    $('.lang-inventory-sale-price').text("বিক্রয় মূল্য");
    $('.lang-inventory-package').text("প্যাকেজ");
    $('.lang-inventory-package-list').text("প্যাকেজ লিস্ট");
    $('.lang-inventory-add-update-package').text("অ্যাড/আপডেট প্যাকেজ");
    $('.lang-inventory-stock').text("স্টক");
    $('.lang-inventory-stock-report').text("স্টক রিপোর্ট");
    $('.lang-inventory-sl').text("ক্রমিক");
    $('.lang-inventory-product-title').text("পণ্যর নাম");
    $('.lang-inventory-price').text("মূল্য");
    $('.lang-inventory-warranty').text("ওয়ারেন্টি");
    $('.lang-inventory-warranty-history').text("ওয়ারেন্টি হিস্ট্রি");
    $('.lang-inventory-return').text("পণ্য ফেরত");
    $('.lang-inventory-back-to-return').text("পণ্য ফেরত দিন");
    $('.lang-inventory-search-by-date').text("তারিখ অনুযায়ী অনুসন্ধান করুন");
    $('.lang-inventory-damage').text("নষ্ট হওয়া পণ্য");
    $('.lang-inventory-add-damage').text("নষ্ট হওয়া পণ্য যুক্ত করুন");
    $('.lang-inventory-stock-warning-qty').text("স্টক ওয়ার্নিং রিপোর্ট");
    $('.lang-invenroty-warning-stock-history').text("স্টক ওয়ার্নিং লিস্ট");
    $('.lang-inventory-expiry').text("মেয়াদউত্তীর্ণ");
    $('.lang-inventory-expired-product-history').text("মেয়াদউত্তীর্ণ প্রোডাক্ট লিস্ট");



    $('.lang-sales-quotation').text("কোটেশন");
    $('.lang-sales-quotation-list').text("কোটেশন তালিকা");
    $('.lang-sales-servicing').text("সার্ভিসিং");
    $('.lang-sales-to').text("হতে");

    $('.lang-reports-purchase-report').text("পারচেজ রিপোর্ট");
    $('.lang-reports-invoice-report').text("ইনভয়েস রিপোর্ট");
    $('.lang-reports-inventory-report').text("ইনভেন্টরি রিপোর্ট");
    $('.lang-reports-transaction').text("লেনদেন");
    $('.lang-reports-supplier-commission').text("সাপ্লাইয়ার কমিশন");
    $('.lang-reports-profit-loss').text("লাভ/লোকসান");
    $('.lang-reports-loss-profit-histroy').text("লাভ/লোকসান হিস্ট্রি");
    $('.lang-reports-summary').text("বিজনেস সামারি");
    $('.lang-reports-sales-sammary').text("বিক্রয় সামারি");
    $('.lang-reports-cash-summary').text("নগদ সামারি");
    $('.lang-reports-accounts-summary').text("অ্যাকাউন্টস সামারি");
    $('.lang-reports-analytic-reports').text("বিশ্লেষণ রিপোর্ট");
    $('.lang-reports-store').text("দোকান");
    $('.lang-reports-from').text("হতে");
    $('.lang-reports-to').text("প্রতি");


    $('.lang-record-store').text("দোকান");
    $('.lang-record-store-list').text("দোকানের তালিকা");
    $('.lang-record-show').text("দেখুন");
    $('.lang-record-entries').text("লেখা");
    $('.lang-record-add-store').text("নতুন দোকান");
    $('.lang-record-store-name').text("দোকানের নাম");
    $('.lang-record-location').text("অবস্থান");
    $('.lang-record-add-location').text("নতুন অবস্থান");
    $('.lang-record-location-list').text("অবস্থান তালিকা");
    $('.lang-record-manufacturer').text("উৎপাদক");
    $('.lang-record-add-manufacturer').text("নতুন উৎপাদক");
    $('.lang-record-manufacturer-list').text("উৎপাদকের তালিকা");
    $('.lang-record-supplier').text("সাপ্লায়ার");
    $('.lang-record-add-supplier').html("নতুন সাপ্লায়ার");
    $('.lang-record-supplier-list').text("সাপ্লায়ার তালিকা");
    $('.lang-record-company-name').text("কোম্পানির নাম");
    $('.lang-record-supplier-code').text("সাপ্লায়ার কোড");
    $('.lang-record-contact-name').text("যোগাযোগের নাম");
    $('.lang-record-contact-designation').text("যোগাযোগের পদবী");
    $('.lang-record-contact-phone').text("ফোনে যোগাযোগ");
    $('.lang-record-contact-email').text("যোগাযোগের ই - মেইল");
    $('.lang-record-discount').text("ডিসকাউন্ট (in %)");
    $('.lang-record-address').text("ঠিকানা");
    $('.lang-record-category').text("ক্যাটাগরি");
    $('.lang-record-add-category').text("নতুন ক্যাটাগরি");
    $('.lang-record-category-list').text("ক্যাটাগরি তালিকা");
    $('.lang-record-category-name').text("ক্যাটাগরি নাম");
    $('.lang-record-unit-of-measurement').text("ইউনিট");
    $('.lang-record-add-unit').text("নতুন ইউনিট");
    $('.lang-record-unit-list').text("ইউনিটের তালিকা");
    $('.lang-record-name').text("নাম");
    $('.lang-record-ratio').text("অনুপাত");
    $('.lang-record-field').text("ফিল্ড");
    $('.lang-record-add-new-field').text("নতুন ফিল্ড");
    $('.lang-record-field-list').text("ফিল্ডের তালিকা");
    $('.lang-record-attribute').text("এট্রিবিউট");
    $('.lang-record-add-attribute').text("নতুন এট্রিবিউট");
    $('.lang-record-attribute-list').text("এট্রিবিউট তালিকা");
    $('.lang-record-field-name').text("ফিল্ড নাম");
    $('.lang-record-attribute-name').text("এট্রিবিউট নাম");
    $('.lang-record-card-name').text("কার্ড নাম");
    $('.lang-record-add-card').text("নতুন কার্ড");
    $('.lang-record-card-discount').text("কার্ড ডিসকাউন্ট");
    $('.lang-record-bank-name').text("ব্যাংক");
    $('.lang-record-add-bank').text("নতুন ব্যাংক");
    $('.lang-record-bank-name-list').text("ব্যাংকের নামের তালিকা");
    $('.lang-record-card').text("কার্ড");
    $('.lang-record-card-list').text("কার্ড তালিকা");
    $('.lang-record-particular').text("পার্টিকুলার");
    $('.lang-record-add-particular').text("নতুন পার্টিকুলার");
    $('.lang-record-particular-list').text("পার্টিকুলার তালিকা");
    $('.lang-record-staff').text("স্টাফ");
    $('.lang-record-add-new-staff').text("নতুন স্টাফ");
    $('.lang-record-staff-list').text("স্টাফের তালিকা");
    $('.lang-record-phone').text("ফোন");
    $('.lang-record-sex').text("লিঙ্গ");
    $('.lang-record-date-of-birth').text("জন্ম তারিখ");
    $('.lang-record-department').text("ডিপার্টমেন্ট");
    $('.lang-record-service-type').text("সার্ভিস টাইপ");
    $('.lang-record-add-service-type').text("নতুন সার্ভিস টাইপ");
    $('.lang-record-service-type-list').text("সার্ভিস টাইপের তালিকা");
    $('.lang-record-service-type-name').text("সার্ভিস টাইপ নাম");

}
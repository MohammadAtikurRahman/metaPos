//jQuery code
jQuery(function ($) {

    'use strict';

    initContact();
});


//Contact Form Validation
function initContact() {


    $('#txtMobile').keypress(function (evt) {
        var val = String.fromCharCode(evt.which);
        if (!(/[0-9]/.test(val))) {
            evt.preventDefault();
        }
    });

 
    // int trige
    var contactForm = $('#contact-form');
    var contactButton = $('#btnSend');


    contactForm.submit(function () { return false; });
    contactButton.on('click', function() {
        

        // int keys 
        var name = $("#txtName").val();
        var mobile = $("#txtMobile").val();
        var company = $("#txtCompany").val();
        var subject = $('#Body_ddlSubject option:selected').text();
        var ques = $("#txtQus").val();


        if (name.length < 1) {
            sweetAlert('সতর্কিকরন', 'আপনার নাম দিন', 'error');
            return false;
        }
        if (mobile.length > 1) {
            if (mobile.length != 11 && mobile.length < 11) {
                sweetAlert('সতর্কিকরন', 'নাম্বার অবশ্যই ১১ ডিজিট হতে হবে', 'error');
                return false;
            }
        }
        else {
            sweetAlert('সতর্কিকরন', 'আপনার মোবাইল নাম্বার দিন', 'error');
            return false;
        }
        if (company.length < 1) {
            sweetAlert('সতর্কিকরন', 'কোম্পানির নাম দিন', 'error');
            return false;
        }
        if ($('#Body_ddlSubject option:selected').val() == 0) {
            sweetAlert('সতর্কিকরন', 'বিষয় নির্বাচন করুন', 'error');
            return false;
        }
        if (ques.length < 1) {
            sweetAlert('সতর্কিকরন', 'আপনার প্রশ্ন লিখুন', 'error');
            return false;
        }
        

           contactButton.attr("disabled", "disabled");
           contactButton.html('<span class="faa-spin animated"></span> &nbsp; পাঠানো হচ্ছে ...');

            var mailInfo = {
                "name": name,
                "mobile": mobile,
                "company": company,
                "subject": subject,
                "ques": ques
            };

            $.ajax({
                url: baseUrl + "Site/Views/Contact.aspx/ContentByEmailAction",
                contentType: "application/json",
                data: "{ 'Data' : '" + JSON.stringify(mailInfo) + "' }",
                crossDomain: true,
                dataType: "json",
                type: "POST",
                success: function (res) {
                    console.log("res:", res);
                    // doing something
                    //var res = JSON.parse(data);
                    if (res.d === "Success") {
                        contactButton.removeAttr("disabled");
                        contactButton.html("পাঠানো হয়েছে");
                        sweetAlert('ধন্যবাদ!', 'ম্যাসেজ পাঠানো হয়েছে', 'success');
                        reset();
                    } else {
                        contactButton.removeAttr("disabled");
                        contactButton.html("আপনি ভুল করেছেন, আবার চেষ্টা করুন");
                    }
                },
                
                error: function (error) {
                    console.log(error);
                    contactButton.removeAttr("disabled");
                    contactButton.html("আপনি ভুল করেছেন");
                    // doing something
                }
                
            });
    });


    function reset() {
        $('#txtName').val("");
        $('#txtMobile').val("");
        $("#txtCompany").val("");
        $('#Body_ddlSubject option').prop('selected', 0);
        $("#txtQus").val("");
        
    }

}

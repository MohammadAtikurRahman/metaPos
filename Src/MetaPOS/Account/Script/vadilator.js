$('document').ready(function() {

    //Login Vadilation  Here
    $('#btnLogIn').click(function() {
        if ($('#txtLogEmail').val() == "") {
            Materialize.toast('Type mail!', 4000);
            return false;
        }
        else {
            if (!validateEmail($('#txtLogEmail').val())) {

                Materialize.toast('Type valid email!', 4000);
                return false;
            }
            else {
                if ($('#txtLogPass').val() == "") {
                    Materialize.toast('Type password!', 4000);
                    return false;
                }
            }
        }

    });
    // vadilation for forGotEmail

    $('#btnForGotPass').click(function() {
        if ($('#txtForGotEmail').val() == "") {
            Materialize.toast('Type mail!', 4000);
            return false;
        }
        else {
            if (!validateEmail($('#txtLogEmail').val())) {

                Materialize.toast('Type valid email!', 4000);
                return false;
            }
        }

    });


// vadilation for sign up
    $('#btnSignUp').click(function() {
        var apple = $('#txtSignFullName').val();
        if ($('#txtSignFullName').val() == "") {
            Materialize.toast('Type full name!', 4000);
            return false;
        }
        else {
            if (apple.length < 5) {
                Materialize.toast('Full name should be getter then 5!', 4000);
                return false;
            }
            else {
                if ($('#txtSignProfession').val() == "") {
                    Materialize.toast('Type profession!', 4000);
                    return false;
                }
                else {
                    if ($('#txtSignMobNo').val() == "") {
                        Materialize.toast('Type mobile number!', 4000);
                        return false;
                    }
                    else {
                        if ($('#txtSignEmail').val() == "") {
                            Materialize.toast('Type email!', 4000);
                            return false;
                        }
                        else {
                            if ($('#txtSignPass').val() == "") {
                                Materialize.toast('Type password!', 4000);
                            }
                        }

                    }
                }
            }

        }
    });


    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test($email);
    }


});
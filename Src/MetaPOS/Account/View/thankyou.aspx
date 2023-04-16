<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="thankyou.aspx.cs" Inherits="MetaPOS.Account.View.thankyou" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" class="body-full-height">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0" />
    <title>Thank you - Robi Amarhishab</title>

    <link href="/Account/Content/materialize.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="/Account/Content/toastr.min.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="/Account/Content/login.css?v=0.008" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="/Account/Content/login-responsive.css?v=0.003" rel="stylesheet" />
    <link href="/Account/Content/modal-loading.css" rel="stylesheet">
    <link href="/Account/Content/main.css?v=0.003" rel="stylesheet" />
    <link href="/Account/Content/signup.css?v=0.003" rel="stylesheet" />

    <!-- font awesome -->
    <link href="/Account/Content/fontawesome/css/all.min.css" rel="stylesheet" />
    <script src="/Account/Content/fontawesome/js/all.min.js"></script>


</head>

<body class="backstretch">

    <div id="loader" style="display: none"></div>

    <div class="backstretch-overlay" id="login-body">
        <form id="form1" runat="server" autocomplete="off">

            <asp:Panel ID="pnlLogin" runat="server">
                <div class="row">

                    <div class="col l6 m12 s12 left-area">

                        <div class="left-content">
                            <div class="container content-index">

                                <div class="type-wrap">
                                    <span>Manage </span>
                                    <div id="typed-strings">

                                        <span>Your Sales</span>
                                        <span>Your Inventory</span>
                                        <span>Your Customer</span>
                                        <span>Your Accounts</span>
                                        <span>Overall Business </span>
                                    </div>
                                    <span id="typed" style="white-space: pre;"></span>
                                </div>

                                <p class="content-desc">Robi Amar Hishab offers you to manage your business in more comfortable way. We flow our slogan for 'Business Yours, Responsibility Ours'.</p>
                                <a href="http://robiamarhishab.com/features" target="_blank" tooltip="See all features" class="waves-effect green darken-4 btn-large features btnFeatures">Checkout Features</a>
                            </div>
                        </div>

                    </div>

                    <div class="col l6 m12 s12 login_box ">
                        <div class="row log_box signup-box">
                            <div class="container">
                                <div class="row">
                                    <img src="/Account/Images/logo.png" />
                                </div>
                                <div class="row signup">
                                    <h5>খুব সহজে আপনার রিটেইল শপ তৈরি করুন </h5>
                                    <p class="category">ব্যবসা আপনার দায়িত্ব আমাদের। </p>
                                    <asp:Label runat="server" ID="lblTest" BackColor="red"></asp:Label>
                                </div>
                            </div>
                            <div class="container">
                                <div class="row">

                                    <div class="s12">
                                        <div class="jumbotron text-center" style="margin-top: 8%">
                                            <h1 class="display-3">Thank You!</h1>
                                            <p class="lead"><strong>Please wait for our confirmation</strong>, We will check and active your account. After contact with you via SMS or Call.</p>
                                            <hr>
                                            <p>
                                                Having trouble? <a href="/contact">Contact us</a>
                                            </p>
                                            <p class="lead">
                                                <a class="btn btn-primary btn-sm" href="http://robiamarhishab.com/" role="button">Continue to homepage</a>
                                            </p>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </asp:Panel>
        </form>
    </div>




    <script src="/Account/Script/jquery-1.10.2.js"></script>
    <script src="/Account/Script/main.js"></script>
    <script src="/Account/Script/toastr.min.js"></script>


    <script src="/Account/Script/signup.js?v=0.10"></script>

    <script src="/Account/Script/materialize.min.js"></script>
    <script src="/Account/Script/jquery.backstretch.js"></script>
    <script src="/Account/Script/SlideShow.js?v=0.01"></script>
    <%-- <script src="Asset/Js/vadilator.js"></script>--%>
    <script src="/Account/Script/typed.js"></script>
    <script src="/Account/Script/typed_custom.js"></script>
    <script src="/Account/Script/modal-loading.js"></script>

    <!-- offline serviceWorker intitial -->

    <script src="/Offline/sw_pages.js?v=0.0.6"></script>

    <script>
        /* Service worker for Firebase Cloud Messaging */
        //navigator.serviceWorker.register('static/service-worker.js', {scope: '/'})
        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('/Offline/sw_pages.js')
                .then(service => console.log('service worker installed :', service))
            .catch(err => console.error('Error', err));
        }
    </script>

</body>
</html>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MetaPOS.Account.View.Login" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" class="body-full-height">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0" />
    <title>Login - metaPOS</title>

    <link href="/Account/Content/materialize.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="/Account/Content/toastr.min.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="/Account/Content/login.css?v=0.008" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="/Account/Content/login-responsive.css?v=0.003" rel="stylesheet" />

    <script src="/Account/Script/jquery-1.10.2.js"></script>
    <script src="/Account/Script/toastr.min.js"></script>
    <script src="/Account/Script/main.js"></script>

</head>

<body class="backstretch">

    <div id="loader" style="display: none"></div>

    <div class="backstretch-overlay" id="login-body">
        <form id="form1" runat="server" autocomplete="off">
            <asp:Panel ID="pnlLogin" runat="server">
                <div class="row">
                    <div class="col l4 m12 s12 login_box">
                        <div class="row log_box">
                            <div class="container">
                                <div class="row">
                                    <img src="/Account/Images/logo.png" />
                                </div>
                                <div class="row signup">
                                    <h5>Welcome, Please login</h5>
                                    <asp:Label runat="server" ID="lblTest" BackColor="red"></asp:Label>
                                </div>
                            </div>
                            <div class="container">
                                <div class="row">

                                    <div class="row input-field">
                                        <div class="col s12">
                                            <asp:TextBox runat="server" placeholder="Email" ID="txtEmail" />
                                        </div>
                                    </div>
                                    <div class="row input-field">
                                        <div class="col s12">
                                            <asp:TextBox runat="server" type="password" placeholder="Password" ID="txtPassword" />
                                        </div>
                                    </div>
                                    <div class="row log-bar custom-bar">
                                        <div class="col l4 m6 s6">
                                            <asp:Button runat="server" CssClass="darken-4" Text="Login" ID="btnLogIn" OnClick="btnLogIn_OnClick" />
                                        </div>
                                        <div class="col l8 m6 s6 forgot-password">
                                            <a href="javascript:void(0)" id="forgePass">Forgot password?</a>
                                        </div>
                                    </div>


                                    <div class="col l12 m12 s12">
                                        <div class="company-signup">
                                            <span>Don't have a shop? 
                                            <a href="/account/signup" id="btnSignUp" runat="server" class="text-right">Try metaPOS for free</a></span>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                        <div class="row forgot_box">
                            <div class="container">
                                <div class="row signup">
                                    <h5>Hi, type your mail to reover</h5>
                                </div>
                            </div>
                            <div class="container">
                                <div class="row">
                                    <div class="row input-field">
                                        <div class="col s12">
                                            <asp:Label ID="lblForGotMsg" runat="server"></asp:Label>
                                            <asp:TextBox runat="server" type="email" placeholder="Email" ID="txtEmailToRecover" />
                                        </div>
                                        <div class="s12">
                                            <asp:Label runat="server" ID="lblWaiting" CssClass="lbl label-control" Text="" ForeColor="White"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row log-bar ">
                                        <div class="col l4 m6 s6">
                                            <asp:Button ID="btnSentMailToRecover" runat="server" CssClass="waves-effect green darken-4 btn" Text="Send Password" OnClick="btnSentMailToRecover_OnClick"></asp:Button>
                                        </div>
                                        <div class="col l8 m6 s6 forgot-password">
                                            <a href="javascript:void(0)" id="backToLogin">Back To Login </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col l8 m12 s12 left-area">
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

                                <p class="content-desc">metaPOS offers you to manage your business in more comfortable way. We flow our slogan for 'Business Yours, Responsibility Ours'.</p>
                                <a href="http://metaposbd.com/features" target="_blank" tooltip="See all features" class="waves-effect green darken-4 btn-large features btnFeatures">Checkout Features</a>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>
        </form>
    </div>

    <script src="/Account/Script/Login.js?v=0.04"></script>
    <script src="/Account/Script/materialize.min.js"></script>
    <script src="/Account/Script/jquery.backstretch.js"></script>
    <script src="/Account/Script/SlideShow.js?v=0.01"></script>
    <script src="/Account/Script/typed.js"></script>
    <script src="/Account/Script/typed_custom.js"></script>

    <!-- offline serviceWorker intitial -->
    <script src="/Offline/sw_pages.js"></script>
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

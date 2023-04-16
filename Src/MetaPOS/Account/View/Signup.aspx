<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="MetaPOS.Account.View.Signup" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" class="body-full-height">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0" />
    <title>Signup - Robi Amar Hishab powered by MetaKave</title>

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
                                <img class="login_box2" src="/Account/Images/logo.png" /><br/><br/>
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

                                <p class="content-desc">Robi Amar Hishab powered by <a href="https://metakave.com" target="_blank" style="color: #fff !important; text-decoration: underline;">MetaKave</a> offers you to manage your business digitally.</p>
                                <br/>
                                <span style="font-size: 24px; color:#00AC4E; font-weight: bold;">যেভাবে সাইনআপ করবেন</span>
                                <hr>
                                <span style="font-size: 20px;">১. সাইনআপ ফর্মটি পূরণ করুন<br/>২. ওয়ার্ক অর্ডার / পারচেজ অর্ডার আপলোড করুন, এবং <br/>৩. পেমেন্ট করার মাধ্যমে একাউন্ট তৈরী করুন  </span>
                            </div>
                        </div>

                    </div>

                    <div class="col l6 m12 s12 login_box ">
                        <div class="row log_box signup-box">
                            <div class="container">
                                
                                <div class="row signup">
                                    <span style="font-size: 24px; color:#fff; font-weight: bold;">রবি আমার হিসাবে সাইনআপ করুন</span>
                                    <asp:Label runat="server" ID="lblTest" BackColor="red"></asp:Label>
                                </div>
                            </div>
                            <div class="container">
                                <div class="row">

                                    <div class="row input-field">

                                        <div class="col s6">
                                            <asp:TextBox ID="txtBusinessName" CssClass="form-control" runat="server" placeholder="ইংরেজীতে আপনার ব্যবসার নাম লিখুন" required></asp:TextBox>
                                            <label class="account-warning account-store-name-warning disNone"><i class="fas fa-exclamation-triangle"></i><span class="store-name-warning-text">please enter your store name</span></label>
                                        </div>

                                        <div class="col s6">
                                            <asp:TextBox runat="server" ID="txtEmail" placeholder="আপনার ই-মেইল লিখুন" required />
                                            <label class="account-warning account-email-warning disNone"><i class="fas fa-exclamation-triangle"></i>please enter email address</label>
                                        </div>

                                    </div>

                                    <%--<div class="row input-field">

                                        <div class="col s6">
                                            <asp:TextBox runat="server" type="password" ID="txtPassword" placeholder="আপনার পাসওয়ার্ড লিখুন" required />
                                            <label class="account-warning account-password-warning disNone"><i class="fas fa-exclamation-triangle"></i>please enter your password</label>
                                        </div>

                                    </div>--%>


                                    <div class="row input-field">
                                        <div class="col s6">
                                            <asp:TextBox ID="txtName" CssClass="form-control" runat="server" placeholder="আপনার নাম" required></asp:TextBox>
                                            <label class="account-warning account-store-name-warning disNone"><i class="fas fa-exclamation-triangle"></i><span class="store-name-warning-text">please enter your store name</span></label>
                                        </div>
                                        <div class="col s6">
                                            <asp:TextBox runat="server" ID="txtPhone" placeholder="মোবাইল নাম্বার (01XXXXXXXXX)" required />
                                            <label class="account-warning account-mobile-warning disNone"><i class="fas fa-exclamation-triangle"></i><span class="account-warning-message">please enter your phone number</span></label>
                                        </div>
                                    </div>
                                    
                                    
                                    <div class="row input-field">
                                        <div class="col s6">
                                            <asp:DropDownList ID="ddlDistrict" runat="server" OnTextChanged="ddlDistrict_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Value="">-- Select District --</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col s6">
                                            <asp:DropDownList ID="ddlSubDistrict" runat="server">
                                                <asp:ListItem Value="">-- Select Sub District --</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 0px;">
                                         <div class="col s6">
                                             <span>প্যাকেজ </span>
                                         </div>
                                         <div class="col s6">
                                             <span> অতিরিক্ত ব্র্যাঞ্চ </span>
                                         </div>
                                    </div>
                                    <div class="row">
                                        <div class="col s6">
                                            <asp:DropDownList ID="ddlPackage" CssClass="toCalculate" runat="server" AutoPostBack="False">
                                                <asp:ListItem Value="0" Selected="True">-- Select --</asp:ListItem>
                                                <asp:ListItem Value="advance">Advance</asp:ListItem>
                                                <asp:ListItem Value="basic">Basic</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator
                                                 ID="RequiredFieldValidator1"
                                                 runat="server"
                                                 ControlToValidate="ddlPackage"
                                                 InitialValue="-- Select --"
                                                 ErrorMessage="* Please select a package item."
                                                 ForeColor="Red"
                                                 Font-Names="Impact">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col s6">
                                             
                                            <asp:DropDownList ID="ddlDomainNo" CssClass="toCalculate text-center" runat="server" AutoPostBack="False">
                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                            <%--<button class="button1" id="button1"></button>--%>
                                        </div>
                                    </div>

                                    <div class="row input-field">

                                        <div class="col s12">
                                            <asp:DropDownList runat="server" ID="ddlBusinessType" class="form-control">
                                                <asp:ListItem Value="">-- Select Business Type --</asp:ListItem>
                                                <asp:ListItem Value="Retail">Retail</asp:ListItem>
                                                <asp:ListItem Value="Distribution">Distribution</asp:ListItem>
                                                <asp:ListItem Value="Pharmacy">Pharmacy</asp:ListItem>
                                                <asp:ListItem Value="Shoe">Shoe</asp:ListItem>
                                                <asp:ListItem Value="Others">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row input-field">

                                        <div class="col s12">
                                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" TextMode="MultiLine" placeholder="ব্যবসার ঠিকানা লিখুন" Style="height: 4rem" required></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="row input-field">

                                        <div class="col s6">
                                            <span>সাবস্ক্রিপশন ফি (টাকা): </span>
                                        </div>
                                         <div class="col s6">
                                             <asp:TextBox runat="server" ID="subscriptionFee" value="0.00" ForeColor="white" Enabled="False"></asp:TextBox>
                                         </div>
                                    </div>
                                    <div class="row">
                                        <div class="col s12">
                                            <span>পারচেজ অর্ডার আপলোড করুন:</span>
                                             <br/>
                                        </div>
                                       
                                        <div class="col s12">
                                            <asp:FileUpload ID="fileUpload" runat="server" CssClass="btn-file btn-custom-uploader" />
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="fileUpload" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.pdf)$" runat="server" ErrorMessage="NB: File name and extension should be logo.png or .jpg or .pdf" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>

                                    <div class="row log-bar custom-bar">
                                        <div class="col l4 m6 s6">
                                            <asp:Button runat="server" ID="btnSignup" Text="Register and Pay" CssClass="btn btn-primary" OnClick="btnSignup_Click" />
                                            <span class="account-warning-signup disNone"><span class="account-warning-message">Please wait ...</span></span>
                                        </div>
                                    </div>


                                    <div class="col l12 m12 s12">
                                        <div class="company-signup">
                                            <span>আপনার কি আকাউন্ট আছে? 
                                            <a href="/login" id="btnLogin" runat="server" class="text-right">তাহলে লগইন করুন</a></span>

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


    <script src="/Account/Script/signup.js?v=0.14"></script>

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
        $(document).ready(function () {
            $(".toCalculate").change(function () {
                var ddlpackage = $("#ddlPackage").val() == "" ? "0" : $("#ddlPackage").val();
                var domainNo = $("#ddlDomainNo").val();
                console.log("ddlpackage", ddlpackage);
                console.log("domainNo", domainNo);
                var vat = 15;
                var amount = 0;
                var domainNoCharge = (250 * domainNo) * 1.15;

                if (ddlpackage == "advance") {
                    amount = 1150;
                }
                else if (ddlpackage == "basic") {
                    amount = 800;
                    var totalVat = (amount * vat) / 100;
                    amount = amount + totalVat;
                }

                amount = amount + domainNoCharge;
                console.log("amount", amount);

                $('#<%=subscriptionFee.ClientID%>').val(amount.toFixed(2));
            });

           



        });
    </script>
    <script>
        /* Service worker for Firebase Cloud Messaging */
        //navigator.serviceWorker.register('static/service-worker.js', {scope: '/'})
        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('/Offline/sw_pages.js');
            //.then(service => console.log('service worker installed :', service))
            //.catch(err => console.error('Error', err));
        }
    </script>

</body>
</html>

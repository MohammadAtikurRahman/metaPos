<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="MetaPOS.Admin.Controller.Header" %>
<%@ Import Namespace="System.Activities.Statements" %>
<style type="text/css">
    .pnl_module {
        display: table-cell;
    }

    /*ul > .pnl_module > li > a {
        color: #eee;
         display: block;
        font-size: 15px;
        padding: 8px 0 8px 22px;
        text-decoration: none;
        text-align: left;
    }*/

    ul > .pnl_module > li > a {
        text-decoration: none;
        display: block;
        font-weight: 400;
        color: #ccc;
        white-space: nowrap;
        font-size: 14px;
        height: 46px;
        padding: 0 11px;
        line-height: 46px;
        padding-left: 9.8px;
    }

        ul > .pnl_module > li > a > span {
            color: #ccc;
            font-size: 14px;
        }

    .nav > li > a {
        height: 46px;
        padding: 0 10px;
        line-height: 46px;
        padding-left: 10.63px;
    }

    /*.fa-angle-down:before {
        display: none;
    }*/
 .barcode-header{
    text-align:center !important;
}
.codeHeader1 {
    text-align: center !important;
}
.price1{
    text-align:center !important;
}
#codeHeader1{
    text-align:center !important;
}
</style>

<div class="header fixed-top" id="menuHeader">

    <link href="https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.1.0/css/flag-icon.min.css" rel="stylesheet">

    <div class="header-notification-bar disNone" id="expiryNotificatonBar">
        <p class="text-center">
            Your subscription will expire at
            <label id="lblSubscriptionDate"></label>
            . <a href="subscription">Click to renew now</a>
            <input class="notification-close" id="btnCloseNotify" type="button" value="X" />
        </p>
    </div>

    <%--<div class="col-md-2 header-logo"> 
        <a href="/admin/dashboard">
            <img src="../../Img/logo.png" alt="metaPOS" />
        </a>
    </div>--%>

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 menu-detail">

        <div class="companyName disNone">
            <asp:Label runat="server" ID="lblLang"></asp:Label>
            <asp:Label runat="server" ID="lblComName"></asp:Label>
        </div>

        <nav class="navbar">

            <div class="col-lg-2 col-md-3 col-sm-3 col-xs-12 header-logo">
                <a href="/admin/dashboard">
                    <img src="../../Img/logo.png" alt="metaPOS" />
                </a>
            </div>
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                    <%--<span class="sr-only">Toggle navigation</span>
                    <span class="nav-icon"></span> --%>
                    <span class="fa fa-bars"></span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">

                <ul class="nav navbar-nav menuBarAll" id="mainTopHeader">

                    <asp:Panel ID="mdlInventory" runat="server" CssClass="pnl_module">
                        <!--  == "bn"? "A" : "B"-->
                        <!-- Inventory -->
                        <li class="dropdown inventory btnLinkInventory" id="linkInventory">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span><%=Resources.Language.Link_inventory %></span> <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkPurchase disNone" value="BulkStock">
                                    <a href="/admin/bulk-stock">
                                        <span runat="server" id="Page_Link_purchase"></span>
                                    </a>
                                </li>
                                <li class="btnLinkStock disNone" value="Stock">
                                    <a href="/admin/stock">
                                        <span runat="server" id="Page_Link_stock"></span>
                                    </a>
                                </li>
                                <li class="btnLinkPackage disNone" value="Package">
                                    <a href="/admin/package">
                                        <span runat="server" id="Page_Link_package"></span>
                                    </a>
                                </li>
                                <li class="btnLinkWarranty disNone" value="Warranty">
                                    <a href="/admin/warranty">
                                        <span runat="server" id="Page_Link_Warranty"></span>
                                    </a>
                                </li>
                                <li class="btnLinkReturn disNone" value="Return">
                                    <a href="/admin/return">
                                        <span runat="server" id="Page_Link_Return"></span>
                                    </a>
                                </li>
                                <li class="btnLinkDamage disNone" value="Damage">
                                    <a href="/admin/damage">
                                        <span runat="server" id="Page_Link_Damage"></span>
                                    </a>
                                </li>
                                <li class="btnLinkWarning disNone" value="Warning">
                                    <a href="/admin/warning">
                                        <span runat="server" id="Page_Link_Warning"></span>
                                    </a>
                                </li>
                                <li class="btnLinkExpiry disNone" value="Warning">
                                    <a href="/admin/expiry">
                                        <span runat="server" id="Page_Link_Expiry"></span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </asp:Panel>

                    <!-- Sales -->
                    <asp:Panel ID="mdlSales" runat="server" CssClass="pnl_module">
                        <li class="dropdown sale" id="linkSale">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Sale">
                                    
                                </span>
                                 <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkSaleInvoice disNone" value="Invoice">
                                    <a href="/admin/invoice-next" id="btnCreateInvoice">
                                        <span runat="server" id="Page_Link_SaleInvoice"></span>
                                    </a>
                                </li>
                                <li class="btnLinkCustomer disNone" value="Customer">
                                    <a href="/admin/customer">
                                        <span runat="server" id="Page_Link_Customer"></span>
                                    </a>
                                </li>
                                <li class="btnLinkService disNone" value="Service">
                                    <a href="/admin/service">
                                        <span runat="server" id="Page_Link_Service"></span>
                                    </a>
                                </li>
                                <li class="btnLinkQuotation disNone" value="Quotation">
                                    <a href="/admin/quotation">
                                        <span runat="server" id="Page_Link_Quotation"></span>
                                    </a>
                                </li>
                                <li class="btnLinkServicing disNone" value="Servicing">
                                    <a href="/admin/Servicing">
                                        <span runat="server" id="Page_Link_Servicing"></span>
                                    </a>
                                </li>
                                <li class="btnLinkDueReminder disNone" value="DueReminder">
                                    <a href="/admin/installment">
                                        <span runat="server" id="Page_Link_DueReminder"></span>
                                    </a>
                                </li>
                                <li class="btnLinkToken disNone" value="Token">
                                    <a href="/admin/token">
                                        <span runat="server" id="Page_Link_token"></span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </asp:Panel>

                    <!-- Accounting -->
                    <asp:Panel ID="mdlAccouting" runat="server" CssClass="pnl_module">
                        <li class="dropdown accounting" id="linkAccounting">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Accounting"><%=Resources.Language.Link_Accounting %></span> <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkSupply disNone" value="Supply">
                                    <a href="/admin/supply">
                                        <span runat="server" id="Page_Link_Supply"><%=Resources.Language.Link_Supply %></span>
                                    </a>
                                </li>
                                <li class="btnLinkReceive disNone" value="Receive">
                                    <a href="/admin/receive">
                                        <span runat="server" id="Page_Link_Receive"><%=Resources.Language.Link_Receive %></span>
                                    </a>
                                </li>
                                <li class="btnLinkExpense disNone" value="Expense">
                                    <a href="/admin/expense">
                                                                                       <span runat="server" id="Page_Link_Expense"><%=Resources.Language.Link_Expense %></span>
                                                                                   </a>
                                </li>
                                <li class="btnLinkSalary disNone" value="Salary">
                                    <a href="/admin/salary">
                                        <span runat="server" id="Page_Link_Salary"><%=Resources.Language.Link_Salary %></span>
                                    </a>
                                </li>
                                <li class="btnLinkBanking disNone" value="Banking">
                                    <a href="/admin/banking">
                                        <span runat="server" id="Page_Link_Banking"><%=Resources.Language.Link_Banking %></span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </asp:Panel>


                    <!-- Reports -->
                    <asp:Panel ID="mdlReport" runat="server" CssClass="pnl_module">

                        <li class="dropdown report" id="linkReport">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Report">
                                    
                                </span> <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkPurchaseReport disNone" value="PurchaseReport">
                                    <a href="/admin/purchase-report">
                                        <span runat="server" id="Page_Link_PurchaseRepor"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSlip disNone" value="Slip">
                                    <a href="/admin/slip">
                                        <span runat="server" id="Page_Link_slip"></span>
                                    </a>
                                </li>
                                <li class="btnLinkInventoryReport disNone" value="InventoryReport">
                                    <a href="/admin/inventory-report">
                                        <span runat="server" id="Page_Link_InventoryReport"></span>
                                    </a>
                                </li>
                                <li class="btnLinkStockReport disNone" value="StockReport">
                                    <a href="/admin/stock-report">
                                        <span runat="server" id="Page_Link_StockReport"></span>
                                    </a>
                                </li>
                                <li class="btnLinkTransaction disNone" value="Transaction">
                                    <a href="/admin/transaction">
                                        <span runat="server" id="Page_Link_Transaction"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSupplierCommission disNone" value="Transaction">
                                    <a href="/admin/report/commission">
                                        <span runat="server" id="Page_Link_SupplierCommission"></span>
                                    </a>
                                </li>
                                <li class="btnLinkProfitLoss disNone" value="Transaction">
                                    <a href="/admin/profitloss">
                                        <span runat="server" id="Page_Link_ProfitLoss"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSummary disNone" value="Summary">
                                    <a href="/admin/summary">
                                        <span runat="server" id="Page_Link_Link_Summary"></span>
                                    </a>
                                </li>
                                <li class="btnLinkAnalytic disNone" value="Analytic">
                                    <a href="/admin/analytics">
                                        <span runat="server" id="Page_Link_Analytic"></span>
                                    </a>
                                </li>

                            </ul>
                        </li>
                    </asp:Panel>

                    <!-- Promotion -->
                    <asp:Panel ID="mdlPromotion" runat="server" CssClass="pnl_module">
                        <li class="dropdown promotion" id="linkPromotion">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Promotion"></span> <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkOffer disNone" value="Offer">
                                    <a href="/admin/offer">
                                        <span runat="server" id="Page_Link_Offer"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSMS disNone" value="SMS">
                                    <a href="/admin/sms" target="_blank">
                                        <span runat="server" id="Page_Link_sms"></span>
                                    </a>
                                </li>
                                <li class="btnLinkEmail disNone" value="Email">
                                    <a href="/admin/email">
                                        <span runat="server" id="Page_Link_email"></span>
                                    </a>
                                </li>
                            </ul>
                        </li>

                    </asp:Panel>


                    <asp:Panel ID="mdlWebsite" runat="server" CssClass="pnl_module">

                        <!-- Website -->
                        <li class="dropdown website" id="linkWebsite">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Website_main"></span>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkEcommerce disNone">
                                    <a href="/admin/ecommerce">
                                        <span runat="server" id="Page_Link_Ecommerce"></span>
                                    </a>
                                </li>
                                <li class="btnLinkWeb disNone">
                                    <a href="/admin/website">
                                        <span runat="server" id="Page_Link_Website"></span>
                                    </a>
                                </li>
                            </ul>
                        </li>

                    </asp:Panel>


                    <asp:Panel ID="mdlRecords" runat="server" CssClass="pnl_module">
                        <!-- Records -->
                        <li class="dropdown record" id="linkRecord">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Record"> </span><i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkWarehouse disNone" value="Store">
                                    <a href="/admin/store">
                                        <span runat="server" id="Page_Link_Warehouse"></span>
                                    </a>
                                </li>
                                <li class="btnLocation disNone" value="Location">
                                    <a href="/admin/location">
                                        <span runat="server" id="Page_Link_Location"></span>
                                    </a>
                                </li>
                                <li class="btnManufacturer disNone" value="Manufacturer">
                                    <a href="/admin/manufacturer">
                                        <span runat="server" id="Page_Link_Manufacturer"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSupplier disNone" value="Supplier">
                                    <a href="/admin/supplier">
                                        <span runat="server" id="Page_Link_Supplier"></span>
                                    </a>
                                </li>
                                <li class="btnLinkCategory disNone" value="Category">
                                    <a href="/admin/category">
                                        <span runat="server" id="Page_Link_Category"></span>
                                    </a>
                                </li>
                                <li class="btnLinkUnitMeasurement disNone" value="UnitMeasurement">
                                    <a href="/admin/UnitMeasurement">
                                        <span runat="server" id="Page_Link_UnitMeasurement"></span>
                                    </a>
                                </li>
                                <li class="btnLinkField disNone" value="Field">
                                    <a href="/admin/field">
                                        <span runat="server" id="Page_Link_Field"></span>
                                    </a>
                                </li>
                                <li class="btnLinkAttribute disNone" value="Attribute">
                                    <a href="/admin/attribute">
                                        <span runat="server" id="Page_Link_Attribute"></span>
                                    </a>
                                </li>
                                <li class="btnLinkBank disNone" value="Bank">
                                    <a href="/admin/bank">
                                        <span runat="server" id="Page_Link_bank"></span>
                                    </a>
                                </li>
                                <li class="btnLinkCard disNone" value="Card">
                                    <a href="/admin/card">
                                        <span runat="server" id="Page_Link_card"></span>
                                    </a>
                                </li>
                                <li class="btnLinkParticular disNone" value="Particular">
                                    <a href="/admin/particular">
                                        <span runat="server" id="Page_Link_Particular"></span>
                                    </a>
                                </li>
                                <li class="btnLinkStaff disNone" value="Staff">
                                    <a href="/admin/staff">
                                        <span runat="server" id="Page_Link_Staff"></span>
                                    </a>
                                </li>
                                <li class="btnLinkServiceType disNone" value="ServiceType">
                                    <a href="/admin/service-type">
                                        <span runat="server" id="Page_Link_ServiceType"></span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </asp:Panel>

                    <asp:Panel ID="mdlSettings" runat="server" CssClass="pnl_module">
                        <!-- Configuration -->
                        <li class="dropdown settings" id="linkSettings">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Settings"></span> <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkUser disNone" value="User">
                                    <a href="/admin/user" id="dynamicLinkUser">
                                        <span runat="server" id="Page_Link_user"></span>
                                    </a>
                                </li>
                                <li class="btnLinkProfile disNone" value="Profile">
                                    <a href="/admin/profile">
                                        <span runat="server" id="Page_Link_Profile"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSubscription disNone" value="Subscription">
                                    <a href="/admin/subscription">
                                        <span runat="server" id="Page_Link_Subscription"></span>
                                    </a>
                                </li>
                                <li class="btnLinkPayment disNone" value="Payment">
                                    <a href="/admin/payment">
                                        <span runat="server" id="Page_Link_Payment"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSecurity disNone" value="Security">
                                    <a href="/admin/security">
                                        <span runat="server" id="Page_Link_Security"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSupport disNone" value="Support">
                                    <a href="/admin/support">
                                        <span runat="server" id="Page_Link_Support"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSetting disNone" value="Setting">
                                    <a href="/admin/setting">
                                        <span runat="server" id="Page_Link_Setting"></span>
                                    </a>
                                </li>
                                <li class="btnLinkSmsConfig disNone" value="SmsConfig">
                                    <a href="/admin/sms-config">
                                        <span runat="server" id="Page_Link_SmsConfig"></span>
                                    </a>
                                </li>
                            </ul>
                        </li>

                    </asp:Panel>


                    <asp:Panel ID="mdlOffline" runat="server" CssClass="pnl_module">

                        <li class="dropdown offline " id="LinkOffline">
                            <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                <span runat="server" id="Page_Link_Oflline1"></span>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="btnLinkOffline disNone" value="Offline">
                                    <a href="javascript:void(0)">
                                        <span runat="server" id="Page_Link_Oflline"></span>
                                    </a>
                                </li>
                                <li class="btnLinkLoadOffline disNone" value="Offline">
                                    <a href="javascript:void(0)">
                                        <span runat="server" id="Page_Link_LoadOffline"></span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </asp:Panel>


                </ul>

                <ul class="nav navbar-nav navbar-right menuBarAll">
                    <li>
                        <asp:LinkButton ID="btnNotification" runat="server" CssClass="btnNotification disNone" Enabled="false"><i class="fa fa-bell alert-bell" aria-hidden="true"></i><span class="alert-text" runat="server" id="lblAlertText">5</span></asp:LinkButton>
                    </li>

                    <li class="dropdown" id="linkPublic">
                        <a href="javascript:void(0)" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            <asp:Label ID="lblRoleID" runat="server" Text="Name"></asp:Label>
                            <span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu">
                            <li class="btnLinkVersion disNone"><a href="/admin/version">Version</a></li>
                            <li class="btnLinkDocs disNone"><a href="/admin/docs">Docs</a></li>
                            <li role="separator" class="divider"></li>
                            <li style="padding-left: 8px;">
                                <asp:Button ID="btnLogout" UseSubmitBehavior="False" CssClass="btn btn-link block menuBtn btn-logout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
                            </li>

                        </ul>
                    </li>

                </ul>

            </div>

        </nav>

    </div>
</div>

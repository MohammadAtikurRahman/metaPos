<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sidebar.ascx.cs" Inherits="MetaPOS.Admin.Controller.Sidebar" %>

<div class="sidebar fixed-top" id="sidebar">
    <div class="scrollY" id="ex3">
        <nav role="navigation">
            <div class="sidebar-collapse">

                <a href="/admin/invoice-next" class="btnLinkInvoice btn btn-link invoice-button disNone"><%=Resources.Language.Link_Invoice %></a>

                <ul class="nav" id="main-menu">

                    <%-- Inventory --%>
                    <li class="btnLinkPurchase disNone"><a href="/admin/bulk-stock"><i class="fa fa-shopping-cart"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_purchase %></span></a></li>
                    <li class="btnLinkStock disNone"><a href="/admin/stock"><i class="fa fa-gift"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_stock %></span></a></li>
                    <li class="btnLinkPackage disNone"><a href="/admin/package"><i class="fa fa-paw"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_package %></span></a></li>
                    <li class="btnLinkWarranty disNone"><a href="/admin/warranty"><i class="fa fa-newspaper-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Warranty %></span></a></li>
                    <li class="btnLinkReturn disNone"><a href="/admin/return"><i class="fa fa-share"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Return %></span></a></li>
                    <li class="btnLinkDamage disNone"><a href="/admin/damage"><i class="fa fa-chain-broken"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Damage %></span></a></li>
                    <li class="btnLinkWarning disNone"><a href="/admin/warning"><i class="fa fa-exclamation-triangle"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Warning %></span></a></li>
                    <li class="btnLinkExpiry disNone"><a href="/admin/expiry"><i class="fa fa-exclamation-triangle"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Expiry %></span></a></li>

                    <%-- Sales --%>
                    <li class="btnLinkCustomer disNone"><a href="/admin/customer"><i class="fa fa-male"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Customer %></span></a></li>
                    <li class="btnLinkService disNone"><a href="/admin/service"><i class='fa fa fa-university'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Service %></span></a></li>
                    <li class="btnLinkQuotation disNone"><a href="/admin/quotation"><i class="fa fa-sticky-note-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Quotation %></span></a></li>
                    <li class="btnLinkServicing disNone"><a href="/admin/Servicing"><i class="fa fa-dashcube"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Servicing %></span></a></li>
                    <li class="btnLinkDueReminder disNone"><a href="/admin/Installment"><i class="fa fa-sort-alpha-asc"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_DueReminder %></span></a></li>
                    <li class="btnLinkToken disNone"><a href="/admin/token"><i class="fa fa-mars-double"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_token %></span></a></li>

                    <%-- Accounting --%>
                    <li class="btnLinkSupply disNone"><a href="/admin/supply"><i class="fa fa-car"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Supply %></span></a></li>
                    <li class="btnLinkReceive disNone"><a href="/admin/receive"><i class="fa fa-dashcube"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Receive %></span></a></li>
                    <li class="btnLinkExpense disNone"><a href="/admin/expense"><i class="fa fa-shopping-bag"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Expense %></span></a></li>
                    <li class="btnLinkSalary disNone"><a href="/admin/salary"><i class="fa fa-check"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Salary %></span></a></li>
                    <li class="btnLinkBanking disNone"><a href="/admin/banking"><i class='fa fa fa-university'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Banking %> </span></a></li>

                    <%-- Reports --%>
                    <li class="btnLinkPurchaseReport disNone"><a href="/admin/purchase-report"><i class="fa fa-shopping-cart"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_PurchaseRepor %></span></a></li>
                    <li class="btnLinkSlip disNone"><a href="/admin/slip"><i class="fa fa-book"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_SaleInvoice %></span></a></li>
                    <li class="btnLinkInventoryReport disNone"><a href="/admin/inventory-report"><i class="fa fa-usd"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_InventoryReport %></span></a></li>
                    <li class="btnLinkStockReport disNone"><a href="/admin/stock-report"><i class="fa fa-sort-alpha-asc"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_stockReport %></span></a></li>
                    <li class="btnLinkTransaction disNone"><a href="/admin/transaction"><i class="fa fa-usd"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Transaction %></span></a></li>
                    <li class="btnLinkProfitLoss disNone"><a href="/admin/profitloss"><i class="fa fa-usd"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_ProfitLoss %></span></a></li>
                    <li class="btnLinkSummary disNone"><a href="/admin/summary"><i class="fa fa-sort-alpha-asc"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Summary %></span></a></li>
                    <li class="btnLinkAnalytic disNone"><a href="/admin/analytics"><i class="fa fa-area-chart"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Analytic %></span></a></li>

                    <%-- Promotion --%>
                    <li class="btnLinkOffer disNone"><a href="/admin/offer"><i class="fa fa-cloud"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Offer %></span></a></li>
                    <li class="btnLinkSMS disNone"><a href="/admin/sms"><i class="fa fa-commenting-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_sms %></span></a></li>
                    <%-- <li class="btnLinkEmail disNone"><a href="/admin/email"><i class="fa fa-commenting-o"></i><span>&nbsp;&nbsp;Email</span></a></li>--%>

                    <%-- Shop --%>
                    <li class="btnLinkEcommerce disNone"><a href="/admin/ecommerce"><i class="fa fa-shopping-basket"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_ShopPage %></span></a></li>
                    <li class="btnLinkWeb disNone"><a href="/admin/website"><i class="fa fa-shopping-basket"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Web %></span></a></li>

                    <%-- Records --%>
                    <li class="btnLinkWarehouse disNone"><a href="/admin/store"><i class='fa fa-tag'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Warehouse %></span></a></li>
                    <li class="btnLocation disNone"><a href="/admin/location"><i class="fa fa-folder-open-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Location %></span></a></li>
                    <li class="btnManufacturer disNone"><a href="/admin/manufacturer"><i class="fa fa-folder-open-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Manufacturer %></span></a></li>
                    <li class="btnLinkSupplier disNone"><a href="/admin/supplier"><i class="fa fa-user"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Supplier %></span></a></li>
                    <li class="btnLinkCategory disNone"><a href="/admin/category"><i class="fa fa-folder-open-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Category %></span></a></li>
                    <li class="btnLinkUnitMeasurement disNone"><a href="/admin/UnitMeasurement"><i class='fa fa-tag'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_UnitMeasurement %></span></a></li>
                    <li class="btnLinkField disNone"><a href="/admin/field"><i class='fa fa-suitcase'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Field %></span></a></li>
                    <li class="btnLinkAttribute disNone"><a href="/admin/attribute"><i class='fa fa-tag'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Attribute %></span></a></li>
                    <li class="btnLinkBank disNone"><a href="/admin/bank"><i class='fa fa-university'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_bank %></span></a></li>
                    <li class="btnLinkCard disNone"><a href="/admin/card"><i class='fa fa-credit-card'></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_card %></span></a></li>
                    <li class="btnLinkParticular disNone"><a href="/admin/particular"><i class="fa fa-recycle"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Particular %></span></a></li>
                    <li class="btnLinkStaff disNone"><a href="/admin/staff"><i class="fa fa-users"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Staff %></span></a></li>
                    <li class="btnLinkServiceType disNone"><a href="/admin/service-type"><i class="fa fa-area-chart"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_ServiceType %></span></a></li>
                    
                    <%-- Settings --%>                    
                    <li class="btnLinkUser disNone"><a href="/admin/user"><i class="fa fa-users"></i>&nbsp;&nbsp;<span id="dynamicLinkUser"><%=Resources.Language.Link_user %></span></a></li>
                    <li class="btnLinkProfile disNone"><a href="/admin/profile"><i class="fa fa-male"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Profile %></span></a></li>
                    <li class="btnLinkSecurity disNone"><a href="/admin/security"><i class="fa fa-recycle"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Security %></span></a></li>        
                    <li class="btnLinksupport disNone"><a href="/admin/support"><i class="fa fa-users"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Support %></span></a></li>
                    <li class="btnLinkSetting disNone"><a href="/admin/setting"><i class="fa fa-commenting-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Setting %></span></a></li>
                    <li class="btnLinkSmsConfig disNone"><a href="/admin/sms-config"><i class="fa fa-commenting-o"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_SmsConfig %></span></a></li>
                    <%--<li class="btnLinkSync disNone"><a href="/admin/sync"><i class="fa fa-users"></i><span>&nbsp;&nbsp;Sync</span></a></li>--%>
                    <%--<li class="btnLinkCustomerImport disNone"><a href="/admin/customer-import" title="ImportCustomer">Import</a></li>--%>             
                    
                    <%-- Settings --%>
                    <li class="btnLinkVersion disNone"><a href="/admin/version"><i class="fa fa-users"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Version %></span></a></li>
                    <li class="btnLinkDocs disNone"><a href="/admin/docs"><i class="fa fa-users"></i><span>&nbsp;&nbsp;<%=Resources.Language.Link_Docs %></span></a></li>
                    
                </ul>
            </div>
        </nav>
    </div>
</div>
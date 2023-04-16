using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web;

namespace MetaPOS
{


    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Page Bundles Route
            RegisterCustomRoutes(RouteTable.Routes);

            RouteTable.Routes.MapHttpRoute(
                name: "ControllersApi",
                routeTemplate: "{controller}/{action}"
            );

            // API Bundles Route
            RouteTable.Routes.MapHttpRoute(
              name: "API Default",
              routeTemplate: "api/{controller}s/{id}",
              defaults: new
              {
                  id = RouteParameter.Optional
              }
          );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

        }





        private static void RegisterCustomRoutes(RouteCollection routes)
        {
            /* Main Website Route */

            routes.Ignore("{resource}.axd/{*pathInfo}"); // this is the fix!

            //routes.MapRoute(
            //    name: "RentalPropertyUnit",
            //    url: "RentalProperties/{rentalPropertyName}/Units/{unitNo}",
            //    defaults: new
            //    {
            //        controller = "RentalProperties",
            //        action = "Unit",
            //    }
            //);

            //routes.MapPageRoute("StandardRoute", // default route
            //    "{*value}",
            //    "~/Admin/AnalyticBundle/View/Dashboard.aspx");


            //routes.MapPageRoute("RouteDomain", "shop/{name}", "~/Admin/AnalyticBundle/View/Dashboard.aspx");


            //routes.MapPageRoute("RouteDomain", "{name}", "~/Default.aspx");

            routes.MapPageRoute("RouteIndex", "web", "~/Site/Views/Default.aspx");

            routes.MapPageRoute("RouteFeatures", "features", "~/Site/Views/Features.aspx");

            routes.MapPageRoute("RoutePricing", "pricing", "~/Site/Views/Pricing.aspx");

            routes.MapPageRoute("RouteContacts", "contact", "~/Site/Views/Contact.aspx");

            routes.MapPageRoute("RouteTermsCondition", "terms-condition", "~/Site/Views/TermsCondition.aspx");


            //routes.MapPageRoute("RouteLogin", "account/login", "~/Account/View/Login.aspx");
            //routes.MapPageRoute("RouteSignup", "account/signup", "~/Account/View/Signup.aspx");

            routes.MapPageRoute("RouteLoginOriginal", "login", "~/Account/View/Login.aspx");
            routes.MapPageRoute("RouteSignupOriginal", "signup", "~/Account/View/Signup.aspx");
            routes.MapPageRoute("RouteSignupThankYou", "thank-you", "~/Account/View/thankyou.aspx");




            /* Ecommerce Route */

            routes.MapPageRoute("Route33", "shop", "~/Shop/Default.aspx");

            routes.MapPageRoute("Route34", "shop/product", "~/Shop/Product.aspx");

            routes.MapPageRoute("Route35", "shop/contact", "~/Shop/Contact.aspx");

            routes.MapPageRoute("Route36", "shop/about", "~/Shop/About.aspx");



            /* Promotion Bundles Route */

            routes.MapPageRoute("Route29", "admin/sms", "~/Admin/SMSBundle/View/SMS.aspx");

            routes.MapPageRoute("RouteSmsConfig", "admin/sms-config", "~/Admin/SMSBundle/View/SmsConfig.aspx");

            routes.MapPageRoute("Route2774", "admin/email", "~/Admin/PromotionBundle/View/Email.aspx");



            /* Record Bundles Route */

            routes.MapPageRoute("RouteStore", "admin/store", "~/Admin/RecordBundle/View/Store.aspx");

            routes.MapPageRoute("RouteManufacturer", "admin/manufacturer", "~/Admin/RecordBundle/View/Manufacturer.aspx");

            routes.MapPageRoute("RouteLocation", "admin/location", "~/Admin/RecordBundle/View/Location.aspx");

            routes.MapPageRoute("Route14", "admin/supplier", "~/Admin/RecordBundle/View/Supplier.aspx");

            routes.MapPageRoute("Route150", "admin/category", "~/Admin/RecordBundle/View/Category.aspx");

            routes.MapPageRoute("RouteUntit", "admin/UnitMeasurement", "~/Admin/RecordBundle/View/Unit.aspx");

            routes.MapPageRoute("Route151", "admin/field", "~/Admin/RecordBundle/View/Field.aspx");

            routes.MapPageRoute("Route15", "admin/attribute", "~/Admin/RecordBundle/View/Attribute.aspx");

            routes.MapPageRoute("Route27", "admin/bank", "~/Admin/RecordBundle/View/Bank.aspx");

            routes.MapPageRoute("RouteCard", "admin/card", "~/Admin/RecordBundle/View/Card.aspx");

            routes.MapPageRoute("Route18", "admin/particular", "~/Admin/RecordBundle/View/Particular.aspx");

            routes.MapPageRoute("Route19", "admin/staff", "~/Admin/RecordBundle/View/Staff.aspx");


            /* Admin Panel Route */

            routes.MapPageRoute("RouteInvoiceNext", "admin/invoice-next", "~/Admin/SaleBundle/View/Invoice.aspx");

            routes.MapPageRoute("Route2", "admin/slip", "~/Admin/ReportBundle/View/Slip.aspx");

            routes.MapPageRoute("RouteQuotation", "admin/quotation", "~/Admin/ReportBundle/View/Slip.aspx");

            //routes.MapPageRoute("RouteStockReport", "admin/StockReport", "~/Admin/ReportBundle/View/StockReport.aspx");

            routes.MapPageRoute("Route3", "admin/customer", "~/Admin/CustomerBundle/View/Customer.aspx");

            routes.MapPageRoute("RouteWarranty", "admin/warranty", "~/Admin/InventoryBundle/View/Warranty.aspx");

            routes.MapPageRoute("Route4", "admin/stock", "~/Admin/InventoryBundle/View/Stock.aspx");

            routes.MapPageRoute("RouteOpt", "admin/bulk-stock", "~/Admin/InventoryBundle/View/StockBulkOpt.aspx");

            routes.MapPageRoute("RoutePurchase", "admin/purchase", "~/Admin/InventoryBundle/View/Purchase.aspx");

            routes.MapPageRoute("RouteStockOpt", "admin/stockopt", "~/Admin/InventoryBundle/View/StockOpt.aspx");

            routes.MapPageRoute("RouteStockSingleOpt", "admin/unit-stock", "~/Admin/StockSingleOpt.aspx");

            routes.MapPageRoute("RoutePackage", "admin/package", "~/Admin/PackageBundle/View/Package.aspx");

            routes.MapPageRoute("Route5", "admin/offer", "~/Admin/PromotionBundle/View/Offer.aspx");

            routes.MapPageRoute("Route6", "admin/ecommerce", "~/Admin/ShopBundle/View/Ecommerce.aspx");

            routes.MapPageRoute("Route7", "admin/return", "~/Admin/InventoryBundle/View/Return.aspx");

            routes.MapPageRoute("RouteDamage", "admin/damage", "~/Admin/InventoryBundle/View/Damage.aspx");

            routes.MapPageRoute("Route8", "admin/cancel", "~/Admin/InventoryBundle/View/Cancel.aspx");

            routes.MapPageRoute("RouteWarning", "admin/warning", "~/Admin/InventoryBundle/View/Warning.aspx");

            routes.MapPageRoute("RouteBarcode", "admin/barcode", "~/Admin/InventoryBundle/View/Barcode.aspx");

            routes.MapPageRoute("Route9", "admin/supply", "~/Admin/AccountBundle/View/Supply.aspx");

            routes.MapPageRoute("Route10", "admin/salary", "~/Admin/AccountBundle/View/Salary.aspx");

            routes.MapPageRoute("Route11", "admin/transaction", "~/Admin/ReportBundle/View/Transaction.aspx");


            routes.MapPageRoute("Route13", "admin/summary", "~/Admin/AnalyticBundle/View/Summary.aspx");

            routes.MapPageRoute("RouteAnalytic", "admin/analytics", "~/Admin/AnalyticBundle/View/Analytic.aspx");

            routes.MapPageRoute("RouteDashboard", "admin/dashboard", "~/Admin/AnalyticBundle/View/Dashboard.aspx");

            routes.MapPageRoute("RouteToken", "admin/token", "~/Admin/SaleBundle/View/Token.aspx");

            routes.MapPageRoute("Route17", "admin/size", "~/Admin/Size.aspx");

            routes.MapPageRoute("Route20", "admin/user", "~/Admin/SettingBundle/View/User.aspx");

            routes.MapPageRoute("RouteUserOpt", "admin/useropt", "~/Admin/SettingBundle/View/UserOpt.aspx");

            routes.MapPageRoute("Route40", "admin/version", "~/Admin/SettingBundle/View/Version.aspx");

            routes.MapPageRoute("Route21", "admin/docs", "~/Admin/Docs.aspx");

            routes.MapPageRoute("Route22", "admin/profile", "~/Admin/ProfileBundle/Views/Profile.aspx");

            routes.MapPageRoute("Route23", "admin/security", "~/Admin/SettingBundle/View/Security.aspx");

            routes.MapPageRoute("Route24", "admin/support", "~/Admin/SettingBundle/View/support.aspx");

            routes.MapPageRoute("RouteCustomerImport", "admin/customer-import", "~/Admin/ImportBundle/View/ImportCustomer.aspx");

            routes.MapPageRoute("Route25", "admin/setting", "~/Admin/SettingBundle/View/Setting.aspx");

            routes.MapPageRoute("Route26", "admin/expense", "~/Admin/AccountBundle/View/Expense.aspx");

            routes.MapPageRoute("Route261", "admin/receive", "~/Admin/AccountBundle/View/Receive.aspx");

            routes.MapPageRoute("Route28", "admin/banking", "~/Admin/AccountBundle/View/Banking.aspx");

            routes.MapPageRoute("Route30", "admin/404", "~/Admin/404.aspx");

            //routes.MapPageRoute("Route31", "admin/quotation", "~/Admin/SaleBundle/View/Quotation.aspx");

            routes.MapPageRoute("RouteWeb", "admin/website", "~/Admin/ShopBundle/View/Web.aspx");

            routes.MapPageRoute("RoutePrint", "admin/print", "~/Admin/Print/LoadQuery.aspx");

            routes.MapPageRoute("RouteServicing", "admin/Servicing", "~/Admin/SaleBundle/View/Servicing.aspx");

            routes.MapPageRoute("RouteSync", "admin/sync", "~/Admin/SyncBundle/View/Sync.aspx");

            routes.MapPageRoute("RouteDueReminder", "admin/installment", "~/Admin/InstallmentBundle/View/Default.aspx");

            routes.MapPageRoute("RoutePurchaseReport", "admin/purchase-report", "~/Admin/ReportBundle/View/PurchaseReport.aspx");
            
            routes.MapPageRoute("RouteStockReport", "admin/stock-report", "~/Admin/ReportBundle/View/StockReport.aspx");

            routes.MapPageRoute("RouteServiceType", "admin/service-type", "~/Admin/RecordBundle/View/ServiceType.aspx");

            routes.MapPageRoute("RouteService", "admin/service", "~/Admin/SaleBundle/View/Service.aspx");

            routes.MapPageRoute("RouteInventoryReport", "admin/inventory-report", "~/Admin/ReportBundle/View/InventoryReport.aspx");

            routes.MapPageRoute("RouteInventorySubscription", "admin/subscription", "~/Admin/SubscriptionBundle/View/Subscription.aspx");

            routes.MapPageRoute("RouteInventoryPayment", "admin/payment", "~/Admin/SubscriptionBundle/View/Payment.aspx");

            routes.MapPageRoute("RouteTerms", "payment/terms", "~/Admin/SubscriptionBundle/View/Terms.aspx");

            routes.MapPageRoute("RouteInventorySupplierCommissionReport", "admin/report/commission", "~/Admin/ReportBundle/View/SupplierCommission.aspx");


            routes.MapPageRoute("RouteExpiry", "admin/expiry", "~/Admin/InventoryBundle/View/Expiry.aspx");

            routes.MapPageRoute("RouteProfitLoss", "admin/profitloss", "~/Admin/ReportBundle/View/ProfitLoss.aspx");

            routes.MapPageRoute("RouteImport", "admin/import", "~/Admin/ImportBundle/View/Import.aspx");
            //master DB
            routes.MapPageRoute("RouteProductWiseSales", "admin/ImportProductFromApi", "~/Admin/ImportBundle/View/ImportProductFromApi.aspx");
            //user logs
            routes.MapPageRoute("RouteUserLogs", "admin/UserLogs", "~/Admin/SettingBundle/View/UserLogs.aspx");




            /* Error */
            routes.MapPageRoute("RouteError", "error", "~/Admin/Error.aspx");



        }





        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }





        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }





        protected void Application_Error(object sender, EventArgs e)
        {
        }



        protected void Application_End(object sender, EventArgs e)
        {
        }





        protected void Session_Start(object sender, EventArgs e)
        {

        }





        protected void Session_End(object sender, EventArgs e)
        {

        }


    }


}
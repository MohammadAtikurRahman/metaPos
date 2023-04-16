using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using MetaPOS.Admin.PromotionBundle.Service;
using MetaPOS.Admin.RecordBundle.View;


namespace MetaPOS.Admin.Controller
{


    public partial class Header : System.Web.UI.UserControl
    {


        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();
        private Model.QuotationModel objQuotationModelModel = new Model.QuotationModel();

        private static string isQuotaNotification = "";
        public static bool accessCheckerRequire = true;





        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // init Pages label

                // inventory Module
                Page_Link_purchase.InnerText = Resources.Language.Link_purchase;
                Page_Link_stock.InnerText = Resources.Language.Link_stock;
                Page_Link_package.InnerText = Resources.Language.Link_package;
                Page_Link_Warranty.InnerText = Resources.Language.Link_Warranty;
                Page_Link_Return.InnerText = Resources.Language.Link_Return;
                Page_Link_Damage.InnerText = Resources.Language.Link_Damage;
                Page_Link_Warning.InnerText = Resources.Language.Link_Warning;
                Page_Link_Expiry.InnerText = Resources.Language.Link_Expiry;

                // Sale Module 
                Page_Link_Sale.InnerHtml = Resources.Language.Link_Sale;
                Page_Link_SaleInvoice.InnerHtml = Resources.Language.Link_Sale;
                Page_Link_SaleInvoice.InnerHtml = Resources.Language.Link_SaleInvoice;
                Page_Link_Customer.InnerHtml = Resources.Language.Link_Customer;
                Page_Link_Service.InnerHtml = Resources.Language.Link_Service;
                Page_Link_Quotation.InnerHtml = Resources.Language.Link_Quotation;
                Page_Link_Servicing.InnerHtml = Resources.Language.Link_Servicing;
                Page_Link_DueReminder.InnerHtml = Resources.Language.Link_DueReminder;
                Page_Link_token.InnerHtml = Resources.Language.Link_token;

                // Reports Module
                Page_Link_Report.InnerHtml = Resources.Language.Link_Report;
                Page_Link_PurchaseRepor.InnerHtml = Resources.Language.Link_PurchaseRepor;
                Page_Link_slip.InnerHtml = Resources.Language.Link_slip;
                Page_Link_InventoryReport.InnerHtml = Resources.Language.Link_InventoryReport;
                Page_Link_StockReport.InnerHtml = Resources.Language.Link_stockReport;
                Page_Link_Transaction.InnerHtml = Resources.Language.Link_Transaction;
                Page_Link_SupplierCommission.InnerHtml = Resources.Language.Link_SupplierCommission;
                Page_Link_ProfitLoss.InnerHtml = Resources.Language.Link_ProfitLoss;
                Page_Link_Link_Summary.InnerHtml = Resources.Language.Link_Summary;
                Page_Link_Analytic.InnerHtml = Resources.Language.Link_Analytic;

                // Promotion Module
                Page_Link_Promotion.InnerHtml = Resources.Language.Link_Promotion;
                Page_Link_Offer.InnerHtml = Resources.Language.Link_Offer;
                Page_Link_sms.InnerHtml = Resources.Language.Link_sms;
                Page_Link_email.InnerHtml = Resources.Language.Link_email;

                // Website Module
                Page_Link_Website_main.InnerHtml = Resources.Language.Link_Website;
                Page_Link_Ecommerce.InnerHtml = Resources.Language.Link_Ecommerce;
                Page_Link_Website.InnerHtml = Resources.Language.Link_Website;


                // Records Module
                Page_Link_Record.InnerHtml = Resources.Language.Link_Record;
                Page_Link_Warehouse.InnerHtml = Resources.Language.Link_Warehouse;
                Page_Link_Location.InnerHtml = Resources.Language.Link_Location;
                Page_Link_Manufacturer.InnerHtml = Resources.Language.Link_Manufacturer;
                Page_Link_Supplier.InnerHtml = Resources.Language.Link_Supplier;
                Page_Link_Category.InnerHtml = Resources.Language.Link_Category;
                Page_Link_UnitMeasurement.InnerHtml = Resources.Language.Link_UnitMeasurement;
                Page_Link_Field.InnerHtml = Resources.Language.Link_Field;
                Page_Link_Attribute.InnerHtml = Resources.Language.Link_Attribute;
                Page_Link_bank.InnerHtml = Resources.Language.Link_bank;
                Page_Link_card.InnerHtml = Resources.Language.Link_card;
                Page_Link_Particular.InnerHtml = Resources.Language.Link_Particular;
                Page_Link_Staff.InnerHtml = Resources.Language.Link_Staff;
                Page_Link_ServiceType.InnerHtml = Resources.Language.Link_ServiceType;


                // Configuration Module
                Page_Link_Settings.InnerHtml = Resources.Language.Link_Setting;
                Page_Link_user.InnerHtml = Resources.Language.Link_user;
                Page_Link_Profile.InnerHtml = Resources.Language.Link_Profile;
                Page_Link_Subscription.InnerHtml = Resources.Language.Link_Subscription;
                Page_Link_Payment.InnerHtml = Resources.Language.Link_Payment;
                Page_Link_Security.InnerHtml = Resources.Language.Link_Security;
                Page_Link_Support.InnerHtml = Resources.Language.Link_Support;
                Page_Link_Setting.InnerHtml = Resources.Language.Link_Setting;
                Page_Link_SmsConfig.InnerHtml = Resources.Language.Link_SmsConfig;

                // Oflline Module 
                Page_Link_Oflline1.InnerHtml = Resources.Language.Link_Oflline;
                Page_Link_Oflline.InnerHtml = Resources.Language.Link_Oflline;
                Page_Link_LoadOffline.InnerHtml = Resources.Language.Link_LoadOffline;






                // Checking active module process
                string accessPage = HttpContext.Current.Session["accessPage"].ToString();

                if (HttpContext.Current.Session["userRight"].ToString() == "Super")
                {
                    accessPage = "Dashboard; User; Security; Version; Docs;";
                } 
                else if (HttpContext.Current.Session["userRight"].ToString() == "Group")
                {
                    accessPage = "Dashboard; User; Security; Support; Web; Version; Docs; Payment;";
                }
                else if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
                {
                    accessPage = "User; Profile; Security; SmsConfig; Version; Docs; " + accessPage; ;
                }

                // Check INVENTORY MODULE
                bool purchase = Regex.IsMatch(accessPage, "\\bPurchase\\b");
                bool stock = Regex.IsMatch(accessPage, "\\bStock\\b");
                bool package = Regex.IsMatch(accessPage, "\\bPackage\\b");
                bool warranty = Regex.IsMatch(accessPage, "\\bWarranty\\b");
                bool Return = Regex.IsMatch(accessPage, "\\bReturn\\b");
                bool damage = Regex.IsMatch(accessPage, "\\bDamage\\b");
                bool cancel = Regex.IsMatch(accessPage, "\\bCancel\\b");
                bool warning = Regex.IsMatch(accessPage, "\\bWarning\\b");
                bool expiry = Regex.IsMatch(accessPage, "\\bExpiry\\b");
                bool import = Regex.IsMatch(accessPage, "\\bImport\\b");

                if (purchase == false && stock == false && package == false && warranty == false && Return == false && damage == false && cancel == false &&warning == false &&expiry== false && import== false)
                {

                    mdlInventory.Visible = false;
                }


                // Check SALES MODULE
                bool invoice = Regex.IsMatch(accessPage, "\\bInvoice\\b");
                bool slip = Regex.IsMatch(accessPage, "\\bSlip\\b");
                bool customer = Regex.IsMatch(accessPage, "\\bCustomer\\b");
                bool quotation = Regex.IsMatch(accessPage, "\\bQuotation\\b");
                bool servicing = Regex.IsMatch(accessPage, "\\bServicing\\b");
                bool dueReminder = Regex.IsMatch(accessPage, "\\bDueReminder\\b");
                bool service = Regex.IsMatch(accessPage, "\\bService\\b");
                bool token = Regex.IsMatch(accessPage, "\\bToken\\b");
                if (invoice == false && slip == false && customer == false && quotation == false && servicing == false &&
                    dueReminder == false && service == false && token == false)
                {
                    mdlSales.Controls.Clear();

                   mdlSales.Dispose();
                }



                // Check Accounting module 
                bool supply = Regex.IsMatch(accessPage, "\\bSupply\\b");
                bool receive = Regex.IsMatch(accessPage, "\\bReceive\\b");
                bool salary = Regex.IsMatch(accessPage, "\\bSalary\\b");
                bool expense = Regex.IsMatch(accessPage, "\\bExpense\\b");
                bool banking = Regex.IsMatch(accessPage, "\\bBanking\\b");

                if (supply == false && receive == false && salary == false && expense == false && banking == false)
                {
                    mdlAccouting.Controls.Clear();
                }


                // Check Reports module 
                bool dashboard = Regex.IsMatch(accessPage, "\\bDashboard\\b");
                bool purchaseReport = Regex.IsMatch(accessPage, "\\bPurchaseReport\\b");
                bool inventoryReport = Regex.IsMatch(accessPage, "\\bInventoryReport\\b");
                bool supplierCommission = Regex.IsMatch(accessPage, "\\bSupplierCommission\\b");
                bool transaction = Regex.IsMatch(accessPage, "\\bTransaction\\b");
                bool profitLoss = Regex.IsMatch(accessPage, "\\bProfitLoss\\b");
                bool analytic = Regex.IsMatch(accessPage, "\\bAnalytic\\b");
                bool summary = Regex.IsMatch(accessPage, "\\bSummary\\b");

                if (dashboard == false && purchaseReport == false && inventoryReport == false &&
                    supplierCommission == false && transaction == false && profitLoss == false && analytic == false &&
                    summary == false)
                {
                    mdlReport.Controls.Clear();
                }



                // Check Promotion Module 
                bool offer = Regex.IsMatch(accessPage, "\\bOffer\\b");
                bool sms = Regex.IsMatch(accessPage, "\\bSMS\\b");


                if (offer == false && sms == false)
                {
                   mdlPromotion.Controls.Clear();
                }


                // Check Website Module 
                bool ecommerce = Regex.IsMatch(accessPage, "\\bEcommerce\\b");

                if (ecommerce == false)
                {
                    mdlWebsite.Controls.Clear();
                }



                // Check Records Module 
                bool store = Regex.IsMatch(accessPage, "\\bStore\\b");
                bool manufacturer = Regex.IsMatch(accessPage, "\\bManufacturer\\b");
                bool location = Regex.IsMatch(accessPage, "\\bLocation\\b");
                bool supplier = Regex.IsMatch(accessPage, "\\bSupplier\\b");
                bool category = Regex.IsMatch(accessPage, "\\bCategory\\b");
                bool unitMeasurement = Regex.IsMatch(accessPage, "\\bUnitMeasurement\\b");
                bool field = Regex.IsMatch(accessPage, "\\bField\\b");
                bool attribute = Regex.IsMatch(accessPage, "\\bAttribute\\b");
                bool bank = Regex.IsMatch(accessPage, "\\bBank\\b");
                bool card = Regex.IsMatch(accessPage, "\\bCard\\b");
                bool particular = Regex.IsMatch(accessPage, "\\bParticular\\b");
                bool staff = Regex.IsMatch(accessPage, "\\bStaff\\b");
                bool serviceType = Regex.IsMatch(accessPage, "\\bServiceType\\b");

                if (store == false && manufacturer == false && location == false && supplier == false &&
                    category == false && unitMeasurement == false && field == false && attribute == false &&
                    bank == false && card == false && particular == false && staff == false && serviceType == false)
                {
                    mdlRecords.Controls.Clear();
                }

                // Check Settings Module 
                bool subscription = Regex.IsMatch(accessPage, "\\bSubscription\\b");
                bool user = Regex.IsMatch(accessPage, "\\bUser\\b");
                bool security = Regex.IsMatch(accessPage, "\\bSecurity\\b");
                bool support = Regex.IsMatch(accessPage, "\\bSupport\\b");
                bool web = Regex.IsMatch(accessPage, "\\bWeb\\b");
                bool version = Regex.IsMatch(accessPage, "\\bVersion\\b");
                bool docs = Regex.IsMatch(accessPage, "\\bDocs\\b");
                bool payment = Regex.IsMatch(accessPage, "\\bPayment\\b");

                if (subscription == false && user == false && security == false && support == false && web == false && version == false && docs == false && payment == false )
                {
                    mdlSettings.Controls.Clear();
                }

                // Check Offline Module 
                bool offline = Regex.IsMatch(accessPage, "\\bOffline\\b");

                if (offline == false)
                {
                    mdlOffline.Controls.Clear();
                }






            }



            string host = Request.Url.Host;

            if (Session["email"] == null && host != "localhost")
            {
                var obj = new DataAccess.CommonFunction();
                obj.logout();
            }

            if (!IsPostBack)
            {
                btnNotification.Visible = false;
                isQuotaNotification = objCommonFun.findSettingItemValue(30);

                if (isQuotaNotification == "1")
                {
                    btnNotification.Visible = true;
                    lblAlertText.InnerText = objQuotationModelModel.Notification();
                }

            }

            checkAuthorization();
        }





        private void checkAuthorization()
        {
            try
            {
                lblRoleID.Text = Session["email"].ToString();
                lblComName.Text = Session["comName"].ToString();
            }
            catch (Exception)
            {
                
            }
        }





        protected void btnLogout_Click(object sender, EventArgs e)
        {
            objCommonFun.logout();
        }






    }


}
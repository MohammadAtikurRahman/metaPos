using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CrystalDecisions.Shared.Json;
using MetaPOS.Admin.AppBundle.Service;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.SaleBundle.View;
using MetaPOS.Admin.SettingBundle.Service;
using Newtonsoft.Json;
using MetaPay.PortWallet;
using Newtonsoft.Json.Linq;
using Color = System.Drawing.Color;
using System.Text;
using System.Data;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using MetaPOS.Admin.ApiBundle.Controllers;
using MetaPay.PortWallet.Entities;
using MetaPOS.Admin.SubscriptionBundle.Service;


namespace MetaPOS.Admin.SubscriptionBundle.View
{
    public partial class Subscription : BasePage
    {
        private CommonFunction commonFunction = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (!commonFunction.accessChecker("Subscription"))
                {
                    commonFunction.pageout();
                }

                var invoice = Request.QueryString["invoice"];
                var status = Request.QueryString["status"];
                if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(invoice))
                {

                    var invoice_num = invoice;
                    if (invoice.Contains(','))
                    {
                        invoice_num = invoice.Split(',')[0];
                    }

                    Response.Redirect("/admin/subscription?invoice=" + invoice_num );

                }

                if (!string.IsNullOrEmpty(invoice))
                {
                    confirmPaymentAction(invoice);
                    autoPaymentWhenBalanceAvailable();
                }


            }
            

            autoPaymentWhenBalanceAvailable();
            lblNextExpiryDate.Text = Convert.ToDateTime(Session["ExpiryDate"]).ToString("dd-MMM-yyyy");
        }



        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void autoPaymentWhenBalanceAvailable()
        {
            var subscription = new Service.Subscription();
            var isAutoPayment = true;
            var totalBalance = subscription.getBalance(isAutoPayment);
            


            var dtSubsciption = subscription.getPendingInvoiceList();
            for (int i = 0; i < dtSubsciption.Rows.Count; i++)
            {
                if (totalBalance >= Convert.ToDecimal(dtSubsciption.Rows[i]["cashout"].ToString()))
                {
                    var id = dtSubsciption.Rows[i]["id"].ToString();
                    subscription.updateStatus(id);

                    DateTime initialdate = Convert.ToDateTime(Session["expiryDate"]);
                    Session["expiryDate"] = initialdate.AddMonths(1);
                    //Session["expiryDate"] = initialdate.AddDays(5);
                    DateTime expiryDate = Convert.ToDateTime(Session["expiryDate"]);
                    subscription.updateSubscriptionDateAfterPayment(expiryDate);
                }
            }



        }








        public enum MessageType
        {
            Success,
            Error,
            Info,
            Warning


        };




        protected void btnPayment_OnClick(object sender, EventArgs e)
        {

            try
            {
                var balance = txtPaymentAmount.Text == "" ? "0" : txtPaymentAmount.Text;
                var addBalance = Convert.ToDecimal(balance);
                if (addBalance < 5)
                {
                    //string title = "Warning";
                    //string body = "Invoice balance already is used";
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                    ScriptMessage(Resources.Language.Lbl_subscription_you_must_pay_10_tk_or_more, MessageType.Warning);
                    return;
                }

                var subscription = new Service.Subscription();
                var dtSubscription = subscription.getSubscriptionDataForPayment();

                if (dtSubscription.Rows.Count == 0)
                {
                    //string title = "Warning";
                    //string body = "Subscription information is not found, please contact support team.";
                    //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                    ScriptMessage(Resources.Language.Lbl_subscription_your_company_information_is_not_available_please_contact_our_support_team, MessageType.Warning);
                    return;
                }

                Session["emailAddress"] = txtEmailAddress.Text;



                string host = HttpContext.Current.Request.Url.Host;
                if (host == "localhost")
                    host = "http://localhost:4355";
                else
                    host = "http://" + host;

                Dictionary<string, string> orderData = new Dictionary<string, string>();
                orderData.Add("amount", addBalance.ToString());
                orderData.Add("currency", "BDT");
                orderData.Add("ipn_url", host);
                orderData.Add("redirect_url", host + "/admin/subscription");
                orderData.Add("reference", "roleID:" + dtSubscription.Rows[0]["roleID"]);

                Dictionary<string, string> productData = new Dictionary<string, string>();
                productData.Add("name", "Add Balance: " + dtSubscription.Rows[0]["branchName"]);
                productData.Add("description", "Payment Collected by: Robi Amar Hishab");

                string customerName = dtSubscription.Rows[0]["branchName"].ToString() == "" ? "Your Company" : dtSubscription.Rows[0]["branchName"].ToString();
                string customerPhoneDb = dtSubscription.Rows[0]["branchPhone"].ToString() == "" ? "01700000000" : dtSubscription.Rows[0]["branchPhone"].ToString();
                string customerEmail = dtSubscription.Rows[0]["email"].ToString() == "" ? "test@metaposbd.com" : dtSubscription.Rows[0]["email"].ToString();
                string customerPhone = customerPhoneDb;
                if (customerPhoneDb.Contains('-'))
                {
                  customerPhone = customerPhone.Replace("-", string.Empty);
                }
                Dictionary<string, string> customerData = new Dictionary<string, string>();
                customerData.Add("name", customerName);
                customerData.Add("phone", customerPhone);
                customerData.Add("email", customerEmail);

                Dictionary<string, string> addressData = new Dictionary<string, string>();
                addressData.Add("street", "Mirpur 6");
                addressData.Add("city", "Dhaka");
                addressData.Add("state", "Asia");
                addressData.Add("zipcode", "1216");
                addressData.Add("country", "BD");

                var paymentReq = new PaymentRequest();
                paymentReq.dicOrderData = orderData;
                paymentReq.dicProductData = productData;
                paymentReq.dicCustomerData = customerData;
                paymentReq.dicAddressData = addressData;
                var msg = paymentReq.CreateInvoice(false);


                var resultJson = commonFunction.deSerializeJsonToObject(msg);
                lblTest.Text += ":A3-2:";
                var result = resultJson["result"].ToString();
                lblTest.Text += "result::" + result;
                if (result == "success")
                {
                    var data = resultJson["data"];
                    var action = resultJson["data"]["action"];
                    string url = resultJson["data"]["action"]["url"].ToString();

                    Response.Write("<script>");
                    Response.Write("window.open('" + url + "','_blank')");
                    Response.Write("</script>");
                    lblTest.Text += "//redirect::";
                    Response.Redirect(url, false);
                }
                else
                {
                    var error = resultJson["error"].ToString();
                    var errorCause = resultJson["error"]["cause"].ToString();
                    var errorExplanation = resultJson["error"]["explanation"].ToString();

                    ScriptMessage(errorExplanation, MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                ScriptMessage(Resources.Language.Lbl_subscription_your_process_is_not_complete_please_contact_our_support_team, MessageType.Error);
                lblMessage.Text = ex.ToString();
            }
        }



        [WebMethod]
        public string confirmPaymentAction(string invoice)
        {
            var subscription = new Service.Subscription();
            var userService = new UserService();
            var invoiceData = subscription.getInvoiceData(invoice);

            //invoice = @"{"result":"success","data":{"invoice_id":"85E78E8316B89073","reference":"roleID:3","order":{"amount":"30440.00","currency":"BDT","type":"purchase","has_emi":0,"discount_availed":0,"redirect_url":"http:\/\/localhost:4355\/admin\/subscription","created_at":"Mon, 23 Mar 2020 22:47:45 +0600","status":"ACCEPTED","is_recurring_payment":0},"product":{"name":" Add Balance: Matador Tredras","description":"Payment collect by metaPOS"},"billing":{"customer":{"name":"Matador Tredras","email":"branch@metaposbd.com","phone":"01723698728","address":{"street":"Mirpur 6","city":"Dhaka","state":"Asia","zipcode":"1216","country":"Bangladesh"}},"payer":{"name":"","account":"01572135011","ip_address":"192.168.32.182","user_agent":"Mozilla\/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit\/537.36 (KHTML, like Gecko) Chrome\/80.0.3987.149 Safari\/537.36"},"gateway":{"category":"mfs","network":"bkash","status":"APPROVED","approval_code":"","reason":"1584982026( 30440 BDT - 2020-03-23T04:47:06.200+06:00 ) "},"source":{"category":"mfs","number":"01572135011","brand":{"name":null,"type":null,"category":null},"issuer":{"name":null,"phone":null,"website":null,"country":{"name":null,"iso2":null,"iso3":null}}}},"shipping":{"customer":{"name":"Matador Tredras","email":"branch@metaposbd.com","phone":"01723698728","address":{"street":"Mirpur 6","city":"Dhaka","state":"Asia","zipcode":"1216","country":"Bangladesh"}}},"transactions":[{"amount":"30440.00","currency":"BDT","status":"ACCEPTED","type":"purchase","time":"Mon, 23 Mar 2020 22:47:58 +0600"},{"amount":"-701.12","currency":"BDT","status":"ACCEPTED","type":"fee","time":"Mon, 23 Mar 2020 22:47:58 +0600"}]}}";
            string result = subscription.checkValidInvoice(invoiceData);

            string title = "", body = "";

            if (result.ToString().Trim() == "ACCEPTED")
            {

                // check to invoice balance already added
                var isInvoieUsed = subscription.checkInvoiceBalanceAlreadyUsed(invoiceData);

                var resultJson = commonFunction.deSerializeJsonToObject(invoiceData);
                var amount = resultJson["data"]["order"]["amount"].ToString();

                if (isInvoieUsed)
                {
                    title = Resources.Language.Lbl_subscription_caution;
                    body = Resources.Language.Lbl_subscription_the_balance_of_this_invoice_has_been_used;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                    return "";
                }


                var msg = subscription.saveSubscription(invoiceData);

                var spiltMesg = msg.Split('|');

                var messageType = MessageType.Success; ;
                if (spiltMesg[0] == "success")
                {
                    messageType = MessageType.Success;
                    title = Resources.Language.Lbl_subscription_success;
                    body = Resources.Language.Lbl_subscription_your_payment_has_been_succesful;

                    //
                    try
                    {
                        string emailAddress = Session["emailAddress"].ToString();
                        if (!String.IsNullOrEmpty(emailAddress))
                            SendPDFToEmail(emailAddress, amount, invoiceData);
                    }
                    catch (Exception)
                    {
                        body += Resources.Language.Lbl_subscription_but_failed_to_send_mail;
                    }

                }
                else if (spiltMesg[0] == "warning")
                {
                    messageType = MessageType.Warning;
                    title = Resources.Language.Lbl_subscription_caution;
                    body = Resources.Language.Lbl_subscription_your_payment_has_been_failed;
                }
                else
                {
                    messageType = MessageType.Error;
                    title = Resources.Language.Lbl_subscription_caution;
                    body = Resources.Language.Lbl_subscription_your_payment_has_been_failed_please_contact_our_support_center;
                }


                //body = spiltMesg[1];
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);

                ScriptMessage(spiltMesg[1], messageType);

            }
            else
            {
                title = Resources.Language.Lbl_subscription_caution;
                body = Resources.Language.Lbl_subscription_your_payment_is_not_succesful_please_contact_our_support_center + result;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);

            }

            return "";
        }




        [WebMethod]
        public static string loadCurrentBalanceAction()
        {
            var subscription = new Service.Subscription();
            var balance = subscription.getBalance(false);

            return balance.ToString("0.00");
        }


        [WebMethod]
        public static string loadTotalDueAmontAction()
        {
            var subscription = new Service.Subscription();
            var dtSubsciption = subscription.getPendingInvoiceList();
            var totalDie = 0M;
            for (int i = 0; i < dtSubsciption.Rows.Count; i++)
            {
                totalDie += Convert.ToDecimal(dtSubsciption.Rows[i]["cashout"].ToString());
            }

            return totalDie.ToString("0.00");
        }







        [WebMethod]
        public static string getSubscriptionDataListAction()
        {

            var commonService = new CommonService();
            var subscription = new Service.Subscription();
            var invoiceData = subscription.getSubscriptionData();
            if (invoiceData.Rows.Count > 0)
                return commonService.serializeDatatableToJson(invoiceData);

            return "";
        }




        private void SendPDFToEmail(string emailAddress, string amount, string invoiceData)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // Generate invoice
                    var baseBalance = amount;
                    var vat = 15M;

                    //baseBalance=1150
                    var withoutVatBalance = (Convert.ToDecimal(baseBalance) * 100) / 115; //1000
                    var totalVat = (withoutVatBalance * vat) / 100;  //150
                    var totalBalance = withoutVatBalance + totalVat;//1150
                    


                    var jsonObject = commonFunction.deSerializeJsonToObject(invoiceData);
                    var data = jsonObject["data"];
                    string invoiceNo = data["invoice_id"].ToString();


                    string customer = "", phone = "";
                    try
                    {
                        customer = string.IsNullOrEmpty(Session["comName"].ToString()) ? " " : Session["comName"].ToString();
                        phone = string.IsNullOrEmpty(Session["comPhone"].ToString()) ? " " : Session["comPhone"].ToString();
                    }
                    catch (Exception)
                    {
                    }

                    StringBuilder stringBuilder = new StringBuilder();

                    string pdfTable = "";


                    pdfTable = "<table width='100%' cellspacing='0' cellpadding='10'>" +
                               "<tr>" +
                               "<td><img width='280px' src='https://new.robiamarhishab.xyz/img/robi-address-pdf-logo.jpg'/></td>" +
                               "<td><img width='110px' align = 'right' src='https://new.robiamarhishab.xyz/img/robi-pdf-logo.jpg'/></td>" +     
                               "</tr>" +
                               "</table>";


                    pdfTable += "<table width='100%' cellspacing='0'>" +
                                "<tr><td align = 'center' colspan = '2'>The Government of the People's Republic of Bangladesh</td></tr>" +
                                "<tr><td align = 'center' colspan = '2'><b>National Board of Revenue</b></td></tr>" +
                                "</table>";

                    pdfTable += "<table cellspacing='0' align = 'right' bordercolor='#ccc'>" +
                                "<tr><td align = 'right' colspan = '2'><b><font size='2'>Mushak-6.3</font></b></td></tr>" +
                                "</table>";

                    pdfTable += "<table width='100%' cellspacing='0'>" +
                                "<tr><td align = 'center' colspan = '2'><b>Tax Invoice</b></td></tr>" +
                                "<tr><td align = 'center' colspan = '2'>(Clauses c and f of Sub Rule 1 of Rule 40)</td></tr>" +
                                "</table>";


                    pdfTable += "<table width='100%' cellspacing='0'>"+

	                            "<tr><td colspan = '2'></td></tr>"+

	                            "<tr>"+
                                "<td><font size='2'>Name of registered Company:</font></td>" +
                                "<td align = 'left'><font size='2'>Robi Axiata Limited</font></td>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>BIN of registered Company:</font></td>" +
                                "<td align = 'left'><font size='2'>000000178-001</font></td>" +
	                           "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>Name of registered Company:</font></td>" +
                                "<td align = 'left'><font size='2'>Robi Corporate Office, 53, Gulshan South Avenue, Gulshan 1, Dhaka 1212.</font></td>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>Name of purchaser:</font></td>" +
                                "<td align = 'left'><font size='2'>" + customer + "</font></td>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>BIN number of purchaser :</font></td>" +
                                "<td align = 'left'><font size='2'>--</td></font>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>Address of purchaser :</font></td>" +
                                "<td align = 'left'><font size='2'>--</font></td>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>Destination of supply :</font></td>" +
                                "<td align = 'left'><font size='2'>Website</font></td>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>Vehicle Type and Number:</font></td>" +
                                "<td align = 'left'><font size='2'>Not applicable</font></td>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>Invoice No:</font></td>" +
                                "<td align = 'left'><font size='2'>#" + invoiceNo + "</font></td>" +
	                            "</tr>"+
	                            "<tr>"+
                                "<td><font size='2'>Date:</td>" +
                                "<td align = 'left'><font size='2'>" + DateTime.Now.ToString("dd-MMM-yyyy") + "</font></td>" +
	                           " </tr>"+

                            "</table>"+
                            "<br />";

                    pdfTable += "<table cellspacing='0' align = 'right' bordercolor='#ccc'>" +
                               "<tr><td align = 'right' colspan = '2'><font size='2'>Mode of Payment: Online</font></td></tr>" +
                               "</table>";


                    // billing table
                    pdfTable += "<table width='100%' border='1' cellspacing='0' cellpadding='10' bordercolor='#ccc'>"+
                                "<tr bgcolor='#DCDCDC' align='center'>"+
                                "<th width='10%'><font size='2'>SL No.</font></th>"+
                                "<th width='15%'><font size='2'>Description of goods/service (where applicable, with brand name)</font></th>"+
                                "<th width='10%'><font size='2'>Unit of Supply</font></th>"+
                                "<th width='10%'><font size='2'>Quantity</font></th>"+
                                "<th width='10%'><font size='2'>Total Price (Taka)</font></th>"+
                                "<th width='15%'><font size='2'>Value Added Tax (VAT)/ Specific tax rate</font></th>"+
                                "<th width='15%'><font size='2'>Amount of VAT / Specific tax rate</font></th>"+
                                "<th width='15%'> <font size='2'>Total Price including all Duty & Tax (Taka)</font></th>"+
                                "</tr>";


                    pdfTable += "<tr align='center'>" +
                                "<td><font size='2'>01</font></td>" +
                                "<td><font size='2'>Robi Amar Hishab</font></td>" +
                                "<td><font size='2'>-</font></td>" +
                                "<td><font size='2'>-</font></td>" +
                                "<td align='right'><font size='2'>" + withoutVatBalance.ToString("0.00") + "</font></td>" +
                                "<td align='right'><font size='2'>"+ vat +"%</font></td>" +
                                "<td align='right'><font size='2'>" + totalVat.ToString("0.00") + "</font></td>" +
                                "<td align='right'><font size='2'>" + baseBalance + "</font></td>" +
                                "</tr>";


                    pdfTable += "<tr>" +
                                "<td colspan='6' align='right'><font size='2'>Total Amount</font></td>" +
                                "<td align='right'><font size='2'>" + totalVat.ToString("0.00") + "</font></td>" +
                                "<td align='right'><font size='2'>" + totalBalance.ToString("0.00") + "</font></td>" +
                                "</tr>" +
                                "</table>";


                    pdfTable += "<br />" +
                                "<p><b cellpadding='10'>This is a computer generated invoice and require no signature.</b></p>"+
                                "<br />" +
                                "<p>Note: As per SRO No. 149-Ain/2020/110-Mushak dated: 11.06.2020, VAT deducation at source is not required from this invoice.</p>";

                    stringBuilder.Append(pdfTable);


                    var template = @" সন্মানিত কাস্টমার, <br/>
                                    ইমেইলের মাধ্যমে আপনাকে নিশ্চিত করা হচ্ছে যে, আপনি সফটওয়্যার সাবস্ক্রিপশন হিসেবে " + totalBalance.ToString("0.00") + @" টাকা রবি আমার হিসাবকে জমা দিয়েছেন। আপনার বিস্তারিত ইনভয়েসটি সংযুক্ত করা রয়েছে।
                                    <br/><br/>
                                    ধন্যবাদ,<br/>
                                    রবি আমার হিসাব ";
                    SendController sendController = new SendController();
                    sendController.Post(emailAddress, "sadiq.alam@gmail.com,hasnat.abul@robi.com.bd", "Robi Amar Hishab Subscription Receipt " + customer + "", template, "1200", stringBuilder);

                }
            }
        }





        protected void pdfGenerate_OnClick(object sender, EventArgs e)
        {


            var emailAddress = txtEmailAddress.Text;
            var subscriptionModel = new SubscriptionModel();
            //var result = subscriptionModel["amount"]["transactions"];
            //var status = result[0]["amount"].ToString();
        }



    }
}
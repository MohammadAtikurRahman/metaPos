using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.ApiBundle.Controllers;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Account.View
{
    public partial class thankyou : System.Web.UI.Page
    {
        private CommonFunction commonFunction = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var status = Request.QueryString["status"];
                var invoice = Request.QueryString["invoice"];
                if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(invoice))
                {
                    Response.Redirect("/thank-you?invoice=" + invoice + "");
                }
                confirmResisterPaymentAction(invoice);
                
            }
            
        }

        public string confirmResisterPaymentAction(string invoice)
        {
            bool isPaymentMood= true;
            var signupService = new Service.SignupService();
            var invoiceData = signupService.getInvoiceData(invoice,isPaymentMood);

           
            //invoice = @"{"result":"success","data":{"invoice_id":"85E78E8316B89073","reference":"roleID:3","order":{"amount":"30440.00","currency":"BDT","type":"purchase","has_emi":0,"discount_availed":0,"redirect_url":"http:\/\/localhost:4355\/admin\/subscription","created_at":"Mon, 23 Mar 2020 22:47:45 +0600","status":"ACCEPTED","is_recurring_payment":0},"product":{"name":" Add Balance: Matador Tredras","description":"Payment collect by metaPOS"},"billing":{"customer":{"name":"Matador Tredras","email":"branch@metaposbd.com","phone":"01723698728","address":{"street":"Mirpur 6","city":"Dhaka","state":"Asia","zipcode":"1216","country":"Bangladesh"}},"payer":{"name":"","account":"01572135011","ip_address":"192.168.32.182","user_agent":"Mozilla\/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit\/537.36 (KHTML, like Gecko) Chrome\/80.0.3987.149 Safari\/537.36"},"gateway":{"category":"mfs","network":"bkash","status":"APPROVED","approval_code":"","reason":"1584982026( 30440 BDT - 2020-03-23T04:47:06.200+06:00 ) "},"source":{"category":"mfs","number":"01572135011","brand":{"name":null,"type":null,"category":null},"issuer":{"name":null,"phone":null,"website":null,"country":{"name":null,"iso2":null,"iso3":null}}}},"shipping":{"customer":{"name":"Matador Tredras","email":"branch@metaposbd.com","phone":"01723698728","address":{"street":"Mirpur 6","city":"Dhaka","state":"Asia","zipcode":"1216","country":"Bangladesh"}}},"transactions":[{"amount":"30440.00","currency":"BDT","status":"ACCEPTED","type":"purchase","time":"Mon, 23 Mar 2020 22:47:58 +0600"},{"amount":"-701.12","currency":"BDT","status":"ACCEPTED","type":"fee","time":"Mon, 23 Mar 2020 22:47:58 +0600"}]}}";
            //string result = signupService.checkValidInvoice(invoiceData);
            
            string title = "", body = "";
            var resultJson = commonFunction.deSerializeJsonToObject(invoiceData);
            var amount = resultJson["data"]["order"]["amount"].ToString();
            var status = resultJson["data"]["order"]["status"].ToString();
            var email = resultJson["data"]["billing"]["customer"]["email"].ToString();
            var subdomain = resultJson["data"]["billing"]["customer"]["name"].ToString().ToLower();


            //result.ToString().
            if (status.Trim() == "ACCEPTED")
            { 
                try
                {
                    if (!String.IsNullOrEmpty(email))
                        excuteDbQuery(subdomain);
                        sendPDFToEmailForResister(email, amount, subdomain);
                }
                catch (Exception)
                {
                    body += Resources.Language.Lbl_subscription_but_failed_to_send_mail;
                }


                //body = spiltMesg[1];
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);

            }
            else
            {
                title = Resources.Language.Lbl_subscription_caution;
                body = Resources.Language.Lbl_subscription_your_payment_is_not_succesful_please_contact_our_support_center + status;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);

            }


            return "";
        }

        private void sendPDFToEmailForResister(string emailAddress, string amount, string subdomain)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // Generate invoice
                    var baseBalance = amount;
                    var vat = 15M;
                    var extraDomain ="0";

                    //baseBalance=1150
                    var withoutVatBalance = (Convert.ToDecimal(baseBalance) * 100) / 115; //1000
                    var totalVat = (withoutVatBalance * vat) / 100;  //150
                    var totalBalance = withoutVatBalance + totalVat;//1150


                    if (amount == "1150.00" || amount == "920.00")
                    {
                        extraDomain = "0";
                    }
                    else if (amount == "1437.50" || amount == "1207.50")
                    {
                        extraDomain = "1";
                    }
                    else if (amount == "1725.00" || amount == "1495.00")
                    {
                        extraDomain = "2";
                    }
                    else if (amount == "2012.50" || amount == "1782.50")
                    {
                        extraDomain = "3";
                    }
                    else if (amount == "2300.00" || amount == "2070.00")
                    {
                        extraDomain = "4";
                    }
                    else if (amount == "2587.50" || amount == "2357.50")
                    {
                        extraDomain = "5";
                    }
                    else if (amount == "2875.00" || amount == "2645.00")
                    {
                        extraDomain = "6";
                    }



                    //var jsonObject = commonFunction.deSerializeJsonToObject(invoiceData);
                    //var data = jsonObject["data"];
                    //string invoiceNo = data["invoice_id"].ToString();
                    string invoiceNo = "INV-RAH-" + DateTime.Now.ToString("hhmmddMMyy") + "";


                    string customer = "";//, phone = "";
                    try
                    {
                        customer = string.IsNullOrEmpty(subdomain) ? " " : subdomain;
                        //phone = string.IsNullOrEmpty(Session["comPhone"].ToString()) ? " " : Session["comPhone"].ToString();
                    }
                    catch (Exception)
                    {
                    }

                    StringBuilder stringBuilder = new StringBuilder();

                    string pdfTable = "";


                    pdfTable = "<table width='100%' cellspacing='0' cellpadding='10'>" +
                               "<tr>" +
                               "<td><img width='280px' src='http://robi.robiamarhishab.com/img/robi-payment-pdf-logo.jpg'/></td>" +
                               "<td><img width='110px' align = 'right' src='http://robi.robiamarhishab.com/img/logo.jpg'/></td>" +
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


                    pdfTable += "<table width='100%' cellspacing='0'>" +

                                "<tr><td colspan = '2'></td></tr>" +

                                "<tr>" +
                                "<td><font size='2'>Name of registered Company:</font></td>" +
                                "<td align = 'left'><font size='2'>Robi Axiata Limited</font></td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>BIN of registered Company:</font></td>" +
                                "<td align = 'left'><font size='2'>000000178-001</font></td>" +
                               "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>Name of registered Company:</font></td>" +
                                "<td align = 'left'><font size='2'>Robi Corporate Office, 53, Gulshan South Avenue, Gulshan 1, Dhaka 1212.</font></td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>Name of purchaser:</font></td>" +
                                "<td align = 'left'><font size='2'>" + customer + "</font></td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>BIN number of purchaser :</font></td>" +
                                "<td align = 'left'><font size='2'>--</td></font>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>Address of purchaser :</font></td>" +
                                "<td align = 'left'><font size='2'>--</font></td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>Destination of supply :</font></td>" +
                                "<td align = 'left'><font size='2'>Website</font></td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>Vehicle Type and Number:</font></td>" +
                                "<td align = 'left'><font size='2'>Not applicable</font></td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>Invoice No:</font></td>" +
                                "<td align = 'left'><font size='2'>" + invoiceNo + "</font></td>" +
                                "</tr>" +
                                "<tr>" +
                                "<td><font size='2'>Date:</td>" +
                                "<td align = 'left'><font size='2'>" + DateTime.Now.ToString("dd-MMM-yyyy") + "</font></td>" +
                               " </tr>" +

                            "</table>" +
                            "<br />";

                    pdfTable += "<table cellspacing='0' align = 'right' bordercolor='#ccc'>" +
                               "<tr><td align = 'right' colspan = '2'><font size='2'>Mode of Payment: Online</font></td></tr>" +
                               "</table>";


                    // billing table
                    pdfTable += "<table width='100%' border='1' cellspacing='0' cellpadding='10' bordercolor='#ccc'>" +
                                "<tr bgcolor='#DCDCDC' align='center'>" +
                                "<th width='10%'><font size='2'>SL No.</font></th>" +
                                "<th width='15%'><font size='2'>Description of goods/service (where applicable, with brand name)</font></th>" +
                                "<th width='10%'><font size='2'>Unit of Supply</font></th>" +
                                "<th width='10%'><font size='2'>Branches</font></th>" +
                                "<th width='10%'><font size='2'>Total Price (Taka)</font></th>" +
                                "<th width='15%'><font size='2'>Value Added Tax (VAT)/ Specific tax rate</font></th>" +
                                "<th width='15%'><font size='2'>Amount of VAT / Specific tax rate</font></th>" +
                                "<th width='15%'> <font size='2'>Total Price including all Duty & Tax (Taka)</font></th>" +
                                "</tr>";


                    pdfTable += "<tr align='center'>" +
                                "<td><font size='2'>01</font></td>" +
                                "<td><font size='2'>Robi Amar Hishab</font></td>" +
                                "<td><font size='2'>-</font></td>" +
                                "<td><font size='2'>" + Convert.ToInt32(extraDomain) + " X 250</font></td>" +
                                "<td align='right'><font size='2'>" + withoutVatBalance.ToString("0.00") + "</font></td>" +
                                "<td align='right'><font size='2'>" + vat + "%</font></td>" +
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
                                "<p><b cellpadding='10'>This is a computer generated invoice and require no signature.</b></p>" +
                                "<br />" +
                                "<p>Note: As per SRO No. 149-Ain/2020/110-Mushak dated: 11.06.2020, VAT deducation at source is not required from this invoice.</p>";

                    stringBuilder.Append(pdfTable);

                    string userDomain = "";
                    var domainName = HttpContext.Current.Request.Url.Host;
                    try
                    {
                        domainName = domainName.ToLower();
                        domainName = domainName.Replace("https://", string.Empty);
                        domainName = domainName.Replace("http://", string.Empty);
                        string[] splitDomain = domainName.Split('.');
                        if (splitDomain.Length > 1)
                            userDomain = splitDomain[splitDomain.Length - 2] + "." + splitDomain[splitDomain.Length - 1];
                        else
                            userDomain = domainName;
                    }
                    catch (Exception)
                    {
                        userDomain = domainName;
                    }

                    var companyUrl = "http://" + subdomain + "." + userDomain + "";
                    var userName = subdomain + "@" + userDomain;
                    //default data insert in UserLogsInfo
                    var accountModel = new AccountModel();
                    accountModel.email = userName;
                    accountModel.defaultUserLogsModel();

                    var template = @" সন্মানিত কাস্টমার, <br/>
                                    ইমেইলের মাধ্যমে আপনাকে নিশ্চিত করা হচ্ছে যে, আপনি সফটওয়্যার সাবস্ক্রিপশন হিসেবে " + totalBalance.ToString("0.00") + @" টাকা রবি আমার হিসাবকে জমা দিয়েছেন।আপনার বিস্তারিত ইনভয়েসটি সংযুক্ত করা রয়েছে।
                                    <br/><br/>আপনার কোম্পানি লিঙ্কঃ " + companyUrl + "<br/>ইউজার নামঃ " + userName + @"<br/>পাসওয়ার্ডঃ XL49S7T$5G 
                                    <br/><br/>
                                    ধন্যবাদ,<br/>
                                    রবি আমার হিসাব ";
                    SendController sendController = new SendController();
                    sendController.Post(emailAddress, "sadiq.alam@gmail.com", "Robi Amar Hishab Subscription Receipt " + customer + "", template, "1200", stringBuilder);

                }
            }
        }





        private void excuteDbQuery(string subdomain)
        {
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Server.MapPath(".") + "\\cdn\\createDB";
            process.StartInfo.FileName = "DB_User_ConnectionString.bat";
            var DbPass = "Gk" + DateTime.Now.ToString("mm") + "Zq" + DateTime.Now.ToString("ss") + "aE";
            process.StartInfo.Arguments = string.Format("{0},{1},{2}", subdomain, subdomain, DbPass);
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

        }

    }
}
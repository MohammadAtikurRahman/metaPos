using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Account.Service;
using MetaPOS.Account.Helper;
using MetaPOS.Admin.ApiBundle.Controllers;
using MetaPOS.Admin.DataAccess;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;
using MetaPOS.Admin.SubscriptionBundle.View;
using MetaPOS.Admin.SubscriptionBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Org.BouncyCastle.Tsp;


namespace MetaPOS.Account.View
{
    public partial class Signup : System.Web.UI.Page
    {
        private CommonFunction commonFunction = new CommonFunction();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var domain = commonFunction.GetDomain();

                string json = (new WebClient()).DownloadString(domain + "/Account/Data/districts.json");
                object result = JsonConvert.DeserializeObject<object>(json);
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < jArray.Length; i++)
                {
                    items.Add(new ListItem { Text = jArray[i]["name"].ToString(), Value = jArray[i]["id"].ToString() });
                }
                items.Insert(0, new ListItem { Text = "--Select District --", Value = "0" });

                ddlDistrict.DataSource = items;
                ddlDistrict.DataTextField = "Text";
                ddlDistrict.DataValueField = "Value";
                ddlDistrict.DataBind();
            }



        }

        [WebMethod]
        public static string signupToMetaposAction(string jsonData)
        {
            var signService = new SignupService();
            return signService.signupToMetapos(jsonData);
        }


        [WebMethod]
        public static bool checkSubdomainDataAction(string subdomain)
        {
            HttpContext.Current.Session["conString"] = "";
            var signService = new SignupService();
            return signService.checkSubdomain(subdomain);
        }


        [WebMethod]
        public static bool checkRegistationEmailDataAction(string email)
        {
            var signService = new SignupService();
            return signService.checkRegistationEmail(email);
        }


        protected void ddlDistrict_TextChanged(object sender, EventArgs e)
        {
            var district = ddlDistrict.SelectedValue;
            var domain = commonFunction.GetDomain();

            string json = (new WebClient()).DownloadString(domain + "/Account/Data/upazilas.json");
            object result = JsonConvert.DeserializeObject<object>(json);
            JToken[] jArray = ((result as JArray) as JToken).ToArray();
            List<ListItem> items = new List<ListItem>();
            for (int i = 0; i < jArray.Length; i++)
            {
                if (jArray[i]["district_id"].ToString() == district.ToString())
                {
                    items.Add(new ListItem { Text = jArray[i]["name"].ToString(), Value = jArray[i]["id"].ToString() });
                }
            }
            ddlSubDistrict.DataSource = items;
            ddlSubDistrict.DataTextField = "Text";
            ddlSubDistrict.DataValueField = "Value";
            ddlSubDistrict.DataBind();
        }




        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }



        protected void btnSignup_Click(object sender, EventArgs e)
        {
            var subdomain = txtBusinessName.Text.ToLower();
            var email = txtEmail.Text;
            var mobile = txtPhone.Text;
            var name = txtName.Text;
            var businessType = ddlBusinessType.SelectedValue;
            var district = ddlDistrict.SelectedItem.ToString();
            var subDistrict = ddlSubDistrict.SelectedItem.ToString();
            var address = txtAddress.Text;
            var package = ddlPackage.SelectedValue;
            var extraDomain = ddlDomainNo.SelectedValue;

            var vat = 15M;
            var amount = 0M;
            var totalVat = 0M;
            var extraDomainCharge = (250 * Convert.ToInt32(extraDomain)) * 1.15;

            if (package == "advance")
            {
                amount = 1150;
            }
            else
            {
                amount = 800;
                totalVat = (amount * vat) / 100;
                amount = amount + totalVat;
            }

            amount = amount + Convert.ToDecimal(extraDomainCharge);
            //btnSignup.Attributes.Add("disabled", "disabled");


            if (string.IsNullOrEmpty(name))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Name is required')", true);
                return;
            }


            if (string.IsNullOrWhiteSpace(subdomain))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Business name is required')", true);
                return;
            }

            if (businessType == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Business Type is required')", true);
                return;
            }

            if (package == "0" || package == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Package is required')", true);
                return;
            }


            if (district == "--Select District --" || district == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('District is required')", true);
                return;
            }

            if (subDistrict == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sub District is required')", true);
                return;
            }
            

            //save file to cdn location

            var folderPath = Server.MapPath("\\cdn\\signup\\");
            var fileName = "";

            if (fileUpload.HasFile)
            {
                fileName = subdomain + Path.GetExtension(fileUpload.PostedFile.FileName);

                if ((File.Exists(folderPath + fileName)))
                    File.Delete(folderPath + fileName);

                fileUpload.SaveAs(folderPath + Path.GetFileName(fileName));
            }
            if (fileName == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please upload purchase file.')", true);
                return;
            }



            string message = createEmailBody(subdomain, businessType, mobile, address, district, subDistrict, name, email, package, extraDomain);


            var sendMsg = SendMailToTeamAboutNewCustomer(message, subdomain, fileName);

            if (sendMsg == true)
            {
               // Response.Redirect("~/thank-you");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went to be wrong!!')", true);
                return;
            }

   

            //sign up payment
            string host = HttpContext.Current.Request.Url.Host;
            if (host == "localhost")
                host = "http://localhost:4355";
            else
                host = "http://" + host;

            Dictionary<string, string> orderData = new Dictionary<string, string>();
            orderData.Add("amount", amount.ToString());
            orderData.Add("currency", "BDT");
            orderData.Add("ipn_url", host);
            orderData.Add("redirect_url", host + "/thank-you");
            orderData.Add("reference", "roleID:" + 3);  //dtSubscription.Rows[0]["roleID"]);

            Dictionary<string, string> productData = new Dictionary<string, string>();
            productData.Add("name", "Add Balance: " + subdomain);    // dtSubscription.Rows[0]["branchName"]);
            productData.Add("description", "Payment Collected by: Robi Amar Hishab");

            string customerName = subdomain == "" ? "Your Company" : subdomain;
            string customerPhone = mobile == "" ? "01700000000" : mobile;
            string customerEmail = email== "" ? "test@metaposbd.com" : email;

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
            bool isPaymentMood = true;
            paymentReq.dicOrderData = orderData;
            paymentReq.dicProductData = productData;
            paymentReq.dicCustomerData = customerData;
            paymentReq.dicAddressData = addressData;
            var msg = paymentReq.CreateInvoice(isPaymentMood);

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


        private bool SendMailToTeamAboutNewCustomer(string message, string subdomain, string fileName)
        {
            var isSend = false;
            try
            {
                // mail to 
                string subject = "New Account Request by " + subdomain;

                //create the mail message 
                MailMessage mail = new MailMessage();

                //set the addresses 
                mail.From = new MailAddress("contact@mail.robiamarhishab.xyz"); //IMPORTANT: This must be same as your smtp authentication address.
                //mail.To.Add("shofikahmed72@gmail.com");
                mail.To.Add("sadiq.alam@gmail.com");
                mail.CC.Add("shofikahmed72@gmail.com");

                Attachment attachment;
                attachment = new Attachment(Server.MapPath("\\cdn\\signup\\") + fileName);
                mail.Attachments.Add(attachment);
                


                //set the content 
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //send the message 
                SmtpClient smtp = new SmtpClient("jupiter.exonhost.com");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("contact@mail.robiamarhishab.xyz", "Uey392p#1");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.Send(mail);
                attachment.Dispose();
                return true;

            }
            catch (Exception)
            {
                return isSend;
            }
        }


        private string createEmailBody(string subdomain, string business, string mobile, string address, string district, string subdistrict, string name, string email, string package, string extraDomain)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(Server.MapPath("~/Account/template/active-account.html")))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{businessname}", subdomain);

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

            body = body.Replace("{subdomain}", subdomain.Replace(" ", string.Empty) + "." + userDomain);

            body = body.Replace("{business}", business);

            body = body.Replace("{mobile}", mobile);

            body = body.Replace("{address}", address);

            body = body.Replace("{district}", district);

            body = body.Replace("{subdistrict}", subdistrict);

            body = body.Replace("{name}", name);

            body = body.Replace("{email}", email);

            body = body.Replace("{package}", package);

            body = body.Replace("domainNo", extraDomain);

            return body;

        }


    }
}
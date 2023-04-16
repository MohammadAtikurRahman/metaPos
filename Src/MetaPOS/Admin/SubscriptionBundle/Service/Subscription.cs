using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using MetaPOS.Admin.AppBundle.Service;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SubscriptionBundle.Service
{
    public class Subscription
    {
        private SubscriptionModel subscriptionModel = new SubscriptionModel();
        private CommonFunction commonFunction = new CommonFunction();

        public string getMonthlyFee()
        {
            var dtSubscription = subscriptionModel.getMonthlyFeeModel();
            var fee = "0.00";
            if (dtSubscription.Rows.Count > 0)
                fee = dtSubscription.Rows[0]["monthlyFee"].ToString();

            return fee;
        }




        public string saveSubscription(string invoiceData)
        {
            try
            {
                var roleId = HttpContext.Current.Session["roleID"].ToString();

                if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                {
                    roleId = HttpContext.Current.Session["branchId"].ToString();
                }


                var jsonObject = commonFunction.deSerializeJsonToObject(invoiceData);
                var data = jsonObject["data"];

                var cashin = Convert.ToDecimal(data["order"]["amount"].ToString() == "" ? "0" : data["order"]["amount"].ToString());

                var subsModel = new SubscriptionModel();
                subsModel.roleId = Convert.ToInt32(roleId);
                subsModel.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
                subsModel.invoiceNo = data["invoice_id"].ToString();
                subsModel.name = "Payment";
                subsModel.description = "";
                subsModel.paymentMode = "";
                subsModel.cashin = cashin;
                subsModel.cashout = 0M;
                subsModel.type = "Payment";
                subsModel.status = "1";
                subsModel.createDate = commonFunction.GetCurrentTime();
                subsModel.updateDate = commonFunction.GetCurrentTime();
                var isSaved = subsModel.saveSubscriptionModel();

                if (isSaved)
                    return "success|your payment is accepted.";
                else
                    return "warning|payment data is not saved in software";
            }
            catch (Exception)
            {
                return "error|Something went to wrong";
            }

        }




        public decimal getBalance(bool isAutoPayment)
        {
            var balance = 0M;
            var subscriptionModel = new SubscriptionModel();
            var dtBal = subscriptionModel.getBalanceModel(isAutoPayment);
            if (dtBal.Rows.Count > 0)
            {
                var totalCashin = Convert.ToDecimal(dtBal.Rows[0]["totalCashin"].ToString() == "" ? "0" : dtBal.Rows[0]["totalCashin"].ToString());
                var totalCashout = Convert.ToDecimal(dtBal.Rows[0]["totalCashout"].ToString() == "" ? "0" : dtBal.Rows[0]["totalCashout"].ToString());
                balance = totalCashin - totalCashout;
                
            }

            return balance;
        }

        public bool updateStatus(string Id)
        {
            var subscriptionModel = new SubscriptionModel();
            return subscriptionModel.updateStatusModel(Id);
        }

        public DataTable getSubscriptionData()
        {
            var subscriptionModel = new SubscriptionModel();
            return subscriptionModel.getSubscriptionDataModel();

        }




        public string getInvoiceData(string invoice)
        {
            string invoiceUrl = "";

            var paymentMode = commonFunction.findSettingItemValueDataTable("paymentMode");
            if (paymentMode == "0")
            {
                // Sandbox
                invoiceUrl = "https://api-sandbox.portwallet.com/payment/v2/invoice/" + invoice;
                MetaPay.PortWallet.Helpers.AppSecret.AppKey = "7deeb13a9ac909a89529933746f290a4";
                MetaPay.PortWallet.Helpers.AppSecret.SecretKey = "740db7ad5e1124a0d281591d2f455ed5";
            }
            else
            {
                // Live
                invoiceUrl = "https://api.portwallet.com/payment/v2/invoice/" + invoice;
                MetaPay.PortWallet.Helpers.AppSecret.AppKey = "54eda75356e89400e5b0d9947f998ff5";
                MetaPay.PortWallet.Helpers.AppSecret.SecretKey = "27391a4139496423c428bd5e40ed7adb";
            }



            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var token = MetaPay.PortWallet.RequestHandler.TokenRequest.generateToken();

                string html = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(invoiceUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                return html;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }




        public string checkValidInvoice(string invoiceData)
        {
            //bool isValid = false;
            var jsonObject = commonFunction.deSerializeJsonToObject(invoiceData);
            var result = jsonObject["data"]["transactions"];
            var status = result[0]["status"].ToString();

            return status;
        }





        public bool checkInvoiceBalanceAlreadyUsed(string invoiceData)
        {
            var jsonObject = commonFunction.deSerializeJsonToObject(invoiceData);

            var data = jsonObject["data"];

            var invoiceNo = data["invoice_id"].ToString();

            var subscriptionModel = new SubscriptionModel();
            subscriptionModel.invoiceNo = invoiceNo;
            var dtSubscription = subscriptionModel.getSubscriotionDataByInvoice();

            if (dtSubscription.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public bool sendPaymentMailToMetaPosTeam(string transectionId)
        {
            // mail to 
            string DomainName = HttpContext.Current.Request.Url.Host;
            string to = "sayedbrur@gmail.com";
            string cc = "khasancsit@gmail.com,mdapplemahmud3@gmail.com";

            string message = "Dear Members,<br/>"
                            + "Please confirm the payment requested by <b>" + HttpContext.Current.Session["comName"] + ".</b><br/>"
                            + "Trans ID: <b>" + transectionId + "</b> <br/>"
                            + "Website: <b>" + DomainName + "</b>"
                            + "<br/>"
                            + "Sincerely,<br/>"
                            + "metaPOS Team<br/>"
                            + "www.metaposbd.com<br/>";

            string subject = "metaPOS Subscription";

            HttpClient client = new HttpClient();
            string url = "http://app.metaposbd.com/api/sends/mail?to=" + to + "&cc=" + cc + "&sub=" + subject + "&msg=" + message + "&key=1200";

            return commonFunction.Sendmail(url);
        }




        public DataTable getPendingInvoiceList()
        {
            var sqlOpertion = new SqlOperation();
            string query = "SELECT * FROM SubscriptionInfo where status='0' " +
                           HttpContext.Current.Session["userAccessParameters"] + "";
            return sqlOpertion.getDataTable(query);
        }




        public DataTable getSubscriptionDataForPayment()
        {
            var subscriptionModel = new SubscriptionModel();
            return subscriptionModel.getSubscriptionDataForPaymentModel();
        }




        public void updateSubscriptionDateAfterPayment(DateTime date)
        {
            var subscriotion = new SubscriptionModel();
            subscriotion.UpdateRoleInfoDateModel(date);
            subscriotion.UpdateSubscriptionInfoDataModel();
        }

    }
}
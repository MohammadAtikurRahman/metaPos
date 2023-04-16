using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;


namespace MetaPOS.Account.Service
{
    public class SignupService
    {
        public int Id { get; set; }
        public string shopName { get; set; }
        public string subdomain { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }


        private CommonFunction commonFunction = new CommonFunction();
        private SqlOperation sqlOperation = new SqlOperation();


        public string signupToMetapos(string jsonData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            var connectionName = data["storeName"].ToString();
            var email = data["email"].ToString();

            // check domain
            var isDomainAvailable = commonFunction.CheckConnectionString(connectionName);
            if (isDomainAvailable)
            {
                return "false|" + connectionName + " is already exits";
            }
            // createMssqlDb
            var dbValue = createRestoreDatabase(connectionName);

            if (dbValue != "")
            {
                var msg = createMetaposProfile(jsonData, dbValue);
                if (msg.Split('|')[0] == "true")
                {
                    // login precode
                    var dtRoleAcess = sqlOperation.getDataTable("SELECT * FROM [RoleInfo] WHERE email = '" + email + "'");
                    if (dtRoleAcess.Rows.Count > 0)
                    {
                        commonFunction.LoginExecutable(dtRoleAcess, connectionName);
                    }
                    else
                        return "false|You can not login automatically.";
                }

                return msg;

            }


            return "false|Your registation is not complete.";

        }



        public string signupAction()
        {
            var connectionName  = subdomain;
            // check domain
            var isDomainAvailable = commonFunction.CheckConnectionString(connectionName);
            if (isDomainAvailable)
            {
                return "false|" + connectionName + " is already exits";
            }
            // createMssqlDb
            var dbValue = createRestoreDatabase(connectionName);

            //if (dbValue != "")
            //{
            //    var msg = createMetaposProfile(jsonData, dbValue);
            //    if (msg.Split('|')[0] == "true")
            //    {
            //        // login precode
            //        var dtRoleAcess = sqlOperation.getDataTable("SELECT * FROM [RoleInfo] WHERE email = '" + email + "'");
            //        if (dtRoleAcess.Rows.Count > 0)
            //        {
            //            commonFunction.LoginExecutable(dtRoleAcess, connectionName);
            //        }
            //        else
            //            return "false|You can not login automatically.";
            //    }

            //    return msg;

            //}

            return "";
        }




        private string createRestoreDatabase(string databaseName)
        {

            string data = "apiid=100171559&apikey={08A05C35-10A7-4162-96B3-D126CCAB94BD}&loginid=metapos-001&dbname=" + databaseName + "&dbloginpwd=qwer1234&dbtype=mssql2016&dbspace=50"; //replace <value>
            //string data = "apiid=100968761&apikey={EE8C32A9-36B1-43C4-AD6E-1A88F0B39076}&loginid=metapos-001&dbname=" + databaseName + "&dbloginpwd=qwer1234&dbtype=mssql2016&dbspace=50"; //replace <value>
            byte[] dataStream = Encoding.UTF8.GetBytes(data);
            //string request = "https://resellerapi.mySitePanel.net/createMssqlDb";
            string request = "https://resellerapi.smarterasp.net/createMssqlDb";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebRequest webRequest = WebRequest.Create(request);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = dataStream.Length;
            Stream newStream = webRequest.GetRequestStream();
            // Send the data.
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();
            WebResponse webResponse = webRequest.GetResponse();

            string databaseResponse = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

            var returnData = (JObject)JsonConvert.DeserializeObject(databaseResponse);
            var status = (bool)returnData["result"]["status"];

            if (status)
            {


                var dbName = returnData["result"]["dbinfo"]["dbname"].ToString();
                var isRestored = restoreCreatedDatabase(dbName);
                if (isRestored)
                {
                    crateWebConfigData(returnData, databaseName);

                    return databaseResponse;
                }
            }

            return "false|Your registation is not completed";

        }

        private void crateWebConfigData(JObject returnData, string databaseName)
        {

            var dbName = returnData["result"]["dbinfo"]["dbname"].ToString();

            var dbserver = returnData["result"]["dbinfo"]["dbserver"].ToString(); ;
            var dbloginid = dbName + "_admin";
            var dbLoginPwd = "qwer1234";
            //var dbtype = "MSSQL2016";


            // set web.config connectionstring name
            string connStringName = databaseName;
            Configuration Config1 = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection conSetting = (ConnectionStringsSection)Config1.GetSection("connectionStrings");


            //ConnectionStringSettings oldString = conSetting.ConnectionStrings[connStringName];
            ConnectionStringSettings newString = new ConnectionStringSettings(connStringName, "Data Source=" + dbserver + ";Initial Catalog=" + dbName + ";User ID=" + dbloginid + ";Password=" + dbLoginPwd + ";");

            //conSetting.ConnectionStrings.Remove(oldString);
            conSetting.ConnectionStrings.Add(newString);
            Config1.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Config1.AppSettings.SectionInformation.Name);
        }

        private bool restoreCreatedDatabase(string databaseName)
        {
            string data = "apiid=100171559&apikey={08A05C35-10A7-4162-96B3-D126CCAB94BD}&loginid=metapos-001&dbname=" + databaseName + "&dbloginpwd=qwer1234&dbtype=mssql2016&dbspace=50&bak_path=/db/saas_restore.bak"; //replace <value>
            byte[] dataStream = Encoding.UTF8.GetBytes(data);
            //string request = "https://resellerapi.mySitePanel.net/restoreMssqlDb"; resellerapi.mySitePanel.net=208.118.63.6
            string request = "https://resellerapi.smarterasp.net/restoremssqldb";
            WebRequest webRequest = WebRequest.Create(request);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = dataStream.Length;
            Stream newStream = webRequest.GetRequestStream();
            // Send the data.
            newStream.Write(dataStream, 0, dataStream.Length);
            newStream.Close();
            WebResponse webResponse = webRequest.GetResponse();
            string changelogResponse = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();

            var returnData = (JObject)JsonConvert.DeserializeObject(changelogResponse);
            var status = (bool)returnData["result"]["status"];
            return status;
        }

        private string createMetaposProfile(string jsonData, string dbValue)
        {
            // Role Update
            var isUpdatedRole = updateRoleData(jsonData);
            if (!isUpdatedRole)
                return "false|Your role data is not saved";

            // Branch Update
            var isUpdatedBranch = updateBranchData(jsonData);
            if (!isUpdatedBranch)
                return "false|Your branch data is not saved";

            // Send Mail
            var isSendMail = sendMailToRegisteredUser();
            if (!isSendMail)
                return "false|Mail is not sended.";

            // Send SMS
            var isSendSMS = sendSMSToRegisteredUser();
            if (!isSendSMS)
                return "false|SMS is not sended.";

            return "true|Your are signup successfully.";

        }

        private bool updateRoleData(string jsonData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            var dbName = data["storeName"].ToString();

            // RoleId 
            HttpContext.Current.Session["conString"] = dbName.Trim().ToLower();

            var accountModel = new AccountModel();
            accountModel.name = dbName;
            accountModel.number = data["phone"].Value<string>();
            accountModel.email = data["email"].Value<string>();
            accountModel.password = data["password"].Value<string>();
            accountModel.city = "YourCity";
            accountModel.company = dbName;
            accountModel.entryDate = commonFunction.GetCurrentTime();
            accountModel.updateDate = commonFunction.GetCurrentTime();
            accountModel.activeDate = commonFunction.GetCurrentTime();
            accountModel.expiryDate = commonFunction.GetCurrentTime().AddDays(30);
            accountModel.monthlyFee = 500M;
            return accountModel.updateRole();

        }

        private bool updateBranchData(string jsonData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            var dbName = data["storeName"].ToString();

            // RoleId 
            HttpContext.Current.Session["conString"] = dbName.Trim().ToLower();

            var accountModel = new AccountModel();
            accountModel.name = dbName;
            accountModel.number = data["phone"].Value<string>();
            accountModel.email = data["email"].Value<string>();
            accountModel.password = data["password"].Value<string>();
            accountModel.city = "Your City";
            accountModel.address = "Your Address";
            accountModel.company = dbName;
            accountModel.entryDate = commonFunction.GetCurrentTime();
            accountModel.updateDate = commonFunction.GetCurrentTime();
            return accountModel.updateBranch();
        }

        private bool sendMailToRegisteredUser()
        {

            var isSend = false;
            try
            {
                // mail to 
                string to = "sadiq.alam@gmail.com";
                string cc = "shofikahmed72@gmail.com";

                string message = "Dear metaPOS Team,<br/>"
                                + "Please add a subdomain requested by <b>.</b><br/>"
                                + "Sub Doamin: <b></b> <br/>"
                                + "Website: <b>.metaposbd.com</b>"
                                + "<br/>"
                                + "Sincerely,<br/>"
                                + "metaPOS Team<br/>"
                                + "www.metaposbd.com<br/>";

                string subject = "metaPOS Account Activation";

                string url = "http://app.metaposbd.com/api/sends/mail?to=" + to + "&cc=" + cc + "&sub=" + subject + "&msg=" + message + "&key=1200";
                isSend = commonFunction.Sendmail(url);
                return isSend;
            }
            catch (Exception)
            {
                return isSend;
            }
        }

        private bool sendSMSToRegisteredUser()
        {
            return true;
        }

        public bool checkSubdomain(string subdomain)
        {
            return commonFunction.CheckConnectionString(subdomain);

        }

        public bool checkRegistationEmail(string email)
        {
            var accountModel = new AccountModel();
            var dtAccount = accountModel.checkRegistationEmailModel(email);
            if (dtAccount.Rows.Count > 0)
                return true;
            else
                return false;
        }

        //get invoice Data here
        public string getInvoiceData(string invoice, bool isPaymentMood)
        {
            string invoiceUrl = "";
            var paymentMode = "0";
            if (isPaymentMood)
            {
                paymentMode = "1";//paymentMode is change 1/0 for signup payment// Sandbox=0 or PortWallet=1
            }
           
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



    }
}
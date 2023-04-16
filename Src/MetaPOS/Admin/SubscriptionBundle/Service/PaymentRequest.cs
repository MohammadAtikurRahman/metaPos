using MetaPay.PortWallet.RequestHandler;
using MetaPOS.Admin.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MetaPOS.Admin.SubscriptionBundle.Service
{
    public class PaymentRequest
    {
        private CommonFunction commonFunction = new CommonFunction();

        public Dictionary<string, string> dicOrderData { get; set; }
        public Dictionary<string, string> dicProductData { get; set; }
        public Dictionary<string, string> dicCustomerData { get; set; }
        public Dictionary<string, string> dicAddressData { get; set; }

        public string CreateInvoice(bool isPaymentMood)
        {
            return invoicePostRequst(isPaymentMood);
        }


        public string invoicePostRequst(bool isPaymentMood)
        {
            string json = "";
            try
            {
                string url = "";
                var paymentMode = "0";
                if (!isPaymentMood)
                {
                     paymentMode = commonFunction.findSettingItemValueDataTable("paymentMode");
                }

                if (isPaymentMood)
                {
                    paymentMode = "1";//paymentMode is change 1/0 for signup payment //Sandbox=0 or PortWallet=1
                }
   
                if (paymentMode == "0")
                {
                    // Sandbox
                    MetaPay.PortWallet.Helpers.AppSecret.AppKey = "7deeb13a9ac909a89529933746f290a4";
                    MetaPay.PortWallet.Helpers.AppSecret.SecretKey = "740db7ad5e1124a0d281591d2f455ed5";

                    url = "https://api-sandbox.portwallet.com/payment/v2/invoice";

                }
                else
                {

                    // Live
                    MetaPay.PortWallet.Helpers.AppSecret.AppKey = "54eda75356e89400e5b0d9947f998ff5";
                    MetaPay.PortWallet.Helpers.AppSecret.SecretKey = "27391a4139496423c428bd5e40ed7adb";

                    url = "https://api.portwallet.com/payment/v2/invoice";

                }


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var result = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                var token = TokenRequest.generateToken();

                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
                httpWebRequest.ContentType = "application/json; charset=utf-8";


                var amount = dicOrderData["amount"];
                var currency = dicOrderData["currency"];

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    //json = "{\"order\":{\"amount\": " +
                    //    Order.Amount + ",\"currency\":\"" +
                    //    Order.Currency + "\",\"redirect_url\":\"" +
                    //    Order.Redirect_url + "\",\"ipn_url\":\"" +
                    //    Order.Ipn_url + "\",\"reference\":\"" +
                    //    Order.Reference + "\",\"validity\":2592000" +
                    //    "}," +
                    //     "\"product\":{\"name\":\" " +
                    //     Product.Name + "\",\"description\":\"" +
                    //     Product.Description + "\"}," +
                    //     "\"billing\":{" +
                    //            "\"customer\":{" +
                    //                    "\"name\":\"" +
                    //                    Customer.Name + "\",\"email\":\"" +
                    //                    Customer.Email + "\",\"phone\":\"" +
                    //                    Customer.Phone + "\"," +
                    //              "\"address\":{" +
                    //              "\"street\":\"" +
                    //              Address.Street + "\",\"city\":\"" +
                    //              Address.City + "\",\"state\":\"" +
                    //              Address.State + "\",\"zipcode\":\"" +
                    //              Address.ZipCode + "\",\"country\":\"" +
                    //              Address.Country + "\"}}}," +
                    //              "\"shipping\":{" +
                    //              "\"customer\":{" +
                    //                    "\"name\":\"" +
                    //                    Customer.Name + "\",\"email\":\"" +
                    //                    Customer.Email + "\",\"phone\":\"" +
                    //                    Customer.Phone + "\"," +
                    //              "\"address\":{" +
                    //              "\"street\":\"" +
                    //              Address.Street + "\",\"city\":\"" +
                    //              Address.City + "\",\"state\":\"" +
                    //              Address.State + "\",\"zipcode\":\"" +
                    //              Address.ZipCode + "\",\"country\":\"" +
                    //              Address.Country +
                    //              "\"}}}}";

                    currency = dicOrderData["currency"];
                    currency = dicOrderData["redirect_url"];
                    currency = dicOrderData["ipn_url"];
                    currency = dicOrderData["reference"];
                    currency = dicProductData["name"];
                    currency = dicProductData["description"];
                    currency = dicCustomerData["name"];
                    currency = dicCustomerData["email"];
                    currency = dicCustomerData["phone"];
                    currency = dicAddressData["street"];
                    currency = dicAddressData["city"];
                    currency = dicAddressData["state"];
                    currency = dicAddressData["zipcode"];
                    currency = dicAddressData["country"];


                    json = "{\"order\":{\"amount\": " +
                        dicOrderData["amount"] + ",\"currency\":\"" +
                        dicOrderData["currency"] + "\",\"redirect_url\":\"" +
                        dicOrderData["redirect_url"] + "\",\"ipn_url\":\"" +
                        dicOrderData["ipn_url"] + "\",\"reference\":\"" +
                        dicOrderData["reference"] + "\",\"validity\":2594000"+
                        "}," +
                         "\"product\":{\"name\":\" " +
                         dicProductData["name"] + "\",\"description\":\"" +
                         dicProductData["description"] + "\"}," +
                         "\"billing\":{" +
                                "\"customer\":{" +
                                        "\"name\":\"" +
                                        dicCustomerData["name"] + "\",\"email\":\"" +
                                        dicCustomerData["email"] + "\",\"phone\":\"" +
                                        dicCustomerData["phone"] + "\"," +
                                  "\"address\":{" +
                                  "\"street\":\"" +
                                  dicAddressData["street"] + "\",\"city\":\"" +
                                  dicAddressData["city"] + "\",\"state\":\"" +
                                  dicAddressData["state"] + "\",\"zipcode\":\"" +
                                  dicAddressData["zipcode"] + "\",\"country\":\"" +
                                  dicAddressData["country"] + "\"}}}," +
                                  "\"shipping\":{" +
                                  "\"customer\":{" +
                                        "\"name\":\"" +
                                        dicCustomerData["name"] + "\",\"email\":\"" +
                                        dicCustomerData["email"] + "\",\"phone\":\"" +
                                        dicCustomerData["phone"] + "\"," +
                                  "\"address\":{" +
                                  "\"street\":\"" +
                                  dicAddressData["street"] + "\",\"city\":\"" +
                                  dicAddressData["city"] + "\",\"state\":\"" +
                                  dicAddressData["state"] + "\",\"zipcode\":\"" +
                                  dicAddressData["zipcode"] + "\",\"country\":\"" +
                                  dicAddressData["country"] + 
                                  "\"}}}}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();


                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString() ;
            }

        }

    }
}
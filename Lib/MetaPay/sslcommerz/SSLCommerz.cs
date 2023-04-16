using System;
using System.IO;
using System.Net;
using System.Text;


namespace MetaPay.SSLCommerz
{
  public  class SSLCommerz
    {


        public void paymentBySSLCommerz()
        {

            string storeId = "test_metamorphosis";
            string storePassword = "test_metamorphosis@ssl";
            decimal amount = Convert.ToDecimal(200);
            string currency = "BDT";
            string transactionId = "5C444RH1KK";
            string successUrl = "http://metamorphosis.com.bd/success.html";
            string failedUrl = "http://metamorphosis.com.bd/fail.html";
            string cancelUrl = "http://metamorphosis.com.bd/cancel.html";
            int emi = 0;

            string cusName = "testCustomer";
            string email = "testCustomer@gmail.com";
            string cust_address = "Dhaka";
            string cusPhone = "+8801737341588";


            //string url = "https://sandbox.sslcommerz.com/gwprocess/v3/api.php?store_id=" + storeId + "&store_passwd=" + storePassword + "" +
            //             "&total_amount=" + amount + "&currency=" + currency + "&tran_id=" + transactionId + "&success_url=" + successUrl +
            //             "&fail_url=" + failedUrl + "&cancel_url=" + cancelUrl + "&emi_option=" + emi + "&cus_name=" + cusName + "&cus_email=" + email +
            //             "&cus_phone=" + cusPhone + "";
            ////string url1 = "https://sandbox.sslcommerz.com/gwprocess/v3/api.php?store_id=metak5a9e38a77feeb&store-password=metak5a9e38a77feeb@ssl&=total_amount=100&currency=BDT&tran_id=test5456E&success_url=http://www.metaposbd.com/web/success.html&fail_url=http://www.metaposbd.com/web/fail.html&cancel_url=http://www.metaposbd.com/web/cancel.html&emi_option=0&&cus_name=testCustomer&cus_email=test@test.com&cus_phone=01737341588";

            //var request = (HttpWebRequest)System.Net.WebRequest.Create(url);
          var request = HttpWebRequest.Create("https://sandbox.sslcommerz.com/gwprocess/v3/api.php?");
            var postData = "store_id='" + storeId + "'&store_passwd='" + storePassword + "'&total_amount='" + amount +
                           "'&currency='" + currency + "'&tran_id='" + transactionId + "'&success_url='" + successUrl + "'&fail_url='" + failedUrl + "'&cancel_url='" + cancelUrl + "'&emi_option='" + emi + "'&cus_name='" + cusName + "'&cus_email='" + email + "'&cus_phone='" + cusPhone + "'";

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //Stream dataStream = request.GetRequestStream();
            //dataStream.Close();
            //WebResponse response = request.GetResponse();
            //dataStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(dataStream);
            //string responseFromServer = reader.ReadToEnd();
           


        }





      public void testPayment()
      {
          string url = "https://sandbox.sslcommerz.com/gwprocess/v3/api.php";
          string data = "store_id=test_metamorphosis&store_passwd=test_metamorphosis@ssl&total_amount=200&currency=BDT&tran_id=REF123&" +
                        "success_url=http://metamorphosis.com.bd/success.html&fail_url=http://metamorphosis.com.bd/fail.html&cancel_url=http://metamorphosis.com.bd/cancel.html&" +
                        "cus_name=testCustomer&cus_email=test@test.com&cus_phone=01737341588";
          WebRequest myRequest = WebRequest.Create(url);
          myRequest.Method = "POST";
          myRequest.ContentLength = data.Length;
          myRequest.ContentType = "application/json; charset=UTF";
          UTF8Encoding enc = new UTF8Encoding();

          Stream ds = myRequest.GetRequestStream();
          ds.Write(enc.GetBytes(data),0,data.Length);

      }

    }
}

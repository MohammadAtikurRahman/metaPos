using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace MetaSMS.Infobip
{
    public class InfobipBalance
    {

        public string getInfobipBalance()
        {
            string url = "https://api.infobip.com/account/1/balance";

            //var base64EncodeText = base64Converter.encodedBase64(model.username + ":" + model.password);

            var req = (HttpWebRequest)WebRequest.Create(url);
            //req.Headers.Add("authorization", "Basic " + base64EncodeText);

            var resp = (HttpWebResponse)req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream());
            string result = sr.ReadToEnd();

            var javaScriptSerializer = new JavaScriptSerializer();
            var infobipBalance = javaScriptSerializer.Deserialize<InfobipBalanceModel>(result);
            double balance = infobipBalance.balance;
            string currency = infobipBalance.currency;

            return currency + " " + balance;
        }

    }
}

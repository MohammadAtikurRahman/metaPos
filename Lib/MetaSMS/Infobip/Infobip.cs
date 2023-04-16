using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace MetaSMS.Infobip
{
    public class Infobip
    {

        public string sendSmsByInfobip(InfobipModel model)
        {
            string url = "https://api.infobip.com/sms/1/text/query?username=" + model.username + "&password=" + model.password + "&from=" + model.senderId + "&to=" + model.phoneNumber + "&text=" + model.message;

            var base64EncodeText = base64Converter.encodedBase64(model.username + ":" + model.password);

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers.Add("authorization", "Basic " + base64EncodeText);

            var resp = (HttpWebResponse)req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream());
            string result = sr.ReadToEnd();

            var javaScriptSerializer = new JavaScriptSerializer();
            var infobipResponse = javaScriptSerializer.Deserialize<InfobipResponseResult>(result);

            string messageId = infobipResponse.messages[0].messageId;
            int statusId = infobipResponse.messages[0].status.id;
            string description = infobipResponse.messages[0].status.description;

            return messageId + ";" + statusId + ";" + description;
        }

    }
}

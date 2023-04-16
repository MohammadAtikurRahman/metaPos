using System.IO;
using System.Net;

namespace MetaSMS.elitbuzz
{
   public class Elitbuzz
   {
       public string sendSMSByElitbuzz(ElitbuzzModel model)
        {
            string url = "http://bangladeshsms.com/smsapi?api_key=" + model.apiKey + "&type=text&contacts=" + model.phoneList + "&senderid=" + model.senderId + "&msg=" + model.message;

            var req = (HttpWebRequest)WebRequest.Create(url);
            var resp = (HttpWebResponse)req.GetResponse();
            var  sr = new StreamReader(resp.GetResponseStream());
            string result = sr.ReadToEnd();

            var output = result;

            if (result == "1002")
            {
                output = "Sender Id is not found!";
            }             
            else if (result == "1003")
            {
                output =  "ApiKey is not found!";
            }
            else if (result == "1004")
            {
                output =  "SPAM Detected";
            }
            else if (result == "1005")
            {
                output =  "Internal Error";
            }
            else if (result == "1006")
            {
                output =  "Internal Error";
            }
            else if (result == "1007")
            {
                output =  "Balance Insufficient!";
            }
            else if (result == "1008")
            {
                output =  "Message is empty";
            }
            else if (result == "1009")
            {
                output =  "Message Type Not Set (text/unicode)";
            }
            else if (result == "1010")
            {
                output =  "Invalid User & Password";
            }
            else if (result == "1011")
            {
                output =  "Invalid User Id";
            }

            return output;
        }

    }
}

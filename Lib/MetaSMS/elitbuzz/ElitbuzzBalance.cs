
using System.IO;
using System.Net;

namespace MetaSMS.elitbuzz
{
   public class ElitbuzzBalance
    {
        public string getSmsBalanceFromElitbuzz(string apiKey)
        {
            string url = "http://bangladeshsms.com/miscapi/"+ apiKey +"/getBalance";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string elitbuzzBalance = sr.ReadToEnd();

            var _result = elitbuzzBalance.Split(':');
            string result = _result[1];
            return result;
        }
    }
}

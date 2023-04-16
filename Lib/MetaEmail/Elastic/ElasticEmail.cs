using System.IO;
using System.Net;
using System.Web.Script.Serialization;



namespace MetaEmail.Elastic
{


    public class ElasticEmail
    {


        public string sendEmailByElasticEmail(ElasticEmailModel elasticModel)
        {
            string url = "https://api.elasticemail.com/v2/email/send?apikey=" + elasticModel.apiKey + "&subject=" +
                         elasticModel.subject + "&from=" + elasticModel.sender + "&to=" + elasticModel.emailList +
                         "&bodyText=" + elasticModel.message;


            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse) req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());

            string result = sr.ReadToEnd();
            var successResult = false;
            var errorResult = "";

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            ElasticEmailModel modelResult = (ElasticEmailModel) javaScriptSerializer.Deserialize(result, typeof(ElasticEmailModel));


            successResult = modelResult.success;
            errorResult = modelResult.error;
            if (successResult == true)
            {
                return "Email send successfuly!";
            }
            else
              return "Email sending failed error is:"+" "+ errorResult;



        }


    }


}
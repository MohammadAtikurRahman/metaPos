using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using MetaPOS.Admin.Model;
using System.Web.Script.Serialization;


namespace MetaPOS.Admin.PromotionBundle.Service
{
    public class FacebookService
    {
        FacebookConfigModel facebookConfigModel = new FacebookConfigModel();

        public string PageId { get; set; }
        public string AccessToken { get; set; }



        public FacebookService()
        {
            var fbData = facebookConfigModel.getFacebookCofigData();

            PageId = fbData.Rows[0]["pageId"].ToString();
            AccessToken = fbData.Rows[0]["accessToken"].ToString();
        }
        public string postInFacebook(string Message)
        {

            string url = "https://graph.facebook.com/" + PageId + "/feed?message=" + Message + "&access_token=" + AccessToken + "";

            //string url = "https://graph.facebook.com/me/photos?message/accessToken";

            var request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            Stream dataStream = request.GetRequestStream();
            dataStream.Close();
            WebResponse response = request.GetResponse();

            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            FacebookServiceModel fbServiceModel = javaScriptSerializer.Deserialize<FacebookServiceModel>(responseFromServer);

            string Id = fbServiceModel.id;

            string[] responseResult = Id.Split('_');

            string responseId = responseResult[0];

            if (responseId == PageId)
            {
                return "Post facebook successfuly";

            }

            else 
            return "Post error!";
        }

    }
}
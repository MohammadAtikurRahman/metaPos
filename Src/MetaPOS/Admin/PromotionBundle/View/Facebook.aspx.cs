using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.UI.WebControls;
using MetaPOS.Admin.PromotionBundle.Service;


namespace MetaPOS.Admin.PromotionBundle.View
{
    public partial class Facebook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //postFb();

            //string url = "https://graph.facebook.com/773791052814253/feed?message=This is second post &access_token=EAACEdEose0cBAH9tFBPiQakPQMEbLEy6uHZCvo6uC1xvEgHjoZAU0nxzqh2kp3dq2fYZBbRk2huTMgA05XxZBI5eYU4K8fCqo77RsJna8OAq6cceRStS3KeK0WdMTQwaz5ZAeDmo4siQdX6sCyOxGGEjZAxpyQ4k47DSCTBXkg94qlRisGS0xIhrkiJrDT0pqr3XRGHTMpGAZDZD";
            //var request = (HttpWebRequest)System.Net.WebRequest.Create(url);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //Stream dataStream = request.GetRequestStream();
            //dataStream.Close();
            //WebResponse response = request.GetResponse();

            //dataStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(dataStream);
            //string responseFromServer = reader.ReadToEnd();



        }





        private FacebookService fbService = new FacebookService();


        protected void btn_PostFacebook(object sender, EventArgs e)
        {
          

            var message = txtPostContent.Value;
            if (message == "")
            {
                lblText.Text = "Post can't be empty!";
                return;
            }
           
           string result = fbService.postInFacebook(message);

            lblText.Text = result;
        }



        
    }

 
}
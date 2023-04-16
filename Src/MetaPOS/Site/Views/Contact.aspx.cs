using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.ApiBundle.Controllers;
using MetaPOS.Admin.Controller;
using MetaPOS.Admin.CustomerBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;


namespace MetaPOS.Site.Views
{



    public partial class Contact : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ContentByEmailAction(string Data)
        {
            var data = (JObject)JsonConvert.DeserializeObject(Data);

            var name = data["name"].Value<string>();
            var mobile = data["mobile"].Value<string>();
            var company = data["company"].Value<string>();
            var subject = data["subject"].Value<string>();
            var question = data["ques"].Value<string>();

            // var logoUrl = HttpUtility.UrlEncode("https://metaposbd.com/Account/Images/logo.png");
            // var logoUrl = "https://metaposbd.com/Account/Images/logo.png";
            //var aTag = "http://www.facebook.com";



            var template = "<html>" +
                           "<head><title>metaPos Inquary</title></head>" +
                           "<body style='background-color: whitesmoke;'>" +
                           "<div style='padding-top:30px;'></div>" +
                               "<div style='background-color: white; width:65%;margin:auto'>" +

                                "<div style='text-align: center; background-color:white;padding:30px 0 20px 0; '><img src='https://metaposbd.com/Account/Images/logo.png' alt=''></div>" +
                                "<div style='padding: 10px 0 10px 0; text-align:center; background-color:#1DC09E;'>" +
                                    "<p style='font-size: 30px; color: white;'>metaPOS Inquary</p> </div>" +

                                        "<div style='text-align: center;padding:15px 0 20px 0;font-family: Helvetica;'>" +
                                            "<i><h3>You have an inquary, plaese check description under bellow</h3></i>" +
                                       "</div>" +
                                       "<div style='font-family:lato'>" +
                                           "<div style='margin:0 45px 17px 45px;font-size: 16px;'><label'><b>Subject:</b></label> " +
                                           subject + "</div>" +
                                           "<div style='margin:0 45px 17px 45px;font-size: 16px;'><label'><b>Name:</b></label>  " +
                                           name + "</div>" +
                                           "<div style='margin:0 45px 17px 45px;font-size: 16px;'><label'><b>Company:</b></label>  " +
                                           company + "</div>" +
                                           "<div style='margin:0 45px 17px 45px;font-size: 16px;'><label'><b>Phone:</b></label>  " +
                                           mobile + "</div>" +
                                           "<div style='margin:0 45px 17px 45px;font-size: 16px;'><label'><b>Details:</b></label>  " +
                                           question + "</div>" +
                                       "</div>" +
                                       "<div style='text-align: center; font-family: Helvetica;margin-top: 30px;'><h3 style='padding-bottom: 30px'><i>Please response as soon as possible</i></h3></div>" +
                                   "</div>" +
                               "<div style='text-align: center; padding-top: 15px;'>" +
                               "<b>Our mailing address</b>" +
                               "<p>402, 24 Avenue 5, Mirpur, Dhaka 1216, Bangaldesh</p>" +
                               "<p style='font-size:12px;'>Copyright © 2019 metaPOS. All Rights Reserved.</p>" +
                               "<p style='font-size:12px;'>Mailing Date: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm") + "</p>" +
                               "</div>" +

                               "<div style='text-align: center; margin-top: 20px;'>" +
                                "<span style='padding-right: 10px;'><a href='https://www.facebook.com/metaposbd/'><img src='https://i.postimg.cc/PqpzNgCp/fb.png' ></a></span>" +
                               "<a href='https://metaposbd.com' ><img src='https://i.postimg.cc/V6z62MPf/web.png' ></a></div>" +
                               "<div style='padding-bottom: 30px;'></div>" +
                           "</body>" +
                           "</html>";
            StringBuilder sb = new StringBuilder();
            SendController sendController = new SendController();
            return sendController.Post("shofikcl26@gmail.com", "khasancsit@gmail.com", "metaPOS Inquiry", template, "1200", sb);

        }



    }
}
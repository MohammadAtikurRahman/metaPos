using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaEmail.Elastic;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.PromotionBundle.Service
{
    public class EmailService
    {
        public string medium { get; set; }
        public string sender { get; set; }
        public string apiKey { get; set; }
        public decimal cost { get; set; }

        public EmailService()
        {

            EmailConfigModel emailConfigModel = new EmailConfigModel();

            var dataList = emailConfigModel.getSmsCofigData();
            if (dataList.Rows.Count > 0)
            {
                medium = dataList.Rows[0]["medium"].ToString();
                sender = dataList.Rows[0]["sender"].ToString();
                apiKey = dataList.Rows[0]["apiKey"].ToString();
                cost = Convert.ToDecimal(dataList.Rows[0]["cost"]);
            }
           


        }

        public string sendEmailService(string subject,string message,string emailList)
        {
            ElasticEmailModel elasticEmailModel = new ElasticEmailModel();
            elasticEmailModel.sender = sender;
            elasticEmailModel.apiKey = apiKey;
            elasticEmailModel.subject = subject;
            elasticEmailModel.message = message;
            elasticEmailModel.emailList = emailList;
            ElasticEmail elasticEmail = new ElasticEmail();
            string result = elasticEmail.sendEmailByElasticEmail(elasticEmailModel);
            //return "Email send successfuly.";
            if (result == "Email send successfuly!")
            {
                EmailLogModel emailLog = new EmailLogModel();
                emailLog.message = message;
                emailLog.emailRecord = emailList;
                emailLog.medium = medium;
                emailLog.emailCost = cost;
                emailLog.sentAt = DateTime.Now;
                emailLog.roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"]);
                emailLog.active = "1";
                emailLog.saveEmailLogInfoModel();
            }
            
           
            return result;
        }
    }
}
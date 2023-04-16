using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MetaPOS.Admin.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;


namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class SendController : ApiController
    {
        //app.metaposbd.com/api/sends/mail?to=khasancsit@gmail.com&cc=kamrulbd02@gmail.com&sub=Test metapos&msg=This is test message using api&key=1200

        // GET api/<controller>
        [Route("api/sends/mail")]
        [HttpPost]
        public string Post(string to, string cc, string sub, string msg, string key, StringBuilder stringBuilder)
        {

            if (key != "1200")
            {
                return "Key not match";
            }


            var sendMsg = "";
            var mailMessage = new MailMessage();

            // int subject here support@robiamarhishab.com 
            mailMessage.From = new MailAddress("contact@mail.robiamarhishab.xyz", "Robi Amar Hishab");
            // split reciver mail

            mailMessage.To.Add(new MailAddress(to));

            if (cc != "" && cc != null)
            {
                string[] splitCc = cc.Split(',');

                for (int i = 0; i < splitCc.Length; i++)
                {
                    mailMessage.CC.Add(new MailAddress(splitCc[i]));
                }
            }

            // Attachment
            try
            {
                if (stringBuilder.Length > 0)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {

                        StringReader sr = new StringReader(stringBuilder.ToString());

                        Document pdfDoc = new Document(PageSize.A4, 15f, 15f, 15f, 15f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        var commonFunction = new CommonFunction();
                        mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), "robi_amarhisab_Subscription_" + commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy") + ".pdf"));

                    }
                }
            }
            catch (Exception)
            {

            }

            //public void Send(string from, string recipients, string subject, string body);
            // message body
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = sub;
            mailMessage.Body = msg;

            //"mail.robiamarhishab.com", 25
            SmtpClient smt = new SmtpClient("jupiter.exonhost.com", 465);

            var NetworkCred = new NetworkCredential("contact@mail.robiamarhishab.xyz", "Uey392p#1");
            smt.Credentials = NetworkCred;
            smt.DeliveryMethod = SmtpDeliveryMethod.Network;
            smt.EnableSsl = true;

            try
            {
                smt.Send(mailMessage);
                sendMsg = "Success";
            }
            catch (Exception ex)
            {
                sendMsg = ex.Message;
            }

            return sendMsg;
        }


    }
}
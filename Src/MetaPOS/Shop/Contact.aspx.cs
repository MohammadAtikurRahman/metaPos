using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web.UI;


namespace MetaPOS.Shop
{


    public partial class Contact : Page
    {


        private Model.Shop objWebModel = new Model.Shop();
        private DataSet ds;
        private string googleMap = "", iframLink = "";


        public enum MessageType
        {


            Success,
            Error,
            Info,
            Warning


        };





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getShopInfo();
            }
        }





        public void getShopInfo()
        {
            ds = objWebModel.getWeb();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblPhone.Text = ds.Tables[0].Rows[0][4].ToString();
                lblShopAddress.Text = ds.Tables[0].Rows[0][5].ToString();
                lblEmailAddress.Text = ds.Tables[0].Rows[0][6].ToString();
                iframLink = ds.Tables[0].Rows[0][11].ToString();
                if (iframLink != "")
                {
                    googleMap = "<iframe src=" + iframLink +
                                " width='600' height='450' frameborder='0' style='border:0' allowfullscreen></iframe>";
                    plhGoogleMapIfram.Controls.Add(new LiteralControl(googleMap));
                }
            }
        }





        public void ScriptMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + message + "','" + type + "');", true);
        }





        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                lblMessage.Text = "Type email id first!";
                //ScriptMessage("Type email id first!", MessageType.Error);
                return;
            }

            if (lblEmailAddress.Text == "")
            {
                lblMessage.Text = "Email not send !! Not set email yet..";
                //ScriptMessage("Email not send !! Not set email yet..", MessageType.Error);
                return;
            }

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("support@metaposbd.com");
            mailMessage.To.Add(new MailAddress(lblEmailAddress.Text));

            string emailBody = "Hello, <br> You have recived a new mail form <b>"
                               + txtName.Text + " </b>."
                               + " <br> <b>Email: </b>" + txtEmail.Text
                               + " <br> <b>Message: </b>" + txtMessage.Text
                               + "<br/><br/><br/>"
                               + "Sincerely,<br/>"
                               + "metaPOS Team<br/>"
                               + "www.metaposbd.com<br/>";
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Login Credentials - metaPOS";
            mailMessage.Body = emailBody;

            var smt = new SmtpClient("mail.metaposbd.com");
            var NetworkCred = new NetworkCredential("support@metaposbd.com", "rtj#s()k8d#k");
            smt.Credentials = NetworkCred;


            try
            {
                smt.Send(mailMessage);
                lblMessage.Text = "Message Send !!";
                //ScriptMessage("Email Send Successfully.", MessageType.Success);
            }
            catch (Exception)
            {
                //ScriptMessage("Email not send!", MessageType.Error);
                lblMessage.Text = "Message not send";
            }
        }


    }


}
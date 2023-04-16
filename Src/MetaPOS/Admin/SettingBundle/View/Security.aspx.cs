using System;
using System.Web.UI;
using System.Data;
using System.Net;
using System.Net.Mail;


namespace MetaPOS.Admin.SettingBundle.View
{


    public partial class Security :BasePage
    {


        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();
        private DataSet ds;
        private string query = "";
        private static string password = "";
        private bool IsUpdated = false;




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Security"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
                searchUserProfile();

               
                
            }

            if (Request["secure"] == "false")
            {
                divSecureAccount.Visible = true;
            }
        }





        public void scriptMessage(string msg)
        {
            string title = "Notification Area";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), title, "alert('" + msg + "');", true);
        }





        private void searchUserProfile()
        {
            try
            {
                query = "SELECT title,email,password FROM [RoleInfo] WHERE roleID = '" + Session["roleID"] + "' ";
                ds = objSql.getDataSet(query);
                txtUserName.Text = ds.Tables[0].Rows[0][0].ToString();
                txtUserEmail.Text = ds.Tables[0].Rows[0][1].ToString();
                password = objCommonFun.Decrypt(ds.Tables[0].Rows[0][2].ToString());
            }
            catch
            {
            }
        }





        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                query = "UPDATE [RoleInfo] SET title = '" + txtUserName.Text + "', email = '" + txtUserEmail.Text +
                        "' WHERE roleID = '" + Session["roleID"] + "' ";
                scriptMessage(objSql.executeQuery(query));
            }
            catch
            {
            }
        }





        protected void btnUpdatePrivacy_Click(object sender, EventArgs e)
        {
            if (txtCurrent.Text == "" || txtNew.Text == "" || txtConfirm.Text == "")
            {
                scriptMessage("Password field cant be empty!");
                return;
            }

            if (password != txtCurrent.Text)
            {
                scriptMessage("Current password is not matched!");
                return;
            }

            if (txtNew.Text != txtConfirm.Text)
            {
                scriptMessage("New & Confirm password is not matched!");
                return;
            }

            try
            {
                string encryptPassword = objCommonFun.Encrypt(txtNew.Text);
                query = "UPDATE [RoleInfo] SET password='" + encryptPassword + "', isSecureAccount = '1' WHERE roleID = '" + Session["roleID"] +
                        "' ";
                scriptMessage(objSql.executeQuery(query));
                IsUpdated = true;
            }
            catch
            {
            }

            if (Request["secure"] == "false" && IsUpdated)
            {
                SendMailActiveUser();
            }
        }





        public void SendMailActiveUser()
        {
            //var mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("shofikcl26@gmail.com");
            //mailMessage.To.Add(new MailAddress("shofikahmed72@gamil.com"));

            //string emailBody = "New client use their software.the client is" + txtUserEmail.Text;
            //mailMessage.IsBodyHtml = true;
            //mailMessage.Subject = "Robi Amar Hishab User";
            //mailMessage.Body = emailBody;

            //var smt = new SmtpClient("mail.metaposbd.com");
            //var ntwordCred = new NetworkCredential("support@metaposbd.com", "rtj#s()k8d#k");
            //smt.Credentials = ntwordCred;

            //try
            //{
            //    smt.Send(mailMessage);
            //}
            //catch
            //{
            //}


            string to = "shofikahmed72@gmail.com";
            string from = "shofikcl26@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Robi Amar Hishab User";
            message.Body = @"New client software use sale from now.The client is:" + txtUserEmail.Text;

            var smt = new SmtpClient("jupiter.exonhost.com", 465);
            var ntwordCred = new NetworkCredential("support@metaposbd.com", "rtj#s()k8d#k");
            smt.Credentials = ntwordCred;
            smt.UseDefaultCredentials = true;

            try
            {
                smt.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }



        }


    }


}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Account.Helper;
using MetaPOS.Account.Service;
using MetaPOS.Admin.Controller;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.ApiBundle.Controllers;


namespace MetaPOS.Account.View
{
    public partial class Login : System.Web.UI.Page
    {
        //private DataSet ds;

        private CommonController objCommonController = new CommonController();
        private CommonFunction commonFunction = new CommonFunction();
        private Initialize initialize = new Initialize();
        private Cryptography cryptography = new Cryptography();


        protected void Page_Load(object sender, EventArgs e)
        {
            commonFunction.isLoggedIn();
            initialize.SessionInitlize();

            if (!IsPostBack)
            {
                // Get pure domain url
                string url = objCommonController.getDomainPartOnly();

                if (url == "www.metaposbd.com" || url == "metaposbd.com" || url == "www.app.metaposbd.com" || url == "app.metaposbd.com")
                {
                    txtEmail.Text = "demo@metaposbd.com";
                    txtPassword.Attributes["type"] = "password";
                    txtPassword.Text = "78910";
                }
            }

            if (Request["mailSuccess"] == "1")
            {
                ScriptMessage("Password sent to your mail successfully.", MessageType.Success);
            }
            else if (Request["mailSuccess"] == "0")
            {
                ScriptMessage("Your mail could not be sent! Please try again.", MessageType.Error);
            }

        }







        protected void btnLogIn_OnClick(object sender, EventArgs e)
        {
            var isValidLink = true;
            var subdomain = commonFunction.GetSubDomain();
            var userDomain = commonFunction.getUserDomain();

            if (!commonFunction.isSaaS())
            {
                //
                Session["conString"] = subdomain;
                userDomain = subdomain;
            }
            else
            {
                //if (userDomain == "")
                //    isValidLink = false;
                userDomain = subdomain;

                if (commonFunction.CheckConnectionString(userDomain))
                {
                    Session["conString"] = userDomain;
                }
                else
                {
                    isValidLink = false;
                }
            }

            if (!isValidLink)
            {
                if (commonFunction.isSaaS())
                    lblTest.Text = "SaaS mode is active";
                ScriptMessage("Sorry, the link you followed may be broken, or the link may have been removed.",
                       MessageType.Warning);
                return;
            }

            GlobalVariable.connectionString = userDomain;
            var sqlOperation = new SqlOperation();
            sqlOperation.updateConnectionString();


            var dtRoleAcess = sqlOperation.getDataTable("SELECT * FROM [RoleInfo] WHERE email = '" + txtEmail.Text.Trim() + "' AND password='" + cryptography.Encrypt(txtPassword.Text) + "'");

            //Last login date insert here sqlOperation.fireQuery

            //string email = txtEmail.Text.Trim();

            //var dtRole = sqlOperation.getDataTable("SELECT * FROM [RoleInfo] WHERE email = '" + email + "'");
            //if (dtRole.Rows.Count <= 0)
            //    return;
            


           
            if (dtRoleAcess.Rows.Count != 0)
            {


                LoginService loginService = new LoginService();
                var userRight = dtRoleAcess.Rows[0]["userRight"].ToString();
                if (!(userRight == "Group" || userRight == "Super"))
                {
                    loginService.userRight = userRight;
                    loginService.roleId = Convert.ToInt32(dtRoleAcess.Rows[0]["roleID"].ToString());
                    loginService.email = dtRoleAcess.Rows[0]["email"].ToString();
                    loginService.branchId = Convert.ToInt32(dtRoleAcess.Rows[0]["branchId"].ToString());

                    if (userRight == "Branch" || userRight == "Regular")
                    {
                        loginService.SaveUserLogsData();
                    }

                }




                if (dtRoleAcess.Rows[0]["active"].ToString() == "False")
                {
                    ScriptMessage("Your account is deactivate now! Please contact your administrator.", MessageType.Warning);
                    return;
                }

                if (!dtRoleAcess.Columns.Contains("storeId"))
                {
                    ScriptMessage("Your Store is not ready! Please contact your administrator.", MessageType.Warning);
                    return;
                }

                checkDirectories();
                commonFunction.LoginExecutable(dtRoleAcess, userDomain);
                commonFunction.isLoggedIn();
            }
            else
            {
                ScriptMessage("Sorry, you entered an incorrect email address or password.", MessageType.Error);
            }
        }       



        protected void btnSentMailToRecover_OnClick(object sender, EventArgs e)
        {
            if (txtEmailToRecover.Text == "")
            {
                ScriptMessage("Email address is required", MessageType.Error);
                return;
            }

            string decryptedPassword = "", clientName = "";
            var sqlOperation = new SqlOperation();

            var domainName = commonFunction.GetSubDomain();
            GlobalVariable.connectionString = domainName;
            Session["conString"] = domainName;


            var dtRole = sqlOperation.getDataTable("SELECT * FROM [RoleInfo] WHERE email = '" + txtEmailToRecover.Text.Trim() + "'");

            if (dtRole.Rows.Count > 0)
            {
                if (dtRole.Rows[0]["active"].ToString() == "False")
                {
                    ScriptMessage(
                        "Your account is deactivate! Please contact your support team.",
                        MessageType.Warning);
                    return;
                }

                decryptedPassword = cryptography.Decrypt(dtRole.Rows[0]["password"].ToString());
                clientName = dtRole.Rows[0]["title"].ToString();
            }
            else
            {
                ScriptMessage("Email address is not found!", MessageType.Error);
                return;
            }

            lblWaiting.Text = "Wait Please...";

            string message = "Dear <b>" + clientName + ",</b><br/>"
                            + "This email is generated from Forgot Password link of Robi Amarhishab. If you didn't ask for password please ignore this email. <br/>"
                            + "Your current password is: <b>" + decryptedPassword + "</b><br/>"
                            + "<br/>"
                            + "Sincerely,<br/>"
                            + "metaPOS Team<br/>"
                            + "www.metaposbd.com<br/>";

            lblWaiting.Text = "";

            var to = txtEmailToRecover.Text;
            var cc = "";
            var subject = "Robi Amarhishab Access Credentials";

            try
            {
                string url = "http://app.metaposbd.com/api/sends/mail?to=" + to + "&cc=" + cc + "&sub=" + subject + "&msg=" + message + "&key=1200";
                var isSend = commonFunction.Sendmail(url);
                ScriptMessage("Recovery Email send successfully. Please check your email", MessageType.Success);
                //Response.Redirect("~/Login/Default.aspx?mailSuccess=1");
            }
            catch (Exception)
            {
                ScriptMessage("Email do not send, please contact with support team.", MessageType.Error);

                //Response.Redirect("~/Login/Default.aspx?mailSuccess=2");
            }
        }



        public void checkDirectories()
        {
            string folderPath = Server.MapPath("~/Admin/BarcodeTool/images/");
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            folderPath = Server.MapPath("~/Img/Product/");
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            folderPath = Server.MapPath("~/Img/Logo/");
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            folderPath = Server.MapPath("~/Img/Slider/");
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            folderPath = Server.MapPath("~/Files/");
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

        }


        public void ScriptMessage(string message, MessageType type)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showNotificationToast('" + message + "','" + type + "')", true);
        }

    }

}
using System;
using System.Web.UI;
using MetaPOS.Admin.DataAccess;

namespace MetaPOS.Admin.SMSBundle.View
{


    
    public partial class SMSConfig : BasePage//Page
    {

        CommonFunction commonFunction = new CommonFunction();
       private SqlOperation sqlOperation = new SqlOperation();
        private string smsConfigBranchId = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(!commonFunction.accessChecker("SmsConfig"))
                {
                    commonFunction.pageout();
                }



                if (Session["userRight"].ToString() == "Branch")
                {
                    smsConfigBranchId = Session["roleId"].ToString();
                }
                else if (Session["userRight"].ToString() == "Regular")
                {
                    smsConfigBranchId = Session["branchId"].ToString();
                }
                lblHiddenSMSConfig.Value = smsConfigBranchId;

                loadSmsConfigData(smsConfigBranchId);

            }

        }



        private void loadSmsConfigData(string id)
        {
            var dtSmsConfig = sqlOperation.getDataTable("SELECT * FROM SmsConfigInfo WHERE roleId ='" + id + "' ");

            if (dtSmsConfig.Rows.Count <= 0)
                return;


            txtUsername.Text = dtSmsConfig.Rows[0]["username"].ToString();
            txtPassword.Text = dtSmsConfig.Rows[0]["password"].ToString();
            txtApiKey.Text = dtSmsConfig.Rows[0]["apiKey"].ToString();
            txtSenderKey.Text = dtSmsConfig.Rows[0]["senderKey"].ToString();
        }

  
    }
}
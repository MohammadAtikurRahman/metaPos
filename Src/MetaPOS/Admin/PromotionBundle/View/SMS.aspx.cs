using System;
using System.Linq;
using System.Text.RegularExpressions;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.PromotionBundle.Service;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Management;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.PromotionBundle.View
{


    public partial class SMS : System.Web.UI.Page
    {


        private SmsService smsService = new SmsService();

        private SmsServiceModel smsServiceModel = new SmsServiceModel();

        private SqlOperation objSql = new SqlOperation();
        private CommonFunction objCommonFun = new CommonFunction();
        private string query = "";
        //private DataSet ds;

        //private string port;
        //private Int32 baudRate;
       // private bool testConnection = false;
       // private ManagementObjectSearcher moSearcher = null;


        public enum MessageType
        {


            Success,
            Error,
            Info,
            Warning


        };





        protected void Page_Load(object sender, EventArgs e)
        {

            getSmsBalance();
            if (!IsPostBack)
            {

                if (!objCommonFun.accessChecker("SMS"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
            }
            
            getMsgCost.Value = "0.3";
            searchResult();

            var id = (Request["id"]);

            if (id != null)
            {
                loadSmsdata(id);
            }
        }





       

        public void loadSmsdata(string id)
        {
            // Get data by id
            string message = "";
            string phoneNumbers = "";
            
            //string labelPhone;
            string number = "";
         
            var smsData = smsService.getReSendSms(id);

            string[] splitSmsData = smsData.Split(';');

            message = splitSmsData[0];

            phoneNumbers = splitSmsData[1];


            var splitPhoneNumber = phoneNumbers.Split(',');

            for (int i = 0; i < splitPhoneNumber.Length; i++)
            {
                number = splitPhoneNumber[i];

                lblTest.Text = number;

                foreach (GridViewRow gvRow in gvContactList.Rows)
                {
                    var result = ((Label)gvRow.FindControl("lblPhone")).Text;

                    lblTest.Text = result.ToString();

                    if (result == number)
                    {
                        CheckBox myCheckBox = (CheckBox)gvRow.FindControl("chkSelect");
                        myCheckBox.Checked = true;
                    }
                }

            }



            // Load message 

            txtSMSText.Text = message;

        }


        string phoneList = "";
        string message = "";
        protected void btnSend_Click(object sender, EventArgs e)
        {
            
            double smsCost = Convert.ToDouble(getMsgCost.Value);
             phoneList = "";
            int i = 0;

            foreach (GridViewRow gvrow in gvContactList.Rows)
            {
                bool result = ((CheckBox)gvrow.FindControl("chkSelect")).Checked;

                if (result)
                {
                    Label phone = gvrow.FindControl("lblPhone") as Label;
                    phoneList += "+88" + phone.Text + ",";
                    i++;
                }

            }

            if (phoneList.Count() == 0)
            {
                lblMessage.Text = "Please select minimum one number";
                return;
            }
            phoneList = phoneList.Remove(phoneList.Length - 1);

             message = txtSMSText.Text;

            if (message == "")
            {
                lblMessage.Text = "Message can't empty";
                return;
            }

            if (checkSmsSendingCost() == false)
            {
                lblMessage.Text = "Message cant be sent.Message cost exceeded from account balance.";
                return;
            }
           
            
            try
            {

                // set medium data
                SmsConfigModel smsConfigModel = new SmsConfigModel();
                var dataList = smsConfigModel.getSmsConfigDataByBranchId();
                if (dataList.Rows.Count > 0)
                {
                    smsService.medium = dataList.Rows[0]["medium"].ToString();
                    smsService.apiKey = dataList.Rows[0]["apiKey"].ToString();
                    smsService.senderId = dataList.Rows[0]["senderId"].ToString();
                    smsService.username = dataList.Rows[0]["username"].ToString();
                    smsService.password = dataList.Rows[0]["password"].ToString();
                }

                smsServiceModel.phoneList = phoneList;
                smsServiceModel.message = message;
                string resultSms = smsService.sendSmsService(phoneList, message, smsCost, messageCount,"");
                lblMessage.Text = resultSms;

                updateBalance(message);

                getSmsBalance();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }



        private void updateBalance(string messageText)
        {

            double messageLength = messageText.Length;
            double doubleMessageCount;
            double perSmsCost = Convert.ToDouble(getMsgCost.Value);
            int contactCount = phoneList.Count() / 14;

            var regex = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\s\\/]+$";
            var match = Regex.Match(message, regex);
            if (match.Success)
            {

                doubleMessageCount = (messageLength / 160);
                messageCount = (int)Math.Ceiling(doubleMessageCount);

            }
            else
            {
                doubleMessageCount = (messageLength / 70);
                messageCount = (int)Math.Ceiling(doubleMessageCount);
            }

            var totalSmsCost = (messageCount * perSmsCost * contactCount);

            smsService.updateBalance(Convert.ToDecimal(totalSmsCost));
        }


        // Sms Balance

       
      
        private void reloadPage()
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            getSmsBalance();

        }
        string getSmsBalanceByApi;
        protected void getSmsBalance()
        {
            getSmsBalanceByApi = smsService.getSmsBalance();
            showBalance.Text = "Account Balance :" + " " + getSmsBalanceByApi;
        }






        // Sms Cost 
        int messageCount;
        protected bool checkSmsSendingCost()
        {
            
            
            double messageLength = message.Length;
            double doubleMessageCount;
            double perSmsCost = Convert.ToDouble(getMsgCost.Value);
            int contactCount = phoneList.Count() / 14;

            var regex = @"^[a-zA-Z0-9@#$%&*+\-_(),+':;?.,!\[\]\s\\/]+$";
            var match = Regex.Match(message, regex);
            if (match.Success)
            {

                doubleMessageCount = (messageLength / 160);
                messageCount = (int)Math.Ceiling(doubleMessageCount);

            }
            else
            {
                doubleMessageCount = (messageLength / 70);
                messageCount = (int)Math.Ceiling(doubleMessageCount);
            }

            var totalSmsCost = (messageCount * perSmsCost * contactCount);
            bool messageSend = true;

            // cheeck balance
            var balance = smsService.getCustomSmsBalance();
            if (Convert.ToDecimal(totalSmsCost) > balance)
            {
                return messageSend = false;
            }
            else

                return messageSend;
        }




        private void refreshGrd(string query)
        {
            SqlDataSource dsGrdStockStatus = new SqlDataSource();
            dsGrdStockStatus.ID = "dsContactList";
            this.Page.Controls.Add(dsGrdStockStatus);
            var constr = GlobalVariable.getConnectionStringName();
            dsGrdStockStatus.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsGrdStockStatus.SelectCommand = query;
            gvContactList.PageIndex = 0;
            gvContactList.DataSource = dsGrdStockStatus;
            gvContactList.DataBind();

        }





        private void searchResult()
        {

            if (ddlSearch.SelectedValue == "")
            {
                query =
                    "SELECT * FROM (SELECT [Id], [name], [phone], [entryDate] FROM [CustomerInfo] WHERE (name LIKE '%" +
                    txtSearch.Text + "%' OR phone LIKE '%" + txtSearch.Text + "%') AND phone !='' " +
                    "UNION ALL " +
                    "SELECT [Id], [name], [phone], [entryDate] FROM [StaffInfo] WHERE (name LIKE '%" + txtSearch.Text +
                    "%' OR phone LIKE '%" + txtSearch.Text + "%') AND phone !='' " +
                    "UNION ALL " +
                    "SELECT Id, supCompany AS name, supPhone AS phone, entryDate FROM [SupplierInfo] WHERE (supCompany LIKE '%" +
                    txtSearch.Text + "%' OR supPhone LIKE '%" + txtSearch.Text +
                    "%') AND supPhone != '' ) dum ORDER BY Id DESC ";
            }

            else if (ddlSearch.SelectedValue == "0")
            {
                query = "SELECT Id, name, phone, entryDate FROM [CustomerInfo]  WHERE (name LIKE '%" + txtSearch.Text +
                        "%' OR phone LIKE '%" + txtSearch.Text + "%') AND phone != '' ORDER BY Id DESC ";
            }
            else if (ddlSearch.SelectedValue == "1")
            {
                query = "SELECT [Id], [name], [phone], [entryDate] FROM [StaffInfo]  WHERE (name LIKE '%" +
                        txtSearch.Text + "%' OR phone LIKE '%" + txtSearch.Text +
                        "%') AND phone != '' ORDER BY Id DESC ";
            }
            else if (ddlSearch.SelectedValue == "2")
            {
                query =
                    "SELECT Id, supCompany AS name, supPhone AS phone, entryDate FROM [SupplierInfo]  WHERE (supCompany LIKE '%" +
                    txtSearch.Text + "%' OR supPhone LIKE '%" + txtSearch.Text +
                    "%') AND supPhone != '' ORDER BY Id DESC ";
            }


            refreshGrd(query);
        }





        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        protected void gvContactList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            saveCheckedValues();
            gvContactList.PageIndex = e.NewPageIndex;
            checkSavedValues();

        }





        private void saveCheckedValues()
        {
            ArrayList usercontent = new ArrayList();
            int index = -1;

            foreach (GridViewRow gvrow in gvContactList.Rows)
            {
                index = Convert.ToInt32(gvContactList.DataKeys[gvrow.RowIndex].Value);
                bool result = ((CheckBox)gvrow.FindControl("chkSelect")).Checked;

                // Check in the Session

                if (Session["chkditems"] != null)
                    usercontent = (ArrayList)Session["chkditems"];

                if (result)
                {
                    if (!usercontent.Contains(index))
                        usercontent.Add(index);
                }
                else
                    usercontent.Remove(index);
            }

            if (usercontent != null && usercontent.Count > 0)
            {
                Session["chkditems"] = usercontent;
            }
        }





        private void checkSavedValues()
        {
            ArrayList usercontent = (ArrayList)Session["chkditems"];



            if (usercontent != null && usercontent.Count > 0)
            {
                foreach (GridViewRow gvrow in gvContactList.Rows)
                {
                    int index = Convert.ToInt32(gvContactList.DataKeys[gvrow.RowIndex].Value);

                    if (usercontent.Contains(index))
                    {
                        lblTest.Text += index.ToString() + "; ";

                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkSelect");

                        myCheckBox.Checked = true;
                    }
                }
            }
        }





        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void ddlMediumName_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMediumDevices();
           

        }





        private void loadMediumDevices()
        {

            //if (IntPtr.Size == 4)
            //{
            //    moSearcher =
            //        new ManagementObjectSearcher("SELECT * FROM Win32_POTSModem WHERE ConfigManagerErrorCode = 0");
            //}

            //else if (IntPtr.Size == 8)
            //{
            //    moSearcher =
            //        new ManagementObjectSearcher("SELECT * FROM Win64_POTSModem WHERE ConfigManagerErrorCode = 0");
            //}

            //foreach (ManagementObject devices in moSearcher.Get())
            //{
            //    ddlMediumName.Items.Add(devices["Caption"].ToString());
            //}
        }


    }


}
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Management;
using MetaPOS.Admin.DataAccess;

namespace MetaPOS.Admin.PromotionBundle.View
{
    public partial class Email : System.Web.UI.Page
    {




        private PromotionBundle.Service.EmailService emailService = new PromotionBundle.Service.EmailService();


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction objCommonFun = new CommonFunction();
        private string query = "";
        //private DataSet ds;

        //private string port;
        //private Int32 baudRate;
        //private bool testConnection = false;
        //private ManagementObjectSearcher moSearcher = null;


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
                if (!objCommonFun.accessChecker("Email"))
                {
                    //var obj = new DataAccess.CommonFunction();
                    //obj.pageout();
                }
            }

            searchResult();
        }





        protected void btnSend_Click(object sender, EventArgs e)
        {
            string emailList = "";
            int i = 0;

            foreach (GridViewRow gvrow in gvContactList.Rows)
            {
                bool result = ((CheckBox)gvrow.FindControl("chkSelect")).Checked;

                if (result)
                {
                    Label email = gvrow.FindControl("lblEmail") as Label;
                    emailList += email.Text + ";";
                    i++;
                }
            }

            if (emailList.Length == 0)
            {
                lblMessage.Text = "Please provide at least one email address";
                return;
            }

            emailList = emailList.Remove(emailList.Length - 1);

            string subject = emailSubject.Text;
            string message = txtEmailText.Text;


            try
            {

                string responseResult = emailService.sendEmailService(subject, message, emailList);
                lblMessage.Text = responseResult;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }





        private void reloadPage()
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }





        private void refreshGrd(string query)
        {
            SqlDataSource dsGrdStockStatus = new SqlDataSource();
            dsGrdStockStatus.ID = "dsGrdEmail";
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
                    "SELECT * FROM (SELECT [Id], [name], [mailInfo] AS email, [entryDate] FROM [CustomerInfo] WHERE (name LIKE '%" +
                    txtSearch.Text + "%' OR mailInfo LIKE '%" + txtSearch.Text + "%') AND mailInfo !='' " +
                    "UNION ALL " +

                    "SELECT Id, supCompany AS name, mailInfo AS email, entryDate FROM [SupplierInfo] WHERE (supCompany LIKE '%" +
                    txtSearch.Text + "%' OR mailInfo LIKE '%" + txtSearch.Text +
                    "%') AND mailInfo != '' ) dum ORDER BY Id DESC ";
            }

            else if (ddlSearch.SelectedValue == "0")
            {
                query = "SELECT Id, name, mailInfo AS email, entryDate FROM [CustomerInfo]  WHERE (name LIKE '%" + txtSearch.Text +
                        "%' OR email LIKE '%" + txtSearch.Text + "%') AND mailInfo != '' ORDER BY Id DESC ";
            }

            else if (ddlSearch.SelectedValue == "1")
            {
                query =
                    "SELECT Id, supCompany AS name, mailInfo AS email, entryDate FROM [SupplierInfo]  WHERE (supCompany LIKE '%" +
                    txtSearch.Text + "%' OR mailInfo LIKE '%" + txtSearch.Text +
                    "%') AND mailInfo != '' ORDER BY Id DESC ";
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
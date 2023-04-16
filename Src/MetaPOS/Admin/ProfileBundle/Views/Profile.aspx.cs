using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.ProfileBundle.Service;
using System.Data;


namespace MetaPOS.Admin.ProfileBundle.Views
{
    public partial class Profile : BasePage
    {
        private  CommonFunction commonFunction = new CommonFunction();
        private SqlOperation sqlOperation = new SqlOperation();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Profile"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }

                lblStoreId.Text = Session["storeId"].ToString();
                searchBranchProfile(lblStoreId.Text);
            }
        }

        public void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }


        private void reloadPage()
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }




        [WebMethod]
        public static string SaveProfileDataAction(string jsonStrData)
        {
            var profileService = new ProfileService();
            return profileService.SaveProfileData(jsonStrData);

        }



        [WebMethod]
        public static List<ListItem> getStoreListDataAction()
        {
            var profileService = new ProfileService();
            return profileService.getStoreListData();

        }

        [WebMethod]
        public static string loadProfileDataAction(string storeId)
        {
            var profileService = new ProfileService();
            return profileService.loadProfileData(storeId);

        }



        protected void btnUpdateImage_Click(object sender, EventArgs e)//here
        {
           
            var folderPath = Server.MapPath("~/Img/Logo/");
            var fileName = "";
            if (fulogo.HasFile)
            {
                fileName = lblStoreId.Text + Path.GetExtension(fulogo.PostedFile.FileName);
                string fileExtwnsion = Path.GetExtension(fulogo.FileName);
                if (fileExtwnsion.ToLower() != ".jpg" && fileExtwnsion.ToLower() != ".png")
                {
                    lblMessage.Text = "Only jpg and png file allowed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    int fileSize = fulogo.PostedFile.ContentLength;
                    if (fileSize > 2097152)
                    {
                        lblMessage.Text = "Maximum size 2(MB) exceeded ";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblMessage.Text = "Logo Uploaded successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;

                        if ((File.Exists(folderPath + fileName)))
                         File.Delete(folderPath + fileName);

                        fulogo.SaveAs(folderPath + Path.GetFileName(fileName));

                        string query = "UPDATE [BranchInfo] SET  branchLogoPath = '" + fileName + "' WHERE storeId = '" + lblStoreId.Text + "'";
                        scriptMessage(sqlOperation.executeQuery(query), MessageType.Success);

                        RefreshImage();
                        reloadPage();
                    }
                }

            }
            else
            {
                lblMessage.Text = "File not uploaded";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            
        }





        public void RefreshImage()
        {
            var  ds = sqlOperation.getDataSet("SELECT * FROM BranchInfo WHERE storeId = '" + lblStoreId.Text + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                string imgName = ds.Tables[0].Rows[0][10].ToString();
                string ImgPath = "~/Img/Logo/" + imgName;

                if (imgName == "")
                {
                    ImgPath = "~/Img/Logo-100x100.png";
                }

                imgLogo.ImageUrl = ImgPath;
            }
        }
       




        protected void btnSaveSmsTemplate_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInvoiceSmsTemplate.Text))
            {
                var query = "Update branchInfo SET invoiceSmsTemplate = '" + txtInvoiceSmsTemplate.Text +
                            "' Where storeId ='" + lblStoreId.Text + "'";
                sqlOperation.executeQuery(query);
            }

            string selectQuery = "SELECT * FROM [BranchInfo] WHERE storeId = '" + lblStoreId.Text + "'";
            DataTable dtBranch = sqlOperation.getDataTable(selectQuery);
            if (dtBranch.Rows.Count == 0)
            {
                commonFunction.createBranchInfo(true, lblStoreId.Text);

                // Again select after insert
                dtBranch = sqlOperation.getDataTable(selectQuery);
            }
            txtInvoiceSmsTemplate.Text = dtBranch.Rows[0]["invoiceSmsTemplate"].ToString();
            scriptMessage("Save changes", MessageType.Success);
        }




        protected void btnSaveInstallantTemplate_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInstallmentTemplate.Text))
            {
                var query = "Update branchInfo SET installmentTemplate = '" + txtInstallmentTemplate.Text +
                            "' Where storeId ='" + lblStoreId.Text + "'";
                sqlOperation.executeQuery(query);
            }



            string selectQuery = "SELECT * FROM [BranchInfo] WHERE storeId = '" + lblStoreId.Text + "'";
            DataTable dtBranch = sqlOperation.getDataTable(selectQuery);
            if (dtBranch.Rows.Count == 0)
            {
                commonFunction.createBranchInfo(true, lblStoreId.Text);

                // Again select after insert
                dtBranch = sqlOperation.getDataTable(selectQuery);
            }
            txtInstallmentTemplate.Text = dtBranch.Rows[0]["installmentTemplate"].ToString();
            scriptMessage("Save changes", MessageType.Success);
        }





        private void searchBranchProfile(string storeId)
        {
            try
            {
               string query = "SELECT * FROM [BranchInfo] WHERE storeId = '" + storeId + "'";
                DataTable dtBranch = sqlOperation.getDataTable(query);
                if (dtBranch.Rows.Count == 0)
                {
                    commonFunction.createBranchInfo(true, lblStoreId.Text);

                    // Again select after insert
                    dtBranch = sqlOperation.getDataTable(query);
                }

                txtInvoiceSmsTemplate.Text = dtBranch.Rows[0]["invoiceSmsTemplate"].ToString();
                txtInstallmentTemplate.Text = dtBranch.Rows[0]["installmentTemplate"].ToString();
            }
            catch
            {
            }

            RefreshImage();

        }




        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            var storeId = ddlStore.SelectedValue;

            searchBranchProfile(storeId);
        }

       
    }


    public enum MessageType
    {


        Success,
        Error,
        Info,
        Warning


    };
}
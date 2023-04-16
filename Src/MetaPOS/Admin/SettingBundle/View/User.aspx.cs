using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using System.Web.Services;
using MetaPOS.Admin.RecordBundle.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.SettingBundle.View
{


    public partial class User : BasePage
    {
        private SqlOperation objSql = new SqlOperation();
        private CommonFunction objCommonFun = new CommonFunction();
        private DataSet ds;
        private RoleModel roleModel = new RoleModel();

        private static string globalUserRoleID = null;


        public enum MessageType
        {
            Success,
            Error,
            Info,
            Warning
        };


        private static string selectedRowID = "", query = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!objCommonFun.accessChecker("User"))
                    {
                        objCommonFun.pageout();
                    }

                    lblEmail.Visible = false;
                    refreshGrd();

                    var msg = Request["msg"];
                    if (msg == "success")
                        DataAccess.CommonFunction.msgCode = 1;
                    confirmationMessage();
                }


                if (Session["userRight"].ToString() == "Super")
                {
                    dynamicSectionBreadCrumb.InnerText = "Reseller";
                    lblGridTitle.InnerText = "Reseller List";
                    btnAddRole.Text = "Add Reseller";
                }
                else if (Session["userRight"].ToString() == "Group")
                {
                    checkUserRoleLimit(Session["roleId"].ToString());
                    dynamicSectionBreadCrumb.InnerText = Resources.Language.Title_company;
                    lblGridTitle.InnerText = Resources.Language.Lbl_company_company_list;
                    btnAddRole.Text = Resources.Language.Btn_company_add_company;


                }
                else if (Session["userRight"].ToString() == "Branch")
                {
                    checkUserRoleLimit(Session["groupId"].ToString());

                    dynamicSectionBreadCrumb.InnerText = Resources.Language.Title_user;
                    lblGridTitle.InnerText = Resources.Language.Lbl_user_user_list;
                    btnAddRole.Text = Resources.Language.Btn_user_add_user;
                }
            }
            catch (Exception)
            {
                objCommonFun.pageout();
            }

        }





        private void checkUserRoleLimit(string roleId)
        {
            int branchLimit = 0, userLimit = 0;
            DataTable dtLimit = roleModel.getRoleDataModelByRoleId(roleId);
            if (dtLimit.Rows.Count > 0)
            {
                branchLimit = Convert.ToInt32(dtLimit.Rows[0]["branchLimit"]);
                userLimit = Convert.ToInt32(dtLimit.Rows[0]["userLimit"]);
            }

            if (Session["userRight"].ToString() == "Group")
            {
                var dtCreatedBranch = roleModel.getRoleDataModelForBranchCreate(roleId);
                if (dtCreatedBranch.Rows.Count >= branchLimit)
                {
                    btnAddRole.Visible = false;
                }
            }
            else if (Session["userRight"].ToString() == "Branch")
            {
                var dtCreatedUser = roleModel.getRoleDataModelForUserCreate(roleId);
                if (dtCreatedUser.Rows.Count >= userLimit)
                {
                    btnAddRole.Visible = false;
                }
            }
        }




        private void confirmationMessage()
        {
            if (DataAccess.CommonFunction.msgCode == -1)
                scriptMessage("Sorry! Operation Failed.", MessageType.Error);
            else if (DataAccess.CommonFunction.msgCode == 1)
                scriptMessage("Operation Successful.", MessageType.Success);

            DataAccess.CommonFunction.msgCode = 0;
        }





        public void scriptMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + message + "','" + type + "');", true);
        }





        private void refreshGrd()
        {
            // Store branch id in a lable for CRUD operation
            lblSearchBranchId.Text = Session["branchId"].ToString();
            lblSearchRoleId.Text = Session["roleId"].ToString();

            // Fire gridview query for different user level 
            if (Session["userRight"].ToString() == "Branch")
            {
                query = "SELECT * FROM [RoleInfo]" +
                        " WHERE ([email] LIKE IsNull('%" + txtSearch.Text + "%', email)) " +
                        " AND [userRight]='Regular' " +
                        " AND [branchId]='" + lblSearchRoleId.Text + "' " +
                        " AND [active] ='" + ddlActiveStatus.SelectedValue + "' " +
                        " ORDER BY [RoleInfo].[roleID] ASC";
                //dtsRoleInfoGrd.SelectCommand = query;
                ViewState["grdQuery"] = query;
            }
            else if (Session["userRight"].ToString() == "Group")
            {
                query = "SELECT * FROM [RoleInfo] " +
                        " WHERE ([email] LIKE IsNull('%" + txtSearch.Text + "%', email)) " +
                        " AND [userRight]='Branch' " +
                        " AND [groupId]='" + lblSearchRoleId.Text + "' " +
                        " AND [active] ='" + ddlActiveStatus.SelectedValue + "' " +
                        " ORDER BY [RoleInfo].[roleID] ASC";
                //dtsRoleInfoGrd.SelectCommand = query;
                ViewState["grdQuery"] = query;
            }
            else if (Session["userRight"].ToString() == "Super")
            {
                query = "SELECT * FROM [RoleInfo] " +
                        " WHERE ([email] LIKE IsNull('%" + txtSearch.Text + "%', email)) " +
                        " AND [userRight]='Group' " +
                        " AND [active] ='" + ddlActiveStatus.SelectedValue + "' " +
                        " ORDER BY [RoleInfo].[roleID] ASC";

                //dtsRoleInfoGrd.SelectCommand = query;
                ViewState["grdQuery"] = query;
            }


            SqlDataSource dsUser = new SqlDataSource();
            dsUser.ID = "dsUser";
            this.Page.Controls.Add(dsUser);
            var constr = GlobalVariable.getConnectionStringName();
            dsUser.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsUser.SelectCommand = query;
            grdRoleInfo.DataSource = dsUser;
            grdRoleInfo.DataBind();
            ViewState["grdQuery"] = query;
        }





        public void isSupperAccess()
        {
            query = "SELECT userRight FROM RoleInfo WHERE userRight='" + Session["userRight"] + "'";
            ds = objSql.getDataSet(query);
            if (ds.Tables[0].Rows[0][0].ToString() == "Super")
            {
                dtlRoleInfo.Fields[8].Visible = false;
                dtlRoleInfo.Fields[9].Visible = false;
                dtlRoleInfo.Fields[12].Visible = false;

            }
            else if (ds.Tables[0].Rows[0][0].ToString() == "Group")
            {
                dtlRoleInfo.Fields[6].Visible = false;
                dtlRoleInfo.Fields[7].Visible = false;
                dtlRoleInfo.Fields[10].Visible = false;
                dtlRoleInfo.Fields[11].Visible = false;
                dtlRoleInfo.Fields[12].Visible = false;

                dtlRoleInfo.Fields[17].Visible = true;
                dtlRoleInfo.Fields[18].Visible = true;
            }
            else
            {

                dtlRoleInfo.Fields[6].Visible = false;
                dtlRoleInfo.Fields[7].Visible = false;
                dtlRoleInfo.Fields[10].Visible = false;
                dtlRoleInfo.Fields[11].Visible = false;

                dtlRoleInfo.Fields[12].Visible = true;

                dtlRoleInfo.Fields[8].Visible = false;
                dtlRoleInfo.Fields[9].Visible = false;
            }
        }




        private void reloadPage()
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }





        private void assessPageListBuilder(CheckBox chk)
        {
            if (chk.Checked)
            {
                lblAccessPageList.Text = lblAccessPageList.Text + chk.Text + "; ";
            }
        }



        private void assessToListBuilder(CheckBox chk)
        {
            if (chk.Checked)
            {
                lblAccessToList.Text = lblAccessToList.Text + chk.Text + "; ";
            }
        }


        private void accessPageEditInsert()
        {
            // Access Pages
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSale"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkInvoice"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSlip"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkCustomer"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkQuotation"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkWarranty"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkStock"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkStockReport"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkProduct"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkPurchase"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkPackage"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkOffer"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkEcommerce"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkReturn"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkDamage"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkCancel"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkWarning"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSupply"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSalary"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkTransaction"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkProfitLoss"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSMS"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSummary"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkDashboard"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkAnalytic"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkToken"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSupplier"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkCategory"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("ckhManufacturer"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkLocation"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkField"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkAttribute"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkParticular"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkStaff"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkExpense"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkReceive"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkBank"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkCard"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkBanking"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkWeb"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkWarehouse"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkUnitMeasurement"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkServicing"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkDueReminder"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkPurchaseReport"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkServiceType"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkService"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkInventoryReport"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSupplierCommission"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkSubscription"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkExpiry"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkImport"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkOffline"));



            assessToListBuilder((CheckBox)dtlRoleInfo.FindControl("chkDisplayBuyPrice"));
            assessToListBuilder((CheckBox)dtlRoleInfo.FindControl("chkDisplayWholesalePrice"));
            assessToListBuilder((CheckBox)dtlRoleInfo.FindControl("chkDisplaySalePrice"));


            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkAdd"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkEdit"));
            assessPageListBuilder((CheckBox)dtlRoleInfo.FindControl("chkDelete"));




            if (Session["userRight"].ToString() == "Super")
            {
                lblAccessPageList.Text = lblAccessPageList.Text + "Dashboard; User; Security; Setting; Coder; Web; Sync; Version; Docs; Summary; SMS; Stock; Add; Edit; Delete;";

                string globalQuery = lblAccessPageList.Text;
                string globalAccessPage = globalQuery.ToString().TrimEnd(' ');
                string[] gloAccPageSplit = globalAccessPage.Split(' ');

                // Update to all branches and all users coresponding to this group access pages
                string accessQueryBranch = "",
                    accessPageBranchDB = "",
                    //accessPageBranchLbl = "",
                    //accessPageUserLbl = "",
                    branchFinalAccess = "";
                int i = 0, j = 0, k = 0;
                DataSet accessDsBranch;//, groupDs;

                // Branch Update
                accessQueryBranch = "SELECT * FROM RoleInfo WHERE groupId ='" + selectedRowID + "' AND branchId='0'";
                accessDsBranch = objSql.getDataSet(accessQueryBranch);
                int countBranch = accessDsBranch.Tables[0].Rows.Count;
                if (countBranch <= 0)
                    return;

                for (k = 0; k < countBranch; k++)
                {
                    string branchId = accessDsBranch.Tables[0].Rows[k][0].ToString();
                    accessPageBranchDB = accessDsBranch.Tables[0].Rows[k][4].ToString().TrimEnd(' ');
                    string[] accessPageDBSplit = accessPageBranchDB.Split(' ');
                    branchFinalAccess = "";

                    for (i = 0; i < gloAccPageSplit.Length; i++)
                    {
                        for (j = 0; j < accessPageDBSplit.Length; j++)
                        {
                            if (gloAccPageSplit[i] == accessPageDBSplit[j])
                                branchFinalAccess += gloAccPageSplit[i] + " ";
                        }
                    }
                    string quaryOfBranchUpdate = "UPDATE RoleInfo SET accessPage = '" + branchFinalAccess +
                                                 "' WHERE roleId = '" + branchId + "'";
                    objSql.executeQuery(quaryOfBranchUpdate);
                }

                // User Update
                string UserId = "", accessQueryUser = "", accessPageUserDB = "", userFinalAccess = "";
                DataSet accessDsUser;

                accessQueryUser = "SELECT * FROM RoleInfo WHERE groupId ='" + selectedRowID + "' AND branchId !='0' ";
                accessDsUser = objSql.getDataSet(accessQueryUser);
                int countUser = accessDsUser.Tables[0].Rows.Count;


                for (k = 0; k < countUser; k++)
                {
                    UserId = accessDsUser.Tables[0].Rows[k][0].ToString();
                    accessPageUserDB = accessDsUser.Tables[0].Rows[k][4].ToString().TrimEnd(' ');

                    string[] accessPageDBUserSplit = accessPageUserDB.Split(' ');
                    userFinalAccess = "";

                    for (i = 0; i < gloAccPageSplit.Length; i++)
                    {
                        for (j = 0; j < accessPageDBUserSplit.Length; j++)
                        {
                            if (gloAccPageSplit[i] == accessPageDBUserSplit[j])
                                userFinalAccess += gloAccPageSplit[i] + " ";
                        }
                    }
                    string quaryOfUserUpdate = "UPDATE RoleInfo SET accessPage = '" + userFinalAccess +
                                               "' WHERE roleId = '" + UserId + "'";
                    objSql.executeQuery(quaryOfUserUpdate);
                }
            }
            else if (Session["userRight"].ToString() == "Group")
            {
                lblAccessPageList.Text = lblAccessPageList.Text + "Subscription; User; Profile; Security; Version; Docs; Add; Edit; Delete; ";

                // Update to all users corresponding to this branch access pages
                string UserId = "", accessQueryUser = "", accessPageUserDB = "", userFinalAccess = "";
                int i = 0, j = 0, k = 0;
                DataSet accessDsUser;

                accessQueryUser = "SELECT * FROM RoleInfo WHERE roleId ='" + selectedRowID + "'";
                accessDsUser = objSql.getDataSet(accessQueryUser);
                int countUser = accessDsUser.Tables[0].Rows.Count;

                if (countUser <= 0)
                    return;

                for (k = 0; k < countUser; k++)
                {
                    UserId = accessDsUser.Tables[0].Rows[k][0].ToString();
                    accessPageUserDB = accessDsUser.Tables[0].Rows[k][4].ToString().TrimEnd(' ');

                    string globalQuery = lblAccessPageList.Text;
                    string globalAccessPage = globalQuery.ToString().TrimEnd(' ');
                    string[] gloAccPageSplit = globalAccessPage.Split(' ');

                    string[] accessPageDBUserSplit = accessPageUserDB.Split(' ');
                    userFinalAccess = "";

                    for (i = 0; i < gloAccPageSplit.Length; i++)
                    {
                        for (j = 0; j < accessPageDBUserSplit.Length; j++)
                        {
                            if (gloAccPageSplit[i] == accessPageDBUserSplit[j])
                                userFinalAccess += gloAccPageSplit[i] + " ";
                        }
                    }
                    string quaryOfUserUpdate = "UPDATE RoleInfo SET accessPage = '" + userFinalAccess +
                                               "' WHERE roleId = '" + UserId + "'";
                    objSql.executeQuery(quaryOfUserUpdate);
                }
                //
            }

            else if (Session["userRight"].ToString() == "Branch")
            {
                lblAccessPageList.Text = lblAccessPageList.Text + "Subscription; Security; Version; Docs; ";
            }
        }





        private bool accessPageChecker(string pageName)
        {
            if (lblAccessPageList.Text.Contains(pageName + ";"))
                return true;

            return false;
        }


        private void accessPageInEdit()
        {
            ((CheckBox)dtlRoleInfo.FindControl("chkSale")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSale")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkInvoice")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkInvoice")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSlip")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSlip")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkCustomer")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkCustomer")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkQuotation")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkQuotation")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkWarranty")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkWarranty")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkStock")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkStock")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkStockReport")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkStockReport")).Text);

            ((CheckBox)dtlRoleInfo.FindControl("chkProduct")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkProduct")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkPurchase")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkPurchase")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkPackage")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkPackage")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkOffer")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkOffer")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkEcommerce")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkEcommerce")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkReturn")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkReturn")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkDamage")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkDamage")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkCancel")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkCancel")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkWarning")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkWarning")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSupply")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSupply")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSalary")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSalary")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkTransaction")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkTransaction")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkProfitLoss")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkProfitLoss")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSMS")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSMS")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSummary")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSummary")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkDashboard")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkDashboard")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkAnalytic")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkAnalytic")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkToken")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkToken")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSupplier")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSupplier")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkCategory")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkCategory")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("ckhManufacturer")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("ckhManufacturer")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkLocation")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkLocation")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkField")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkField")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkAttribute")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkAttribute")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkParticular")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkParticular")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkStaff")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkStaff")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkExpense")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkExpense")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkReceive")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkReceive")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkBank")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkBank")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkCard")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkCard")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkBanking")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkBanking")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkWeb")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkWeb")).Text);

            ((CheckBox)dtlRoleInfo.FindControl("chkWarehouse")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkWarehouse")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkUnitMeasurement")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkUnitMeasurement")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkServicing")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkServicing")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkDueReminder")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkDueReminder")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkPurchaseReport")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkPurchaseReport")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkServiceType")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkServiceType")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkService")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkService")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkInventoryReport")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkInventoryReport")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSupplierCommission")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSupplierCommission")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkSubscription")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkSubscription")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkExpiry")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkExpiry")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkImport")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkImport")).Text);


            ((CheckBox)dtlRoleInfo.FindControl("chkOffline")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkOffline")).Text);


            ((CheckBox)dtlRoleInfo.FindControl("chkAdd")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkAdd")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkEdit")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkEdit")).Text);
            ((CheckBox)dtlRoleInfo.FindControl("chkDelete")).Checked =
                accessPageChecker(((CheckBox)dtlRoleInfo.FindControl("chkDelete")).Text);

        }




        private void checkAccessible()
        {
            dtlRoleInfo.FindControl("chkSale").Visible = objCommonFun.accessCheckerCreatingUser("Sale");
            dtlRoleInfo.FindControl("chkInvoice").Visible = objCommonFun.accessCheckerCreatingUser("Invoice");
            dtlRoleInfo.FindControl("chkSlip").Visible = objCommonFun.accessCheckerCreatingUser("Slip");
            dtlRoleInfo.FindControl("chkCustomer").Visible = objCommonFun.accessCheckerCreatingUser("Customer");
            dtlRoleInfo.FindControl("chkQuotation").Visible = objCommonFun.accessCheckerCreatingUser("Quotation");
            dtlRoleInfo.FindControl("chkWarranty").Visible = objCommonFun.accessCheckerCreatingUser("Warranty");
            dtlRoleInfo.FindControl("chkStock").Visible = objCommonFun.accessCheckerCreatingUser("Stock");
            dtlRoleInfo.FindControl("chkStockReport").Visible = objCommonFun.accessCheckerCreatingUser("chkStockReport");
            dtlRoleInfo.FindControl("chkProduct").Visible = objCommonFun.accessCheckerCreatingUser("Product");
            dtlRoleInfo.FindControl("chkPurchase").Visible = objCommonFun.accessCheckerCreatingUser("Purchase");
            dtlRoleInfo.FindControl("chkPackage").Visible = objCommonFun.accessCheckerCreatingUser("Package");
            dtlRoleInfo.FindControl("chkOffer").Visible = objCommonFun.accessCheckerCreatingUser("Offer");
            dtlRoleInfo.FindControl("chkEcommerce").Visible = objCommonFun.accessCheckerCreatingUser("Ecommerce");
            dtlRoleInfo.FindControl("chkReturn").Visible = objCommonFun.accessCheckerCreatingUser("Return");
            dtlRoleInfo.FindControl("chkDamage").Visible = objCommonFun.accessCheckerCreatingUser("Damage");
            dtlRoleInfo.FindControl("chkCancel").Visible = objCommonFun.accessCheckerCreatingUser("Cancel");
            dtlRoleInfo.FindControl("chkWarning").Visible = objCommonFun.accessCheckerCreatingUser("Warning");
            dtlRoleInfo.FindControl("chkSupply").Visible = objCommonFun.accessCheckerCreatingUser("Supply");
            dtlRoleInfo.FindControl("chkSalary").Visible = objCommonFun.accessCheckerCreatingUser("Salary");
            dtlRoleInfo.FindControl("chkTransaction").Visible = objCommonFun.accessCheckerCreatingUser("Transaction");
            dtlRoleInfo.FindControl("chkProfitLoss").Visible = objCommonFun.accessCheckerCreatingUser("ProfitLoss");
            dtlRoleInfo.FindControl("chkSMS").Visible = objCommonFun.accessCheckerCreatingUser("SMS");
            dtlRoleInfo.FindControl("chkSummary").Visible = objCommonFun.accessCheckerCreatingUser("Summary");
            dtlRoleInfo.FindControl("chkDashboard").Visible = objCommonFun.accessCheckerCreatingUser("Dashboard");
            dtlRoleInfo.FindControl("chkAnalytic").Visible = objCommonFun.accessCheckerCreatingUser("Analytic");
            dtlRoleInfo.FindControl("chkToken").Visible = objCommonFun.accessCheckerCreatingUser("Token");
            dtlRoleInfo.FindControl("chkSupplier").Visible = objCommonFun.accessCheckerCreatingUser("Supplier");
            dtlRoleInfo.FindControl("chkCategory").Visible = objCommonFun.accessCheckerCreatingUser("Category");
            dtlRoleInfo.FindControl("ckhManufacturer").Visible = objCommonFun.accessCheckerCreatingUser("Manufacturer");
            dtlRoleInfo.FindControl("chkLocation").Visible = objCommonFun.accessCheckerCreatingUser("Location");
            dtlRoleInfo.FindControl("chkField").Visible = objCommonFun.accessCheckerCreatingUser("Field");
            dtlRoleInfo.FindControl("chkAttribute").Visible = objCommonFun.accessCheckerCreatingUser("Attribute");
            dtlRoleInfo.FindControl("chkParticular").Visible = objCommonFun.accessCheckerCreatingUser("Particular");
            dtlRoleInfo.FindControl("chkStaff").Visible = objCommonFun.accessCheckerCreatingUser("Staff");
            dtlRoleInfo.FindControl("chkExpense").Visible = objCommonFun.accessCheckerCreatingUser("Expense");
            dtlRoleInfo.FindControl("chkReceive").Visible = objCommonFun.accessCheckerCreatingUser("Receive");
            dtlRoleInfo.FindControl("chkBanking").Visible = objCommonFun.accessCheckerCreatingUser("Banking");
            dtlRoleInfo.FindControl("chkBank").Visible = objCommonFun.accessCheckerCreatingUser("Bank");
            dtlRoleInfo.FindControl("chkCard").Visible = objCommonFun.accessCheckerCreatingUser("Card");
            dtlRoleInfo.FindControl("chkWeb").Visible = objCommonFun.accessCheckerCreatingUser("Web");
            dtlRoleInfo.FindControl("chkWarehouse").Visible = objCommonFun.accessCheckerCreatingUser("Store");
            dtlRoleInfo.FindControl("chkUnitMeasurement").Visible = objCommonFun.accessCheckerCreatingUser("UnitMeasurement");
            dtlRoleInfo.FindControl("chkServicing").Visible = objCommonFun.accessCheckerCreatingUser("Servicing");
            dtlRoleInfo.FindControl("chkDueReminder").Visible = objCommonFun.accessCheckerCreatingUser("DueReminder");
            dtlRoleInfo.FindControl("chkPurchaseReport").Visible = objCommonFun.accessCheckerCreatingUser("PurchaseReport");
            dtlRoleInfo.FindControl("chkServiceType").Visible = objCommonFun.accessCheckerCreatingUser("ServiceType");
            dtlRoleInfo.FindControl("chkService").Visible = objCommonFun.accessCheckerCreatingUser("Service");
            dtlRoleInfo.FindControl("chkInventoryReport").Visible = objCommonFun.accessCheckerCreatingUser("InventoryReport");
            dtlRoleInfo.FindControl("chkSupplierCommission").Visible = objCommonFun.accessCheckerCreatingUser("SupplierCommission");
            dtlRoleInfo.FindControl("chkSubscription").Visible = objCommonFun.accessCheckerCreatingUser("Subscription");
            dtlRoleInfo.FindControl("chkExpiry").Visible = objCommonFun.accessCheckerCreatingUser("Expiry");
            dtlRoleInfo.FindControl("chkImport").Visible = objCommonFun.accessCheckerCreatingUser("Import");

            dtlRoleInfo.FindControl("chkOffline").Visible = objCommonFun.accessCheckerCreatingUser("Offline");

            dtlRoleInfo.FindControl("chkAdd").Visible = objCommonFun.accessCheckerCreatingUser("Add");
            dtlRoleInfo.FindControl("chkEdit").Visible = objCommonFun.accessCheckerCreatingUser("Edit");
            dtlRoleInfo.FindControl("chkDelete").Visible = objCommonFun.accessCheckerCreatingUser("Delete");
        }





        private bool accessToChecker(string function)
        {
            if (lblAccessToList.Text.Contains(function))
                return true;

            return false;
        }





        private void accessToInEdit()
        {
            ((CheckBox)dtlRoleInfo.FindControl("chkDisplayBuyPrice")).Checked = accessToChecker(((CheckBox)dtlRoleInfo.FindControl("chkDisplayBuyPrice")).Text);

            ((CheckBox)dtlRoleInfo.FindControl("chkDisplayWholesalePrice")).Checked = accessToChecker(((CheckBox)dtlRoleInfo.FindControl("chkDisplayWholesalePrice")).Text);

            ((CheckBox)dtlRoleInfo.FindControl("chkDisplaySalePrice")).Checked = accessToChecker(((CheckBox)dtlRoleInfo.FindControl("chkDisplaySalePrice")).Text);
        }





        private void checkAccessibleToFunction()
        {
            dtlRoleInfo.FindControl("chkDisplayBuyPrice").Visible = objCommonFun.accessToCreatingUser("DisplayBuyPrice");

            dtlRoleInfo.FindControl("chkDisplayWholesalePrice").Visible = objCommonFun.accessToCreatingUser("DisplayWholesalePrice");

            dtlRoleInfo.FindControl("chkDisplaySalePrice").Visible = objCommonFun.accessToCreatingUser("DisplaySalePrice");
        }





        //--> Gridview
        protected void grdRoleInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRowID = (grdRoleInfo.SelectedRow.FindControl("lblRoleID") as Label).Text;

            Response.Redirect("~/admin/useropt?id=" + selectedRowID + "&mode=view");
        }





        protected void grdRoleInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = grdRoleInfo.Rows[e.NewEditIndex];

            Label selectedRowID = (Label)row.FindControl("lblRoleID");

            Response.Redirect("~/admin/useropt?id=" + selectedRowID.Text + "&mode=edit");
        }





        protected void grdRoleInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = grdRoleInfo.Rows[e.RowIndex];

            Label lblRowId = (Label)row.FindControl("lblRoleID");
            string rowId = lblRowId.Text;

            if (ddlActiveStatus.SelectedValue == "1")
            {
                if (Session["userRight"].ToString() == "Super")
                {
                    // inactive Group
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '0' WHERE roleID = '" + rowId + "' ");

                    // inactive Branches
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '0' WHERE groupId = '" + rowId + "' ");

                    // inactive Users
                    ds = objSql.getDataSet("SELECT roleID FROM [RoleInfo] WHERE groupId = '" + rowId + "' ");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objSql.executeQuery("UPDATE [RoleInfo] SET active = '0' WHERE branchId = '" +
                                            ds.Tables[0].Rows[i][0] + "' ");
                    }
                }
                else if (Session["userRight"].ToString() == "Group")
                {
                    // inactive Branches
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '0' WHERE roleID = '" + rowId + "' ");

                    // inactive Users
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '0' WHERE branchId = '" + rowId + "' ");
                }
                else if (Session["userRight"].ToString() == "Branch")
                {
                    // inactive users
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '0' WHERE roleID = '" + rowId + "' ");
                }

                scriptMessage("User Information Removed Successfully.", MessageType.Success);
            }
            else if (ddlActiveStatus.SelectedValue == "0")
            {
                if (Session["userRight"].ToString() == "Super")
                {
                    // active Group
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '1' WHERE roleID = '" + rowId + "' ");

                    // active Branches
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '1' WHERE groupId = '" + rowId + "' ");

                    // inactive Users
                    ds = objSql.getDataSet("SELECT roleID FROM [RoleInfo] WHERE groupId = '" + rowId + "' ");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objSql.executeQuery("UPDATE [RoleInfo] SET active = '1' WHERE branchId = '" +
                                            ds.Tables[0].Rows[i][0].ToString() + "' ");
                    }
                }
                else if (Session["userRight"].ToString() == "Group")
                {
                    // active Branches
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '1' WHERE roleID = '" + rowId + "' ");

                    // active Users
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '1' WHERE branchId = '" + rowId + "' ");
                }
                else if (Session["userRight"].ToString() == "Branch")
                {
                    // active users
                    objSql.executeQuery("UPDATE [RoleInfo] SET active = '1' WHERE roleID = '" + rowId + "' ");
                }

                scriptMessage("User Information Restored Successfully.", MessageType.Success);
            }

            refreshGrd();
        }





        protected void grdRoleInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("btnVerify");

                bool active = btn.Text == "True";

                if (active)
                {
                    btn.Text = "Verified";
                    btn.Enabled = false;
                    btn.CssClass = "verified";
                }
                else
                {
                    btn.Text = "Do Verify";
                }
            }


            if (ddlActiveStatus.SelectedValue == "1")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[10].Text = Resources.Language.Lbl_user_archive;
                }
            }
            else if (ddlActiveStatus.SelectedValue == "0")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[10].Text = Resources.Language.Lbl_user_restore;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    (e.Row.Cells[8].FindControl("btnGrdDelete") as LinkButton).Text =
                        "<span class='glyphicon glyphicon-retweet'></span>";
                }
            }
        }


        private void checkSetting(string Id)
        {
            // if no row exist, create first
            ds = objSql.getDataSet("SELECT * FROM [SettingInfo] WHERE id='" + Id + "' ");

            if (ds.Tables[0].Rows.Count == 0)
            {
                objSql.executeQuery("INSERT INTO [SettingInfo] (id, updateDate,printBarcodeFor,isGoAction,isBetaInvoicePrint) VALUES('" + Id + "', '" +
                                    objCommonFun.GetCurrentTime() + "','Product','1','1') ");
            }
        }





        protected void dtlRoleInfo_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {

            // update storeId
            var dtRole = objSql.getDataTable("SELECT storeId FROM RoleInfo WHERE roleId='" + globalUserRoleID + "'");
            if (dtRole.Rows.Count > 0)
            {
                if (dtRole.Rows[0]["storeId"].ToString() == "0")
                {
                    var dtBranch = objSql.getDataTable("SELECT storeId FROM BranchInfo WHERE Id='" + globalUserRoleID + "' ");
                    if (dtBranch.Rows.Count > 0)
                    {
                        objSql.executeQuery("UPDATE RoleInfo SET storeId='" + dtBranch.Rows[0]["storeId"] + "' WHERE roleId='" + globalUserRoleID + "'");
                    }
                }
            }



            DataAccess.CommonFunction.msgCode = -1;

            if (e.Exception == null)
                if (e.AffectedRows == 1)
                    DataAccess.CommonFunction.msgCode = 1;

            reloadPage();
        }





        protected void dtlRoleInfo_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            lblBranchType.Text = ((DropDownList)((DetailsView)sender).FindControl("ddlBranchType")).SelectedValue;
            lblInheritId.Text = ((DropDownList)((DetailsView)sender).FindControl("ddlInheritId")).SelectedValue;


            if (Session["userRight"].ToString() == "Branch")
            {
                lblStoreId.Text = ((DropDownList)((DetailsView)sender).FindControl("ddlStoreId")).SelectedValue;
            }
            else
            {
                var dtRole = objSql.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + lblRoleId.Text + "'");
                if (dtRole.Rows.Count > 0)
                {
                    lblStoreId.Text = dtRole.Rows[0]["storeId"].ToString();
                }
            }

            if (Session["userRight"].ToString() == "Super" || Session["userRight"].ToString() == "Branch" || Session["userRight"].ToString() == "Super")
            {
                lblBranchType.Text = "''";
                lblInheritId.Text = "0";
            }

            if ((lblBranchType.Text == "sub" && lblInheritId.Text != "0") ||
                (lblBranchType.Text != "sub" && lblInheritId.Text == "0"))
            {
                TextBox tbEmail = (TextBox)dtlRoleInfo.Rows[0].FindControl("txtEmail");

                if (tbEmail.Text != lblEmailUpdate.Text)
                {
                    lblEmailUpdate.Text = tbEmail.Text;
                    lblIsVerify.Text = "0";
                }
                else
                {
                    lblEmailUpdate.Text = lblEmailUpdate.Text;
                    lblIsVerify.Text = lblIsVerify.Text;
                }

                lblPassword.Text =
                    objCommonFun.Encrypt(((TextBox)((DetailsView)sender).FindControl("txtPassword")).Text);
                lblDomainName.Text = ((TextBox)((DetailsView)sender).FindControl("txtDomainName")).Text;
                lblUserLimit.Text = ((TextBox)((DetailsView)sender).FindControl("txtUserLimit")).Text;
                lblBranchLimit.Text = ((TextBox)((DetailsView)sender).FindControl("txtBranchLimit")).Text;

                lblIsDomainActive.Text =
                    ((RadioButtonList)((DetailsView)sender).FindControl("rbIsDomainActive")).SelectedValue;

                lblBranchType.Text = ((DropDownList)((DetailsView)sender).FindControl("ddlBranchType")).SelectedValue;
                lblInheritId.Text = ((DropDownList)((DetailsView)sender).FindControl("ddlInheritId")).SelectedValue;

                //if()
                //lblStoreId.Text = ((DropDownList)((DetailsView)sender).FindControl("ddlStoreId")).SelectedValue;


                //work here

                //lblExpiryDate.Text = ((TextBox)((DetailsView)sender).FindControl("txtExpiryDate")).Text;//Session["expiryDate"] "txtExpiryDate"
                lblMonthlyFee.Text = ((TextBox)((DetailsView)sender).FindControl("txtMonthlyFee")).Text;


                if (lblExpiryDate.Text == "")
                    lblExpiryDate.Text = objCommonFun.GetCurrentTime().ToString();
                if (lblMonthlyFee.Text == "")
                    lblMonthlyFee.Text = "0";
                if (lblUserLimit.Text == "")
                    lblUserLimit.Text = "0";
                if (lblBranchLimit.Text == "")
                    lblBranchLimit.Text = "0";
                if (lblBranchType.Text == "")
                    lblBranchType.Text = "";
                lblAccessPageList.Text = "";
                lblAccessToList.Text = "";

                accessPageEditInsert();

                //query =
                //    "UPDATE [RoleInfo] SET [title] = '', [password] = '" + lblPassword.Text + "', [email] = '" + lblEmailUpdate.Text + "', [accessPage] = '" + lblAccessPageList.Text + "', [accessTo] = '" + lblAccessToList.Text + "', [updateDate] = '" + lblCurrentDate.Text + "',verify='" + lblIsVerify.Text + "', domainName='" + lblDomainName.Text + "',isDomainActive='" + lblIsDomainActive.Text + "',branchLimit='" + lblBranchLimit.Text + "', userLimit='" + lblUserLimit.Text + "',branchType='" + lblBranchType.Text + "',inheritId='" + lblInheritId.Text + "',storeId='" + lblStoreId.Text + "',monthlyFee='" + lblMonthlyFee.Text + "',ExpiryDate='" + lblExpiryDate.Text + "' WHERE [roleID] = '21'";
            }
        }





        protected void dtlRoleInfo_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            DataAccess.CommonFunction.msgCode = -1;
            if (e.Exception == null)
                if (e.AffectedRows == 1)
                    DataAccess.CommonFunction.msgCode = 1;

            reloadPage();
        }





        protected void dtlRoleInfo_DataBound(object sender, EventArgs e)
        {
            try
            {

                if (dtlRoleInfo.CurrentMode == DetailsViewMode.ReadOnly || dtlRoleInfo.CurrentMode == DetailsViewMode.Edit)
                {
                    if (Session["userRight"].ToString() == "Super")
                    {
                        lblModalTitle.Text = "Reseller / ";
                    }
                    else if (Session["userRight"].ToString() == "Group")
                    {
                        lblModalTitle.Text = "Company / ";
                    }
                    else if (Session["userRight"].ToString() == "Branch")
                    {
                        lblModalTitle.Text = "User / ";
                    }
                }

                if (dtlRoleInfo.CurrentMode == DetailsViewMode.Insert)
                {
                    if (Session["userRight"].ToString() == "Super")
                    {
                        lblModalTitle.Text = "Reseller / New";
                    }
                    else if (Session["userRight"].ToString() == "Group")
                    {
                        lblModalTitle.Text = "Company / New";
                    }
                    else if (Session["userRight"].ToString() == "Branch")
                    {
                        lblModalTitle.Text = "User / New";
                    }

                    lblNewID.Text = "";

                    var ddl = dtlRoleInfo.FindControl("ddlInheritId") as DropDownList;
                    if (ddl != null)
                    {
                        DataSet ds = objSql.getDataSet("SELECT [roleID], [title] FROM [RoleInfo] where branchType='main'");
                        ddl.Items.Insert(0, new ListItem("None", "0"));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ddl.Items.Insert(i + 1, new ListItem(ds.Tables[0].Rows[i]["title"].ToString(), ds.Tables[0].Rows[i]["roleID"].ToString()));
                        }
                    }


                    var roleId = Session["roleId"].ToString();
                    var branchId = Session["roleId"].ToString();

                    // Branch all user roleid
                    var branchAllUserRoleId = objCommonFun.getBranchAllUserRoleId(roleId, branchId);

                    var ddlStore = dtlRoleInfo.FindControl("ddlStoreId") as DropDownList;
                    if (ddlStore != null)
                    {
                        var ds = objSql.getDataSet("SELECT [Id], [name] FROM [WarehouseInfo] where name !='' AND active='1' " + branchAllUserRoleId + "");
                        if (Session["userRight"].ToString() != "Branch")
                        {
                            ddlStore.Items.Insert(0, new ListItem("None", "0"));
                        }
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ddlStore.Items.Insert(i, new ListItem(ds.Tables[0].Rows[i]["name"].ToString(), ds.Tables[0].Rows[i]["Id"].ToString()));
                        }

                        ddlStore.SelectedValue = lblStoreId.Text;
                    }

                    checkAccessible();
                    //checkAccessibleToFunction();
                }


                if (dtlRoleInfo.CurrentMode == DetailsViewMode.Edit)
                {
                    accessPageInEdit();
                    checkAccessible();

                    accessToInEdit();
                    //checkAccessibleToFunction();

                    lblRoleId.Text = ((TextBox)((DetailsView)sender).FindControl("txtRoleID")).Text;

                    var ddl = dtlRoleInfo.FindControl("ddlInheritId") as DropDownList;
                    if (ddl != null)
                    {
                        DataSet ds = objSql.getDataSet("SELECT [roleID], [title] FROM [RoleInfo] where branchType='main'");
                        ddl.Items.Insert(0, new ListItem("None", "0"));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ddl.Items.Insert(i, new ListItem(ds.Tables[0].Rows[i]["title"].ToString(), ds.Tables[0].Rows[i]["roleID"].ToString()));
                        }

                        ddl.SelectedValue = lblInheritId.Text;
                    }


                    var roleId = Session["roleId"].ToString();
                    var branchId = Session["roleId"].ToString();

                    // Branch all user roleid
                    var branchAllUserRoleId = objCommonFun.getBranchAllUserRoleId(roleId, branchId);

                    var ddlSore = dtlRoleInfo.FindControl("ddlStoreId") as DropDownList;
                    if (ddlSore != null)
                    {
                        DataSet ds = objSql.getDataSet("SELECT [Id], [name] FROM [WarehouseInfo] where name != '' AND active='1' " + branchAllUserRoleId + "");
                        if (Session["userRight"].ToString() != "Branch")
                        {
                            ddlSore.Items.Insert(0, new ListItem("None", "0"));
                        }
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ddlSore.Items.Insert(i, new ListItem(ds.Tables[0].Rows[i]["name"].ToString(), ds.Tables[0].Rows[i]["Id"].ToString()));
                        }
                        ddlSore.SelectedValue = lblStoreId.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                lblTest.Text = ex.ToString();
            }
        }





        protected void cusEmail_ServerValidateInsertMode(object source, ServerValidateEventArgs e)
        {
            ds = objSql.getDataSet("SELECT * FROM [RoleInfo] WHERE email = '" + (dtlRoleInfo.FindControl("txtEmail") as TextBox).Text + "'");
            if (ds.Tables[0].Rows.Count != 0)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                "<script>$('#modalDtl').modal('show');</script>", false);
        }





        protected void cusEmail_ServerValidateEditMode(object source, ServerValidateEventArgs e)
        {
            ds = objSql.getDataSet("SELECT * FROM [RoleInfo] WHERE email = '" + (dtlRoleInfo.FindControl("txtEmail") as TextBox).Text + "'");
            if (ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0].ToString() != selectedRowID)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                "<script>$('#modalDtl').modal('show');</script>", false);
        }





        //<-- DetailsView 

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/useropt");
        }











        protected void btnMultipleSelection_Click(object sender, EventArgs e)
        {
            //lblMultipleSelection.Text = hdnfldVariable.Value;
            //ddlMultipleSelection.SelectedIndex = -1;
        }





        protected void txtSearchByID_TextChanged(object sender, EventArgs e)
        {
            refreshGrd();
        }





        protected void grdRoleInfo_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "verify")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdRoleInfo.Rows[rowIndex];

                //Access Cell values.
                Label lblEmailCs = (Label)row.FindControl("lblEmail");
                lblEmail.Text = lblEmailCs.Text;

                //open modal
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "none",
                    "<script>$('#mailVerify').modal('show');</script>", false);


                //Random Number Generator
                string randomNum = "";
                Random rng = new Random();

                for (int i = 1; i < 6; i++)
                {
                    int rngInt = rng.Next(0, 5);
                    randomNum += rngInt.ToString();
                }

                //update database
                query = "UPDATE RoleInfo SET verifyNum = '" + randomNum + "' WHERE email = '" + lblEmailCs.Text + "'";
                objSql.executeQuery(query);

                //send mail
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("support@metaposbd.com");
                mailMessage.To.Add(new MailAddress(lblEmail.Text));

                string emailBody = "This email is generated from Email Verification link of MetaPOS. If you didn't ask to verify your mail, please ignore this email.<br/><br/>"
                                   + "Your current verification code is : " + randomNum
                                   + "<br/><br/><br/>"
                                   + "Sincerely,<br/>"
                                   + "MetaPOS Team<br/>"
                                   + "www.metaposbd.com<br/>";
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Email Verify Credentials - MetaPOS";
                mailMessage.Body = emailBody;

                var smt = new SmtpClient("mail.metaposbd.com");
                var networkCred = new NetworkCredential("support@metaposbd.com", "rtj#s()k8d#k");
                smt.Credentials = networkCred;

                try
                {
                    smt.Send(mailMessage);
                    lblMessage.Text = "Please check your mail to get the verification number.";
                }
                catch (Exception)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mailVerify",
                        "$('#mailVerify').modal('hide');", true);

                    scriptMessage("Sorry! your mail could not be sent. Please try again.", MessageType.Error);
                }
            }
        }





        protected void btnVerify_OnClick(object sender, EventArgs e)
        {
            ds = objSql.getDataSet("SELECT verifyNum FROM [RoleInfo] WHERE email = '" + lblEmail.Text + "' ");
            string vNumber = ds.Tables[0].Rows[0][0].ToString();

            if (vNumber != txtVerify.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#mailVerify').modal('hide');</script>", false);

                scriptMessage("Email is not verified..", MessageType.Error);
            }
            else
            {
                query = "UPDATE RoleInfo SET verify = '" + 1 + "' WHERE email = '" + lblEmail.Text + "'";
                objSql.executeQuery(query);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "none",
                    "<script>$('#mailVerify').modal('hide');</script>", false);

                scriptMessage("Email verified", MessageType.Success);

                refreshGrd();
            }
        }





        public void GrdDataVisibilityControl()
        {
            List<DataControlField> columns = dtlRoleInfo.Rows.Cast<DataControlField>().ToList();
            columns.Find(col => col.HeaderText == "User ID").Visible = false;
            columns.Find(col => col.HeaderText == "Website URL").Visible = false;
            columns.Find(col => col.HeaderText == "Activate URL").Visible = false;
        }






        [WebMethod]
        public static string updateSubscriptionAction(string jsonData)
        {
            var userModel = new UserModel();
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);

            userModel.branchId = Convert.ToInt32(data["branchId"].Value<string>());
            userModel.sign = data["sign"].Value<string>();
            userModel.subscriptionFee = data["addonsPrice"].Value<decimal>();

            return userModel.updateMonthFee();
        }



    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AccountBundle.View
{


    public partial class Salary :BasePage
    {


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        SqlDataSource dsCashReportSalary = new SqlDataSource();
        private DataSet ds;

        private static string query = "";//, descr = "";
        private int i;
        private static decimal cashInTotal, cashOutTotal, cashInHandTotal;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Salary"))
                {
                    commonFunction.pageout();
                }

                //DataSet
                dsCashReportSalary.ID = "dsStock";
                this.Page.Controls.Add(dsCashReportSalary);

                commonFunction.fillAllDdl(ddlCashOutTypeStaff,
                                            "SELECT DISTINCT staffID,name FROM [StaffInfo] WHERE active='1' " +
                                            Session["userAccessParameters"] + " ORDER BY name ASC", "name", "staffID");

                commonFunction.fillAllDdl(ddlSearchStaff, "SELECT DISTINCT staffID,name FROM [StaffInfo] WHERE active='1'" +
                                                            Session["userAccessParameters"] + " ORDER BY name ASC", "name", "staffID");

                ddlCashOutTypeStaff.Items.Insert(0, Resources.Language.Lbl_salary_select_staff);
                ddlSearchStaff.Items.Insert(0, new ListItem(Resources.Language.Lbl_salary_search_all, "0"));

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
                if (Session["userRight"].ToString() == "Branch")
                {
                    ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_salary_search_all_store, "0"));
                }

                // User wise filter
                if (Session["userRight"].ToString() == "Branch")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE (userRight='Regular' OR roleId='" + Session["roleId"] + "') AND active='1' " + Session["storeAccessParameters"] + "", "title", "roleId");  
                    ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_salary_search_all_user, "0")); 
                }
                else if (Session["userRight"].ToString() == "Regular")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE userRight='Regular' AND active='1' AND roleId='" + Session["roleId"] + "'", "title", "roleId");
                    divUserList.Visible = false;
                    ddlStoreList.SelectedValue = Session["storeId"].ToString();
                }

                txtSearchDateFrom.Text = txtSalaryDate.Text = txtSearchDateTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
            }

            searchResult();
            DataVisibilityControl();
        }





        //--> Function  
        private void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void refreshGrd(string query)
        {
            cashInTotal = 0M;
            cashOutTotal = 0M;
            cashInHandTotal = 0M;
            try
            {
                ds = objSql.getDataSet(query);
                cashInHandTotal = Convert.ToDecimal(ds.Tables[0].Rows[0][5].ToString());
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cashInTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][3].ToString());
                    cashOutTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][4].ToString());
                }
                
            }
            catch
            {
                cashInTotal = 0M;
                cashOutTotal = 0M;
                cashInHandTotal = 0M;
            }

            lblTotalDueBalance.Text = Resources.Language.Lbl_salary_total_salary + " : " + cashOutTotal.ToString();

            var constr = GlobalVariable.getConnectionStringName();
            dsCashReportSalary.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsCashReportSalary.SelectCommand = query;
            grdCashReportInfo.DataSource = dsCashReportSalary;
            grdCashReportInfo.DataBind();
        }





        private void searchResult()
        {
            DateTime searchFrom, searchTo;
            try
            {
                searchFrom = Convert.ToDateTime(txtSearchDateFrom.Text);
            }
            catch
            {
                searchFrom = Convert.ToDateTime("01-jan-2000");
            }
            try
            {
                searchTo = Convert.ToDateTime(txtSearchDateTo.Text);
            }
            catch
            {
                searchTo = Convert.ToDateTime("01-jan-2090");
            }

            
            query = "SELECT tbl.Id,tbl.cashType,tbl.descr,tbl.cashIn,tbl.cashout,tbl.cashInHand,tbl.entryDate,tbl.billNo,tbl.mainDescr,tbl.roleID,tbl.status,tbl.adjust,tbl.branchId,tbl.groupId,staff.staffID,staff.name AS Particular,warehouse.name as storeName FROM CashReportInfo AS tbl " +
                    "INNER JOIN StaffInfo AS staff ON tbl.descr = staff.staffID " +
                    "LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id=tbl.storeId " +
                    "WHERE (tbl.status = '1') AND (tbl.descr = '" + ddlSearchStaff.SelectedValue + "' OR '" + ddlSearchStaff.SelectedValue + "' = '0') " +
                    "AND (tbl.storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') " +
                    "AND (tbl.roleId='" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "'='0') " +
                    "AND (tbl.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  " +
                    "AND (tbl.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') " +
                    commonFunction.getUserAccessParameters("tbl") + " ORDER BY tbl.Id DESC, tbl.entryDate ASC";

            refreshGrd(query);
        }





        private void reset()
        {
            txtCashOutAmount.Text = "";
            txtCashOutMainDescr.Text = "";
        }





        //<-- Function 

        //--> Gridview
        protected void grdCashReportInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ds = objSql.getDataSet("SELECT * FROM [CashReportInfo] ORDER BY ID DESC");
            if (((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblId") as Label).Text !=
                ds.Tables[0].Rows[0][0].ToString())
            {
                e.Cancel = true;
                scriptMessage("You can Delete Only Last Transaction.", MessageType.Warning);
            }
            else if (((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblCashType") as Label).Text == "Sales")
            {
                e.Cancel = true;
                scriptMessage("You can't Delete Sales Transaction.", MessageType.Warning);
            }

            //--> cashDue and Balance setting
            decimal cashOut = 0;
            cashOut = -Convert.ToDecimal(((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblCashOut") as Label).Text);
            commonFunction.cashDueAndBalanceValue(
                ((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblDescr") as Label).Text);
            DataAccess.CommonFunction.cashDue -= cashOut;
            query =
                "INSERT INTO [SupplierStatus] (                             supCompany,                                        billNo,   status,        payment,                        cashDue,                                      balance,                                  entryDate                 ) VALUES ('" +
                ((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblDescr") as Label).Text + "', '-1',   'cashOut', '" +
                cashOut + "', '" + DataAccess.CommonFunction.cashDue + "', '" + DataAccess.CommonFunction.balance +
                "', '" + commonFunction.GetCurrentTime().ToShortDateString() + "')";
            objSql.executeQuery(query);
            //<-- cashDue and Balance setting
        }





        protected void grdCashReportInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            searchResult();
        }





        protected void grdCashReportInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblCashIn")).Text = cashInTotal.ToString();
                ((Label)e.Row.FindControl("lblCashOut")).Text = cashOutTotal.ToString();
                //((Label)e.Row.FindControl("lblCashInHand")).Text = cashInHandTotal.ToString();
            }
        }





        //<-- Gridview

        protected void btnCashOutAdd_Click(object sender, EventArgs e)
        {
            if (ddlCashOutTypeStaff.Text == "Select Staff")
            {
                scriptMessage("Select must a Staff!", MessageType.Warning);
                return;
            }

            if (txtCashOutAmount.Text == "")
            {
                scriptMessage("Cash-Out Amount Requird!", MessageType.Warning);
                return;
            }

            if (Convert.ToDecimal(txtCashOutAmount.Text) == 0)
            {
                scriptMessage("0 amount is not possible!", MessageType.Warning);
                return;
            }

            if (String.IsNullOrEmpty(txtSalaryDate.Text))
                txtSalaryDate.Text = commonFunction.GetCurrentTime().ToString();

            scriptMessage(
                commonFunction.cashTransactionWithDate(0, Convert.ToDecimal(txtCashOutAmount.Text), "Staff Salary",
                    ddlCashOutTypeStaff.Text, "", txtCashOutMainDescr.Text, "1", "0", txtSalaryDate.Text), MessageType.Success);
            searchResult();
            reset();
        }





        protected void btnCashSearch_Click(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (grdCashReportInfo.Rows.Count == 0)
            {
                scriptMessage("There are no data records to Print.", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "TransReport";
                Session["reportName"] = "Salary";
                Session["Type"] = "0";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }





        protected void btnAddPaticular_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Particular"))
            {
                DataAccess.CommonFunction obj = new DataAccess.CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification",
                    "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Staff");
            }
        }





        protected void ddlStoreList_OnSelectedIndexChangedlStoreList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            commonFunction.getUserListByStore(ddlUserList, ddlStoreList);

            searchResult();
        }


        private void DataVisibilityControl()
        {
            if (Session["userRight"].ToString() != "Branch")
            {
                divStoreList.Visible = false;

                // Gridview 
                List<DataControlField> columns = grdCashReportInfo.Columns.Cast<DataControlField>().ToList();
                columns.Find(col => col.SortExpression == "storeName").Visible = false;
            }
        }





        protected void ddlUserList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
        }



        protected void grdCashReportInfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCashReportInfo.PageIndex = e.NewPageIndex;
            searchResult();
        }



        public enum MessageType
        {
            Success,
            Error,
            Info,
            Warning
        };


    }


}
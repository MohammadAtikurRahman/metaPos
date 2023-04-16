using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AccountBundle.View
{


    public partial class Expense : BasePage//Page
    {


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        private static string query = "", descr = "";
        private int i;
        private static decimal cashInTotal, cashOutTotal, cashInHandTotal;

        SqlDataSource dsExpense = new SqlDataSource();

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
                if (!commonFunction.accessChecker("Expense"))
                {
                    commonFunction.pageout();
                }

                commonFunction.fillAllDdl(ddlCashInTypeSup,
                    "SELECT DISTINCT Id,cashType FROM [CashCatInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY cashType ASC", "cashType", "Id");

                commonFunction.fillAllDdl(ddlCashOutTypeSup,
                    "SELECT DISTINCT Id,cashType FROM [CashCatInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY cashType ASC", "cashType", "Id");

                commonFunction.fillAllDdl(ddlCashSearchType,
                    "SELECT DISTINCT Id,cashType FROM [CashCatInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY cashType ASC", "cashType", "Id");

                //ddlCashInType.Items.Insert(0, "Select Particular");
                //ddlCashInType.Items.Insert(1, "Bank Deposit");

                ddlCashOutTypeSup.Items.Insert(0, Resources.Language.Lbl_expense_select_particular);

                ddlCashSearchType.Items.Insert(0, new ListItem(Resources.Language.Lbl_expense_search_all, "0"));

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
                if (Session["userRight"].ToString() == "Branch")
                {
                    ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_expense_search_all_store, "0"));
                }

                // User wise filter
                if (Session["userRight"].ToString() == "Branch")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE (userRight='Regular' OR roleId='" + Session["roleId"] + "') AND active='1' " + Session["storeAccessParameters"] + "", "title", "roleId");
                    ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_expense_search_all_user, "0"));
                }
                else if (Session["userRight"].ToString() == "Regular")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE userRight='Regular' AND active='1' AND roleId='" + Session["roleId"] + "'", "title", "roleId");
                    divUserList.Visible = false;
                    ddlStoreList.SelectedValue = Session["storeId"].ToString();
                }

                txtExpenseDate.Text = txtSearchDateFrom.Text = txtSearchDateTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");

                
                dsExpense.ID = "dsExpense";
                this.Page.Controls.Add(dsExpense);
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
                var ds = objSql.getDataSet(query);
                cashInHandTotal = Convert.ToDecimal(ds.Tables[0].Rows[0][5].ToString());
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cashInTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][3].ToString());
                    cashOutTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][4].ToString());
                }

                //lblTotalExpense.Text = "Total Expense: " + cashOutTotal.ToString();

                lblTotalExpense.Text = Resources.Language.Lbl_expense_total_expense+ " : " + cashOutTotal.ToString();
            }
            catch
            {
                cashInTotal = 0M;
                cashOutTotal = 0M;
                cashInHandTotal = 0M;
            }


            
            var contr = GlobalVariable.getConnectionStringName();
            dsExpense.ConnectionString = ConfigurationManager.ConnectionStrings[contr].ConnectionString;
            dsExpense.SelectCommand = query;
            //grdCashReportInfo.PageIndex = 0;
            grdCashReportInfo.DataSource = dsExpense;
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


            //query = "SELECT cat.Id, cat.cashType, cash.Id, cash.cashType, cash.descr, cash.cashIn, cash.cashout, cash.cashInHand, cash.entryDate, cash.billNo, cash.mainDescr, cash.roleID, cash.branchId, cash.groupId FROM CashCatInfo AS cat INNER JOIN CashReportInfo AS cash ON cat.cashType = cash.cashType WHERE (cash.cashType = '" + ddlCashSearchType.Text + "' OR '" + ddlCashSearchType.Text + "' = 'Search All') AND (cash.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  AND (cash.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') AND (cash.roleId = '" + Session["roleId"] + "' OR cash.branchId = '" + Session["roleId"] + "' OR cash.groupId = '" + Session["roleId"] + "') ORDER BY cash.Id DESC";

            //"WHERE (tbl.status = '2' OR tbl.status = '6') " +
            query = "SELECT tbl.Id,tbl.cashType,tbl.descr,tbl.cashIn,tbl.cashout,tbl.cashInHand,tbl.entryDate,tbl.billNo,tbl.mainDescr,tbl.roleID,tbl.status,tbl.adjust,tbl.branchId,tbl.groupId, cat.Id, cat.cashType AS Particular, stock.prodCode,warehouse.name as storeName FROM CashReportInfo AS tbl " +
                    "LEFT JOIN cashCatInfo cat ON tbl.descr = CAST(cat.Id AS nvarchar) " +
                    "LEFT JOIN StockInfo AS stock ON tbl.descr = stock.prodCode " +
                    "LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id=tbl.storeId " +
                    "WHERE (tbl.status = '2') " +
                    "AND ((tbl.descr = '" + ddlCashSearchType.SelectedValue + "' OR cat.cashType = '" + ddlCashSearchType.SelectedValue + "') OR '" + ddlCashSearchType.SelectedValue + "' = '0') " +
                    "AND (tbl.storeId = '" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') " +
                    "AND (tbl.roleId = '" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "'='0') " +
                    "AND (tbl.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  " +
                    "AND (tbl.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') " + commonFunction.getUserAccessParameters("tbl") +
                    " ORDER BY tbl.Id DESC, tbl.entryDate ASC";

            refreshGrd(query);
        }





        private void reset()
        {
            txtCashInAmount.Text = "";
            txtCashOutAmount.Text = "";
            txtExpenseDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
            ddlCashOutTypeSup.SelectedIndex = 0;
            txtCashOutMainDescr.Text = "";
        }





        private void supplierStatusTableValue()
        {
            commonFunction.cashDueAndBalanceValue(ddlCashOutTypeSup.Text);
            DataAccess.CommonFunction.cashDue -= Convert.ToDecimal(txtCashOutAmount.Text);
            query =
                "INSERT INTO [SupplierStatus] (   supCompany,            billNo, status,              payment,                                cashDue,                                       balance,                                  entryDate                 ) VALUES ('" +
                ddlCashOutTypeSup.Text + "', '-1', 'cashOut', '" + txtCashOutAmount.Text + "', '" +
                DataAccess.CommonFunction.cashDue + "', '" + DataAccess.CommonFunction.balance + "', '" +
                commonFunction.GetCurrentTime().ToShortDateString() + "')";
            objSql.executeQuery(query);
        }





        //<-- Function 

        //--> Dropdownlist   
        protected void ddlCashOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divCashOutTypeSup.Visible = false;
            //divCashOutTypeStaff.Visible = false;
            //if (ddlCashOutType.Text == "Supplier")
            //    divCashOutTypeSup.Visible = true;
            //else if (ddlCashOutType.Text == "Staff")
            //    divCashOutTypeStaff.Visible = true;
        }





        protected void ddlCashInType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divCashInTypeSup.Visible = false;

            //if (ddlCashInType.Text == "Supplier")
            //    divCashInTypeSup.Visible = true;
            //else if (ddlCashInType.Text == "Staff")
            //    divCashInTypeStaff.Visible = true;
        }





        protected void ddlCashSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divSearchSup.Visible = false;
            //divSearchStaff.Visible = false;

            //if (ddlCashSearchType.Text == "Supplier")
            //    divSearchSup.Visible = true;
            //else if (ddlCashSearchType.Text == "Staff")
            //    divSearchStaff.Visible = true;

            btnCashSearch_Click(null, null);
        }





        //<-- Dropdownlist

        //--> Gridview
        protected void grdCashReportInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var ds = objSql.getDataSet("SELECT * FROM [CashReportInfo] ORDER BY ID DESC");

            if (((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblId") as Label).Text !=
                ds.Tables[0].Rows[0][0].ToString())
            {
                e.Cancel = true;
                scriptMessage("You can Delete Only Last Transection.", MessageType.Warning);
            }
            else if (((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblCashType") as Label).Text == "Sales")
            {
                e.Cancel = true;
                scriptMessage("You can't Delete Sales Transection.", MessageType.Warning);
            }

            ////--> cashDue and Balance setting
            //decimal cashOut = 0;
            //cashOut = -Convert.ToDecimal(((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblCashOut") as Label).Text);
            //commonFunction.cashDueAndBalanceValue(((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblDescr") as Label).Text);
            //DataAccess.CommonFunction.cashDue -= cashOut;
            //query = "INSERT INTO [SupplierStatus] (                             supCompany,                                        billNo,   status,        payment,                        cashDue,                                      balance,                                  entryDate                 ) VALUES ('" +
            //                                     ((grdCashReportInfo.Rows[e.RowIndex]).FindControl("lblDescr") as Label).Text + "', '-1',   'cashOut', '" + cashOut + "', '" + DataAccess.CommonFunction.cashDue + "', '" + DataAccess.CommonFunction.balance + "', '" + commonFunction.GetCurrentTime().ToShortDateString() + "')";
            //objSql.executeQuery(query);
            ////<-- cashDue and Balance setting
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
                ((Label)e.Row.FindControl("lblCashOutFooter")).Text = cashOutTotal.ToString();
                //((Label)e.Row.FindControl("lblCashInHand")).Text = cashInHandTotal.ToString();
            }
        }





        //<-- Gridview

        //--> Button
        protected void btnCashInAdd_Click(object sender, EventArgs e)
        {
            if (ddlCashInType.Text == "Select Particular")
            {
                scriptMessage("Select must a Particular!", MessageType.Warning);
                return;
            }

            if (txtCashInAmount.Text == "")
            {
                scriptMessage("Cash-In Amount Requird!", MessageType.Warning);
                return;
            }

            if (Convert.ToDecimal(txtCashInAmount.Text) == 0)
            {
                scriptMessage("0 amount is not possible!", MessageType.Warning);
                return;
            }



            if (ddlCashInType.Text == "Supplier")
                descr = ddlCashInTypeSup.Text;
            else if (ddlCashInType.Text == "Staff")
                descr = ddlCashInTypeStaff.Text;
            else
                descr = "";

            scriptMessage(
                commonFunction.cashTransaction(Convert.ToDecimal(txtCashInAmount.Text), 0, ddlCashInType.Text, descr, "",
                    txtCashInMainDescr.Text, "2", "0"), MessageType.Success);
            searchResult();
            reset();
        }





        protected void btnCashOutAdd_Click(object sender, EventArgs e)
        {
            if (ddlCashOutTypeSup.Text == "Select Particular")
            {
                scriptMessage("Select must a Particular!", MessageType.Warning);
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
            if (String.IsNullOrEmpty(txtExpenseDate.Text))
                txtExpenseDate.Text = commonFunction.GetCurrentTime().ToString();


            scriptMessage(
                commonFunction.cashTransactionWithDate(0, Convert.ToDecimal(txtCashOutAmount.Text), "Expense",
                    ddlCashOutTypeSup.Text, "", txtCashOutMainDescr.Text, "2", "0", txtExpenseDate.Text), MessageType.Success);

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
                Session["reportName"] = "Expense";
                Session["Type"] = "0";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }





        protected void btnAddPaticular_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Particular"))
            {
                var obj = new DataAccess.CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification",
                    "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Particular");
            }
        }





        protected void ddlStoreList_OnSelectedIndexChangeded(object sender, EventArgs e)
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


    }


}
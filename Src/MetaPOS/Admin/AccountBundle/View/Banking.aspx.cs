using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AccountBundle.View
{


    public partial class Banking : BasePage
    {


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        SqlDataSource dsCashReportBanking = new SqlDataSource();
        private DataSet ds;

        private static string query = "";//, descr = "";
        private int i;
        private static decimal cashInTotal, cashOutTotal, cashInHandTotal;


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
                if (!commonFunction.accessChecker("Banking"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }

                //DataSet
                dsCashReportBanking.ID = "dsStock";
                this.Page.Controls.Add(dsCashReportBanking);

                commonFunction.fillAllDdl(ddlBankSearch,
                    "SELECT DISTINCT bankName, Id FROM [BankNameInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY bankName ASC", "bankName", "Id");

                commonFunction.fillAllDdl(ddlBankCashOut, "SELECT [Id], [bankName] FROM [BankNameInfo] WHERE active = '1'" + Session["userAccessParameters"] + " ORDER BY bankName ASC", "bankName", "Id");

                commonFunction.fillAllDdl(ddlBankCashIn, "SELECT [Id], [bankName] FROM [BankNameInfo] WHERE active = '1'" + Session["userAccessParameters"] + " ORDER BY bankName ASC", "bankName", "Id");

                //ddlCashInType.Items.Insert(0, "Select Particular");
                ddlCashInType.Items.Insert(0, "Bank Deposit");

                //ddlCashOutType.Items.Insert(0, "Select Particular");
                ddlCashOutType.Items.Insert(0, "Bank Withdraw" + "");

                ddlCashSearchType.Items.Insert(0, new ListItem(Resources.Language.Lbl_banking_search_all, "0"));
                ddlCashSearchType.Items.Insert(1, "Bank Deposit");
                ddlCashSearchType.Items.Insert(2, "Bank Withdraw");

                ddlBankCashIn.Items.Insert(0, Resources.Language.Lbl_banking_select_a_bank);
                ddlBankCashOut.Items.Insert(0, Resources.Language.Lbl_banking_select_a_bank);

                ddlBankSearch.Items.Insert(0, new ListItem(Resources.Language.Lbl_banking_select_a_bank, "0"));

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
                if (Session["userRight"].ToString() == "Branch")
                {
                    ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_banking_search_all_store, "0"));
                }

                // User wise filter
                if (Session["userRight"].ToString() == "Branch")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE (userRight='Regular' OR roleId='" + Session["roleId"] + "') AND active='1' " + Session["storeAccessParameters"] + "", "title", "roleId");
                    ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_banking_search_all_user, "0"));
                }
                else if (Session["userRight"].ToString() == "Regular")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE userRight='Regular' AND active='1' AND roleId='" + Session["roleId"] + "'", "title", "roleId");
                    divUserList.Visible = false;
                    ddlStoreList.SelectedValue = Session["storeId"].ToString();
                }
                txtSearchDateFrom.Text = txtSearchDateTo.Text = txtWithdrawDate.Text = txtDipositDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
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

                lblTotalBanking.Text = Resources.Language.Lbl_banking_total_balance + " : " + (cashInTotal - cashOutTotal).ToString();

                //lblTotalBanking.Text = "Total Balance: " + (cashInTotal - cashOutTotal).ToString();

            }
            catch
            {
                cashInTotal = 0M;
                cashOutTotal = 0M;
                cashInHandTotal = 0M;
            }
            
            var constr = GlobalVariable.getConnectionStringName();
            dsCashReportBanking.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsCashReportBanking.SelectCommand = query;
            grdCashReportInfo.DataSource = dsCashReportBanking;
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


            query = "SELECT tbl.Id,tbl.CashType,tbl.descr,tbl.cashIn,tbl.cashOut,tbl.cashInHand,tbl.entryDate,tbl.billNo,tbl.mainDescr,tbl.roleID, b.Id, b.bankName AS Particular,warehouse.name as storeName FROM CashReportInfo AS tbl " +
                    "JOIN BankNameInfo AS b ON tbl.descr = CAST(b.Id AS NVARCHAR(50)) " +
                    "LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id=tbl.storeId " +
                    "WHERE tbl.Status='4'AND ([cashType] = '" + ddlCashSearchType.Text + "' OR '" + ddlCashSearchType.Text + "' = '0') " +//Search All
                    "AND (tbl.storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0')" +
                    "AND (tbl.roleId='" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "'='0')" +
                    "AND (tbl.descr = '" + ddlBankSearch.SelectedValue + "' OR '" + ddlBankSearch.SelectedValue + "' = '0') " +//Select a Bank
                    "AND (tbl.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  " +
                    "AND (tbl.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') " + commonFunction.getUserAccessParameters("tbl") +
                " ORDER BY tbl.Id DESC, tbl.entryDate ASC";

            refreshGrd(query);
        }





        private void reset()
        {
            txtCashInAmount.Text = "";
            txtCashOutAmount.Text = "";
            txtCashOutMainDescr.Text = txtCashInMainDescr.Text = "";
            ddlBankCashOut.SelectedIndex = ddlBankCashIn.SelectedIndex = 0;
            txtWithdrawDate.Text = txtDipositDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
        }





        private void supplierStatusTableValue()
        {
            commonFunction.cashDueAndBalanceValue(ddlCashOutTypeSup.Text);
            DataAccess.CommonFunction.cashDue -= Convert.ToDecimal(txtCashOutAmount.Text);
            query =
                "INSERT INTO [SupplierStatus] (   supCompany,billNo, status,payment,cashDue,balance,entryDate) VALUES ('" +
                ddlCashOutTypeSup.Text + "', '-1', 'cashOut', '" + txtCashOutAmount.Text + "', '" +
                DataAccess.CommonFunction.cashDue + "', '" + DataAccess.CommonFunction.balance + "', '" +
                commonFunction.GetCurrentTime().ToShortDateString() + "')";
            objSql.executeQuery(query);
        }





        //<-- Function 

        //--> Dropdownlist   
        protected void ddlCashOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            divCashOutTypeSup.Visible = false;
            divCashOutTypeStaff.Visible = false;
            if (ddlCashOutType.Text == "Supplier")
                divCashOutTypeSup.Visible = true;
            else if (ddlCashOutType.Text == "Staff")
                divCashOutTypeStaff.Visible = true;
        }





        protected void ddlCashInType_SelectedIndexChanged(object sender, EventArgs e)
        {
            divCashInTypeSup.Visible = false;

            if (ddlCashInType.Text == "Supplier")
                divCashInTypeSup.Visible = true;
            else if (ddlCashInType.Text == "Staff")
                divCashInTypeStaff.Visible = true;
        }





        protected void ddlCashSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();

            divSearchSup.Visible = false;
            divSearchStaff.Visible = false;

            //if (ddlcashsearchtype.text == "bank deposit")
            //    divsearchsup.visible = true;
            //else if (ddlcashsearchtype.text == "bank withdraw")
            //    divsearchstaff.visible = true;

            //btncashsearch_click(null, null);
        }





        //<-- Dropdownlist

        //--> Gridview
        protected void grdCashReportInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ds = objSql.getDataSet("SELECT * FROM [CashReportInfo] ORDER BY ID DESC");
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
                ((Label)e.Row.FindControl("lblCashOut")).Text = cashOutTotal.ToString();
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

            if (ddlBankCashIn.Text == "Select a Bank")
            {
                scriptMessage("Select must a Bank Name!", MessageType.Warning);
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

            scriptMessage(
                commonFunction.cashTransactionWithDate(0, Convert.ToDecimal(txtCashInAmount.Text), ddlCashInType.Text,
                    ddlBankCashIn.SelectedValue, "", txtCashInMainDescr.Text, "4", "0", txtDipositDate.Text), MessageType.Success);
            searchResult();
            reset();
        }





        protected void btnCashOutAdd_Click(object sender, EventArgs e)
        {
            if (ddlCashOutType.Text == "Select Particular")
            {
                scriptMessage("Select must a Particular!", MessageType.Warning);
                return;
            }

            if (ddlBankCashOut.Text == "Select a Bank")
            {
                scriptMessage("Select must a Bank Name!", MessageType.Warning);
                return;
            }

            if (txtCashOutAmount.Text == "")
            {
                scriptMessage("Bank withdrow Amount is Requird!", MessageType.Warning);
                return;
            }

            if (Convert.ToDecimal(txtCashOutAmount.Text) == 0)
            {
                scriptMessage("Enter amount must be more than ZERO!", MessageType.Warning);
                return;
            }

            //descr = ddlBankCashOut.SelectedValue;

            //if (ddlCashOutType.Text == "Supplier")
            //{
            //    descr = ddlCashOutTypeSup.Text;
            //    supplierStatusTableValue();
            //}
            //else if (ddlCashOutType.Text == "Staff")
            //    descr = ddlCashOutTypeStaff.Text;
            //else
            //    descr = "";

            scriptMessage(
                commonFunction.cashTransactionWithDate(Convert.ToDecimal(txtCashOutAmount.Text), 0, ddlCashOutType.Text,
                    ddlBankCashOut.SelectedValue, "", txtCashOutMainDescr.Text, "4", "0", txtWithdrawDate.Text), MessageType.Success);
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
                Session["pageName"] = "CashReport";
                Session["reportName"] = "Banking Report";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }





        protected void btnAddBank_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Bank"))
            {
                var obj = new DataAccess.CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Notification", "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Bank");
            }
        }





        protected void ddlBankSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
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





        protected void ddlStoreList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
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
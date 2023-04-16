using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AnalyticBundle.View
{


    public partial class Transaction :BasePage //Page
    {


        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction commonFunction = new DataAccess.CommonFunction();
        private DataSet ds;

        private static string query = "", descr = "";
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
                if (!commonFunction.accessChecker("Transaction"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
                commonFunction.fillAllDdl(ddlCashInType, "SELECT DISTINCT cashType FROM [CashCatInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY cashType ASC", "cashType", "cashType");

                commonFunction.fillAllDdl(ddlCashOutType, "SELECT DISTINCT cashType FROM [CashCatInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY cashType ASC", "cashType", "cashType");

                commonFunction.fillAllDdl(ddlCashOutTypeSup,
                    "SELECT DISTINCT supCompany FROM [SupplierInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY supCompany ASC", "supCompany", "supCompany");

                commonFunction.fillAllDdl(ddlCashOutTypeStaff, "SELECT DISTINCT name FROM [StaffInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY name ASC", "name", "name");

                //commonFunction.fillAllDdl(ddlCashSearchType, "SELECT DISTINCT cashType FROM [CashCatInfo] WHERE active='1'", "cashType");
                commonFunction.fillAllDdl(ddlSearchSup, "SELECT DISTINCT supCompany FROM [SupplierInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY supCompany ASC", "supCompany", "supCompany");

                commonFunction.fillAllDdl(ddlSearchStaff, "SELECT DISTINCT name FROM [StaffInfo] WHERE active='1'" + Session["userAccessParameters"] + " ORDER BY name ASC", "name", "name");

                ddlCashInType.Items.Insert(0, "Select Particular");
                ddlCashInType.Items.Insert(1, "Bank Deposit");

                ddlCashOutType.Items.Insert(0, "Select Particular");
                ddlCashOutType.Items.Insert(1, "Bank Withdrawl");

                //ddlCashSearchType.Items.Insert(, "Search All");
                //ddlCashSearchType.Items.Insert(0, "Purchase");
                //ddlCashSearchType.Items.Insert(1, "Salary");
                //ddlCashSearchType.Items.Insert(2, "Expense");
                //ddlCashSearchType.Items.Insert(3, "Receive");
                //ddlCashSearchType.Items.Insert(4, "Banking");
                //ddlCashSearchType.Items.Insert(5, "Sales");

                txtSearchDateFrom.Text = txtSearchDateTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");

                DataVisibilityControl();
                searchResult();
            }

            
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
                lblTotalDueBalance.Text = Resources.Language.Lbl_transaction_total_balance + ": " + (cashInTotal - cashOutTotal).ToString();
            }
            catch
            {
                cashInTotal = 0M;
                cashOutTotal = 0M;
                cashInHandTotal = 0M;
            }

            SqlDataSource dsCashReport = new SqlDataSource();
            dsCashReport.ID = "dsCashReport";
            this.Page.Controls.Add(dsCashReport);
            var constr = GlobalVariable.getConnectionStringName();
            dsCashReport.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsCashReport.SelectCommand = query;
            grdCashReportInfo.DataSource = dsCashReport;
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

            query = "SELECT DISTINCT tbl.Id,tbl.CashType,tbl.descr,tbl.cashIn,tbl.cashOut,tbl.cashInHand,tbl.entryDate,tbl.billNo,tbl.mainDescr," 
                    + "tbl.roleID,staff.staffID,cat.Id, sup.supID, bank.Id, warehouse.name as storeName, " +
                    "CONCAT(staff.name,sale.billNo,cat.cashType,sup.supCompany,bank.bankName,cus.name,stock.prodName) AS Particular FROM CashReportInfo AS tbl "
                    + " LEFT JOIN staffInfo AS staff ON (tbl.descr = CAST(staff.staffID AS NVARCHAR(50)) AND tbl.status = '1') "
                    + " LEFT JOIN CashCatInfo AS cat ON (tbl.descr = CAST(cat.Id AS NVARCHAR(50)) AND (tbl.status = '2' OR tbl.status = '3')) "
                    + " LEFT JOIN SupplierInfo AS sup ON (tbl.descr = CAST(sup.supID AS NVARCHAR(50)) AND tbl.status = '0' ) "
                    + " LEFT JOIN BankNameInfo AS bank ON (tbl.descr = CAST(bank.Id AS NVARCHAR(50)) AND tbl.status = '4') "
                    + " LEFT JOIN SaleInfo AS sale ON (tbl.descr = CAST(sale.billNo AS NVARCHAR(50)) AND tbl.status = '5') "
                    + " LEFT JOIN CustomerInfo AS cus ON (tbl.descr = CAST(cus.cusID AS NVARCHAR(50)) ) "
                    + " LEFT JOIN StockInfo AS stock ON (tbl.descr = CAST(stock.prodID AS NVARCHAR(50)) AND tbl.status = '0') "
                    + " LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = tbl.storeId "
                    + " WHERE  (tbl.Status = '" + ddlCashSearchType.SelectedValue + "' OR '" + ddlCashSearchType.SelectedValue + "' = 'SearchAll') "
                    + "AND (tbl.storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "' = '0') "
                    + "AND (tbl.roleId = '" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "' = '0')"
                    + "AND (CONCAT(staff.name,sale.billNo,cat.cashType,sup.supCompany,bank.bankName,cus.name,stock.prodName) LIKE '%" + txtSearch.Text.Trim() + "%' OR '" + txtSearch.Text.Trim() + "' = '')"
                    + "AND (tbl.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  "
                    + "AND (tbl.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') "
                    //+ commonFunction.getUserAccessParameters("tbl") + " "
                    + commonFunction.getStoreAccessParameters("tbl") + " "
                    + "AND (tbl.adjust='0') AND tbl.isScheduled ='0' AND tbl.isReceived = '1' "
                    + "AND (tbl.status != '6') AND (tbl.status != '8') "
                    + "ORDER BY tbl.Id DESC, tbl.entryDate ASC";

            refreshGrd(query);
        }





        private void reset()
        {
            txtCashInAmount.Text = "";
            txtCashOutAmount.Text = "";
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
            searchResult();

            //divSearchSup.Visible = false;
            //divSearchStaff.Visible = false;

            ////if (ddlCashSearchType.Text == "Supplier")
            ////    divSearchSup.Visible = true;
            ////else if (ddlCashSearchType.Text == "Staff")
            ////    divSearchStaff.Visible = true;

            //btnCashSearch_Click(null, null);
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell1 = e.Row.Cells[0];
                TableCell statusCell2 = e.Row.Cells[1];
                Label lblPayMethod = ((Label)e.Row.FindControl("lblDescr"));

                TableCell statusCell = e.Row.Cells[3];
                if (lblPayMethod.Text == "0")
                {
                    lblPayMethod.Text = "Cash";
                    lblPayMethod.Visible = true;
                }
                if (lblPayMethod.Text == "1")
                {
                    lblPayMethod.Text = "Card";
                    lblPayMethod.Visible = true;
                }
                if (lblPayMethod.Text == "2")
                {
                    lblPayMethod.Text = "Check";
                    lblPayMethod.Visible = true;
                }
                if (lblPayMethod.Text == "3")
                {
                    lblPayMethod.Text = "bKash";
                    lblPayMethod.Visible = true;
                }

                if (lblPayMethod.Text == "4")
                {
                    lblPayMethod.Text = "Bank Deposit";
                    lblPayMethod.Visible = true;
                }

                if (lblPayMethod.Text == "5")
                {
                    lblPayMethod.Text = "Cash On Delivery";
                    lblPayMethod.Visible = true;
                }
                else if (lblPayMethod.Text == "6")
                {
                    lblPayMethod.Text = "Credit";
                    lblPayMethod.Visible = true;
                }

                Label lblParticular = ((Label)e.Row.FindControl("lblDescr"));
                if (lblParticular.Text == "")
                    lblPayMethod.Visible = true;
                else
                    lblPayMethod.Visible = false;
            }

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
                    txtCashInMainDescr.Text, "0", "0"), MessageType.Success);
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


            if (ddlCashOutType.Text == "Supplier")
            {
                descr = ddlCashOutTypeSup.Text;
                supplierStatusTableValue();
            }
            else if (ddlCashOutType.Text == "Staff")
                descr = ddlCashOutTypeStaff.Text;
            else
                descr = "";

            scriptMessage(
                commonFunction.cashTransaction(0, Convert.ToDecimal(txtCashOutAmount.Text), ddlCashOutType.Text, descr, "",
                    txtCashOutMainDescr.Text, "0", "0"), MessageType.Success);
            searchResult();
            reset();
        }





        protected void btnCashSearch_Click(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (grdCashReportInfo.Rows.Count <= 0)
            {
                scriptMessage("There are no data records to print", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "CashReport";
                Session["reportName"] = "Transcrtion Report";
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
                Response.Redirect("Particular");
            }
        }





        protected void grdCashReportInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }





        protected void ddlStoreList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            getUserListByStore();
            searchResult();
        }




        private void DataVisibilityControl()
        {
            if (Session["userRight"].ToString() == "Branch")
            {
                ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_transaction_search_all_store, "0"));

                getUserListByStore();
            }
            else if (Session["userRight"].ToString() == "Regular")
            {
                ddlStoreList.Visible = false;
                ddlUserList.Visible = false;

                ddlStoreList.SelectedValue = Session["storeId"].ToString();

                // Gridview 
                List<DataControlField> columns = grdCashReportInfo.Columns.Cast<DataControlField>().ToList();
                columns.Find(col => col.SortExpression == "storeName").Visible = false;

                commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE userRight='Regular' AND active='1' AND roleId='" + Session["roleId"] + "'", "title", "roleId");
                
            }
        }





        protected void ddlUserList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
        }



        private void getUserListByStore()
        {
            commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE active='1' AND (storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') AND storeId !='0' " + Session["storeAccessParameters"] + " ", "title", "roleId");
            ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_transaction_search_all_user, "0"));
        }








        protected void grdCashReportInfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCashReportInfo.PageIndex = e.NewPageIndex;
            searchResult();
        }


    }


}
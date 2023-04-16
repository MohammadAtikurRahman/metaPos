using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AccountBundle.View
{


    public partial class Receive : BasePage//Page
    {


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        SqlDataSource dsCashReportRecive = new SqlDataSource();
        private DataSet ds;

        private static string query = "", descr = "";
        private int i;
        private static decimal cashInTotal, trackAmtTotal, cashInHandTotal;


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
                if (!commonFunction.accessChecker("Receive"))
                {
                    commonFunction.pageout();
                }
                commonFunction.fillAllDdl(ddlCashInTypeSup,
                    "SELECT DISTINCT supCompany,supID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC", "supCompany", "supID");

                commonFunction.fillAllDdl(ddlCashOutType,
                    "SELECT DISTINCT supCompany,supID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC", "supCompany", "supID");

                commonFunction.fillAllDdl(ddlCashSearchType,
                    "SELECT DISTINCT supCompany,supID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC", "supCompany", "supID");

                //ddlCashInType.Items.Insert(0, "Select Particular");
                //ddlCashInType.Items.Insert(1, "Bank Deposit");

                ddlCashOutType.Items.Insert(0, Resources.Language.Lbl_receive_select_supplier);
                ddlCashInTypeSup.Items.Insert(0, Resources.Language.Lbl_receive_select_supplier);
                ddlCashSearchType.Items.Insert(0, Resources.Language.Lbl_receive_search_all);

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
                if (Session["userRight"].ToString() == "Branch")
                {
                    ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_receive_search_all_store, "0"));
                }

                // User wise filter
                if (Session["userRight"].ToString() == "Branch")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE (userRight='Regular' OR roleId='" + Session["roleId"] + "') AND active='1' " + Session["storeAccessParameters"] + "", "title", "roleId");
                    ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_receive_search_all_user, "0"));
                }
                else if (Session["userRight"].ToString() == "Regular")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE userRight='Regular' AND active='1' AND roleId='" + Session["roleId"] + "'", "title", "roleId");
                    divUserList.Visible = false;

                    ddlStoreList.SelectedValue = Session["storeId"].ToString();
                }

                //DataSet
                
                dsCashReportRecive.ID = "dsCashReportRecive";
                this.Page.Controls.Add(dsCashReportRecive);


                txtSearchDateFrom.Text = txtSearchDateTo.Text = txtRecivedDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
            }

            searchResult();
            DataVisibilityControl();


            //if (commonFunction.findSettingItemValueDataTable("language") == "bn")
            //{
            //    chkAnyDate.Text = "সমস্ত ট্র্যাক প্রাপ্ত পরিমাণ দেখান";
            //}
            //else
            //{
            //    chkAnyDate.Text = "Show all track received amount";
            //}


        }







        private void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void refreshGrd(string query)
        {
            cashInTotal = 0M;
            trackAmtTotal = 0M;
            cashInHandTotal = 0M;
            try
            {
                ds = objSql.getDataSet(query);
                cashInHandTotal = Convert.ToDecimal(ds.Tables[0].Rows[0][5].ToString());
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cashInTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][3].ToString());
                    trackAmtTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][4].ToString());
                }

                decimal futureReceived = (trackAmtTotal - cashInTotal);
                lblTotalReceive.Text = Resources.Language.Lbl_receive_total_received_amount + " : " + cashInTotal;  //"Total Received Amount: " + cashInTotal;
                lblTrackAmt.Text = Resources.Language.Lbl_receive_total_track_received_amount + " : " + trackAmtTotal;    //"Total Track Received Amount: " + trackAmtTotal;
                lblBalance.Text = Resources.Language.Lbl_receive_total_balance + " : " + futureReceived;    //"Total Balance: " + futureReceived;
            }
            catch
            {
                cashInTotal = 0M;
                trackAmtTotal = 0M;
                cashInHandTotal = 0M;
            }


            var constr = GlobalVariable.getConnectionStringName();
            dsCashReportRecive.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsCashReportRecive.SelectCommand = query;
            grdCashReportInfo.DataSource = dsCashReportRecive;
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


            if (chkAnyDate.Checked)
            {
                searchFrom = Convert.ToDateTime("01-jan-2000");
                searchTo = Convert.ToDateTime("01-jan-2090");
            }

            query = "SELECT tbl.Id, tbl.cashType,tbl.descr,tbl.cashIn, tbl.trackAmt,tbl.cashout,tbl.cashInHand,tbl.entryDate,tbl.billNo,tbl.mainDescr,tbl.roleID,tbl.status,tbl.adjust,tbl.branchId,tbl.groupId, sup.supId, sup.supCompany AS Particular, cus.cusID AS CustomerID,cus.name AS CustomerName, warehouse.name as storeName FROM CashReportInfo AS tbl " +
                    "LEFT JOIN supplierInfo sup ON tbl.descr = CAST(sup.supID as nvarchar) " +
                    "LEFT JOIN CustomerInfo AS cus ON tbl.descr = CAST(cus.cusID as nvarchar) " +
                    "LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id=tbl.storeId " +
                    "WHERE " +
                    "(tbl.status = '3') " +
                    "AND ((tbl.descr = '" + ddlCashSearchType.Text + "' OR sup.supId = '" + ddlCashSearchType.Text + "') " + "OR '" + ddlCashSearchType.Text + "' = '" + Resources.Language.Lbl_receive_search_all + "') " +
                    "AND (tbl.storeId = '" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "' ='0') " +
                    "AND (tbl.roleId = '" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "' ='0') " +
                    "AND (tbl.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  " +
                    "AND (tbl.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') " +
                    commonFunction.getUserAccessParameters("tbl") +
                " ORDER BY tbl.Id DESC, tbl.entryDate ASC";

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
                "INSERT INTO [SupplierStatus] ( supCompany, billNo,   status,        payment,                        cashDue, balance, entryDate) VALUES ('" +
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
                ((Label)e.Row.FindControl("lblTotalCashIn")).Text = cashInTotal.ToString();
                ((Label)e.Row.FindControl("lblTotalTrackAmt")).Text = trackAmtTotal.ToString();
                //((Label)e.Row.FindControl("lblCashInHand")).Text = cashInHandTotal.ToString();
            }
        }





        protected void btnCashInAdd_Click(object sender, EventArgs e)
        {
            if (ddlCashInTypeSup.Text == "Select Supplier")
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

            if (string.IsNullOrEmpty(txtRecivedDate.Text))
                txtRecivedDate.Text = commonFunction.GetCurrentTime().ToString();

            scriptMessage(
                commonFunction.cashTransactionWithDate(Convert.ToDecimal(txtCashInAmount.Text), 0, "Receive",
                    ddlCashInTypeSup.Text, "", txtCashInMainDescr.Text, "3", "0",txtRecivedDate.Text), MessageType.Success);
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
                scriptMessage("Cash-Out Amount Required!", MessageType.Warning);
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
                commonFunction.cashTransactionFormPurchase(0, Convert.ToDecimal(txtCashOutAmount.Text), ddlCashOutType.Text, descr, "",
                    txtCashOutMainDescr.Text, "3", "0", "", commonFunction.GetCurrentTime().ToString(), false, true, 0), MessageType.Success);
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
                scriptMessage("There is no data records to Print.", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "TransReport";
                Session["reportName"] = "Received";
                Session["Type"] = "1";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }





        protected void btnAddPaticular_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Supplier"))
            {
                DataAccess.CommonFunction obj = new DataAccess.CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification",
                    "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Supplier");
            }
        }





        protected void chkAnyDate_OnCheckedChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void ddlStoreList_OnSelectedIndexChanged(object sender, EventArgs e)
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
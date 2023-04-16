using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.DataAccess;
using System.Collections.Generic;


namespace MetaPOS.Admin.AccountBundle.View
{


    public partial class Purchase : BasePage
    {


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        SqlDataSource dsCashReport = new SqlDataSource();

        private static string query = "";
        private static decimal cashInTotal, cashOutTotal, cashInHandTotal;
        public decimal total = 0, bPrice = 0;


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
                if (!commonFunction.accessChecker("Supply"))
                {
                    commonFunction.pageout();
                }

                commonFunction.fillAllDdl(ddlCashOutTypeSup,
                    "SELECT DISTINCT supCompany,supID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC",
                    "supCompany", "supID");
                commonFunction.fillAllDdl(ddlCashInTypeSup,
                    "SELECT DISTINCT supCompany,supID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC",
                    "supCompany", "supID");
                commonFunction.fillAllDdl(ddlSearchSup,
                    "SELECT DISTINCT supCompany,supID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC",
                    "supCompany", "supID");

                ddlCashInTypeSup.Items.Insert(0, Resources.Language.Lbl_supply_select_supplier);
                ddlCashOutTypeSup.Items.Insert(0, Resources.Language.Lbl_supply_select_supplier);
                ddlSearchSup.Items.Insert(0, new ListItem(Resources.Language.Lbl_supply_search_all, "0"));


                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
                if (Session["userRight"].ToString() == "Branch")
                {
                    ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_supply_search_all_store, "0"));
                }

                // User wise filter
                if (Session["userRight"].ToString() == "Branch")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE (userRight='Regular' OR roleId='" + Session["roleId"] + "') AND active='1' " + Session["storeAccessParameters"] + "", "title", "roleId");
                    ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_supply_search_all_store, "0"));
                }
                else if (Session["userRight"].ToString() == "Regular")
                {
                    commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE userRight='Regular' AND active='1' AND roleId='" + Session["roleId"] + "'", "title", "roleId");
                    divUserList.Visible = false;
                    ddlStoreList.SelectedValue = Session["storeId"].ToString();
                }

                txtSearchDateFrom.Text = txtSearchDateTo.Text = txtOpeinginDueDate.Text = txtGivePaymentDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");


                //if (commonFunction.findSettingItemValueDataTable("language") == "bn")
                //{
                //    //chkAnyDate.Text = "যেকোন তারিখ";
                //    //chkSchedule.Text = "শিডিউল";
                //    chkAnyDate.Text = Resources.Language.Lbl_supply_schedule;
                //    chkSchedule.Text = Resources.Language.Lbl_supply_any_date;
                //}
                //else
                //{
                //    chkAnyDate.Text = "Any Date";
                //    chkSchedule.Text = "Schedule";
                //}




                var message = Request["msg"];
                if (message == "warning")
                    scriptMessage("Sorry! Operation Failed.", MessageType.Warning);
                else if (message == "success")
                    scriptMessage("Operation successful.", MessageType.Success);
                else if (message == "error")
                    scriptMessage("Please enter amount equal or under due amount", MessageType.Warning);
                else if (message == "less")
                    scriptMessage("You can not pay ZERO or less then ZERO.", MessageType.Warning);
                else if (message == "big")
                    scriptMessage("You can not pay more than schedule payment amount.", MessageType.Warning);
            }

            if (chkAnyDate.Checked)
            {
                lblTotalDueBalance.Visible = true;
                lblTotalStock.Visible = true;
            }
            else
            {
                lblTotalDueBalance.Visible = false;
                lblTotalStock.Visible = false;
            }

            //PurchaseBalance();
            searchResult();
            DataVisibilityControl();
        }





        private void PurchaseBalance()
        {
            query = "SELECT * FROM [StockInfo] WHERE ([supCompany] = IsNull('" + ddlSearchSup.Text + "', supCompany) OR '" + ddlSearchSup.SelectedValue + "' = '0' )" +
                    Session["userAccessParameters"];

            var ds = objSql.getDataSet(query);
            int i;
            decimal stock, qty;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                qty = Convert.ToDecimal(ds.Tables[0].Rows[i][7].ToString());
                bPrice = Convert.ToDecimal(ds.Tables[0].Rows[i][8]);
                stock = (bPrice * qty);
                total += stock;
            }

            decimal OpeningBalance = 0M;
            query = "SELECT * FROM [CashReportInfo] WHERE status = '0'" + Session["userAccessParameters"];
            DataSet dsBalance = objSql.getDataSet(query);
            dsBalance.Tables[0].Rows.Count.ToString();
            for (i = 0; i < dsBalance.Tables[0].Rows.Count; i++)
            {
                OpeningBalance += Convert.ToDecimal(dsBalance.Tables[0].Rows[i][3]);
            }
            total = OpeningBalance;

            lblTotalStock.Text = "Total Purchase Amount: <b>" + total.ToString("0.00") + "</b>";
        }





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
                cashInHandTotal = Convert.ToDecimal(ds.Tables[0].Rows[0][6].ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cashInTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][4].ToString());
                    cashOutTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][5].ToString());
                }

                lblTotalStock.Text = "<span class='purchase-amount'>"+Resources.Language.Lbl_supply_total_purchase_amount+" <b>" +
                                     cashInTotal.ToString("0.00") + "</b></span>";
                lblTotalDueBalance.Text = "<span class='supplier-payment'>"+Resources.Language.Lbl_supply_total_supplier_payment+" <b>" + cashOutTotal +
                                          "</b></span> <span class='total-payment'>"+Resources.Language.Lbl_supply_total_balance+" <b>" +
                                          (cashInTotal - cashOutTotal) + "</b></span>";
            }
            catch
            {
                cashInTotal = 0M;
                cashOutTotal = 0M;
                cashInHandTotal = 0M;
            }


            // Dataset 
            dsCashReport.ID = "dsCashReport";
            this.Page.Controls.Add(dsCashReport);
            var constr = GlobalVariable.getConnectionStringName();
            dsCashReport.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;
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

            // Schedule Payment 
            string scheduleConditionQuery = " AND isScheduled ='0' ";
            if (chkSchedule.Checked)
            {
                scheduleConditionQuery = " AND isScheduled ='1' ";
                chkAnyDate.Checked = true;
            }

            // any date serach
            if (chkAnyDate.Checked)
            {
                searchFrom = Convert.ToDateTime("01-jan-2000");
                searchTo = Convert.ToDateTime("01-jan-2090");

                txtSearchDateFrom.Enabled = txtSearchDateTo.Enabled = false;
                txtSearchDateFrom.BackColor = txtSearchDateTo.BackColor = Color.Gray;
            }
            else
            {
                txtSearchDateFrom.Enabled = txtSearchDateTo.Enabled = true;
                txtSearchDateFrom.BackColor = txtSearchDateTo.BackColor = Color.White;
            }


            query = "SELECT tbl.Id,tbl.cashType,tbl.descr,tbl.purchaseCode,tbl.cashIn,tbl.cashout,tbl.cashInHand,tbl.entryDate,tbl.billNo,tbl.mainDescr,tbl.roleID,tbl.status,tbl.adjust,tbl.branchId,tbl.groupId,sup.supCompany AS Particular, cat.Id, cat.catName,tbl.isScheduled,warehouse.name as storeName FROM CashReportInfo AS tbl " +
                    "LEFT JOIN SupplierInfo AS sup ON tbl.descr = CAST(sup.supID AS NVARCHAR(50)) " +
                    "LEFT JOIN CategoryInfo AS cat ON CAST(cat.Id AS nvarchar(50)) = tbl.descr " +
                    "LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id=tbl.storeId " +
                    "WHERE (tbl.status = '0') " +
                    "AND (tbl.descr = '" + ddlSearchSup.Text + "' OR '" + ddlSearchSup.SelectedValue + "' = '0') " +
                    "AND (tbl.storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "' = '0') " +
                    "AND (tbl.roleId='" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "' = '0') " +
                    "AND (tbl.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '') " +
                    "AND (tbl.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') " +
                    commonFunction.getUserAccessParameters("tbl") + " " + scheduleConditionQuery +
                    " ORDER BY tbl.Id DESC, tbl.entryDate ASC";

            refreshGrd(query);
        }





        private void reset()
        {
            txtCashOutAmount.Text = "";
            txtCashOutMainDescr.Text = "";
            txtCashInAmount.Text = "";
            txtCashInMainDescr.Text = "";
            txtOpeinginDueDate.Text = txtGivePaymentDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
        }





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
                scriptMessage("You can not delete Sales Transactions.", MessageType.Warning);
            }
        }





        protected void grdCashReportInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            searchResult();
        }





        protected void grdCashReportInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblScheduleStatus = (e.Row.FindControl("lblScheduleStatus") as Label);

                if (lblScheduleStatus.Text == "Yes")
                {
                    (e.Row.FindControl("lblCashOut") as Label).Visible = false;
                    (e.Row.FindControl("btnGrdSchedulePay") as LinkButton).Visible = true;
                }
                else
                {
                    (e.Row.FindControl("lblCashOut") as Label).Visible = true;
                    (e.Row.FindControl("btnGrdSchedulePay") as LinkButton).Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblCashIn")).Text = cashInTotal.ToString();
                ((Label)e.Row.FindControl("lblCashOut")).Text = cashOutTotal.ToString();
                //((Label)e.Row.FindControl("lblCashInHand")).Text = cashInHandTotal.ToString();
            }
        }





        protected void btnCashOutAdd_Click(object sender, EventArgs e)
        {
            if (ddlCashOutTypeSup.Text == "Select Supplier")
            {
                scriptMessage("Select must a Supplier!", MessageType.Warning);
                return;
            }

            if (txtCashOutAmount.Text == "")
            {
                scriptMessage("Supplier payment amount is requird!", MessageType.Warning);
                return;
            }

            if (Convert.ToDecimal(txtCashOutAmount.Text) == 0)
            {
                scriptMessage("Enter amount must be more than ZERO!", MessageType.Warning);
                return;
            }

            string givePaymentDueDate = txtGivePaymentDate.Text;
            if (givePaymentDueDate == "")
                givePaymentDueDate = commonFunction.GetCurrentTime().ToString();


            scriptMessage(
                commonFunction.cashTransactionSales(0, Convert.ToDecimal(txtCashOutAmount.Text), "Supplier Payment",
                    ddlCashOutTypeSup.Text, "", txtCashOutMainDescr.Text, "0", "0", givePaymentDueDate), MessageType.Success);
            searchResult();
            reset();
        }





        protected void btnCashInAdd_Click(object sender, EventArgs e)
        {
            if (ddlCashInTypeSup.Text == "Select Supplier")
            {
                scriptMessage("Select must a Supplier!", MessageType.Warning);
                return;
            }

            if (txtCashInAmount.Text == "")
            {
                scriptMessage("Opening balance amount is requird!", MessageType.Warning);
                return;
            }

            if (Convert.ToDecimal(txtCashInAmount.Text) == 0)
            {
                scriptMessage("Enter amount must be more than ZERO !", MessageType.Warning);
                return;
            }

            string opeingDueDate = txtOpeinginDueDate.Text;
            if (opeingDueDate == "")
                opeingDueDate = commonFunction.GetCurrentTime().ToString();

            scriptMessage(
                commonFunction.cashTransactionSales(Convert.ToDecimal(txtCashInAmount.Text), 0, "Opening Balance" +
                                                                                              "",
                    ddlCashInTypeSup.Text, "", txtCashInMainDescr.Text, "0", "1", opeingDueDate), MessageType.Success);

            //PurchaseBalance();
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
                scriptMessage("There are no data records to display.", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "CashReport";
                Session["reportName"] = "Supplier Report";
                Session["TotalBalance"] = lblTotalDueBalance.Text;
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }





        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Supplier"))
            {
                string msg = commonFunction.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification",
                    "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Supplier");
            }
        }





        protected void grdCashReportInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Receipt")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdCashReportInfo.Rows[index];
                Label PayID = (Label)row.FindControl("lblDescr");
                Label lblCashout = (Label)row.FindControl("lblCashOut");

                double cash = Convert.ToDouble(lblCashout.Text);

                if (cash <= 0)
                {
                    scriptMessage("Supplier payment is not available", MessageType.Warning);
                    return;
                }

                Session["payment"] = cash.ToString();
                Session["pageName"] = "PaymentReceipt";

                string queryPay =
                    "SELECT * FROM CashReportInfo AS tbl INNER JOIN SupplierInfo AS sup ON  tbl.descr = sup.supID WHERE tbl.descr='" +
                    PayID.Text + "'" + commonFunction.getUserAccessParameters("tbl");

                Session["reportQury"] = queryPay;
                Response.Redirect("Print/LoadQuery.aspx");
            }
            else if (e.CommandName == "schedulePayment")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = grdCashReportInfo.Rows[index];


                Label cashId = (Label)row.FindControl("lblId");
                Label supplierId = (Label)row.FindControl("lblDescr");
                Label lblParticular = (Label)row.FindControl("lblParticular");
                Label schedulePayAmt = (Label)row.FindControl("lblCashOut");

                lblCashId.Text = cashId.Text;
                txtSchedulePayment.Text = schedulePayAmt.Text;
                lblSupplierName.Text = "Payment To:" + lblParticular.Text;
                lblSupplierId.Text = supplierId.Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#schedulePayment').modal('show');</script>", false);
            }
        }





        protected void btnShedulePay_OnClick(object sender, EventArgs e)
        {
            decimal schedulePayAmt = Convert.ToDecimal(txtSchedulePayment.Text);
            if (schedulePayAmt <= 0)
            {
                Response.Redirect("~/Admin/Purchase.aspx?msg=less");
                //scriptMessage("You can not pay ZERO or less then ZERO.", MessageType.Warning);
                //return;
            }

            DataTable dtSchedule = objSql.getDataTable("SELECT * FROM CashReportInfo WHERE Id= '" + lblCashId.Text + "'");
            if (dtSchedule.Rows.Count > 0)
            {
                decimal dbSchedulePayAmt = Convert.ToDecimal(dtSchedule.Rows[0]["cashOut"]);

                if (schedulePayAmt > dbSchedulePayAmt)
                {
                    Response.Redirect("~/Admin/Purchase.aspx?msg=big");
                    //scriptMessage("You can not pay more than schedule payment amount.", MessageType.Warning);
                    //return;
                }

                commonFunction.cashTransactionFormPurchase(0, schedulePayAmt, "Schedule Payment", lblSupplierId.Text, "",
                    "", "0", "0", "", commonFunction.GetCurrentTime().ToString(), false, true, 0);

                objSql.executeQuery("UPDATE CashReportInfo SET cashOut=cashout-'" + schedulePayAmt + "' WHERE Id='" +
                                    lblCashId.Text + "'");

                Response.Redirect("~/Admin/Purchase.aspx?msg=success");
                // scriptMessage("Schedule pay successfully", MessageType.Success);
            }
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
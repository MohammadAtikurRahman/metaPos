using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using MetaPOS.Admin.DataAccess;
using ListItem = System.Web.UI.WebControls.ListItem;


namespace MetaPOS.Admin
{


    public partial class Slip :BasePage 
    {


        private DataAccess.CommonFunction commonFunction = new DataAccess.CommonFunction();
        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();

        private static string query = "";
        private static decimal totalAmount, totalReceive, totalDue, totalMiscCost;

        private string exactData = "", isUrlSearch = "0", pageName = "";
        private decimal exactAmt, resultAmt, tmpPreAmt;

        //private DataSet ds;





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Slip"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }

                string path = HttpContext.Current.Request.Url.AbsolutePath;
                string[] pathSplit = path.Split('/');
                pageName = pathSplit[pathSplit.Length - 1];
                if (pageName.ToLower() == "quotation")
                {
                    if (commonFunction.findSettingItemValueDataTable("language") == "en")
                        lblSlipTitle.InnerText = "Quotation";
                    else
                        lblSlipTitle.InnerText = "কোটেশন";
                }
                else
                {
                    if (commonFunction.findSettingItemValueDataTable("language") == "en")

                        lblSlipTitle.InnerText = Resources.Language.Title_invoiceReport;
                    else
                        lblSlipTitle.InnerText = Resources.Language.Title_invoiceReport; 
                }


                txtTo.Text = txtFrom.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
                var cusid = Request.QueryString["cusid"];
                if (!String.IsNullOrEmpty(cusid))
                {
                    txtSearchInvoiceNo.Text = cusid;
                    isUrlSearch = "1";
                    // DataVisibilityControl();
                }

                if (commonFunction.findSettingItemValueDataTable("invoiceWiseSearchProduct") == "0")
                    txtSearchProduct.Visible = false;

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
                commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE (userRight='Regular' OR roleId='" + Session["roleId"] + "') AND active='1' " + Session["storeAccessParameters"] + "", "title", "roleId");

                commonFunction.fillAllDdl(ddlReferredBy, "SELECT DISTINCT staffID, name FROM [StaffInfo] WHERE active='1' and storeId='" + Session["storeId"] + "'", "name", "staffID");
                ddlReferredBy.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoiceReport_all_referral, "0"));


                DataVisibilityControl();
                searchResult();

            }
        }





        private void refreshGrd(string query)
        {
            SqlDataSource dsSlip = new SqlDataSource();
            dsSlip.ID = "dsSlip";
            this.Page.Controls.Add(dsSlip);
            var constr = GlobalVariable.getConnectionStringName();
            dsSlip.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsSlip.SelectCommand = query;
            grdSlip.DataSource = dsSlip;
            grdSlip.DataBind();
        }





        private void searchResult()
        {
            DateTime searchFrom, searchTo;

            if (ddlDateSearch.SelectedValue == "0")
            {
                txtFrom.Visible = true;
                txtTo.Visible = true;
               // txtFrom.Text = txtTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");

                searchFrom = Convert.ToDateTime(txtFrom.Text);
                searchTo = Convert.ToDateTime(txtTo.Text).AddDays(1);
            }
            else
            {
                txtFrom.Visible = false;
                txtTo.Visible = false;

                searchTo = Convert.ToDateTime(DateTime.Now).AddDays(1);
                searchFrom = Convert.ToDateTime("01/Jan/2010");
            }

            if (isUrlSearch == "1")
            {
                txtFrom.Visible = false;
                txtTo.Visible = false;
                ddlDateSearch.Visible = false;
                searchFrom = Convert.ToDateTime("01/Jan/2010");
            }

            string addInsideProductSearch = "", addConditionProductSearch = "";//, addJoinProductSearch = ""
            if (txtSearchProduct.Text != "")
            {
                addInsideProductSearch = ", MIN(stockstatus.prodName) as prodName ";
                //addJoinProductSearch = "LEFT JOIN StockStatusInfo as stockstatus on stockstatus.billNo = tbl.billNo";
                addConditionProductSearch = " AND (stockstatus.prodName LIKE '%" + txtSearchProduct.Text + "%') ";
            }

            if (pageName.ToLower() == "quotation")
                addConditionProductSearch += "AND (tbl.status = 'draft') ";
            else
                addConditionProductSearch += "AND (tbl.status != 'draft') ";

            if (ddlTransection.SelectedValue == "0")
            {
                grdSlip.ShowFooter = false;

                query = "SELECT DISTINCT tbl.billNo,MIN(tbl.Id) as Id,MIN(tbl.CusID) as CusID,MIN(tbl.CusType) as CusType, " +
                        "MIN(staffInfo.name) as name, MIN(tbl.netAmt) as netAmt,MIN(tbl.discAmt) as discAmt,MIN(tbl.grossAmt) as grossAmt," +
                        "MIN(tbl.balance) as paycash,MIN(tbl.giftAmt) as giftAmt,MIN(tbl.entryDate) as entryDate, " +
                        "MIN(tbl.status) as status,MIN(cus.name) as cusName, CAST(MIN(CAST(isAutoSalesPerson AS INT)) AS BIT) as isAutoSalesPerson ," +
                        "MIN(tbl.miscCost) as miscCost, MIN(role.title) as title,MIN(store.name) as storeName, min(ref.name) as referalName, min(stockstatus.serialNo) as serialNo " +
                         addInsideProductSearch + " FROM SlipInfo as tbl " +
                        "LEFT JOIN  StaffInfo on tbl.salesPersonId = staffInfo.staffID " +
                        "LEFT JOIN  StaffInfo as ref on tbl.referredBy = ref.staffID " +
                        "LEFT JOIN CustomerInfo as cus on tbl.cusID = cus.cusID  " +
                        "LEFT JOIN RoleInfo as role on role.roleID = tbl.salesPersonId " +
                        "LEFT JOIN WarehouseInfo as store on store.Id = tbl.storeId  " +
                        "LEFT JOIN StockStatusInfo as stockstatus on stockstatus.billNo = tbl.billNo " +
                    " WHERE tbl.entryDate BETWEEN '" + searchFrom.ToShortDateString() + "' " +
                    "AND '" + searchTo.ToShortDateString() + "' " +
                    " AND (tbl.CusType = '" + ddlCusType.Text + "' OR '" + ddlCusType.Text + "' = 'Search All') " +
                    "AND (tbl.storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') " +
                    "AND (tbl.status='" + ddlSlipStatus.SelectedValue + "' OR '" + ddlSlipStatus.SelectedValue + "'='') " +
                    " AND (tbl.roleId='" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "'='0') " +
                    " AND (ref.staffID='" + ddlReferredBy.SelectedValue + "' OR '" + ddlReferredBy.SelectedValue + "'='0') " +
                    " AND (tbl.billNo LIKE '%" + txtSearchInvoiceNo.Text + "%' OR tbl.cusID LIKE '%" + txtSearchInvoiceNo.Text + "%' " +
                    " OR staffInfo.name LIKE '%" + txtSearchInvoiceNo.Text + "%' OR cus.name LIKE '%" + txtSearchInvoiceNo.Text + "%' " +
                    " OR stockstatus.serialNo LIKE '%" + txtSearchInvoiceNo.Text + "%' " +
                    "OR tbl.prodID LIKE '%" + txtSearchInvoiceNo.Text + "%' ) " +
                     addConditionProductSearch +
                    commonFunction.getUserAccessParameters("tbl") + " " +
                    commonFunction.getStoreAccessParameters("tbl") + " " +
                    " group by tbl.billNo ORDER BY  MIN(tbl.Id) DESC";
            }
            else if (ddlTransection.SelectedValue == "1")
            {
                query = "SELECT DISTINCT tbl.billNo,MIN(tbl.Id) as Id,MIN(tbl.CusID) as CusID,MIN(tbl.CusType) as CusType, " +
                        "MIN(staffInfo.name) as name, MIN(tbl.netAmt) as netAmt,MIN(tbl.discAmt) as discAmt,MIN(tbl.grossAmt) as grossAmt," +
                        "MIN(tbl.balance) as paycash,MIN(tbl.giftAmt) as giftAmt,MIN(tbl.entryDate) as entryDate, " +
                        "MIN(tbl.status) as status,MIN(cus.name) as cusName, CAST(MIN(CAST(isAutoSalesPerson AS INT)) AS BIT) as isAutoSalesPerson ," +
                        "MIN(tbl.miscCost) as miscCost, MIN(role.title) as title,MIN(store.name) as storeName, min(ref.name) as referalName, min(stockstatus.serialNo) as serialNo " +
                        addInsideProductSearch + " FROM SlipInfo as tbl " +
                        "LEFT JOIN  StaffInfo on tbl.salesPersonId = staffInfo.staffID " +
                        "LEFT JOIN  StaffInfo as ref on tbl.referredBy = ref.staffID " +
                        "LEFT JOIN CustomerInfo as cus on tbl.cusID = cus.cusID  " +
                        "LEFT JOIN RoleInfo as role on role.roleID = tbl.salesPersonId " +
                        "LEFT JOIN WarehouseInfo as store on store.Id = tbl.storeId  " +
                        "LEFT JOIN StockStatusInfo as stockstatus on stockstatus.billNo = tbl.billNo " +
                        " WHERE tbl.entryDate BETWEEN '" + searchFrom.ToShortDateString() + "' AND '" + searchTo.ToShortDateString() + "' " +
                        " AND (tbl.CusType = '" + ddlCusType.Text + "' OR '" + ddlCusType.Text + "' = 'Search All') " +
                        " AND (tbl.storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') " +
                        "AND (tbl.status='" + ddlSlipStatus.SelectedValue + "' OR '" + ddlSlipStatus.SelectedValue + "'='') " +
                        " AND (tbl.roleId='" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "'='0') " +
                        " AND (ref.staffID='" + ddlReferredBy.SelectedValue + "' OR '" + ddlReferredBy.SelectedValue + "'='0') " +
                        " AND (tbl.billNo LIKE '%" + txtSearchInvoiceNo.Text + "%' OR tbl.cusID LIKE '%" + txtSearchInvoiceNo.Text + "%' " +
                        " OR staffInfo.name LIKE '%" + txtSearchInvoiceNo.Text + "%'  OR cus.name LIKE '%" + txtSearchInvoiceNo.Text + "%' " +
                        " OR stockstatus.serialNo LIKE '%" + txtSearchInvoiceNo.Text + "%' " +
                        " OR tbl.prodID LIKE '%" + txtSearchInvoiceNo.Text + "%') " +
                        addConditionProductSearch +
                        " AND tbl.Id IN (SELECT MAX(temp.Id) FROM SlipInfo as temp " +
                        " WHERE temp.entryDate BETWEEN '" + searchFrom.ToShortDateString() + "' " +
                        " AND '" + searchTo.ToShortDateString() + "'  GROUP BY temp.billNo) " +
                        commonFunction.getUserAccessParameters("tbl") + " " +
                        commonFunction.getStoreAccessParameters("tbl") + "  " +
                        " group by tbl.billNo ORDER BY  MIN(tbl.Id) DESC";

                totalAmount = 0M;
                totalReceive = 0M;
                totalDue = 0M;
                totalMiscCost = 0M;

                grdSlip.ShowFooter = true;

                try
                {
                    var dtStockFooter = objSql.getDataTable(query);

                    for (int i = 0; i < dtStockFooter.Rows.Count; i++)
                    {
                        totalAmount += Convert.ToDecimal(dtStockFooter.Rows[i][7].ToString());
                        totalReceive += Convert.ToDecimal(dtStockFooter.Rows[i][8].ToString());
                        totalDue += Convert.ToDecimal(dtStockFooter.Rows[i][9].ToString());
                        totalMiscCost += Convert.ToDecimal(dtStockFooter.Rows[i]["miscCost"].ToString());
                    }
                }
                catch
                {
                    totalAmount = 0M;
                    totalReceive = 0M;
                    totalDue = 0M;
                }
            }
            //lblTest.Text = query;
            refreshGrd(query);
        }





        private void DataVisibilityControl()
        {
            // Gridview 
            List<DataControlField> columns = grdSlip.Columns.Cast<DataControlField>().ToList();

            columns.Find(col => col.SortExpression == "netAmt").Visible = false;

            if (commonFunction.findSettingItemValueDataTable("isMiscCost") == "0")
            {
                columns.Find(col => col.SortExpression == "miscCost").Visible = false;
            }

            if (commonFunction.findSettingItemValueDataTable("isReferredBy") == "0")
            {
                ddlReferredBy.Visible = false;
                columns.Find(col => col.SortExpression == "referalName").Visible = false;
            }

            if (commonFunction.findSettingItemValueDataTable("isDisplaySerialNo") == "0")
            {
                columns.Find(col => col.SortExpression == "serialNo").Visible = false;
            }

            if (Session["userRight"].ToString() == "Branch")
            {
                ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoiceReport_search_all_store, "0"));
                ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoiceReport_search_all_user, "0"));
            }
            else if (Session["userRight"].ToString() == "Regular")
            {
                ddlUserList.Visible = false;
                ddlStoreList.Visible = false;

                ddlUserList.SelectedValue = Session["roleId"].ToString();
                ddlStoreList.SelectedValue = Session["storeId"].ToString();

                // Gridview 
                columns.Find(col => col.SortExpression == "storeName").Visible = false;
            }

        }





        protected void grdSlip_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string invoiceId = grdSlip.SelectedRow.Cells[1].Text;
            string invoiceId = (grdSlip.SelectedRow.FindControl("lblInvoice") as Label).Text;
            string CustomerId = (grdSlip.SelectedRow.FindControl("lblCusID") as Label).Text;
            string status = (grdSlip.SelectedRow.FindControl("lblStatus") as Label).Text;

            Response.Redirect("invoice-next?id=" + invoiceId + "&cusid=" + CustomerId + "&from=slip&status=" + status);
        }





        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    Session["pageName"] = "SlipReport";
        //    Session["reportQury"] = query;
        //    Response.Redirect("Print/LoadQuery.aspx");

        //}
        //work here btnPrintSlip_Click
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] pathSplit = path.Split('/');
            pageName = pathSplit[pathSplit.Length - 1];
            if (pageName == "quotation")
            {
                Session["reportName"] = "Quotation Report";
            }
            else
                Session["reportName"] = "Invoice Report";



            Session["pageName"] = "SlipReport";
            Session["reportQury"] = query;
            Response.Redirect("Print/LoadQuery.aspx");

        }






        protected void grdSlip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSlip.PageIndex = e.NewPageIndex;
            this.searchResult();
        }





        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void ddlTransection_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void grdSlip_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label tmpInvoiceId = ((Label)e.Row.FindControl("lblInvoice"));
                ViewState["tmpPreDataId"] = tmpInvoiceId.Text;

                Label tmpPreRecivedAmt = ((Label)e.Row.FindControl("lblReceivedAmt"));
                tmpPreAmt = Convert.ToDecimal(tmpPreRecivedAmt.Text);

                if (tmpInvoiceId.Text == exactData)
                {
                    resultAmt = tmpPreAmt - exactAmt;
                }
                else
                {
                    resultAmt = Convert.ToDecimal(tmpPreRecivedAmt.Text);
                }

                exactData = ViewState["tmpPreDataId"].ToString();
                exactAmt = Convert.ToDecimal(tmpPreAmt);

                Label lblShowStatus = ((Label)e.Row.FindControl("lblShowStatus"));
                if (lblShowStatus.Text == "draft")
                {
                    lblShowStatus.Text = "Quotation";
                }
                else if (lblShowStatus.Text == "sale")
                {
                    lblShowStatus.Text = "Sold";
                }

                //((Label)e.Row.FindControl("lblReceivedAmt")).Text = resultAmt.ToString();
            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalAmt")).Text = totalAmount.ToString();
                ((Label)e.Row.FindControl("lblTotalReceiveAmt")).Text = totalReceive.ToString();
                ((Label)e.Row.FindControl("lblTotalDue")).Text = totalDue.ToString();
                ((Label)e.Row.FindControl("lblTotalMiscCost")).Text = totalMiscCost.ToString();
            }
        }





        protected void ddlDateSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void ddlStoreList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            getUserListByStore();
            searchResult();
        }





        protected void ddlUserList_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            searchResult();
        }



        private void getUserListByStore()
        {
            commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE active='1' AND (storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') AND storeId !='0'", "title", "roleId");
            ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoiceReport_search_all_user, "0"));
        }




    }


}
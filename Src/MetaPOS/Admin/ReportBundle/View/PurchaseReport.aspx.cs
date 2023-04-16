using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AnalyticBundle.View
{
    public partial class PurchaseReport :BasePage
    {
        CommonFunction commonFunction = new CommonFunction();

        private string query = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("PurchaseReport"))
                {
                    commonFunction.pageout();
                }

                commonFunction.fillAllDdl(ddlSupplierName, "SELECT supID,supCompany FROM [SupplierInfo] WHERE active='1'" + Session["userAccessParameters"] +
                " ORDER BY supId ASC", "supCompany", "supId");
                ddlSupplierName.Items.Insert(0, new ListItem(Resources.Language.Lbl_purchaseReport_search_all, "0"));

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");

                txtSearchDateFrom.Text = txtSearchDateTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");

                DataVisibilityControl();
                searchResult();
                
            }


        }



        private void refreshGrd(string query)
        {
            SqlDataSource dsPurchaseReport = new SqlDataSource();
            dsPurchaseReport.ID = "dsPurchaseReport";
            this.Page.Controls.Add(dsPurchaseReport);
            var constr = GlobalVariable.getConnectionStringName();
            dsPurchaseReport.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsPurchaseReport.SelectCommand = query;
            grdPurchaseReportInfo.DataSource = dsPurchaseReport;
            grdPurchaseReportInfo.DataBind();
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

            query = "SELECT  tbl.purchaseCode,sup.supCompany,SUM(CAST(tbl.qty as decimal)) as qty , SUM(CAST(tbl.freeQty as decimal)) as freeQty ,SUM(tbl.stockTotal) as stockTotal,tbl.statusDate,warehouse.name as storeName FROM StockStatusInfo tbl " +
                    "LEFT JOIN SupplierInfo sup ON tbl.supCompany = sup.supId LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = tbl.storeId " +
                    " WHERE tbl.purchaseCode LIKE isNULL('%" + txtSearch.Text + "%',tbl.purchaseCode) " +
                    "AND (tbl.supCompany = '" + ddlSupplierName.SelectedValue + "' OR '" + ddlSupplierName.SelectedValue + "' = '0') " +
                    "AND (tbl.roleId='" + ddlUserList.SelectedValue + "' OR '" + ddlUserList.SelectedValue + "' = '0')" +
                    "AND (tbl.storeId= '" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') " +
                    "AND (CONVERT(date, tbl.statusDate, 103) >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '') " +
                    "AND tbl.purchaseCode !='' AND CAST(tbl.qty as decimal) > 0 " +
                    "AND tbl.searchType ='product' " +
                    "AND (CONVERT(date, tbl.statusDate, 103) <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') " +
                    commonFunction.getUserAccessParameters("tbl") + " " +
                    "GROUP BY tbl.purchaseCode,sup.supCompany,tbl.statusDate,warehouse.name " +
                    "ORDER BY tbl.statusDate DESC";

            refreshGrd(query);
        }



        protected void btnGrdSearch_OnClick(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void btnPrint_OnClickick(object sender, EventArgs e)
        {


        }





        protected void grdPurchaseReportInfo_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }





        protected void grdPurchaseReportInfo_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PurchaseReport")
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


                int index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = grdPurchaseReportInfo.Rows[index];



                Label lblPurchaseCode = (Label)row.FindControl("lblPurchaseCode");
                
                query = "SELECT tbl.prodCode, tbl.prodName,tbl.prodDescr,tbl.qty as qty,tbl.freeQty as freeQty,tbl.bPrice,tbl.sPrice,tbl.stockTotal,tbl.statusDate,tbl.purchaseCode, sup.supCompany,cat.catName FROM StockStatusInfo tbl " +
                        "LEFT JOIN SupplierInfo sup ON tbl.supCompany = sup.supId " +
                        "LEFT JOIN CategoryInfo cat ON tbl.catName=cat.Id "
                    + " WHERE tbl.purchaseCode='" + lblPurchaseCode.Text + "' AND tbl.purchaseCode LIKE isNULL('%" + txtSearch.Text + "%',tbl.purchaseCode) " +
                    "AND (tbl.supCompany = '" + ddlSupplierName.SelectedValue + "' OR '" +
                    ddlSupplierName.SelectedValue + "' = '0') AND (tbl.storeId= '" + ddlStoreList.SelectedValue + "' " +
                    "OR '" + ddlStoreList.SelectedValue + "'='0') AND (CONVERT(date, tbl.statusDate, 103) >= '" +
                    searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '') AND tbl.purchaseCode !='' " +
                    "AND tbl.searchType ='product' " +
                    "AND (CONVERT(date, tbl.statusDate, 103) <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text +
                    "' = '') " + commonFunction.getUserAccessParameters("tbl") + " " +
                    " group by tbl.purchaseCode,sup.supCompany,tbl.statusDate,tbl.prodCode,tbl.prodName,tbl.prodDescr,tbl.qty,tbl.freeQty,tbl.bPrice,tbl.sPrice,tbl.stockTotal,tbl.statusDate,tbl.purchaseCode,sup.supCompany,cat.catName";


                Session["pageName"] = "PurchaseItemReport";
                Session["reportName"] = "Transcrtion Report";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");

            }
        }





        protected void txtSearch_OnTextChanged(object sender, EventArgs e)
        {
            searchResult();
        }






        protected void grdPurchaseReportInfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPurchaseReportInfo.PageIndex = e.NewPageIndex;
            searchResult();
        }





        protected void grdPurchaseReportInfo_OnSelectedIndexChanged(object sender, EventArgs e)
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

            string lblPurchaseCode = (grdPurchaseReportInfo.SelectedRow.FindControl("lblPurchaseCode") as Label).Text;

            string grdQuery = "SELECT tbl.prodCode, tbl.prodName,tbl.prodDescr,tbl.lastQty as qty,tbl.bPrice,tbl.sPrice,tbl.stockTotal,tbl.statusDate,tbl.purchaseCode, sup.supCompany,cat.catName FROM StockStatusInfo tbl " +
                    "LEFT JOIN categoryInfo cat ON cat.Id=tbl.catName " +
                    "LEFT JOIN SupplierInfo sup ON sup.supID = tbl.supCompany " +
                    "where tbl.purchaseCode ='" + lblPurchaseCode + "'  AND (tbl.Status = '" + ddlSupplierName.SelectedValue + "' OR '" +
                ddlSupplierName.SelectedValue + "' = '0') AND (tbl.entryDate >= '" +
                searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text +
                "' = '')  AND (tbl.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text +
                "' = '') " + commonFunction.getUserAccessParameters("tbl");


            Session["pageName"] = "PurchaseItemReport";
            Session["reportName"] = "Transcrtion Report";
            Session["reportQury"] = grdQuery;
            Response.Redirect("Print/LoadQuery.aspx");

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
                ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_purchaseReport_search_all_store, "0"));

                getUserListByStore();
            }
            else if (Session["userRight"].ToString() == "Regular")
            {
                ddlStoreList.Visible = false;
                ddlUserList.Visible = false;
                ddlStoreList.SelectedValue = Session["storeId"].ToString();
                // Gridview 
                List<DataControlField> columns = grdPurchaseReportInfo.Columns.Cast<DataControlField>().ToList();
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
            commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE active='1' AND (storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') AND storeId !='0'", "title", "roleId");
            ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_purchaseReport_search_all_user, "0"));
        }

    }
}
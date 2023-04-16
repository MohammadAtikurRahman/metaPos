using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.InventoryBundle.View
{
    public partial class Expiry :BasePage
    {

        private CommonFunction commonFunction = new CommonFunction();
        //private DataSet ds;
        private static string query = "";//, descr = "";




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Expiry"))
                {
                    commonFunction.pageout();
                }


                txtDateFrom.Text = txtDateTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
                LoadStockDropdownlist();
                searchResult();
                
            }

        }

        private void LoadStockDropdownlist()
        {
            commonFunction.fillAllDdl(ddlSupplierList,
                "SELECT supCompany,SupID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY supCompany ASC", "supCompany", "SupID");
            ddlSupplierList.Items.Insert(0, new ListItem(Resources.Language.Lbl_expiry_search_all_supplier, "0"));

            commonFunction.fillAllDdl(ddlCatagoryList,
                "SELECT catName,Id FROM [CategoryInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY catName ASC", "catName", "Id");
            ddlCatagoryList.Items.Insert(0, new ListItem(Resources.Language.Lbl_expiry_search_all_Category, "0"));
        }





        private void refreshGrd(string query)
        {
            SqlDataSource dsWarningQty = new SqlDataSource();
            dsWarningQty.ID = "dsWarningQty";
            this.Page.Controls.Add(dsWarningQty);
            var constr = GlobalVariable.getConnectionStringName();
            dsWarningQty.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsWarningQty.SelectCommand = query;
            grdExpiryQty.DataSource = dsWarningQty;
            grdExpiryQty.DataBind();
        }





        private void searchResult()
        {
            var conditionalSearch = "";
            if (!chkListExpiry.Checked)
                conditionalSearch = "CAST(expiryDate as date) >='" + txtDateFrom.Text + "' AND CAST(expiryDate as date) <='" + txtDateTo.Text + "' AND ";


            query =
                "SELECT stock.Id, stock.prodName, cat.catName, stock.qty, stock.expiryDate FROM [StockInfo] as stock " +
                "LEFT JOIN CategoryInfo as cat ON stock.catName= cat.Id WHERE "+conditionalSearch+" (CAST(stock.qty as float) <= CAST(stock.warningQty as float)) " +
                "AND ((stock.prodName LIKE IsNULL('%" + txtSearch.Text + "%',stock.prodName)) OR (cat.catName LIKE IsNULL('%" + txtSearch.Text + "%',cat.catName))) AND ((stock.supCompany='" +
                ddlSupplierList.SelectedValue + "' OR '" + ddlSupplierList.SelectedValue + "'='0')  AND (stock.catName='" +
                ddlCatagoryList.SelectedValue + "' OR '" + ddlCatagoryList.SelectedValue + "'='0')) " + commonFunction.getUserAccessParameters("stock") + "" +
                "ORDER BY stock.prodName ";

            refreshGrd(query);
        }





        protected void txtSearch_action(object sender, EventArgs e)
        {
            searchResult();
        }




        protected void grdExpiryQty_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var expiryDate = Convert.ToDateTime(((Label)e.Row.FindControl("lblExpiryDate")).Text);
                var currentDate = commonFunction.GetCurrentTime();

                var totalDays = (Convert.ToDateTime(expiryDate.ToShortDateString()) - Convert.ToDateTime(currentDate.ToShortDateString())).TotalDays;

                ((Label)e.Row.FindControl("lblExpiryDay")).Text = totalDays + " Day (" + expiryDate.ToString("dd-MMM-yyyy") + ")";

            }
        }




        protected void ddlUserList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void grdExpiryQty_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdExpiryQty.PageIndex = e.NewPageIndex;
            searchResult();
        }





        protected void btnPrintExpiry_OnClick(object sender, EventArgs e)
        {
            Session["pageName"] = "ExpiryReport";
            Session["reportQury"] = query;
            Response.Redirect("Print/LoadQuery.aspx");
        }

        protected void chkListExpiry_CheckedChanged(object sender, EventArgs e)
        {
            if(chkListExpiry.Checked)
            {
                txtDateTo.Enabled = false;
                txtDateFrom.Enabled = false;
            }
            else
            {
                txtDateTo.Enabled = true;
                txtDateFrom.Enabled = true;
            }
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
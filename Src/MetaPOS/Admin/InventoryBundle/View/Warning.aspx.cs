using System;
using System.Data;
using System.Net.Http;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.InventoryBundle.View
{


    public partial class Warning : BasePage
    {
        private CommonFunction commonFunction = new CommonFunction();
        //private DataSet ds;

        private static string query = "";//, descr = "";


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
                if (!commonFunction.accessChecker("Warning"))
                {
                    commonFunction.pageout();
                }

                LoadStockDropdownlist();
            }

            searchResult();
        }





        private void LoadStockDropdownlist()
        {
            commonFunction.fillAllDdl(ddlSupplierList,
                "SELECT supCompany,SupID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY supCompany ASC", "supCompany", "SupID");
            ddlSupplierList.Items.Insert(0, new ListItem(Resources.Language.Lbl_warning_search_all_supplier, "0"));

            commonFunction.fillAllDdl(ddlCatagoryList,
                "SELECT catName,Id FROM [CategoryInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY catName ASC", "catName", "Id");
            ddlCatagoryList.Items.Insert(0, new ListItem(Resources.Language.Lbl_warning_search_all_Category, "0"));
        }





        private void refreshGrd(string query)
        {
            try
            {
                SqlDataSource dsWarningQty = new SqlDataSource();
                this.Page.Controls.Add(dsWarningQty);
                var constr = GlobalVariable.getConnectionStringName();
                dsWarningQty.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
                dsWarningQty.SelectCommand = query;
                grdWarningQty.DataSource = dsWarningQty;
                grdWarningQty.DataBind();
            }
            catch (Exception)
            {

            }

        }





        private void searchResult()
        {

            query =
                "SELECT stock.Id, stock.prodName, cat.catName, sup.supCompany, "+commonFunction.getQtyQueryStock("stock", Session["storeId"].ToString(),"inquery")+", stock.warningQty FROM [StockInfo] as stock " +
                "LEFT JOIN CategoryInfo as cat ON stock.catName= cat.Id " +
                "LEFT JOIN SupplierInfo as sup ON stock.supCompany= sup.supId " +
                "WHERE (" + commonFunction.getQtyQueryStock("stock", Session["storeId"].ToString(), "incondition") + ") <= CAST(stock.warningQty as float) " +
                "AND ((stock.prodName LIKE IsNULL('%" + txtSearch.Text + "%',stock.prodName)) " +
                "OR (cat.catName LIKE IsNULL('%" + txtSearch.Text + "%',cat.catName))) " +
                "AND ((stock.supCompany='" + ddlSupplierList.SelectedValue + "' OR '" + ddlSupplierList.SelectedValue + "'='0') " +
                "AND  stock.warningQty != '0' " +
                "AND (stock.catName='" + ddlCatagoryList.SelectedValue + "'  " +
                "OR '" + ddlCatagoryList.SelectedValue + "'='0')) " + commonFunction.getUserAccessParameters("stock") + "  ORDER BY stock.prodName ";

            refreshGrd(query);
        }





        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchResult();
        }




        protected void ddlStore_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            searchResult();
        }




        protected void grdWarningQty_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdWarningQty.PageIndex = e.NewPageIndex;
            searchResult();
        }

        protected void btnPrintWarning_OnClick(object sender, EventArgs e)
        {
            Session["pageName"] = "WarningReport";
            Session["reportQury"] = query;
            Response.Redirect("Print/LoadQuery.aspx");
        }


    }


}
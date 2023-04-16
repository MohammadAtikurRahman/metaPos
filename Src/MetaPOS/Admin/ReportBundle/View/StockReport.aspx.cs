using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MetaPOS.Admin.DataAccess;
using System.IO;
using System.Web.Services;

namespace MetaPOS.Admin.ReportBundle.View
{
    public partial class StockReport : BasePage
    {
        private CommonFunction commonFunction = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("StockReport"))
                {
                    commonFunction.pageout();
                }


                commonFunction.fillAllDdl(ddlSupplierList,
                "SELECT supCompany,SupID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY supCompany ASC", "supCompany", "SupID");
                ddlSupplierList.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Resources.Language.Lbl_stockReport_all, "0"));

                commonFunction.fillAllDdl(ddlCatagoryList,
                    "SELECT catName,Id FROM [CategoryInfo] WHERE active='1' " + Session["userAccessParameters"] +
                    " ORDER BY catName ASC", "catName", "Id");
                ddlCatagoryList.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Resources.Language.Lbl_stockReport_all, "0"));

                commonFunction.fillAllDdl(ddlStoreList,
                    "SELECT name,Id FROM [warehouseInfo] WHERE active='1' " + Session["userAccessParameters"] +
                    " ORDER BY name ASC", "name", "Id");
            }
        }



        [WebMethod]
        public static string getStockReportAction(string category, string supplier, string store)
        {
            string condition = "";
            if (category != "0")
                condition += " AND stock.catName='" + category + "'";
            if (supplier != "0")
                condition += " AND stock.supCompany='" + supplier + "'";

            string query = "SELECT stock.prodId,stock.prodcode,stock.prodName,stock.bprice,stock.dealerPrice,stock.sprice,qtm.stockqty,supplier.supCompany, cat.catName,warehouse.name as storename, branch.branchname, branch.branchaddress,branch.branchphone,branch.branchmobile "
                + "FROM StockInfo as stock left join qtyManagement as qtm ON stock.prodId = qtm.productId "
                +"LEFT JOIN SupplierInfo as supplier ON stock.supCompany = supplier.supId "
                +"LEFT JOIN CategoryInfo as cat ON cat.Id = stock.catName "
                + "LEFT JOIN warehouseInfo as warehouse ON qtm.storeId = warehouse.Id "
                + "LEFT JOIN BranchInfo as branch ON qtm.storeId = branch.storeId "
                +"WHERE stock.active='1' and qtm.storeId = '" + store + "'" + condition + "  ";

            var sqlOperation = new SqlOperation();
            var stockReportData = sqlOperation.getDataTable(query);
            var commonFunction = new CommonFunction();
            return commonFunction.serializeDatatableToJson(stockReportData);
        }



    }
}
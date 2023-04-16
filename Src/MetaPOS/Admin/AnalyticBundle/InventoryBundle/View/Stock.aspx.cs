using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.Service;
using MetaPOS.Admin.Model;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using Label = System.Web.UI.WebControls.Label;
using MetaPOS.Core.Services;
using ClosedXML.Excel;

namespace MetaPOS.Admin.InventoryBundle.View
{


    public partial class Stock : Page
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        private StockModel stockModel = new StockModel();

        private DataSet ds, dsLoadField;

        private static string selectedRowID = "",
            query = "",
            queryGrdDisplay = "",
            offerOnCondition = "",
            groupByDependency = " ",
            whereCondition = "",
            queryOffset = "",
            queryLimit = "";

        private Decimal totalFooterQty = 0M,
        buyTotalFooter = 0M,
        wholesaleTotalFooter = 0M,
        saleTotalFooter = 0M,
        companyPriceFooter = 0M,
        ddlBuyTotalFooter = 0M,
        ddlBuyWholesaleTotalFooter = 0M,
        ddlSaleTotalFooter = 0M;

        private static int i, j, indexOfColumn;
        private static decimal footerTotalBuyPrice, footerTotalSalePrice, entryTotal, dealerTotal;
        private static bool checkingExist;
        private static int fieldCount, jsCount = 0;
        private static string isManufacturer, isShipmentStatus, isImage, isDealer;
        public static DateTime searchFrom, searchTo;

        private static decimal
            entryQtyTotal,
            sellQtyTotal,
            returnQtyTotal,
            footerStockTotalQty,
            damageTotalQty,
            totalQtyFooter = 0;


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
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                Response.Cache.SetNoServerCaching();
                Response.Cache.SetNoStore();

                if (!commonFunction.accessChecker("Stock"))
                {
                    commonFunction.pageout();
                }

                LoadStockDropdownlist();
                FieldVisibilityControl();
                DataVisibilityControl();

                // Check stock operation 
                if (Request["msg"] == "success")
                {
                    ScriptMessage("Product added successfully.", MessageType.Success);
                }
                if (Request["msg"] == "warning")
                {
                    ScriptMessage("Product can not saved.", MessageType.Warning);
                }
                else if (Request["msg"] == "successTransfer")
                {
                    ScriptMessage("Product Transfer successfully.", MessageType.Success);
                }
                else if (Request["msg"] == "warningTransfer")
                {
                    ScriptMessage("Product can not Transfer.", MessageType.Warning);
                }
                else if (Request["msg"] == "nodelete")
                {
                    ScriptMessage("Can not delete, This product has transection data.", MessageType.Warning);
                }
                else if (Request["msg"] == "restoreSuccess")
                {
                    ScriptMessage("Restore successfully completed.", MessageType.Success);
                }
                else if (Request["msg"] == "fail")
                {
                    ScriptMessage("Operation fail.", MessageType.Warning);
                }
                else if (Request["msg"] == "delSuccess")
                {
                    ScriptMessage("Delete Successfully.", MessageType.Success);
                }

                Session["offset"] = 0;
                Session["limit"] = 50;
                StockSearch("", false);

                lblStoreId.Text = Session["storeId"].ToString();


                DataVisibilityControl();

            }
            else if (ViewState["saveQuery"] != null)
            {
                //dsGrdStock.SelectCommand = ViewState["saveQuery"].ToString();

            }



        }





        private void LoadStockDropdownlist()
        {
            commonFunction.fillAllDdl(ddlSupplierList,
                "SELECT supCompany,SupID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY supCompany ASC", "supCompany", "SupID");
            ddlSupplierList.Items.Insert(0, new ListItem("Search All Supplier", "0"));

            commonFunction.fillAllDdl(ddlCatagoryList,
                "SELECT catName,Id FROM [CategoryInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY catName ASC", "catName", "Id");
            ddlCatagoryList.Items.Insert(0, new ListItem("Search All Category", "0"));


            commonFunction.fillAllDdl(ddlManufacturer,
                "SELECT manufacturerName,Id FROM [ManufacturerInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY manufacturerName ASC", "manufacturerName", "Id");
            ddlManufacturer.Items.Insert(0, new ListItem("Search All Manufacturer", "0"));


            //commonFunction.fillAllDdl(ddlWarehouseList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
            commonFunction.fillAllDdl(ddlWarehouseList, "WarehouseInfo", "1");
            //ddlWarehouseList.Items.Insert(0, new ListItem("Search All Store", "0"));

            commonFunction.fillAllDdl(ddlLocationList,
                "SELECT name,Id FROM [LocationInfo] WHERE active='1' " + Session["userAccessParameters"] +
                " ORDER BY name ASC", "name", "Id");
            ddlLocationList.Items.Insert(0, new ListItem("Search All Location", "0"));


            if (Session["userRight"].ToString() == "Regular")
            {
                commonFunction.fillAllDdl(ddlBrachWiseSearch,
                    "SELECT title,branchId FROM [RoleInfo] WHERE roleId='" + Session["roleId"] +
                    "' AND active='1' ORDER BY title ASC", "title", "branchId");
                ddlBrachWiseSearch.Visible = false;
                ddlWarehouseList.Visible = false;
                ddlGrdCols.Visible = false;
            }
            else if (Session["userRight"].ToString() == "Branch")
            {
                commonFunction.fillAllDdl(ddlBrachWiseSearch,
                    "SELECT title,roleId FROM [RoleInfo] WHERE roleId='" + Session["roleId"] +
                    "' AND active='1' ORDER BY title ASC ", "title", "roleId");
                ddlBrachWiseSearch.Visible = false;

            }
            else
            {
                commonFunction.fillAllDdl(ddlBrachWiseSearch,
                    "SELECT title,roleID FROM [RoleInfo] WHERE groupId='" + Session["roleId"] +
                    "' AND active='1' AND userRight='Branch' ORDER BY title ASC ", "title", "roleID");
                ddlBrachWiseSearch.Items.Insert(0, new ListItem("All Branch", "0"));
                ddlBrachWiseSearch.Visible = true;
                btnUnitStock.Visible = false;
            }
        }





        private void FieldVisibilityControl()
        {
            btnAddSingleStock.Visible = commonFunction.accessChecker("UnitStock");
            btnAddMulStock.Visible = commonFunction.accessChecker("BulkStock");

            if (commonFunction.findSettingItemValueDataTable("manufacturer") == "0")
            {
                ddlManufacturer.Visible = false;
            }
        }





        private void DataVisibilityControl()
        {
            // Gridview 
            List<DataControlField> columns = grdStock.Columns.Cast<DataControlField>().ToList();
            columns.Find(col => col.SortExpression == "image").Visible = false;
            columns.Find(col => col.SortExpression == "prodCode").Visible = false;
            columns.Find(col => col.SortExpression == "supCompany").Visible = false;
            columns.Find(col => col.SortExpression == "catName").Visible = false;
            columns.Find(col => col.SortExpression == "manufacturerName").Visible = false;
            columns.Find(col => col.SortExpression == "comPrice").Visible = false;
            columns.Find(col => col.SortExpression == "dealerPrice").Visible = false;
            columns.Find(col => col.SortExpression == "buyTotal").Visible = false;
            columns.Find(col => col.SortExpression == "wholesaleTotal").Visible = false;
            columns.Find(col => col.SortExpression == "soldTotal").Visible = false;


            // Image display 
            if (commonFunction.findSettingItemValueDataTable("Upload") == "1")
            {
                columns.Find(col => col.HeaderText == "Image").Visible = true;
            }

            if (commonFunction.findSettingItemValueDataTable("isWholeSeller") == "1")
            {
                columns.Find(col => col.SortExpression == "dealerPrice").Visible = true;
            }

            if (commonFunction.findSettingItemValueDataTable("displayComPrice") == "1")
            {
                columns.Find(col => col.SortExpression == "comPrice").Visible = true;
            }

            var selectVal = ddlGrdCols.SelectedValue;
            if (selectVal == "0")
            {
                columns.Find(col => col.SortExpression == "buyTotal").Visible = true;
                columns.Find(col => col.SortExpression == "wholesaleTotal").Visible = true;
                columns.Find(col => col.SortExpression == "soldTotal").Visible = true;
            }
            else if (selectVal == "0")
            {
                columns.Find(col => col.SortExpression == "buyTotal").Visible = false;
                columns.Find(col => col.SortExpression == "wholesaleTotal").Visible = false;
                columns.Find(col => col.SortExpression == "soldTotal").Visible = false;
            }

            else if (selectVal == "1")
            {
                columns.Find(col => col.SortExpression == "buyTotal").Visible = true;
            }

            else if (selectVal == "2")
            {
                columns.Find(col => col.SortExpression == "wholesaleTotal").Visible = true;
            }

            else if (selectVal == "3")
            {
                columns.Find(col => col.SortExpression == "soldTotal").Visible = true;
            }

            if (commonFunction.findSettingItemValueDataTable("displayComPrice") == "1")
            {
                columns.Find(col => col.SortExpression == "comPrice").Visible = true;
            }

            if (Session["userRight"].ToString() == "Regular")
            {
                //columns.Find(col => col.SortExpression == "storeName").Visible = false;

                // Check buy Price for user
                if (commonFunction.findSettingItemValueDataTable("displayBuyPriceForUser") == "0")
                {
                    columns.Find(col => col.SortExpression == "bPrice").Visible = false;
                }

                // Print Stock 
                if (commonFunction.findSettingItemValueDataTable("displayPrintAllStock") == "1")
                {
                    btnPrint.Visible = false;
                }

                // CSV hide
                btnExportToCsvModal.Visible = false;

            }
            else if (Session["userRight"].ToString() == "Branch")
            {

            }


            if (commonFunction.findSettingItemValueDataTable("offer") == "0")
            {
                columns.Find(col => col.SortExpression == "freeQty").Visible = false;
                columns.Find(col => col.SortExpression == "freeTotal").Visible = false;
            }


            if (commonFunction.findSettingItemValueDataTable("isBarcodeShowInStock") == "1")
            {
                columns.Find(col => col.SortExpression == "prodCode").Visible = true;
            }
        }





        [WebMethod]
        public static string[] GetProducts(string prefix)
        {
            var products = new List<string>();

            using (var conn = new SqlConnection())
            {
                var constr = GlobalVariable.getConnectionStringName();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText =
                        "SELECT DISTINCT prodName, prodId FROM StockInfo WHERE (prodName like '%' + @SearchText + '%') " +
                        HttpContext.Current.Session["userAccessParameters"];
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            products.Add(string.Format("{0} <> {1}", sdr["prodName"], sdr["prodId"]));
                        }
                    }
                    conn.Close();
                }
            }

            return products.ToArray();
        }





        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void StockSearch(string productId, bool isSearch)
        {
            queryGrdDisplay = GetStockSearchQuery(productId);
            RefreshGrd(queryGrdDisplay, isSearch);

        }





        private string GetStockSearchQuery(string productId)
        {
            totalQtyFooter = 0;

            string dependency = " ",
                warehouseDependency = " ",
                offerInsideQty = "",
                offerAfterWhere = "",
                searchProductQuery = "";

            if (productId != "")
            {
                whereCondition = "stock.parentId = '" + productId + "' AND stock.isChild != '0' " + commonFunction.getUserAccessParameters("stock");
            }
            else
            {
                whereCondition = "stock.prodId != '0' AND stock.parentId='0' " + searchProductQuery +
                                 " AND stock.active='" + ddlStockStatus.SelectedValue + "' AND " +
                                 "(stock.supCompany = '" + ddlSupplierList.SelectedValue + "' OR '" +
                                 ddlSupplierList.SelectedValue + "' = '0') AND " +
                                 "(stock.catName = '" + ddlCatagoryList.SelectedValue + "' OR '" +
                                 ddlCatagoryList.SelectedValue + "' = '0') " +
                                 "AND (stock.locationId = '" + ddlLocationList.SelectedValue + "' OR '" +
                                 ddlLocationList.SelectedValue + "' = '0') " +
                                 "AND ('" + ddlManufacturer.SelectedValue + "' = '0') " +
                                 "AND (stock.prodName LIKE IsNull('%" + txtSearch.Text + "%', stock.prodName) " +
                                 "OR stock.prodId LIKE IsNull('%" + txtSearch.Text + "%', stock.prodId) " +
                                 "OR stock.prodCode LIKE IsNull('%" + txtSearch.Text + "%', stock.prodCode)) " +
                                 commonFunction.getUserAccessParameters("stock") +
                                 dependency + offerAfterWhere;
            }

            var fatchRow = Convert.ToInt32(Session["limit"].ToString()) <= 0 ? 1 : Session["limit"];
            queryGrdDisplay = @"SET QUERY_GOVERNOR_COST_LIMIT 0;
                                Select 
                                stock.prodId AS prodId,
                                MIN(stock.Id) AS Id, 
                                MIN(stock.prodName) AS prodName, 
                                MIN(stock.prodCode) AS prodCode, 
                                MIN(stock.bPrice) AS bPrice,
                                MIN(stock.sPrice) AS sPrice,
                                MIN(stock.comPrice) AS comPrice,
                                MIN(stock.supCompany) AS supCompanyId,
                                MIN(stock.dealerPrice) AS dealerPrice,
                                MIN(category.catName) AS catName,
                                MIN(supplier.supCompany) AS supCompany,
                                MIN(stock.active) as active,
                                MIN(stock.unitId) as unitId,
                                MIN(stock.attributeRecord) as attributeRecord,
                                MIN(stock.parentId) as parentId,
                                MIN(stock.isParent) as isParent,
                                " + offerInsideQty + @"
                                '0' AS qty,
                                MIN(manufacter.manufacturerName) AS manufacturerName,
                                MIN(ecom.image) AS image,
                                MIN(stock.entryDate) AS entryDate
                                FROM StockInfo stock
                                LEFT JOIN Ecommerce ecom ON stock.prodId = ecom.prodId 
                                LEFT JOIN CategoryInfo as category ON category.Id= stock.catName 
                                LEFT JOIN SupplierInfo as supplier ON supplier.supId = stock.supCompany 
                                LEFT JOIN manufacturerInfo as manufacter ON manufacter.Id = stock.manufacturerId 
                                " + offerOnCondition + @" 
                                WHERE " + whereCondition + " GROUP BY stock.prodId " + groupByDependency +
                              " ORDER BY stock.prodId DESC OFFSET " +
                              Session["offset"] + " ROWS FETCH NEXT " +
                              fatchRow + " ROWS ONLY";

            return queryGrdDisplay;
        }


        private void RefreshDtl(string prodId, string storeId, string unitId)
        {
            var currentQty = commonFunction.balanceQtyOperation(prodId, storeId, unitId);
            query = "SELECT tbl.Id,tbl.prodCode,tbl.supCompany,tbl.prodName,tbl.bPrice,tbl.dealerPrice,tbl.sPrice,tbl.size,tbl.stockTotal,tbl.weight,tbl.size,tbl.title,tbl.tax,tbl.sku,tbl.warranty,tbl.imei,tbl.receivedDate,tbl.expiryDate,tbl.batchNo,tbl.serialNo, tbl.comPrice, tbl.warningQty, tbl.shipmentStatus,tbl.notes,tbl.commission,ware.name as warehouse, ecom.prodCode,ecom.image, cat.catName as CategoryName, sup.supCompany as Supplier,manu.manufacturerName as manufacturerName,unit.unitName as unitName," +
                @"" + currentQty + " AS qty FROM StockInfo as tbl " +
                " LEFT JOIN CategoryInfo as cat ON cat.Id = tbl.catName LEFT JOIN SupplierInfo as sup ON sup.supID = tbl.supCompany LEFT JOIN Ecommerce as ecom ON ecom.prodCode = tbl.prodCode LEFT JOIN ManufacturerInfo AS manu ON manu.Id = tbl.manufacturerId LEFT JOIN WarehouseInfo ware ON ware.Id = tbl.warehouse LEFT JOIN UnitInfo as unit ON tbl.unitId = unit.Id  WHERE tbl.prodId = '" + prodId + "'";


            SqlDataSource dsDtlStock = new SqlDataSource();
            dsDtlStock.ID = "dsDtlStock";
            this.Page.Controls.Add(dsDtlStock);
            var constr = GlobalVariable.getConnectionStringName();
            dsDtlStock.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsDtlStock.SelectCommand = query;
            dtlStock.PageIndex = 0;
            dtlStock.DataSource = dsDtlStock;
            dtlStock.DataBind();
        }





        protected void grdStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStock.PageIndex = e.NewPageIndex;
            StockSearch("", false);
        }











        private void RefreshGrd(string queryGrid, bool isSearch)
        {
            entryQtyTotal = 0;
            sellQtyTotal = 0;
            returnQtyTotal = 0;
            footerStockTotalQty = 0;
            footerTotalBuyPrice = 0M;
            footerTotalSalePrice = 0M;
            dealerTotal = 0M;
            damageTotalQty = 0;

            string queryCount = "SELECT * FROM StockInfo WHERE active='1' " + Session["userAccessParameters"] + "";
            if (isSearch)
            {
                queryCount = queryGrid;
            }
            var dtStock = sqlOperation.getDataTable(queryCount);
            lblloadProductCounter.Text = dtStock.Rows.Count.ToString();

            if (dtStock.Rows.Count > 50)
            {
                if (Session["offset"] == null || Convert.ToInt32(Session["offset"]) > dtStock.Rows.Count ||
                    Convert.ToInt32(Session["offset"]) < 0)
                    Session["offset"] = 0;

                if (Session["limit"].ToString() == "")
                    Session["limit"] = 50;

                if (Convert.ToInt32(Session["offset"]) <= 0)
                {
                    btnPrevious.Enabled = false;
                    btnPrevious.Attributes.Add("disabled", "true");
                }
                else
                {
                    btnPrevious.Enabled = true;
                    btnPrevious.Attributes.Remove("disabled");
                }

                if (Convert.ToInt32(Session["offset"]) + 50 >= dtStock.Rows.Count)
                {
                    btnNext.Enabled = false;
                    btnNext.Attributes.Add("disabled", "true");
                }
                else
                {
                    btnNext.Enabled = true;
                    btnNext.Attributes.Remove("disabled");
                }

                lblLodProductCounter.Text = " (" + (Convert.ToInt32(Session["offset"]) + 1) + " - " +
                                         (Convert.ToInt32(Session["offset"]) + 50) + ")";


            }
            else
            {
                Session["offset"] = 0;
                Session["limit"] = dtStock.Rows.Count;
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Attributes.Remove("disabled");
                btnPrevious.Attributes.Remove("disabled");

                lblLodProductCounter.Text = dtStock.Rows.Count.ToString();
            }




            // summary
            queryLimit = dtStock.Rows.Count.ToString();
            queryOffset = "0";
            var querySummary = GetStockSearchQuery("");
            var dtTotalStock = sqlOperation.getDataTable(querySummary);
            for (int k = 0; k < dtTotalStock.Rows.Count; k++)
            {
                var qty = Convert.ToDecimal(dtTotalStock.Rows[k]["qty"].ToString());
                var bPrice = Convert.ToDecimal(dtTotalStock.Rows[k]["bPrice"].ToString());
                var sPrice = Convert.ToDecimal(dtTotalStock.Rows[k]["sPrice"].ToString());
                var wholeSale = Convert.ToDecimal(dtTotalStock.Rows[k]["dealerPrice"].ToString());

                totalFooterQty += qty;
                buyTotalFooter += bPrice;
                saleTotalFooter += sPrice;
                wholesaleTotalFooter += wholeSale;
                companyPriceFooter += Convert.ToDecimal(dtTotalStock.Rows[k]["comPrice"].ToString());
                ddlBuyTotalFooter += qty * bPrice;
                ddlBuyWholesaleTotalFooter += qty * wholeSale;
                ddlSaleTotalFooter += qty * sPrice;
            }

            lblSummary.Visible = false;
            lblSummary.Text = "Total Qty: " + totalFooterQty + " Total Buy: " + ddlBuyTotalFooter
                               + " Total WholeSale: " + ddlBuyWholesaleTotalFooter
                              + " Total Sale: " + ddlSaleTotalFooter;

            var dsStock = new SqlDataSource();
            dsStock.ID = "dsStock";
            this.Page.Controls.Add(dsStock);
            var constr =  GlobalVariable.getConnectionStringName();
            dsStock.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsStock.SelectCommand = queryGrid;
            grdStock.PageIndex = 0;
            grdStock.DataSource = dsStock;
            grdStock.DataBind();
            ViewState["saveQuery"] = queryGrid;

            queryLimit = "";
            queryOffset = "";


        }





        private int iCounterQty = 0;
        protected void grdStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    var qty = Convert.ToInt32(((Label)e.Row.FindControl("lblQty")).Text);
            //    var piece = Convert.ToInt32(((Label)e.Row.FindControl("lblPiece")).Text);
            //    var unitId = ((Label)e.Row.FindControl("lblUnitId")).Text;
            //    var dtRatio = sqlOperation.getDataTable("SELECT unitRatio FROM unitInfo WHERE Id='" + unitId + "'");
            //    var ratio = 1;
            //    var originalQty = qty;
            //    var originalPiece = 0;
            //    if (dtRatio.Rows.Count > 0)
            //    {
            //        ratio = Convert.ToInt32(dtRatio.Rows[0][0].ToString());
            //        if (piece < 0)
            //        {
            //            var pieceToQty = piece / ratio;
            //            var exitingPiece = piece % ratio;

            //            originalQty -= pieceToQty + 1;
            //            originalPiece = ratio - Math.Abs(exitingPiece);
            //        }
            //        else
            //        {
            //            var pieceToQty = piece / ratio;
            //            var exitingPiece = piece % ratio;

            //            originalQty += pieceToQty;
            //            originalPiece = exitingPiece;
            //        }
            //    }
            //    else
            //    {
            //        originalPiece = piece;
            //    }


            //    string originalQtyWithPiece = originalQty + "." + originalPiece;

            //    // 
            //    var totalQtyWithoutFree = originalQtyWithPiece;
            //    var totalFreeQty = "0";
            //    if (commonFunction.findSettingItemValueDataTable("offer") == "1")
            //    {
            //        var offerType = ((Label)e.Row.FindControl("lblOfferType")).Text;
            //        var offerValue = ((Label)e.Row.FindControl("lblOfferValue")).Text;
            //        var discountType = ((Label)e.Row.FindControl("lblDiscountType")).Text;
            //        var discontValue = ((Label)e.Row.FindControl("lblDiscountValue")).Text;

            //        if (offerType == "0" && discountType == "0")
            //        {
            //            var originalQtyInt = 0;
            //            if (originalQty.ToString().Contains('.'))
            //                originalQtyInt = Convert.ToInt32(originalQty.ToString().Split('.')[0]);
            //            else
            //                originalQtyInt = Convert.ToInt32(originalQty);

            //            if (offerValue.Contains('.'))
            //            {
            //                offerValue = offerValue.Split('.')[0];
            //            }

            //            var freeQtyRatio = Convert.ToInt32(originalQtyInt) / Convert.ToInt32(offerValue);

            //            var freeQtyWithoutPiece = 0;
            //            var freePiece = 0;
            //            if (discontValue.Contains('.'))
            //            {
            //                freeQtyWithoutPiece = Convert.ToInt32(discontValue.Split('.')[0]) * freeQtyRatio;
            //                freePiece = Convert.ToInt32(discontValue.Split('.')[1]) * freeQtyRatio;


            //                if (freePiece > ratio)
            //                {
            //                    freeQtyWithoutPiece += freePiece / ratio;
            //                    freePiece = freePiece % ratio;
            //                }
            //            }
            //            else
            //            {
            //                freeQtyWithoutPiece = Convert.ToInt32(discontValue) * freeQtyRatio;
            //            }

            //            // Original qty without free qty
            //            var freePieceInt = 0;
            //            if (freePiece.ToString().Contains('.'))
            //                freePieceInt = Convert.ToInt32(freePiece.ToString().Split('.')[0]);
            //            else
            //                freePieceInt = Convert.ToInt32(freePiece);


            //            if (freePiece > originalPiece)
            //            {
            //                // deduct free qty
            //                originalQty = originalQty - freeQtyWithoutPiece;

            //                originalQty -= 1;
            //                originalPiece = (originalPiece + ratio) - freePieceInt;
            //            }
            //            else
            //            {
            //                originalQty = originalQty - freeQtyWithoutPiece;
            //                originalPiece = originalPiece - freePieceInt;
            //            }


            //            if (originalPiece > ratio)
            //            {
            //                originalQty += freePieceInt / ratio;
            //                originalPiece = freePieceInt % ratio;
            //            }

            //            totalQtyWithoutFree = originalQty + "." + originalPiece;
            //            totalFreeQty = freeQtyWithoutPiece + "." + freePiece;

            //        }
            //    }

            //    ((Label)e.Row.FindControl("lblOrginalQty")).Text = totalQtyWithoutFree;
            //    ((Label)e.Row.FindControl("lblTotalFreeQty")).Text = totalFreeQty;


            //    // set stock total and free total
            //    var buyPrice = ((Label)e.Row.FindControl("lblBPrice")).Text;

            //    var totalBuyPrice = getTotalPriceByQty(buyPrice, totalQtyWithoutFree, ratio);
            //    var totalFreePrice = getTotalPriceByQty(buyPrice, totalFreeQty, ratio);

            //    ((Label)e.Row.FindControl("lblBuyTotal")).Text = totalBuyPrice.ToString("0.00");
            //    ((Label)e.Row.FindControl("lblFreeTotal")).Text = totalFreePrice.ToString("0.00");

            //    // Variant Product
            //    if (commonFunction.findSettingItemValueDataTable("displayVariant") == "1")
            //    {
            //        var attrubuteName = "";
            //        var lblVariantAttr = ((Label)e.Row.FindControl("lblAttributeRecord"));
            //        if (lblVariantAttr.Text != "" && lblVariantAttr.Text != "0")
            //        {
            //            var stock = new Service.Stock();
            //            attrubuteName = stock.getAttributeNameData(lblVariantAttr.Text);
            //        }

            //        ((Label)e.Row.FindControl("lblProdName")).Text += attrubuteName;
            //    }

            //}

        }

        private decimal getTotalPriceByQty(string buyPrice, string totalQtyWithoutFree, int ratio)
        {
            var totalPrice = 0M;
            if (totalQtyWithoutFree.Contains('.'))
            {
                var qtyWithoutPiece = Convert.ToDecimal(totalQtyWithoutFree.Split('.')[0]);
                var Piece = Convert.ToDecimal(totalQtyWithoutFree.Split('.')[1]);

                var perPiecePrice = Convert.ToDecimal(buyPrice) / ratio;

                totalPrice = (qtyWithoutPiece * Convert.ToDecimal(buyPrice)) + (Piece * perPiecePrice) / 100;

            }
            else
            {
                totalPrice = Convert.ToDecimal(buyPrice) * Convert.ToDecimal(totalQtyWithoutFree);
            }

            return totalPrice;
        }





        protected void grdStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lblProdID = grdStock.SelectedRow.FindControl("lblProdID") as Label;
            var lblProductName = grdStock.SelectedRow.FindControl("lblProdName") as Label;
            var lblUnitId = grdStock.SelectedRow.FindControl("lblUnitId") as Label;

            var storeId = Session["storeId"].ToString();
            var productCode = grdStock.SelectedRow.FindControl("lblProdCode") as Label;

            if (lblProdID != null)
                selectedRowID = lblProdID.Text;


            if (lblProductName != null) lblModalTitle.Text = "" + lblProductName.Text;

            lblStoreIdDtl.Text = storeId;
            lblProductCodeDtl.Text = productCode.Text;

            RefreshDtl(lblProdID.Text, storeId, lblUnitId.Text);
            DtlReadOnlyMode();
        }





        protected void grdStock_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)grdStock.Rows[e.RowIndex];
            Label lblProductId = (Label)row.FindControl("lblProdID");


            var dtStockStatus = sqlOperation.getDataTable("SELECT * FROM StockStatusInfo WHERE prodId='" + lblProductId.Text + "' AND qty !='0' and status !='stock'");
            if (dtStockStatus.Rows.Count > 0)
            {
                Response.Redirect("~/admin/stock?msg=nodelete");
                return;
            }

            sqlOperation.executeQuery("UPDATE StockStatusInfo SET active='0' WHERE prodID='" + lblProductId.Text + "'");
            sqlOperation.executeQuery("UPDATE StockInfo SET active='0' WHERE prodID='" + lblProductId.Text + "'");

            Response.Redirect("~/admin/stock?msg=delSuccess");

        }





        protected void grdStock_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            StockSearch("", false);
        }





        private void DtlReadOnlyMode()
        {
            dtlStock.ChangeMode(DetailsViewMode.ReadOnly);

            dtlStock.Fields[0].Visible = false;

            dtlStock.Fields[1].Visible = true;
            dtlStock.Fields[2].Visible = true;
            dtlStock.Fields[3].Visible = true;
            dtlStock.Fields[4].Visible = true;
            dtlStock.Fields[5].Visible = true;
            dtlStock.Fields[7].Visible = true;

            dtlStock.Fields[8].Visible = true;
            dtlStock.Fields[12].Visible = true;
            dtlStock.Fields[13].Visible = true;

            dtlStock.Fields[15].Visible = true;
            dtlStock.Fields[16].Visible = true;
            dtlStock.Fields[17].Visible = true;
            dtlStock.Fields[18].Visible = true;

            isShipmentStatus = commonFunction.findSettingItemValue(40);
            if (isShipmentStatus == "0")
                dtlStock.Fields[21].Visible = false;

            // Image ON/OFF
            isImage = commonFunction.findSettingItemValue(39);
            if (isImage == "0")
            {
                dtlStock.Fields[1].Visible = false;
            }

            // Manufacturer 
            isManufacturer = commonFunction.findSettingItemValue(38);
            if (isManufacturer == "0")
                dtlStock.Fields[6].Visible = false;

            // Label WarningQty = ((Label)dtlStock.FindControl("lblWarningQty")) as Label;
            string WarningQty = commonFunction.findSettingItemValue(20);
            if (Convert.ToInt16(WarningQty) == 0)
                dtlStock.Fields[8].Visible = false;

            //Dealer PRice
            string DealerPrice = commonFunction.findSettingItemValue(21);
            if (Convert.ToDecimal(DealerPrice) == 0)
                dtlStock.Fields[10].Visible = false;

            //Tax 
            string Tax = commonFunction.findSettingItemValue(11);
            if (Convert.ToDecimal(Tax) == 0)
                dtlStock.Fields[14].Visible = false;

            //SKU 
            string SKU = commonFunction.findSettingItemValue(10);
            if (Convert.ToDecimal(SKU) == 0)
                dtlStock.Fields[15].Visible = false;

            //Warranty 
            string Warranty = commonFunction.findSettingItemValue(14);
            if (Convert.ToInt16(Warranty) == 0)
                dtlStock.Fields[16].Visible = false;

            //IMEI 
            string IMEI = commonFunction.findSettingItemValue(15);
            if (Convert.ToInt16(IMEI) == 0)
                dtlStock.Fields[17].Visible = false;

            modalFooter.Visible = true;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["pageName"] = "Stock";
            Session["reportQury"] = queryGrdDisplay;
            Response.Redirect("Print/LoadQuery.aspx");
        }





        protected void btnPrintImage_Click(object sender, EventArgs e)
        {
            // Gridview check count
            string productIdConditionalQuery = "";
            bool isSelected = false;
            foreach (GridViewRow grv in grdStock.Rows)
            {
                if (grv.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)(grv.FindControl("chkGrdSelect"));
                    if (chk.Checked)
                    {
                        isSelected = true;
                        Label lblProductId = (Label)(grv.FindControl("lblProdID"));
                        string productId = lblProductId.Text;

                        Label lblProdCode = (Label)(grv.FindControl("lblProdCode"));

                        if ((lblProdCode.Text.Length < 4) ||
                            (commonFunction.findSettingItemValueDataTable("noBarcode") == "1"))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                                "<script>$('#noBarcodeModal').modal('show');</script>", false);
                            return;
                        }

                        productIdConditionalQuery += " si.prodId='" + productId + "' OR";

                        // Check barcode images
                        var prodCode = "";
                        var dtSotckBarcode = stockModel.getStockDataListModelByProdID(productId);
                        if (dtSotckBarcode.Rows.Count > 0)
                        {
                            prodCode = dtSotckBarcode.Rows[0]["prodCode"].ToString();

                            File.WriteAllText(Server.MapPath("BarcodeTool/BarCode.txt"), prodCode);
                            // Check barcode image create or not
                            if (!File.Exists(Server.MapPath("BarcodeTool/images/" + prodCode + ".png")))
                            {
                                Process process = Process.Start(Server.MapPath("BarcodeTool/BarCodeGenerate.exe"));
                                if (process != null) process.WaitForExit();
                            }

                            //commonFunction.CheckBarcodeImageCreated(prodCode);
                        }
                    }
                }
            }


            if (isSelected)
            {
                productIdConditionalQuery = productIdConditionalQuery.Substring(0,
                    productIdConditionalQuery.Length - 2);
            }
            else
            {
                ScriptMessage("Please select a product", MessageType.Warning);
                return;
            }

            ProdCodeField.Value = productIdConditionalQuery;
            lastQtyField.Value = "1";
            currentQtyField.Value = "1";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                "<script>$('#stockBarcodePrintOption').modal('show');</script>", false);
        }





        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Supplier"))
            {
                var obj = new CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Notification", "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Supplier.aspx");
            }
        }





        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Category"))
            {
                var obj = new CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Notification", "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Category");
            }
        }





        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Product"))
            {
                var obj = new CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Notification", "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Product.aspx");
            }
        }





        protected void btnAddSize_Click(object sender, EventArgs e)
        {
            if (!commonFunction.accessChecker("Size"))
            {
                var obj = new CommonFunction();
                string msg = obj.pageVerificationSignal();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Notification", "alert('" + msg + "');", true);
            }
            else
            {
                Response.Redirect("Size");
            }
        }





        protected void btnSearchDate_Click(object sender, EventArgs e)
        {
            var dtStock = sqlOperation.getDataTable("SELECT Id FROM StockInfo Where active='1'");
            Session["offset"] = 0;
            Session["limit"] = dtStock.Rows.Count;

            var isSearch = true;
            if (ddlWarehouseList.SelectedValue == "0" & ddlLocationList.SelectedValue == "0" &
                ddlSupplierList.SelectedValue == "0" & ddlCatagoryList.SelectedValue == "0" &
                ddlStockStatus.SelectedValue == "1")
            {
                isSearch = false;
                Session["offset"] = 0;
                Session["limit"] = 50;
            }

            StockSearch("", isSearch);
        }





        protected void grdStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];

                Label lblProductID = (Label)row.FindControl("lblProdID");
                var storeId = Session["storeId"].ToString();

                //Response.Redirect("~/admin/bulk-stock?page=unit&prodCode=" + pCode);
                Response.Redirect("~/admin/bulk-stock?page=unit&productId=" + lblProductID.Text + "&storeId=" + storeId + "&status=update");

            }

            if (e.CommandName == "Print")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];

                Label lblProdCode = (Label)row.FindControl("lblProdCode");
                Label lblQty = (Label)row.FindControl("lblQty");
                string prodCode = lblProdCode.Text;

                ProdCodeField.Value = "si.prodCode ='" + prodCode + "'";
                lastQtyField.Value = lblQty.Text;
                currentQtyField.Value = lblQty.Text;
                hfProductCode.Value = prodCode;

                if ((lblProdCode.Text.Length < 4) || (commonFunction.findSettingItemValueDataTable("noBarcode") == "1"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                        "<script>$('#noBarcodeModal').modal('show');</script>", false);
                    return;
                }



                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#stockBarcodePrintOption').modal('show');</script>", false);

                commonFunction.CheckBarcodeImageCreated(prodCode);

            }

            if (e.CommandName == "StockStatus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];
                Label lblProdId = (Label)row.FindControl("lblProdID");

                Response.Redirect("~/Admin/inventory-report?prodId=" + lblProdId.Text);

            }

            if (e.CommandName == "Variant")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];
                Label lblProdID = (Label)row.FindControl("lblProdID");
                Label lblProdName = (Label)row.FindControl("lblProdName");

                lblHeaderTitleInnerAction.InnerText = "Product: " + lblProdName.Text;
                lblHeaderTitle.Visible = false;
                actionRefresh.Visible = true;

                var stock = new Service.Stock();
                var isVariant = stock.getProductData(lblProdID.Text);

                if (isVariant)
                    StockSearch(lblProdID.Text, false);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                //    "<script>$('#variantProductModal').modal('show');</script>", false);

            }

            if (e.CommandName == "addVariant")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];
                Label lblProdID = (Label)row.FindControl("lblProdID");
                Label lblProdName = (Label)row.FindControl("lblProdName");

                lblAddVariantProductId.Text = lblProdID.Text;
                lblProductNameAddVariant.InnerText = "Product: " + lblProdName.Text;

                commonFunction.fillAllDdl(ddlFieldAddVariant,
                   "SELECT field,Id FROM FieldInfo WHERE active ='1' ", "field", "Id");
                ddlFieldAddVariant.Items.Insert(0, new ListItem("Select", "0"));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#AddVariantModal').modal('show');</script>", false);

            }

            if (e.CommandName == "Transfer")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];

                Label lblProdId = (Label)row.FindControl("lblProdID");

                var storeId = Session["storeId"].ToString();
                var branchId = "";
                var roleId = "";
                if (Session["userRight"].ToString() == "Regular")
                {
                    roleId = Session["branchId"].ToString();
                    branchId = Session["branchId"].ToString();

                    storeId = Session["storeId"].ToString();
                }
                else if (Session["userRight"].ToString() == "Branch")
                {
                    roleId = Session["roleId"].ToString();
                    branchId = Session["roleId"].ToString();
                }

                // Branch all user roleid
                var branchAllUserRoleId = commonFunction.getBranchAllUserRoleId(roleId, branchId);


                lblTransFromStoreId.Text = storeId;
                lblTransFromProdId.Text = lblProdId.Text;
                Label lblUnitId = (Label)row.FindControl("lblUnitId");

                var currentQty = commonFunction.balanceQtyOperation(lblProdId.Text, storeId, lblUnitId.Text);

                lblTransferAvailableQty.Text = currentQty;
                Label lblProductName = (Label)row.FindControl("lblProdName");
                lblTransferProductName.Text = lblProductName.Text;



                commonFunction.fillAllDdl(ddlShiftTo,
                    "SELECT Id,name FROM WarehouseInfo WHERE Id != '" + storeId + "' AND active ='1' " +
                    branchAllUserRoleId + "", "name", "Id");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "stockTransferModal();", true);
            }


            if (e.CommandName == "customBarcode")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];

                Label lblProdName = (Label)row.FindControl("lblProdName");
                Label lblProdCode = (Label)row.FindControl("lblProdCode");
                Label lblProdPrice = (Label)row.FindControl("lblSPrice");

                if ((lblProdCode.Text.Length < 4) || (commonFunction.findSettingItemValueDataTable("noBarcode") == "1"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                        "<script>$('#noBarcodeModal').modal('show');</script>", false);
                    return;
                }

                lblProdNameBarcodePrint.Value = lblProdName.Text;
                lblPriceBarcodePrint.Value = lblProdPrice.Text;
                lblCodeBarcodePrint.Value = lblProdCode.Text;


                // var url = "http://localhost:4350/Admin/BarcodeTool/Barcode.html/barcode.html?name='" + lblProdName.Text + "'&code ='" +


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "customBarcodePrint();", true);

            }

            if (e.CommandName == "Restore")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdStock.Rows[index];
                Label lblProdId = (Label)row.FindControl("lblProdID");

                var stockModel = new StockModel();
                var isRestore = stockModel.restoreStockProduct(lblProdId.Text);

                var msg = "restoreSuccess";
                if (!isRestore)
                    msg = "fail";

                Response.Redirect("~/Admin/stock?msg=" + msg + "");

            }
        }





        protected void ddlGrdCols_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataVisibilityControl();
        }





        protected void btnAddMulStock_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/bulk-stock");
        }





        protected void btnAddSingleStock_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/unit-stock");
        }





        public dynamic ProcessMyDataItem(object myValue)
        {
            string path = myValue.ToString();

            if (path == null || path == "")
            {
                return "../default_product.png";
            }

            return myValue + "?" + DateTime.Now.ToLongTimeString();
        }





        public enum ShipmentStatus
        {


            Stocked = 0,
            Moving = 1,
            Shipped = 3


        }




        private string getStockUnitId(string productId)
        {
            DataTable dt = stockModel.getItemStockDataListModelByProdID(productId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["unitId"].ToString();
            else
                return "0";
        }





        private int getQtyPerItem(string itemQty)
        {
            return Convert.ToInt32(itemQty.Split('.')[0]);
        }





        private int getPiecePerItem(string itemQty)
        {
            return Convert.ToInt32(itemQty.Split('.')[1]);
        }











        protected void btnBarcodePrintByOption_OnClick(object sender, EventArgs e)
        {

        }





        protected void btnStockBarcodePrintFromModal_OnClick(object sender, EventArgs e)
        {

            string totalQty = txtInputStock.Text;

            // Check barcode image create or not
            if (!File.Exists(Server.MapPath("BarcodeTool/images/" + hfProductCode.Value + ".png")))
            {
                File.WriteAllText(Server.MapPath("BarcodeTool/BarCode.txt"), hfProductCode.Value);

                Process process = Process.Start(Server.MapPath("BarcodeTool/BarCodeGenerate.exe"));
                if (process != null) process.WaitForExit();
            }

            int n;
            string conditionalStr;
            bool isNumeric = int.TryParse(totalQty, out n);
            if (totalQty == "" || isNumeric == false || Convert.ToDecimal(totalQty) <= 0)
            {
                conditionalStr = @"IncrementingInfo.Number < ((Select  (
                                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,2) as INT) END),0)) -
                                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,2) as INT) END),0)+
                                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,2) as INT) END),0))
                                ) AS qty
                                FROM StockStatusInfo as stockstatus
                                WHERE stockstatus.prodId = si.prodID AND stockstatus.storeId='" + Session["storeId"] + "' AND (" + ProdCodeField.Value + "))) ";


                //                conditionalStr = "IncrementingInfo.Number < CAST(si.qty as decimal)";
            }
            else
            {
                int qtyStock = 0;

                if (totalQty.Contains("."))
                {
                    string[] splitLastQty = totalQty.Split('.');
                    qtyStock = Convert.ToInt32(splitLastQty[0]);
                }
                else
                {
                    qtyStock = Convert.ToInt32(totalQty);
                }

                conditionalStr = "IncrementingInfo.Number < '" + qtyStock + "'";
            }

            PrintBarcodeFormModal(conditionalStr);
        }





        private void PrintBarcodeFormModal(string condionalWithQty)
        {
            query =
                @"SELECT si.prodId, si.prodCode, si.prodName, si.sku, si.bPrice, si.sPrice, sup.supCompany, sup.supCode,cat.catName,((Select  (
                                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,2) as INT) END),0)) -
                                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,2) as INT) END),0)+
                                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,2) as INT) END),0) +
                                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,2) as INT) END),0))
                                ) AS qty
                                FROM StockStatusInfo as stockstatus
                                WHERE stockstatus.prodId = si.prodID AND stockstatus.storeId='" + Session["storeId"] + "' AND (" + ProdCodeField.Value + "))) as qty FROM StockInfo si " +
                "INNER JOIN IncrementingInfo ON " + condionalWithQty + " " +
                "LEFT JOIN SupplierInfo AS sup ON si.supCompany = sup.supCompany " +
                "LEFT JOIN CategoryInfo AS cat ON cat.Id = si.catName " +
                "WHERE " + ProdCodeField.Value + " ";
            //"WHERE si.prodCode = '" + ProdCodeField.Value + "' ";


            Session["pageName"] = "Image";
            Session["reportQury"] = query;
            Response.Redirect("Print/LoadQuery.aspx");
        }





        protected void btnStockTransfer_OnClick(object sender, EventArgs e)
        {
            var stock = new Service.Stock();

            string TransProdId = lblTransFromProdId.Text;
            string transQty = txtTransferQty.Text;
            string stockQty = lblTransferAvailableQty.Text;

            if (Convert.ToDecimal(transQty) > Convert.ToDecimal(stockQty))
            {
                ScriptMessage("Qty is invalid", MessageType.Warning);
                return;
            }

            if (!transQty.Contains("."))
            {
                transQty = transQty + ".0";
            }

            //string isExist = stock.CheckTransferProductOrQty(TransIdTo, TransProdId, transQty);

            var stockstatus = new StockStatus();

            string TransId = lblTransFromStoreId.Text;
            bool stockTransfer = stockstatus.upsertStockstatusTransfer(TransId, TransProdId, transQty, "stockTransfer");


            TransId = ddlShiftTo.SelectedValue;
            string status = "stockReceive";
            if (commonFunction.findSettingItemValueDataTable("isDeliveryStatus") == "1")
                status = "stockPending";

            bool stockReceive = stockstatus.upsertStockstatusTransfer(TransId, TransProdId, transQty, status);


            if (stockTransfer || stockReceive)
                Response.Redirect("~/Admin/stock?msg=successTransfer");
            else
                Response.Redirect("~/Admin/stock?msg=warningTransfer");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeBackdrop();", true);

        }





        protected void dtlStock_OnDataBound(object sender, EventArgs e)
        {
            
            if (dtlStock.CurrentMode == DetailsViewMode.ReadOnly)
            {
                
                try
                {
                    Label lblImei = (Label)dtlStock.FindControl("lblImei");
                    if (lblProductCodeDtl.Text != "" && lblStoreIdDtl.Text != "")
                    {
                        lblImei.Text = commonFunction.getIMEIStoreWise(lblStoreIdDtl.Text, lblProductCodeDtl.Text);

                        var stock = new InventoryBundle.Service.Stock();

                        var lblProductID = grdStock.SelectedRow.FindControl("lblProdID") as Label;
                        Label lblQty = (Label)dtlStock.FindControl("lblQty");

                        lblQty.Text = stock.getStoreWiseQtyByStoreID(lblProductCodeDtl.Text, ddlWarehouseList.SelectedValue);
                        lblImei.Text = commonFunction.getIMEIStoreWise(ddlWarehouseList.SelectedValue, lblProductID.Text);

                    }
                    string storeId = ddlWarehouseList.SelectedValue;
                    var lblProdID = grdStock.SelectedRow.FindControl("lblProdID") as Label;
                    string productId = lblProdID.Text;
                       lblImei.Text = commonFunction.getIMEIStoreWise(storeId, productId);
                    if(lblImei.Text.Length <= 0)
                       lblImei.Text = "None";



                    var lblBuyTotal = (Label)dtlStock.FindControl("lblBuyTotal");
                    var lblWholesaleTotal = (Label)dtlStock.FindControl("lblWholesaleTotal");
                    var lblSaleTotal = (Label)dtlStock.FindControl("lblSaleTotal");

                    Label lblQuantity =(Label) dtlStock.FindControl("lblQty");
                    string qty = lblQuantity.Text;
                    //buy total
                    Label lblBuyPrice = (Label) dtlStock.FindControl("lblBPrice");
                    string buyPrice = lblBuyPrice.Text;

                    var buyTotal = Convert.ToDecimal(qty) * Convert.ToDecimal(buyPrice);
                    lblBuyTotal.Text = buyTotal.ToString("0.00");

                    //wholesale total
                    Label lblDealerPrice = (Label)dtlStock.FindControl("lblDealerPrice");
                    string wholesalePrice = lblDealerPrice.Text;

                    var wholesaleTotal = Convert.ToDecimal(qty) * Convert.ToDecimal(wholesalePrice);
                    lblWholesaleTotal.Text = wholesaleTotal.ToString("0.00");
                    //sale total
                    Label lblSalePrice = (Label)dtlStock.FindControl("lblSPrice");
                    string salePrice = lblSalePrice.Text;

                    var saleTotal = Convert.ToDecimal(qty) * Convert.ToDecimal(salePrice);
                    lblSaleTotal.Text = saleTotal.ToString("0.00");

                }
                catch (Exception )
                {

                }
                

            }

        }




        protected void btnExportToCsv_OnClick(object sender, EventArgs e)
        {
            //StockSearch();
            //ExportGridToCSV();



            string queryExportToExcel = "";
            if (ddlExportCSVOption.SelectedValue == "0")
            {
                // Company wise
                queryExportToExcel = @"Select 
                                        stock.prodId AS ProdId,
                                        MIN(stock.prodName) AS ProdName, 
                                        MIN(stock.prodCode) AS ProdCode,
                                        MIN(stock.supCompany) AS SupplierID,
                                        MIN(stock.catName) AS CategoryID,
                                        MIN(stock.sku) AS SKU,
                                        ((Select  (
                                                (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(qty as decimal) END),0)) -
                                                (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(qty as decimal) END),0)+
                                                COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(qty as decimal) END),0))
                                        ) AS qty
                                        FROM StockStatusInfo as stockstatus
                                        WHERE stockstatus.prodId = stock.prodID)) AS Qty,
                                        MIN(stock.bPrice) AS PurchasePrice,
                                        MIN(stock.dealerPrice) AS WholeSalePrice,
                                        MIN(stock.sPrice) AS SalePrice,
                                        MIN(stock.unitId) AS UnitID,
                                        MIN(stockstatus.storeId) AS StoreID,
                                        MIN(stock.comPrice) AS CompanyPrice,
                                        ((Select  (
                                                (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(qty as decimal) END),0)) -
                                                (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(qty as decimal) END),0)+
                                                COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(qty as decimal) END),0) +
                                                COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(qty as decimal) END),0))
                                        ) AS qty
                                        FROM StockStatusInfo as stockstatus
                                        WHERE stockstatus.prodId = stock.prodID)) AS LastQty
                                        FROM StockInfo stock
                                        LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodId = stock.prodId 
                                        LEFT JOIN Ecommerce ecom ON stock.prodId = ecom.prodId  
                                        LEFT JOIN CategoryInfo as category ON category.Id= stock.catName 
                                        LEFT JOIN SupplierInfo as supplier ON supplier.supId = stock.supCompany
                                        LEFT JOIN manufacturerInfo as manufacter ON manufacter.Id = stock.manufacturerId 
                                        LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = stockstatus.storeId 
                                        " + offerOnCondition + @" 
	                                    WHERE " + whereCondition + " GROUP BY stock.prodId " + groupByDependency +
                              " ORDER BY stock.prodId DESC";
            }
            else
            {
                // storewise
                queryExportToExcel = queryGrdDisplay;
            }


            ExportGridToExcel(queryExportToExcel, "Inventory");

        }





        private void ExportGridToExcel(string queryExport, string exportFileName)
        {

            string constr = ConfigurationManager.ConnectionStrings["dbPOS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(queryExport))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "StockStatus");

                                string fileName = exportFileName + "_" + commonFunction.GetCurrentTime() + ".xlsx";

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

        }





        protected void ddlStockStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            StockSearch("", false);
        }


        protected string ShowButtonRestore(object DataItem)
        {
            //Here you can place as many conditions as you like 
            //Provided you always return either true or false
            if (DataItem.ToString() == "0")
                return "disBlock";
            else
                return "disNone";
        }

        protected string ShowButtonVariantProduct(object DataItem)
        {
            //Here you can place as many conditions as you like 
            //Provided you always return either true or false
            if (DataItem.ToString() == "1")
                return "disBlock";
            else
                return "disNone";
        }


        protected string ShowButtonDelete(object DataItem)
        {
            //Here you can place as many conditions as you like 
            //Provided you always return either true or false
            if (DataItem.ToString() != "0")
                return "disBlock";
            else
                return "disNone";
        }





        protected void dsGridVariant_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

        }





        protected void dsGridVariant_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProdName = (Label)e.Row.FindControl("lblProdName");
                Label lblAttriRecord = (Label)e.Row.FindControl("lblAttriRecord");

                var attributeName = "";
                if (lblAttriRecord.Text.Contains(","))
                {
                    var attr = lblAttriRecord.Text.Split(',');
                    for (int k = 0; k < attr.Length; k++)
                    {
                        var dtAttr = sqlOperation.getDataTable("SELECT * FROM AttributeInfo WHERE Id='" + attr[k].Trim() + "'");
                        if (dtAttr.Rows.Count > 0)
                        {
                            attributeName += " - " + dtAttr.Rows[0]["attributeName"];
                        }
                    }
                }
                else
                {
                    var dtAttr =
                        sqlOperation.getDataTable("SELECT * FROM AttributeInfo WHERE Id='" + lblAttriRecord.Text + "'");
                    if (dtAttr.Rows.Count > 0)
                    {
                        attributeName += " - " + dtAttr.Rows[0][0];
                    }
                }

                lblProdName.Text = lblProdName.Text + attributeName;
            }
        }


        [WebMethod]
        public static string getFieldAttributeNameDataAction(string attributeRecord)
        {
            var variant = new Variant();
            return variant.getAttributeNameData(attributeRecord);
        }





        protected void btnSaveVariant_OnClick(object sender, EventArgs e)
        {

        }





        protected void dsGrdStock_OnSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            Server.ScriptTimeout = 600;
            e.Command.CommandTimeout = 600;
        }





        protected void btnPrevious_OnClick(object sender, EventArgs e)
        {
            Session["offset"] = Convert.ToInt32(Session["offset"]) - 50;

            StockSearch("", false);
        }





        protected void btnNext_OnClick(object sender, EventArgs e)
        {
            Session["offset"] = Convert.ToInt32(Session["offset"]) + 50;
            StockSearch("", false);
        }





        protected void txtSearch_OnTextChanged(object sender, EventArgs e)
        {
            var dtStock = sqlOperation.getDataTable("SELECT Id FROM StockInfo Where active='1'");
            Session["offset"] = 0;
            Session["limit"] = dtStock.Rows.Count;
            StockSearch("", true);
        }





        protected void btnShowAllProducts_OnClick(object sender, EventArgs e)
        {
            var dtStock = sqlOperation.getDataTable("SELECT Id FROM StockInfo Where active='1'");
            Session["offset"] = 0;
            Session["limit"] = dtStock.Rows.Count;
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;
            StockSearch("", true);
        }





        protected void grdStock_OnDisposed(object sender, EventArgs e)
        {

        }



        [WebMethod]
        public static string getProductQtyDataAction(string prodId, string storeId, string unitId)
        {
            var commonFunction = new CommonFunction();
            return commonFunction.balanceQtyOperation(prodId, storeId, unitId);
        }


        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
           
        }

    }


    internal enum enumStockStatusProperty
    {


        Sale,
        Stock,
        Damage,
        Return,
        Transfer,
        Receive


    }





}


/***


string storeIdSearch = ddlWarehouseList.SelectedValue;
            if (Session["userRight"].ToString() == "Regular")
            {
                if (ddlWarehouseList.SelectedValue == "0")
                {
                    storeIdSearch = Session["storeId"].ToString();
                    dependency += " ";
                }

                insideDepency = " AND (stockstatus.storeId ='" + storeIdSearch + "') ";

                warehouseDependency += " Id='" + storeIdSearch + "' ";
            }
            else
            {
                if (ddlWarehouseList.SelectedValue == "0")
                {
                    dependency += " AND (stockStatus.storeId = '" + ddlWarehouseList.SelectedValue + "' OR '" +
                                  ddlWarehouseList.SelectedValue + "' = '0') ";
                    insideDepency += " AND stockstatus.storeId = warehouse.Id " +
                                     commonFunction.getStoreAccessParameters("stockstatus");

                    warehouseDependency = " Id=stockstatus.storeId ";
                    groupByDependency += ",stockstatus.storeId,warehouse.Id ";
                }
                else
                {
                    insideDepency += " AND stockstatus.storeId='" + ddlWarehouseList.SelectedValue + "' ";
                    warehouseDependency += " Id='" + storeIdSearch + "' ";
                }
            }

            //if (commonFunction.findSettingItemValueDataTable("offer") == "1")
            if (true)
            {
                offerInsideQty = " MIN(offer.offerValue) as offerValue, MIN(offer.discountValue) as discountValue, MIN(offer.offerType) as offerType, MIN(offer.discountType) as discountType,";
                offerOnCondition = " LEFT JOIN OfferInfo as offer ON stock.prodId=offer.prodId ";
                offerAfterWhere = " OR (offer.offerType ='0' AND offer.discountType='0') ";
            }


            if (productId != "")
            {
                whereCondition = "stock.parentId = '" + productId + "' AND stock.isChild != '0' ";
            }
            else
            {
                whereCondition = "stock.prodId != '0' AND stock.parentId='0' " + searchProductQuery +
                                 " AND stock.active='" + ddlStockStatus.SelectedValue + "' AND " +
                                 "(stock.supCompany = '" + ddlSupplierList.SelectedValue + "' OR '" +
                                 ddlSupplierList.SelectedValue + "' = '0') AND " +
                                 "(stock.catName = '" + ddlCatagoryList.SelectedValue + "' OR '" +
                                 ddlCatagoryList.SelectedValue + "' = '0') " +
                                 "AND (stock.locationId = '" + ddlLocationList.SelectedValue + "' OR '" +
                                 ddlLocationList.SelectedValue + "' = '0') " +
                                 "AND ('" + ddlManufacturer.SelectedValue + "' = '0') " +
                                 "AND (stock.prodName LIKE IsNull('%" + txtSearch.Text + "%', stock.prodName) " +
                                 "OR stock.prodId LIKE IsNull('%" + txtSearch.Text + "%', stock.prodId) " +
                                 "OR stock.prodCode LIKE IsNull('%" + txtSearch.Text + "%', stock.prodCode)) " +
                                 commonFunction.getStoreAccessParametersWithSelectedValue("stockstatus",
                                     ddlWarehouseList.SelectedValue) +
                                 commonFunction.getUserAccessParameters("stockstatus") +
                                 dependency + offerAfterWhere;
            }


            queryGrdDisplay = @"SET QUERY_GOVERNOR_COST_LIMIT 0;
                                Select 
                                stock.prodId AS prodId,
                                MIN(stock.Id) AS Id, 
                                MIN(stock.prodName) AS prodName, 
                                MIN(stock.prodCode) AS prodCode, 
                                MIN(stock.bPrice) AS bPrice,
                                MIN(stock.sPrice) AS sPrice,
                                MIN(stock.comPrice) AS comPrice,
                                MIN(stock.supCompany) AS supCompanyId,
                                MIN(stock.dealerPrice) AS dealerPrice,
                                MIN(category.catName) AS catName,
                                MIN(supplier.supCompany) AS supCompany,
                                MIN(warehouse.Id) as warehouseId,
                                MIN(stock.active) as active,
                                MIN(stock.unitId) as unitId,
                                MIN(stock.attributeRecord) as attributeRecord,
                                MIN(stock.parentId) as parentId,
                                MIN(stock.isParent) as isParent,
                                " + offerInsideQty + @"
                                (CASE WHEN MIN(stockstatus.storeId) != '" + storeIdSearch +
                              @"' THEN MIN(stockstatus.storeId) ELSE MIN(stockstatus.storeId) END) AS storeId,  
                                (CASE WHEN MIN(stockstatus.storeId) != '" + storeIdSearch +
                              @"' THEN (SELECT name FROM warehouseInfo where " + warehouseDependency +
                              @") ELSE MIN(warehouse.name) END) AS storeName,
                                '0' AS qty,
                                MIN(manufacter.manufacturerName) AS manufacturerName,
                                MIN(ecom.image) AS image,
                                MIN(stock.entryDate) AS entryDate
                                FROM StockInfo stock
                                LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodId = stock.prodId 
                                LEFT JOIN Ecommerce ecom ON stock.prodId = ecom.prodId 
                                LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = stockstatus.storeId 
                                LEFT JOIN CategoryInfo as category ON category.Id= stock.catName 
                                LEFT JOIN SupplierInfo as supplier ON supplier.supId = stock.supCompany
                                LEFT JOIN manufacturerInfo as manufacter ON manufacter.Id = stock.manufacturerId
                                " + offerOnCondition + @" 
                                WHERE " + whereCondition + " GROUP BY stock.prodId " + groupByDependency +
                              " ORDER BY stock.prodId DESC OFFSET " + queryOffset + " ROWS FETCH NEXT " + queryLimit + " ROWS ONLY";

*/
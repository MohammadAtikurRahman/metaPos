using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.Controller;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.Service;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.InventoryBundle.View
{


    public partial class Damage : System.Web.UI.Page
    {


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction objCommonFun = new CommonFunction();
        private StockModel objStock = new StockModel();
        private DataSet ds;

        //Controller 
        private StockStatusController objStockStatusController = new StockStatusController();
        private StockController objStockController = new StockController();
        private CommonController objCommonController = new CommonController();
        private CashReportController objsCashReportController = new CashReportController();

        private Service.Return inventoryReturn = new Service.Return();

        // Sotckstatus Model
        private string query = "",searchQuery="";
        private static int qtyTotal, i;
        private static decimal bPrice, stockTotalTotal, returnTotal;


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
                if (!objCommonFun.accessChecker("Damage"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
                objCommonFun.fillAllDdl(ddlSup, "SELECT DISTINCT supId,supCompany FROM [SupplierInfo] WHERE active='1'",
                    "supCompany", "supId");
                objCommonFun.fillAllDdl(ddlCat, "SELECT DISTINCT Id,catName FROM [CategoryInfo] WHERE active='1'",
                    "catName", "Id");
                objCommonFun.fillAllDdl(ddlStore, "SELECT DISTINCT Id,name FROM warehouseInfo where active='1' AND (roleId='" + Session["roleId"] + "' OR roleId='" + Session["branchId"] + "')", "name", "Id");
                ddlSup.Items.Insert(0, new ListItem("Search All", "0"));
                ddlCat.Items.Insert(0, new ListItem("Search All", "0"));

                txtFrom.Text = txtTo.Text = objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy");
                
            }
            searchResult();
            txtCode.Focus();
        }





        private void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void refreshDtl(string selectedRowID)
        {
            query =
                "SELECT stockStatus.*,cat.catName as Category, sup.supCompany AS Supplier FROM StockStatusInfo AS stockStatus LEFT JOIN CategoryInfo AS cat ON cat.Id = stockStatus.CatName LEFT JOIN SupplierInfo as sup ON sup.supID = stockStatus.SupCompany WHERE stockStatus.Id = '" +
                selectedRowID + "'";

            SqlDataSource dsDtlStockStatus = new SqlDataSource();
            dsDtlStockStatus.ID = "dsDtlStockStatus";
            this.Page.Controls.Add(dsDtlStockStatus);
            var constr = GlobalVariable.getConnectionStringName();
            dsDtlStockStatus.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsDtlStockStatus.SelectCommand = query;
            dtlStockStatus.PageIndex = 0;
            dtlStockStatus.DataSource = dsDtlStockStatus;
            dtlStockStatus.DataBind();
        }





        private void refreshGrd(string query)
        {
            qtyTotal = 0;
            stockTotalTotal = 0M;
            try
            {
                ds = objSql.getDataSet(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        qtyTotal += Convert.ToInt32(ds.Tables[0].Rows[i][7].ToString());
                        stockTotalTotal += Convert.ToDecimal(ds.Tables[0].Rows[i][13].ToString());
                    }
                }
            }
            catch
            {
                qtyTotal = 0;
                stockTotalTotal = 0M;
            }



            SqlDataSource dsGrdStockStatus = new SqlDataSource();
            dsGrdStockStatus.ID = "dsGrdStockStatus";
            this.Page.Controls.Add(dsGrdStockStatus);
            var constr = GlobalVariable.getConnectionStringName();
            dsGrdStockStatus.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsGrdStockStatus.SelectCommand = query;
            grdStockStatus.DataSource = dsGrdStockStatus;
            grdStockStatus.DataBind();

        }





        private void dtlReadOnlyMode()
        {
            dtlStockStatus.Fields[0].Visible = false;
            modalFooter.Visible = true;

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "none",
                "<script>$('#modalStockReturn').modal('show');</script>", false);
        }





        private void reloadPage()
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }





        private void searchResult()
        {
            DateTime searchFrom, searchTo;
            try
            {
                searchFrom = Convert.ToDateTime(txtFrom.Text);
            }
            catch
            {
                searchFrom = Convert.ToDateTime("01-jan-2000");
            }
            try
            {
                searchTo = Convert.ToDateTime(txtTo.Text);
            }
            catch
            {
                searchTo = Convert.ToDateTime("01-jan-2090");
            }
            searchQuery = "SELECT tbl.*,cat.catName as CategoryName, sup.supCompany AS Supplier FROM StockStatusInfo AS tbl " +
                "LEFT JOIN CategoryInfo AS cat ON cat.Id = tbl.CatName " +
                "LEFT JOIN SupplierInfo as sup ON sup.supID = tbl.SupCompany " +
                "WHERE  status = 'damage' " +
                "AND (tbl.supCompany = '" + ddlSup.SelectedValue + "' OR '" + ddlSup.SelectedValue + "' = '0') " +
                "AND (tbl.catName = '" + ddlCat.SelectedValue + "' OR '" + ddlCat.SelectedValue + "' = '0') " +
                "AND (tbl.statusDate >= '" + searchFrom.ToShortDateString() + "' OR '" +
                txtFrom.Text + "' = '')  AND (tbl.statusDate <= '" + searchTo.ToShortDateString() + "' OR '" +
                txtTo.Text + "' = '')  and tbl.active='1' " + objCommonFun.getUserAccessParameters("tbl") + objCommonFun.getStoreAccessParameters("tbl") + " ORDER BY tbl.Id DESC";

            refreshGrd(searchQuery);
        }
        



        protected void grdStockStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRowID = (grdStockStatus.SelectedRow.FindControl("lblID") as Label).Text;
            lblModalTitle.Text = "Company: " + (grdStockStatus.SelectedRow.FindControl("lblSupCompany") as Label).Text;
            refreshDtl(selectedRowID);
            dtlReadOnlyMode();
        }
        



        protected void grdStockStatus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (objCommonFun.GetCurrentTime().ToShortDateString() ==
                Convert.ToDateTime((grdStockStatus.Rows[e.NewEditIndex].FindControl("lblStatusDate") as Label).Text)
                    .ToShortDateString())
            {
                string productId = "";
                ds = objSql.getDataSet("SELECT * FROM [StockInfo] WHERE prodID = '" +
                                      (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblProdID") as Label).Text + "'");


                string prodImei = (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblImei") as Label).Text.ToString();

                string supplierName = (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblSupCompany") as Label).Text;
                string stockQty = (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblQty") as Label).Text;
                decimal stockBPrice = Convert.ToDecimal((grdStockStatus.Rows[e.NewEditIndex].FindControl("lblBPrice") as Label).Text);
                string ID = (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblID") as Label).Text;
                string prodID = (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblProdID") as Label).Text;

                var stock = new Service.Stock();
                string currentTotalStockQty = stock.getCurrentTotalStockQty(stockQty, prodID);
                decimal stockTotal = stock.getStockTotalPrice(prodID, stockBPrice, stockQty);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    query = "BEGIN TRANSACTION " +
                    "INSERT [StockStatusInfo] (prodID, prodCode, prodName, prodDescr, supCompany, catName, qty, bPrice, sPrice, weight, size, discount, stockTotal, entryDate, status, statusDate, entryQty, title, roleID, branchId, groupId, imei,transceiverId,storeId) SELECT prodID, prodCode, prodName, prodDescr, supCompany, catName, qty, bPrice, sPrice, weight, size, discount, stockTotal, entryDate, 'stock', statusDate, entryQty, title, roleID, branchId, groupId, imei,transceiverId,storeId FROM StockStatusInfo WHERE Id = '" + ID + "' " +
                    "UPDATE StockStatusInfo SET active='0' WHERE Id='" + ID + "'" +
                    "COMMIT";
                }
                else
                {

                    query = "BEGIN TRANSACTION "
                            + "INSERT [StockInfo] (prodID, prodCode, prodName, prodDescr, supCompany, catName, qty, bPrice, sPrice, weight, size, discount, stockTotal, entryDate, entryQty, title, roleId, branchId, groupId, fieldAttribute, sku, tax)  SELECT prodID, prodCode, prodName, prodDescr, supCompany, catName, qty, bPrice, sPrice, weight, size, discount, stockTotal, entryDate, entryQty, title, '" +
                                Session["roleId"] + "', '" + Session["branchId"] + "', '" + Session["groupId"] + "', fieldAttribute, sku, tax FROM [StockStatusInfo] WHERE ID = '" + prodID + "' "
                            + "INSERT [StockStatusInfo] (prodID, prodCode, prodName, prodDescr, supCompany, catName, qty, bPrice, sPrice, weight, size, discount, stockTotal, entryDate, status, statusDate, entryQty, title, roleID, branchId, groupId, imei,transceiverId,storeId) SELECT prodID, prodCode, prodName, prodDescr, supCompany, catName, qty, bPrice, sPrice, weight, size, discount, stockTotal, entryDate, 'stock', statusDate, entryQty, title, roleID, branchId, groupId, imei,transceiverId,storeId FROM StockStatusInfo WHERE Id = '" + ID + "' "
                            + "UPDATE StockStatusInfo SET active='0' WHERE Id='" + ID + "' "
                            + "COMMIT";
                }



                objSql.executeQuery(query);
                // return to stock
                objCommonFun.cashTransaction(0, -stockTotal, "Expense",
                    (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblSupCompany") as Label).Text, "", "", "2", "0");

                //--> cashDue and Balance setting  
                string supCompany = (grdStockStatus.Rows[e.NewEditIndex].FindControl("lblSupCompany") as Label).Text;
                returnTotal =
                    -Convert.ToDecimal((grdStockStatus.Rows[e.NewEditIndex].FindControl("lblStockTotal") as Label).Text);
                query =
                    "INSERT INTO [SupplierStatus] (supCompany,     billNo,  status,       returnTotal,                      entryDate)  VALUES ('" +
                    supCompany + "', '-1', damaged','" + returnTotal + "' ,  '" +
                    objCommonFun.GetCurrentTime().ToShortDateString() + "')";
                objSql.executeQuery(query);
                //<-- cashDue and Balance setting 
                ScriptMessage("Data moved to Stock Successfully.", MessageType.Success);
                searchResult();
            }
            else
            {
                ScriptMessage("Can not Retrive Previous Entry.", MessageType.Warning);
            }
        }





        protected void grdStockStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblQty")).Text = qtyTotal.ToString();
                ((Label)e.Row.FindControl("lblStockTotal")).Text = stockTotalTotal.ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bprices = ((Label)e.Row.FindControl("lblBPrice")).Text;
                var qty = ((Label)e.Row.FindControl("lblQty")).Text;

                if (bprices == "")
                    bprices = "0";
                if (qty == "")
                    qty = "0";

                ((Label)e.Row.FindControl("lblStockTotal")).Text = (Convert.ToDecimal(bprices) * Convert.ToDecimal(qty)).ToString();
            }
        }
        



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchResult();
        }




        protected void btnCheck_Click(object sender, EventArgs e)
        {

            if (txtCode.Text == "")
            {
                ScriptMessage("Product Code is required",MessageType.Warning);
                return;
            }

            SearchDamageProduct();
        }




        protected void btnBackToDamage_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtQty.Text) <= 0)
            {
                ScriptMessage("Qty must be more then ZERO.", MessageType.Warning);
                return;
            }


            inventoryReturn.productCode = txtCode.Text;
            inventoryReturn.storeId = ddlStore.SelectedValue;
            var dicDamageItem = inventoryReturn.getReturnOrDamageItemData();

            if (dicDamageItem.Count == 0)
            {
                ScriptMessage("Item not found.", MessageType.Warning);
                return;
            }

            int qty = Convert.ToInt32(dicDamageItem["qty"]);
            int piece = Convert.ToInt32(dicDamageItem["piece"]);
            string catName = dicDamageItem["catName"];
            decimal buyPrice = Convert.ToDecimal(dicDamageItem["bPrice"]);
            string ProdId = dicDamageItem["prodID"];
            string imei = objCommonFun.getIMEIStoreWise(ddlStore.SelectedValue, ProdId);
            string ProdIdDamaged = dicDamageItem["prodID"];

            string prodCode = txtCode.Text;

            // input check
            string inputDamageQty = txtQty.Text;
            int inputRetQty = 0, inputRetPiece = 0;
            if (inputDamageQty.Contains("."))
            {
                string[] splitInputReturnQty = inputDamageQty.Split('.');

                inputRetQty = Convert.ToInt32(splitInputReturnQty[0]);
                inputRetPiece = Convert.ToInt32(splitInputReturnQty[1]);
            }
            else
            {
                inputRetQty = Convert.ToInt32(inputDamageQty);
                inputRetPiece = 0;
            }

            if (txtQty.Text == "0")
            {
                ScriptMessage("0 is wrong input!", MessageType.Warning);
                return;
            }

            if (qty < inputRetQty || piece < inputRetPiece)
            {
                ScriptMessage("Product Qty Exceed Limit!", MessageType.Warning);
                return;
            }

            if (Convert.ToInt32(txtQty.Text) < 0)
            {
                ScriptMessage("Product qty cant be negative!", MessageType.Warning);
                return;
            }

            // check imei and unit
            string returnImei;

            string isImei = objCommonFun.findSettingItemValueDataTable("imei");
            string isUnit = objCommonFun.findSettingItemValueDataTable("isUnit");

            if (isImei == "1" && imei != "")
            {
                if (imei != "" && txtImei.Value == "")
                {
                    ScriptMessage("Imei number required!", MessageType.Warning);
                    return;
                }

                // Update imei number

                returnImei = txtImei.Value;

                string[] splitDbImei = imei.Trim().Split(',');
                string[] splitReturnImei = returnImei.Trim().Split(',');
                bool imeiEixsts = true;

                //qty and imei match
                if (txtQty.Text != splitReturnImei.Length.ToString())
                {
                    ScriptMessage("Qty and imei number not match!", MessageType.Warning);
                    return;
                }

                for (int i = 0; i < splitReturnImei.Length; i++)
                {
                    for (int j = 0; j < splitDbImei.Length; j++)
                    {
                        if (splitReturnImei[i] == splitDbImei[j])
                        {
                            string reImei = splitReturnImei[i].ToString();
                            imei = imei.Replace(reImei + ',', "");
                            imeiEixsts = true;
                            break;
                        }
                        else
                        {
                            imeiEixsts = false;
                        }
                    }

                    if (imeiEixsts == false)
                    {
                        ScriptMessage("Please input correct imei number!", MessageType.Warning);
                        return;
                    }
                }
            }


            decimal totalQty = 0, inputTotalQty = 0;
            if (isUnit == "1")
            {
                string inputQtyTxt = txtQty.Text;

                int inputQty = 0, inputPiece = 0;

                if (inputQtyTxt.Contains("."))
                {
                    string[] splitInputQty = inputQtyTxt.Split('.');
                    inputQty = Convert.ToInt32(splitInputQty[0]);
                    inputPiece = Convert.ToInt32(splitInputQty[1]);

                }
                else
                {
                    inputQty = Convert.ToInt32(inputQtyTxt);
                    inputPiece = 0;
                }
                inputTotalQty = Convert.ToDecimal(inputQty + "." + inputPiece);

                int updateQty = qty - inputQty;
                int updatePiece = piece - inputPiece;

                if (updatePiece > 0)
                    totalQty = Convert.ToDecimal(updateQty + "." + updatePiece);
                else
                    totalQty = Convert.ToDecimal(updateQty);
            }
            else
            {
                inputTotalQty = Convert.ToDecimal(inputDamageQty);
                totalQty = qty - Convert.ToInt32(txtQty.Text);
            }

            // get stock total price
            var stock = new Service.Stock();
            string prodId = stock.getProductIDbyProdCode(prodCode);
            decimal stockInputTotalPrice = stock.getStockTotalPrice(prodId, buyPrice, inputTotalQty.ToString());
            decimal stockUpdateTotalPrice = stock.getStockTotalPrice(prodId, buyPrice, totalQty.ToString());




            Dictionary<string, string> dicStock = new Dictionary<string, string>();
            dicStock.Add("prodCode", txtCode.Text);

            var getConditinalParameter = objCommonController.getConditinalParameter(dicStock);

            DataSet dsDamage = objStock.getStockConditinalParameter(getConditinalParameter);

            if (dsDamage.Tables[0].Rows.Count == 0)
                return;

            if (!txtQty.Text.Contains("."))
            {
                txtQty.Text = txtQty.Text + ".0";
            }

            // Set perameter for Insert StockStatusInfo
            Dictionary<string, string> dicStockStatus = new Dictionary<string, string>();
            dicStockStatus.Add("prodID", dsDamage.Tables[0].Rows[0][1].ToString());
            dicStockStatus.Add("prodCode", dsDamage.Tables[0].Rows[0][2].ToString());
            dicStockStatus.Add("prodName", dsDamage.Tables[0].Rows[0][3].ToString());
            dicStockStatus.Add("prodDescr", dsDamage.Tables[0].Rows[0][4].ToString());
            dicStockStatus.Add("supCompany", dsDamage.Tables[0].Rows[0][5].ToString());
            dicStockStatus.Add("catName", dsDamage.Tables[0].Rows[0][6].ToString());
            dicStockStatus.Add("qty", txtQty.Text);
            dicStockStatus.Add("bPrice", dsDamage.Tables[0].Rows[0][8].ToString());
            dicStockStatus.Add("sPrice", dsDamage.Tables[0].Rows[0][9].ToString());
            dicStockStatus.Add("weight", dsDamage.Tables[0].Rows[0][10].ToString());
            dicStockStatus.Add("size", dsDamage.Tables[0].Rows[0][11].ToString());
            dicStockStatus.Add("discount", dsDamage.Tables[0].Rows[0][12].ToString());
            dicStockStatus.Add("stockTotal", stockInputTotalPrice.ToString());
            dicStockStatus.Add("status", "Damage");
            dicStockStatus.Add("entryDate", dsDamage.Tables[0].Rows[0][15].ToString());
            dicStockStatus.Add("statusDate", dsDamage.Tables[0].Rows[0][16].ToString());
            dicStockStatus.Add("entryQty", dsDamage.Tables[0].Rows[0][17].ToString());
            dicStockStatus.Add("title", dsDamage.Tables[0].Rows[0][18].ToString());
            dicStockStatus.Add("roleID", HttpContext.Current.Session["roleID"].ToString());
            dicStockStatus.Add("branchId", HttpContext.Current.Session["branchId"].ToString());
            dicStockStatus.Add("groupId", HttpContext.Current.Session["groupId"].ToString());
            dicStockStatus.Add("imei", txtImei.Value);
            dicStockStatus.Add("storeId", ddlStore.SelectedValue);
            dicStockStatus.Add("lastQty", objCommonFun.getLastStockQty(prodId, HttpContext.Current.Session["storeId"].ToString()));

            if (imei != "")
                imei = "," + imei;

            // Insert StockStatusInfo
            objStockStatusController.createStockStatusInfo(dicStockStatus);

            // Damge must be expensive

            Dictionary<string, string> dicDamaged = new Dictionary<string, string>();
            dicDamaged.Add("cashType", "damaged");
            dicDamaged.Add("descr", txtCode.Text);
            dicDamaged.Add("cashOut", stockInputTotalPrice.ToString());
            dicDamaged.Add("billNo", "");
            //
            objsCashReportController.createCashReport(dicDamaged);

            string supplierId = dsDamage.Tables[0].Rows[0][5].ToString();
            objCommonFun.cashTransaction(0, stockInputTotalPrice, "Damage", supplierId, "", ProdIdDamaged, "8","0");


            ScriptMessage("Damaged Added Successfully", MessageType.Success);

            reset();

            searchResult();
        }





        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (grdStockStatus.Rows.Count <= 0)
            {
                ScriptMessage("There are no data records to print ", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "Damage";
                Session["reportQury"] = searchQuery;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }





        private void reset()
        {
            txtQty.Text = "";
            txtImei.Value = "";
        }


        //<-- Button
        protected void ddlStore_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SearchDamageProduct();
        }




        protected void grdStockStatus_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStockStatus.PageIndex = e.NewPageIndex;
            searchResult();
        }




        private void SearchDamageProduct()
        {

            inventoryReturn.productCode = txtCode.Text;
            inventoryReturn.storeId = ddlStore.SelectedValue;
            Dictionary<string, string> dicReturnItem = inventoryReturn.getReturnOrDamageItemData();

            if (dicReturnItem.Count == 0)
            {
                ScriptMessage("Item not found.", MessageType.Warning);
                reset();
                return;
            }

            txtQty.Text = objCommonFun.getStockQtyByUnitMeasurement(txtCode.Text, ddlStore.SelectedValue);
            bPrice = Convert.ToDecimal(dicReturnItem["bPrice"]);
            string catName = dicReturnItem["catName"];
            string ProdIdReturn = dicReturnItem["prodID"];
            string imei = objCommonFun.getIMEIStoreWise(ddlStore.SelectedValue, ProdIdReturn);

            // Check has Imei number
            if (imei != "")
            {
                txtImei.Value = imei;
                pnlReturnImei.Visible = true;
            }
            else
            {
                pnlReturnImei.Visible = false;
            }
        }

    }


}
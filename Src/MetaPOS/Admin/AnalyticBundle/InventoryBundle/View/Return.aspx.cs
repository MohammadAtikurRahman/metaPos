using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.Service;
using System.Web;



namespace MetaPOS.Admin.InventoryBundle.View
{


    public partial class Return : Page
    {


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        private DataSet ds;
        private Service.Return inventoryReturn = new Service.Return();

        private static string selectedRowID = "",
            query = "",
            supCompany = "",
            catName = "",
            imei = "",
            isImei = "",
            isUnit;

        private static int i;
        private static decimal bPrice, stockTotalTotal, returnTotal, qtyTotal;


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
                if (!commonFunction.accessChecker("Return"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }
                commonFunction.fillAllDdl(ddlSup, "SELECT DISTINCT supCompany FROM [SupplierInfo] WHERE active='1'",
                    "supCompany", "supCompany");
                commonFunction.fillAllDdl(ddlCat, "SELECT DISTINCT catName FROM [CategoryInfo] WHERE active='1'",
                    "catName", "catName");
                commonFunction.fillAllDdl(ddlStore, "SELECT DISTINCT Id,name FROM warehouseInfo where active='1' AND (roleId='" + Session["roleId"] + "' OR roleId='" + Session["branchId"] + "')", "name", "Id");

                ddlSup.Items.Insert(0, "Search All");
                ddlCat.Items.Insert(0, "Search All");

                txtFrom.Text = txtTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");


                isImei = commonFunction.findSettingItemValue(15);
                isUnit = commonFunction.findSettingItemValue(44);

                searchResult();
            }

            txtCode.Focus();
        }





        private void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void refreshDtl()
        {
            query = "SELECT stockStatus.*,cat.catName as Category, sup.supCompany AS Supplier FROM StockStatusInfo AS stockStatus LEFT JOIN CategoryInfo AS cat ON cat.Id = stockStatus.CatName LEFT JOIN SupplierInfo as sup ON sup.supID = stockStatus.SupCompany WHERE stockStatus.Id = '" +
                selectedRowID + "'";

            SqlDataSource dsDtlStockStatus = new SqlDataSource();
            dsDtlStockStatus.ID = "dsDtlStockStatus";
            this.Page.Controls.Add(dsDtlStockStatus);
            var constr = GlobalVariable.getConnectionStringName();
            dsDtlStockStatus.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsDtlStockStatus.SelectCommand = query;
            dtlStockStatus.DataSource = dsDtlStockStatus;
            dtlStockStatus.DataBind();
        }





        private void refreshGrd(string query)
        {
            qtyTotal = 0M;
            stockTotalTotal = 0M;
            try
            {
                var dtReturn = objSql.getDataTable(query);
                for (i = 0; i < dtReturn.Rows.Count; i++)
                {
                    qtyTotal += Convert.ToDecimal(dtReturn.Rows[i]["qty"].ToString());
                    stockTotalTotal += Convert.ToDecimal(dtReturn.Rows[i]["stockTotal"].ToString());
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
            query =
                "SELECT tbl.*,cat.catName as CategoryName, sup.supCompany AS Supplier FROM StockStatusInfo AS tbl LEFT JOIN CategoryInfo AS cat ON cat.Id = tbl.CatName LEFT JOIN SupplierInfo as sup ON sup.supID = tbl.SupCompany WHERE  status = 'stockReturn' AND (tbl.supCompany = '" +
                ddlSup.Text + "' OR '" + ddlSup.Text + "' = 'Search All') AND (tbl.catName = '" + ddlCat.Text + "' OR '" +
                ddlCat.Text + "' = 'Search All') AND (tbl.statusDate >= '" + searchFrom.ToShortDateString() + "' OR '" +
                txtFrom.Text + "' = '')  AND (tbl.statusDate <= '" + searchTo.ToShortDateString() + "' OR '" +
                txtTo.Text + "' = '') and tbl.active='1' " + commonFunction.getUserAccessParameters("tbl") + commonFunction.getStoreAccessParameters("tbl") + " ORDER BY tbl.Id DESC";

            refreshGrd(query);
        }





        //void checkProdCode()
        //{
        //    DataTable dt = objSql.getDataTable("SELECT * FROM [StockInfo] WHERE prodCode = '" + txtCode.Text + "'" +  Session["userAccessParameters"] + " ");
        //    try
        //    {
        //        string strQty = dt.Rows[0]["qty"].ToString();
        //        try
        //        {
        //            string[] qtyWithPieces = strQty.Split('.');
        //            foreach (var item in qtyWithPieces)
        //            {
        //                qty = Convert.ToInt32(qtyWithPieces[0]);
        //                piece = Convert.ToInt32(qtyWithPieces[1]);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            qty = Convert.ToInt32(strQty);
        //            piece = 0;
        //        }

        //        bPrice = Convert.ToDecimal(dt.Rows[0]["bPrice"].ToString());
        //        catName = dt.Rows[0]["catName"].ToString();
        //        imei = dt.Rows[0]["imei"].ToString();
        //    }
        //    catch
        //    {
        //        qty = 0;
        //        bPrice = 0;
        //        ScriptMessage("Product Code Not Found.", MessageType.Warning); ;
        //    }
        //}
        //<-- Function

        //--> Gridview




        protected void grdStockStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRowID = (grdStockStatus.SelectedRow.FindControl("lblID") as Label).Text;
            lblModalTitle.Text = "Company: " + (grdStockStatus.SelectedRow.FindControl("lblSupCompany") as Label).Text;
            refreshDtl();
            dtlReadOnlyMode();
        }





        protected void grdStockStatus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (commonFunction.GetCurrentTime().ToShortDateString() ==
                Convert.ToDateTime((grdStockStatus.Rows[e.NewEditIndex].FindControl("lblStatusDate") as Label).Text)
                    .ToShortDateString())
            {
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


                if (prodImei != "")
                    prodImei = "," + prodImei;

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
                            + "UPDATE StockStatusInfo SET active='0' WHERE Id='" + ID + "'"
                            + "COMMIT";
                }

                objSql.executeQuery(query);

                commonFunction.cashTransaction(stockTotal, 0, "Purchase Amount", catName, "", "", "0", "1");


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
                ((Label)e.Row.FindControl("lblQtyFooter")).Text = qtyTotal.ToString();
                ((Label)e.Row.FindControl("lblStockTotal")).Text = stockTotalTotal.ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string prodCode = ((Label)e.Row.FindControl("lblProdCode")).Text;
                decimal bPrice = Convert.ToDecimal(((Label)e.Row.FindControl("lblBPrice")).Text);
                string qty = ((Label)e.Row.FindControl("lblQty")).Text.ToString();
                ((Label)e.Row.FindControl("lblStockTotal")).Text = inventoryReturn.getItemPriceData(prodCode, bPrice, qty);
            }
        }





        //<-- Gridview

        //--> Button  
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchResult();
        }



        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                ScriptMessage("Product code is required", MessageType.Warning);
                return;
            }

            SearchReturnProduct();
        }




        protected void btnBackToReturn_Click(object sender, EventArgs e)
        {
            //checkProdCode();

            if (txtQty.Text == "")
                txtQty.Text = "0.0";

            if (Convert.ToDecimal(txtQty.Text) <= 0)
            {
                ScriptMessage("Qty must be more then ZERO", MessageType.Warning);
                return;
            }


            var stock = new Service.Stock();

            inventoryReturn.productCode = txtCode.Text;
            inventoryReturn.storeId = ddlStore.SelectedValue;
            Dictionary<string, string> dicReturnItem = inventoryReturn.getReturnOrDamageItemData();
            if (dicReturnItem.Count == 0)
            {
                ScriptMessage("Item not found.", MessageType.Warning);
                return;
            }

            int qty = Convert.ToInt32(dicReturnItem["qty"]);
            int piece = Convert.ToInt32(dicReturnItem["piece"]);
            bPrice = Convert.ToDecimal(dicReturnItem["bPrice"]);
            catName = dicReturnItem["catName"];
            string ProdIdReturn = dicReturnItem["prodID"];
            imei = commonFunction.getIMEIStoreWise(ddlStore.SelectedValue, ProdIdReturn);
            


            // input check
            string inputReturnQty = txtQty.Text;
            int inputRetQty = 0, inputRetPiece = 0;
            if (inputReturnQty.Contains("."))
            {
                string[] splitInputReturnQty = inputReturnQty.Split('.');

                inputRetQty = Convert.ToInt32(splitInputReturnQty[0]);
                inputRetPiece = Convert.ToInt32(splitInputReturnQty[1]);
            }
            else
            {
                inputRetQty = Convert.ToInt32(inputReturnQty);
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

            if (Convert.ToDecimal(txtQty.Text) < 0)
            {
                ScriptMessage("Product qty cant be negative!", MessageType.Warning);
                return;
            }


            string returnImei, availableImei = "";

            if (isImei == "1" && imei != "")
            {
                if (imei != "" && txtReturnImei.Value == "")
                {
                    ScriptMessage("Imei number required!", MessageType.Warning);
                    return;
                }
                // Update imei number

                returnImei = txtReturnImei.Value;

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
                    string reImei = splitReturnImei[i];
                    imei = imei.Replace(reImei + ',', "");
                    imei = imei.Replace(',' + reImei, "");
                }
            }


            decimal totalQty = 0, inputTotalQty = 0;
            if (txtQty.Text.Contains("."))
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
                inputTotalQty = Convert.ToDecimal(inputReturnQty);
                totalQty = qty - Convert.ToInt32(txtQty.Text);
            }

            

            // get stock total price
            string prodId = stock.getProductIDbyProdCode(txtCode.Text);
            decimal stockInputTotalPrice = stock.getStockTotalPrice(prodId, bPrice, inputTotalQty.ToString());
            decimal stockUpdateTotalPrice = stock.getStockTotalPrice(prodId, bPrice, totalQty.ToString());

            var returnQty = inputTotalQty.ToString();
            if (!returnQty.Contains("."))
            {
                returnQty = returnQty + ".0";
            }

            var lastQty = commonFunction.getLastStockQty(prodId, ddlStore.SelectedValue);
            

            var balanceQty = commonFunction.calculateQty(prodId, lastQty, returnQty, "-");

            returnImei = txtReturnImei.Value;
            query = "BEGIN TRANSACTION " +
                    "INSERT [StockStatusInfo] (prodID, prodCode, prodName, prodDescr, supCompany, catName, qty, bPrice, sPrice, weight, size, discount, stockTotal, entryDate, status, statusDate, entryQty, title, roleID, branchId, groupId, imei,transceiverId,storeId,lastQty,balanceQty)  SELECT prodID, prodCode, prodName, prodDescr, supCompany, catName, '" +
                    returnQty + "', bPrice, sPrice, weight, size, discount,  '" + stockInputTotalPrice +
                    "', entryDate, 'stockReturn', '" + commonFunction.GetCurrentTime().ToShortDateString() +
                    "', entryQty, title, '" + Session["roleId"] + "', '" + Session["branchId"] + "', '" +
                    Session["groupId"] + "', '" + returnImei + "'," + HttpContext.Current.Session["roleId"] + "," + ddlStore.SelectedValue + ",'" + lastQty + "','"+balanceQty+"' FROM [StockInfo] WHERE prodID = '" + prodId + "' " +
                    "COMMIT";
            objSql.executeQuery(query);

            // Supply
            var totalReturnAmt = bPrice * Convert.ToDecimal(returnQty);
            string supplierId = dicReturnItem["supCompany"];
            commonFunction.cashTransaction((-totalReturnAmt), 0, "Return", supplierId, "", prodId, "0", "1");

            ScriptMessage("Return Successfully", MessageType.Success);

            reset();

            searchResult();
        }





        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //query = "SELECT        stockStatus.Id, stockStatus.prodID, stockStatus.prodCode, stockStatus.prodName, stockStatus.prodDescr, stockStatus.supCompany, stockStatus.catName, " +
            //             "stockStatus.qty, stockStatus.bPrice, stockStatus.sPrice, stockStatus.weight, stockStatus.size, stockStatus.discount, stockStatus.stockTotal, stockStatus.status," +
            //             "stockStatus.entryDate, stockStatus.statusDate, stockStatus.entryQty, stockStatus.title, stockStatus.roleID, stockStatus.billNo, cat.catName AS CategoryName, "+
            //             "sup.supCompany AS Supplier "+
            //             "FROM            StockStatusInfo AS stockStatus LEFT OUTER JOIN " +
            //             "CategoryInfo AS cat ON stockStatus.catName = cat.Id LEFT OUTER JOIN "+
            //             "SupplierInfo AS sup ON sup.supID = stockStatus.supCompany";
            if (grdStockStatus.Rows.Count <= 0)
            {
                ScriptMessage("There are no data records to print ", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "Return";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        } 






        private void reset()
        {
            txtCode.Text = "";
            txtQty.Text = "";
            txtReturnImei.Value = "";
        }




        //<-- Button
        protected void ddlStore_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            SearchReturnProduct();
        }




        protected void grdStockStatus_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStockStatus.PageIndex = e.NewPageIndex;
            searchResult();
        }




        private void SearchReturnProduct()
        {
            //checkProdCode();
            inventoryReturn.productCode = txtCode.Text;
            inventoryReturn.storeId = ddlStore.SelectedValue;
            var dicReturnItem = inventoryReturn.getReturnOrDamageItemData();

            if (dicReturnItem.Count == 0)
            {
                ScriptMessage("Product is not found.", MessageType.Warning);
                pnlQty.Visible = false;
                return;
            }

            pnlQty.Visible = true;

            txtQty.Text = commonFunction.getStockQtyByUnitMeasurement(txtCode.Text, ddlStore.SelectedValue);


            bPrice = Convert.ToDecimal(dicReturnItem["bPrice"]);
            catName = dicReturnItem["catName"];
            string ProdIdReturn = dicReturnItem["prodID"];
            imei = commonFunction.getIMEIStoreWise(ddlStore.SelectedValue, ProdIdReturn);


            // Check has Imei number
            if (imei != "")
            {
                txtReturnImei.Value = imei;
                pnlReturnImei.Visible = true;
            }
            else
            {
                pnlReturnImei.Visible = false;
            }
        }


    }


}
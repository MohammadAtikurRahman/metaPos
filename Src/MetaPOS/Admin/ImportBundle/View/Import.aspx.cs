using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Presentation;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.ImportBundle.Service;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.SaleBundle.View;


namespace MetaPOS.Admin.ImportBundle.View
{
    public partial class Import : BasePage
    {
        private static CommonFunction commonFunction = new CommonFunction();

        private static string roleId, groupId, errorlist = "";
        private static int preBarcode, prodID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Import"))
                {
                    commonFunction.pageout();
                }

                var urlParameter = Request.QueryString["action"];
                //dynamicSectionBreadCrumb.InnerHtml = urlParameter.ToUpperInvariant() + " Import";

                roleId = Session["roleId"].ToString();
                groupId = Session["groupId"].ToString();

                preBarcode = generateBarcodePreNumber();
                prodID = commonFunction.GenerateNewProductId();

                if (urlParameter == "stock")
                {
                    divSupplier.Visible = true;
                    commonFunction.fillAllDdl(ddlSupplierList,
                        "SELECT supCompany,SupID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] +
                        " ORDER BY supCompany ASC", "supCompany", "SupID");
                    ddlCategoryList.Items.Insert(0, new ListItem(Resources.Language.Lbl_stockImport_search, "0"));

                    divCategory.Visible = true;
                    commonFunction.fillAllDdl(ddlCategoryList,
                    "SELECT catName,Id FROM [CategoryInfo] WHERE active='1' " + Session["userAccessParameters"] +
                    " ORDER BY catName ASC", "catName", "Id");
                    ddlCategoryList.Items.Insert(0, new ListItem(Resources.Language.Lbl_stockImport_search, "0"));

                    divStore.Visible = true;
                    commonFunction.fillAllDdl(ddlStoreList,
                    "SELECT name,Id FROM [warehouseInfo] WHERE active='1' " + Session["userAccessParameters"] +
                    " ORDER BY id ASC", "name", "Id");

                    divUnit.Visible = true;
                    commonFunction.fillAllDdl(ddlUnitList,
                    "SELECT unitName,Id FROM [unitInfo] WHERE active='1' " + Session["userAccessParameters"] +
                    " ORDER BY id ASC", "unitName", "Id");
                    ddlUnitList.Items.Insert(0, new ListItem("Piece", "0"));
                }
            }

        }





        protected void btnImport_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (fileUpload.HasFile)
                {

                    if (fileUpload.FileContent.Length > 0)
                    {
                        //string Foldername;
                        string Extension = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
                        string filename = Path.GetFileName(fileUpload.PostedFile.FileName.ToString());


                        if (Extension == ".XLS" || Extension == ".XLSX" || Extension == ".xls" || Extension == ".xlsx")
                        {
                            string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                            //string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);

                            //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                            string FolderPath = Server.MapPath("~/Files/");
                            string FilePath = (FolderPath + FileName);
                            fileUpload.SaveAs(FilePath);
                            var msg = importOperation(FilePath, Extension);
                        }
                        else
                        {
                            string title = "Warning";
                            string body = "File extension are not allowed, only allowed types: .xls, .xlsx";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblTest.Text += ex;
            }
        }




        static DataSet ds;
        private string importOperation(string FilePath, string Extension)
        {
            var isImported = "";

            // init import
            ds = initImport(FilePath, Extension);

            // get parameter
            var urlParameter = Request.QueryString["action"];
            if (urlParameter == "category")
            {
                isImported = categoryImport(ds);
            }
            else if (urlParameter == "supplier")
            {
                isImported = supplierImport(ds);
            }
            else if (urlParameter == "stock")
            {
                isImported = importMinimalStock(ds);
            }
            else if (urlParameter == "oldstock")
            {
                //Thread threadStock = new Thread(new ThreadStart(stockImport));
                //threadStock.Start();

                stockImport();
            }


            return isImported;
        }


        private string importMinimalStock(DataSet ds)
        {
            string batchSteatement = "";
            var stockModel = new StockModel();
            var batchProcess = new BatchProcess();
            var sqlProcess = new SqlProcess();

            if (ds.Tables[0].Columns.Count > 7)
            {
                string title = "Warning";
                string body = "Invalid file, please download sample then input your value.";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                return "";
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Dictionary<string, string> fieldStock = new Dictionary<string, string>();

                prodID = prodID + 1;
                var name = ds.Tables[0].Rows[i][0].ToString().Trim();
                var buyPrice = ds.Tables[0].Rows[i][1].ToString().Trim();
                var wholeSalePrice = ds.Tables[0].Rows[i][2].ToString().Trim();
                var salePrice = ds.Tables[0].Rows[i][3].ToString().Trim();
                var qty = ds.Tables[0].Rows[i][4].ToString().Trim();
                var code = ds.Tables[0].Rows[i][5].ToString().Trim();
                // var warenty = ds.Tables[0].Rows[i][6].ToString().Trim(); 
                

                buyPrice = buyPrice == "" ? "0" : buyPrice;
                wholeSalePrice = wholeSalePrice == "" ? "0" : wholeSalePrice;
                salePrice = salePrice == "" ? "0" : salePrice;
                qty = qty == "" ? "0.0" : qty ;

                //decimal aBuy = Convert.ToDecimal(buyPrice);
               // warenty = warenty == "" ? "0-0-0" : warenty;

                decimal number1 = 0;
                long number2 = 0;
                bool isNumericBuyPrice = decimal.TryParse(buyPrice, out number1);
                bool isNumericWholeSale = decimal.TryParse(wholeSalePrice, out number1);
                bool isNumericSalePrice = decimal.TryParse(salePrice, out number1);
                bool isNumericQty = long.TryParse(qty, out number2);
                if (!isNumericBuyPrice || !isNumericWholeSale || !isNumericSalePrice || !isNumericQty)
                {
                    string title = "Warning";
                    string body = "Price and Qty must be numeric value. Warning lines: " + (Convert.ToInt32(i) + 2).ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                    return "";
                }

                if (string.IsNullOrWhiteSpace(code))
                {
                    code = commonFunction.barcodeGeneratorImport(preBarcode);
                    preBarcode = preBarcode + 1; ;
                }

                var dtStock = stockModel.getItemStockDataListModelByProdCode(code);
                if (dtStock.Rows.Count > 0 || fieldStock.ContainsValue(code))
                {
                    string title = "Warning";
                    string body = "Product Code must be uniqe, Duplicate line: " + (Convert.ToInt32(i) + 2).ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                    return "";
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    string title = "Warning";
                    string body = "Product Name is required, empty line: " + Convert.ToInt32(i) + 2;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
                    return "";
                }


                var storeId = ddlStoreList.SelectedValue;
                var supplierId = ddlSupplierList.SelectedValue;
                var categoryId = ddlCategoryList.SelectedValue;
                var unitId = ddlUnitList.SelectedValue;

                var stockTotal = Convert.ToDecimal(qty)*Convert.ToDecimal(buyPrice);
                if (!qty.Contains('.'))
                {
                    qty = qty + ".0";
                }

                fieldStock.Add("prodId", prodID.ToString());
                fieldStock.Add("prodCode", code);
                fieldStock.Add("prodName", name);
                fieldStock.Add("supCompany", supplierId);
                fieldStock.Add("catName", categoryId);
                fieldStock.Add("qty", qty);
                fieldStock.Add("bPrice", buyPrice);
                fieldStock.Add("sPrice", salePrice);
                fieldStock.Add("dealerPrice", wholeSalePrice);
                fieldStock.Add("roleId", Session["roleId"].ToString());
                fieldStock.Add("groupId", Session["groupId"].ToString());
                fieldStock.Add("branchId", Session["branchId"].ToString());
                fieldStock.Add("unitId", unitId);
                fieldStock.Add("entryDate", commonFunction.GetCurrentTime().ToString());
                fieldStock.Add("warehouse", storeId);
                fieldStock.Add("warranty", "0-0-0");
                fieldStock.Add("purchaseDate", commonFunction.GetCurrentTime().ToString());
                fieldStock.Add("stockTotal",stockTotal.ToString());

                

                var sql = sqlProcess.sqlStatement(fieldStock, "stockinfo");
                batchSteatement += batchProcess.addBatch(sql);

                //StockstatusInfo
                fieldStock.Remove("warehouse");
                fieldStock.Remove("warranty");
                fieldStock.Remove("purchaseDate");

                fieldStock.Add("storeId", storeId);
                fieldStock.Add("statusDate", commonFunction.GetCurrentTime().ToString());
                fieldStock.Add("status", "stock");
                fieldStock.Add("purchaseCode", DateTime.Now.Ticks.ToString());
               

                sql = sqlProcess.sqlStatement(fieldStock, "stockstatusinfo");
                batchSteatement += batchProcess.addBatch(sql);
                fieldStock.Clear();

                //Ecommerce
                Dictionary<string, string> fieldEcommerce = new Dictionary<string, string>();
                fieldEcommerce.Add("prodCode", code);
                fieldEcommerce.Add("prodTitle", name);
                fieldEcommerce.Add("oPrice", buyPrice);
                fieldEcommerce.Add("roleId", Session["roleId"].ToString());
                fieldEcommerce.Add("entryDate", commonFunction.GetCurrentTime().ToString());
                fieldEcommerce.Add("updateDate", commonFunction.GetCurrentTime().ToString());
                fieldEcommerce.Add("branchId", Session["branchId"].ToString());
                fieldEcommerce.Add("groupId", Session["groupId"].ToString());
                fieldEcommerce.Add("prodId", prodID.ToString());


                sql = sqlProcess.sqlStatement(fieldEcommerce, "Ecommerce");
                batchSteatement += batchProcess.addBatch(sql);
                fieldEcommerce.Clear();

                // qty management
                Dictionary<string, string> fieldQtyManagement = new Dictionary<string, string>();
                fieldQtyManagement.Add("productId", prodID.ToString());
                fieldQtyManagement.Add("stockQty", qty);
                fieldQtyManagement.Add("storeId", storeId);
                fieldQtyManagement.Add("roleId", Session["roleId"].ToString());
                fieldQtyManagement.Add("branchId", Session["branchId"].ToString());
                fieldQtyManagement.Add("groupId", Session["groupId"].ToString());
                fieldQtyManagement.Add("entryDate", commonFunction.GetCurrentTime().ToString());
                fieldQtyManagement.Add("updateDate", commonFunction.GetCurrentTime().ToString());

                sql = sqlProcess.sqlStatement(fieldQtyManagement, "qtyManagement");
                batchSteatement += batchProcess.addBatch(sql);
                fieldQtyManagement.Clear();
            }
            return batchProcess.executeBatch(batchSteatement);
            
        }




        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }


        static void stockImport()
        {
            try
            {
                var importService = new ImportService();

                var dictSupplier = getDataDictImport("supId", "supCompany", "supplierInfo");
                var dictCategory = getDataDictImport("Id", "catName", "categoryInfo");
                var dictManufacturer = getDataDictImport("Id", "manufacturerName", "ManufacturerInfo");


                //
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        // check supplier exist
                        var supplierId = "0";
                        var suplierValue = ds.Tables[0].Rows[i][3].ToString().Trim();
                        if (suplierValue != "0" && suplierValue != "")
                        {
                            supplierId = dictSupplier.FirstOrDefault(x => x.Value.Trim() == suplierValue).Key;
                            if (supplierId == null)
                            {
                                supplierId = dictSupplier.FirstOrDefault(x => x.Key.Trim() == suplierValue).Key;
                            }

                            if (supplierId == null)
                            {
                                // create 
                                importService.createSupplier(suplierValue);

                                // load to dictonary
                                dictSupplier = getDataDictImport("supId", "supCompany", "supplierInfo");
                            }
                        }

                        // Check category exit
                        var catagoryValue = ds.Tables[0].Rows[i][4].ToString().Trim();
                        var categoryId = "0";
                        if (catagoryValue != "0" && catagoryValue != "")
                        {
                            categoryId = dictCategory.FirstOrDefault(x => x.Value.Trim() == catagoryValue).Key;
                            if (categoryId == null)
                            {
                                categoryId = dictCategory.FirstOrDefault(x => x.Key.Trim() == catagoryValue).Key;
                            }

                            if (categoryId == null)
                            {
                                // create 
                                importService.createCategory(catagoryValue);

                                // load to dictonary
                                dictCategory = getDataDictImport("Id", "catName", "categoryInfo");
                            }
                        }

                        // check manufacturer exit
                        var manufacturer = "0";
                        var manufacturerValue = ds.Tables[0].Rows[i][5].ToString().Trim();
                        if (manufacturerValue != "0" && manufacturerValue != "")
                        {
                            manufacturer = dictManufacturer.FirstOrDefault(x => x.Value.Trim() == manufacturerValue.Trim()).Key;
                            if (manufacturer == null)
                            {
                                manufacturer = dictManufacturer.FirstOrDefault(x => x.Key.Trim() == manufacturerValue.Trim()).Key;
                            }

                            if (manufacturer == null)
                            {
                                // create 
                                importService.createManufacturer(manufacturerValue);

                                // load to dictonary
                                dictManufacturer = getDataDictImport("Id", "manufacturerName", "ManufacturerInfo");
                            }
                        }


                        var productId = ds.Tables[0].Rows[i][0].ToString();
                        var productName = ds.Tables[0].Rows[i][1].ToString();
                        var productCode = ds.Tables[0].Rows[i][2].ToString();
                        var stockQty = ds.Tables[0].Rows[i][6].ToString();
                        var buyPrice = ds.Tables[0].Rows[i][7].ToString();
                        var WholeSalePrice = ds.Tables[0].Rows[i][8].ToString();
                        var salePrice = ds.Tables[0].Rows[i][9].ToString();
                        var CompanyPrice = ds.Tables[0].Rows[i][10].ToString();
                        var sku = ds.Tables[0].Rows[i][11].ToString();
                        var unitId = ds.Tables[0].Rows[i][12].ToString();
                        var storeId = ds.Tables[0].Rows[i][13].ToString();

                        if (!stockQty.Contains('.'))
                        {
                            stockQty = stockQty + ".0";
                        }

                        if (!string.IsNullOrEmpty(productName))
                        {
                            var stockModel = new StockModel();

                            if (string.IsNullOrEmpty(productCode))
                            {
                                stockModel.prodCode = commonFunction.barcodeGeneratorImport(preBarcode);
                                preBarcode++;
                            }
                            else
                            {
                                stockModel.prodCode = productCode;
                            }

                            var newProductId = 0;
                            if (!string.IsNullOrEmpty(productId))
                            {
                                newProductId = Convert.ToInt32(productId);
                            }
                            else
                            {
                                prodID++;
                                newProductId = prodID;
                            }


                            stockModel.prodId = newProductId;
                            stockModel.prodName = productName;
                            stockModel.prodDescr = "";
                            stockModel.supCompany = supplierId;
                            stockModel.catName = categoryId;
                            stockModel.qty = stockQty;
                            stockModel.bPrice = Convert.ToDecimal(buyPrice);
                            stockModel.sPrice = Convert.ToDecimal(salePrice);
                            stockModel.dealerPrice = Convert.ToDecimal(WholeSalePrice);
                            stockModel.weight = 0M;
                            stockModel.size = "";
                            stockModel.discount = 0;
                            stockModel.stockTotal = 0;
                            stockModel.entryQty = stockQty;
                            stockModel.title = "";
                            stockModel.branchName = "";
                            stockModel.fieldAttribute = "";
                            stockModel.tax = "0";
                            stockModel.sku = "0";
                            stockModel.lastQty = stockQty;
                            stockModel.warningQty = "0";
                            stockModel.imei = "";
                            stockModel.warranty = "0-0-0";
                            //stockModel.dealerPrice = 0;
                            stockModel.purchaseCode = ""; //commonFunction.nextPurchaseCode();
                            stockModel.roleId = roleId;
                            stockModel.createdFor = roleId;
                            stockModel.receivedDate = DateTime.Now;
                            stockModel.expiryDate = DateTime.Now;
                            stockModel.entryDate = DateTime.Now;
                            stockModel.batchNo = "";
                            stockModel.serialNo = "";
                            stockModel.ShipmentStatus = 0;
                            stockModel.Status = "stock";
                            stockModel.searchType = "product";
                            stockModel.manufacturerId = Convert.ToInt32(manufacturer);
                            stockModel.notes = "";
                            stockModel.unitId = Convert.ToInt32(unitId);
                            stockModel.storeId = Convert.ToInt32(storeId);
                            stockModel.SupCommission = 0;
                            stockModel.purchaseDate = DateTime.Now;
                            stockModel.imeiStatus = '0';
                            stockModel.engineNumber = "";
                            stockModel.cecishNumber = "";
                            stockModel.comPrice = 0;
                            stockModel.locationId = 0;
                            stockModel.freeQty = "0";
                            stockModel.billNo = "0";
                            stockModel.fieldRecord = "0";
                            stockModel.attributeRecord = "0";
                            stockModel.parentId = "0";
                            stockModel.roleId = roleId;
                            stockModel.groupId = groupId;
                            stockModel.balanceQty = stockQty;
                            stockModel.storeId = Convert.ToInt32(storeId);
                            stockModel.entryDate = commonFunction.GetCurrentTime();
                            stockModel.updateDate = commonFunction.GetCurrentTime();

                            //Thread.Sleep(100);
                            // Create stock
                            var isSaved = stockModel.createStock();

                            // Create Stock Qty
                            isSaved = stockModel.createStockQtyManagement();

                            // Create StockStatusInfo
                            isSaved = stockModel.createStockStatus();
                        }
                        else
                        {
                            //errorlist += (i + 1).ToString() + ",";
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                //success msg 
               // ScriptMessage("Product imported successfully", MessageType.Success);
               
            }
            catch (Exception)
            {
            }
        }


        private static int generateBarcodePreNumber()
        {
            string barcodeStr1 = "", barcodeStr2 = "";
            int barcodeInt1, barcodeInt2;

            var sqlOperation = new SqlOperation();

            int digit = 11;

            var ds =
                sqlOperation.getDataSet(
                    "SELECT TOP 1 prodCode FROM [StockInfo] WHERE prodCode LIKE 'ZZ-%[0-9]' AND LEN(prodCode)='" + digit +
                    "' ORDER BY prodCode DESC");
            var ds2 =
                sqlOperation.getDataSet(
                    "SELECT TOP 1 prodCode FROM [StockStatusInfo] WHERE prodCode LIKE 'ZZ-%[0-9]' AND LEN(prodCode)='" +
                    digit + "' ORDER BY prodCode DESC");

            try
            {
                barcodeStr1 = ds.Tables[0].Rows[0][0].ToString();
                barcodeInt1 = Convert.ToInt32(barcodeStr1.Remove(0, 3)) + 1;
            }
            catch
            {
                barcodeInt1 = 1;
            }

            try
            {
                barcodeStr2 = ds2.Tables[0].Rows[0][0].ToString();
                barcodeInt2 = Convert.ToInt32(barcodeStr2.Remove(0, 3)) + 1;
            }
            catch
            {
                barcodeInt2 = 1;
            }

            return barcodeInt2 > barcodeInt1 ? barcodeInt2 : barcodeInt1;
        }

        private static string getIdByValue(string value, string column, string columnSearch, string table)
        {
            var importModel = new ImportModel();
            var dtValue = importModel.getIdByValueModel(value, column, columnSearch, table);
            if (dtValue.Rows.Count > 0)
            {
                return dtValue.Rows[0][0].ToString();
            }
            else
            {
                // crete this
            }
            return "";
        }

        private static Dictionary<string, string> getDataDictImport(string key, string value, string table)
        {
            var dict = new Dictionary<string, string>();
            var importModel = new ImportModel();
            var dtValue = importModel.getDataDictImportModel(key, value, table);
            for (int i = 0; i < dtValue.Rows.Count; i++)
            {
                dict.Add(dtValue.Rows[i]["dictKey"].ToString(), dtValue.Rows[i]["distValue"].ToString());
            }

            return dict;
        }

        private string supplierImport(DataSet ds)
        {
            var importModel = new ImportModel();
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var name = ds.Tables[0].Rows[i][0].ToString();
                    var Id = ds.Tables[0].Rows[i][1].ToString();

                    if (!string.IsNullOrEmpty(name) && !importExistValueChecker(name, "supCompany", "SupplierInfo"))
                    {

                        importModel.supId = Id;
                        importModel.supplierName = name;
                        importModel.saveImportedSupplier();
                    }
                }
            }
            catch (Exception)
            {

            }

            return "";
        }





        private string generateSupplierID(string suplierId)
        {


            //var importModel = new ImportModel();
            //var dtSupplier = importModel.checkSupplierID(suplierId);

            //if (dtSupplier.Rows.Count > 0)
            //{

            //    generateSupplierID(suplierId);
            //}

            //suplierId = ;

            return suplierId;
        }

        private string categoryImport(DataSet ds)
        {
            var importModel = new ImportModel();
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var name = ds.Tables[0].Rows[i][0].ToString();

                    if (!string.IsNullOrEmpty(name) && !importExistValueChecker(name, "catName", "CategoryInfo"))
                    {
                        importModel.catName = name;
                        importModel.saveImportedCategory();
                    }
                }
            }
            catch (Exception)
            {

            }

            return "";
        }





        private bool importExistValueChecker(string value, string column, string table)
        {
            var importModel = new ImportModel();
            var dtImport = importModel.importExistValueCheckerModel(value, column, table);
            if (dtImport.Rows.Count > 0)
                return true;
            else
                return false;
        }


        private DataSet initImport(string FilePath, string Extension)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                        .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                        .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            //string query = "SELECT * From [" + SheetName + "]";
            //DataSet ds = objSql.getDataSet(query);

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    string branchId = ds.Tables[0].Rows[i][0].ToString();
            //    lblTest.Text += branchId + ", ";
            //}

            connExcel.Open();
            cmdExcel.CommandText = String.Format("SELECT * From [" + SheetName + "] ");
            oda.SelectCommand = cmdExcel;
            oda.Fill(ds);
            connExcel.Close();

            if (Directory.Exists(FilePath))
                Directory.Delete(FilePath);

            return ds;
        }


        private void Import_To_Grid(string FilePath, string Extension)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                        .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                        .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            //string query = "SELECT * From [" + SheetName + "]";
            //DataSet ds = objSql.getDataSet(query);

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    string branchId = ds.Tables[0].Rows[i][0].ToString();
            //    lblTest.Text += branchId + ", ";
            //}

            connExcel.Open();
            cmdExcel.CommandText = String.Format("SELECT * From [" + SheetName + "] ");
            oda.SelectCommand = cmdExcel;
            oda.Fill(ds);
            connExcel.Close();

            //if (Directory.Exists(FilePath))
            //    Directory.Delete(FilePath);

            //int rCnt, counter = 0, contiErrorCounter = 0;
            //string pName, pDescritopn, errorRows = "", duppliRows = "";
            //string sku = "", store_id = "";

            //string supplierValue = "";

            //List<string> updateList = new List<string>();

            //int count = ds.Tables[0].Rows.Count;

            //// prod ID
            //int _prodID = 0;
            //DataSet dsCount, dsProdID;
            //dsCount = objSql.getDataSet("Select * FROM StockInfo");
            //if (dsCount.Tables[0].Rows.Count > 0)
            //{
            //    dsProdID = objSql.getDataSet("SELECT MAX(prodId) as ProdID FROM StockInfo");
            //    _prodID = Convert.ToInt32(dsProdID.Tables[0].Rows[0][0]);

            //}
            //else
            //{
            //    _prodID = 0;
            //}


            //prodId = _prodID + 1;

            //try
            //{

            //    if (count > 0)
            //    {
            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            var _productName = ds.Tables[0].Rows[i][4].ToString();
            //            bool isExists = objCommonFun.productExists(_productName);
            //            if (isExists)
            //            {
            //                duppliRows += (i + 2) + ", ";

            //            }
            //            bool isExistsProdCode = false;
            //            var _prodCode = "";
            //            try
            //            {
            //                _prodCode = ds.Tables[0].Rows[i][9].ToString();
            //                isExistsProdCode = objCommonFun.prodCodeExists(_prodCode);
            //                if (isExistsProdCode)
            //                {
            //                    duppliRows += (i + 2) + ", ";

            //                }
            //            }
            //            catch (Exception)
            //            {

            //            }

            //            var _storeId = ds.Tables[0].Rows[i][0].ToString();
            //            var _Supplier = ds.Tables[0].Rows[i][1].ToString();
            //            var _Category = ds.Tables[0].Rows[i][2].ToString();
            //            var _sku = ds.Tables[0].Rows[i][3].ToString();

            //            var _qty = ds.Tables[0].Rows[i][5].ToString();
            //            var _bPrice = ds.Tables[0].Rows[i][6].ToString();
            //            var _dPrice = ds.Tables[0].Rows[i][7].ToString();
            //            var _sPrice = ds.Tables[0].Rows[i][8].ToString();

            //            var _unitId = ds.Tables[0].Rows[i][10].ToString();


            //            // 
            //            if (String.IsNullOrEmpty(_qty))
            //                _qty = "0.0";

            //            if (String.IsNullOrEmpty(_bPrice))
            //                _bPrice = "0";

            //            if (String.IsNullOrEmpty(_dPrice))
            //                _dPrice = "0";

            //            if (String.IsNullOrEmpty(_sPrice))
            //                _sPrice = "0";
            //            if (String.IsNullOrEmpty(_unitId) || !stockModel.checkUnitIdExist(_unitId))
            //                _unitId = "0";

            //            int num1;
            //            bool _isSupCompany = int.TryParse(_Supplier, out num1);
            //            bool _isCatName = int.TryParse(_Category, out num1);
            //            bool _isQty = int.TryParse(_qty, out num1);
            //            bool _isBPrice = int.TryParse(_bPrice, out num1);
            //            bool _isWholesalePrice = int.TryParse(_dPrice, out num1);
            //            bool _isSalePrice = int.TryParse(_sPrice, out num1);


            //            // Check supplier validation
            //            var dtSupplier =
            //                objSql.getDataTable("SELECT * FROM SupplierInfo WHERE supID = '" + _Supplier + "'");
            //            var dtCategory =
            //                objSql.getDataTable("SELECT * FROM CategoryInfo WHERE Id = '" + _Category + "'");

            //            var dtStore =
            //                objSql.getDataTable("SELECT * FROM WarehouseInfo WHERE Id !='' AND Id = '" + _storeId + "'");

            //            if (!isExists
            //                && !isExistsProdCode
            //                && !string.IsNullOrEmpty(_productName)
            //                && !string.IsNullOrEmpty(_Supplier)
            //                && !string.IsNullOrEmpty(_Category)
            //                && (_isSupCompany)
            //                && (_isCatName)
            //                && (_isQty)
            //                && (_isBPrice)
            //                && (_isWholesalePrice)
            //                && (_isSalePrice)
            //                && dtSupplier.Rows.Count > 0
            //                && dtCategory.Rows.Count > 0
            //                && dtStore.Rows.Count > 0 &&
            //                _prodCode != "0" &&
            //                _unitId != "")
            //            {
            //                if ((string.IsNullOrEmpty(_qty)))
            //                    _qty = "0.0";
            //                if ((string.IsNullOrEmpty(_bPrice)))
            //                    _bPrice = "0";
            //                if (string.IsNullOrEmpty(_dPrice))
            //                    _dPrice = "0";
            //                if (string.IsNullOrEmpty(_sPrice))
            //                    _sPrice = "0";

            //                if (!_qty.Contains("."))
            //                {
            //                    _qty = _qty + ".0";
            //                }

            //                if (_prodCode == "")
            //                    setBarcode = BarCodeGeneratorForBulkStock();
            //                else
            //                    setBarcode = _prodCode;

            //                dicProductListCreate.Add("prodId" + stockCounter, (prodId + i).ToString());
            //                dicProductListCreate.Add("prodCode" + stockCounter, setBarcode);
            //                dicProductListCreate.Add("prodName" + stockCounter, _productName.Replace("\'", ""));
            //                dicProductListCreate.Add("prodDescr" + stockCounter, "");
            //                dicProductListCreate.Add("supCompany" + stockCounter, _Supplier);
            //                dicProductListCreate.Add("catName" + stockCounter, _Category);
            //                dicProductListCreate.Add("qty" + stockCounter, _qty);
            //                dicProductListCreate.Add("bPrice" + stockCounter, _bPrice);
            //                dicProductListCreate.Add("sPrice" + stockCounter, _sPrice);
            //                dicProductListCreate.Add("weight" + stockCounter, "0.00");
            //                dicProductListCreate.Add("size" + stockCounter, "");
            //                dicProductListCreate.Add("discount" + stockCounter, "0.00");
            //                dicProductListCreate.Add("stockTotal" + stockCounter, (Convert.ToDecimal(_bPrice) * Convert.ToDecimal(_qty)).ToString());
            //                dicProductListCreate.Add("entryDate" + stockCounter, objCommonFun.GetCurrentTime().ToShortDateString());
            //                dicProductListCreate.Add("entryQty" + stockCounter, "0");
            //                dicProductListCreate.Add("title" + stockCounter, "");
            //                dicProductListCreate.Add("branchName" + stockCounter, "");
            //                dicProductListCreate.Add("fieldAttribute" + stockCounter, "");
            //                dicProductListCreate.Add("tax" + stockCounter, "");
            //                dicProductListCreate.Add("sku" + stockCounter, _sku);
            //                dicProductListCreate.Add("lastQty" + stockCounter, "0");
            //                dicProductListCreate.Add("warranty" + stockCounter, "");
            //                dicProductListCreate.Add("imei" + stockCounter, "");
            //                dicProductListCreate.Add("warningQty" + stockCounter, "0");
            //                dicProductListCreate.Add("dealerPrice" + stockCounter, _dPrice);
            //                dicProductListCreate.Add("purchaseCode" + stockCounter, "");
            //                dicProductListCreate.Add("createdFor" + stockCounter, "");
            //                dicProductListCreate.Add("receivedDate" + stockCounter, objCommonFun.GetCurrentTime().ToShortDateString());
            //                dicProductListCreate.Add("expiryDate" + stockCounter, objCommonFun.GetCurrentTime().ToShortDateString());
            //                dicProductListCreate.Add("batchNo" + stockCounter, "");
            //                dicProductListCreate.Add("serialNo" + stockCounter, "");
            //                dicProductListCreate.Add("shipmentStatus" + stockCounter, "0");
            //                dicProductListCreate.Add("manufacturer" + stockCounter, "0");
            //                dicProductListCreate.Add("notes" + stockCounter, "");
            //                dicProductListCreate.Add("unitId" + stockCounter, _unitId);
            //                dicProductListCreate.Add("piece" + stockCounter, "0");
            //                dicProductListCreate.Add("warehouse" + stockCounter, _storeId);
            //                dicProductListCreate.Add("supCommission" + stockCounter, "0");
            //                dicProductListCreate.Add("engineNumber" + stockCounter, "");
            //                dicProductListCreate.Add("cecishNumber" + stockCounter, "");
            //                dicProductListCreate.Add("comPrice" + stockCounter, "0");
            //                dicProductListCreate.Add("location" + stockCounter, "0");
            //                dicProductListCreate.Add("freeQty" + stockCounter, "0.0");
            //                dicProductListCreate.Add("variant" + stockCounter, "");


            //                supplierValue = dicProductListCreate["supCompany" + stockCounter];

            //                arrInsertData[stockCounter] = setBarcode;


            //                // Add ecommerce info
            //                dicEcommerceCreate.Add("eprodCode" + stockCounter, setBarcode);
            //                dicEcommerceCreate.Add("image" + stockCounter, "");
            //                dicEcommerceCreate.Add("prodTitle" + stockCounter, _productName);
            //                dicEcommerceCreate.Add("oPrice" + stockCounter, _sPrice);
            //                dicEcommerceCreate.Add("prodId" + stockCounter, (prodId + i).ToString()); ;

            //                //
            //                updateList.Add("insert-");
            //                stockCounter++;
            //                insertCount++;


            //            }
            //            else
            //            {
            //                if (!isExists)
            //                    errorRows += (i + 2) + ", ";
            //            }

            //            if (_productName == "")
            //            {
            //                contiErrorCounter++;
            //            }
            //            else
            //            {
            //                // err count reinitilize
            //                contiErrorCounter = 0;
            //            }


            //            // product name null > 10 return;
            //            if (contiErrorCounter > 10)
            //                break;


            //        }
            //        errorRows = errorRows.TrimEnd();



            //    }
            //}
            //catch (Exception ex)
            //{
            //    return ex.ToString();
            //}

            ////Bind Data to GridView
            //GridView1.Caption = Path.GetFileName(FilePath);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }



    }
}
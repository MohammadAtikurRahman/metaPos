using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Web.UI.WebControls;
using MetaPOS.Admin.InventoryBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using ListItem = System.Web.UI.WebControls.ListItem;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.Controller;
using System.Web.Services;
using System.Web.Script.Serialization;


namespace MetaPOS.Admin.InventoryBundle.View
{


    public partial class StockBulkOpt : Page
    {
        private CommonFunction commonFunction = new CommonFunction();
        private SqlOperation objSql = new SqlOperation();
        // Model 
        private StockModel stockModel = new StockModel();
        private AttributeDetailModel objAttrDeailModel = new AttributeDetailModel();

        //Controller
        private StockController objStockController = new StockController();
        private ApiController objApiController = new ApiController();


        private Service.Stock inventoryStock = new Service.Stock();


        private DataSet ds, dsLoadField;


        public enum MessageType
        {
            Success,
            Error,
            Info,
            Warning


        };


        private Label[] lblField = new Label[50000];
        private DropDownList[] ddlAttribute = new DropDownList[50000];
        private TextBox[] txtAttribute = new TextBox[50000];

        // Product dictionary
        private static Dictionary<string, string> dicProductListCreate = new Dictionary<string, string>();
        private static Dictionary<string, string> dicProductListUpdate = new Dictionary<string, string>();
        private static Dictionary<string, string> dicProductConditionData = new Dictionary<string, string>();
        private static Dictionary<string, string> dicSupplierList = new Dictionary<string, string>();
        private static Dictionary<string, string> dicAttributeData = new Dictionary<string, string>();
        private static Dictionary<string, string> dicEcommerceCreate = new Dictionary<string, string>();
        private static Dictionary<string, string> dicEcommerceUpdate = new Dictionary<string, string>();

        private static string
            WarrantyDate = "",
            isWariningQty = "",
            setBarcode = "",
            isApiEcomm = "",
            whichPage = "",
            getProductId = "",
            imageName = "";

        private static bool checkingExist;
        private static int fieldCount, j, k;
        private static decimal entryTotal, priceTotalList = 0;
        public int jvantConter { get; set; }

        private static int stockCounter = 0,
            supplierCounter = 0,
            stockCounterUpdate = 0,
            //counter = 0,
            prodId = 0,
            barCodeCounter = 0,
            supplierId = 0,
            productUpdate = 0,
            productInsert = 0,
            barcodeCounter = 0;

        private static string barcode = "", barCodeTmp = "", isImeiAvailable = "";
        private static decimal priceTotal = 0;

        public static bool accessProduct = false,
            accessbarCode = false,
            isProductUpdate = false,
            isProductInsert = true,
            isSearchByProdId = false, isImportProduct = false;

        private static int updateCount = 0, eCounter = 0;
        private static int insertCount = 0, i = 0, attributeCount = 0, attributeInputCounter = 0;
        private static string updateListStatus = "", storeId = "", status = "";

        // Supplier List
        private static Label[] lblSL = new Label[50000];
        private static Label[] lblProdName = new Label[50000];
        private static Label[] lblbPrice = new Label[50000];
        private static Label[] lblQty = new Label[50000];
        private static Label[] lblTotalPriceList = new Label[50000];
        private static Label[] lblsPrice = new Label[50000];
        private static Label[] lblSupplier = new Label[50000];
        private static Button[] btnCancel = new Button[50000];

        // Insert and update 
        private static string[] arrInsertData = new string[50000];
        private static string[] arrUpdateData = new string[50000];
        private static string[] arrSupplierId = new string[50000];

        // barcode
        //private static Label lblBarCodeGen = new Label();

        protected void Page_Load(object sender, EventArgs e)
        {
            UpsertAttributeDetail("");

            loadProductVarient();

            imageName = txtImgFileName.Value;

            if (!IsPostBack)
            {
                // Reset all
                MultiReset();

                if (whichPage == "unit")
                {
                    if (!commonFunction.accessChecker("Stock"))
                    {
                        var obj = new CommonFunction();
                        obj.pageout();
                    }
                }
                else
                {
                    if (!commonFunction.accessChecker("Purchase"))
                    {
                        var obj = new CommonFunction();
                        obj.pageout();
                    }
                }

                if (!isImportProduct)
                {
                    MultiReset();
                }


                // Load Shipment status information
                LoadShipmentStatusInfo();

                // Get stock page info
                whichPage = Request["page"];

                if (whichPage == "unit")
                {
                    divProductList.Visible = false;
                    btnAdd.Text = "Save";
                    lblStockTitle.Text = "Product";
                    divUnitPurchaseAmt.Visible = true;
                    txtUnitTotalPurchaseAmt.Text = "0";
                }

                // Store ID
                storeId = Request["storeId"];
                if (storeId == "")
                    storeId = HttpContext.Current.Session["storeId"].ToString();


                status = Request["status"];



                //
                isApiEcomm = commonFunction.findSettingItemValueDataTable("apiEcomm");

                // search branch info
                //selectBranch();




                // barcode temp
                barCodeTmp = commonFunction.barcodeGenerator();

                //-->Check Item to visable 
                if (commonFunction.findSettingItemValueDataTable("sku") == "0")
                    divSku.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("tax") == "0")
                    divTax.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("imei") == "0")
                {
                    divImei.Visible = false;
                    chkIMEIEnable.Checked = false;
                }
                else
                    chkIMEIEnable.Checked = true;

                if (commonFunction.findSettingItemValueDataTable("warranty") == "0")
                    divWarranty.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isWarningQty") == "0")
                    divWaringinQty.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isWholeSeller") == "1")
                    divDealerPrice.Visible = true;

                if (commonFunction.findSettingItemValueDataTable("isImport") == "0" || whichPage == "unit")
                    divImport.Visible = false;
                else
                    divImport.Visible = true;

                // Manufacturer setting
                if (commonFunction.findSettingItemValueDataTable("manufacturer") == "0")
                    divManufacturer.Visible = false;

                // Upoad status
                if (commonFunction.findSettingItemValueDataTable("upload") == "0")
                    divUploader.Visible = false;

                // Shipment Status Setting
                if (commonFunction.findSettingItemValueDataTable("shipment") == "0")
                    divShipmentStatus.Visible = false;

                // Size field attr
                if (commonFunction.findSettingItemValueDataTable("size") == "0")
                    divSize.Visible = false;


                if (commonFunction.findSettingItemValueDataTable("customField") == "0")
                {
                    divFieldList.Visible = false;
                    divAttributeList.Visible = false;
                }


                if (commonFunction.findSettingItemValueDataTable("isUnit") == "0")
                {
                    divUnit.Visible = false;
                }

                if (commonFunction.findSettingItemValueDataTable("isWarehouse") == "0")
                {
                    divWarehouse.Visible = true;

                }

                if (commonFunction.findSettingItemValueDataTable("isRecivedDate") == "0")
                    divRecivedDate.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isExpiryDate") == "0")
                    divExpireDate.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isBatchNo") == "0")
                    divBatchNo.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isSerialNo") == "0")
                    divSerialNo.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isSupplierCom") == "0")
                    divSupplierCom.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("displaySchedulePayment") == "0")
                    divSchedulePayment.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("displaySupplierReceived") == "0")
                    divSupplierReceived.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isCecishNumber") == "0")
                    divCecishNumber.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("isEngineNumber") == "0")
                    divEngineNumber.Visible = false;

                if (commonFunction.findSettingItemValueDataTable("displayComPrice") == "0")
                    divCompanyPrice.Visible = false;
                if (commonFunction.findSettingItemValueDataTable("offer") == "0")
                    divFreeQty.Visible = false;
                if (commonFunction.findSettingItemValueDataTable("displayVariant") == "0")
                    divVariantControl.Visible = false;




                //load Dealer Price Percentance
                loadddlBuyPrice();


                // Product ID
                prodId = GenerateProductID();


                commonFunction.fillAllDdl(ddlSup, "SELECT supCompany,SupID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC",
                    "supCompany", "SupID");
                commonFunction.fillAllDdl(ddlCat, "SELECT catName,Id FROM [CategoryInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY catName ASC", "catName",
                    "Id");
                commonFunction.fillAllDdl(ddlUnit, "SELECT unitName,Id FROM [UnitInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY unitName ASC", "unitName", "Id");
                ddlUnit.Items.Insert(0, new ListItem("Piece", "0"));

                commonFunction.fillAllDdl(ddlWarehouse, "SELECT name,Id FROM [WarehouseInfo] WHERE active='1' AND name !='' " + Session["userAccessParameters"] + " ORDER BY name ASC", "name",
                    "Id");

                if (commonFunction.findSettingItemValueDataTable("displayLocation") == "1")
                {
                    divLocation.Visible = true;
                    commonFunction.fillAllDdl(ddlLocationList, "SELECT name,Id FROM [LocationInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY name ASC", "name",
                        "Id");

                }
                ddlLocationList.Items.Insert(0, new ListItem("Select", "0"));

                // Store Selected value
                var dtSelectedStore = objSql.getDataTable("SELECT storeId FROM RoleInfo where roleId='" + Session["roleId"] + "'");

                if (dtSelectedStore.Rows.Count > 0)
                    ddlWarehouse.SelectedValue = dtSelectedStore.Rows[0]["storeId"].ToString();




                if (commonFunction.findSettingItemValueDataTable("manufacturer") == "1")
                    commonFunction.fillAllDdl(ddlManufacturer, "SELECT manufacturerName,Id FROM [ManufacturerInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY manufacturerName ASC", "manufacturerName", "Id");


                ddlManufacturer.Items.Insert(0, new ListItem("Select", "0"));
                ddlSup.Items.Insert(0, new ListItem("Select", "0"));
                ddlCat.Items.Insert(0, new ListItem("Select", "0"));


                // Gererate Purchase Code
                txtPurchaseCode.Text = commonFunction.nextPurchaseCode();

                // Get Product code
                getProductId = Request["productId"];

                if (getProductId != null)
                {
                    isSearchByProdId = true;
                    lblProdID.Text = getProductId;
                    txtSearchNameCode.Text = getProductId;
                    btnLoadProductDetails_Click(null, null);
                }
                else
                {
                    isSearchByProdId = false;
                }

                txtPurchaseDate.Text = txtSchedulePaymentDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yy");


                string generateBarcodeType = commonFunction.findSettingItemValueDataTable("generateBardCodeType");
                if (generateBarcodeType == "1")
                {
                    divBarcodeGenerator.Visible = false;
                    if (getProductId == null)
                        btnGenerateBarCode_Click(null, null);
                }


                var dtStore = objSql.getDataTable("SELECT * FROM RoleInfo WHERE userRight='Branch' and roleId='" + Session["roleId"] + "' ");
                if (dtStore.Rows.Count == 0)
                    divWarehouse.Visible = false;

                // Check for USER
                if (Session["userRight"].ToString() == "Regular")
                {
                    if (commonFunction.findSettingItemValueDataTable("displayBuyPriceForUser") == "0")
                        divCostPrice.Visible = false;
                }

            }

            //Warranty Date 
            WarrantyDate = ddlWarrantyYear.SelectedValue + "-" + ddlWarrantyMonth.SelectedValue + "-" +
                           ddlWarrantyDays.SelectedValue;
        }


        protected void btnLoadProductDetails_Click(object sender, ImageClickEventArgs e)
        {
            //load tag javascript
            //
            loadTagJs();
            //

            if (txtSearchNameCode.Text == "")
            {
                ScriptMessage("Product name is not valid.", MessageType.Warning);
                ShowShoppingList();
                return;
            }


            if (string.IsNullOrEmpty(storeId))
                storeId = Session["storeId"].ToString();


            var prouctId = lblProdID.Text;
            if (!isSearchByProdId)
            {
                var searchProduct = txtSearchNameCode.Text;
                var stock = new Service.Stock();
                prouctId = stock.getProductIdBySearchProduct(searchProduct);
            }


            var dtStock = commonFunction.LoadProductDetail(prouctId, storeId);
            if (dtStock.Rows.Count <= 0)
            {
                ScriptMessage("Product is not found", MessageType.Warning);
                return;
            }

            Reset();

            if (dicSupplierList.Count > 0)
            {
                foreach (DataRow row in dtStock.Rows)
                {
                    string supValue = row["supCompany"].ToString();
                    if (supValue == "")
                        supValue = "0";
                    if (!dicSupplierList.ContainsValue(supValue))
                    {
                        ScriptMessage("Supplier is not same", MessageType.Warning);
                        ddlSup.Enabled = false;
                        ddlSup.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
                        string generateBarcodeType = commonFunction.findSettingItemValueDataTable("generateBardCodeType");
                        if (generateBarcodeType == "1")
                        {
                            btnGenerateBarCode_Click(null, null);
                        }
                        ShowShoppingList();

                        return;
                    }


                    string prodCodeValue = row["prodCode"].ToString();
                    if (dicProductListUpdate.ContainsValue(prodCodeValue) ||
                        dicProductListCreate.ContainsValue(prodCodeValue))
                    {
                        ScriptMessage("Product already added", MessageType.Warning);
                        ddlSup.Enabled = false;
                        ddlSup.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
                        ShowShoppingList();
                        return;
                    }
                }
            }
            else
            {
                ShowShoppingList();
            }


            if (dtStock.Rows.Count > 0)
            {
                string supValue, catValue, prodValue, prodCodeValue;

                if (accessProduct)
                {
                    foreach (DataRow row in dtStock.Rows)
                    {
                        checkingExist = true;

                        supValue = row["supCompany"].ToString();
                        if (supValue == "")
                            supValue = "0";

                        catValue = row["catName"].ToString();
                        if (catValue == "")
                            catValue = "0";

                        prodValue = row["prodName"].ToString();
                        if (prodValue == "")
                            prodValue = "0";

                        prodCodeValue = row["prodCode"].ToString();
                        if (prodCodeValue == "")
                            prodCodeValue = "";

                        if (dicSupplierList.Count > 0)
                        {
                            if (!dicSupplierList.ContainsValue(supValue))
                            {
                                ScriptMessage("Supplier is not same", MessageType.Warning);
                                ddlSup.Enabled = false;
                                ddlSup.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
                                ShowShoppingList();
                                return;
                            }
                        }

                        ddlSup.SelectedValue = supValue;
                        ddlCat.SelectedValue = catValue;
                        txtProduct.Text = prodValue;
                        txtScanCode.Text = prodCodeValue;
                        ddlWarehouse.SelectedValue = storeId;


                        ddlLocationList.SelectedValue = row["locationId"].ToString();


                        accessbarCode = true;
                        // Product Update false
                        isProductUpdate = false;
                        isProductInsert = true;
                        productInsert++;

                        ddlSup.Enabled = txtScanCode.Enabled = btnGenerateBarCode.Enabled = false;
                        ddlSup.BackColor = txtScanCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
                    }
                }
                else
                {
                    foreach (DataRow row in dtStock.Rows)
                    {
                        accessbarCode = true;
                        checkingExist = true;
                        // Product Update true
                        isProductUpdate = true;
                        isProductInsert = false;
                        productUpdate++;

                        btnAdd.Text = "Update";


                        supValue = row["supCompany"].ToString();
                        if (supValue == "")
                            supValue = "0";

                        catValue = row["catName"].ToString();
                        if (catValue == "")
                            catValue = "0";

                        prodValue = row["prodName"].ToString();
                        if (prodValue == "")
                            prodValue = "0";

                        prodCodeValue = row["prodCode"].ToString();
                        if (prodCodeValue == "")
                            prodCodeValue = "";



                        //
                        lblProdID.Text = row["prodID"].ToString();
                        txtScanCode.Text = prodCodeValue;
                        txtProduct.Text = prodValue;
                        ddlSup.SelectedValue = supValue;
                        ddlCat.SelectedValue = catValue;
                        txtBPrice.Text = row["bPrice"].ToString();
                        lblBPrice.Text = row["bPrice"].ToString();
                        txtSPrice.Text = row["sPrice"].ToString();
                        txtSize.Text = row["size"].ToString();
                        txtTax.Text = row["tax"].ToString();
                        txtSku.Text = row["sku"].ToString();
                        ddlUnit.SelectedValue = row["unitId"].ToString();
                        ddlWarehouse.SelectedValue = storeId;
                        // Qty Sepearate
                        lblCurrentQty.Text = commonFunction.getStockQtyByUnitMeasurement(prodCodeValue, storeId);



                        //Warranty Data edit
                        WarrantyDate = row["warranty"].ToString();
                        if (WarrantyDate != "")
                        {
                            if (WarrantyDate == "0")
                            {
                                WarrantyDate = "0-0-0";
                            }
                            string[] wDates = WarrantyDate.Split('-');
                            if (wDates.Length == 3)
                            {
                                ArrayList wDateList = new ArrayList();
                                foreach (string wDate in wDates)
                                {
                                    wDateList.Add(wDate);
                                }
                                ddlWarrantyYear.SelectedValue = wDateList[0].ToString();
                                ddlWarrantyMonth.SelectedValue = wDateList[1].ToString();
                                ddlWarrantyDays.SelectedValue = wDateList[2].ToString();
                            }
                        }

                        txtIMEI.Value = commonFunction.getIMEIStoreWise(storeId, prouctId);
                        txtWarningQty.Text = row["warningQty"].ToString();
                        txtDealerPrice.Text = row["dealerPrice"].ToString();
                        ddlUnit.SelectedValue = row["unitId"].ToString();
                        txtSuplierCommission.Text = row["commission"].ToString();

                        ddlLocationList.SelectedValue = row["locationId"].ToString();


                        txtRecivedDate.Text = Convert.ToDateTime(row["receivedDate"]).ToString("dd-MMM-yyyy");
                        txtExpiryDate.Text = Convert.ToDateTime(row["expiryDate"]).ToString("dd-MMM-yyyy");
                        txtBatchNo.Text = row["batchNo"].ToString();
                        txtSerialNo.Text = row["serialNo"].ToString();
                        int shipment = Convert.ToInt32(row["shipmentStatus"]);
                        string shipmentid = shipment.ToString();
                        ddlShipmentStatus.SelectedValue = shipmentid;
                        //ddlManufacturer.SelectedValue = row["manufacturerId"].ToString();
                        txtNotes.Text = row["notes"].ToString();
                        chkIMEIEnable.Checked = row["imeiStatus"].ToString() == "1" ? true : false;

                        txtEngineNumber.Text = row["engineNumber"].ToString();
                        txtCecishNumber.Text = row["cecishNumber"].ToString();
                        txtCompanyPrice.Text = row["comPrice"].ToString();



                        string dbImageName = row["image"].ToString();
                        if (!string.IsNullOrWhiteSpace(dbImageName) || !string.IsNullOrWhiteSpace(dbImageName))
                        {
                            txtImgDb.Value = dbImageName;
                            Image1.Src = "~/Img/Product/" + dbImageName;

                            // split form db extension
                            string[] splitImg = dbImageName.Split('.');
                            txtImgFileName.Value = splitImg[0];
                        }

                        if (commonFunction.findSettingItemValueDataTable("manufacturer") != "0")
                        {
                            string manufacturer = row["manufacturerId"].ToString();
                            if (manufacturer == "0")
                                ddlManufacturer.SelectedIndex = 0;
                            else
                                ddlManufacturer.SelectedValue = manufacturer;
                        }

                        // Variant 
                        if (commonFunction.findSettingItemValueDataTable("displayVariant") == "1")
                        {
                            if (!Convert.ToBoolean(row["isChild"]))
                            {
                                ddlProductType.SelectedValue = "1";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "changeVariant()", true);
                            }
                            else
                            {
                                divVariantControl.Visible = false;
                            }


                            divVariantControlForUpdate.Visible = true;


                            var variant = new Variant();
                            var attrId = row["attributeRecord"].ToString();
                            lblVaiantUpdateLabel.Text = variant.getAttributeNameData(attrId);

                        }

                        txtScanCode.Enabled =
                            btnGenerateBarCode.Enabled = ddlSup.Enabled = false;
                        txtScanCode.BackColor =
                            btnGenerateBarCode.BackColor =
                                ddlSup.BackColor = ddlCat.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
                        // read only mode
                        txtBPrice.ReadOnly = txtSPrice.ReadOnly = true;
                        txtBPrice.BackColor = txtSPrice.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");

                        if (dicProductListUpdate.ContainsValue(txtScanCode.Text))
                        {
                            var prodCode = dicProductListUpdate.FirstOrDefault(x => x.Value == txtScanCode.Text).Key;
                            var myKey = prodCode.LastOrDefault();

                            txtQty.Text = dicProductListUpdate["qty" + myKey];
                            txtIMEI.Value = dicProductListUpdate["imei" + myKey];
                        }
                    }
                }

                // Load Attribute Value 
                AttributeLoadToTextbox();

                // Show Shoping list
                ShowShoppingList();
            }
            else
            {
                Reset();
            }
        }





        protected void btnGenerateBarCode_Click(object sender, ImageClickEventArgs e)
        {
            txtScanCode.Text = BarCodeGeneratorForBulkStock();

            // Show Product item list
            ShowShoppingList();

            //GetKeyValue();
        }





        public string BarCodeGeneratorForBulkStock()
        {
            string digitValue = "", itemValue = "", prodCode = "";
            var increaseNum = 0;

            if (barcodeCounter == 0)
            {
                prodCode = commonFunction.barcodeGenerator();
                barCodeTmp = prodCode;
                barcodeCounter++;
            }
            else
            {
                itemValue = commonFunction.findSettingItemValue(3);

                increaseNum = (Convert.ToInt32(barCodeTmp.Remove(0, 3)) + stockCounter);

                //if (accessbarCode == true)
                //{
                //    increaseNum = (Convert.ToInt32(barCodeTmp.Remove(0, 3)) + stockCounter);
                //    //txtScanCode.Text = increaseNum.ToString("00000000").Insert(0, "ZZ-");
                //}
                //else
                //{
                //    increaseNum = (Convert.ToInt32(barcode.Remove(0, 3)) + stockCounter);
                //    //txtScanCode.Text = increaseNum.ToString("00000000").Insert(0, "ZZ-");
                //}

                if (itemValue == "0")
                    digitValue = "00000000";
                else if (itemValue == "1")
                    digitValue = "0000000";
                else if (itemValue == "2")
                    digitValue = "000000";

                prodCode = increaseNum.ToString(digitValue).Insert(0, "ZZ-");
            }

            return prodCode;
        }





        protected void btnBPriceEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBPrice.ReadOnly)
            {
                txtBPrice.ReadOnly = false;
                txtBPrice.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }

            else
            {
                txtBPrice.ReadOnly = true;
                txtBPrice.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
            }

            // Load Product list
            ShowShoppingList();
        }

        protected void btnCompanyPrice_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBPrice.ReadOnly)
            {
                txtCompanyPrice.ReadOnly = false;
                txtCompanyPrice.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }

            else
            {
                txtCompanyPrice.ReadOnly = true;
                txtCompanyPrice.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
            }

            // Load Product list
            ShowShoppingList();
        }






        protected void ddlSetBuyPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dPrice = txtDealerPrice.Text;
            if (dPrice == "")
                dPrice = "0";
            decimal dealerPrice = Convert.ToDecimal(dPrice);
            decimal tempPercentValue = (dealerPrice * Convert.ToDecimal(ddlSetBuyPrice.SelectedValue)) / 100;
            decimal actualBuyPrice = dealerPrice - tempPercentValue;
            txtBPrice.Text = actualBuyPrice.ToString();

            // Show product item
            ShowShoppingList();
        }





        protected void btnSPriceEdt_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSPrice.ReadOnly)
            {
                txtSPrice.ReadOnly = false;
                txtSPrice.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }

            else
            {
                txtSPrice.ReadOnly = true;
                txtSPrice.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");
            }

            // Load Product list
            ShowShoppingList();
        }





        protected void btnJSLoad_Click(object sender, ImageClickEventArgs e)
        {
            //load tag javascript
            loadTagJs();

            // Show product item list
            ShowShoppingList();
        }





        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
            ShowShoppingList();
        }





        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var updateList = new List<string>();


                var isValidInput = checkInputValidation();
                if (!isValidInput)
                    return;


                var isValidRequired = checkReuiredValidation();
                if (!isValidRequired)
                    return;



                decimal Qty = Convert.ToDecimal(txtQty.Text);

                if (txtRecivedDate.Text == "")
                    txtRecivedDate.Text = commonFunction.GetCurrentTime().ToString();

                if (txtExpiryDate.Text == "")
                    txtExpiryDate.Text = commonFunction.GetCurrentTime().ToString();

                decimal bPrice = Convert.ToDecimal(txtBPrice.Text);

                string tempFieldAttribute = "";
                string supplierValue = "";

                string prodCode = txtScanCode.Text;


                // Add in Dictionary 
                if (isProductUpdate)
                {
                    if (dicProductListUpdate.ContainsValue(txtScanCode.Text))
                    {
                        var prodCodeUpdate = dicProductListUpdate.FirstOrDefault(x => x.Value == txtScanCode.Text).Key;
                        var myKey = prodCodeUpdate.LastOrDefault();

                        dicProductListUpdate["qty" + myKey] = Qty.ToString();
                        dicProductListUpdate["imei" + myKey] = txtIMEI.Value;
                        dicProductListUpdate["stockTotal" + myKey] = txtUnitTotalPurchaseAmt.Text;
                        dicProductListUpdate.Add("sPrice" + stockCounter, txtSPrice.Text);
                        dicProductListUpdate.Add("freeQty" + stockCounter, txtFreeQty.Text);
                        stockCounter--;
                    }
                    else
                    {
                        dicProductListUpdate.Add("tempId" + stockCounter, stockCounter.ToString());
                        dicProductListUpdate.Add("prodID" + stockCounter, lblProdID.Text);
                        dicProductListUpdate.Add("prodName" + stockCounter, txtProduct.Text);
                        dicProductListUpdate.Add("catName" + stockCounter, ddlCat.SelectedValue);
                        dicProductListUpdate.Add("prodCode" + stockCounter, txtScanCode.Text);
                        dicProductListUpdate.Add("qty" + stockCounter, txtQty.Text);
                        dicProductListUpdate.Add("bPrice" + stockCounter, txtBPrice.Text);
                        dicProductListUpdate.Add("sPrice" + stockCounter, txtSPrice.Text);
                        dicProductListUpdate.Add("size" + stockCounter, txtSize.Text);
                        dicProductListUpdate.Add("stockTotal" + stockCounter, txtUnitTotalPurchaseAmt.Text);
                        dicProductListUpdate.Add("fieldAttribute" + stockCounter, tempFieldAttribute);
                        dicProductListUpdate.Add("sku" + stockCounter, txtSku.Text);
                        dicProductListUpdate.Add("tax" + stockCounter, txtTax.Text);
                        dicProductListUpdate.Add("lastQty" + stockCounter, txtQty.Text);
                        dicProductListUpdate.Add("warranty" + stockCounter, WarrantyDate.ToString());
                        dicProductListUpdate.Add("imei" + stockCounter, chkIMEIEnable.Checked ? txtIMEI.Value : "");
                        dicProductListUpdate.Add("dealerPrice" + stockCounter, txtDealerPrice.Text);
                        dicProductListUpdate.Add("supCompany" + stockCounter, ddlSup.Text);
                        dicProductListUpdate.Add("receivedDate" + stockCounter, txtRecivedDate.Text);
                        dicProductListUpdate.Add("expiryDate" + stockCounter, txtExpiryDate.Text);
                        dicProductListUpdate.Add("batchNo" + stockCounter, txtBatchNo.Text);
                        dicProductListUpdate.Add("serialNo" + stockCounter, txtSerialNo.Text);
                        dicProductListUpdate.Add("shipmentStatus" + stockCounter, ddlShipmentStatus.SelectedValue);
                        dicProductListUpdate.Add("manufacturer" + stockCounter, ddlManufacturer.SelectedValue);
                        dicProductListUpdate.Add("warningQty" + stockCounter, txtWarningQty.Text);
                        dicProductListUpdate.Add("notes" + stockCounter, txtNotes.Text);
                        dicProductListUpdate.Add("unitId" + stockCounter, ddlUnit.SelectedValue);
                        dicProductListUpdate.Add("warehouse" + stockCounter, ddlWarehouse.SelectedValue);
                        dicProductListUpdate.Add("supCommission" + stockCounter, txtSuplierCommission.Text);
                        dicProductListUpdate.Add("createdFor" + stockCounter, Session["roleId"].ToString());
                        dicProductListUpdate.Add("engineNumber" + stockCounter, txtEngineNumber.Text);
                        dicProductListUpdate.Add("cecishNumber" + stockCounter, txtCecishNumber.Text);
                        dicProductListUpdate.Add("purchaseCode" + stockCounter, txtPurchaseCode.Text);
                        dicProductListUpdate.Add("comPrice" + stockCounter, txtCompanyPrice.Text);
                        dicProductListUpdate.Add("location" + stockCounter, ddlLocationList.SelectedValue);
                        dicProductListUpdate.Add("freeQty" + stockCounter, txtFreeQty.Text);

                        // Image ecommerce
                        dicEcommerceUpdate.Add("eprodCode" + stockCounter, txtScanCode.Text);
                        dicEcommerceUpdate.Add("prodTitle" + stockCounter, txtProduct.Text);
                        dicEcommerceUpdate.Add("oPrice" + stockCounter, txtSPrice.Text);
                        dicEcommerceUpdate.Add("image" + stockCounter, txtImgDb.Value);
                        dicEcommerceUpdate.Add("prodId" + stockCounter, lblProdID.Text); ;
                        //dicEcommerceUpdate.Add("active" + stockCounter, "0");

                        //dictionary add conditional data for update
                        dicProductConditionData.Add("prodCode" + stockCounter, prodCode.ToString());
                        dicProductConditionData.Add("createdFor" + stockCounter, Session["roleId"].ToString());

                        supplierValue = dicProductListUpdate["supCompany" + stockCounter].ToString();
                        // stockCounterUpdate++;
                        arrUpdateData[stockCounter] = prodCode;
                        updateCount++;
                    }

                    // Attribute set to dictionary 
                    addDictionaryAttribute();

                    updateList.Add("update-");
                }
                else
                {
                    // Add product info
                    dicProductListCreate.Add("tempId" + stockCounter, stockCounter.ToString());
                    dicProductListCreate.Add("prodId" + stockCounter, prodId.ToString());
                    dicProductListCreate.Add("prodCode" + stockCounter, prodCode);
                    dicProductListCreate.Add("prodName" + stockCounter, txtProduct.Text);
                    dicProductListCreate.Add("prodDescr" + stockCounter, "");
                    dicProductListCreate.Add("supCompany" + stockCounter, ddlSup.SelectedValue);
                    dicProductListCreate.Add("catName" + stockCounter, ddlCat.SelectedValue);
                    dicProductListCreate.Add("qty" + stockCounter, txtQty.Text);
                    dicProductListCreate.Add("bPrice" + stockCounter, txtBPrice.Text);
                    dicProductListCreate.Add("sPrice" + stockCounter, txtSPrice.Text);
                    dicProductListCreate.Add("weight" + stockCounter, "0.00");
                    dicProductListCreate.Add("size" + stockCounter, txtSize.Text);
                    dicProductListCreate.Add("discount" + stockCounter, "0.00");
                    dicProductListCreate.Add("stockTotal" + stockCounter, txtUnitTotalPurchaseAmt.Text);
                    dicProductListCreate.Add("entryDate" + stockCounter, commonFunction.GetCurrentTime().ToShortDateString());
                    dicProductListCreate.Add("entryQty" + stockCounter, txtQty.Text);
                    dicProductListCreate.Add("title" + stockCounter, "");
                    dicProductListCreate.Add("branchName" + stockCounter, "");
                    dicProductListCreate.Add("fieldAttribute" + stockCounter, tempFieldAttribute);
                    dicProductListCreate.Add("tax" + stockCounter, txtTax.Text);
                    dicProductListCreate.Add("sku" + stockCounter, txtSku.Text);
                    dicProductListCreate.Add("lastQty" + stockCounter, "0.0");
                    dicProductListCreate.Add("warranty" + stockCounter, WarrantyDate);
                    dicProductListCreate.Add("imei" + stockCounter, chkIMEIEnable.Checked ? txtIMEI.Value : "");
                    dicProductListCreate.Add("warningQty" + stockCounter, txtWarningQty.Text);
                    dicProductListCreate.Add("dealerPrice" + stockCounter, txtDealerPrice.Text);
                    dicProductListCreate.Add("purchaseCode" + stockCounter, txtPurchaseCode.Text);
                    dicProductListCreate.Add("createdFor" + stockCounter, Session["roleId"].ToString());
                    dicProductListCreate.Add("receivedDate" + stockCounter, txtRecivedDate.Text);
                    dicProductListCreate.Add("expiryDate" + stockCounter, txtExpiryDate.Text);
                    dicProductListCreate.Add("batchNo" + stockCounter, txtBatchNo.Text);
                    dicProductListCreate.Add("serialNo" + stockCounter, txtSerialNo.Text);
                    dicProductListCreate.Add("shipmentStatus" + stockCounter, ddlShipmentStatus.SelectedValue);
                    dicProductListCreate.Add("manufacturer" + stockCounter, ddlManufacturer.SelectedValue);
                    dicProductListCreate.Add("notes" + stockCounter, txtNotes.Text);
                    dicProductListCreate.Add("unitId" + stockCounter, ddlUnit.SelectedValue);
                    dicProductListCreate.Add("warehouse" + stockCounter, ddlWarehouse.SelectedValue);
                    dicProductListCreate.Add("supCommission" + stockCounter, txtSuplierCommission.Text);
                    dicProductListCreate.Add("engineNumber" + stockCounter, txtEngineNumber.Text);
                    dicProductListCreate.Add("cecishNumber" + stockCounter, txtCecishNumber.Text);
                    dicProductListCreate.Add("comPrice" + stockCounter, txtCompanyPrice.Text);
                    dicProductListCreate.Add("location" + stockCounter, ddlLocationList.SelectedValue);
                    dicProductListCreate.Add("freeQty" + stockCounter, txtFreeQty.Text);

                    // variant 
                    dicProductListCreate.Add("variant" + stockCounter, hiddenAttributeJosnValue.Value);

                    // Add ecommerce info
                    dicEcommerceCreate.Add("eprodCode" + stockCounter, txtScanCode.Text);
                    dicEcommerceCreate.Add("image" + stockCounter, txtImgDb.Value);
                    dicEcommerceCreate.Add("prodTitle" + stockCounter, txtProduct.Text);
                    dicEcommerceCreate.Add("oPrice" + stockCounter, txtBPrice.Text);
                    dicEcommerceCreate.Add("prodId" + stockCounter, prodId.ToString()); ;

                    // Set attribute to dictionary 
                    addDictionaryAttribute();

                    // Track individual supplier
                    supplierValue = dicProductListCreate["supCompany" + stockCounter];

                    // Track distinct product
                    arrInsertData[stockCounter] = prodCode;


                    updateList.Add("insert-");

                    insertCount++;
                    eCounter++;

                    // lblTest.Text = dicProductListCreate.ElementAt(0).ToString(); 
                    // http://www.c-sharpcorner.com/UploadFile/0f68f2/creating-a-lookup-from-a-list-of-objects-using-lambda-expres/
                }

                updateListStatus += string.Join("-", updateList.ToArray());

                //Suplier add to list
                if (!dicSupplierList.ContainsValue(supplierValue))
                {
                    dicSupplierList.Add("supCompany" + supplierCounter, supplierValue);
                    supplierCounter++;
                }

                // Variant
                if (commonFunction.findSettingItemValueDataTable("displayVariant") == "1")
                {
                    divVariantControl.Visible = true;
                    divVariantControlForUpdate.Visible = false;
                }


                isProductUpdate = false;
                prodId++;
                stockCounter++;
                MultiStockList();
                Reset();

                // For Payment option 
                ddlSup.Enabled = false;
                ddlSup.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F3F4");

                // auto generate barcode 
                string generateBarcodeType = commonFunction.findSettingItemValueDataTable("generateBardCodeType");
                if (generateBarcodeType == "1")
                {
                    divBarcodeGenerator.Attributes.Add("class", "disNone");
                    btnGenerateBarCode_Click(null, null);
                }

                // Single product save using bulk stock
                if (whichPage == "unit")
                    btnSaveMultiStock_Click(null, null);
            }
            catch (Exception ex)
            {
                lblTest.Text = ex.ToString();
                return;
            }
        }




        private bool checkReuiredValidation()
        {
            if (txtProduct.Text == "")
            {
                ScriptMessage("Product Name Required!", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            if (txtScanCode.Text == "")
            {
                ScriptMessage("Product Code Required!", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            if (txtQty.Text == "" && lblCurrentQty.Text == "0")
            {
                ScriptMessage(ddlUnit.Text + " Required!", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            if (whichPage != "unit" && Convert.ToDecimal(txtQty.Text) < 0)
            {
                ScriptMessage("Qty is invalid !", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            if (commonFunction.findSettingItemValueDataTable("isUnit") == "0")
            {
                string inputQtyUnit = txtQty.Text == "" ? "0" : txtQty.Text;
                if (inputQtyUnit.Contains("."))
                {
                    ScriptMessage("Unit measurement is not active", MessageType.Warning);
                    return false;
                }
            }




            // Insert barcode check
            if (btnAdd.Text == "Add")
            {
                var dsCheckBulkStock = objSql.getDataSet("SELECT * FROM StockInfo WHERE prodCode ='" + txtScanCode.Text + "' " +
                                      HttpContext.Current.Session["userAccessParameters"] + " AND warehouse = '" + ddlWarehouse.SelectedValue + "'");
                int prodCounter = dsCheckBulkStock.Tables[0].Rows.Count;

                if (dicProductListCreate.ContainsValue(txtScanCode.Text) || (prodCounter > 0))
                {
                    ScriptMessage("Product Code exists!", MessageType.Warning);
                    ShowShoppingList();
                    return false;
                }

                var dsSku =
                    objSql.getDataSet("SELECT * FROM StockInfo WHERE sku ='" + txtSku.Text + "' " + HttpContext.Current.Session["userAccessParameters"] + " AND warehouse = '" + ddlWarehouse.SelectedValue + "'");

                int skuCounter = dsSku.Tables[0].Rows.Count;
                //                if (skuCounter > 0)
                //                    prodSku = dsSku.Tables[0].Rows[0][0].ToString();

                if (commonFunction.findSettingItemValueDataTable("sku") == "1")
                {
                    if (dicProductListCreate.ContainsValue(txtSku.Text) || skuCounter > 0)
                    {
                        ScriptMessage("SKU exists! Please enter new SKU.", MessageType.Warning);
                        ShowShoppingList();
                        return false;
                    }
                }
            }


            // Check IMEI 
            isImeiAvailable = commonFunction.findSettingItemValue(15);
            if (isImeiAvailable == "1" && chkIMEIEnable.Checked)
            {
                string[] splitImei = txtIMEI.Value.Split(',');
                int totalImei = splitImei.Count();

                if (txtQty.Text == "")
                    txtQty.Text = "0";

                string existQty = lblCurrentQty.Text,
                        inputQty = txtQty.Text;
                if (existQty.Contains('.'))
                    existQty = lblCurrentQty.Text.Split('.')[0];

                if (inputQty.Contains('.'))
                    inputQty = txtQty.Text.Split('.')[0];

                int totalQtyForImei = Convert.ToInt32(inputQty) + Convert.ToInt32(existQty);
                if (totalQtyForImei != totalImei)
                {
                    ScriptMessage("IMEI and Qty is not matched!", MessageType.Warning);
                    ShowShoppingList();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                    return false;
                }
            }

            // Check stock qty
            if (txtQty.Text != "")
            {
                float tempValue = float.Parse(lblCurrentQty.Text) + float.Parse(txtQty.Text);

                if (tempValue < 0)
                {
                    ScriptMessage("Stock qty can not be negative value!", MessageType.Warning);
                    return false;
                }

            }

            string prodCode = txtScanCode.Text;

            string queryProdCode = "SELECT * FROM StockInfo WHERE prodCode ='" + txtScanCode.Text + "' " + HttpContext.Current.Session["userAccessParameters"] + " AND warehouse = '" + ddlWarehouse.SelectedValue + "'";
            var dsProuctCode = objSql.getDataSet(queryProdCode);

            if (dsProuctCode.Tables[0].Rows.Count > 0 && accessProduct == false)
            {
                if (checkingExist == false)
                {
                    ScriptMessage("This product code exists! Generate new product code.", MessageType.Warning);
                    return false;
                }

                if (txtQty.Text == "")
                    txtQty.Text = "0";
            }
            else
            {
                // checking same product code
                if (txtScanCode.Text.Substring(0, 2) == "ZZ" && txtScanCode.Text.Substring(2, 1) != "-")
                {
                    ScriptMessage("We are very sorry! This barcode format we could not accept.", MessageType.Warning);
                    return false;
                }

                // Start check item scanning and set value to variable
                string prodName = txtProduct.Text;
                //prodCode = objCommonFun.barcodeGenerator();

                if ((prodCode.Length < 4) && (commonFunction.findSettingItemValueDataTable("noBarcode") == "0"))
                {
                    ScriptMessage("Product code length must be minimum 4", MessageType.Warning);
                    return false;
                }


                if (commonFunction.findSettingItemValueDataTable("noBarcode") == "0")
                {
                    File.WriteAllText(Server.MapPath("BarcodeTool/BarCode.txt"), prodCode);

                    //http://stackoverflow.com/questions/4291912/process-start-how-to-get-the-output
                    Process process = Process.Start(Server.MapPath("BarcodeTool/BarCodeGenerate.exe"));
                    if (process != null) process.WaitForExit();

                    // Check barcode image create or not
                    if (!File.Exists(Server.MapPath("BarcodeTool/images/" + prodCode + ".png")))
                    {
                        ScriptMessage("Build barcode image failed! Please try again.", MessageType.Warning);
                        return false;
                    }
                }
            }

            return true;
        }





        private bool checkInputValidation()
        {
            // Assagin Value
            if (string.IsNullOrEmpty(txtBPrice.Text))
                txtBPrice.Text = txtDealerPrice.Text;

            if (string.IsNullOrEmpty(txtBPrice.Text))
            {
                txtBPrice.Text = "0";
                lblBPrice.Text = "0";
            }

            if (string.IsNullOrEmpty(txtSPrice.Text))
                txtSPrice.Text = "0";

            if (string.IsNullOrEmpty(txtTax.Text))
                txtTax.Text = "0";

            if (string.IsNullOrEmpty(txtDealerPrice.Text))
                txtDealerPrice.Text = "0";

            if (string.IsNullOrEmpty(txtCompanyPrice.Text))
                txtCompanyPrice.Text = "0";

            if (string.IsNullOrEmpty(txtWarningQty.Text))
                txtWarningQty.Text = "0";

            if (string.IsNullOrEmpty(txtQty.Text))
                txtQty.Text = "0";

            if (txtSuplierCommission.Text == "")
                txtSuplierCommission.Text = "0";

            if (string.IsNullOrEmpty(txtFreeQty.Text))
                txtFreeQty.Text = "0";

            if (txtUnitTotalPurchaseAmt.Text == "")
                txtUnitTotalPurchaseAmt.Text = "0";


            // Check product exist or not         
            stockModel.prodName = txtProduct.Text.Trim();
            var dsProdName = stockModel.getproduct();

            bool dbProdExists = false;
            if (dsProdName.Tables[0].Rows.Count > 0)
            {
                if (dsProdName.Tables[0].Rows[0][3].ToString() == txtProduct.Text.Trim())
                {
                    dbProdExists = true;
                }
            }

            // Check dictionary product exists 
            bool dicProdExists = false;
            if (dicProductListCreate.ContainsValue(txtProduct.Text))
            {
                dicProdExists = true;
            }

            if ((commonFunction.findSettingItemValueDataTable("isExistProductName") == "0" && isProductInsert) && (dbProdExists || dicProdExists))
            {
                ScriptMessage("Product name already exists!", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            if (commonFunction.findSettingItemValueDataTable("isWarehouse") == "1" && ddlWarehouse.SelectedValue == "0")
            {
                ScriptMessage("Select Store list", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            if (ddlSup.SelectedValue == "0")
            {
                ScriptMessage("Select a supplier", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            if (ddlCat.SelectedValue == "0")
            {
                ScriptMessage("Select a category", MessageType.Warning);
                ShowShoppingList();
                return false;
            }


            // Check stock qty and attribute qty
            if (commonFunction.findSettingItemValueDataTable("customField") == "1" && !IsQtyEquelsDynamicAttribute())
            {
                ScriptMessage("Attribute qty does not match with total qty!", MessageType.Warning);
                ShowShoppingList();
                return false;
            }

            // Check ratio 
            string existingPiece = "0";
            if (lblCurrentQty.Text.Contains('.'))
                existingPiece = lblCurrentQty.Text.Split('.')[1];
            string inputPiece = "0";
            if (txtQty.Text.Contains('.'))
                inputPiece = txtQty.Text.Split('.')[1];


            int totalPiece = Convert.ToInt32(existingPiece) +
                             Convert.ToInt32(inputPiece);

            if (commonFunction.findSettingItemValueDataTable("isUnit") == "1" && totalPiece != 0 && ddlUnit.SelectedValue != "0")
            {
                if (isRatioOverflow(ddlUnit.SelectedValue, totalPiece))
                {
                    ScriptMessage("Pieces can not more than ratio!", MessageType.Warning);
                    ShowShoppingList();
                    return false;
                }
            }

            return true;

        }





        private bool isRatioOverflow(string unitValueId, int pieceValue)
        {
            bool exists = false;
            int ratioPiece = 0;

            if (unitValueId != "0")
            {
                DataTable dtRatio = objSql.getDataTable("SELECT * FROM UnitInfo WHERE Id = '" + unitValueId + "'");
                ratioPiece = Convert.ToInt32(dtRatio.Rows[0][2]);
            }

            if (ratioPiece <= pieceValue)
                exists = true;

            return exists;
        }





        // Product Max ID and Plus One
        private int GenerateProductID()
        {
            int prodId = commonFunction.nextIdPlusOne("SELECT MAX(prodID) FROM [StockInfo]");
            int prodId2 = commonFunction.nextIdPlusOne("SELECT MAX(prodID) FROM [StockStatusInfo]");

            if (prodId2 > prodId)
                prodId = prodId2;

            return prodId;
        }





        // Add Attribute to dictionary

        private void addDictionaryAttribute()
        {
            // Get Product Id
            string productId;
            if (isProductUpdate == true)
                productId = commonFunction.getProductId(txtScanCode.Text, false);
            else
                productId = GenerateProductID().ToString();

            for (i = 0; i < attributeCount; i++)
            {
                string[] splitAttributeDetail = new string[] { };

                splitAttributeDetail = txtAttribute[i].ID.Split(new string[] { "contentBody_" }, StringSplitOptions.None);

                // totalAttributeColumnCount += "AttributeID:" + splitAttributeDetail[1] + "????";

                // Update/Insert into check attribute in database 
                if (txtAttribute[i].Text != "")
                {
                    // Set property to dictionary
                    dicAttributeData.Add("fieldId" + attributeInputCounter, splitAttributeDetail[0]);
                    dicAttributeData.Add("attributeId" + attributeInputCounter, splitAttributeDetail[1]);
                    dicAttributeData.Add("stockQty" + attributeInputCounter, txtAttribute[i].Text);
                    dicAttributeData.Add("productId" + attributeInputCounter, productId);

                    attributeInputCounter++;

                    // Property Set
                    //objAttributeModel.fieldId = Convert.ToInt32(splitAttributeDetail[0]);
                    //objAttributeModel.attributeId = Convert.ToInt32(splitAttributeDetail[1]);
                    //objAttributeModel.stockQty = Convert.ToInt32(txtAttribute[i].Text);
                    //objAttributeModel.productId = Convert.ToInt32(productId);

                    // Check Attribute exists
                    //DataSet dsCheckAttr = objAttributeModel.ExistsAttribute();

                    //if (dsCheckAttr.Tables[0].Rows.Count > 0)
                    //    objAttributeModel.updateAttributeDetail();
                    //else
                    //    objAttributeModel.saveAttributeDetail();
                }
            }
        }





        private void MultiStockList()
        {
            //lblTest.Text += "// Counter - " + counter.ToString() + "//";

            string listOfUpdateStatus = updateListStatus.TrimEnd('-');
            string[] commdStatus = listOfUpdateStatus.Split('-');

            priceTotal = 0;

            for (int i = 0; i < stockCounter; i++)
            {
                if (commdStatus[i].ToString() == "update")
                {
                    lblSL[i] = new Label { Text = (i + 1).ToString(), ID = "lblSl" + i.ToString() };

                    lblProdName[i] = new Label
                    {
                        Text = dicProductListUpdate["prodName" + i],
                        ID = "prodName" + i.ToString()
                    };

                    lblbPrice[i] = new Label { Text = dicProductListUpdate["bPrice" + i], ID = "bPrice" + i.ToString() };
                    if (dicProductListUpdate["unitId" + i] != "0")
                        lblQty[i] = new Label
                        {
                            Text = dicProductListUpdate["qty" + i],
                            ID = "qty" + i
                        };
                    else
                        lblQty[i] = new Label { Text = dicProductListUpdate["qty" + i], ID = "qty" + i.ToString() };


                    lblTotalPriceList[i] = new Label
                    {
                        Text = dicProductListUpdate["stockTotal" + i],
                        ID = "stockTotal" + i.ToString()
                    };

                    //priceTotal = Convert.ToDecimal(lblTotalPriceList[i].Text);

                    //for stock update

                    lblsPrice[i] = new Label { Text = dicProductListUpdate["sPrice" + i], ID = "sPrice" + i.ToString() };


                    lblSupplier[i] = new Label
                    {
                        Text = dicProductListUpdate["supCompany" + i],
                        ID = "supCompany" + i.ToString()
                    };

                    btnCancel[i] = new Button { Text = "   X   ", ID = "btnCancel" + i.ToString() };
                    btnCancel[i].Click += new EventHandler(btnCancelClick);
                    btnCancel[i].CssClass = "btnCross";
                }
                else if (commdStatus[i].ToString() == "insert")
                {
                    lblSL[i] = new Label { Text = (i + 1).ToString(), ID = "lblSL" + i.ToString() };

                    lblProdName[i] = new Label
                    {
                        Text = dicProductListCreate["prodName" + i],
                        ID = "lblProdName" + i.ToString()
                    };

                    lblbPrice[i] = new Label
                    {
                        Text = dicProductListCreate["bPrice" + i],
                        ID = "lblbPrice" + i.ToString()
                    };

                    lblQty[i] = new Label { Text = dicProductListCreate["qty" + i] };

                    lblTotalPriceList[i] = new Label
                    {
                        Text = dicProductListCreate["stockTotal" + i],
                        ID = "lblTotalPriceList" + i.ToString()
                    };

                    //priceTotal = Convert.ToDecimal(lblTotalPriceList[i].Text);

                    //for stock update

                    lblsPrice[i] = new Label
                    {
                        Text = dicProductListCreate["sPrice" + i],
                        ID = "lblsPrice" + i.ToString()
                    };


                    lblSupplier[i] = new Label
                    {
                        Text = dicProductListCreate["supCompany" + i],
                        ID = "lblSupplier" + i.ToString()
                    };

                    btnCancel[i] = new Button { Text = "   X   ", ID = "btnCancel" + i };
                    //btnCancel[i].Click += new EventHandler(btnCancelClick);
                    btnCancel[i].CssClass = "btnCross";
                    btnCancel[i].Click += (s, e) =>
                    {
                        txtStockAmount.Text = "0";
                    };
                    

                }

                priceTotal += Convert.ToDecimal(lblTotalPriceList[i].Text);
            }

            ShowShoppingList();
        }





        protected void ShowShoppingList()
        {
            divStockProductList.Controls.Clear();
            for (int i = 0; i < stockCounter; i++)
            {
                // start 
                divStockProductList.Controls.Add(new LiteralControl("<div class = dynamicContentUnit>"));

                // 0
                //divStockProductList.Controls.Add(lblSL[i]);

                // 1
                divStockProductList.Controls.Add(lblProdName[i]);

                //2
                divStockProductList.Controls.Add(lblbPrice[i]);

                //3
                divStockProductList.Controls.Add(lblQty[i]);

                //4
                divStockProductList.Controls.Add(lblTotalPriceList[i]);
                //priceTotalList = Convert.ToDecimal(lblTotalPriceList[i].Text);

                //// 2
                //divStockProductList.Controls.Add(lblsPrice[i]);

                //
                //divStockProductList.Controls.Add(lblSupplier[i]);

                // 3
                //divStockProductList.Controls.Add(btnCancel[i]);

                //Button b = new Button();
                //b.ID = "Button" + i.ToString();
                //b.Text = "Button" + i.ToString();
                //b.Click += (s, e) =>
                //{
                //    txtStockAmount.Text = "0";
                //};
                //divStockProductList.Controls.Add(b);
                

                // end 
                divStockProductList.Controls.Add(new LiteralControl("</div>"));
            }

            //
            //priceTotal += priceTotalList; 
            //lblStockAmout.Text = priceTotal.ToString();
            txtStockAmount.Text = priceTotal.ToString();
        }





        protected void Button1_Click(object sender, EventArgs e)
        {
            string prodName = Request.Form[txtSearchNameCode.UniqueID];
            string prodId = Request.Form[hfProductDetails.UniqueID];
            ScriptMessage(prodName + "\\nID: " + prodId, MessageType.Info);
        }





        protected void btnSearchDate_Click(object sender, EventArgs e)
        {
        }





        protected void btnCancelClick(object sender, EventArgs e)
        {
            txtStockAmount.Text = "0";

            ShowShoppingList();
        }





        private void loadddlBuyPrice()
        {
            for (int i = 0; i <= 20; i++)
            {
                ddlSetBuyPrice.Items.Add(i.ToString());
            }
        }





        private void loadTagJs()
        {
            //load tag javascript
            var javaScriptCode = "<script type='text/javascript' src='../Js/bootstrap-tagsinput.min.js'></script>";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), javaScriptCode,
                false);
        }





        private void CreateFieldAttribute(string tempFieldAttribute)
        {
            divFieldList.Controls.Clear();

            dsLoadField = objSql.getDataSet("SELECT * FROM [FieldInfo] WHERE active='1'");
            fieldCount = dsLoadField.Tables[0].Rows.Count;

            for (int i = 0; i < dsLoadField.Tables[0].Rows.Count; i++)
            {
                // load html
                divFieldList.Controls.Add(new LiteralControl("<div class='form-group'>"));

                // load field
                string fieldId = dsLoadField.Tables[0].Rows[i][0].ToString();
                lblField[i] = new Label
                {
                    Text = dsLoadField.Tables[0].Rows[i][1].ToString(),
                    CssClass = "lbl col-sm-4 control-label"
                }; //ID = fieldId, 
                divFieldList.Controls.Add(lblField[i]);

                // load html
                divFieldList.Controls.Add(new LiteralControl("<div class='col-sm-8'>"));

                // load attributes 
                string[] splitText = tempFieldAttribute.Split(new string[] { ";" }, StringSplitOptions.None);
                DataSet dsLoadAttribute =
                    objSql.getDataSet("SELECT * FROM [AttributeInfo] WHERE fieldId='" + fieldId + "' AND active='1' ");
                ddlAttribute[i] = new DropDownList { ID = fieldId, CssClass = "form-control" };

                for (j = 0; j < dsLoadAttribute.Tables[0].Rows.Count; j++)
                {
                    if (j == 0)
                        ddlAttribute[i].Items.Insert(j, " ");

                    string attributeId = dsLoadAttribute.Tables[0].Rows[j][0].ToString();
                    string attributeName = dsLoadAttribute.Tables[0].Rows[j][2].ToString();

                    ddlAttribute[i].Items.Add(new ListItem(attributeName, attributeId));

                    for (int k = 0; k < splitText.Count() - 1; k++)
                    {
                        string[] fieldnattribute = splitText[k].Split('-');
                        if (fieldId == fieldnattribute[0] && attributeId == fieldnattribute[1])
                        {
                            ddlAttribute[i].ClearSelection();
                            ddlAttribute[i].Items.FindByValue(attributeId).Selected = true;
                            break;
                        }
                    }
                }

                divFieldList.Controls.Add(ddlAttribute[i]);
                dsLoadAttribute.Clear();

                // load html 
                divFieldList.Controls.Add(new LiteralControl("</div></div>"));
            }
        }





        private string SaveFieldAttribute()
        {
            string text = "";

            for (int i = 0; i < fieldCount; i++)
            {
                text += ddlAttribute[i].ID + "-" + ddlAttribute[i].SelectedValue + ";";
            }

            return text;
        }





        // Attribute Qty equels to total qty
        private bool IsQtyEquelsDynamicAttribute()
        {
            // Checking Qty equal or less then stock qty
            int totalQtyAttr = 0;
            string oldQty = "", fieldId = "";
            for (int j = 0; j <= attributeCount; j++)
            {
                try
                {
                    string[] splitAttributeDetail = new string[] { };

                    splitAttributeDetail = txtAttribute[j].ID.Split(new string[] { "contentBody_" },
                        StringSplitOptions.None);

                    if (j == 0)
                        oldQty = splitAttributeDetail[0];

                    int a = Convert.ToInt32(splitAttributeDetail[0]);

                    fieldId = splitAttributeDetail[0].ToString();
                }
                catch (Exception)
                {
                    fieldId += 1;
                }


                if (fieldId == oldQty)
                {
                    if (txtAttribute[j].Text != "")
                        totalQtyAttr += Convert.ToInt32(txtAttribute[j].Text);
                    oldQty = fieldId;
                }
                else
                {
                    if (txtQty.Text == "")
                        txtQty.Text = "0";
                    int dbInputQty = Convert.ToInt32(lblCurrentQty.Text) + Convert.ToInt32(txtQty.Text);
                    if (dbInputQty < totalQtyAttr || dbInputQty > totalQtyAttr)
                    {
                        //ScriptMessage("Product qty not more than input total qty: " + dbInputQty.ToString(), MessageType.Warning);
                        return false;
                    }

                    try
                    {
                        totalQtyAttr = 0;

                        if (txtAttribute[j].Text != "")
                            totalQtyAttr = Convert.ToInt32(txtAttribute[j].Text);
                        oldQty = fieldId;
                    }
                    catch (Exception)
                    {
                        totalQtyAttr = 0;
                        oldQty = fieldId = "";
                    }
                }
            }

            return true;
        }





        // CHeck this barcode exists when barcode saved
        private bool IsBarCodeAvailable()
        {
            DataTable dtBcode =
                objSql.getDataTable("Select prodCode FROM StockInfo WHERE prodCode = '" + txtScanCode.Text.Trim() + "'");
            if (dtBcode.Rows.Count > 0)
                return false;

            return true;
        }





        private void Reset()
        {
            txtProduct.Text =
                txtWarningQty.Text =
                    txtQty.Text =
                        txtSPrice.Text =
                            txtSearchNameCode.Text =
                                txtBPrice.Text =
                                    txtSku.Text =
                                        txtTax.Text =
                                            txtSize.Text = txtCompanyPrice.Text =
                                                txtNotes.Text = txtSuplierCommission.Text = "";
            lblCurrentQty.Text = lblBPrice.Text = "0";
            ddlSetBuyPrice.SelectedIndex =
                ddlManufacturer.SelectedIndex = ddlShipmentStatus.SelectedIndex = ddlUnit.SelectedIndex = 0;
            ddlWarrantyYear.SelectedValue = ddlWarrantyMonth.SelectedValue = ddlWarrantyDays.SelectedValue = "0";

            txtBPrice.ReadOnly = false;
            txtSPrice.ReadOnly = false;
            txtImgFileName.Value = "";
            txtBPrice.BackColor =
                txtSPrice.BackColor =
                    txtProduct.BackColor =
                        txtScanCode.BackColor =
                            btnGenerateBarCode.BackColor =
                                ddlSup.BackColor = ddlCat.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            txtProduct.Enabled =
                txtScanCode.Enabled = btnGenerateBarCode.Enabled = ddlSup.Enabled = ddlCat.Enabled = true;
            txtRecivedDate.Text = txtExpiryDate.Text = txtBatchNo.Text = txtSerialNo.Text = "";
            //CreateFieldAttribute("");

            // Attribute Reset
            for (int i = 0; i < attributeCount; i++)
            {
                txtAttribute[i].Text = "";
            }

            btnAdd.Text = "Add";
            checkingExist = false;
            txtIMEI.Value = "";
            txtDealerPrice.Text = "";
            txtScanCode.Text = "";

            accessProduct = isProductUpdate = false;
            isProductInsert = true;
            txtFreeQty.Text = "0";

            isSearchByProdId = false;

            Image1.Src = "";
        }





        public void MultiReset()
        {
            // Clear field array
            for (int i = 0; i < stockCounter; i++)
            {
                lblSL[i].Text = "0";
                lblProdName[i].Text = "";
                lblbPrice[i].Text = "0";
                lblsPrice[i].Text = "0";
                lblQty[i].Text = "0";
                lblTotalPriceList[i].Text = "0";

                btnCancel[i].Text = "";
            }

            lblmsg.Text = "";
            attributeInputCounter = 0;

            // Clear field array
            Array.Clear(lblSL, 0, lblSL.Length);
            Array.Clear(lblProdName, 0, lblProdName.Length);
            Array.Clear(lblsPrice, 0, lblsPrice.Length);
            Array.Clear(lblbPrice, 0, lblbPrice.Length);
            Array.Clear(lblTotalPriceList, 0, lblTotalPriceList.Length);
            Array.Clear(lblQty, 0, lblQty.Length);
            Array.Clear(btnCancel, 0, btnCancel.Length);

            // Clear dictionary
            dicProductListCreate.Clear();
            dicProductListUpdate.Clear();
            dicProductConditionData.Clear();
            dicSupplierList.Clear();
            dicAttributeData.Clear();

            // Ecommerce dic clear
            dicEcommerceCreate.Clear();
            dicEcommerceUpdate.Clear();

            // Clear control variable
            divStockProductList.Controls.Clear();

            // Clear parameters
            updateCount = 0;
            insertCount = 0;
            // counter = 0;
            priceTotal = 0;
            eCounter = 0;
            supplierCounter = 0;
            updateListStatus = "";
            txtStockAmount.Text = "";
            txtComment.Text = "";
            txtComment.Text = "";


            // Clear array
            Array.Clear(arrSupplierId, 0, arrSupplierId.Length);
            Array.Clear(arrInsertData, 0, arrInsertData.Length);
            Array.Clear(arrUpdateData, 0, arrUpdateData.Length);

            WarrantyDate = isWariningQty;
            fieldCount = j = k = 0;
            entryTotal = priceTotalList = 0;
            stockCounter =
                stockCounterUpdate = prodId = barCodeCounter = supplierId = productUpdate = productInsert = 0;
            barcode = barCodeTmp = "";
            accessProduct = accessbarCode = isProductUpdate = false;
            isProductInsert = true;

            // Gererate Purchase Code
            txtPurchaseCode.Text = commonFunction.nextPurchaseCode();

            txtStockAmount.Text = "0";
            txtPurchasePayment.Text = "";
            txtSupplierReceived.Text = "";
            txtPurchaseDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyy");
            txtComment.Text = "";
            txtSchedulePayment.Text = "";
            txtSchedulePaymentDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyy");
            txtScheduleComment.Text = "";
            isImportProduct = false;
        }





        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        protected void btnSaveMultiStock_Click(object sender, EventArgs e)
        {
            var confirmSaved = "";

            inputValidationPurchaseSave();


            decimal purchasePayment = Convert.ToDecimal(txtPurchasePayment.Text);

            var purchaseCode = txtPurchaseCode.Text.Trim();
            if (string.IsNullOrEmpty(purchaseCode) || string.IsNullOrWhiteSpace(purchaseCode))
            {
                purchaseCode = commonFunction.nextPurchaseCode();
            }

            // check purchase code existing
            var isExist = inventoryStock.isPurchaseCodeExist(purchaseCode);
            if (isExist)
            {
                ScriptMessage("Purchase code has already exist", MessageType.Warning);
                ShowShoppingList();
                return;
            }

            try
            {
                if (dicProductListCreate.Count <= 0 && dicProductListUpdate.Count <= 0)
                {
                    ScriptMessage("Product list is empty!", MessageType.Warning);
                    return;
                }

                string listOfUpdateStatus = updateListStatus.TrimEnd('-');
                string[] commdStatus = listOfUpdateStatus.Split('-');

                for (k = 0; k < stockCounter; k++)
                {
                    if (commdStatus[k] == "update")
                    {
                        // Update Supplier Commission
                        updateSupplierCommission(purchaseCode, k);

                        confirmSaved = updatetStockInfoFromDic(purchaseCode);


                    }
                    else if (commdStatus[k] == "insert")
                    {
                        confirmSaved = saveStockInfoFomDic(purchaseCode, k);
                        //eCommerceInfoFromStock();

                    }
                }

                var isStrockTryCatch = confirmSaved.Split('|')[0];
                if (isStrockTryCatch == "0")
                {
                    lblTest.Text = confirmSaved;
                    return;
                }

                // Save attribute Detail Info
                saveAttributeDetailInfo();


                decimal supplierTotalAmt = 0;

                for (int j = 0; j < dicSupplierList.Count; j++)
                {
                    for (int i = 0; i < stockCounter; i++)
                    {
                        decimal supplierAmt, productAmt, productQty;

                        if (insertCount > 0 && commdStatus[i] == "insert")
                        {
                            string supplier = dicSupplierList["supCompany" + j];
                            string supplierCreateList = dicProductListCreate["supCompany" + i];

                            if (supplier == supplierCreateList)
                            {
                                productAmt = Convert.ToDecimal(dicProductListCreate["bPrice" + i]);
                                productQty = Convert.ToDecimal(dicProductListCreate["qty" + i]);
                                supplierAmt = (productAmt * productQty);
                                supplierTotalAmt += supplierAmt;
                            }
                        }
                        else if (updateCount > 0 && commdStatus[i] == "update")
                        {
                            if ((dicSupplierList["supCompany" + j]) == (dicProductListUpdate["supCompany" + i]))
                            {
                                productAmt = Convert.ToDecimal(dicProductListUpdate["bPrice" + i]);
                                productQty = Convert.ToDecimal(dicProductListUpdate["qty" + i]);
                                supplierAmt = (productAmt * productQty);
                                supplierTotalAmt += supplierAmt;
                            }
                        }
                    }


                    var purchaseDate = txtPurchaseDate.Text.Trim();
                    if (string.IsNullOrEmpty(purchaseDate) || string.IsNullOrWhiteSpace(purchaseDate))
                    {
                        purchaseDate = commonFunction.GetCurrentTime().ToShortDateString();
                    }


                    string supplierTotal = txtStockAmount.Text;

                    if (whichPage == "unit")
                    {
                        supplierTotal = txtUnitTotalPurchaseAmt.Text;
                    }
                    if (string.IsNullOrEmpty(supplierTotal) || string.IsNullOrWhiteSpace(supplierTotal))
                    {
                        supplierTotal = "0";
                    }
                    supplierTotalAmt = Convert.ToDecimal(supplierTotal);


                    // Supplier Payment
                    if (supplierTotalAmt > 0)
                    {
                        commonFunction.cashTransactionFormPurchase(supplierTotalAmt, 0, "Supplier Purchase",
                            dicSupplierList["supCompany" + j], "", txtComment.Text, "0", "0", purchaseCode, purchaseDate,
                            false, false, 0);
                    }
                    else
                    {
                        commonFunction.cashTransactionFormPurchase(supplierTotalAmt, 0, "Scrap Amount",
                            dicSupplierList["supCompany" + j], "", txtComment.Text, "0", "0", purchaseCode, purchaseDate,
                            false, false, 0);
                    }

                    // Supplier Payment
                    if (purchasePayment > 0)
                    {
                        string purchaseComment = txtComment.Text;
                        commonFunction.cashTransactionFormPurchase(0, purchasePayment, "Supplier Payment",
                            dicSupplierList["supCompany" + j], "", purchaseComment, "0", "0", purchaseCode,
                            purchaseDate,
                            false, true, 0);
                    }
                    // schedule payment

                    string scheduleComment = txtScheduleComment.Text;
                    string scheduleDate = txtSchedulePaymentDate.Text;
                    string getSchedulePayment = txtSchedulePayment.Text;

                    if (string.IsNullOrWhiteSpace(getSchedulePayment) || string.IsNullOrEmpty(getSchedulePayment))
                    {
                        getSchedulePayment = "0";
                    }

                    if (string.IsNullOrWhiteSpace(scheduleDate) || string.IsNullOrEmpty(scheduleDate))
                    {
                        scheduleDate = commonFunction.GetCurrentTime().ToString();
                    }

                    decimal schedulePayment = Convert.ToDecimal(getSchedulePayment);
                    if (schedulePayment > 0)
                    {
                        commonFunction.cashTransactionFormPurchase(0, schedulePayment, "Supplier Schedule Payment",
                            dicSupplierList["supCompany" + j], "", scheduleComment, "0", "0", purchaseCode,
                            scheduleDate, true, true, 0);
                    }


                    // supplier received
                    decimal supReceivedAmt = 0;
                    string supReceived = txtSupplierReceived.Text;
                    if (string.IsNullOrEmpty(supReceived) || string.IsNullOrWhiteSpace(supReceived))
                    {
                        supReceivedAmt = 0;
                    }
                    else
                    {
                        supReceivedAmt = Convert.ToDecimal(supReceived);
                    }

                    if (supReceivedAmt > 0)
                    {
                        commonFunction.cashTransactionFormPurchase(0, 0, "Supplier Track Received Amount",
                            dicSupplierList["supCompany" + j], "", txtSupplierReceivedComment.Text, "3", "0", purchaseCode,
                            purchaseDate, false, false, supReceivedAmt);
                    }

                    supplierTotalAmt = 0;
                }


                ScriptMessage("Operation Successful.", MessageType.Success);
                Reset();
                MultiReset();

                //Reset array
                Array.Clear(commdStatus, 0, commdStatus.Length);

                // Go to stock page
                if (isStrockTryCatch == "1")
                {
                    Response.Redirect("~/Admin/Stock?msg=success");
                }
                else
                {
                    Response.Redirect("~/Admin/Stock?msg=warning");
                }
            }
            catch (Exception ex)
            {
                lblTest.Text = ex.ToString();
                ScriptMessage("Went to wrong.. Contact your support.", MessageType.Error);
                Reset();
                MultiReset();
            }
        }





        private void updateSupplierCommission(string purchaseCode, int exten)
        {
            var productId = dicProductListUpdate["prodID" + exten];
            var inputSupCommission = dicProductListUpdate["supCommission" + exten];
            var buyPrice = dicProductListUpdate["bPrice" + exten];

            var stock = new Service.Stock();
            var dbSupCommission = stock.getSupplierCommissionFormStock(productId);


            // stockstatusinfo
            initializeStockUpdateData(purchaseCode, exten);

            if (dbSupCommission != inputSupCommission)
            {

                var actualDiff = Convert.ToDecimal(dbSupCommission) - Convert.ToDecimal(inputSupCommission);

                var currentQty = stock.getStockStatusDataListByProductId(productId);

                var mathAcutalDiff = Math.Abs(actualDiff);
                var amountOfCommission = ((mathAcutalDiff * Convert.ToDecimal(buyPrice)) / 100) * Convert.ToDecimal(currentQty);


                decimal commission = 0, cashin = 0, cashout = 0;
                if (actualDiff > 0) // cashout
                {
                    commission = amountOfCommission;
                    cashin = amountOfCommission;
                }
                else // cashin
                {
                    commission = -amountOfCommission;
                    cashout = amountOfCommission;
                }

                // Update buy price 
                var dbQty = commonFunction.getLastStockQty(productId, ddlWarehouse.SelectedValue);
                var commissionDiff = commission / Convert.ToDecimal(dbQty);

                decimal buyPriceAfterCommission = commissionDiff + Convert.ToDecimal(buyPrice);
                var stockForSupCommission = new SupplierCommision();
                stockForSupCommission.prodId = Convert.ToInt32(productId);
                stockForSupCommission.bPrice = buyPriceAfterCommission;
                stockForSupCommission.UpdateStockBuyPriceForSupplierCommission();

                dicProductListUpdate["bPrice" + exten] = buyPriceAfterCommission.ToString();


                dicProductListUpdate["bPrice" + exten] = buyPriceAfterCommission.ToString();


                // cashReportInfo
                //commonFunction.cashTransaction(cashin, cashout, "Supplier Commission", productId, "", "", "0", "0");


                saveSupplierCommission(Convert.ToDecimal(commission), Convert.ToDecimal(mathAcutalDiff), 0, true);

            }

            var lastQty = dicProductListUpdate["qty" + exten] == "" ? "0" : dicProductListUpdate["qty" + exten];
            if (Convert.ToDecimal(lastQty) > 0)
                saveSupplierCommission(Convert.ToDecimal(buyPrice), Convert.ToDecimal(inputSupCommission), Convert.ToDecimal(lastQty), false);


        }

        private decimal getSupplierCommission(decimal buyPrice, decimal inputSupCommission, decimal qty)
        {
            var commissionAmt = (buyPrice * inputSupCommission) / 100;
            return (commissionAmt * qty);
        }





        private void saveSupplierCommission(decimal buyPrice, decimal supplierCommission, decimal qty, bool direct)
        {
            // stockstatusinfo
            decimal getCommission = 0;
            if (direct)
                getCommission = buyPrice;
            else
                getCommission = getSupplierCommission(buyPrice, supplierCommission, qty);

            stockModel.SupCommission = Convert.ToDecimal(supplierCommission);
            stockModel.commissionAmt = getCommission;
            stockModel.qty = qty.ToString();
            stockModel.Status = "supplierCommission";
            stockModel.searchType = "commission";

            stockModel.createStockStatus();
        }


        private void inputValidationPurchaseSave()
        {
            if (txtPurchasePayment.Text == "")
                txtPurchasePayment.Text = "0";
            if (txtSupplierReceived.Text == "")
                txtSupplierReceived.Text = "0";
            if (txtSchedulePayment.Text == "")
                txtSchedulePayment.Text = "0";
        }





        // Image upload form stock poage
        public void eCommerceInfoFromStock()
        {
            for (int k = 0; k < eCounter; k++)
            {
                stockModel.EprodCode = dicEcommerceCreate["eprodCode" + k].ToString();
                stockModel.prodTitle = dicEcommerceCreate["prodTitle" + k].ToString();
                stockModel.oPrice = Convert.ToDecimal(dicEcommerceCreate["oPrice" + k]);
                stockModel.image = dicEcommerceCreate["image" + k].ToString();
                //objStock.active = Convert.ToBoolean(dicEcommerceCreate["active" + k]);

                stockModel.InsertEcomProducts();
            }
        }





        // Insert MultiStock function in StockInfo 
        private string saveStockInfoFomDic(string purchaseCode,int sCounter)
        {
            int rowTest = 0;
            var productName = "";
            try
            {
                string insert = arrInsertData[sCounter];
                rowTest++;

                if (dicProductListCreate.ContainsValue(insert))
                {
                    int inputQty = 0, inputPiece = 0;
                    if (dicProductListCreate["qty" + sCounter].Contains('.'))
                    {
                        inputQty = int.Parse(dicProductListCreate["qty" + sCounter].Split('.')[0]);
                        inputPiece = int.Parse(dicProductListCreate["qty" + sCounter].Split('.')[1]);
                    }
                    else
                    {
                        inputQty = int.Parse(dicProductListCreate["qty" + sCounter]);
                        inputPiece = 0;
                    }

                    string qty = "";
                    if (dicProductListCreate["unitId" + sCounter] == "0")
                        qty = inputQty.ToString();
                    else
                        qty = RatioPlaceValue(inputPiece, inputQty, 0, 0);

                    if (!qty.Contains('.'))
                    {
                        qty = qty + ".0";
                    }

                    // Dictionary ready for Create
                    string generateBarcodeType = commonFunction.findSettingItemValueDataTable("generateBardCodeType");
                    if (generateBarcodeType == "1")
                    {
                        stockModel.prodCode = commonFunction.barcodeGenerator();
                    }
                    else
                    {
                        stockModel.prodCode = dicProductListCreate["prodCode" + sCounter];
                    }

                    var buyPrice = Convert.ToDecimal(dicProductListCreate["bPrice" + sCounter]);
                    decimal supCommission = Convert.ToDecimal(dicProductListCreate["supCommission" + sCounter]);

                    rowTest++;

                    productName = dicProductListCreate["prodName" + sCounter];

                    var newProductId = commonFunction.GenerateNewProductId();
                    stockModel.prodId = newProductId;
                    stockModel.prodName = dicProductListCreate["prodName" + sCounter];
                    stockModel.prodDescr = dicProductListCreate["prodDescr" + sCounter];
                    stockModel.supCompany = dicProductListCreate["supCompany" + sCounter];
                    stockModel.catName = dicProductListCreate["catName" + sCounter];
                    stockModel.qty = qty;
                    stockModel.bPrice = buyPrice;
                    stockModel.sPrice = Convert.ToDecimal(dicProductListCreate["sPrice" + sCounter]);
                    stockModel.weight = Convert.ToDecimal(dicProductListCreate["weight" + sCounter]);
                    stockModel.size = dicProductListCreate["size" + sCounter];
                    stockModel.discount = Convert.ToDecimal(dicProductListCreate["discount" + sCounter]);
                    stockModel.stockTotal = Convert.ToDecimal(dicProductListCreate["stockTotal" + sCounter]);
                    stockModel.entryQty = dicProductListCreate["entryQty" + sCounter];
                    stockModel.title = dicProductListCreate["title" + sCounter];
                    stockModel.branchName = dicProductListCreate["branchName" + sCounter];
                    stockModel.fieldAttribute = dicProductListCreate["fieldAttribute" + sCounter];
                    stockModel.tax = dicProductListCreate["tax" + sCounter];
                    stockModel.sku = dicProductListCreate["sku" + sCounter];
                    stockModel.lastQty = dicProductListCreate["lastQty" + sCounter];
                    stockModel.warningQty = dicProductListCreate["warningQty" + sCounter];
                    stockModel.imei = dicProductListCreate["imei" + sCounter];
                    stockModel.warranty = dicProductListCreate["warranty" + sCounter];
                    stockModel.dealerPrice = Convert.ToDecimal(dicProductListCreate["dealerPrice" + sCounter]);
                    stockModel.purchaseCode = purchaseCode;
                    stockModel.createdFor = dicProductListCreate["createdFor" + sCounter];
                    stockModel.receivedDate = Convert.ToDateTime(dicProductListCreate["receivedDate" + sCounter]);
                    stockModel.expiryDate = Convert.ToDateTime(dicProductListCreate["expiryDate" + sCounter]);
                    stockModel.batchNo = dicProductListCreate["batchNo" + sCounter];
                    stockModel.serialNo = dicProductListCreate["serialNo" + sCounter];
                    stockModel.ShipmentStatus = Convert.ToInt32(dicProductListCreate["shipmentStatus" + sCounter]);
                    stockModel.manufacturerId = Convert.ToInt32(dicProductListCreate["manufacturer" + sCounter]);
                    stockModel.notes = dicProductListCreate["notes" + sCounter];
                    stockModel.unitId = Convert.ToInt32(dicProductListCreate["unitId" + sCounter]);
                    stockModel.warehouseId = Convert.ToInt32(dicProductListCreate["warehouse" + sCounter]);
                    stockModel.SupCommission = supCommission;
                    stockModel.purchaseDate = Convert.ToDateTime(txtPurchaseDate.Text == "" ? commonFunction.GetCurrentTime().ToString() : txtPurchaseDate.Text);
                    stockModel.imeiStatus = chkIMEIEnable.Checked == true ? '1' : '0';
                    stockModel.engineNumber = dicProductListCreate["engineNumber" + sCounter];
                    stockModel.cecishNumber = dicProductListCreate["cecishNumber" + sCounter];
                    stockModel.comPrice = Convert.ToDecimal(dicProductListCreate["comPrice" + sCounter]);
                    stockModel.locationId = Convert.ToInt32(dicProductListCreate["location" + sCounter]);
                    stockModel.freeQty = dicProductListCreate["freeQty" + sCounter];
                    stockModel.billNo = "0";
                    stockModel.fieldRecord = "0";
                    stockModel.attributeRecord = "0";
                    stockModel.parentId = "0";
                    stockModel.balanceQty = qty;
                    stockModel.roleId = Session["roleId"].ToString();
                    stockModel.branchId = Session["branchId"].ToString();
                    stockModel.entryDate = commonFunction.GetCurrentTime();
                    stockModel.searchType = "product";

                    rowTest++;

                    // status
                    if (insertCount > 1 || updateCount > 1 || (insertCount == 1 && updateCount == 1))
                        stockModel.Status = "stock";
                    else
                        stockModel.Status = "stock";


                    if (dicProductListCreate["variant" + sCounter] != "")
                        stockModel.isParent = "1";

                    // Create stock
                    var q = stockModel.createStock();


                    var getCommission = getSupplierCommission(buyPrice, supCommission, Convert.ToDecimal(qty));

                    stockModel.SupCommission = supCommission;
                    stockModel.commissionAmt = getCommission;

                    // Create StockStatusInfo
                    stockModel.createStockStatus();

                    rowTest++;
                    // save supplier commission
                    //saveSupplierCommission(buyPrice, Convert.ToDecimal(supCommission), Convert.ToDecimal(qty), false);

                    // Variant data
                    var filedRecord = "";
                    if (dicProductListCreate["variant" + sCounter] != "")
                    {

                        Func<IEnumerable<IEnumerable<string>>, IEnumerable<string>> f = null;
                        f = xss =>
                        {
                            if (!xss.Any())
                            {
                                return new[] { "" };
                            }
                            else
                            {
                                var query =
                                from x in xss.First()
                                from y in f(xss.Skip(1))
                                select x + ", " + y;
                                return query;
                            }
                        };

                        var variantData = dicProductListCreate["variant" + sCounter];
                        var variantObj = new JavaScriptSerializer().Deserialize<List<VariantData>>(variantData);

                        var input = new List<string[]>();

                        int jvantConter = 0;
                        for (jvantConter = 0; jvantConter < variantObj.Count; jvantConter++)
                        {
                            string text = variantObj[jvantConter].attr;
                            string[] ids = text.Split(',');
                            input.Add(ids);
                            filedRecord += variantObj[jvantConter].field + ",";
                        }

                        var results = f(input);


                        foreach (var row in results.ToList())
                        {
                            stockModel.prodId = commonFunction.GenerateNewProductId();
                            stockModel.prodCode = commonFunction.barcodeGenerator();
                            stockModel.fieldRecord = filedRecord;
                            stockModel.attributeRecord = row;
                            stockModel.isChild = true;
                            stockModel.qty = qty;
                            stockModel.parentId = newProductId.ToString();
                            stockModel.createStock();

                            stockModel.createStockStatus();
                        }
                    }

                    rowTest++;

                    // Save Ecommerce
                    stockModel.EprodCode = dicEcommerceCreate["eprodCode" + sCounter];
                    stockModel.prodTitle = dicEcommerceCreate["prodTitle" + sCounter];
                    stockModel.oPrice = Convert.ToDecimal(dicEcommerceCreate["oPrice" + sCounter]);
                    stockModel.image = dicEcommerceCreate["image" + sCounter];
                    stockModel.ecomProdId = Convert.ToInt32(dicEcommerceCreate["prodId" + sCounter]);
                    //objStock.active = Convert.ToBoolean(dicEcommerceCreate["active" + sCounter]);
                    stockModel.InsertEcomProducts();


                    // Sync stock qty to Ecommerce
                    //if (isApiEcomm == "1")
                    //{
                    //    int totalQty = Convert.ToInt32(dicProductListUpdate["qty" + sCounter]);
                    //    objApiController.synchStock(dicProductListUpdate["sku" + sCounter], totalQty);
                    //}
                    
                }
                return "1|"+"";
            }
            catch (Exception ex)
            {
                MultiReset();
                return "0|" + productName +"//"+ rowTest + "//" + ex;
            }
        }

        /* prodName
            prodDescr
            supCompany
            catName
            qty
            bPrice
            unitId
            prodCode
            sPrice
            weight
            size
            discount
            stockTotal
            entryQty
            title
            branchName
            fieldAttribute
            tax
            sku
            lastQty
            warningQty
            imei
            warranty
            dealerPrice
            createdFor
            receivedDate
            expiryDate
            batchNo
            serialNo
            shipmentStatus
            manufacturer
            notes
            unitId
            warehouse
            engineNumber
            cecishNumber
            comPrice
            location
            freeQty
            variant
            eprodCode
            prodTitle
            oPrice
            image
            prodId
            qty
            sku*/




        private string removeImei = "";
        // Update MultiStock function in StockInfo 
        private string updatetStockInfoFromDic(string purchaseCode)
        {
            try
            {


                var update = arrUpdateData[k];
                if (dicProductListUpdate.ContainsValue(update))
                {

                    initializeStockUpdateData(purchaseCode, k);


                    string dbQtyWithPiece = getQtyWithPice(k);

                    // Update Stock Info
                    stockModel.updateStock();

                    // Update Stock Status Info
                    var prodId = dicProductListUpdate["prodID" + k];
                    var lastQty = commonFunction.getLastStockQty(prodId, Session["storeId"].ToString());
                    if (!lastQty.Contains('.'))
                    {
                        lastQty = lastQty + ".0";
                    }

                    string qtyStockUpdate = dicProductListUpdate["qty" + k];
                    if (!qtyStockUpdate.Contains("."))
                    {
                        qtyStockUpdate = qtyStockUpdate + ".0";
                    }

                    var balanceQty = commonFunction.calculateQty(prodId, lastQty, qtyStockUpdate, "+");
                    if (!balanceQty.Contains("."))
                    {
                        balanceQty = balanceQty + ".0";
                    }

                    stockModel.Status = "stock";
                    stockModel.qty = qtyStockUpdate;
                    stockModel.lastQty = lastQty;
                    stockModel.balanceQty = balanceQty;
                    stockModel.searchType = "product";

                    stockModel.createStockStatus();


                    // remove Imei 
                    if (removeImei != "")
                    {
                        stockModel.imei = removeImei;
                        stockModel.Status = "stockRemove";
                        stockModel.createStockStatus();
                    }

                    // Save Ecommerce data
                    stockModel.EprodCode = dicEcommerceUpdate["eprodCode" + k];
                    stockModel.prodTitle = dicEcommerceUpdate["prodTitle" + k];
                    stockModel.oPrice = Convert.ToDecimal(dicEcommerceUpdate["oPrice" + k]);
                    stockModel.image = dicEcommerceUpdate["image" + k];
                    //objStock.active = Convert.ToBoolean(dicEcommerceCreate["active" + k]);

                    stockModel.UpdateEcomProducts();

                    // Ecommerce synchronous 
                    //if (isApiEcomm == "1")
                    //{
                    //    int totalQty = int.Parse(dbQtyWithPiece) + int.Parse(dicProductListUpdate["qty" + k]);
                    //    objApiController.synchStock(dicProductListUpdate["sku" + k], totalQty);
                    //}

                }

                // Update for Supply 
                isProductUpdate = true;

                return "1|"+" ";
            }
            catch (Exception ex)
            {
                return "0|" + ex;
            }
        }





        private void initializeStockUpdateData(string purchaseCode, int counter)
        {

            string dbQtyWithPiece = getQtyWithPice(counter);

            string qty = "0";

            if (dicProductListUpdate["unitId" + counter] != "0")
            {
                int currentQty = 0, dbQty = 0, dbPiece = 0;
                string[] splitDbQtyWithPiece = dbQtyWithPiece.Split('.');
                if (splitDbQtyWithPiece.Length > 1)
                {
                    foreach (var item in splitDbQtyWithPiece)
                    {
                        dbQty = Convert.ToInt32(splitDbQtyWithPiece[0]);
                        dbPiece = Convert.ToInt32(splitDbQtyWithPiece[1]);
                    }
                }
                int inputQty = 0;
                int inputPiece = 0;
                string inputQtyString = dicProductListUpdate["qty" + counter];
                if (inputQtyString.Contains('.'))
                {
                    inputQty = Convert.ToInt32(inputQtyString.Split('.')[0]);
                    inputPiece = Convert.ToInt32(inputQtyString.Split('.')[1]);
                }
                else
                {
                    inputQty = Convert.ToInt32(dicProductListUpdate["qty" + counter]);
                    inputPiece = 0;
                }


                qty = RatioPlaceValue(inputPiece, inputQty, dbPiece, dbQty);
            }
            else
            {
                int inputQty = 0;
                int inputPiece = 0;
                string inputQtyString = dicProductListUpdate["qty" + counter];
                if (inputQtyString.Contains('.'))
                {
                    inputQty = Convert.ToInt32(inputQtyString.Split('.')[0]);
                    inputPiece = Convert.ToInt32(inputQtyString.Split('.')[1]);
                }
                qty = (inputQty + float.Parse(dbQtyWithPiece)).ToString();
            }

            if (!qty.Contains('.'))
            {
                qty = qty + ".0";
            }

            /* Imei */
            var inputImeiUpdate = dicProductListUpdate["imei" + counter];
            var prodCodeUpdate = dicProductConditionData["prodCode" + counter];
            var prodId = dicProductListUpdate["prodID" + counter];
            var storeIdUpdate = dicProductListUpdate["warehouse" + counter];
            var dbImei = commonFunction.getIMEIStoreWise(storeIdUpdate, prodId);


            string[] splitDbImei = dbImei.Split(',');
            string[] splitInpImei = inputImeiUpdate.Split(',');
            int inpImeiCount = 0, dbImeiCount = 0;
            string newImei = "", tempImei = "";
            bool isMatch = false;

            // remove Imei
            for (dbImeiCount = 0; dbImeiCount < splitDbImei.Length; dbImeiCount++)
            {

                isMatch = false;
                for (inpImeiCount = 0; inpImeiCount < splitInpImei.Length; inpImeiCount++)
                {
                    if (splitInpImei[inpImeiCount] == splitDbImei[dbImeiCount])
                    {
                        isMatch = true;
                        break;
                    }
                }

                if (!isMatch)
                {
                    tempImei = splitDbImei[dbImeiCount];
                    removeImei += "," + tempImei;
                }
            }

            // new imei
            for (inpImeiCount = 0; inpImeiCount < splitInpImei.Length; inpImeiCount++)
            {
                isMatch = false;
                for (dbImeiCount = 0; dbImeiCount < splitDbImei.Length; dbImeiCount++)
                {
                    if (splitDbImei[dbImeiCount] == splitInpImei[inpImeiCount])
                    {
                        isMatch = true;
                        break;
                    }


                }

                if (!isMatch)
                {
                    tempImei = splitInpImei[inpImeiCount];
                    newImei += "," + tempImei;
                }
            }

            inputImeiUpdate = newImei;

            var bPrice = Convert.ToDecimal(dicProductListUpdate["bPrice" + counter]);
            var supCommission= Convert.ToInt32(dicProductListUpdate["supCommission" + counter]);

            // Dictionary ready for update
            stockModel.prodId = Convert.ToInt32(dicProductListUpdate["prodID" + counter]);
            stockModel.prodName = dicProductListUpdate["prodName" + counter];
            stockModel.supCompany = dicProductListUpdate["supCompany" + counter];
            stockModel.catName = dicProductListUpdate["catName" + counter];
            stockModel.qty = qty;
            stockModel.bPrice = bPrice;
            stockModel.sPrice = Convert.ToDecimal(dicProductListUpdate["sPrice" + counter]);
            stockModel.stockTotal = Convert.ToDecimal(dicProductListUpdate["stockTotal" + counter]);
            stockModel.fieldAttribute = dicProductListUpdate["fieldAttribute" + counter];
            stockModel.tax = dicProductListUpdate["tax" + counter];
            stockModel.sku = dicProductListUpdate["sku" + counter];
            stockModel.lastQty = dicProductListUpdate["lastQty" + counter];
            stockModel.warranty = dicProductListUpdate["warranty" + counter];
            stockModel.imei = inputImeiUpdate;
            stockModel.dealerPrice = Convert.ToDecimal(dicProductListUpdate["dealerPrice" + counter]);
            stockModel.receivedDate = Convert.ToDateTime(dicProductListUpdate["receivedDate" + counter]);
            stockModel.expiryDate = Convert.ToDateTime(dicProductListUpdate["expiryDate" + counter]);
            stockModel.batchNo = dicProductListUpdate["batchNo" + counter];
            stockModel.serialNo = dicProductListUpdate["serialNo" + counter];
            stockModel.ShipmentStatus = Convert.ToInt32(dicProductListUpdate["shipmentStatus" + counter]);
            stockModel.manufacturerId = Convert.ToInt32(dicProductListUpdate["manufacturer" + counter]);
            stockModel.size = dicProductListUpdate["size" + counter];
            stockModel.warningQty = dicProductListUpdate["warningQty" + counter].ToString();
            stockModel.notes = dicProductListUpdate["notes" + counter];
            stockModel.unitId = Convert.ToInt32(dicProductListUpdate["unitId" + counter]);
            stockModel.warehouseId = Convert.ToInt32(dicProductListUpdate["warehouse" + counter]);
            stockModel.purchaseDate = stockModel.purchaseDate = Convert.ToDateTime(txtPurchaseDate.Text == "" ? commonFunction.GetCurrentTime().ToString() : txtPurchaseDate.Text);
            stockModel.imeiStatus = chkIMEIEnable.Checked == true ? '1' : '0';
            stockModel.engineNumber = dicProductListUpdate["engineNumber" + counter];
            stockModel.cecishNumber = dicProductListUpdate["cecishNumber" + counter];
            stockModel.purchaseCode = purchaseCode;
            stockModel.comPrice = Convert.ToDecimal(dicProductListUpdate["comPrice" + counter]);
            stockModel.locationId = Convert.ToInt32(dicProductListUpdate["location" + counter]);
            stockModel.freeQty = dicProductListUpdate["freeQty" + counter];
            stockModel.entryDate = commonFunction.GetCurrentTime();
            stockModel.roleId = Session["roleId"].ToString();
            stockModel.branchId = Session["branchId"].ToString();

            // Supplier Commission
            var getCommission = getSupplierCommission(bPrice, supCommission, Convert.ToDecimal(qty));

            stockModel.SupCommission = supCommission;
            stockModel.commissionAmt = getCommission;


            // Dictionary ready for conditional data
            stockModel.prodCode = dicProductConditionData["prodCode" + counter];
            stockModel.createdFor = dicProductConditionData["createdFor" + counter];
        }





        private string getQtyWithPice(int counter)
        {
            DataSet dsStockQty = stockModel.getStock(dicProductConditionData["prodCode" + counter],
                        dicProductConditionData["createdFor" + counter]);
            string dbQtyWithPiece = "0";
            if (dsStockQty.Tables[0].Rows.Count > 0)
            {
                dbQtyWithPiece = dsStockQty.Tables[0].Rows[0][7].ToString();
            }

            return dbQtyWithPiece;
        }






        private string RatioPlaceValue(int piece, int Qty, int dbPiece, int dbQty)
        {
            int totalQty = Qty + dbQty;
            int totalPiece = piece + dbPiece;

            string totalQtyWithPiece = totalQty.ToString() + "." + totalPiece.ToString();

            return totalQtyWithPiece;
        }





        private void saveAttributeDetailInfo()
        {
            try
            {
                string tempProductId = "";


                for (i = 0; i < attributeInputCounter; i++)
                {
                    // Delete all previous attribute detail info for once

                    objAttrDeailModel.attributeId = Convert.ToInt32(dicAttributeData["attributeId" + i]);
                    objAttrDeailModel.fieldId = Convert.ToInt32(dicAttributeData["fieldId" + i]);
                    objAttrDeailModel.stockQty = Convert.ToInt32(dicAttributeData["stockQty" + i]);
                    objAttrDeailModel.productId = Convert.ToInt32(dicAttributeData["productId" + i]);

                    if (tempProductId != dicAttributeData["productId" + i])
                    {
                        // Delete all existing attribute 
                        objAttrDeailModel.DeleteAttributeSingleProduct(dicAttributeData["productId" + i]);
                        tempProductId = dicAttributeData["productId" + i];
                    }


                    objAttrDeailModel.saveAttributeDetail();
                }
            }
            catch (Exception)
            {
                ScriptMessage("Attribute is not saved! Please contact with your system administrator.",
                    MessageType.Error);
                MultiReset();
            }
        }





        protected void btnCancelMultiStock_Click(object sender, EventArgs e)
        {
            MultiReset();
        }





        protected void btnEditPurchaseAmt_Click(object sender, EventArgs e)
        {
        }





        protected void btnUpdatePurchaseAmt_Click(object sender, EventArgs e)
        {
        }





        protected void btnEditPurAmt_Click(object sender, EventArgs e)
        {
            txtStockAmount.Enabled = true;
            txtStockAmount.CssClass = "PurchaseTxtBoxEnable form-control";

            // Show product item
            ShowShoppingList();
        }





        private void UpsertAttributeDetail(string tempFieldAttribute)
        {
            try
            {
                divAttributeList.Controls.Clear();

                dsLoadField = objSql.getDataSet("SELECT * FROM [FieldInfo] WHERE active='1'");
                if (dsLoadField.Tables[0].Rows.Count > 0)
                    fieldCount = dsLoadField.Tables[0].Rows.Count;

                divAttributeList.Controls.Add(new LiteralControl("<div class='panel-group' id='accordion' role='tablist'>"));

                int k = 0;
                for (i = 0; i < dsLoadField.Tables[0].Rows.Count; i++)
                {
                    // load html
                    divAttributeList.Controls.Add(new LiteralControl("<div class='panel-default form-group'>"));
                    var collapseId = "collapse" + i;
                    Label tempField;

                    // load field
                    string fieldId = dsLoadField.Tables[0].Rows[i][0].ToString();
                    tempField = new Label
                    {
                        Text = "Collapse: " + dsLoadField.Tables[0].Rows[i][1],
                        CssClass = "lbl col-sm-4 control-label"
                    };
                    tempField.Attributes.Add("data-toggle", "collapse");
                    tempField.Attributes.Add("data-parent", "#accordion");
                    tempField.Attributes.Add("role", "button");
                    tempField.Attributes.Add("href", "#" + collapseId);
                    divAttributeList.Controls.Add(tempField);

                    // load html
                    divAttributeList.Controls.Add(
                        new LiteralControl("<div id='" + collapseId +
                                           "' collapseOne' class='panel-collapse collapse col-sm-8' role='tabpanel'>"));

                    // load attributes 
                    DataSet dsLoadAttribute =
                        objSql.getDataSet("SELECT * FROM [AttributeInfo] WHERE fieldId='" + fieldId + "' AND active='1' ");

                    for (j = 0; j < dsLoadAttribute.Tables[0].Rows.Count; j++)
                    {
                        divAttributeList.Controls.Add(new LiteralControl("<div class='row'>"));

                        // Get attribute id and name
                        string attributeId = dsLoadAttribute.Tables[0].Rows[j][0].ToString();
                        string attributeName = dsLoadAttribute.Tables[0].Rows[j][2].ToString();

                        // Add atrribute label
                        tempField = new Label { Text = attributeName, CssClass = "lbl col-sm-4 control-label" };
                        divAttributeList.Controls.Add(tempField);

                        // Add atrribute qty input box
                        divAttributeList.Controls.Add(new LiteralControl("<div class='col-md-8 form-group'>"));
                        txtAttribute[k] = new TextBox
                        {
                            ID = fieldId + "contentBody_" + attributeId,
                            CssClass = "form-control"
                        };

                        // If exit
                        //txtAttribute[k].Text("");

                        divAttributeList.Controls.Add(txtAttribute[k]);
                        divAttributeList.Controls.Add(new LiteralControl("</div></div>"));

                        k++;
                    }

                    // load html 
                    divAttributeList.Controls.Add(new LiteralControl("</div></div>"));
                }

                attributeCount = k;
                divAttributeList.Controls.Add(new LiteralControl("</div>"));
            }
            catch (Exception)
            {

            }
        }





        // Load Attribute to dynamic textbox
        private void AttributeLoadToTextbox()
        {
            // Get ProductId 
            string productId = commonFunction.getProductId(txtScanCode.Text, false);

            DataSet dsField = objSql.getDataSet("SELECT * FROM FieldInfo WHERE Active = '1'");
            for (int i = 0; i < dsField.Tables[0].Rows.Count; i++)
            {
                string[] splitAttributeDetail = new string[] { };


                string fieldId = dsField.Tables[0].Rows[i][0].ToString();


                DataSet dsAttributeDetail = objAttrDeailModel.findAttributeDetail(productId, fieldId);

                DataSet dsAttribute = objAttrDeailModel.getAttributeInfo(fieldId);


                for (int j = 0; j < dsAttributeDetail.Tables[0].Rows.Count; j++)
                {
                    splitAttributeDetail = txtAttribute[j].ID.Split(new string[] { "contentBody_" },
                        StringSplitOptions.None);

                    int attributeDetailId = Convert.ToInt32(dsAttributeDetail.Tables[0].Rows[j][2]);
                    string attributeStockQty = dsAttributeDetail.Tables[0].Rows[j][5].ToString();

                    string attId = "contentBody_" + fieldId + "contentBody_" + attributeDetailId.ToString();

                    //txtAttribute[attributeDetailId-1].Text = attributeStockQty;

                    for (int n = 0; n < dsAttribute.Tables[0].Rows.Count; n++)
                    {
                        if (txtAttribute[n].ClientID == attId)
                            txtAttribute[n].Text = attributeStockQty;
                    }
                }
            }

            // lblTest.Text = totalAttributeColumnCount;
        }





        protected void btnImportFile_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                isImportProduct = true;
                string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);

                //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FolderPath = Server.MapPath("~/Files/");
                string FilePath = (FolderPath + FileName);
                fileUpload.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension);

            }
        }





        // https://www.aspsnippets.com/Articles/Read-and-Import-Excel-File-into-DataSet-or-DataTable-using-C-and-VBNet-in-ASPNet.aspx
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
            //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\metaPOS\MetaPOS\Files\Inventory_4_24_2019 1_58_35 PM.xlsx; Extended Properties='Excel 8.0;HDR=Yes';
            //conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\metaPOS\MetaPOS\Files\Inventory_4_24_2019 4_25_28 PM.xlsx; Extended Properties='Excel 12.0;HDR=Yes'";
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

            int rCnt, counter = 0, contiErrorCounter = 0;
            string pName, pDescritopn, errorRows = "", duppliRows = "";
            string sku = "", store_id = "";

            string supplierValue = "";

            List<string> updateList = new List<string>();

            int count = ds.Tables[0].Rows.Count;



            try
            {

                if (count > 0)
                {
                    var isExitingProduct = false;

                    // New Product Id

                    DataSet dsCount, dsProdID;
                    var _prodId = 0;
                    dsCount = objSql.getDataSet("Select * FROM StockInfo");
                    if (dsCount.Tables[0].Rows.Count > 0)
                    {
                        dsProdID = objSql.getDataSet("SELECT MAX(prodId) as ProdID FROM StockInfo");
                        _prodId = Convert.ToInt32(dsProdID.Tables[0].Rows[0][0]);

                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // prod ID
                        string _importProdID = ds.Tables[0].Rows[i][0].ToString();

                        int importProdID = 0;
                        if (!String.IsNullOrEmpty(_importProdID.ToString()))
                        {
                            isExitingProduct = true;
                            importProdID = Convert.ToInt32(_importProdID);
                        }
                        else
                        {
                            isExitingProduct = false;


                            importProdID = _prodId + 1;
                        }

                        // Common parameter
                        var _productName = ds.Tables[0].Rows[i][1].ToString();
                        var _prodCode = ds.Tables[0].Rows[i][2].ToString();
                        var _Supplier = ds.Tables[0].Rows[i][3].ToString();
                        var _Category = ds.Tables[0].Rows[i][4].ToString();
                        var _sku = ds.Tables[0].Rows[i][5].ToString();
                        var _qty = ds.Tables[0].Rows[i][6].ToString();
                        var _bPrice = ds.Tables[0].Rows[i][7].ToString();
                        var _dPrice = ds.Tables[0].Rows[i][8].ToString();
                        var _sPrice = ds.Tables[0].Rows[i][9].ToString();
                        var _unitId = ds.Tables[0].Rows[i][10].ToString();
                        var _storeId = ds.Tables[0].Rows[i][11].ToString();
                        var _comPrice = ds.Tables[0].Rows[i][12].ToString();


                        // 
                        if (String.IsNullOrEmpty(_qty))
                            _qty = "0.0";

                        if (!_qty.Contains("."))
                        {
                            _qty = _qty + ".0";
                        }

                        if (String.IsNullOrEmpty(_bPrice))
                            _bPrice = "0";

                        if (String.IsNullOrEmpty(_dPrice))
                            _dPrice = "0";

                        if (String.IsNullOrEmpty(_sPrice))
                            _sPrice = "0";
                        if (String.IsNullOrEmpty(_unitId) || !stockModel.checkUnitIdExist(_unitId))
                            _unitId = "0";

                        if (String.IsNullOrWhiteSpace(_comPrice))
                            _comPrice = "0";

                        if (_prodCode == "")
                            setBarcode = BarCodeGeneratorForBulkStock();
                        else
                            setBarcode = _prodCode;

                        int num1;
                        float num2;
                        bool _isSupCompany = int.TryParse(_Supplier, out num1);
                        bool _isCatName = int.TryParse(_Category, out num1);
                        //bool _isQty = int.TryParse(_qty, out num1);
                        bool _isBPrice = float.TryParse(_bPrice, out num2);
                        bool _isWholesalePrice = float.TryParse(_dPrice, out num2);
                        bool _isSalePrice = float.TryParse(_sPrice, out num2);


                        // Check supplier validation
                        var dtSupplier =
                            objSql.getDataTable("SELECT * FROM SupplierInfo WHERE supID = '" + _Supplier + "'");
                        var dtCategory =
                            objSql.getDataTable("SELECT * FROM CategoryInfo WHERE Id = '" + _Category + "'");

                        var dtStore =
                            objSql.getDataTable("SELECT * FROM WarehouseInfo WHERE Id !='' AND Id = '" + _storeId + "'");

                        if (!string.IsNullOrEmpty(_productName)
                            && !string.IsNullOrEmpty(_Supplier)
                            && !string.IsNullOrEmpty(_Category)
                            && (_isSupCompany)
                            && (_isCatName)
                            && (_isBPrice)
                            && (_isWholesalePrice)
                            && (_isSalePrice)
                            && dtSupplier.Rows.Count > 0
                            && dtCategory.Rows.Count > 0
                            && dtStore.Rows.Count > 0 &&
                            _prodCode != "0" &&
                            _unitId != "")
                        {

                            if (isExitingProduct)
                            {
                                if (Convert.ToDecimal(_qty) >= 0)
                                {
                                    dicProductListUpdate.Add("prodID" + stockCounter, importProdID.ToString());
                                    dicProductListUpdate.Add("prodCode" + stockCounter, setBarcode);
                                    dicProductListUpdate.Add("prodName" + stockCounter, _productName.Replace("\'", ""));
                                    dicProductListUpdate.Add("prodDescr" + stockCounter, "");
                                    dicProductListUpdate.Add("supCompany" + stockCounter, _Supplier);
                                    dicProductListUpdate.Add("catName" + stockCounter, _Category);
                                    dicProductListUpdate.Add("qty" + stockCounter, _qty);
                                    dicProductListUpdate.Add("bPrice" + stockCounter, _bPrice);
                                    dicProductListUpdate.Add("sPrice" + stockCounter, _sPrice);
                                    dicProductListUpdate.Add("weight" + stockCounter, "0.00");
                                    dicProductListUpdate.Add("size" + stockCounter, "");
                                    dicProductListUpdate.Add("discount" + stockCounter, "0.00");
                                    dicProductListUpdate.Add("stockTotal" + stockCounter,
                                        (Convert.ToDecimal(_bPrice) * Convert.ToDecimal(_qty)).ToString());
                                    dicProductListUpdate.Add("entryDate" + stockCounter,
                                        commonFunction.GetCurrentTime().ToShortDateString());
                                    dicProductListUpdate.Add("entryQty" + stockCounter, "0");
                                    dicProductListUpdate.Add("title" + stockCounter, "");
                                    dicProductListUpdate.Add("branchName" + stockCounter, "");
                                    dicProductListUpdate.Add("fieldAttribute" + stockCounter, "");
                                    dicProductListUpdate.Add("tax" + stockCounter, "");
                                    dicProductListUpdate.Add("sku" + stockCounter, _sku);
                                    dicProductListUpdate.Add("lastQty" + stockCounter, "0");
                                    dicProductListUpdate.Add("warranty" + stockCounter, "");
                                    dicProductListUpdate.Add("imei" + stockCounter, "");
                                    dicProductListUpdate.Add("warningQty" + stockCounter, "0");
                                    dicProductListUpdate.Add("dealerPrice" + stockCounter, _dPrice);
                                    dicProductListUpdate.Add("purchaseCode" + stockCounter, "");
                                    dicProductListUpdate.Add("createdFor" + stockCounter, "");
                                    dicProductListUpdate.Add("receivedDate" + stockCounter,
                                        commonFunction.GetCurrentTime().ToShortDateString());
                                    dicProductListUpdate.Add("expiryDate" + stockCounter,
                                        commonFunction.GetCurrentTime().ToShortDateString());
                                    dicProductListUpdate.Add("batchNo" + stockCounter, "");
                                    dicProductListUpdate.Add("serialNo" + stockCounter, "");
                                    dicProductListUpdate.Add("shipmentStatus" + stockCounter, "0");
                                    dicProductListUpdate.Add("manufacturer" + stockCounter, "0");
                                    dicProductListUpdate.Add("notes" + stockCounter, "");
                                    dicProductListUpdate.Add("unitId" + stockCounter, _unitId);
                                    dicProductListUpdate.Add("piece" + stockCounter, "0");
                                    dicProductListUpdate.Add("warehouse" + stockCounter, _storeId);
                                    dicProductListUpdate.Add("supCommission" + stockCounter, "0");
                                    dicProductListUpdate.Add("engineNumber" + stockCounter, "");
                                    dicProductListUpdate.Add("cecishNumber" + stockCounter, "");
                                    dicProductListUpdate.Add("comPrice" + stockCounter, _comPrice);
                                    dicProductListUpdate.Add("location" + stockCounter, "0");
                                    dicProductListUpdate.Add("freeQty" + stockCounter, "0.0");
                                    dicProductListUpdate.Add("variant" + stockCounter, "");


                                    supplierValue = dicProductListUpdate["supCompany" + stockCounter];

                                    // Add ecommerce info
                                    dicEcommerceUpdate.Add("eprodCode" + stockCounter, setBarcode);
                                    dicEcommerceUpdate.Add("image" + stockCounter, "");
                                    dicEcommerceUpdate.Add("prodTitle" + stockCounter, _productName);
                                    dicEcommerceUpdate.Add("oPrice" + stockCounter, _sPrice);
                                    dicEcommerceUpdate.Add("prodId" + stockCounter, importProdID.ToString());


                                    dicProductConditionData.Add("prodCode" + stockCounter, setBarcode.ToString());
                                    dicProductConditionData.Add("createdFor" + stockCounter,
                                        Session["roleId"].ToString());

                                    //
                                    updateList.Add("update-");

                                    arrUpdateData[stockCounter] = setBarcode;
                                    updateCount++;
                                    stockCounter++;
                                }
                            }
                            else
                            {
                                dicProductListCreate.Add("prodId" + stockCounter, importProdID.ToString());
                                dicProductListCreate.Add("prodCode" + stockCounter, setBarcode);
                                dicProductListCreate.Add("prodName" + stockCounter, _productName.Replace("\'", ""));
                                dicProductListCreate.Add("prodDescr" + stockCounter, "");
                                dicProductListCreate.Add("supCompany" + stockCounter, _Supplier);
                                dicProductListCreate.Add("catName" + stockCounter, _Category);
                                dicProductListCreate.Add("qty" + stockCounter, _qty);
                                dicProductListCreate.Add("bPrice" + stockCounter, _bPrice);
                                dicProductListCreate.Add("sPrice" + stockCounter, _sPrice);
                                dicProductListCreate.Add("weight" + stockCounter, "0.00");
                                dicProductListCreate.Add("size" + stockCounter, "");
                                dicProductListCreate.Add("discount" + stockCounter, "0.00");
                                dicProductListCreate.Add("stockTotal" + stockCounter, (Convert.ToDecimal(_bPrice) * Convert.ToDecimal(_qty)).ToString());
                                dicProductListCreate.Add("entryDate" + stockCounter, commonFunction.GetCurrentTime().ToShortDateString());
                                dicProductListCreate.Add("entryQty" + stockCounter, "0");
                                dicProductListCreate.Add("title" + stockCounter, "");
                                dicProductListCreate.Add("branchName" + stockCounter, "");
                                dicProductListCreate.Add("fieldAttribute" + stockCounter, "");
                                dicProductListCreate.Add("tax" + stockCounter, "");
                                dicProductListCreate.Add("sku" + stockCounter, _sku);
                                dicProductListCreate.Add("lastQty" + stockCounter, "0");
                                dicProductListCreate.Add("warranty" + stockCounter, "");
                                dicProductListCreate.Add("imei" + stockCounter, "");
                                dicProductListCreate.Add("warningQty" + stockCounter, "0");
                                dicProductListCreate.Add("dealerPrice" + stockCounter, _dPrice);
                                dicProductListCreate.Add("purchaseCode" + stockCounter, "");
                                dicProductListCreate.Add("createdFor" + stockCounter, "");
                                dicProductListCreate.Add("receivedDate" + stockCounter, commonFunction.GetCurrentTime().ToShortDateString());
                                dicProductListCreate.Add("expiryDate" + stockCounter, commonFunction.GetCurrentTime().ToShortDateString());
                                dicProductListCreate.Add("batchNo" + stockCounter, "");
                                dicProductListCreate.Add("serialNo" + stockCounter, "");
                                dicProductListCreate.Add("shipmentStatus" + stockCounter, "0");
                                dicProductListCreate.Add("manufacturer" + stockCounter, "0");
                                dicProductListCreate.Add("notes" + stockCounter, "");
                                dicProductListCreate.Add("unitId" + stockCounter, _unitId);
                                dicProductListCreate.Add("piece" + stockCounter, "0");
                                dicProductListCreate.Add("warehouse" + stockCounter, _storeId);
                                dicProductListCreate.Add("supCommission" + stockCounter, "0");
                                dicProductListCreate.Add("engineNumber" + stockCounter, "");
                                dicProductListCreate.Add("cecishNumber" + stockCounter, "");
                                dicProductListCreate.Add("comPrice" + stockCounter, "0");
                                dicProductListCreate.Add("location" + stockCounter, "0");
                                dicProductListCreate.Add("freeQty" + stockCounter, "0.0");
                                dicProductListCreate.Add("variant" + stockCounter, "");


                                supplierValue = dicProductListCreate["supCompany" + stockCounter];

                                arrInsertData[stockCounter] = setBarcode;


                                // Add ecommerce info
                                dicEcommerceCreate.Add("eprodCode" + stockCounter, setBarcode);
                                dicEcommerceCreate.Add("image" + stockCounter, "");
                                dicEcommerceCreate.Add("prodTitle" + stockCounter, _productName);
                                dicEcommerceCreate.Add("oPrice" + stockCounter, _sPrice);
                                dicEcommerceCreate.Add("prodId" + stockCounter, importProdID.ToString()); ;

                                //
                                updateList.Add("insert-");
                                stockCounter++;
                                insertCount++;

                                // create barcode
                                File.WriteAllText(Server.MapPath("BarcodeTool/BarCode.txt"), setBarcode);

                                //http://stackoverflow.com/questions/4291912/process-start-how-to-get-the-output
                                Process process = Process.Start(Server.MapPath("BarcodeTool/BarCodeGenerate.exe"));
                                if (process != null) process.WaitForExit();

                                // Check barcode image create or not
                                if (!File.Exists(Server.MapPath("BarcodeTool/images/" + setBarcode + ".png")))
                                {
                                    ScriptMessage("Build barcode image failed! Please try again.", MessageType.Warning);
                                    return;
                                }

                            }


                        }
                        else
                        {

                            errorRows += (i + 2) + ", ";
                        }

                        if (_productName == "")
                        {
                            contiErrorCounter++;
                        }
                        else
                        {
                            // err count reinitilize
                            contiErrorCounter = 0;
                        }


                        // product name null > 10 return;
                        if (contiErrorCounter > 10)
                            break;


                    }
                    errorRows = errorRows.TrimEnd();


                    //Suplier add to list
                    if (!dicSupplierList.ContainsValue(supplierValue))
                    {
                        dicSupplierList.Add("supCompany" + supplierCounter, supplierValue);
                        supplierCounter++;
                    }

                    lblmsg.Text = "Imported " + stockCounter + " rows out of " + count +
                                  " rows <br/> <span style='color:red;'> Error rows: " + errorRows + ". Dupplicate rows: " + duppliRows + ".</span> ";
                    updateListStatus += string.Join("", updateList.ToArray());

                    MultiStockList();


                    Reset();
                }
            }
            catch (Exception ex)
            {
                Reset();
                lblmsg.Text = " " + ex.ToString();
            }

            ////Bind Data to GridView
            //GridView1.Caption = Path.GetFileName(FilePath);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }





        private void ExcelToDictionary(string path)
        {
            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;

                int rCnt, counter = 0;
                string pName, pDescritopn;
                string sku = "";
                int pQty = 0, pSupplier = 0, pCategory = 0;
                decimal pbPrice, psPrice, pDeaderPrice, branchid;
                pbPrice = psPrice = pDeaderPrice = branchid = 0;

                //Upload and save the file
                //string pathLocation = Server.MapPath("~/Files/");
                //string path = pathLocation + Path.GetFileName(fileUpload.PostedFile.FileName);

                //if (Directory.Exists(path))
                //    Directory.Delete(path);

                //fileUpload.SaveAs(path);
                lblTest.Text += "Pre <br/>";
                xlApp = new Excel.Application();
                lblTest.Text += "App Ok <br/>";
                //xlWorkBook = xlApp.Workbooks.Open(@"F:\SampleBulkStock.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //xlWorkBook = (Excel.Workbook)xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkBook = xlApp.Workbooks.Open(path);
                lblTest.Text += "Open Ok <br/>";

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                lblTest.Text += "Item Ok <br/>";

                range = xlWorkSheet.UsedRange;
                rCnt = range.Rows.Count;
                string supplierValue = "";

                List<string> updateList = new List<string>();

                for (int r = 2; r <= rCnt; r++)
                {
                    //var _branch = (xlWorkSheet.Cells[r, 1] as Excel.Range).Value;
                    var _supplier = (xlWorkSheet.Cells[r, 1] as Excel.Range).Value;
                    var _category = (xlWorkSheet.Cells[r, 2] as Excel.Range).Value;

                    if (
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[r, 1] as Excel.Range).Text.ToString()
                            .Length > 0)
                    {
                        if (
                            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[r, 2] as Excel.Range).Text
                                .ToString().Length > 0)
                        {
                            //if (((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[r, 3] as Excel.Range).Text.ToString().Length > 0)
                            //{
                            //int value1, value2, value3;
                            //if (int.TryParse(_branch, out value1) && int.TryParse(_supplier, out value2) && int.TryParse(_category, out value3))
                            //{
                            setBarcode = BarCodeGeneratorForBulkStock();

                            //branchid = (decimal)(xlWorkSheet.Cells[r, 1] as Excel.Range).Value;
                            pSupplier = (int)(xlWorkSheet.Cells[r, 1] as Excel.Range).Value;
                            pCategory = (int)(xlWorkSheet.Cells[r, 2] as Excel.Range).Value;
                            sku = (string)(xlWorkSheet.Cells[r, 3] as Excel.Range).Value;
                            pName = (string)(xlWorkSheet.Cells[r, 4] as Excel.Range).Value;
                            pQty = (int)(xlWorkSheet.Cells[r, 5] as Excel.Range).Value;
                            pbPrice = (decimal)(xlWorkSheet.Cells[r, 6] as Excel.Range).Value;
                            pDeaderPrice = (decimal)(xlWorkSheet.Cells[r, 7] as Excel.Range).Value;
                            psPrice = (decimal)(xlWorkSheet.Cells[r, 8] as Excel.Range).Value;


                            if (pQty.ToString() == "")
                                pQty = 0;
                            if (pbPrice.ToString() == "")
                                pbPrice = 0;
                            if (psPrice.ToString() == "")
                                psPrice = 0;


                            dicProductListCreate.Add("prodId" + stockCounter, prodId.ToString());
                            dicProductListCreate.Add("prodCode" + stockCounter, setBarcode.ToString());
                            dicProductListCreate.Add("prodName" + stockCounter, pName.ToString().Replace("\'", ""));
                            dicProductListCreate.Add("prodDescr" + stockCounter, "");
                            dicProductListCreate.Add("supCompany" + stockCounter, pSupplier.ToString());
                            dicProductListCreate.Add("catName" + stockCounter, pCategory.ToString());
                            dicProductListCreate.Add("qty" + stockCounter, pQty.ToString());
                            dicProductListCreate.Add("bPrice" + stockCounter, pbPrice.ToString());
                            dicProductListCreate.Add("sPrice" + stockCounter, psPrice.ToString());
                            dicProductListCreate.Add("weight" + stockCounter, "0.00");
                            dicProductListCreate.Add("size" + stockCounter, "");
                            dicProductListCreate.Add("discount" + stockCounter, "0.00");
                            dicProductListCreate.Add("stockTotal" + stockCounter, (pQty * pbPrice).ToString("0.00"));
                            dicProductListCreate.Add("entryDate" + stockCounter,
                                commonFunction.GetCurrentTime().ToShortDateString());
                            dicProductListCreate.Add("entryQty" + stockCounter, pQty.ToString());
                            dicProductListCreate.Add("title" + stockCounter, "");
                            dicProductListCreate.Add("branchName" + stockCounter, "");
                            dicProductListCreate.Add("fieldAttribute" + stockCounter, "");
                            dicProductListCreate.Add("tax" + stockCounter, "");
                            dicProductListCreate.Add("sku" + stockCounter, sku.ToString());
                            dicProductListCreate.Add("lastQty" + stockCounter, "0");
                            dicProductListCreate.Add("warranty" + stockCounter, "0");
                            dicProductListCreate.Add("imei" + stockCounter, "");
                            dicProductListCreate.Add("warningQty" + stockCounter, "0");
                            dicProductListCreate.Add("dealerPrice" + stockCounter, pDeaderPrice.ToString());
                            dicProductListCreate.Add("purchaseCode" + stockCounter, txtPurchaseCode.Text);
                            dicProductListCreate.Add("createdFor" + stockCounter, ddlBrahchForImport.SelectedValue);


                            supplierValue = dicProductListCreate["supCompany" + stockCounter].ToString();

                            arrInsertData[stockCounter] = setBarcode.ToString();

                            //
                            updateList.Add("insert-");
                            stockCounter++;
                            insertCount++;
                            //}
                            //}
                            //else
                            //{
                            //    lblImport.Visible = true;
                            //    lblImport.Text += r.ToString() + ", ";
                            //}
                        }
                        else
                        {
                            lblImport.Visible = true;
                            lblImport.Text += r.ToString() + ", ";
                        }
                    }
                    else
                    {
                        //lblTest.Text += r.ToString() + "" + Environment.NewLine;
                        lblImport.Visible = true;
                        lblImport.Text += r.ToString() + ", ";
                    }

                    lblmsg.Text = "Import " + stockCounter + " rows out of " + (rCnt - 1) + ".";
                    updateListStatus += string.Join("", updateList.ToArray());
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);


                MultiStockList();

                if (Directory.Exists(path))
                    Directory.Delete(path);

                Reset();
            }
            catch (Exception ex)
            {
                lblTest.Text += ex.ToString();
            }
        }





        private int? TryParse2(string s)
        {
            int i;
            if (!int.TryParse(s, out i))
            {
                return null;
            }
            else
            {
                return i;
            }
        }





        public void LoadShipmentStatusInfo()
        {
            Array _status = Enum.GetValues(typeof(ShipmentStatus));
            foreach (ShipmentStatus status in _status)
            {
                ddlShipmentStatus.Items.Add(new ListItem(status.ToString(), ((int)status).ToString()));
            }
        }





        protected void ddlUnit_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ShowShoppingList();
        }





        private void changeUnit()
        {
            if (ddlUnit.SelectedValue == "0")
            {
                divPiece.Visible = false;
            }
            else
            {
                divPiece.Visible = true;
            }
        }





        protected void btnEditParchaseCode_OnClick(object sender, EventArgs e)
        {
            txtPurchaseCode.Enabled = true;
            ShowShoppingList();
        }








        protected void btnUnitEditPurAmt_OnClick(object sender, EventArgs e)
        {
            txtUnitTotalPurchaseAmt.Enabled = true;
            txtUnitTotalPurchaseAmt.CssClass = "PurchaseTxtBoxEnable form-control";
        }





        protected void unitTotalPurchaseAmt_click(object sender, EventArgs e)
        {

            string qty = "0", bPrice = "0";
            if (txtQty.Text != "")
                qty = txtQty.Text;
            if (txtBPrice.Text != "")
                bPrice = txtBPrice.Text;

            var originalQty = 0;
            var originalPiece = 0;

            var newQty = txtQty.Text == "" ? "0" : txtQty.Text;

            if (commonFunction.findSettingItemValueDataTable("offer") == "1" && lblProdID.Text != "")
            {
                var prodCode = txtScanCode.Text;
                if (prodCode != "")
                {
                    var offerValue = 1;
                    var discountValue = "0";
                    var dtOffer = objSql.getDataTable("SELECT * FROM OfferInfo WHERE prodId='" + lblProdID.Text + "'");
                    if (dtOffer.Rows.Count > 0)
                    {
                        var offerValueWithPoint = dtOffer.Rows[0]["offerValue"].ToString();
                        if (offerValueWithPoint.Contains("."))
                        {
                            offerValue = Convert.ToInt32(offerValueWithPoint.Split('.')[0]);
                        }
                        else
                        {
                            offerValue = Convert.ToInt32(offerValueWithPoint);
                        }

                        discountValue = dtOffer.Rows[0]["discountValue"].ToString();

                    }

                    var freeQty = 0;

                    var qtyWithoutPiece = 0;
                    var piece = 0;
                    if (Convert.ToDecimal(newQty) > 0)
                    {
                        if (newQty.Contains("."))
                        {
                            qtyWithoutPiece = Convert.ToInt32(newQty.Split('.')[0]);
                            piece = Convert.ToInt32(newQty.Split('.')[1]);
                        }
                        else
                        {
                            qtyWithoutPiece = Convert.ToInt32(newQty);
                        }

                        freeQty = qtyWithoutPiece / offerValue;
                    }

                    var freeQtyWithoutPiece = 0;
                    var freePiece = 0;
                    if (discountValue.Contains("."))
                    {
                        freeQtyWithoutPiece = Convert.ToInt32(discountValue.Split('.')[0]);
                        freePiece = Convert.ToInt32(discountValue.Split('.')[1]);
                    }
                    else
                    {
                        freeQtyWithoutPiece = Convert.ToInt32(discountValue);
                    }

                    // get total free qty
                    var totalFreeQty = freeQty * freeQtyWithoutPiece;
                    var totalFreePiece = freeQty * freePiece;

                    var ratio = 1;
                    var dtUnit =
                        objSql.getDataTable(
                            "SELECT unit.unitRatio FROM StockInfo as stock LEFT JOIN UnitInfo as unit ON stock.UnitId = unit.Id WHERE stock.prodId='" +
                            lblProdID.Text + "'");
                    if (dtUnit.Rows.Count > 0 && dtUnit.Rows[0][0].ToString() != "")
                    {
                        ratio = Convert.ToInt32(dtUnit.Rows[0][0].ToString());
                    }

                    if (totalFreePiece > ratio)
                    {
                        totalFreeQty += totalFreePiece / ratio;
                        totalFreePiece = totalFreePiece % ratio;
                    }

                    txtFreeQty.Text = totalFreeQty + "." + totalFreePiece;


                    // original qty
                    originalQty = qtyWithoutPiece - totalFreeQty;
                    originalPiece = piece - totalFreePiece;
                }
            }
            else
            {
                if (newQty.Contains("."))
                {
                    originalQty = Convert.ToInt32(newQty.Split('.')[0]);
                    originalPiece = Convert.ToInt32(newQty.Split('.')[1]);
                }
                else
                {
                    originalQty = Convert.ToInt32(newQty);
                }
            }

            var totalQtyAmt = Convert.ToDecimal(originalQty) * Convert.ToDecimal(bPrice);
            var totalPieceAmt = Convert.ToDecimal(originalPiece) * Convert.ToDecimal(bPrice);

            decimal totalPurchaseAmt = totalQtyAmt + totalPieceAmt / 100;

            txtUnitTotalPurchaseAmt.Text = totalPurchaseAmt.ToString("0.00");

            ShowShoppingList();

            txtBPrice.Focus();

        }





        protected void ddlWarehouse_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string dtStoreWiseQty = "0";
            if (lblProdID.Text != "")
            {
                var stock = new InventoryBundle.Service.Stock();
                dtStoreWiseQty = stock.getStoreWiseQtyByStoreID(txtScanCode.Text, ddlWarehouse.SelectedValue);
                txtIMEI.Value = commonFunction.getIMEIStoreWise(ddlWarehouse.SelectedValue, lblProdID.Text);
            }

            lblCurrentQty.Text = dtStoreWiseQty;

            ShowShoppingList();

        }





        protected void btnImportSample_OnClick(object sender, EventArgs e)
        {

        }





        public void loadProductVarient()
        {
            divFieldList.Controls.Add(new LiteralControl("<div class='form-group'>"));
            divFieldList.Controls.Add(new LiteralControl("<div class='col-md-6'><input type='text' class='form-control'/></div>"));
            divFieldList.Controls.Add(new LiteralControl("<div class='col-md-6'><input type='text' class='form-control'/></div>"));
            divFieldList.Controls.Add(new LiteralControl("</div>"));
        }



        [WebMethod]
        public static List<ListItem> getVariantDataAction(string type, string value)
        {
            var variant = new Variant();
            return variant.getVariantData(type, value);
        }





        protected void btnImeiChecker_OnClick(object sender, EventArgs e)
        {
            SetFocus(txtIMEI);
        }



    }


    public enum ShipmentStatus
    {
        Select = 0,
        Stocked = 1,
        Moving = 2,
        Shipped = 3
    }


}
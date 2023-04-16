using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Helpers;
using System.Web.Services.Description;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.Service;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.InstallmentBundle.View;
using MetaPOS.Admin.InstallmentBundle.Service;
using MetaPOS.Admin.SaleBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Reflection;
using ListItem = System.Web.UI.WebControls.ListItem;


namespace MetaPOS.Admin.SaleBundle.View
{


    public partial class Invoice : BasePage
    {


        public string DisAmt { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var commonFunction = new CommonFunction();
                var saleInvoice = new SaleInvoice();

                if (!commonFunction.accessChecker("Invoice"))
                {
                    commonFunction.pageout();
                }

                var isSecureAccount = false;
                var roleId = Session["roleID"].ToString();
                var expireDate = commonFunction.GetCurrentTime();
                var roleModel = new RoleModel();
                var dtRole = roleModel.getRoleDataModelByRoleId(roleId);
                if (dtRole.Rows.Count > 0)
                {
                    expireDate = Convert.ToDateTime(dtRole.Rows[0]["ExpiryDate"].ToString());
                    isSecureAccount = Convert.ToBoolean(dtRole.Rows[0]["isSecureAccount"]);
                }

                if (expireDate.Date <= commonFunction.GetCurrentTime().Date)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                         "<script>$('#expiryModal').modal('show');</script>", false);
                }

                if (!isSecureAccount)
                {
                    Response.Redirect("~//admin/security?secure=false");
                   
                }


                lblStockProblemId.Text = Session["stockProblmemId"].ToString();

                lblBillNoHidden.Value = saleInvoice.getSaleLastID();


                if (Request["from"] == "slip")
                {
                    lblBillingNo.Text = Request["id"];
                    txtBillingNo.Text = Request["id"];
                    lblBillNoHidden.Value = Request["id"];
                    


                    ShortInvoice.printBillingNo = lblBillingNo.Text;
                    ReceiptInvoice.printBillingNo = lblBillingNo.Text;
                    GodownInvoice.printBillingNo = lblBillingNo.Text;
                }
                else
                {
                    string billNo = saleInvoice.getNextBillNo();

                    // cs print
                    saleInvoice.getLastBillNo(billNo);

                    lblBillingDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
                    txtBillingDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
                }

                // Load staff list
                commonFunction.fillAllDdl(ddlStaff, "SELECT DISTINCT staffID, name FROM [StaffInfo] WHERE active='1' and department='0' and storeId='" + Session["storeId"] + "'", "name", "staffID");
                ddlStaff.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_select_person, "0"));

                // Load Referred list
                //commonFunction.fillAllDdl(ddlReferredBy, "SELECT DISTINCT staffID, name FROM [StaffInfo] WHERE active='1' and department='1' and storeId='" + Session["storeId"] + "'", "name", "staffID");
                //ddlReferredBy.Items.Insert(0, new ListItem("Select Person", "0"));

                // Load bank name list
                commonFunction.fillAllDdl(ddlBankName,
                    "SELECT DISTINCT bankName, Id FROM [BankNameInfo] WHERE active='1'" + Session["userAccessParameters"], "bankName", "Id");
                ddlBankName.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_select_bank, "0"));

                // Load card list
                commonFunction.fillAllDdl(ddlCardType,
                    "SELECT DISTINCT cardName, Id FROM [CardTypeInfo] WHERE active='1'" + Session["userAccessParameters"], "cardName", "Id");
                ddlCardType.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_select_card_type, "0"));


                txtBillingDate.Text = txtPayDate.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy ");



                lblSaleById.Text = Session["roleID"].ToString();
                rblPaperSizeOption.SelectedValue = commonFunction.findSettingItemValueDataTable("SalesPrintPageType");


                    if (commonFunction.findSettingItemValueDataTable("imei") == "1")
                        rblSearchIn.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_IMEI, "3"));
                    if (commonFunction.findSettingItemValueDataTable("displayService") == "1")
                        rblSearchIn.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_service, "2"));
                    rblSearchIn.Items.Insert(0, new ListItem(Resources.Language.Link_package, "1"));
                    rblSearchIn.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_Product, "0"));

                    rblInterestType.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_down_payment, "0"));
                    rblInterestType.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_cart_payment, "1"));
                    rblPaperSizeOption.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_normal, "1"));
                    rblPaperSizeOption.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_small, "0"));


                    rblCusType.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_Retail, "0"));
                    var bType = commonFunction.findSettingItemValueDataTable("businessType");
                    if (commonFunction.findSettingItemValueDataTable("businessType").Trim() == "electronics")
                        rblCusType.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_wholesale_btype_elec, "1"));
                    else
                        rblCusType.Items.Insert(0, new ListItem(Resources.Language.Lbl_invoice_wholesale, "1"));


                var interstType = commonFunction.findSettingItemValueDataTable("displayInstalment");
                rblInterestType.SelectedValue = interstType;
                //rblPaperSizeOption   smallPrintPaperWidth

                var paperSize = commonFunction.findSettingItemValueDataTable("SalesPrintPageType");
                rblPaperSizeOption.SelectedValue = paperSize;

                var serachBy = commonFunction.findSettingItemValueDataTable("autoSelectSearchBy");
                rblSearchIn.SelectedValue = serachBy;

                rblCusType.SelectedValue = "0";


                Array mobileBanks = Enum.GetValues(typeof(MobileBanks));
                foreach (MobileBanks mobileBank in mobileBanks)
                {
                    ddlMobileBankType.Items.Add(new ListItem(mobileBank.ToString(), ((int)mobileBank).ToString()));
                }


            }

        }


        public enum MobileBanks
        {
            Select = 0,
            bKash = 1,
            Roket = 2,
            Nogod = 3,
            Mcash = 4
        }


        //public enum EMI
        //{
        //    [Description("Select")]
        //    zero,
        //    [Description("3 Months")]
        //    frist,
        //    [Description("6 Months")]
        //    second,
        //    [Description("9 Months")]
        //    third,
        //    [Description("12 Months")]
        //    fourth,
        //    [Description("18 Months")]
        //    fiveth,
        //}


        //public static string GetEnumDescription(Enum value)
        //{
        //    FieldInfo fi = value.GetType().GetField(value.ToString());

        //    DescriptionAttribute[] attributes =
        //        (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        //    if (attributes != null && attributes.Length > 0)
        //        return attributes[0].Description;
        //    else
        //        return value.ToString();

        //}






        [WebMethod]
        public static string[] SetAttribute(string attributeValue)
        {
            var arrValue = new string[100];
            arrValue[0] = attributeValue;
            return arrValue;
        }





        [WebMethod]
        public static string[] getSearchProductListAction(string search, string itemType)
        {
            var saleSearch = new SaleSearch();
            return saleSearch.getSearchProductList(search, itemType);
        }





        [WebMethod]
        public static string getStockDataListAction(string prodCode)
        {
            var saleProduct = new SaleProduct();
            return saleProduct.getStockDataList(prodCode);
        }



        [WebMethod]
        public static string getSaleDataDataListAction(string billNo, string prodCode)
        {
            var saleStock = new SaleStock();
            var saleProduct = new SaleProduct();
            var saleStockStatus = new SaleStockStatus();
            var saleProductStock = new SaleProduct();
            var commonFunction = new CommonFunction();
            var salePackage = new SalePackage();
            var reminderCustomer = new InstallmentCustomer();

            // Find product Id
            var prodId = saleStock.getProductIDbyProdCode(prodCode);
            if (prodId == "")
                return "";

            var stockStatusData = "";
            var saleData = "";
            var reminderData = "";
            if (billNo != "0")
            {
                //SaleInfo
                saleData = saleProduct.getSaleInfoDataList(billNo, prodId);

                //stockInfo
                stockStatusData = saleStockStatus.getStockStatusDataListJson(billNo, prodId);
            }


            // unitInfo
            var unitData = saleStock.getProductUnitRatio(prodId);

            // load StockInfo or PackageInfo
            var prodCodes = "";
            if (!String.IsNullOrWhiteSpace(stockStatusData))
            {
                var stockStatusObject = commonFunction.deSerializeJsonToObject(stockStatusData);
                if (stockStatusObject != null)
                {
                    prodCodes = stockStatusObject["prodCodes"].ToString();
                }
            }

            var stockData = "";
            if (prodCodes == "")
            {
                // stockInfo
                stockData = saleProductStock.getStockDataList(prodCode);
            }
            else
            {
                // PackageInfo
                stockData = salePackage.getPackageDataList(prodId);
            }

            var dicSelectData = new Dictionary<string, string>();
            dicSelectData.Add("saleData", saleData);
            dicSelectData.Add("stockStatusData", stockStatusData);
            dicSelectData.Add("unitData", unitData);
            dicSelectData.Add("stockData", stockData);
            dicSelectData.Add("reminderData", reminderData);

            var multiSerialize = commonFunction.serializeDictionayToJson(dicSelectData);
            return multiSerialize;
        }





        [WebMethod]
        public static string getPackageDataListAction(string billNo, string prodCode)
        {
            var saleProductStock = new SaleProduct();
            return saleProductStock.getPackageDataList(billNo, prodCode);
        }





        [WebMethod]
        public static string getProductDataAction(List<string> productIdList)
        {
            var saleProduct = new SaleProduct();
            var commonFunction = new CommonFunction();

            string prodId = "", billNo = "";
            var dicItemList = new Dictionary<string, string>();
            for (int i = 0; i < productIdList.Count - 1; i++)
            {
                prodId = productIdList[i];
                billNo = productIdList[productIdList.Count - 1];

                var dataListSoldItem = "";
                if (billNo != "" && prodId != "")
                {
                    dataListSoldItem = saleProduct.getProductDataListSoldItem(billNo, prodId);

                    dicItemList.Add(prodId, dataListSoldItem);

                }
                else
                {
                    var dataListNewItem = "";
                    if (prodId != "" && billNo == "")
                    {
                        dataListNewItem = saleProduct.getProductDataListNewItem(prodId);

                        dicItemList.Add(prodId, dataListNewItem);
                    }
                }
            }

            var multiSerialize = commonFunction.serializeDictionayToJson(dicItemList);
            return multiSerialize;
        }




        [WebMethod]
        public static string getProductAccountAction(string billNo, string prodId)
        {
            var saleStock = new SaleStock();
            var saleProduct = new SaleProduct();
            var saleProductStock = new SaleProduct();
            var commonFunction = new CommonFunction();
            var saleStockStatus = new SaleStockStatus();

            if (string.IsNullOrEmpty(prodId))
            {
                return "";
            }

            // saleInfo
            var saleData = "";
            var stockStatusData = "";
            if (billNo != "" && prodId != "")
            {
                saleData = saleProduct.getSaleInfoDataList(billNo, prodId);

                stockStatusData = saleStockStatus.getStockStatusDataListJson(billNo, prodId);
            }

            var unitData = "";
            var stockData = "";
            // unitInfo
            if (prodId != "")
            {
                unitData = saleStock.getProductUnitRatio(prodId);

                stockData = saleProductStock.getStockDataListByProductId(prodId);
            }

            var dicSelectData = new Dictionary<string, string>();
            dicSelectData.Add("saleData", saleData);
            dicSelectData.Add("stockStatusData", stockStatusData);
            dicSelectData.Add("unitData", unitData);
            dicSelectData.Add("stockData", stockData);

            var multiSerialize = commonFunction.serializeDictionayToJson(dicSelectData);

            return multiSerialize;
        }





        [WebMethod]
        public static string getStockStatusDataListAction(string billNo)
        {
            var saleStockStatus = new SaleStockStatus();
            return saleStockStatus.getStockStatusDataListJson(billNo);
        }





        [WebMethod]
        public static string[] getInvoiceByCustomerOrBill(string prefix, string searchOption)
        {
            var products = new List<string>();
            string query = "";
            using (var conn = new SqlConnection())
            {
                string conString = GlobalVariable.getConnectionStringName();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
                using (var cmd = new SqlCommand())
                {
                    if (searchOption == "customer")
                        query =
                            "SELECT DISTINCT name, phone, cusID FROM CustomerInfo WHERE (name like '%' + @SearchText + '%' OR phone like '%' + @SearchPhone + '%' OR cusID like '%' + @searchCusID + '%'  ) " +
                            HttpContext.Current.Session["userAccessParameters"];
                    else
                        query =
                            "SELECT DISTINCT billNo, cusID FROM SaleInfo WHERE (billNo like '%' + @SearchText + '%') " +
                            HttpContext.Current.Session["userAccessParameters"];


                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Parameters.AddWithValue("@SearchPhone", prefix);
                    cmd.Parameters.AddWithValue("@searchCusID", prefix);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (searchOption == "customer")
                                products.Add(string.Format("{0}, {1}, {2}", sdr["name"], sdr["phone"], sdr["cusID"]));
                        }
                    }
                    conn.Close();
                }
            }
            return products.ToArray();
        }





        [WebMethod]
        public static string saveSaleDataListAction(string jsonStrData)
        {
            var transactionQuery = "";
            try
            {
                var isDraftToInvoice = true;

                var commonFunction = new CommonFunction();
                var saleCustomer = new SaleCustomer();
                var saleService = new SaleService();

                var data = JsonConvert.DeserializeObject<dynamic>(jsonStrData);
                var length = ((Newtonsoft.Json.Linq.JArray)data).Count;
                transactionQuery = "BEGIN TRANSACTION ";

                // BillNo
                var billNo = data[0]["billNo"].ToString();
                if (billNo == "")
                {
                    var saleInvoice = new SaleInvoice();
                    billNo = saleInvoice.getNextBillNo();
                    isDraftToInvoice = false;
                }

                // Customer
                var cusId = data[0]["cusId"].ToString();
                if (cusId == "0")
                {
                    // insert customer
                    cusId = saleCustomer.saveCustomerData(data[0]);
                }

                // Delete draft
                if (isDraftToInvoice)
                    saleService.deleteDraft(billNo);


                for (int i = 0; i < length; i++)
                {

                data[i].billNo = billNo;
                data[i].cusId = cusId;
                var saleData = data[i];
                   

                    var inventoryStock = new Stock();
                    transactionQuery += inventoryStock.saveStockStatusInfoList(saleData);

                    var saleUpsert = new SaleUpsert();
                    transactionQuery += saleUpsert.saveSaleWithSlipData(saleData);

                    if (saleData["opt"] != "draft")
                    {
                        if (commonFunction.findSettingItemValueDataTable("saveMiscCostToExpense") == "1")
                        {
                            var cashReportInfo = new SaleCashReport();
                            transactionQuery += cashReportInfo.saveMiscCostToExpense(saleData);
                        }

                        if (i == 0)
                        {
                            int instalmentNumber = Convert.ToInt32(saleData["instalmentNumber"]);
                            if (instalmentNumber > 0)
                            {
                                var customer = new SaleInstallment();
                                transactionQuery += customer.CreateInstallment(saleData);
                            }
                        }

                        if (saleData["discountType"].ToString() == "point")
                        {
                            var customerPoint = new SalePoint();
                            transactionQuery += customerPoint.saveCustomerPoint(saleData);
                        }

                        // qty management
                        transactionQuery += saleUpsert.updateSaleOrderQty(saleData);

                        string executeCounter = saleData["executeCounter"].ToString();
                        if (executeCounter == "0")
                        {
                            transactionQuery += saleUpsert.createCashReportData(saleData);
                        }

                    }

                }
                transactionQuery += "COMMIT ";

                var sqlOperation = new SqlOperation();
                var message = sqlOperation.executeQueryWithLog(transactionQuery);


                // Send SMS
                if (commonFunction.findSettingItemValueDataTable("sendInvoiceBySms") == "1" || commonFunction.findSettingItemValueDataTable("isSendSmsOwnerNumber") == "1")
                {
                    var salePromotion = new SalePromotion();
                    message += salePromotion.SendInvoiceSms(data[0]);
                }


                // Sale Log
                SaleLog saleLog = new SaleLog();
                var isLogSaved = saleLog.SaveSaleLogData(message, "Invoice");

                return "true|" + billNo + "|" + transactionQuery;
            }
            catch (Exception ex)
            {
                return "false|" + ex + "//" + transactionQuery;
            }
        }


        [WebMethod]
        public static string updateSaleDataListAction(string jsonStrData)
        {
            var transactionQuery = "";
            try
            {

                var saleUpsert = new SaleUpsert();

                transactionQuery = "BEGIN TRANSACTION ";

                var data = JsonConvert.DeserializeObject<dynamic>(jsonStrData);
                var length = ((Newtonsoft.Json.Linq.JArray)data).Count;


                for (int i = 0; i < length; i++)
                {
                    transactionQuery += saleUpsert.updateSaleDataOperation(data[i]);
                }

                transactionQuery += "COMMIT ";


                var sqlOperation = new SqlOperation();
                var message = sqlOperation.executeQueryWithLog(transactionQuery);

                string billNo = data[0]["billNo"].ToString();

                // Sale Log
                SaleLog saleLog = new SaleLog();
                var isLogSaved = saleLog.SaveSaleLogData(message, "Update Invoice");

                if (message.Split('|')[0] == "True")
                    return "True|" + billNo + "|" + message;
                else
                    return message;

            }
            catch (Exception ex)
            {
                return "false|" + ex + "//" + transactionQuery;
            }
        }



        [WebMethod]
        public static string updateDueAdjustmentAction(string jsonStrData)
        {
            var saleUpsert = new SaleUpsert();
            return saleUpsert.updateDueAdjustment(jsonStrData);
        }



        [WebMethod]
        public static bool updateCustomerAdvanceAction(string jsonStrData)
        {
            var saleUpsert = new SaleUpsert();
            return saleUpsert.updateCustomerAdvance(jsonStrData);
        }










        [WebMethod]
        public static string getInvoiceSaleDataListAction(string searchTxt)
        {
            var saleInvoice = new SaleInvoice();

            // For Invoice Print
            saleInvoice.setInvoiceBillNo(searchTxt);

            return saleInvoice.getInvoiceDataListSerialize(searchTxt);
        }





        [WebMethod]
        public static string getItemSaleDataListAction(string billNo)
        {
            var saleInvoice = new SaleInvoice();
            return saleInvoice.getItemSaleDataList(billNo);
        }





        [WebMethod]
        public static string generateBillNoInfoAction()
        {
            var saleInvoice = new SaleInvoice();
            return saleInvoice.genearateBillNoInfo();
        }





        [WebMethod]
        public static string saleSettingOptionAction()
        {
            var saleSetting = new SaleSetting();
            return saleSetting.saleSettingOption();
        }





        [WebMethod]
        public static string getSupplierCommissionAction(string prodCode)
        {
            var saleSupCom = new SaleAccount();
            return saleSupCom.getSupplierCommission(prodCode);
        }





        [WebMethod]
        public static string getProductUnitRatioAction(string prodId)
        {
            var inventoryStock = new Stock();
            return inventoryStock.getProductUnitRatio(prodId);
        }








        [WebMethod]
        public static string getItemStockDataListAction(string prodId)
        {
            var SaleStock = new SaleStock();
            return SaleStock.getItemStockDataList(prodId);
        }





        [WebMethod]
        public static bool suspendInvoiceAction(string billNo, string returnAmt, decimal balance)
        {
            var transactionQuery = "BEGIN TRANSACTION ";

           // string isStatus;

            // Status change stockstatusInfo
            var saleStockStatus = new SaleStockStatus();
            transactionQuery += saleStockStatus.changeStockSatusInfo(billNo);

            // Sync stock
            var saleUpsert = new SaleUpsert();
            transactionQuery += saleUpsert.suspendSaleOrderQty(billNo);

            // Update slipInfo
            var saleSlip = new SaleSlip();
            transactionQuery += saleSlip.UpdateSlipInfo(billNo);

            // Update cashReportInfo
            var saleCashReport = new SaleCashReport();
            transactionQuery += saleCashReport.saleCashReportInfoData(billNo, returnAmt);

            // Update Customer
            var saleCustomer = new SaleCustomer();
            transactionQuery += saleCustomer.saleSuspendCustomerInfo(billNo, returnAmt, balance);

            // Change saleInfo
            var saleInvoice = new SaleInvoice();
            transactionQuery += saleInvoice.changeSaleInfoStatus(billNo);

            // installment
            var installment = new SaleInstallment();
            transactionQuery += installment.suspendInstallment(billNo);

            transactionQuery += "COMMIT ";

            var sqlOperation = new SqlOperation();
            var message = sqlOperation.executeQueryWithLog(transactionQuery);


            // Sale Log
            SaleLog saleLog = new SaleLog();
            var isLogSaved = saleLog.SaveSaleLogData(message, "Suspend");

            if (Convert.ToBoolean(message.Split('|')[0]))
                return true;
            else
                return false;

        }





        [WebMethod]
        public static string getSalePersonAsLogin(string userId)
        {
            return "";
        }




        [WebMethod]
        public static string getCurrentLoginUsernameAction(string roleId)
        {
            SaleRole saleRole = new SaleRole();
            return saleRole.getCurrentLoginUsername(roleId);
        }



        [WebMethod]
        public static string getStockCurrentQtyAction(string prodId)
        {
            var inventoryStock = new Stock();
            return inventoryStock.getStockCurrentQty(prodId);
        }



        [WebMethod]
        public static string getStockMinQtyDataAction(string prodCodes)
        {
            CommonFunction commonFunction = new CommonFunction();
            return commonFunction.getPackageQty(prodCodes);
        }



        [WebMethod]
        public static string getInvoicePrintDataAction(string billNo)
        {
            var salePrint = new SalePrint();
            return salePrint.getInvoicePrintData(billNo);
        }


        [WebMethod]
        public static string getProductImeiListAction(string prodID)
        {
            var storeId = HttpContext.Current.Session["storeId"].ToString();
            var commonFunction = new CommonFunction();
            return commonFunction.getIMEIStoreWise(storeId, prodID);
        }





        [WebMethod]
        public static string getProductDataListAddToCartAction(string prodId)
        {
            var saleStock = new SaleStock();
            return saleStock.getProductDataListAddToCart(prodId);
        }





        [WebMethod]
        public static string getProductIdByProductCodeAction(string productCode)
        {
            var saleStock = new SaleStock();
            return saleStock.getProductIdByProductCodeData(productCode);
        }


        [WebMethod]
        public static string getAttributeNameDataAction(string attributeRecord)
        {
            var saleStock = new SaleStock();
            return saleStock.getAttributeNameData(attributeRecord);
        }





        [WebMethod]
        public static string syncSaleDataListAction(string jsonStrData)
        {
            var commonFunction = new CommonFunction();


            // Varify Stock and Customer
            string jsonDic = jsonStrData.Substring(1, jsonStrData.Length - 2);
            string[] splitDic = jsonDic.Split('}');
            var jsonDataDeserilize = (JObject)JsonConvert.DeserializeObject(splitDic[0] + "}");

            // Check Stock
            var isAvailProdQty = true;
            for (int i = 0; i < splitDic.Length; i++)
            {
                if (splitDic[i].ToString() == "")
                    break;

                var singleProduct = splitDic[i] + '}';
                if (singleProduct.StartsWith(","))
                {
                    singleProduct = singleProduct.Substring(1, singleProduct.Length - 1);
                }

                var jsonProductData = (JObject)JsonConvert.DeserializeObject(singleProduct);
                var prodId = jsonProductData["prodID"].Value<string>();

                var stockModel = new Stock();
                var stockQty = stockModel.getStockQtyByProductId(prodId);
                if (Convert.ToDecimal(stockQty) <= 0)
                    isAvailProdQty = false;
            }

            if (!isAvailProdQty)
            {
                return "False|Sorry, your Stock are not available. ";
            }

            // Check Customer
            var cusId = jsonDataDeserilize["cusId"].Value<string>();
            var customerService = new SaleCustomer();
            customerService.cusId = cusId;
            var existsCustomer = customerService.existsCustomer();
            if (!existsCustomer)
            {
                // Create customer
                var customerModel = new CustomerModel();
                customerModel.name = jsonDataDeserilize["cusName"].ToString();
                customerModel.phone = jsonDataDeserilize["phone"].ToString();
                customerModel.mailInfo = jsonDataDeserilize["email"].ToString();
                customerModel.address = jsonDataDeserilize["address"].ToString();
                customerModel.CusType = jsonDataDeserilize["type"].ToString();
                cusId = customerModel.saveCustomerInfoModel();
            }


            string billNo = "";
            try
            {
                string lastSaleId = commonFunction.getSaleLastID();
                billNo = commonFunction.nextIdGenerator(lastSaleId);
            }
            catch
            {
                billNo = "AA00001";
            }


            var message = "";

            message = saveSaleDataListAction(jsonStrData);

            return message;
        }


        [WebMethod]
        public static string initializeCustomerAction()
        {
            var customer = new SaleCustomer();
            return customer.initializeCustomer();
        }



        [WebMethod]
        public static string initializeConfigAction()
        {
            var saleSetting = new SaleSetting();
            return saleSetting.initializeConfig();
        }



        [WebMethod]
        public static string getProductIDByIMEIAction(string IMEI)
        {
            var saleStockStatus = new SaleStockStatus();
            return saleStockStatus.getProductIDByIMEI(IMEI);
        }


    }


}
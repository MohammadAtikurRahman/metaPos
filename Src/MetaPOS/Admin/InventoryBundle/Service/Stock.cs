using System;
using System.Collections.Generic;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.RecordBundle.View;
using MetaPOS.Admin.SaleBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;



namespace MetaPOS.Admin.InventoryBundle.Service
{


    public class Stock
    {


        private StockModel stockModel = new StockModel();
        private CommonFunction commonFunction = new CommonFunction();
        private StockStatusModel stockStatusModel = new StockStatusModel();





        public string saveStockStatusInfoList(dynamic data)
        {

            var transactionQuery = "";

            stockStatusModel.billNo = data["billNo"].ToString();
            stockStatusModel.isPackage = (bool)data["isPackage"];
            string prodId = data["prodID"].ToString();
            var d = data["entryDate"];
            var entryDate = Convert.ToDateTime(d);
            string prodCodes = data["prodCodes"].ToString();
            string soldQty = data["qty"].ToString();
            string serialNo = data["serialNo"].ToString();
            string removeImei = data["removeImei"].ToString();
            string payType = Convert.ToString(data["payType"]);
            string payDescr = Convert.ToString(data["payDescr"]);
            string invoiceType = data["invoiceType"].ToString();

            if (soldQty == "")
                soldQty = "0";

            if (!soldQty.Contains("."))
                soldQty = soldQty.Trim() + ".0";
            else
                soldQty = soldQty.Trim();

            if (serialNo == "0")
                serialNo = "";

            decimal sPrice = (decimal)data["sPrice"];
            stockStatusModel.cecishNumber = data["cecishNumber"].ToString();
            stockStatusModel.engineNumber = data["engineNumber"].ToString();
            stockStatusModel.offerType = data["discountType"].ToString();
            stockStatusModel.salesPersonId = data["salesPersonId"].ToString();
            stockStatusModel.referredBy = data["referredBy"].ToString();

            var offer = data["offer"].ToString();
            if (string.IsNullOrEmpty(offer))
                offer = "0";

            stockStatusModel.offer = offer;

            if (data["searchType"].ToString() == "product")
            {
                // Product save to StockStatusInfo
                var dtStockInfo = stockModel.getStockDataListModelByProdID(prodId);
                foreach (DataRow row in dtStockInfo.Rows)
                {
                    stockStatusModel.prodId = (Int32)row["prodID"];
                    stockStatusModel.prodCode = row["prodCode"].ToString();
                    stockStatusModel.prodName = row["prodName"].ToString();
                    stockStatusModel.prodDescr = row["prodDescr"].ToString();
                    stockStatusModel.supCompany = row["supCompany"].ToString();
                    stockStatusModel.catName = row["catName"].ToString();
                    stockStatusModel.qty = soldQty;
                    stockStatusModel.serialNo = serialNo;
                    stockStatusModel.payType = payType;
                    stockStatusModel.payDescr = payDescr;
                    stockStatusModel.invoiceType = invoiceType;
                    stockStatusModel.bPrice = (decimal)row["bprice"];
                    stockStatusModel.sPrice = sPrice;
                    stockStatusModel.weight = (decimal)row["weight"];
                    stockStatusModel.stockTotal = (decimal)row["stockTotal"];
                    stockStatusModel.status = data["opt"].ToString();
                    stockStatusModel.entryDate = entryDate;
                    stockStatusModel.statusDate = entryDate;
                    stockStatusModel.entryQty = row["qty"].ToString();
                    stockStatusModel.title = row["title"].ToString();
                    stockStatusModel.roleId = HttpContext.Current.Session["roleId"].ToString();
                    stockStatusModel.branchId = HttpContext.Current.Session["branchId"].ToString();
                    stockStatusModel.groupId = HttpContext.Current.Session["groupId"].ToString();
                    stockStatusModel.fieldAttribute = "";
                    stockStatusModel.tax = row["tax"].ToString();
                    stockStatusModel.sku = row["sku"].ToString();
                    stockStatusModel.productSource = "";
                    stockStatusModel.prodCodes = prodCodes;
                    stockStatusModel.imei = data["imei"].ToString().Replace('×', ',').Replace(" ", "").TrimEnd(',').TrimStart(',');
                    stockStatusModel.fieldId = "";
                    stockStatusModel.attributeId = "";
                    stockStatusModel.supCommission = Convert.ToDecimal(row["commission"]);
                    stockStatusModel.dealerPrice = Convert.ToDecimal(row["dealerPrice"]);
                    stockStatusModel.createdFor = row["createdFor"].ToString();
                    stockStatusModel.unitId = row["unitId"].ToString();
                    stockStatusModel.searchType = data["searchType"].ToString();
                    stockStatusModel.storeId = HttpContext.Current.Session["storeId"].ToString();
                    stockStatusModel.fieldRecord = data["fieldRecord"].ToString();
                    stockStatusModel.attributeRecord = data["attributeRecord"].ToString();

                    var lastQty = commonFunction.getLastStockQty(prodId,
                        HttpContext.Current.Session["storeId"].ToString());
                    if (!lastQty.Contains("."))
                    {
                        lastQty = lastQty + ".0";
                    }

                    var balanceQty = commonFunction.calculateQty(prodId, lastQty, soldQty, "-");
                    if (!balanceQty.Contains("."))
                    {
                        balanceQty = balanceQty + ".0";
                    }

                    stockStatusModel.lastQty = lastQty;
                    stockStatusModel.balanceQty = balanceQty;
                }
            }
            else if (data["searchType"].ToString() == "salePackage")
            {
                // Save Pacakge 
                transactionQuery += savePackage(data);

                PackageModel packageModel = new PackageModel();
                DataTable dtStockInfo = packageModel.getPackageDataListModelByPackId(prodId);

                // get package buy price
                string[] splitProdIDs = prodCodes.Split(';');
                string packProdId = "";
                decimal packBuyPrice = 0;
                for (int i = 0; i < splitProdIDs.Length; i++)
                {
                    if (splitProdIDs[i] == "")
                        break;

                    packProdId = splitProdIDs[i];
                    var dtStock = stockModel.getItemStockDataListModelByProdID(packProdId);
                    if (dtStock.Rows.Count > 0)
                        packBuyPrice += (decimal)dtStock.Rows[0]["bPrice"];
                }

                foreach (DataRow row in dtStockInfo.Rows)
                {
                    stockStatusModel.prodId = Convert.ToInt32(prodId);
                    stockStatusModel.prodCode = prodId;
                    stockStatusModel.prodName = row["packageName"].ToString();
                    stockStatusModel.prodDescr = "";
                    stockStatusModel.supCompany = "0";
                    stockStatusModel.catName = "0";
                    stockStatusModel.qty = soldQty;
                    stockStatusModel.serialNo = serialNo;
                    stockStatusModel.bPrice = packBuyPrice;
                    stockStatusModel.sPrice = sPrice;
                    stockStatusModel.weight = 0;
                    stockStatusModel.stockTotal = 0;
                    stockStatusModel.status = "salePackage";
                    stockStatusModel.entryDate = entryDate;
                    stockStatusModel.statusDate = entryDate;
                    stockStatusModel.entryQty = row["qty"].ToString();
                    stockStatusModel.title = "";
                    stockStatusModel.roleId = HttpContext.Current.Session["roleId"].ToString();
                    stockStatusModel.branchId = HttpContext.Current.Session["branchId"].ToString();
                    stockStatusModel.groupId = HttpContext.Current.Session["groupId"].ToString();
                    stockStatusModel.fieldAttribute = "";
                    stockStatusModel.tax = "";
                    stockStatusModel.sku = "";
                    stockStatusModel.lastQty = "0";
                    stockStatusModel.productSource = "";
                    stockStatusModel.prodCodes = prodCodes;
                    stockStatusModel.imei = "";
                    stockStatusModel.fieldId = "";
                    stockStatusModel.attributeId = "";
                    stockStatusModel.supCommission = 0;
                    stockStatusModel.dealerPrice = 0;
                    stockStatusModel.createdFor = "";
                    stockStatusModel.unitId = "0";
                    stockStatusModel.searchType = data["searchType"].ToString();
                    stockStatusModel.storeId = HttpContext.Current.Session["storeId"].ToString();
                }
            }
            else
            {
                // Save Service 
                var serviceModel = new ServiceModel();
                var dtServiceInfo = serviceModel.getServiceDataListModelByID(prodId);
                foreach (DataRow row in dtServiceInfo.Rows)
                {
                    stockStatusModel.prodId = Convert.ToInt32(row["Id"]);
                    stockStatusModel.prodCode = row["Id"].ToString();
                    stockStatusModel.prodName = row["name"].ToString();
                    stockStatusModel.prodDescr = row["type"].ToString();
                    stockStatusModel.supCompany = "";
                    stockStatusModel.catName = "";
                    stockStatusModel.qty = soldQty;
                    stockStatusModel.serialNo = serialNo;
                    stockStatusModel.bPrice = 0;
                    stockStatusModel.sPrice = sPrice;
                    stockStatusModel.weight = 0;
                    stockStatusModel.stockTotal = 0;
                    stockStatusModel.status = data["opt"].ToString();
                    stockStatusModel.entryDate = entryDate;
                    stockStatusModel.statusDate = entryDate;
                    stockStatusModel.entryQty = "0";
                    stockStatusModel.title = "";
                    stockStatusModel.roleId = HttpContext.Current.Session["roleId"].ToString();
                    stockStatusModel.branchId = HttpContext.Current.Session["branchId"].ToString();
                    stockStatusModel.groupId = HttpContext.Current.Session["groupId"].ToString();
                    stockStatusModel.fieldAttribute = "";
                    stockStatusModel.tax = "";
                    stockStatusModel.sku = "";
                    stockStatusModel.lastQty = "0";
                    stockStatusModel.productSource = "";
                    stockStatusModel.prodCodes = prodCodes;
                    stockStatusModel.imei = "";
                    stockStatusModel.fieldId = "";
                    stockStatusModel.attributeId = "";
                    stockStatusModel.supCommission = 0;
                    stockStatusModel.dealerPrice = Convert.ToDecimal(row["wholePrice"]);
                    stockStatusModel.createdFor = HttpContext.Current.Session["roleId"].ToString();
                    stockStatusModel.unitId = "";
                    stockStatusModel.searchType = data["searchType"].ToString();
                    stockStatusModel.storeId = HttpContext.Current.Session["storeId"].ToString();
                }
            }

            transactionQuery += "BEGIN ";
            transactionQuery += stockStatusModel.saveStockStatusInfoListModel();
            transactionQuery += "END ";

            if (data["discountType"].ToString() == "qty")
            {
                if (Convert.ToDecimal(data["offer"]) > 0)
                {
                    stockStatusModel.qty = data["offer"].ToString();
                    stockStatusModel.isOfferQty = true;

                    transactionQuery += "BEGIN ";
                    transactionQuery += stockStatusModel.saveStockStatusInfoListModel();
                    transactionQuery += "END ";
                }
            }
            return transactionQuery;
        }





        public string savePackage(dynamic data)
        {
            /* 
             *  Package save 
             */
            var transactionQuery = "";
            var prodId = data["prodID"].ToString();
            var prodCodes = data["prodCodes"].ToString();

            // Package Save to StockStatusInfo
            PackageModel packageModel = new PackageModel();
            DataTable dtStockInfo = packageModel.getPackageDataListModelByPackId(prodId);


            // get package buy price
            string[] splitProdIDs = prodCodes.Split(';');

            for (int i = 0; i < splitProdIDs.Length; i++)
            {
                if (splitProdIDs[i] == "")
                    break;

                string packProdId = splitProdIDs[i];
                var dtStock = stockModel.getItemStockDataListModelByProdID(packProdId);

                foreach (DataRow row in dtStockInfo.Rows)
                {
                    var opt = data["opt"].ToString();
                    if (opt == "save")
                    {

                        var qty = data["qty"].ToString();
                        if (!qty.Contains("."))
                        {
                            qty = qty + ".0";
                        }

                        stockStatusModel.prodId = Convert.ToInt32(packProdId);
                        stockStatusModel.prodCode = prodId;
                        stockStatusModel.prodName = dtStock.Rows[0]["prodName"].ToString();
                        stockStatusModel.billNo = data["billNo"].ToString();
                        stockStatusModel.prodDescr = "";
                        stockStatusModel.supCompany = dtStock.Rows[0]["supCompany"].ToString();
                        stockStatusModel.catName = dtStock.Rows[0]["catName"].ToString();
                        stockStatusModel.bPrice = Convert.ToDecimal(dtStock.Rows[0]["bPrice"].ToString());
                        stockStatusModel.sPrice = Convert.ToDecimal(dtStock.Rows[0]["sPrice"].ToString());
                        stockStatusModel.weight = 0;
                        stockStatusModel.stockTotal = 0;
                        stockStatusModel.entryDate = Convert.ToDateTime(data["entryDate"].ToString());
                        stockStatusModel.statusDate = commonFunction.GetCurrentTime();
                        stockStatusModel.entryQty = row["qty"].ToString();
                        stockStatusModel.title = "";
                        stockStatusModel.roleId = HttpContext.Current.Session["roleId"].ToString();
                        stockStatusModel.branchId = HttpContext.Current.Session["branchId"].ToString();
                        stockStatusModel.groupId = HttpContext.Current.Session["groupId"].ToString();
                        stockStatusModel.fieldAttribute = "";
                        stockStatusModel.tax = "";
                        stockStatusModel.sku = "";
                        stockStatusModel.lastQty = "0";
                        stockStatusModel.productSource = "";
                        stockStatusModel.prodCodes = "";
                        stockStatusModel.imei = "";
                        stockStatusModel.fieldId = "";
                        stockStatusModel.attributeId = "";
                        stockStatusModel.supCommission = 0;
                        stockStatusModel.dealerPrice = 0;
                        stockStatusModel.createdFor = "";
                        stockStatusModel.unitId = "0";
                        stockStatusModel.searchType = "product";
                        stockStatusModel.storeId = HttpContext.Current.Session["storeId"].ToString();
                        stockStatusModel.isPackage = true;
                        stockStatusModel.status = "sale";
                        stockStatusModel.qty = qty;
                        stockStatusModel.offer = "0";

                        transactionQuery += "BEGIN ";
                        transactionQuery += stockStatusModel.saveStockStatusInfoListModel();
                        transactionQuery += "END ";

                    }
                    else
                    {
                        var billNo = data["billNo"].ToString();
                        string removeImei = data["removeImei"].ToString();
                        var packReturnQty = Convert.ToDecimal(data["returnQty"].ToString() == "" ? "0" : data["returnQty"].ToString());

                        decimal packReturnQtyDb = 0;
                        var dtPackReturnQty = stockStatusModel.getStockStatusSaleReturnSumOfQtyModel(billNo, Convert.ToInt32(packProdId));
                        if (dtPackReturnQty.Rows.Count > 0 && dtPackReturnQty.Rows[0][0].ToString() != "")
                            packReturnQtyDb = Convert.ToDecimal(dtPackReturnQty.Rows[0][0].ToString());

                        if (packReturnQty > packReturnQtyDb)
                        {
                            var statusSaleReturn = "saleReturn";
                            var searchType = data["searchType"].ToString();
                            stockStatusModel.returnImei = removeImei;
                            var qtySaleReturn = packReturnQty - packReturnQtyDb;

                            transactionQuery += "BEGIN ";
                            transactionQuery += stockStatusModel.saveStockStatusInfoListForSaleReturn(billNo, Convert.ToInt32(packProdId), qtySaleReturn.ToString(), statusSaleReturn, searchType);
                            transactionQuery += "END ";
                        }

                    }

                }
            }

            return transactionQuery;
        }






        public string calculateSaleQty(string prodId, string inputTotalQty, string dbSoldTotalQty, string saleReturnQty)
        {
            int dbRatio = 1;
            var dtStock = stockModel.getProductRatioModelByProductId(prodId);
            if (dtStock.Rows.Count > 0)
                dbRatio = Convert.ToInt32(dtStock.Rows[0]["unitRatio"]);
            else
                dbRatio = 1;

            dtStock = stockModel.getStockDataListModelByProdID(prodId);

            // stock Qty and Pieces split
            string dbStockTotalQty = dtStock.Rows[0]["qty"].ToString();
            bool containsStockQty = dbStockTotalQty.Contains(".");
            int dbStockQty = 0, dbStockPiece = 0;
            if (containsStockQty)
            {
                string[] splitDbQty = dbStockTotalQty.Split('.');
                if (splitDbQty.Length > 1)
                {
                    dbStockQty = Convert.ToInt32(splitDbQty[0]);
                    dbStockPiece = Convert.ToInt32(splitDbQty[1]);
                }
            }
            else
            {
                dbStockQty = Convert.ToInt32(dbStockTotalQty);
            }

            // Sold qty and price
            bool containsSoldQty = dbSoldTotalQty.Contains(".");
            int dbSoldQty = 0, dbSoldPiece = 0;
            if (containsSoldQty)
            {
                string[] splitDbSoldQty = dbSoldTotalQty.Split('.');
                if (splitDbSoldQty.Length > 1)
                {
                    dbSoldQty = Convert.ToInt32(splitDbSoldQty[0]);
                    dbSoldPiece = Convert.ToInt32(splitDbSoldQty[1]);
                }
            }
            else
            {
                dbSoldQty = Convert.ToInt32(dbSoldTotalQty);
            }
            int totalQty = dbStockQty + dbSoldQty;
            int totalPiece = dbStockPiece + dbSoldPiece;

            // Input qty and piece split
            int inputQty = 0, inputPiece = 0;
            bool containsInput = inputTotalQty.Contains(".");
            if (containsInput)
            {
                string[] splitQty = inputTotalQty.Split('.');
                if (splitQty.Length > 1)
                {
                    inputQty = Convert.ToInt32(splitQty[0]);
                    inputPiece = Convert.ToInt32(splitQty[1]);
                }
            }
            else
            {
                inputQty = Convert.ToInt32(inputTotalQty);
            }

            // Check total piece and ratio
            if (totalPiece > dbRatio)
            {
                totalPiece = totalPiece - dbRatio;
                totalQty = totalQty + 1;
            }


            // Check input and total piece
            if (inputPiece > totalPiece)
            {
                totalQty -= 1;
                totalPiece = (dbRatio + totalPiece) - inputPiece;
            }
            else
            {
                totalPiece = totalPiece - inputPiece;
            }

            int returnQty = 0, returnPiece = 0;
            bool containsReturnQtyFloat = saleReturnQty.Contains(".");
            if (containsReturnQtyFloat)
            {
                string[] splitQty = inputTotalQty.Split('.');
                if (splitQty.Length > 1)
                {
                    returnQty = Convert.ToInt32(splitQty[0]);
                    returnPiece = Convert.ToInt32(splitQty[1]);
                }
            }
            else
            {
                returnQty = Convert.ToInt32(saleReturnQty);
                returnPiece = 0;
            }

            totalQty = totalQty - inputQty + returnQty;
            totalPiece += returnPiece;

            if (dbRatio > 1)
                return totalQty + "." + totalPiece;
            else
                return totalQty.ToString();
        }





        public bool updateStockStatusInfoList(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            stockStatusModel.billNo = data["billNo"].Value<string>();
            var prodId = data["prodID"].Value<string>();
            var entryDate = data["entryDate"].Value<DateTime>();
            var dtStockInfo = stockModel.getStockDataListModelByProdID(prodId);

            foreach (DataRow row in dtStockInfo.Rows)
            {
                stockStatusModel.prodId = Convert.ToInt32(row["prodID"]);
                stockStatusModel.prodCode = row["prodCode"].ToString();
                stockStatusModel.prodName = row["prodName"].ToString();
                stockStatusModel.prodDescr = row["prodDescr"].ToString();
                stockStatusModel.supCompany = row["supCompany"].ToString();
                stockStatusModel.catName = row["catName"].ToString();
                stockStatusModel.qty = data["qty"].Value<string>();
                stockStatusModel.bPrice = Convert.ToDecimal(row["bprice"]);

                stockStatusModel.sPrice = data["sPrice"].Value<decimal>();

                stockStatusModel.weight = Convert.ToInt32(row["weight"]);
                stockStatusModel.stockTotal = Convert.ToDecimal(row["stockTotal"]);
                stockStatusModel.status = "1";
                stockStatusModel.entryDate = entryDate;
                stockStatusModel.statusDate = commonFunction.GetCurrentTime();
                stockStatusModel.entryQty = row["qty"].ToString();
                stockStatusModel.title = row["title"].ToString();
                stockStatusModel.roleId = HttpContext.Current.Session["roleId"].ToString();
                stockStatusModel.branchId = HttpContext.Current.Session["branchId"].ToString();
                stockStatusModel.groupId = HttpContext.Current.Session["groupId"].ToString();
                stockStatusModel.fieldAttribute = "";
                stockStatusModel.tax = row["tax"].ToString();
                stockStatusModel.sku = row["sku"].ToString();
                stockStatusModel.lastQty = row["lastQty"].ToString();
                stockStatusModel.productSource = "";
                stockStatusModel.prodCodes = "";
                stockStatusModel.imei = row["imei"].ToString();
                stockStatusModel.fieldId = "";
                stockStatusModel.attributeId = "";
                stockStatusModel.supCommission = Convert.ToDecimal(row["commission"]);
                stockStatusModel.dealerPrice = Convert.ToDecimal(row["dealerPrice"]);
                stockStatusModel.createdFor = row["createdFor"].ToString();
            }

            return stockStatusModel.updateStockStatusInfoListModel();
        }





        public string getProductUnitRatio(string prodId)
        {
            var stockModel = new StockModel();
            return stockModel.getProductUnitRatioSerializeDataModel(prodId);
        }



        public bool isPurchaseCodeExist(string purchaseCode)
        {
            return stockModel.isPurchaseCodeExistModel(purchaseCode);
        }



        public string getStockCurrentQty(string prodId)
        {
            DataTable dt = stockModel.getItemStockDataListModelByProdID(prodId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["qty"].ToString();
            else
                return "0";
        }





        public int getUnitRatioByProductID(string prodId)
        {
            var dtRatio = stockModel.getProductRatioModelByProductId(prodId);
            if (dtRatio.Rows.Count > 0)
                return Convert.ToInt32(dtRatio.Rows[0]["unitRatio"]);
            else
                return 1;
        }





        public string getProductIDbyProdCode(string prodCode)
        {
            var dtStock = stockModel.getProductDataByProductCode(prodCode);

            string prodId = "";
            if (dtStock.Rows.Count > 0)
            {
                prodId = dtStock.Rows[0]["prodId"].ToString();
            }

            return prodId;
        }


        public string getCurrentTotalStockQty(string inputQty, string prodId)
        {
            int qty = 0, piece = 0;
            bool hasUnitInputQty = inputQty.Contains(".");
            if (hasUnitInputQty)
            {
                string[] splitQty = inputQty.Split('.');
                if (splitQty.Length > 1)
                {
                    qty = Convert.ToInt32(splitQty[0]);
                    piece = Convert.ToInt32(splitQty[1]);
                }
            }
            else
            {
                qty = Convert.ToInt32(inputQty);
            }

            // database stock qty
            string dbStockQty = getStockCurrentQty(prodId);

            int dbQty = 0, dbPiece = 0;
            bool hasUnitDbQty = dbStockQty.Contains(".");
            if (hasUnitDbQty)
            {
                string[] splitQty = dbStockQty.Split('.');
                if (splitQty.Length > 1)
                {
                    dbQty = Convert.ToInt32(splitQty[0]);
                    dbPiece = Convert.ToInt32(splitQty[1]);
                }
            }
            else
            {
                dbQty = Convert.ToInt32(dbStockQty);
            }


            // check with unit 
            var unitRatio = getUnitRatioByProductID(prodId);
            if (String.IsNullOrEmpty(unitRatio.ToString()))
            {
                unitRatio = 1;
            }

            int totalQty = qty + dbQty;
            int totalPiece = piece + dbPiece;


            if (totalPiece >= unitRatio)
            {
                totalQty += 1;
                totalPiece -= unitRatio;
            }

            string currentStockQty = totalQty + "." + totalPiece;

            return currentStockQty;
        }



        public decimal getStockTotalPrice(string prodId, decimal CostPrice, string inputQty)
        {
            int qty = 0, piece = 0;
            bool hasUnitInputQty = inputQty.Contains(".");
            if (hasUnitInputQty)
            {
                string[] splitQty = inputQty.Split('.');
                if (splitQty.Length > 1)
                {
                    qty = Convert.ToInt32(splitQty[0]);
                    piece = Convert.ToInt32(splitQty[1]);
                }
            }
            else
            {
                qty = Convert.ToInt32(inputQty);
            }

            decimal totalQtyPrice = qty * CostPrice;

            // check with unit 
            var unitRatio = getUnitRatioByProductID(prodId);
            if (String.IsNullOrEmpty(unitRatio.ToString()))
            {
                unitRatio = 1;
            }

            decimal perItemCost = CostPrice / unitRatio;

            decimal totalPiecePrice = piece * perItemCost;

            return totalQtyPrice + totalPiecePrice;

        }




        public string CheckTransferProductOrQty(string TransIdTo, string TransProdId, string transQty)
        {
            DataTable dtStockProduct = stockModel.CheckTransferProductModel(TransIdTo, TransProdId);
            if (dtStockProduct.Rows.Count <= 0)
            {
                return "noproduct";
            }

            DataTable dtStockQty = stockModel.CheckTransferQtyModel(TransIdTo, TransProdId, transQty);

            decimal stockQty = 0;
            if (dtStockQty.Rows.Count > 0)
            {
                stockQty =
                    Convert.ToDecimal(dtStockQty.Rows[0]["qty"].ToString() == ""
                        ? "0"
                        : dtStockQty.Rows[0]["qty"].ToString());

            }

            if (stockQty >= Convert.ToDecimal(transQty))
                return "success";
            else
                return stockQty + " " + transQty + "" + TransIdTo;

            //return "";

        }

        public bool updateTransferStockQty(string TransIdTo, string TransIdForm, string transQty, string shiftTo)
        {
            stockModel.updateOriginalStockQty(TransIdForm, transQty, shiftTo);
            return stockModel.updateTransferStockQty(TransIdTo, transQty, shiftTo);

        }

        public bool insertTransferStockStatusQty(string TransIdTo, string TransIdForm, string transQty, string shiftTo)
        {
            bool isSave = false;
            isSave = stockStatusModel.saveStockStatusInfoTransferModel(TransIdForm, transQty, shiftTo);
            if (isSave)
                isSave = stockStatusModel.saveStockStatusInfoTransReceivedModel(TransIdTo, transQty, shiftTo);

            return isSave;
        }



        public string getStoreWiseQtyByStoreID(string prodCode, string storeId)
        {
            var stockModel = new StockModel();
            var dtStock = stockModel.getStoreWiseQtyStoreIdModel(prodCode, storeId);
            if (dtStock.Rows.Count > 0)
                return dtStock.Rows[0]["stockQty"].ToString();
            else
                return "0";
        }




        public string getStockQtyByUnitService(string prodId, string storeId)
        {
            var stockQty = "0";
            var stockModel = new StockModel();
            stockModel.prodId = Convert.ToInt32(prodId);
            stockModel.storeId = Convert.ToInt32(storeId);
            var dtStock = stockModel.getStockQtyManagement();
            if (dtStock.Rows.Count > 0)
            {
                stockQty = dtStock.Rows[0]["stockQty"].ToString();
            }
            return stockQty;
        }



        private int getTotalQtyOrPiece(string totalQty, string status)
        {
            var total = 0;
            if (status == "0")
            {
                // qty
                if (totalQty.Contains("."))
                    total = Convert.ToInt32(totalQty.Split('.')[0]);
                else
                    total = Convert.ToInt32(totalQty);
            }
            else
            {
                // piece
                if (totalQty.Contains("."))
                    total = Convert.ToInt32(totalQty.Split('.')[1]);
            }

            return total;
        }





        public string getQtyByUnitValue(string status, string prodCode, string storeId)
        {
            var dtQtyUnit = stockModel.getStcokQtyByUnitModel(status, prodCode, storeId);

            int totalQty = 0, totalPiece = 0;
            for (int i = 0; i < dtQtyUnit.Rows.Count; i++)
            {
                var qtydb = dtQtyUnit.Rows[i][0].ToString();
                if (qtydb.Contains("."))
                {
                    totalQty += Convert.ToInt32(qtydb.Split('.')[0]);
                    totalPiece += Convert.ToInt32(qtydb.Split('.')[1]);
                }
                else
                {
                    totalQty += Convert.ToInt32(dtQtyUnit.Rows[i][0].ToString());
                }
            }

            return totalQty + "." + totalPiece;
        }


        public string getAttributeNameData(string attributeRecord)
        {
            var attribute = new VariantModel();
            var attrName = "";
            if (attributeRecord.Contains(","))
            {
                var splitAttr = attributeRecord.Split(',');
                for (int i = 0; i < splitAttr.Length; i++)
                {
                    var dtAttr = attribute.getAttributeNameModel(splitAttr[i]);
                    if (dtAttr.Rows.Count > 0)
                    {

                        attrName += " - ";

                        attrName += dtAttr.Rows[0]["attributeName"].ToString();
                    }
                }
            }
            else
            {
                var dtAttr = attribute.getAttributeNameModel(attributeRecord);
                if (dtAttr.Rows.Count > 0)
                {
                    attrName += dtAttr.Rows[0]["attributeName"].ToString();
                }
            }

            return attrName;
        }



        public bool getProductData(string prodID)
        {
            var dtStock = stockModel.getProductDataByParentProductId(prodID);
            if (dtStock.Rows.Count > 1)
                return true;
            else
                return false;
        }

        public string getProductIdBySearchProduct(string searchProduct)
        {
            string finalText = searchProduct;
            if (searchProduct.Contains("["))
            {
                var splitText = new string[] { };

                int i = 0;
                while ((i = searchProduct.IndexOf('[', i)) != -1)
                {
                    string codeWithBrakets = searchProduct.Substring(i).Trim();
                    finalText = codeWithBrakets.Substring(1, codeWithBrakets.Length - 2);
                    finalText = finalText.Trim();
                    i++;
                }
            }
            else
            {
                var stock = new Stock();
                finalText = stock.getProductIDbyProdCode(searchProduct);

            }

            return finalText;
        }

        public string getStockStatusDataListByProductId(string prodId)
        {
            var stockModel = new StockModel();
            var dtStock = stockModel.getStockStatusDataListByProductIdModel(prodId);
            if (dtStock.Rows.Count > 0)
            {
                return dtStock.Rows[0]["stockQty"].ToString();
            }
            else
            {
                return "0";
            }
        }





        public string getSupplierCommissionFormStock(string productId)
        {
            var dtSupCommission = stockModel.getItemStockDataListModelByProdID(productId);
            if (dtSupCommission.Rows.Count > 0)
            {
                return dtSupCommission.Rows[0]["commission"].ToString();
            }
            return "0";
        }

        public string getStockQtyByProductId(string prodId)
        {
            var stockModel = new StockModel();
            var dtStock = stockModel.getStockQtyModel(prodId);
            if (dtStock.Rows.Count > 0)
                return dtStock.Rows[0]["stockQty"].ToString();
            else
                return "0";
        }
    }


}
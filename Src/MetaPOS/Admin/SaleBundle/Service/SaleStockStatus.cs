using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleStockStatus
    {

        private CommonFunction commonFunction = new CommonFunction();
        StockStatusModel stockStatusModel = new StockStatusModel();
        public string changeStockSatusInfo(string billNo)
        {
            var transactionQuery = "";

            var stockStatusModel = new StockStatusModel();
            var saleModel = new SaleModel();
            var saleStock = new SaleStock();

            var prodId = "";
            var soldQty = "0";
            //var stockQty = "0";
            var prodCodes = "";
            var returnQty = "0";
            //var imei = "";

            var dtStockStatus = stockStatusModel.getStockStatusDataListModel(billNo);
            foreach (DataRow row in dtStockStatus.Rows)
            {
                prodId = row["prodId"].ToString();
                prodCodes = row["prodCodes"].ToString();

                if (prodCodes == "")
                {
                    // sold Qty
                    var dtSale = saleModel.getSaleInfoDataListModel(prodId, billNo);
                    if (dtSale.Rows.Count > 0)
                    {
                        soldQty = dtSale.Rows[0]["qty"].ToString();
                        returnQty = dtSale.Rows[0]["returnQty"].ToString();
                        stockStatusModel.returnImei = dtSale.Rows[0]["imei"].ToString();
                    }


                    string suspendTotalQty = getSuspendQty(prodId, soldQty, returnQty);


                    var lastQty = commonFunction.getLastStockQty(prodId, HttpContext.Current.Session["storeId"].ToString());
                    stockStatusModel.balanceQty = commonFunction.calculateQty(prodId, lastQty, suspendTotalQty, "+");

                    transactionQuery += "BEGIN ";
                    transactionQuery += stockStatusModel.saveStockStatusInfoListForSaleReturnQuery(billNo, Convert.ToInt32(prodId), suspendTotalQty, "saleReturn", "product");
                    transactionQuery += "END ";

                    if (commonFunction.findSettingItemValueDataTable("offer") == "1")
                    {
                        var dtOfferQty = stockStatusModel.getStockStatusQtyOfferDataListModel(prodId, billNo);

                        if (dtOfferQty.Rows.Count > 0)
                        {
                            var suspendOfferValue = dtOfferQty.Rows[0]["offer"].ToString();
                            if (dtOfferQty.Rows[0]["offerType"].ToString() == "qty")
                            {
                                transactionQuery += "BEGIN ";
                                transactionQuery += stockStatusModel.saveStockStatusInfoListForSaleReturnQuery(billNo, Convert.ToInt32(prodId), suspendOfferValue, "saleReturn", "product");
                                transactionQuery += "END ";
                            }

                        }

                        // Point 
                        var dtOfferPoint = stockStatusModel.getStockStatusPointOfferDataListModel(prodId, billNo);
                        if (dtOfferPoint.Rows.Count > 0)
                        {
                            var pointOfferValue = dtOfferPoint.Rows[0]["offer"].ToString();
                            if (dtOfferPoint.Rows[0]["offerType"].ToString() == "point")
                            {
                                var salePoint = new SalePoint();
                                int cusId = Convert.ToInt32(dtSale.Rows[0]["cusId"].ToString());

                                transactionQuery += "BEGIN ";
                                transactionQuery += salePoint.suspendCustomerPoint(cusId, Convert.ToDecimal(pointOfferValue));
                                transactionQuery += "END ";
                            }
                        }
                    }


                }
                else
                {
                    string[] splitText = new string[] { };
                    splitText = prodCodes.Split(';');
                    int arrayCount = splitText.Length - 1;


                    for (int j = 0; j < arrayCount; j++)
                    {
                        // sold Qty
                        saleModel.prodID = prodId;
                        saleModel.billNo = billNo;
                        var dtSalePackageReturn = saleModel.getSalePackageReturnModel();
                        if (dtSalePackageReturn.Rows.Count > 0)
                        {
                            soldQty = dtSalePackageReturn.Rows[0]["returnQty"].ToString();
                        }

                        saleModel.prodID = prodId;
                        saleModel.billNo = billNo;
                        var dtSale = saleModel.getSalePackageQtyModel();
                        if (dtSale.Rows.Count > 0)
                        {
                            soldQty = dtSale.Rows[0]["qty"].ToString();
                        }

                       

                        string suspendReturnQty = getSuspendQty(prodId, soldQty, returnQty);

                        if (!suspendReturnQty.Contains("."))
                            suspendReturnQty = suspendReturnQty + ".0";
                        var prodIdPack = splitText[j];

                        transactionQuery += "BEGIN ";
                        transactionQuery += stockStatusModel.saveStockStatusInfoListForSaleReturnQuery(billNo, Convert.ToInt32(prodIdPack), suspendReturnQty, "saleReturn", "salePackage");
                        transactionQuery += "END ";
                    }
                }
            }

            // Update suspend status.
            transactionQuery += "BEGIN ";
            transactionQuery += stockStatusModel.changeSuspendStatus(billNo);
            transactionQuery += "END ";

            return transactionQuery;
        }





        public string getSuspendQty(string prodId, string soldQty, string returnQty)
        {
            if (!returnQty.Contains("."))
            {
                returnQty = returnQty + ".0";
            }

            if (!soldQty.Contains("."))
            {
                soldQty = soldQty + ".0";
            }

            var soldQtyOnly = soldQty.Split('.')[0];
            var soldPieceOnly = soldQty.Split('.')[1];

            var returnQtyOnly = returnQty.Split('.')[0];
            var returnPieceOnly = returnQty.Split('.')[1];

            var suspendReturnQty = Convert.ToDecimal(soldQtyOnly) - Convert.ToDecimal(returnQtyOnly);

            var totalSuspendPiece = 0;
            if (Convert.ToInt32(soldPieceOnly) < Convert.ToInt32(returnPieceOnly))
            {
                var ratio = commonFunction.getRatioByProductId(prodId);
                suspendReturnQty -= 1;
                totalSuspendPiece = (ratio + Convert.ToInt32(soldPieceOnly)) - Convert.ToInt32(returnPieceOnly);

            }
            else
            {
                totalSuspendPiece = Convert.ToInt32(soldPieceOnly) - Convert.ToInt32(returnPieceOnly);
            }
            string suspendTotalQty = suspendReturnQty + "." + totalSuspendPiece;

            return suspendTotalQty;
        }




        public string getStockStatusDataListJson(string billNo, string prodId)
        {
            var commonFunction = new CommonFunction();
            var stockModel = new StockModel();
            var dtItemList = stockModel.getStockStatusDataListModel(billNo, prodId);

            //return commonFunction.serializeDatatableToJson(dtItemList);

            var rows = new List<Dictionary<string, string>>();
            Dictionary<string, string> items;

            foreach (DataRow row in dtItemList.Rows)
            {
                items = new Dictionary<string, string>();
                foreach (DataColumn col in dtItemList.Columns)
                {
                    items.Add(col.ColumnName, row[col].ToString());
                }
                rows.Add(items);
            }

            var js = new JavaScriptSerializer();
            return js.Serialize(rows);
        }

        public string getStockStatusDataListJson(string billNo)
        {
            var commonFunction = new CommonFunction();
            var stockModel = new StockModel();
            DataTable dtItemList = stockModel.getStockStatusDataListModel(billNo);

            //return commonFunction.serializeDatatableToJson(dtItemList);

            var rows = new List<Dictionary<string, string>>();
            Dictionary<string, string> items;

            foreach (DataRow row in dtItemList.Rows)
            {
                items = new Dictionary<string, string>();
                foreach (DataColumn col in dtItemList.Columns)
                {
                    items.Add(col.ColumnName, row[col].ToString());
                }
                rows.Add(items);
            }

            var js = new JavaScriptSerializer();
            return js.Serialize(rows);
        }





        public DataTable getStockStatusDataList(string billNo)
        {
            var stockModel = new StockModel();
            return stockModel.getStockStatusDataListModel(billNo);

        }



        public string getProductIDByIMEI(string IMEI)
        {
            var prodId = "";
            var stockStatusModel = new StockStatusModel();
            var  dtStockStatus = stockStatusModel.getProductIDByIMEI(IMEI);
            if (dtStockStatus.Rows.Count > 0)
            {
                prodId = dtStockStatus.Rows[0]["prodID"].ToString();

            }
            //var storeId = HttpContext.Current.Session["storeId"].ToString();
            //var IMEIList = commonFunction.getIMEIStoreWise(storeId);
            return prodId;
        }
    }
}
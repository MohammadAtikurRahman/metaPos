using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SaleStock
    {

        CommonFunction commonFunction = new CommonFunction();
        StockStatusModel stockStatusModel = new StockStatusModel();
        StockModel stockModel = new StockModel();

        public string getItemStockDataList(string prodId)
        {
            var stockModel = new StockModel();
            return stockModel.getItemStockDataListSerializeModel(prodId);
        }



        //public string updateStockQty(string billNo)
        //{
        //    var transactionQuery = "";

        //    var saleModel = new SaleModel();
        //    var prodId = "";
        //    var soldQty = "0";
        //    var stockQty = "0";
        //    var prodCodes = "";
        //    var returnQty = "0";
        //    var imei = "";

        //    var dtStockStatus = stockStatusModel.getStockStatusDataListModel(billNo);
        //    foreach (DataRow row in dtStockStatus.Rows)
        //    {
        //        prodId = row["prodId"].ToString();
        //        prodCodes = row["prodCodes"].ToString();

        //        if (prodCodes == "")
        //        {
        //            // sold Qty
        //            var dtSale = saleModel.getSaleInfoDataListModel(prodId, billNo);
        //            if (dtSale.Rows.Count > 0)
        //            {
        //                soldQty = dtSale.Rows[0]["qty"].ToString();
        //                returnQty = dtSale.Rows[0]["returnQty"].ToString();
        //            }

        //            var currentQty = commonFunction.calculateQty(prodId, "0", soldQty, "+", returnQty);
        //            transactionQuery += "BEGIN ";
        //            transactionQuery += stockModel.updateStockQtyModelForSave(prodId, currentQty);
        //            transactionQuery += "END ";
        //        }
        //        else
        //        {
        //            string[] splitText = new string[] { };
        //            splitText = prodCodes.Split(';');
        //            int arrayCount = splitText.Length - 1;


        //            for (int j = 0; j < arrayCount; j++)
        //            {


        //                // sold Qty
        //                var dtSale = saleModel.getSaleInfoDataListModel(prodId, billNo);
        //                if (dtSale.Rows.Count > 0)
        //                {
        //                    soldQty = dtSale.Rows[0]["qty"].ToString();
        //                    returnQty = dtSale.Rows[0]["returnQty"].ToString();
        //                }




        //                var productCode = splitText[j];
        //                prodId = getProductIDbyProdCode(productCode);

        //                var currentQty = commonFunction.calculateQty(prodId, "0", soldQty, "+", returnQty);

        //                transactionQuery += "BEGIN ";
        //                transactionQuery += stockModel.updateStockQtyModelForSave(prodId, currentQty);
        //                transactionQuery += "END ";
        //            }
        //        }

        //        // IMEI 
        //        if (commonFunction.findSettingItemValueDataTable("imei") == "1")
        //        {
        //            imei = row["imei"].ToString();
        //            var dbImei = "";
        //            var dtStockImei = stockModel.getStockDataListModelByProdID(prodId);
        //            if (dtStockImei.Rows.Count > 0)
        //            {
        //                dbImei = dtStockImei.Rows[0]["imei"].ToString();
        //            }

        //            if (dbImei != "")
        //            {
        //                imei = imei.TrimEnd(',') + "," + dbImei;
        //            }

        //            transactionQuery += "BEGIN ";
        //            transactionQuery += stockModel.updateProdImeiNumber(prodId, imei);
        //            transactionQuery += "END ";
        //        }

        //    }

        //    return transactionQuery;
        //}


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



        public string getProductUnitRatio(string prodId)
        {
            var stockModel = new StockModel();
            return stockModel.getProductUnitRatioSerializeDataModel(prodId);
        }


        public string getProductDataListAddToCart(string prodId)
        {
            var stockModel = new StockModel();
            return stockModel.getProductDataListAddToCartSerializeModelByProdCode(prodId);
        }

        public string getProductIdByProductCodeData(string productCode)
        {
            var stockModel = new StockModel();
            var dtSotck = stockModel.getProductIdByProductCodeModel(productCode);
            if (dtSotck.Rows.Count > 0)
                return dtSotck.Rows[0][0].ToString();
            else
                return "";
        }

        public string getAttributeNameData(string attributeRecord)
        {
            if (attributeRecord == "")
                return "";

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
                        if (i != 0)
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


        
    }




}
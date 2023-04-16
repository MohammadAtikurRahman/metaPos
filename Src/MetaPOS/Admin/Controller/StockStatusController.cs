using System;
using System.Collections.Generic;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Controller
{


    public class StockStatusController
    {


        //Model declear
        private Model.StockStatusModel objStockStatusModel = new Model.StockStatusModel();
        private Model.SaleModel objSaleModel = new Model.SaleModel();
        private CommonFunction commonFunction = new CommonFunction();

        // Global variable declear

       // private string output = "";
        public string status { get; set; }
        private static string billNo = "";
        public int count = 0;





        public dynamic createStockStatusInfo(Dictionary<string, string> dicData)
        {
            objStockStatusModel.billNo = objSaleModel.generateSaleId();
            objStockStatusModel.qty = dicData["qty"];
            //objStockStatusModel. = dicData["cusId" + i][i].ToString();
            objStockStatusModel.prodCode = dicData["prodCode"];
            objStockStatusModel.prodId = Convert.ToInt32(dicData["prodID"]);
            objStockStatusModel.sPrice = Convert.ToDecimal(dicData["sPrice"]);
            objStockStatusModel.prodName = dicData["prodName"];
            objStockStatusModel.supCompany = dicData["supCompany"];
            objStockStatusModel.catName = dicData["catName"];
            objStockStatusModel.bPrice = Convert.ToDecimal(dicData["bPrice"]);
            objStockStatusModel.status = dicData["status"];
            objStockStatusModel.imei = dicData["imei"];
            objStockStatusModel.stockTotal = Convert.ToDecimal(dicData["stockTotal"]);
            objStockStatusModel.storeId = dicData["storeId"];
            objStockStatusModel.lastQty = dicData["lastQty"];

            var lastQty = dicData["lastQty"];
            var qty = dicData["qty"];
            var prodId = dicData["prodID"];

            objStockStatusModel.balanceQty = commonFunction.calculateQty(prodId, lastQty, qty, "-");

            //
            return objStockStatusModel.createStockStatus();
        }





        public dynamic createListStockStatusInfo(Dictionary<string, Dictionary<int, object>> dicData)
        {
            var msg = "";
            billNo = objSaleModel.generateSaleId();

            for (int i = 0; i < count; i++)
            {
                objStockStatusModel.billNo = billNo;
                objStockStatusModel.qty = dicData["qty" + i][i].ToString();
                //objStockStatusModel. = dicData["cusId" + i][i].ToString();
                objStockStatusModel.prodCode = dicData["Code" + i][i].ToString();
                objStockStatusModel.prodId = Convert.ToInt32(dicData["prodId" + i][i]);
                objStockStatusModel.sPrice = Convert.ToDecimal(dicData["sPrice" + i][i]);
                objStockStatusModel.prodName = dicData["prodName" + i][i].ToString();
                objStockStatusModel.supCompany = dicData["supCompany" + i][i].ToString();
                objStockStatusModel.catName = dicData["catName" + i][i].ToString();
                objStockStatusModel.bPrice = Convert.ToDecimal(dicData["bPrice" + i][i]);
                objStockStatusModel.status = dicData["status" + i][i].ToString();
                //
                objStockStatusModel.createStockStatus();
            }

            return msg.ToString();
        }


    }


}
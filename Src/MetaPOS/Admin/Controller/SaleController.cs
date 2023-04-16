using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.Controller
{


    public class SaleController
    {


        public Dictionary<string, Dictionary<int, object>> dicSaleData =
            new Dictionary<string, Dictionary<int, object>>();

        private Model.SaleModel objSaleModel = new Model.SaleModel();
        private Model.SlipModel objSlipModel = new Model.SlipModel();
        private Model.CashReportModel _objCashReportModelModel = new Model.CashReportModel();
        private Model.StockModel objStock = new Model.StockModel();
        private Model.QuotationModel objQuotationModelModel = new Model.QuotationModel();
        private Model.TempPrintSaleModel objTempPrintSaleModel = new Model.TempPrintSaleModel();
        private Model.StockStatusModel objStockStatusModel = new Model.StockStatusModel();
        private Model.TempSaleModel objTempSaleModel = new Model.TempSaleModel();

        private ApiController objApiController = new ApiController();
        private CommonController objCommonController = new CommonController();
        private QuotationController objQuotationController = new QuotationController();

        public int count = 0;

        private int i = 0;
        //Variable Declearation
        public string disType, comment, warranty;
        private string[] orderId = new string[100];
        private string[] sku = new string[100];
        public int[] qty = new int[50];
        private string[] cusId = new string[100];
        private string[] prodCode = new string[100];
        private int[] prodId = new int[100];
        private decimal[] sPrice = new decimal[100];
        private string[] prodName = new string[100];
        private string[] supCompany = new string[100];
        private string[] catName = new string[100];
        private decimal[] bPrice = new decimal[100];
        private decimal netAmt = 0, grossAmt = 0;
        //private int branchId = 0;
       // private decimal disAmt, payCash, giftAmt, balance, currentCash;

        //Variable Value form Model
        private static string billNo = "";//, Status = "Sold";

        //Admin.Model.Quotation objQuotationModel = new Admin.Model.Quotation();
        // data Insert SalesInfo Table
        public dynamic createSaleInfo(Dictionary<string, Dictionary<int, object>> dicData)
        {
            //
            var msg = "";
           // Status = "Sold";
            billNo = objSaleModel.generateSaleId().ToString();

            for (i = 0; i < count; i++)
            {
                //orderId[i] = dicData["orderId" + i][i].ToString();
                //sku[i] = dicData["sku" + i][i].ToString();
                qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
                cusId[i] = dicData["cusId" + i][i].ToString();
                prodCode[i] = dicData["Code" + i][i].ToString();
                prodId[i] = Convert.ToInt32(dicData["prodId" + i][i]);
                sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);

                //Calculation with parameter
                netAmt += qty[i]*sPrice[i];
                grossAmt += qty[i]*sPrice[i];

                //Assaign with model perameter
                objSaleModel.billNo = billNo;
                objSaleModel.qty = qty[i].ToString();
                objSaleModel.cusID = cusId[i];
                objSaleModel.prodID = prodId[i].ToString();
                objSaleModel.netAmt = Convert.ToDecimal(netAmt);
                objSaleModel.grossAmt = Convert.ToDecimal(grossAmt);

                //Insert Sale data using Model
                objSaleModel.createSale();

                msg = "Successfully saved";
            }
            return msg;
        }





        // Insert data SlipInfo Table
        //public void createSlipInfo(Dictionary<string, Dictionary<int, object>> dicData)
        //{
        //    // Analysis Perameter

        //    // Assaigin Perameter with Model
        //    //for (i = 0; i < count; i++)
        //    //{
        //    //    //orderId[i] = dicData["orderId" + i][i].ToString();
        //    //    //sku[i] = dicData["sku" + i][i].ToString();
        //    //    qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
        //    //    cusId[i] = Convert.ToInt32(dicData["cusId" + i][i]);
        //    //    prodCode[i] = dicData["Code" + i][i].ToString();
        //    //    prodId[i] = Convert.ToInt32(dicData["prodId" + i][i]);
        //    //    sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);

        //    //    //Calculation with parameter
        //    //     netAmt = qty[i] * sPrice[i];
        //    //     grossAmt += netAmt;
        //    //    //grossAmt += qty[i] * sPrice[i];
        //    //    Status = "Sold";

        //    //    //Assaign with model perameter
        //    //    objSlipModel.billNo = billNo.ToString();
        //    //    objSlipModel.qty = qty[i];
        //    //    objSlipModel.cusId = cusId[i];
        //    //    objSlipModel.prodId = prodId[i];
        //    //    objSlipModel.netAmt = netAmt; ;
        //    //    objSlipModel.grossAmt = grossAmt;


        //    //}

        //    objSlipModel.billNo = billNo;
        //    objSlipModel.status = Status;

        //    //Insert Sale data using Model
        //    objSlipModel.createSlip();
        //}

        // Insert data CashReportInfo Table
        public void insertCashReportInfo(Dictionary<string, Dictionary<int, object>> dicData)
        {
            //for (i = 0; i < count; i++)
            //{
            //    //orderId[i] = dicData["orderId" + i][i].ToString();
            //    //sku[i] = dicData["sku" + i][i].ToString();
            //    qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
            //    cusId[i] = Convert.ToInt32(dicData["cusId" + i][i]);
            //    prodCode[i] = dicData["Code" + i][i].ToString();
            //    prodId[i] = Convert.ToInt32(dicData["prodId" + i][i]);
            //    sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);

            //    //Calculation with parameter
            //    netAmt += qty[i] * sPrice[i];
            //    grossAmt += qty[i] * sPrice[i];

            //    //Assaign with model perameter
            //    objCashReportModel.billNo = billNo.ToString();
            //    objCashReportModel.qty = qty[i];
            //    objCashReportModel.cusId = cusId[i];
            //    objCashReportModel.prodId = prodId[i];
            //    objCashReportModel.netAmt = netAmt; ;
            //    objCashReportModel.grossAmt = grossAmt;

            //    //Insert Sale data using Model
            //    objSlipModel.createSlip();
            //}
        }





        // Updata data StockInfo Table
        //public string updateStockInfo(Dictionary<string, Dictionary<int, object>> dicData)
        //{
        //    string countStock = "";
        //    for (i = 0; i < count; i++)
        //    {
        //        sku[i] = dicData["sku" + i][i].ToString();
        //        qty[i] = Convert.ToInt32(dicData["qty" + i][i]);

        //        objStock.sku = sku[i];
        //        DataSet ds = objStock.listStock();
        //        int stockQty = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

        //        int currentQty = (stockQty + qty[i]);

        //        objStock.qty = currentQty;
        //        objStock.sku = sku[i];

        //        // Dataset Stock
        //        DataSet dsStock = objStock.listStock();
        //        countStock = dsStock.Tables[0].Rows.Count.ToString();


        //        // Set Stock update data
        //        Dictionary<string, string> dicStockUpdateData = new Dictionary<string, string>();
        //        dicStockUpdateData.Add("qty", currentQty.ToString());

        //        // Set stock conditional data
        //        Dictionary<string, string> dicStockConditionalData = new Dictionary<string, string>();
        //        dicStockConditionalData.Add("sku", sku.ToString());

        //        var getFormatUpdateItemData = objCommonController.getFormatedUpdateParameter(dicStockUpdateData);
        //        var getFormatedConditinalParameter = objCommonController.getFormatedConditinalParameter(dicStockConditionalData);
        //        objStock.updateStock(getFormatUpdateItemData, getFormatedConditinalParameter);

        //        // Syncronous with eCommerce 
        //        objApiController.synchStock(sku[i], currentQty);

        //    }

        //    return countStock;
        //}

        //Insert StockStatusInfo
        //public void createStockStatusInfo(Dictionary<string, Dictionary<int, object>> dicData)
        //{
        //    var msg = "";
        //    Status = "sale";
        //    for (int i = 0; i < count; i++)
        //    {
        //        qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
        //        cusId[i] = Convert.ToInt32(dicData["cusId" + i][i]);
        //        prodCode[i] = dicData["Code" + i][i].ToString();
        //        prodId[i] = Convert.ToInt32(dicData["prodId" + i][i]);
        //        sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);
        //        prodName[i] = dicData["prodName" + i][i].ToString();
        //        supCompany[i] = dicData["supCompany" +i][i].ToString();
        //        catName[i] = dicData["catName"+i][i].ToString();
        //        bPrice[i] = Convert.ToDecimal(dicData["bPrice" + i][i]);

        //        //Assign in controller
        //        objStockStatusModel.billNo = billNo;
        //        objStockStatusModel.prodId = prodId[i];
        //        objStockStatusModel.prodCode = prodCode[i];
        //        objStockStatusModel.prodName = prodName[i];
        //        objStockStatusModel.supCompany = supCompany[i];
        //        objStockStatusModel.catName= catName[i];
        //        objStockStatusModel.qty= qty[i];
        //        objStockStatusModel.status = Status;
        //        objStockStatusModel.sPrice = sPrice[i];
        //        objStockStatusModel.bPrice = bPrice[i];
        //        //Insert model
        //        //objCashReportModel.createCashReport();
        //        objStockStatusModel.createStockStatus();
        //    }
        //}


        // update quotation info Table
        //public void updateQuotation(Dictionary<string, Dictionary<int, object>> dicData)
        //{
        //    for (i = 0; i < count; i++)
        //    {
        //        orderId[i] = dicData["orderId" + i][i].ToString();
        //        objQuotationModel.orderId = orderId[i];
        //        objQuotationModel.status = '1';
        //        //
        //        objQuotationController.updateQuotation(orderId[i]);
        //    }
        //}

        //public void cancelQuotation(Dictionary<string, Dictionary<int, object>> dicData)
        //{
        //    for (i = 0; i < count; i++)
        //    {
        //        orderId[i] = dicData["orderId" + i][i].ToString();
        //        objQuotationModel.orderId = orderId[i];
        //        objQuotationModel.status = '2';
        //        //
        //        objQuotationController.updateQuotation(orderId[i]);
        //    }
        //}


        ////Insert Data TempPrintSaleInfo Table 
        //public void createTempPrintSaleInfo(Dictionary<string, Dictionary<int, object>> dicData)
        //{
        //    for (i = 0; i < count; i++)
        //    {

        //        //orderId[i] = dicData["orderId" + i][i].ToString();
        //        //sku[i] = dicData["sku" + i][i].ToString();
        //        qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
        //        cusId[i] = Convert.ToInt32(dicData["cusId" + i][i]);
        //        prodCode[i] = dicData["Code" + i][i].ToString();
        //        prodId[i] = Convert.ToInt32(dicData["prodId" + i][i]);
        //        sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);
        //        //
        //        objTempPrintSaleModel.billNo = billNo;
        //        objTempPrintSaleModel.cusId = cusId[i].ToString();
        //        objTempPrintSaleModel.prodId = prodId[i].ToString();

        //        //
        //       objTempPrintSaleModel.createTempPrintSale();
        //    }
        //}

        ////Insert TempSaleInfo Table 
        //public void createTempSaleInfo(Dictionary<string, Dictionary<int, object>> dicData)
        //{
        //    for (i = 0; i < count; i++)
        //    {
        //        objTempSaleModel.billNo = billNo;

        //        //orderId[i] = dicData["orderId" + i][i].ToString();
        //        //sku[i] = dicData["sku" + i][i].ToString();
        //        //qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
        //        //cusId[i] = Convert.ToInt32(dicData["cusId" + i][i]);
        //        //prodCode[i] = dicData["Code" + i][i].ToString();
        //        //prodId[i] = Convert.ToInt32(dicData["prodId" + i][i]);
        //        //sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);
        //        ////
        //        //objTempPrintSaleModel.billNo = billNo;
        //        //objTempPrintSaleModel.cusId = cusId[i].ToString();
        //        //objTempPrintSaleModel.prodId = prodId[i].ToString();

        //        //
        //        objTempSaleModel.createTemSale();
        //    }
        //}
    }


}
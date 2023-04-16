using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.Controller
{


    public class StockController
    {


        // Controller object declear 
        private Controller.CommonController objCommonController = new CommonController();
        private Controller.ApiController objApiController = new ApiController();
        private DataAccess.SqlOperation objSqlOperation = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();


        // Model object declear
        private Model.StockModel objStock = new Model.StockModel();

        // Global variable declear 
        public int count = 0, i = 0;
        private string[] sku = new string[100];
        private int[] qty = new int[100];


        //private string output = "";





        public void createStockInfo()
        {
        }





        // Stock Qty Minus
        public dynamic updateStockInfo(Dictionary<string, string> dicData)
        {
            string countStock = "", output = "", prodCode = "",currentQty = "0";;
           // int qty = 0, stockQty = 0;
            
            prodCode = dicData["prodCode"];
            currentQty = dicData["qty"];
            decimal stockTotal = Convert.ToDecimal(dicData["stockTotal"]);


            objStock.qty = currentQty;
            objStock.sku = prodCode;

            // Dataset Stock
            DataSet dsStock = objStock.listStock();
            countStock = dsStock.Tables[0].Rows.Count.ToString();


            // Set Stock update data
            Dictionary<string, string> dicStockUpdateData = new Dictionary<string, string>();
            dicStockUpdateData.Add("qty", currentQty.ToString());
            dicStockUpdateData.Add("stockTotal", stockTotal.ToString());
            dicStockUpdateData.Add("imei", dicData["imei"]);

            // Set stock conditional data
            Dictionary<string, string> dicStockConditionalData = new Dictionary<string, string>();
            dicStockConditionalData.Add("prodCode", prodCode.ToString());

            var getFormatUpdateItemData = objCommonController.getUpdateParameter(dicStockUpdateData);
            var getFormatedConditinalParameter = objCommonController.getConditinalParameter(dicStockConditionalData);
            objStock.updateStock(getFormatUpdateItemData, getFormatedConditinalParameter);

            // Syncronous with eCommerce 
            //objApiController.synchStock(sku, currentQty);

            return output;
        }





        public dynamic updateListStockInfo(Dictionary<string, Dictionary<int, object>> dicData)
        {
            string countStock = "", output = "";
            int stockQty = 0, currentQty = 0;

            for (i = 0; i < count; i++)
            {
                sku[i] = dicData["sku" + i][i].ToString();
                qty[i] = Convert.ToInt32(dicData["qty" + i][i]);

                //objStock.sku = sku[i];
                Dictionary<string, string> dicStock = new Dictionary<string, string>();
                dicStock.Add("sku", sku[i]);
                var getConditinalParameter = objCommonController.getConditinalParameter(dicStock);
                DataSet ds = objStock.getStockConditinalParameter(getConditinalParameter);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    stockQty = Convert.ToInt32(ds.Tables[0].Rows[0][7]);
                    currentQty = (stockQty + Convert.ToInt32(qty[i]));

                    objStock.qty = currentQty.ToString();
                    objStock.sku = sku[i];
                }

                // Dataset Stock
                DataSet dsStock = objStock.listStock();
                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    countStock = dsStock.Tables[0].Rows.Count.ToString();
                }

                // Set Stock update data
                Dictionary<string, string> dicStockUpdateData = new Dictionary<string, string>();
                dicStockUpdateData.Add("qty", currentQty.ToString());

                // Set stock conditional data
                Dictionary<string, string> dicStockConditionalData = new Dictionary<string, string>();
                dicStockConditionalData.Add("sku", sku[i].ToString());

                var getFormatUpdateItemData = objCommonController.getUpdateParameter(dicStockUpdateData);
                var getFormatedConditinalParameter = objCommonController.getConditinalParameter(dicStockConditionalData);
                objStock.updateStock(getFormatUpdateItemData, getFormatedConditinalParameter);

                // Syncronous with eCommerce 
                objApiController.synchStock(sku[i], currentQty);
            }

            return output;
        }





        





        // Get Stock
        //public dynamic getStockInfo(Dictionary<string, Dictionary<int, object>> dicData)
        //{

        //}
    }


}
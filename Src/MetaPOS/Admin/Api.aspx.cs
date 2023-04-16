using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;


namespace MetaPOS.Admin
{


    public partial class Api : Page
    {


        private Controller.ApiController objApiController = new Controller.ApiController();
        private Controller.CommonController objCommonController = new Controller.CommonController();
        private Model.StockModel objStock = new Model.StockModel();
        private Model.QuotationModel objQuotationModelModel = new Model.QuotationModel();





        protected void Page_Load(object sender, EventArgs e)
        {
            // string remoteUrl = "http://vaporcloudpos.com/Admin/Api.aspx?orderId=113&sku=jax-blueberry-vanilla-custard-60ml-3mg&qty=5&prodId=&name=Rahim&phone=01783663&mailInfo=sayed@gmail.com";

            DataSet ds;//, dsQuotationStock;
            string orderId = "";//getQuotationOrderId = "", getQuotationSku = "", sku = "",
            int stockQty = 0;//, orderQty = 0;
            //objStock.sku = Request["sku"];
            Dictionary<string, string> dicStock = new Dictionary<string, string>();
            dicStock.Add("sku", Request["sku"]);
            var getConditinalParameter = objCommonController.getConditinalParameter(dicStock);
            ds = objStock.getStockConditinalParameter(getConditinalParameter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                stockQty = Convert.ToInt32(ds.Tables[0].Rows[0][7]);
            }


            try
            {
                orderId = Request["orderId"].ToString();
            }
            catch (Exception)
            {
                orderId = "";
            }

            //check order or search
            if (orderId != "")
                funcNewOrder();
            else
                funcSearchProducts();


            //DataSet ds, dsQuotationStock;
            //string getQuotationOrderId = "", getQuotationSku = "";
            //int stockQty = 0, orderQty = 0;
            ////objStock.sku = Request["sku"];
            //Dictionary<string, string> dicStock = new Dictionary<string, string>();
            //dicStock.Add("sku", Request["sku"]);
            //var getConditinalParameter = objCommonController.getConditinalParameter(dicStock);
            //ds = objStock.getStockConditinalParameter(getConditinalParameter);
            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //    stockQty = Convert.ToInt32(ds.Tables[0].Rows[0][7]);
            //}

            //orderQty = Convert.ToInt32(Request["qty"]);

            //// Check order duplicate 
            //string orderId = Request["orderId"].ToString();
            //string sku = Request["sku"].ToString();

            //objQuotationModelModel.orderId = orderId;
            //dsQuotationStock = objQuotationModelModel.getQuotationOrderUsingApi();

            //if (dsQuotationStock.Tables[0].Rows.Count != 0)
            //{
            //    getQuotationOrderId = dsQuotationStock.Tables[0].Rows[0][0].ToString();
            //    getQuotationSku = dsQuotationStock.Tables[0].Rows[0][2].ToString();
            //}

            //if (stockQty < orderQty)
            //{
            //    lblResult.Text = "Stock is not available.";
            //    return;
            //}
            //if ((orderId == getQuotationOrderId) && (sku == getQuotationSku))
            //{
            //    lblResult.Text = "Warning !! order Id and sku code same !!!  ";
            //    return;
            //}

            //lblResult.Text = objApiController.synchNewOrder(Request["orderId"], Request["sku"], Request["qty"], "0", Request["name"], Request["phone"], Request["mailInfo"], Request["branchId"]);
        }





        private void funcNewOrder()
        {
            string getQuotationOrderId = "", getQuotationSku = "";
            int stockQty = 0;

            int orderQty = Convert.ToInt32(Request["qty"]);

            // Check order duplicate 
            string orderId = Request["orderId"].ToString();
            string sku = Request["sku"].ToString();

            objQuotationModelModel.orderId = orderId;
            DataSet dsQuotationStock = objQuotationModelModel.getQuotationOrderUsingApi();

            if (dsQuotationStock.Tables[0].Rows.Count != 0)
            {
                getQuotationOrderId = dsQuotationStock.Tables[0].Rows[0][0].ToString();
                getQuotationSku = dsQuotationStock.Tables[0].Rows[0][2].ToString();
            }

            if (stockQty < orderQty)
            {
                lblResult.Text = "Stock is not available.";
                return;
            }
            if ((orderId == getQuotationOrderId) && (sku == getQuotationSku))
            {
                lblResult.Text = "Warning !! order Id and sku code same !!!  ";
                return;
            }

            lblResult.Text = objApiController.synchNewOrder(Request["orderId"], Request["sku"], Request["qty"], "0",
                Request["name"], Request["phone"], Request["mailInfo"], Request["branchId"]);
        }





        private void funcSearchProducts()
        {
            string sku = Request["sku"].ToString();

            if (sku == "")
            {
                lblResult.Text = "Warning !! SKU is empty.";
                return;
            }


            lblResult.Text = objApiController.getProductRowsbySku(Request["sku"]);
        }


    }


}
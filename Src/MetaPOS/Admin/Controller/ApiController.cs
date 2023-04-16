using System.IO;
using System.Net;
using System.Text;
using System;
using System.Data;
using System.Collections.Generic;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Controller
{


    public class ApiController
    {


        private CustomerModel objCustomerModelModel = new CustomerModel();
        private QuotationModel objQuotationModelModel = new QuotationModel();
        private StockModel objStock = new StockModel();
        private CommonController objCommonController = new CommonController();
        private SqlOperation objSql = new SqlOperation();


        public void synchStock(string sku, int qty)
        {
            string remoteUrl = "http://vaporcloudbd.com/vc_api/insert_details.php?sku=" + sku + "&qty=" + qty;

            ASCIIEncoding encoding = new ASCIIEncoding();
            string data = string.Format("sku={0}&qty={1}", sku, qty);
            byte[] bytes = encoding.GetBytes(data);

            var httpRequest = (HttpWebRequest) System.Net.WebRequest.Create(remoteUrl);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.ContentLength = bytes.Length;

            using (Stream stream = httpRequest.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
        }





        public dynamic getProductRowsbySku(string sku)
        {
            // Select rows
            string query = "SELECT sku,qty,createdFor FROM StockInfo WHERE sku = '" + sku + "'";
            DataTable dt = objSql.getDataTable(query);

            if (dt.Rows.Count <= 0)
                return "";

            return DataTableToJsonWithStringBuilder(dt);
        }





        public string DataTableToJsonWithStringBuilder(DataTable table)
        {
            var jsonString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                              + "\":" + "\""
                                              + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            jsonString.Append("\"" + table.Columns[j].ColumnName.ToString()
                                              + "\":" + "\""
                                              + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        jsonString.Append("}");
                    }
                    else
                    {
                        jsonString.Append("},");
                    }
                }
                jsonString.Append("]");
            }
            return jsonString.ToString();
        }





        public dynamic synchNewOrder(string orderId, string sku, string qty, string prodId, string name, string phone,
            string mailInfo, string branchId)
        {
            //Initializer parameter data
            //orderId = "1020";
            //sku = "pk-0018";
            //qty = "12";
            //prodId = 5124;
            //name = "Rohim Ahmed";
            //phone = "017856411";
            //mailInfo = "sayed22@gmail.com";

            // Insert customer data assign

            //bool exists = false;
            string getCusId = "";
            objQuotationModelModel.orderId = orderId;
            DataSet ds = objQuotationModelModel.getQuotationOrderUsingApi();

            //var quotationOrderId = ds.Tables[0].Rows[0][0].ToString();

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    quotationOrderId = ds.Tables[0].Rows[i][0].ToString();
            //    if (quotationOrderId == orderId)
            //        exists = true;

            //    //cusId = ds.Tables[0].Rows[0][1].ToString();
            //}
            //return ds.Tables[0].Rows.Count.ToString();

            int count = ds.Tables[0].Rows.Count;

            if (count <= 0)
            {
                // Generate customer new id
                getCusId = objCustomerModelModel.generateCustomerId();
                //if (orderId == )
                objCustomerModelModel.cusId = getCusId;
                objCustomerModelModel.name = name;
                objCustomerModelModel.phone = phone;
                objCustomerModelModel.mailInfo = mailInfo;
                objCustomerModelModel.orderId = orderId;

                // Insert Customer Data
                objCustomerModelModel.createCustomer();
            }
            else
            {
                getCusId = ds.Tables[0].Rows[0][1].ToString();
            }

            // Insert Quotation Data
            objQuotationModelModel.cusId = getCusId;
            objQuotationModelModel.prodId = prodId;
            objQuotationModelModel.sku = sku;
            objQuotationModelModel.qty = qty;
            objQuotationModelModel.orderId = orderId;
            objQuotationModelModel.branchId = branchId;
            objQuotationModelModel.createQuotation();

            // Stock Update 
            objStock.sku = sku;

            Dictionary<string, string> dicStock = new Dictionary<string, string>();
            dicStock.Add("sku", sku);
            var getConditinalParameter = objCommonController.getConditinalParameter(dicStock);
            ds = objStock.getStockConditinalParameter(getConditinalParameter);
            int stockQty = Convert.ToInt32(ds.Tables[0].Rows[0][7]);
            int currentQty = (stockQty - Convert.ToInt32(qty));

            // Dataset Stock
            DataSet dsStock = objStock.listStock();

            // Set Stock update data 
            var dicStockUpdateData = new Dictionary<string, string>();
            dicStockUpdateData.Add("qty", currentQty.ToString());

            // Set stock conditional data
            var dicStockConditionalData = new Dictionary<string, string>();
            dicStockConditionalData.Add("sku", sku);

            // Perameter send in common controller
            var getFormatUpdateItemData = objCommonController.getUpdateParameter(dicStockUpdateData);
            var getFormatedConditinalParameter = objCommonController.getConditinalParameter(dicStockConditionalData);

            objStock.updateStock(getFormatUpdateItemData, getFormatedConditinalParameter);

            return "Order requested successfully.";
        }


    }


}
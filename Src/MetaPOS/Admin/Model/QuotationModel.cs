using System.Collections.Generic;
using System.Data;


namespace MetaPOS.Admin.Model
{


    public class QuotationModel
    {


        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();
        private DataAccess.SqlOperation objSqlOperation = new DataAccess.SqlOperation();
        private DataSet ds;
        private string query = "";

        public Dictionary<string, string> dicData = new Dictionary<string, string>();

        //set database perameter
        public string orderId = "";
        public string sku = "";
        public string qty = "0";
        public string prodId = "0";
        public string cusId = "";
        //entry date
        public string branchId { get; set; }
        public char status { get; set; }

        // Create new Quotation
        public void createQuotation()
        {
            query = "INSERT INTO QuotationInfo VALUES ('" +
                    prodId + "','" +
                    sku + "','" +
                    qty + "','" +
                    orderId + "','" +
                    cusId + "','" +
                    objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy") + "','" +
                    '0' + "','" +
                    branchId + "')";
            objSqlOperation.executeQuery(query);
        }





        // Get Data Quotation
        public DataSet getQuotation()
        {
            query = "SELECT * FROM QuotationInfo";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        // Update Data Quotation
        public void updateQuotation(string getFormatedConditinalParameter, string num)
        {
            query = "UPDATE QuotationInfo SET status ='" + num + "' WHERE " + getFormatedConditinalParameter + "";
            objSqlOperation.executeQuery(query);
        }





        // Get quotation data with customers
        public DataSet getQuotationJoinCustomer()
        {
            query =
                "SELECT quot.prodId as prodId,quot.prodSku as sku,quot.prodQty as qty,quot.cusID as cusId,quot.orderId as orderId,stock.prodCode as Code,stock.prodID as prodId, stock.sPrice AS sPrice, stock.prodName, stock.supCompany,stock.catName,stock.bPrice FROM	QuotationInfo AS quot LEFT JOIN StockInfo AS stock ON quot.prodSku = stock.sku WHERE orderId= '" +
                ((Dictionary<string, string>) dicData)["orderId"] + "' AND quot.orderId != 0";
            ds = objSqlOperation.getDataSet(query);

            return ds;
        }





        // Get Quotation OrderId 
        public DataSet getQuotationOrderUsingApi()
        {
            query = "SELECT orderId,cusId,prodSku FROM QuotationInfo WHERE orderId = '" + orderId + "'";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        // Update Quotation
        public string updateQuotation(string getFormatedConditinalParameter)
        {
            query = "UPDATE QuotationInfo SET status= '1' WHERE " + getFormatedConditinalParameter + "";
            objSqlOperation.executeQuery(query);
            return query;
        }





        //Notification 
        public string Notification()
        {
            query = "SELECT * FROM QuotationInfo WHERE status = '0'";
            ds = objSqlOperation.getDataSet(query);
            string NotificationCount = ds.Tables[0].Rows.Count.ToString();
            return NotificationCount;
        }


    }


}
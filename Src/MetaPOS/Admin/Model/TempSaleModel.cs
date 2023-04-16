using System;
using System.Data;


namespace MetaPOS.Admin.Model
{


    public class TempSaleModel
    {


        private DataAccess.SqlOperation objSqlOperation = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();

        // Global Variable 
        private string query = "";
        private DataSet ds, dsTempSaleInfo;

        // Initialize TempSale
        public string billNo { get; set; }
        public string prodId { get; set; }
        public string prodCode { get; set; }
        public string prodName { get; set; }
        public int qty { get; set; }
        public decimal sPrice { get; set; }
        public decimal totalPrice { get; set; }
        //entry date
        //role Id
        public string productSource { get; set; }
        public string prodCodes { get; set; }

        // Insert TempSaleInfo Table
        public void createTemSale()
        {
            //query = "INSERT INTO TempSaleInfo VALUES '" +
            //                                            billNo + "','" +
            //                                            prodId + "','" +
            //                                            prodCode + "','" +
            //                                            prodName + "','" +
            //                                            qty + "','" +
            //                                            sPrice + "','" +
            //                                            totalPrice + "','" +
            //                                            objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy") + "','" +
            //                                            HttpContext.Current.Session["roleId"] + "','" +
            //                                            productSource + "','" +
            //                                            prodCodes + "'";


            //var msg = "";
            ds = objSqlOperation.getDataSet("SELECT * FROM SaleInfo WHERE billNo = '" + billNo + "'");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dsTempSaleInfo =
                    objSqlOperation.getDataSet("SELECT * FROM [StockStatusInfo] WHERE prodID = '" +
                                               ds.Tables[0].Rows[i][4] + "' AND  billNo = '" + billNo +
                                               "' AND status = 'sale' ");


                query =
                    "INSERT INTO [TempSaleInfo] (   billNo,                              prodID,                                         prodCode,                                               prodName,                                                  qty,                                               sPrice,                                                                                 totalPrice,                                                                                entryDate,                                    productSource,                                prodCodes) VALUES ('" +
                    billNo + "', '" + ds.Tables[0].Rows[i][4].ToString() + "', '" +
                    dsTempSaleInfo.Tables[0].Rows[0][2].ToString() + "', '" +
                    dsTempSaleInfo.Tables[0].Rows[0][3].ToString() + "', '" + ds.Tables[0].Rows[i][5].ToString() +
                    "', '" + dsTempSaleInfo.Tables[0].Rows[0][9].ToString() + "', '" +
                    Convert.ToDecimal(ds.Tables[0].Rows[i][5].ToString())*
                    Convert.ToDecimal(dsTempSaleInfo.Tables[0].Rows[0][9].ToString()) + "', '" +
                    objCommonFun.GetCurrentTime().ToShortDateString() + "', '" + dsTempSaleInfo.Tables[0].Rows[0][27] +
                    "', '" + dsTempSaleInfo.Tables[0].Rows[0][28] + "') ";
                objSqlOperation.executeQuery(query);
            }
        }





        // Update TempSaleInfo Table 
        public void updateTempSale()
        {
        }





        //Delete TempSaleInfo Table
        public void deleteTempSale()
        {
        }


    }


}
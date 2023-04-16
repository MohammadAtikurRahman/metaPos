using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.Model
{


    public class TempPrintSaleModel
    {


        private Admin.DataAccess.SqlOperation objSqlOperation = new DataAccess.SqlOperation();
        private Admin.DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();

        // Global variables
        private string query = "";
        //private DataSet ds;

        // Initialize Database Perameter
        public string billNo { get; set; }
        //roleId
        public string cusId { get; set; }
        public string prodId { get; set; }
        public string payMethod { get; set; }
        //entry date
        public string productSource { get; set; }
        public string prodCodes { get; set; }

        // Insert TempPrintSaleInfo Table
        public string createTempPrintSale()
        {
            query = "INSERT INTO TempPrintSaleInfo VALUES ('" +
                    billNo + "','" +
                    HttpContext.Current.Session["roleId"] + "','" +
                    cusId + "','" +
                    prodId + "','" +
                    payMethod + "','" +
                    objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy") + "','" +
                    productSource + "','" +
                    prodCodes + "')";
            var msg = objSqlOperation.executeQuery(query);
            return query;
        }





        // Update TempPrintSaleInfo Table
        public void updateTempPrintSale()
        {
        }





        // Delete TempPrintSaleInfo Table 
        public void deleteTempPrintSale()
        {
        }


    }


}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;


namespace MetaPOS.Api.Service
{
   public class CustomerModel : ProductsEntiyes
   {
       private CommonFunction commonFunction = new CommonFunction();
       SqlOperation sqlOperation = new SqlOperation(); 
       //private DataSet ds;

       //public CustomerModel()
       //{
       //    totalPaid = 0;
       //    totalDue = 0;
       //    bloodGroup = "";
       //    memberId = "";
       //    openingDue = 0;
       //    phone = "";
       //    mailInfo = "";
       //    address = "";
       //    installmentStatus = false;
       //    designation = "";


       //}



       







        // List of customer data
       public DataSet listCustomer(string shopname)
       {
           DataSet ds;
           sqlOperation.conString = shopname;
           string query = "SELECT cusID FROM CustomerInfo WHERE cusID !='0' ORDER BY cusID DESC";
           ds = sqlOperation.getDataSet(query);
           return ds;
       }



        // Generate customer new id
        public string generateCusId(string subdomain)
        {
            string cusID = string.Empty;
            DataSet lastCusId = listCustomer(subdomain);


            try
            {
                int currentInt = Convert.ToInt32(lastCusId.Tables[0].Rows[0][0].ToString());
                ++currentInt;
                cusID = currentInt.ToString("0000000");
            }
            catch
            {
                cusID = "0000001";
            }
            return cusID;
        }
    }
}

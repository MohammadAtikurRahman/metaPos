using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class SmsConfigModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        public string medium { get; set; }
        public string apiKey { get; set; }
        public string senderId { get; set; }
        public string username { get; set; }
        public string password { get; set; }





        public DataTable getSmsCofigData()
        {
            return sqlOperation.getDataTable("SELECT medium,apiKey,senderId,username,password,senderKey FROM SmsConfigInfo");
        }





        public DataTable getSmsConfigDataByBranchId()
        {
            return
                sqlOperation.getDataTable("SELECT * FROM SmsConfigInfo WHERE roleId='" +
                                          commonFunction.getBranchID(HttpContext.Current.Session["roleId"].ToString()) + "'");
        }


        public void updateBalanceModel(decimal smsTotalCost)
        {
            sqlOperation.executeQuery("UPDATE SmsConfigInfo SET balance=balance - " + smsTotalCost + " where roleId='" +
                                      commonFunction.getBranchID(HttpContext.Current.Session["roleId"].ToString()) + "'");
        }


        public DataTable getSmsConfigDataModel() 
        {
            return
                sqlOperation.getDataTable("SELECT * FROM SmsConfigInfo WHERE roleId='" +
                                          commonFunction.getBranchID(HttpContext.Current.Session["roleId"].ToString()) + "'");
        }


        // Get customer data for API
        public DataTable getSmsConfigApiDataModel(string getConditionalParameter)
        {
            var query = "SELECT * FROM SmsConfigInfo WHERE " + getConditionalParameter;
            var dt = sqlOperation.getDataTable(query);
            return dt;
        }
    }
}

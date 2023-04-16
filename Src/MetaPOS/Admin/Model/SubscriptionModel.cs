using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class SubscriptionModel
    {

        private readonly CommonFunction commonFunction = new CommonFunction();
        private readonly SqlOperation sqlOperation = new SqlOperation();


        public int Id { get; set; }
        public int roleId { get; set; }
        public int storeId { get; set; }
        public string invoiceNo { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string paymentMode { get; set; }
        public decimal cashin { get; set; }
        public decimal cashout { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime createDate { get; set; }
        public DateTime expiryDate { get; set; }


        public DataTable getMonthlyFeeModel()
        {
            return sqlOperation.getDataTable("SELECT monthlyFee FROM RoleInfo WHERE active='1' " + HttpContext.Current.Session["userAccessParameters"] + "");
        }




        public bool saveSubscriptionModel()
        {
            string query = "INSERT INTO SubscriptionInfo(roleId,storeId,invoiceNo,name,description,cashin,cashout,status,paymentMode,type,createDate,updateDate) VALUES ('" +
                    roleId + "','" + storeId + "','" + invoiceNo + "','" + name + "','" + description + "','" + cashin + "','" + cashout + "','" + status + "','" +
                    paymentMode + "','" + type + "','" + createDate + "','" + updateDate + "')";
            
            return sqlOperation.fireQuery(query);

        }




        public DataTable getBalanceModel(bool isAutoPayment)
        {
            string queryStr = "";
            if (isAutoPayment == true)
            {
                queryStr += "status != '0' AND";
            }

            return sqlOperation.getDataTable("SELECT SUM(cashin) as totalCashin, SUM(cashout) as totalCashout FROM SubscriptionInfo WHERE "+queryStr+" type !='' " + HttpContext.Current.Session["userAccessParameters"] + "");
        }




        public DataTable getSubscribeCreatedInfo()
        {
            string query = "SELECT * FROM SubscriptionInfo WHERE type ='Billed' AND  MONTH(createDate) = MONTH('" +
                           commonFunction.GetCurrentTime() + "') " + HttpContext.Current.Session["userAccessParameters"] +
                           " ORDER BY Id DESC";
            return sqlOperation.getDataTable(query);
        }




        public bool updateStatusModel(string Id)
        {
            return sqlOperation.fireQuery("UPDATE SubscriptionInfo SET status='1',createDate='" + commonFunction.GetCurrentTime() + "' WHERE Id='" + Id + "'");
        }




        public DataTable getSubscriptionDataModel()
        {
            string query =
                "SELECT * FROM subscriptionInfo WHERE invoiceNo !='' " + HttpContext.Current.Session["userAccessParameters"] + "";
            return sqlOperation.getDataTable(query);
        }





        public DataTable getSubscriotionDataByInvoice()
        {
            return sqlOperation.getDataTable("SELECT * FROM SubscriptionInfo WHERE invoiceNo='" + invoiceNo + "'");
        }




        public DataTable getSubscriptionDataForPaymentModel()
        {
            string query =
                "SELECT * FROM RoleInfo as role LEFT JOIN BranchInfo branch ON role.storeId = branch.storeId  WHERE role.branchId='0' " +
                commonFunction.getUserAccessParameters("role") + "";
            return sqlOperation.getDataTable(query);
        }



        //update roleInfo
        public string UpdateRoleInfoDateModel(DateTime expiryDate)
        {
            try
            {
                string query = "UPDATE RoleInfo SET ExpiryDate='" + expiryDate + "' WHERE active='1' " + HttpContext.Current.Session["userAccessParameters"] + "";
                return sqlOperation.executeQuery(query);
            }
            catch (Exception ex)
            {
                return "false|" + ex.ToString();
            }
        }

        public string UpdateSubscriptionInfoDataModel()
        {
            try
            {
                string query = "UPDATE SubscriptionInfo SET createDate='" + expiryDate.ToString("dd-MMM-yyyy") + "', updateDate='" + expiryDate + "' WHERE " + HttpContext.Current.Session["userAccessParameters"];
                return sqlOperation.executeQuery(query);
            }
            catch (Exception ex)
            {
                return "false|" + ex.ToString();
            }
        }

    }
}
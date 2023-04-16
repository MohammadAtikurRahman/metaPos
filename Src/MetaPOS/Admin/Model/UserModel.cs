using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using System.Data;
using MetaPOS.Entities.UserAggregate;


namespace MetaPOS.Admin.Model
{
    public class UserModel
    {
        private readonly CommonFunction commonFunction = new CommonFunction();
        private SqlOperation sqlOperation = new SqlOperation();

        public string sign { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public decimal subscriptionFee { get; set; }
        public string branchLimit { get; set; }
        public string userLimit { get; set; }
        public DateTime expiryDate { get; set; }
        public string accessPage { get; set; }
        public string userRight { get; set; }
        public int roleId { get; set; }
        public int branchId { get; set; }
        public int groupId { get; set; }
        public int storeId { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }
        public Decimal version { get; set; }

        //for subs
        public string invoiceNo { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string paymentMode { get; set; }
        public decimal cashin { get; set; }
        public decimal cashout { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public DateTime createDate { get; set; }

        public string updateMonthFee()
        {
            return sqlOperation.executeQuery("Update RoleInfo SET monthlyFee=monthlyFee" + sign + subscriptionFee + " WHERE roleId='" +
                                      branchId + "'");
        }


        public DataTable GetUserDataModel()
        {
            return sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleId+ "'");
        }


        public string SaveUserDataModel()
        {
            try
            {
                string query = "INSERT INTO RoleInfo (title, password, accessPage, userRight, roleId, branchId, groupId, email, entryDate, updateDate, version, branchLimit, userLimit, storeId, activeDate, expiryDate, monthlyFee) VALUES ('" + title + "','" +
                    password + "','" + accessPage + "','" + userRight + "','" + roleId + "','" + branchId + "','" + groupId + "','" + email + "','" + entryDate + "','" + updateDate + "','" + version + "','" + branchLimit + "','" + userLimit + "','" + storeId + "','" + entryDate + "','" + expiryDate + "','" + subscriptionFee + "')";
                return sqlOperation.executeQuery(query);
            }
            catch (Exception ex)
            {
                return "false|" + ex;
            }
        }


        public string UpdateUserDataModel()
        {
            try
            {
                string query = "UPDATE RoleInfo SET title='" + title + "', email='" + email + "',password='" + password + "',monthlyfee='" + subscriptionFee + "',branchLimit='" + branchLimit + "',userLimit='" + userLimit + "',storeId='" + storeId + "', expiryDate='" + expiryDate + "',accessPage='" + accessPage + "' WHERE roleId='" + roleId + "'";
                return sqlOperation.executeQuery(query);
            }
            catch (Exception ex)
            {
                return "false|" + ex.ToString();
            }
        }



        //update subscriptionInfo  expiryDate.ToString("dd-MMM-yyyy") 
        public string UpdateSubscriptionInfoDataModel(string q)
        {
            try
            {
                string query = "UPDATE SubscriptionInfo SET status='" + q + "', createDate='" + commonFunction.GetCurrentTime() + "', updateDate='" + commonFunction.GetCurrentTime() + "' WHERE roleId='" + roleId + "'";
                return sqlOperation.executeQuery(query);
            }
            catch (Exception ex)
            {
                return "false|" + ex.ToString();
            }
        }


        //subscriptionInfo for manual payment
        public bool saveSubscriptionModel()
        {
            string query = "INSERT INTO SubscriptionInfo(roleId,storeId,invoiceNo,name,description,cashin,cashout,status,paymentMode,type,createDate,updateDate) VALUES ('" +
                    roleId + "','" + storeId + "','" + invoiceNo + "','" + name + "','" + description + "','" + cashin + "','" + cashout + "','" + status + "','" +
                    paymentMode + "','" + type + "','" + createDate + "','" + updateDate + "')";

            return sqlOperation.fireQuery(query);

        }



        public DataTable getSubscribeCreatedInfo()
        {
            string query = "SELECT * FROM SubscriptionInfo WHERE type ='Billed' AND MONTH(createDate) = MONTH('" +
                           commonFunction.GetCurrentTime() + "') " + HttpContext.Current.Session["userAccessParameters"] +
                           " ORDER BY Id DESC";
            return sqlOperation.getDataTable(query);
        }

    }
}
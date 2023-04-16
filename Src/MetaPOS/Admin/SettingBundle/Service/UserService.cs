using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.SettingBundle.Service
{
    public class UserService
    {
        SqlOperation sqlOperation = new SqlOperation();
        CommonFunction commonFunction = new CommonFunction();

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
        public Decimal version { get; set; }


        public DataTable GetUserData(string roleId)
        {
            var userModel = new UserModel();
            userModel.roleId = Convert.ToInt32(roleId);
            return userModel.GetUserDataModel();
        }


        public string UpdateUserData()
        {
            var userModel = new UserModel();
            userModel.title = title;
            userModel.email = email;
            userModel.password = password;
            userModel.branchLimit = branchLimit;
            userModel.userLimit = userLimit;
            userModel.expiryDate = expiryDate;
            userModel.accessPage = accessPage;
            userModel.userRight = userRight;
            userModel.subscriptionFee = subscriptionFee;
            userModel.roleId = roleId;
            userModel.branchId = branchId;
            userModel.groupId = groupId;
            userModel.storeId = storeId;
            userModel.entryDate = commonFunction.GetCurrentTime();
            userModel.updateDate = commonFunction.GetCurrentTime();
            UpdateSubscriptionInfo();
            return userModel.UpdateUserDataModel();
        }




        // subscriptionInfo for manual payment
        public void UpdateSubscriptionInfo()
        {
            var userModel = new UserModel();
            var query = string.Empty;
            var dtSubsInfo = userModel.getSubscribeCreatedInfo();
            if (dtSubsInfo.Rows[0]["cashin"].ToString() == "0.00" && dtSubsInfo.Rows[0]["status"].ToString() == "0")
            {
                if (expiryDate.Date > commonFunction.GetCurrentTime().Date.AddDays(27))
                {
                    userModel.roleId = Convert.ToInt32(roleId);
                    userModel.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
                    userModel.invoiceNo = commonFunction.GetCurrentTime().Ticks.ToString();
                    userModel.name = "Payment";
                    userModel.description = "";
                    userModel.paymentMode = "";
                    userModel.cashin = subscriptionFee;
                    userModel.cashout = 0M;
                    userModel.type = "Payment";
                    userModel.status = "1";
                    userModel.createDate = commonFunction.GetCurrentTime();
                    userModel.updateDate = commonFunction.GetCurrentTime();
                    userModel.saveSubscriptionModel();
                    query = "1";
                }
                //userModel.UpdateSubscriptionInfoDataModel();
                //else if (expiryDate < commonFunction.GetCurrentTime())
                //{
                //    userModel.UpdateSubscriptionInfoDataModel();
                //}
            }
            userModel.UpdateSubscriptionInfoDataModel(query);

            
        }





        public string SaveUserData()
        {
            var userModel = new UserModel();
            userModel.title = title;
            userModel.email = email;
            userModel.password = password;
            userModel.branchLimit = branchLimit;
            userModel.userLimit = userLimit;
            userModel.expiryDate = expiryDate;
            userModel.accessPage = accessPage;
            userModel.userRight = userRight;
            userModel.roleId = roleId;
            userModel.branchId = branchId;
            userModel.groupId = groupId;
            userModel.storeId = storeId;
            userModel.version = version;
            userModel.subscriptionFee = subscriptionFee;
            userModel.entryDate = commonFunction.GetCurrentTime();
            userModel.updateDate = commonFunction.GetCurrentTime();

            return userModel.SaveUserDataModel();
        }

    }
}
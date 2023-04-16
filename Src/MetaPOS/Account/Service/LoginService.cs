using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Account.Service
{
    public class LoginService
    {
        CommonFunction commonFunction = new CommonFunction();
        public string userRight { get; set; }
        public int roleId { get; set; }
        public int branchId { get; set; }
        public string email { get; set; }
        public DateTime loginDate { get; set; }


        public string SaveUserLogsData()
        {
            var userLogsModel = new UserLogsModel();
            userLogsModel.email = email;
            userLogsModel.userRight = userRight;
            userLogsModel.roleId = roleId;
            userLogsModel.branchId = branchId;
            userLogsModel.loginDate = commonFunction.GetCurrentTime();
            userLogsModel.ipAddress = getIpAddress();

            return userLogsModel.SaveUserLogsDataModel();
        }




        protected string getIpAddress()
        {
            string IpAddress = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDER_FOR"] != null)
            {
                IpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDER_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != null)
            {
                IpAddress = HttpContext.Current.Request.UserHostAddress;
            }
            return IpAddress;
        }
    }
}
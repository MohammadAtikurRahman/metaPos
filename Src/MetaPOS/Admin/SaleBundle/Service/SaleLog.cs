using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleLog
    {

        public bool SaveSaleLogData(string message,string name)
        {
            var logModel = new LogModel();
            logModel.name = name;
            logModel.description = message.Replace("\'","\"");
            logModel.roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"].ToString());
            logModel.branchId = Convert.ToInt32(HttpContext.Current.Session["branchId"].ToString());
            logModel.groupId = Convert.ToInt32(HttpContext.Current.Session["groupId"].ToString());
            logModel.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
            return logModel.SaveSaleLogDataModel();
        }
    }
}
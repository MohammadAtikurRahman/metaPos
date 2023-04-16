﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MetaPOS.Admin.Model;
using System.Web.Services;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.PromotionBundle.View
{
    public partial class EmailLog : System.Web.UI.Page
    {
        [WebMethod]
        public static string getEmailLogModelList()
        {
            var emailLogModel = new EmailLogModel();
            var dataList = emailLogModel.getEmailLogInfoListModel();

            CommonFunction commonFunction = new CommonFunction();
            return commonFunction.serializeDatatableToJson(dataList);
        }

        
    }
}
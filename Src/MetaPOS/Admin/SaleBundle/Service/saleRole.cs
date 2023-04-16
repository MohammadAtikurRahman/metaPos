using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleRole
    {


        public string getCurrentLoginUsername(string userId)
        {
            string username = "";
            SaleRoleModel saleRoleModel = new SaleRoleModel();
            DataTable dtRole = saleRoleModel.getCurrentLoginUsernameModel(userId);
            if (dtRole.Rows.Count > 0)
                username = dtRole.Rows[0]["title"].ToString();

            return username;
        }
    }
}
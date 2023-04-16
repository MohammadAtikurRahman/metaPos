using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using System.Data;


namespace MetaPOS.Admin.Model
{
    public class SaleRoleModel
    {

        CommonFunction commonFunction = new CommonFunction();
        SqlOperation sqlOperation = new SqlOperation();
        public DataTable getCurrentLoginUsernameModel(string userId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleID='" + userId + "'");
            return dt;
        }


        public DataTable getBranchInfoDataByBranchId(string storeId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM BranchInfo WHERE storeId='" + storeId + "'");
            return dt;
        }
    }
}
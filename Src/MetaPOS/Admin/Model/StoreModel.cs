using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class StoreModel
    {

        SqlOperation sqlOperation = new SqlOperation();
        CommonFunction commonFunction = new CommonFunction();
        public DataSet getStoreListModel(string roleId, string branchId)
        {
            return sqlOperation.getDataSet("SELECT * FROM WarehouseInfo WHERE active ='1' " + commonFunction.getBranchAllUserRoleId(roleId, branchId) + "");
        }


    }
}
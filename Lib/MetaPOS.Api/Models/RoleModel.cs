using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Common;


namespace MetaPOS.Api.Models
{
    public class RoleModel
    {
        //private  SqlOperationApi sqlOperationApi = new SqlOperationApi();
        public string shopname { get; set; }

        public DataTable getRoleDataModel(string roleId, string shopname)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;

            return
                sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleId + "' OR branchId='" + roleId +
                                          "'");
        }


        public DataTable getRoleIdByBranchId(string roleId, string shopname)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;

            string query = "SELECT * FROM RoleInfo WHERE roleId='" + roleId + "' OR branchId='" + roleId + "'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getStoreList(string roleList, string shopname)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;

            string query = "SELECT * FROM warehouseInfo WHERE active='1' " + roleList;
            return sqlOperation.getDataTable(query);
        }
    }
}

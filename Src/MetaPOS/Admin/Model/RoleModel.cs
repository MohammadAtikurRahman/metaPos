using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class RoleModel
    {
        SqlOperation sqlOperation = new SqlOperation();
        public List<ListItem> getRoleDataModel()
        {
            string query = "SELECT * FROM RoleInfo WHERE userRight='Branch' AND active='1' AND branchType='main'";
            var dtRole = sqlOperation.getDataTable(query);

            var RoleList = new List<ListItem>();

            foreach (DataRow row in dtRole.Rows)
            {
                RoleList.Add(new ListItem(row["title"].ToString(), row["roleID"].ToString()));
            }

            return RoleList;
        }

        internal DataTable getRoleDataModelByUrl(string url)
        {
            return sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE domainName='" + url + "'");
        }





        public DataTable getRoleDataModelByRoleId(string roleId)
        {
            string query = "SELECT * FROM RoleInfo WHERE roleId='" + roleId + "'";
            return sqlOperation.getDataTable(query);
        }



        internal DataTable getRoleDataModelForBranchCreate(string roleId)
        {
            string query = "SELECT * FROM RoleInfo WHERE groupId='" + roleId + "' and branchId ='0'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getRoleDataModelForUserCreate(string roleId)
        {
            string query = "SELECT * FROM RoleInfo WHERE groupId='" + roleId + "' AND branchId !='0'";
            return sqlOperation.getDataTable(query);
        }

        public List<ListItem> getSubBranchDataListModel(string mainBranchId)
        {
            string query = "SELECT * FROM RoleInfo WHERE  inheritId='" + mainBranchId+ "' AND userRight='Branch'";
            var dtRole = sqlOperation.getDataTable(query);

            var RoleList = new List<ListItem>();

            foreach (DataRow row in dtRole.Rows)
            {
                RoleList.Add(new ListItem(row["title"].ToString(), row["roleID"].ToString()));
            }

            return RoleList;
        }

        public DataTable getBranchAllUserRoleIdModel(string roleId, string branchId)
        {
            return sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleId + "' OR branchId='" + branchId + "'");
        }


        public bool updateRoleInfoForPayment(DateTime activeDate, DateTime expiryDate, string branchId)
        {
            return sqlOperation.fireQuery("UPDATE RoleInfo SET activeDate='" + activeDate + "',expiryDate='" + expiryDate +
                                   "' WHERE roleId= '" + branchId + "'");
        }
    }
}
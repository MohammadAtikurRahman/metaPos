using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class LogModel
    {
        SqlOperation sqlOperation = new SqlOperation();
        CommonFunction commonFunction = new CommonFunction();

        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int roleId { get; set; }
        public int branchId { get; set; }
        public int groupId { get; set; }
        public int storeId { get; set; }


        public bool SaveSaleLogDataModel()
        {
            string query =
                "INSERT INTO LogInfo (name,description,roleId,branchId,groupId,storeId,createDate) VALUES ('" + name +
                "','" + description + "','" + roleId + "','" + branchId + "','" + groupId + "','" + storeId + "','" +
                commonFunction.GetCurrentTime() + "')";

            return sqlOperation.fireQuery(query);
        }
    }
}
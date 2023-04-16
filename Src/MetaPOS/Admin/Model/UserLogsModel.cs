using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using MetaPOS.Admin.DataAccess;
using System.Data;

namespace MetaPOS.Admin.Model
{
    public class UserLogsModel
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public string userRight { get; set; }
        public int roleId { get; set; }
        public int branchId { get; set; }
        public string email { get; set; }
        public DateTime loginDate { get; set; }
        public string ipAddress { get; set; }




      
        public string SaveUserLogsDataModel()
        {

            daleteUserLogsDataModel(roleId);
            string query = "INSERT INTO UserLogsInfo (roleId, branchId, userRight, email, ipAddress, loginDate) VALUES ('" + roleId + "','" + branchId + "','" + userRight + "','" + email + "','" + ipAddress + "','" + loginDate + "')";
            return sqlOperation.executeQueryWithoutAuth(query);

           


            //var dtRole = sqlOperation.getDataTable("SELECT * FROM [UserLogsInfo] WHERE email = '" + email + "'");
            ////int userRoleId = Convert.ToInt32(dtRole.Rows[0]["roleID"].ToString());
            //foreach (DataRow row in dtRole.Rows)
            //{

            //}
            //var dbIpAddress = dtRole.Rows[0]["ipAddress"].ToString();
            //if (dbIpAddress!=ipAddress)
            //{
            //    try
            //    {
            //        string query = "INSERT INTO UserLogsInfo (roleId, branchId, userRight, email, ipAddress, loginDate) VALUES ('" + roleId + "','" + branchId + "','" + userRight + "','" + email + "','" + ipAddress + "','" + loginDate + "')";
            //        return sqlOperation.executeQueryWithoutAuth(query);
            //    }
            //    catch (Exception ex)
            //    {
            //        return "false|" + ex;
            //    }
            //}
            //daleteUserLogsData();
            //string updateQuery = "UPDATE UserLogsInfo SET loginDate='" + loginDate + "' WHERE ipAddress='" + ipAddress + "'";
            //return sqlOperation.executeQueryWithoutAuth(updateQuery);


        }





        public string daleteUserLogsDataModel(int roleId)
        {
            string deleteQuery = "DELETE FROM [UserLogsInfo] WHERE roleId=" + roleId + " AND [loginDate] < GETDATE()- 15";
            return sqlOperation.executeQueryWithoutAuth(deleteQuery);
        }

    }
}
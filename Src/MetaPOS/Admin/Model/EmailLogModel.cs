using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class EmailLogModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private string query;
        public string message { get; set; }
        public string emailRecord { get; set; }
        public string medium { get; set; }
        public decimal emailCost { get; set; }
        public DateTime sentAt { get; set; }
        public int roleId { get; set; }
        public string active { get; set; }

        public DataTable getEmailLogInfoListModel()
        {
            return
                sqlOperation.getDataTable(
                    "SELECT Id, message, emailCost, convert(varchar(25), sentAt, 120) AS sentAt, medium FROM EmailLogInfo");
        }




        public bool saveEmailLogInfoModel()
        {
            query = "INSERT INTO EmailLogInfo(message,emailRecord,medium,emailCost,sentAt,roleID,active) VALUES('" +
                    message + "','"
                    + emailRecord + "','"
                    + medium + "','"
                    + emailCost + "','"
                    + sentAt + "', '"
                    + roleId + "','"
                    + active + "')";

            return sqlOperation.fireQuery(query); 
        }



        public bool deleteEmailLogInfoModel()
        {
            return sqlOperation.fireQuery("");
        }
    }
}
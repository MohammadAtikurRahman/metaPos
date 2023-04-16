using System.Data;
using MetaPOS.Admin.DataAccess;
using System;


namespace MetaPOS.Admin.Model
{


    public class SmsLogModel
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private string query;

        public string message { get; set; }
        public string deliveryId { get; set; }
        public string phoneRecord { get; set; }
        public string medium { get; set; }
        public int msgCounter { get; set; }
        public decimal msgCost { get; set; }
        public DateTime sentAt { get; set; }
        public int roleId { get; set; }
        public string active { get; set; }





        public DataTable getSmsLogInfoListModel()
        {
            return
                sqlOperation.getDataTable(
                    "SELECT Id, message,phoneRecord, medium,msgCounter, msgCost, convert(varchar(25), sentAt, 120) AS sentAt FROM SmsLogInfo");
        }


        public DataTable getReSendSmsData(string id)
        {
            return
              sqlOperation.getDataTable(
                    "SELECT message,phoneRecord FROM SmsLogInfo WHERE Id='" + id + "'");
        }


        public bool saveSmsLogInfoModel()
        {
            query =
                "INSERT INTO SmsLogInfo(message,deliveryId,phoneRecord,medium,msgCounter,msgCost,sentAt,roleID,active) VALUES(N'" +
                message + "','"
                +deliveryId + "','"
                + phoneRecord + "','"
                + medium + "','"
                + msgCounter + "','"
                + msgCost + "','"
                + sentAt + "', '"
                + roleId + "','"
                + active + "')";
            return sqlOperation.fireQuery(query);
        }





        public bool delteSmsLogInfoModel()
        {
            return sqlOperation.fireQuery("");
        }


    }


}
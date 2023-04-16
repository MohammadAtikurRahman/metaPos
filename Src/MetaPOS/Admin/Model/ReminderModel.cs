using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class ReminderModel
    {
        SqlOperation sqlOperation = new SqlOperation();

        public DataTable getCustomerReminderDataListModel(string billNo)
        {
            return sqlOperation.getDataTable("SELECT * FROM CustomerReminderInfo where billNo='" + billNo + "' AND payStatus='0' ");
        }

        public string changePayStatusModel(string Id)
        {
            string query = "UPDATE CustomerReminderInfo SET payStatus='1' where Id='" + Id + "' ";
            return query;
        }

        public string UpdateCustomerRemainderPaidAmountModel(string Id, decimal payCash)
        {
            string queryCust = "UPDATE CustomerReminderInfo SET paidAmt=paidAmt + " + payCash + " where Id='" + Id + "'";
            return queryCust;
        }

        public string allChangePayStatusModel(string billNo)
        {
            string query = "UPDATE CustomerReminderInfo SET payStatus='1' WHERE billNo ='" + billNo + "' ";
            return query;
        }

        public DataTable getCustomerReminderDataListModelOrderByDesc(string billNo)
        {
            return sqlOperation.getDataTable("SELECT * FROM CustomerReminderInfo where billNo='" + billNo + "' AND payStatus='0' ORDER BY Id DESC");
        }
    }
}
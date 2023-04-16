using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class InstallmentModel
    {

        public CommonFunction CommonFunction = new CommonFunction();
        public SqlOperation SqlOperation = new SqlOperation();


        public string CustomerId { get; set; }
        public DateTime nextPayDate { get; set; }

        public string status { get; set; }

        public string entryDate { get; set; }

        public string actve { get; set; }

        public string billNo { get; set; }
        public int instalmentNumber { get; set; }
        public decimal downPayment { get; set; }
        public decimal paidAmt { get; set; }





        public DataTable getCustomerByCusIdModel(string cusId)
        {
            return SqlOperation.getDataTable("SELECT * FROM CustomerReminderInfo where customerId='" + cusId + "'");
        }

        public DataTable getCustomerReminderByBillNoModel(string billNO)
        {
            return SqlOperation.getDataTable("SELECT * FROM CustomerReminderInfo where billNo='" + billNO + "'");
        }

        public bool updateCustomerRemainderNextPayDate()
        {
            return
                SqlOperation.fireQuery("UPDATE CustomerReminderInfo SET nextPayDate='" + nextPayDate + "',updateDate='" +
                                       CommonFunction.GetCurrentTime() + "' where billNo='" + billNo + "'");
        }

        public string saveCustomerRemainderNextPayDate()
        {
            string query =
                " INSERT INTO CustomerReminderInfo(customerId,nextPayDate,status,entryDate,updateDate,roleId,active,billNo,instalmentNumber,downPayment,paidAmt) VALUES (" +
                "'" + CustomerId + "','" + nextPayDate + "','" + status + "','" + CommonFunction.GetCurrentTime() +
                "','" + CommonFunction.GetCurrentTime() + "','" + HttpContext.Current.Session["roleId"] + "','" +
                actve + "','" + billNo + "','" + instalmentNumber + "','" + downPayment + "','" + paidAmt + "') ";
            return query;
        }

        public string suspendInstallmentModel()
        {
            return "UPDATE CustomerReminderInfo SET active='0' WHERE billNo = '" + billNo + "'";
        }

        public DataTable getInstallmentByCustomer(string cusId)
        {
            string query = "SELECT SUM(downpayment)-SUM(paidAmt) as totalDue FROM CustomerReminderInfo WHERE customerId='" + cusId + "' AND payStatus='0'";
            return SqlOperation.getDataTable(query);
        }

        public DataTable getCustomerTotalDue(string cusId)
        {
            string query = "SELECT SUM(cashOut)-SUM(cashIn) FROM CashReportInfo where descr = '" + cusId + "' AND status='6' ";
            return SqlOperation.getDataTable(query);
        }
    }
}
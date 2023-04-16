using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class ProfitLossModel
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public DateTime searchFrom { get; set; }
        public DateTime searchTo { get; set; }

        public DataTable getProfitLossCalculationModel(string query)
        {
            return sqlOperation.getDataTable(query);
        }

        public DataTable getSupllierPaidAmountModel()
        {
            string query = "SELECT SUM(cashin) as balance FROM CashReportInfo WHERE status='0' and cashType='Supplier Payment' " +
                           HttpContext.Current.Session["userAccessParameters"] + "";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getSupllierPayableAmountModel()
        {
            string query = "SELECT SUM(stockTotal) as stockTotal FROM StockStatusInfo WHERE status='stock'  " +
                           HttpContext.Current.Session["userAccessParameters"] + "";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getRevenueAmountModel()
        {
            string query = "SELECT SUM(cashin) as balance FROM CashReportInfo WHERE status !='6' " +
                           HttpContext.Current.Session["userAccessParameters"] + "";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getExpenseAmountModel()
        {
            string query = "SELECT SUM(cashout) as balance FROM CashReportInfo WHERE status !='6' " +
                          HttpContext.Current.Session["userAccessParameters"] + "";
            return sqlOperation.getDataTable(query);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;

namespace MetaPOS.Api.Models
{
    public class SummaryModel : AccountSummary
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string storeAccessParameters { get; set; }
        public string shopname { get; set; }


        public DataTable getSalesProfitModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;

            string query = "SELECT DISTINCT BillNo, prodId,SUM((sPrice-bPrice)* CAST(qty as decimal)) as balance,min(commission) as commission, min(bPrice) as bPrice FROM StockStatusInfo where ((status='sale' AND isPackage='false') OR (status='salePackage' AND isPackage='true')) AND (entryDate >='" + startDate + "' AND entryDate <='" + endDate + "') AND BillNo !='' " + storeAccessParameters + " GROUP BY BillNo,prodId";
            return sqlOperation.getDataTable(query);
        }



        public DataTable getSalesDiscountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;

            string query = "SELECT distinct billNo, discAmt FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS DATE) >='" + startDate + "' AND CAST(entryDate AS DATE) <='" + endDate + "') " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }


        public DataTable getSalesReturnModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;

            string query = "SELECT DISTINCT BillNo,SUM((sPrice-bPrice)*CAST(qty as decimal)) as balance FROM StockStatusInfo where ((status='saleReturn' AND isPackage='false' AND searchType='product') OR (status='saleReturn' AND isPackage='true' AND searchType='salePackage') OR (status='saleReturn' AND isPackage='false' AND searchType='service')) AND(entryDate >='" + startDate + "' AND entryDate <='" + endDate + "')  AND BillNo !='' " + storeAccessParameters + " GROUP BY BillNo";
            return sqlOperation.getDataTable(query);
        }


        public DataTable getExpensiveModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;

            string query = "SELECT SUM(cashOut) as cashOut FROM CashReportInfo WHERE status='2' AND entryDate >= '" + startDate + "' AND entryDate <= '" + endDate + "' " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }
    }
}

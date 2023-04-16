using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Infrastructure.Data
{
    public class SummaryModel : SearchDto
    {
        SqlOperation sqlOperation = new SqlOperation();

        public DataTable SaleSummary()
        {
            string query = "select SUM(netAmt) as netAmt, SUM(discAmt) AS discAmt, SUM(grossAmt) as grossAmt, " +
                "SUM(balance) as balance, SUM(giftAmt) as giftAmt,SUM(loadingCost) as loadingCost,SUM(carryingCost) as carryingCost," +
                "SUM(unloadingCost) as unloadingCost,SUM(shippingCost) as shippingCost,SUM(serviceCharge) as serviceCharge FROM " +
                "(SELECT DISTINCT BillNo,netAmt,discAmt,grossAmt,balance,giftAmt,loadingCost,carryingCost,unloadingCost,shippingCost,serviceCharge FROM SaleInfo " +
                "WHERE status='1' AND (CAST(entryDate AS date) >= '" + From.ToShortDateString() + "' AND CAST(entryDate AS date) <= '" + To.ToShortDateString() + "') " + storeAccessParameter + ") as sale";

            return sqlOperation.getDataTable(query);
        }

        public DataTable CashSummary()
        {
            string query = "SELECT SUM(cashin)-SUM(cashout) as balance FROM CashReportInfo cashReport where cashReport.payMethod='" + PayMethod + "' AND cashReport.status='5' AND cashReport.cashType !='Discount' AND cashType !='Invoice' AND cashType !='Product Return' AND cashtype!='Suspended' AND " +
                "(CAST(cashReport.entryDate as Date) >= '" + From.ToShortDateString() + "' AND CAST(cashReport.entryDate as Date) <='" + To.ToShortDateString() + "') " + storeAccessParameter + "";

            return sqlOperation.getDataTable(query);
        }


        public DataTable AccountsSummary()
        {
            throw new NotImplementedException();
        }


        public DataTable SaleRecord()
        {
            string querySale =
                "SELECT SUM(grossAmt),(SUM(CAST(qty as decimal))-SUM(CAST(returnQty as decimal))) as qty FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS date) >= '" +
                From.ToShortDateString() + "' AND CAST(entryDate AS date) <= '" + To.AddDays(1).ToShortDateString() + "') " +
                storeAccessParameter + " GROUP BY billNo";

            return sqlOperation.getDataTable(querySale);
        }


        public DataTable SaleReturnRecord()
        {
            //string query = "SELECT DISTINCT BillNo,SUM((sPrice-bPrice)*CAST(qty as decimal)) as balance FROM StockStatusInfo where ((status='saleReturn' AND isPackage='false' AND searchType='product') OR (status='saleReturn' AND isPackage='true' AND searchType='salePackage') OR (status='saleReturn' AND isPackage='false' AND searchType='service')) AND(entryDate >='" + From + "' AND entryDate <='" + To + "')  AND BillNo !='' " + storeAccessParameter + " GROUP BY BillNo";
            string query = "select SUM(cashOut) as balance from CashReportInfo where cashType='Cash Return' AND status='6' AND(entryDate >='" + From + "' AND entryDate <='" + To + "')";
            return sqlOperation.getDataTable(query);
        }


        public DataTable CashReportSaleRecord()
        {
            string query = "SELECT SUM(cashIn) as cashIn, SUM(cashout) as cashout FROM CashReportInfo WHERE status='5' AND (entryDate BETWEEN '" + From.ToShortDateString() + "' AND '" + To.AddDays(1).ToShortDateString() + "') " + storeAccessParameter + "";
            return sqlOperation.getDataTable(query);
        }
    }
}

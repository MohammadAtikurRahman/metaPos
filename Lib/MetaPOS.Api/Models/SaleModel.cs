using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Common;


namespace MetaPOS.Api.Models
{
    public class SaleModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string storeAccessParameters { get; set; }
        public string shopName { get; set; }
        public string pCode { get; set; }

        //public SqlOperation sqlOperation = new SqlOperation();

        public DataTable SaleAmountModel(DateTime startDate, DateTime endDate, string storeId)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string query = "select SUM(netAmt) as netAmt, SUM(discAmt) AS discAmt, SUM(grossAmt) as grossAmt, SUM(balance) as balance, SUM(giftAmt) as giftAmt,SUM(loadingCost) as loadingCost,SUM(carryingCost) as carryingCost,SUM(unloadingCost) as unloadingCost,SUM(shippingCost) as shippingCost,SUM(serviceCharge) as serviceCharge FROM (SELECT DISTINCT BillNo,netAmt,discAmt,grossAmt,balance,giftAmt,loadingCost,carryingCost,unloadingCost,shippingCost,serviceCharge FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS date) >= '" +
                startDate.ToShortDateString() + "' AND CAST(entryDate AS date) <= '" + endDate.ToShortDateString() + "') AND storeId='" + storeId + "' ) as sale";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getStoreId(string roleId)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleId + "'");
        }


        public DataTable getSaleRecordModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string querySale =
                "SELECT SUM(grossAmt),(SUM(CAST(qty as decimal))-SUM(CAST(returnQty as decimal))) as qty FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS date) >= '" +
                startDate.ToShortDateString() + "' AND CAST(entryDate AS date) <= '" + endDate.AddDays(1).ToShortDateString() + "') " +
                storeAccessParameters + " GROUP BY billNo";
            return sqlOperation.getDataTable(querySale);
        }




        public DataTable SaleProfitModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("select  MIN(sPrice) as salePrice,MIN(bPrice) as buyPrice,MIN(qty) as qty from StockStatusInfo where status='sale' AND CAST(entryDate  as date) >= '" + startDate.ToShortDateString() + "' AND CAST(entryDate as date) <='" + endDate.ToShortDateString() + "' " + storeAccessParameters + "  group by  billNo,prodID");
        }




        public DataTable CommissionAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string query = "SELECT DISTINCT BillNo,min(commission) as commission, min(bPrice) as bPrice,min(qty) as qty FROM StockStatusInfo where ((status='sale' AND isPackage='false') OR (status='salePackage' AND isPackage='true')) AND (entryDate >='" + startDate + "' AND entryDate <='" + endDate + "') AND BillNo !='' " + storeAccessParameters + " GROUP BY BillNo,prodId";
            return sqlOperation.getDataTable(query);
        }


        public DataTable DiscountAmountModel()
        {
            var sqlOperation = new SqlOperation();
            string query = "SELECT distinct billNo, discAmt FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS DATE) >='" + startDate + "' AND CAST(entryDate AS DATE) <='" + endDate + "') " + storeAccessParameters + " ";
            
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable(query);
        }


        public DataTable ReturnAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string query = "SELECT DISTINCT BillNo,SUM((sPrice-bPrice)*CAST(qty as decimal)) as balance FROM StockStatusInfo where ((status='saleReturn' AND isPackage='false' AND searchType='product') OR (status='saleReturn' AND isPackage='true' AND searchType='salePackage') OR (status='saleReturn' AND isPackage='false' AND searchType='service')) AND(entryDate >='" + startDate + "' AND entryDate <='" + endDate + "')  AND BillNo !='' " + storeAccessParameters + " GROUP BY BillNo";
            return sqlOperation.getDataTable(query);
        }

        public DataTable MiscCostAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT SUM(loadingCost) as loadingCost,SUM(carryingCost) as carryingCost,SUM(unloadingCost) as unloadingCost,SUM(shippingCost) as shippingCost,SUM(serviceCharge) as serviceCharge FROM SaleInfo WHERE status='1' AND CAST(entryDate  as date) >= '" + startDate.ToShortDateString() + "' AND CAST(entryDate as date) <='" + endDate.ToShortDateString() + "' " + storeAccessParameters);
        }


        public DataTable SupplierRecivedAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string query = "SELECT SUM(cashout) as cashout FROM CashReportInfo WHERE status='0' AND entryDate >= '" + startDate + "' AND entryDate <= '" + endDate + "' " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }


        public DataTable ExpenceAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
             string query = "SELECT SUM(cashOut) as cashOut FROM CashReportInfo WHERE status='2' AND entryDate >= '" + startDate + "' AND entryDate <= '" + endDate + "' " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }

        public DataTable SalesReceivedAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT SUM(cashIn) as cashIn FROM CashReportInfo WHERE status='5' AND (entryDate BETWEEN '" + startDate.ToShortDateString() + "' AND '" + endDate.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }

        public DataTable ReceivedAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT SUM(cashIn) as cashIn FROM CashReportInfo WHERE status='3' AND (entryDate BETWEEN '" + startDate.ToShortDateString() + "' AND '" + endDate.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }


        public DataTable SalaryAmountModel()
        {
            var sqlOperation = new SqlOperation();
            return sqlOperation.getDataTable("select SUM(cashOut) as totalSalary from cashReportInfo where status='1' AND (entryDate BETWEEN '" + startDate.ToShortDateString() + "' AND '" + endDate.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }

        public DataTable SaleNetAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT SUM(netAmt) as totalNetAmount FROM SaleInfo WHERE status='1' AND (entryDate BETWEEN '" + startDate.ToShortDateString() + "' AND '" + endDate.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }



        public DataTable SaleQtyModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string querySale =
                "SELECT (SUM(CAST(qty as decimal))-SUM(CAST(returnQty as decimal))) as qty FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS date) >= '" +
                startDate.ToShortDateString() + "' AND CAST(entryDate AS date) <= '" + endDate.ToShortDateString() + "') " +
                storeAccessParameters + " GROUP BY billNo";

            return sqlOperation.getDataTable(querySale);
        }



        public DataTable SaleGrossAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string query ="select SUM(grossAmt) as grossAmt FROM SaleInfo WHERE status='1' AND CAST(entryDate AS date) >= '" + startDate.ToShortDateString() + "' AND CAST(entryDate AS date) <= '" + endDate.ToShortDateString() +"' " + storeAccessParameters;

            return sqlOperation.getDataTable(query);
        }






        public DataTable getCashReportByPayMethodModel(DateTime startDate, DateTime endDate, string storeAccessParameters, int payMethod)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            string query = "SELECT SUM(cashin)-SUM(cashout) as balance FROM CashReportInfo cashReport where cashReport.payMethod='" + payMethod + "' AND cashReport.status='5' AND cashReport.cashType !='Discount' AND cashType !='Invoice' AND cashType !='Product Return' AND cashtype!='Suspended' AND (CAST(cashReport.entryDate as Date) BETWEEN '" +
               startDate.ToShortDateString() + "' AND '" + endDate.AddDays(1).ToShortDateString() + "') " +
               storeAccessParameters + "";

            return sqlOperation.getDataTable(query);
        }

        public DataTable DueAmountModel()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT SUM(giftAmt) as totalDueAmt FROM SaleInfo WHERE (entryDate BETWEEN '" + startDate.ToShortDateString() + "' AND '" + endDate.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }


        public DataTable SaleData()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT * FROM SaleInfo WHERE (entryDate BETWEEN '" + startDate.ToShortDateString() + "' AND '" + endDate.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }


        //here
        public DataTable SaleAmountTotalQtyModel(string prodID)
        {
            var sqlOperation = new SqlOperation();
            //sqlOperation.conString = shopName;
            string query = "SELECT SUM(grossAmt) as totalAmt,(SUM(CAST(qty as decimal))-SUM(CAST(returnQty as decimal))) as totalQty FROM SaleInfo WHERE status='1' and prodID = '" +
                prodID + "'";
            return sqlOperation.getDataTable(query);
        }


    }
}

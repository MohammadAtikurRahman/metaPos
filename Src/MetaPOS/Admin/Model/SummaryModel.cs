using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class SummaryModel
    {


        public DateTime searchFrom { get; set; }
        public DateTime searchTo { get; set; }

        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();





        public DataTable cashReportSummeryModel()
        {
            string query = "";
            DataTable dt = sqlOperation.getDataTable(query);
            return dt;
        }





        public DataTable getSaleSummaryDataModel()
        {
            string query = "SELECT " +
                           "distinct stockStatus.prodId,stockStatus.bPrice, stockStatus.sPrice, " +
                           "tbl.qty,tbl.balance, tbl.giftAmt, tbl.discAmt, tbl.netAmt, tbl.grossAmt, " +
                           "stock.unitId " +
                           "FROM SaleInfo as tbl " +
                           "JOIN StockStatusInfo as stockStatus ON tbl.prodID=stockStatus.prodID " +
                           "LEFT JOIN StockInfo as stock ON stockStatus.prodId = stock.prodId " +
                           "WHERE tbl.Status != '0' AND isPackage !='1' AND tbl.BillNo = stockStatus.BillNo " +
                           "AND CAST(tbl.entryDate AS date) BETWEEN '" + searchFrom.ToShortDateString() + "' " +
                           "AND '" + searchTo.ToShortDateString() + "'" + commonFunction.getUserAccessParameters("tbl");

            return sqlOperation.getDataTable(query);
        }





        public DataTable getDistinctBillNo()
        {
            string query = "SELECT DISTINCT BillNo,balance, giftAmt, discAmt, netAmt, grossAmt FROM SaleInfo WHERE status !='0' AND " +
                " CAST(entryDate AS date) BETWEEN '" + searchFrom.ToShortDateString() + "' " +
                " AND '" + searchTo.ToShortDateString() + "'" + HttpContext.Current.Session["userAccessParameters"];

            return sqlOperation.getDataTable(query);

        }


        public DataTable getPackageDataSummaryModel()
        {
            string query = "SELECT " +
                           "distinct stockStatus.prodId,stockStatus.bPrice, stockStatus.sPrice, " +
                           "tbl.qty,tbl.balance, tbl.giftAmt, tbl.discAmt, tbl.netAmt, tbl.grossAmt " +
                           "FROM SaleInfo as tbl " +
                           "JOIN StockStatusInfo as stockStatus ON tbl.prodID=stockStatus.prodID " +
                           "LEFT JOIN PackageInfo as pack ON stockStatus.prodId = pack.Id " +
                           "WHERE tbl.Status != '0' AND isPackage ='1' AND tbl.BillNo = stockStatus.BillNo " +
                           "AND CAST(tbl.entryDate AS date) BETWEEN '" + searchFrom.ToShortDateString() + "' " +
                           "AND '" + searchTo.ToShortDateString() + "'" + commonFunction.getUserAccessParameters("tbl");

            return sqlOperation.getDataTable(query);
        }
    }
}
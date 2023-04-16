using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{

    public class SupplierCommisionModel
    {

        public string category { get; set; }
        public DateTime datetFrom { get; set; }
        public DateTime dateTo { get; set; }
        public string prodId { get; set; }
        public string status { get; set; }
        public string storeId { get; set; }
        public string userId { get; set; }

        public DateTime To { get; set; }
        public DateTime From { get; set; }

        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();


        public SupplierCommisionModel()
        {
            if (datetFrom.ToString() == "")
                datetFrom = commonFunction.GetCurrentTime();
            if (dateTo.ToString() == "")
                dateTo = commonFunction.GetCurrentTime();
        }

        public DataTable getSupplierCommisionModel(string supId)
        {
            return sqlOperation.getDataTable("SELECT * FROM SupplierInfo where supId='" + supId + "'");
        }


        public string getSupplierCommissionReportModel()
        {
            string query = "";//, whereCondition = "";

            query = "SELECT stockStatus.Id,stockStatus.prodID as prodId,stockStatus.prodName as itemName,stockStatus.prodCode, stockStatus.status,stockStatus.qty," +
                    "stockStatus.sPrice,stockStatus.searchType,stockStatus.entryDate,stockStatus.deliveryStatus," +
                    "store.name as storeName,stockStatus.billNo as details,stockStatus.attributeRecord,stockStatus.lastQty," +
                    "stockStatus.balanceQty,stockStatus.commissionAmt,stockstatus.commission FROM StockStatusInfo as stockStatus " +
                    "LEFT JOIN WarehouseInfo as store ON store.Id=stockstatus.storeId " +
                    "where (stockStatus.searchType = 'commission' OR stockStatus.searchType = 'product') " +
                    "AND (stockStatus.status='supplierCommission' OR stockStatus.status='stock') " +
                    "AND (stockStatus.storeId='" + storeId + "' OR '" + storeId + "'='0' ) " +
                    "AND (stockStatus.roleId='" + userId + "' OR '" + userId + "'='0' ) " +
                    "AND stockstatus.entryDate BETWEEN '" + datetFrom.ToShortDateString() + "' " +
                    "AND '" + dateTo.AddDays(1).ToShortDateString() + "' " +
                    "AND (stockStatus.catName='" + category + "' OR '" + category + "' = '0') " +
                    "AND (stockstatus.commission !='0') " +
                    commonFunction.getStoreAccessParameters("stockStatus") + commonFunction.getUserAccessParameters("stockStatus");

            var dtInventory = sqlOperation.getDataTable(query);
            return commonFunction.serializeDatatableToJson(dtInventory);
        }

        public DataTable getSupplierCommissionForSummary()
        {
            string query = "SELECT SUM(commissionAmt) as totalSupCommissionAmt FROM StockStatusInfo where (storeId='" + storeId + "' OR '"+storeId+"'='0') AND entryDate >= '" + From +
                           "' AND entryDate <= '" + To + "' AND status ='SupplierCommission' ";
            return sqlOperation.getDataTable(query);
        }
    }
}
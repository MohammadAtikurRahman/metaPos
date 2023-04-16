using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class InventoryModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();


        public string searchType { get; set; }
        public string category { get; set; }
        public DateTime datetFrom { get; set; }
        public DateTime dateTo { get; set; }
        public string prodId { get; set; }
        public string status { get; set; }
        public string storeId { get; set; }
        public string userId { get; set; }
        public string buyPrice { get; set; }




        public InventoryModel()
        {
            if (datetFrom.ToString() == "")
                datetFrom = commonFunction.GetCurrentTime();
            if (dateTo.ToString() == "")
                dateTo = commonFunction.GetCurrentTime();
        }

        public string getInventoryReportModel()
        {
            string query = "", whereCondition = "";

            if (searchType == "product")
            {
              

                //query = "SELECT sale.Id,sale.qty, sale.grossAmt,sale.sPrice,sale.searchType,sale.entryDate,stock.prodName as itemName FROM SaleInfo as sale LEFT JOIN StockInfo as stock ON sale.prodID = stock.prodID where sale.searchType = '" + searchType + "' AND CONVERT(date, sale.entryDate) >= '" + datetFrom.ToShortDateString() + "' AND CONVERT(date, sale.entryDate) <= '" + dateTo.ToShortDateString() + "' " + whereCondition + " " + commonFunction.getUserAccessParameters("sale");

                query = "SELECT stockStatus.Id,stockStatus.prodID as prodId,stockStatus.prodName as itemName,stockStatus.prodCode, stockStatus.status,stockStatus.qty,stockStatus.sPrice,stockStatus.searchType,stockStatus.entryDate,stockStatus.deliveryStatus,store.name as storeName,stockStatus.billNo as details,stockStatus.attributeRecord,stockStatus.lastQty,stockStatus.balanceQty as balanceQty ,stockStatus.bPrice as bPrice,(balanceQty*bPrice) as inventoryAmount  FROM StockStatusInfo as stockStatus " +
                        "LEFT JOIN WarehouseInfo as store ON store.Id=stockstatus.storeId " +
                        "where stockStatus.searchType = '" + searchType + "' " +
                        "AND (stockStatus.status='" + status + "' OR '" + status + "'='') " +
                        "AND (stockStatus.storeId='" + storeId + "' OR '" + storeId + "'='0' ) " +
                        "AND (stockStatus.roleId='" + userId + "' OR '" + userId + "'='0' OR stockStatus.status ='stockReceive' OR stockStatus.status ='stockTransfer' OR stockStatus.status='damage') " +
                        "AND stockstatus.entryDate BETWEEN '" + datetFrom.ToShortDateString() + "' " +
                        "AND '" + dateTo.AddDays(1).ToShortDateString() + "' " +
                        "AND (stockStatus.catName='" + category + "' OR '" + category + "' = '0') " +
                        "AND (stockStatus.prodId='" + prodId + "' OR '" + prodId + "' = '') " +
                        "AND (stockStatus.qty !='0' OR stockStatus.status ='stockPending') " +
                        commonFunction.getStoreAccessParameters("stockStatus") + commonFunction.getUserAccessParameters("stockStatus");

            }
            else if (searchType == "salePackage")
            {

                query = "SELECT stockStatus.Id,stockStatus.prodID as prodId,stockStatus.prodName as itemName,stockStatus.status,stockStatus.qty,stockStatus.sPrice,stockStatus.searchType,stockStatus.entryDate,store.name as storeName FROM StockStatusInfo as stockStatus LEFT JOIN WarehouseInfo as store ON store.Id=stockstatus.storeId where stockStatus.searchType = '" + searchType + "' AND (stockStatus.status='" + status + "' OR '" + status + "'='') AND stockstatus.entryDate BETWEEN '" + datetFrom.ToShortDateString() + "' AND '" + dateTo.AddDays(1).ToShortDateString() + "' " + whereCondition + "" + commonFunction.getUserAccessParameters("stockStatus");
            }
            else if (searchType == "service")
            {
                if (category != null && category != "0")
                    whereCondition = " AND service.type = '" + category + "'";
                if (prodId != "")
                    whereCondition += " AND stockStatus.prodID = '" + prodId + "' ";

                query = "SELECT stockStatus.Id,stockStatus.prodID as prodId,stockStatus.prodName as itemName,stockStatus.status,stockStatus.qty,stockStatus.sPrice,stockStatus.searchType,stockStatus.entryDate, store.name as storeName FROM StockStatusInfo as stockStatus LEFT JOIN WarehouseInfo as store ON store.Id=stockstatus.storeId where stockStatus.searchType = '" + searchType + "' AND (stockStatus.status='" + status + "' OR '" + status + "'='') AND stockstatus.entryDate BETWEEN '" + datetFrom.ToShortDateString() + "' AND '" + dateTo.AddDays(1).ToShortDateString() + "' " + whereCondition + "" + commonFunction.getUserAccessParameters("stockStatus");
            }

            var dtInventory = sqlOperation.getDataTable(query);
            return commonFunction.serializeDatatableToJson(dtInventory);
        }

        public bool changeStatusModel(string deliveryId)
        {
            return sqlOperation.fireQuery("UPDATE StockStatusInfo SET status='stockReceive' WHERE Id='" + deliveryId + "'");
        }

        public bool changeDeliveryStatusModel(string deliveryId)
        {
            return sqlOperation.fireQuery("UPDATE StockStatusInfo SET deliveryStatus='1' WHERE Id='" + deliveryId + "'");
        }
    }
}
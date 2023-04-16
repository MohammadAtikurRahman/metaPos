using System;
using System.Data;
using System.Net.Http;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.RecordBundle.View;


namespace MetaPOS.Admin.Model
{


    public class StockModel : eCommerceModel
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        // Global Variable
        private string query = "";
        private DataSet ds;

        // Initialize Database perameter
        public int prodId { get; set; }
        public string prodCode { get; set; }
        public string prodName { get; set; }
        public string prodDescr { get; set; }
        public string supCompany { get; set; }
        public string catName { get; set; }
        public string qty { get; set; }
        public decimal bPrice { get; set; }
        public decimal sPrice { get; set; }
        public decimal weight { get; set; }
        public string size { get; set; }
        public decimal discount { get; set; }
        public decimal stockTotal { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }
        public string entryQty { get; set; }
        public string title { get; set; }
        public string branchName { get; set; }
        public string groupId { get; set; }
        public string roleId { get; set; }
        public string branchId { get; set; }
        public string fieldAttribute { get; set; }
        public string tax { get; set; }
        public string sku { get; set; }
        public string lastQty { get; set; }
        public string warranty { get; set; }
        public string imei { get; set; }
        public string warningQty { get; set; }
        public decimal dealerPrice { get; set; }
        public string purchaseCode { get; set; }
        public string createdFor { get; set; }
        public DateTime receivedDate { get; set; }
        public DateTime expiryDate { get; set; }
        public string batchNo { get; set; }
        public string serialNo { get; set; }
        public int ShipmentStatus { get; set; }
        public int manufacturerId { get; set; }
        public string Status { get; set; }
        public string productSource { get; set; }
        public int fieldId { get; set; }
        public int attributeId { get; set; }
        public string billNo { get; set; }
        public string notes { get; set; }
        public int unitId { get; set; }
        public int storeId { get; set; }
        public decimal SupCommission { get; set; }
        public DateTime purchaseDate { get; set; }
        public char imeiStatus { get; set; }
        public string engineNumber { get; set; }
        public string cecishNumber { get; set; }
        public decimal comPrice { get; set; }
        public int locationId { get; set; }
        public string freeQty { get; set; }
        public string fieldRecord { get; set; }
        public string attributeRecord { get; set; }
        public string parentId { get; set; }
        public bool isChild { get; set; }
        public string isParent { get; set; }
        public string balanceQty { get; set; }
        public string searchType { get; set; }
        public bool isSuspend { get; set; }
        public string referredBy { get; set; }
        public string salesPersonId { get; set; }
        public string offer { get; set; }

        public string offerType { get; set; }

        public string deliveryStatus { get; set; }

        public bool isOfferQty { get; set; }
        public decimal commissionAmt { get; set; }
        public string pCode { get; set; }


        public StockModel()
        {
            try
            {
                Status = "Purchase";
                isParent = "0";
                isSuspend = false;
                referredBy = "0";
                salesPersonId = "0";
                offer = "0";
                offerType = "";
                deliveryStatus = "0";
                warningQty = "0";
                commissionAmt = 0;
                isOfferQty = false;
                roleId = HttpContext.Current.Session["roleId"].ToString();
                groupId = HttpContext.Current.Session["groupId"].ToString();
                branchId = HttpContext.Current.Session["branchId"].ToString();
            }
            catch (Exception)
            {

            }

        }






        public bool createStock()
        {
            query =
                "INSERT INTO StockInfo (" +
                "prodId," +
                "prodCode," +
                "prodName," +
                "prodDescr," +
                "supCompany," +
                "catName," +
                "qty," +
                "bPrice," +
                "sPrice," +
                "weight," +
                "size," +
                "discount," +
                "stockTotal," +
                "entryDate," +
                "entryQty," +
                "title," +
                "branchName," +
                "roleId," +
                "branchId," +
                "groupId," +
                "fieldAttribute," +
                "tax," +
                "sku," +
                "lastQty," +
                "warranty," +
                "imei," +
                "warningQty," +
                "dealerPrice," +
                "createdFor," +
                "receivedDate," +
                "expiryDate," +
                "batchNo," +
                "serialNo," +
                "shipmentStatus," +
                "manufacturerId," +
                "notes," +
                "unitId," +
                "warehouse," +
                "commission," +
                "purchaseDate," +
                "imeiStatus," +
                "refId," +
                "engineNumber," +
                "cecishNumber," +
                "comPrice," +
                "locationId," +
                "fieldRecord," +
                "attributeRecord," +
                "parentId," +
                "isChild," +
                "isParent," +
                "pCode" +
                ") VALUES('" +
                prodId + "','" +
                prodCode + "',N'" +
                prodName + "',N'" +
                prodDescr + "','" +
                supCompany + "','" +
                catName + "','" +
                qty + "','" +
                bPrice + "','" +
                sPrice + "','" +
                weight + "','" +
                size + "','" +
                discount + "','" +
                stockTotal + "','" +
                entryDate + "','" +
                entryQty + "','" +
                title + "','" +
                branchName + "','" +
                roleId + "','" +
                branchId + "','" +
                groupId + "','" +
                fieldAttribute + "','" +
                tax + "','" +
                sku + "','" +
                lastQty + "','" +
                warranty + "','" +
                imei + "','" +
                warningQty + "','" +
                dealerPrice + "','" +
                createdFor + "','" +
                receivedDate + "','" +
                expiryDate + "','" +
                batchNo + "','" +
                serialNo + "','" +
                ShipmentStatus + "','" +
                manufacturerId + "',N'" +
                notes + "','" +
                unitId + "','" +
                storeId + "','" +
                SupCommission + "','" +
                purchaseDate + "','" +
                imeiStatus + "','','" +
                engineNumber + "','" +
                cecishNumber + "','" +
                comPrice + "','" +
                locationId + "','" +
                fieldRecord + "','" +
                attributeRecord + "','" +
                parentId + "','" +
                isChild + "','" +
                isParent + "','" +
                pCode + "')";
            return sqlOperation.fireQuery(query);
        }



        public string getStockListSeriliaze()
        {
            string queryStock = @"SET QUERY_GOVERNOR_COST_LIMIT 0; 
                                Select 
                                stock.prodId AS prodId,
                                MIN(stock.Id) AS Id, 
                                MIN(stock.prodName) AS name, 
                                MIN(stock.prodCode) AS code, 
                                MIN(stock.bPrice) AS buyPrice,
                                MIN(stock.sPrice) AS salePrice,
                                MIN(stock.dealerPrice) AS dealerPrice,
                                MIN(stock.comPrice) AS comPrice,
                                MIN(stock.supCompany) AS supId,
                                MIN(supplier.supCompany) AS supName,
                                MIN(stock.catName) AS catId,
                                MIN(category.catName) AS catName,
                                MIN(stock.imei) AS imei,
                                MIN(stock.active) as active,
                                MIN(stock.unitId) as unitId,
                                MIN(unit.unitName) as unitName,
                                MIN(stock.roleId) as roleId,
                                MIN(stock.branchId) as branchId,
                                MIN(stock.groupId) as groupId,
                                MIN(stock.attributeRecord) as attributeRecord,
                                MIN(qm.storeId) as storeId,
                                MIN(warehouse.name) as storeName,
                                MIN(stock.parentId) as parentId, MIN(qm.stockQty) as stockQty 
                                FROM StockInfo stock 
                                LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodId = stock.prodId 
                                LEFT JOIN qtyManagement as qm ON stock.prodId = qm.productId 
                                LEFT JOIN Ecommerce ecom ON stock.prodId = ecom.prodId 
                                LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = qm.storeId 
                                LEFT JOIN CategoryInfo as category ON category.Id= stock.catName 
                                LEFT JOIN SupplierInfo as supplier ON supplier.supId = stock.supCompany 
                                LEFT JOIN manufacturerInfo as manufacter ON manufacter.Id = stock.manufacturerId 
                                LEFT JOIN unitInfo as unit ON stock.unitId = unit.Id 
                                LEFT JOIN OfferInfo as offer ON stock.prodId=offer.prodId 
                                WHERE stock.prodId !='0' AND qm.storeId = '" + HttpContext.Current.Session["storeId"].ToString()
                                + "' GROUP BY stock.prodId  ,stockstatus.storeId,warehouse.Id  ORDER BY stock.prodId DESC";

            var dt = sqlOperation.getDataTable(queryStock); ;
            return commonFunction.serializeDatatableToJson(dt);

        }





        public bool createStockStatus()
        {
            query = "INSERT INTO StockStatusInfo(" +
                    "prodId,prodCode,prodName,prodDescr,supCompany,catName," +
                    "qty,bPrice,sPrice,weight,size," +
                    "discount,stockTotal,status,entryDate," +
                    "statusDate,entryQty,title,roleID,billNo," +
                    "branchId,groupId,fieldAttribute,tax," +
                    "sku,lastQty,productSource,prodCodes,imei," +
                    "fieldId,attributeId,commission,dealerPrice," +
                    "createdFor,unitId,isPackage," +
                    "engineNumber,cecishNumber,transceiverId,searchType,storeId,active," +
                    "offer,offerType,deliveryStatus,isOfferQty,purchaseCode," +
                    "freeQty,fieldRecord,attributeRecord," +
                    "parentId,isChild,balanceQty,isSuspend,referredBy,salesPersonId,commissionAmt) " +
                    "VALUES('" +
                    prodId + "','" + prodCode + "',N'" + prodName + "',N'" + prodDescr + "','" +
                    supCompany + "','" + catName + "','" + qty + "','" + bPrice + "','" + sPrice + "','" + weight + "','" + size + "','" +
                    discount + "','" + stockTotal + "','" + Status + "','" + entryDate + "','" + entryDate + "','" +
                    entryQty + "','" + title + "','" + roleId + "','" + billNo + "', '" +
                    branchId + "','" + groupId + "','" +
                    fieldAttribute + "','" + tax + "','" + sku + "','" + lastQty + "','" + productSource + "','" +
                    prodCode + "','" + imei + "','" + fieldId + "','" + attributeId + "','" + SupCommission + "','" + dealerPrice + "','" + createdFor + "','" +
                    unitId + "','0','" + engineNumber + "','" + cecishNumber + "','" + storeId + "','" + searchType + "','" +
                    storeId + "','" + active + "','" + offer + "','" + offerType + "','" + deliveryStatus + "','" +
                    isOfferQty + "','" + purchaseCode + "','" + freeQty + "','" + fieldRecord + "','" + attributeRecord + "','" + parentId + "','" + isChild
                    + "','" + balanceQty + "','" + isSuspend + "','" + referredBy + "','" + salesPersonId + "','" + commissionAmt + "')";
            return sqlOperation.fireQuery(query);
        }





        public void updateStock()
        {
            query = "UPDATE StockInfo SET prodName=N'" +
                    prodName + "', catName = '" + catName + "', qty =  '" + qty + "', bPrice = '" + bPrice +
                    "', sPrice ='" + sPrice + "', stockTotal = '" + stockTotal + "', entryQty= entryQty, " +
                    "fieldAttribute='" + fieldAttribute + "', sku = '" + sku + "',tax = '" + tax + "', lastQty = '" + lastQty + "', warranty = '" +
                    warranty + "',dealerPrice ='" + dealerPrice + "', imei='" + imei + "',receivedDate = '" +
                    receivedDate + "',expiryDate='" + expiryDate + "',batchNo = '" + batchNo + "', serialNo='" +
                    serialNo + "', shipmentStatus = '" + ShipmentStatus + "', manufacturerId = '" +
                    manufacturerId + "',size ='" + size + "',notes=N'" + notes + "',warningQty = '" + warningQty
                    + "', unitId = '" + unitId + "',warehouse='" + storeId + "',commission='" + SupCommission
                    + "',purchaseDate='" + purchaseDate + "', imeiStatus='" + imeiStatus + "',engineNumber='" +
                    engineNumber + "',cecishNumber='" + cecishNumber + "', comPrice='" + comPrice + "', locationId='" +
                    locationId + "' " +
                    "WHERE prodId ='" + prodId + "' ";

            sqlOperation.executeQuery(query);
        }





        public dynamic updateStock(string getFormatUpdateItemData, string getFormatedConditinalParameter)
        {
            query = "UPDATE StockInfo SET " + getFormatUpdateItemData + " WHERE " + getFormatedConditinalParameter + "";
            sqlOperation.executeQuery(query);
            return query;
        }



        public dynamic getStock(string prodCode, string createdFor)
        {
            query = "SELECT * FROM StockInfo WHERE prodCode ='" + prodCode + "' AND createdFor ='" + createdFor + "'";
            ds = sqlOperation.getDataSet(query);
            return ds;
        }



        public dynamic getStockConditinalParameter(string getConditinalParameter)
        {
            query = "SELECT * FROM StockInfo WHERE " + getConditinalParameter + "";
            ds = sqlOperation.getDataSet(query);
            return ds;
        }




        public DataSet listStock()
        {
            query = "SELECT * FROM StockInfo WHERE sku = '" + sku + "'";
            ds = sqlOperation.getDataSet(query);
            return ds;
        }



        public DataSet getproduct()
        {
            query = "SELECT * FROM StockInfo WHERE prodName = '" + prodName + "'";
            ds = sqlOperation.getDataSet(query);
            return ds;
        }




        public DataTable getSearchProductListModel(string searchVal)
        {
            searchVal = searchVal.Replace("\"", "");

            string query = "SELECT TOP 10 prodName as name, ProdId as code, attributeRecord FROM StockInfo WHERE active ='1' AND (prodName like N'%" +
                           searchVal.Trim() + "%') " +
                           HttpContext.Current.Session["userAccessParameters"] + " ORDER BY prodName ASC ";

            return sqlOperation.getDataTable(query);
        }





        public DataTable getStockDataTableModelByProdCode(string prodCode)
        {
            string query =
                "SELECT TOP 10 tbl.prodName as prodName, tbl.qty as qty, tbl.sPrice as sPrice, tbl.prodId as prodId, " +
                "cat.catName as catName, sup.supCompany as supcompany, tbl.bPrice as bPrice, tbl.commission as commission," +
                "tbl.dealerPrice as dealerPrice, tbl.unitId, tbl.engineNumber, tbl.cecishNumber," +
                "warehouse.name as warehouseName,location.name as locationName FROM stockInfo as tbl " +
                "LEFT JOIN CategoryInfo as cat ON tbl.catName= cat.Id " +
                "LEFT JOIN supplierInfo as sup ON tbl.supCompany = sup.supId " +
                "LEFT JOIN WarehouseInfo warehouse ON tbl.warehouse=warehouse.Id " +
                "LEFT JOIN LocationInfo location ON tbl.locationId=location.Id " +
                "WHERE tbl.prodCode = '" + prodCode + "'" + commonFunction.getUserAccessParameters("tbl");

            return sqlOperation.getDataTable(query);
        }





        public DataTable getStockDataListModelByProdID(string prodId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodId = '" + prodId + "'");
            return dt;
        }






        public string getSupCommissionModel(string prodCode)
        {
            string query = "SELECT commission FROM StockInfo WHERE ProdCode = '" + prodCode + "'";
            DataSet dsSupComm = sqlOperation.getDataSet(query);

            return dsSupComm.Tables[0].Rows[0][0].ToString();
        }





        public string getProductUnitRatioSerializeDataModel(string prodId)
        {
            string query = "SELECT unit.unitName, unit.unitRatio, unit.Id, stock.ProdId, stock.qty as qty,stock.attributeRecord  FROM UnitInfo as unit LEFT JOIN StockInfo as stock ON unit.Id = stock.unitId  WHERE stock.prodId = '" + prodId + "'";
            //string query = "SELECT * FROM UnitInfo WHERE Id = '" + prodId + "'";
            var dtUnitRatio = sqlOperation.getDataTable(query);

            return commonFunction.serializeDatatableToJson(dtUnitRatio);
        }





        public DataTable getProductRatioModelByProductId(string prodId)
        {
            string query = "SELECT unit.unitRatio,unit.unitName, unit.Id, stock.ProdId, stock.qty  FROM UnitInfo as unit LEFT JOIN StockInfo as stock ON unit.Id = stock.unitId  WHERE stock.prodId = '" +
                prodId + "'";

            return sqlOperation.getDataTable(query);

        }





        public DataTable getStockStatusDataListModel(string billNo, string prodId)
        {
            var unitId = "0";
            var dtUnit = sqlOperation.getDataTable("SELECT unitId FROM StockInfo WHERE ProdId='" + prodId + "'");
            if (dtUnit.Rows.Count > 0)
                unitId = dtUnit.Rows[0][0].ToString();

            var storeId = HttpContext.Current.Session["storeId"].ToString();
            string query =
                "SELECT distinct tbl.billNo, tbl.prodId as prodId, tbl.prodName as prodName, tbl.sPrice as sPrice, cat.catName as catName, sup.supCompany as supcompany, tbl.bPrice as bPrice, tbl.commission as commission,tbl.dealerPrice as dealerPrice,tbl.prodCode as prodCode, tbl.Id as stockStatusId,tbl.status, cus.advance as advance,tbl.unitId as unitId,tbl.prodCodes, tbl.engineNumber, tbl.cecishNumber,tbl.imei,tbl.storeId,tbl.offer,tbl.offerType,tbl.serialNo, unit.unitRatio, " +
                commonFunction.balanceQtyOperation(prodId, storeId, unitId) +
                " as stockQty FROM stockStatusInfo as tbl " +
                "LEFT JOIN StockInfo as stock ON tbl.prodId= stock.prodId " +
                "LEFT JOIN CategoryInfo as cat ON tbl.catName= cat.Id " +
                "LEFT JOIN supplierInfo as sup ON tbl.supCompany = sup.supId " +
                "LEFT JOIN SaleInfo as sale ON sale.billNo = tbl.billNo " +
                "LEFT JOIN CustomerInfo as cus ON sale.cusID= cus.cusID " +
                "LEFT JOIN UnitInfo as unit ON tbl.unitId = unit.Id " +
                "WHERE tbl.billNo = '" + billNo + "' AND tbl.isOfferQty='0' AND tbl.prodId='" + prodId + "' AND tbl.status='sale' AND isPackage='0' " + commonFunction.getUserAccessParameters("tbl");

            return sqlOperation.getDataTable(query);
        }



        public DataTable getStockStatusDataListByProductIdModel(string productId)
        {
            var unitId = "0";
            var dtUnit = sqlOperation.getDataTable("SELECT unitId FROM StockInfo WHERE ProdId='" + productId + "'");
            if (dtUnit.Rows.Count > 0)
                unitId = dtUnit.Rows[0][0].ToString();

            var storeId = HttpContext.Current.Session["storeId"].ToString();
            string query =
                "SELECT distinct tbl.billNo, tbl.prodId as prodId, tbl.prodName as prodName, tbl.sPrice as sPrice, cat.catName as catName, sup.supCompany as supcompany, tbl.bPrice as bPrice, tbl.commission as commission,tbl.dealerPrice as dealerPrice,tbl.prodCode as prodCode, tbl.Id as stockStatusId,tbl.status, cus.advance as advance,tbl.unitId as unitId,tbl.prodCodes, tbl.engineNumber, tbl.cecishNumber,tbl.imei,tbl.storeId,tbl.offer,tbl.offerType, unit.unitRatio, qm.stockQty as stockQty FROM stockStatusInfo as tbl " +
                "LEFT JOIN StockInfo as stock ON tbl.prodId= stock.prodId " +
                "LEFT JOIN qtyManagement as qm ON tbl.prodId= qm.productId " +
                "LEFT JOIN CategoryInfo as cat ON tbl.catName= cat.Id " +
                "LEFT JOIN supplierInfo as sup ON tbl.supCompany = sup.supId " +
                "LEFT JOIN SaleInfo as sale ON sale.billNo = tbl.billNo " +
                "LEFT JOIN CustomerInfo as cus ON sale.cusID= cus.cusID " +
                "LEFT JOIN UnitInfo as unit ON tbl.unitId = unit.Id " +
                "WHERE tbl.prodId='" + productId + "' " + commonFunction.getUserAccessParameters("tbl");

            return sqlOperation.getDataTable(query);
        }


        public DataTable getStockStatusDataListModel(string billNo)
        {
            string query = "SELECT distinct tbl.billNo, tbl.prodId as prodId, tbl.prodName as prodName, tbl.qty as qty, tbl.sPrice as sPrice,tbl.searchType, cat.catName as catName, sup.supCompany as supcompany, tbl.bPrice as bPrice, tbl.commission as commission,tbl.dealerPrice as dealerPrice,tbl.prodCode as prodCode, tbl.Id as stockStatusId,tbl.status, cus.advance as advance,tbl.unitId as unitId,tbl.prodCodes,tbl.branchId,tbl.roleId,tbl.offer,tbl.offerType FROM stockStatusInfo as tbl " +
                "LEFT JOIN CategoryInfo as cat ON tbl.catName= cat.Id " +
                "LEFT JOIN supplierInfo as sup ON tbl.supCompany = sup.supId " +
                "LEFT JOIN SaleInfo as sale ON sale.billNo = tbl.billNo " +
                "LEFT JOIN CustomerInfo as cus ON sale.cusID= cus.cusID " +
                "WHERE tbl.billNo = '" + billNo + "' AND isOfferQty='0' AND ((tbl.status='sale' AND isPackage='0') OR tbl.status='salePackage') " + commonFunction.getUserAccessParameters("tbl");

            return sqlOperation.getDataTable(query);
        }





        public string getItemStockDataListSerializeModel(string prodId)
        {
            DataTable dtStockData = sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodId='" + prodId + "'");
            return commonFunction.serializeDatatableToJson(dtStockData);
        }



        public DataTable getItemStockDataListModelByProdID(string prodId)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE CAST(prodId as NVARCHAR)='" + prodId + "'");
        }

        public DataTable getItemStockDataListModelByProdCode(string prodCode)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodCode='" + prodCode + "'");
        }

        public bool isPurchaseCodeExistModel(string purchaseCode)
        {
            bool isExist = false;
            DataTable dt = sqlOperation.getDataTable("SELECT purchaseCode FROM [StockStatusInfo] WHERE purchaseCode = '" + purchaseCode + "'");
            if (dt.Rows.Count > 0)
                isExist = true;
            return isExist;
        }



        public string getProductDataListAddToCartSerializeModelByProdCode(string productId)
        {
            var unitId = "0";
            var dtUnit = sqlOperation.getDataTable("SELECT unitId FROM StockInfo WHERE prodId='" + productId + "'");
            if (dtUnit.Rows.Count > 0)
                unitId = dtUnit.Rows[0][0].ToString();

            string queryAddprod = @"SELECT DISTINCT stock.prodId,stock.prodCode,stock.prodName,stock.bPrice,stock.sPrice,stock.comPrice,unit.unitRatio,stock.fieldRecord,stock.attributeRecord,
                                    stock.dealerPrice,stock.entryDate,ecom.image, category.catName, supplier.supCompany, manufacter.manufacturerName,stock.Id,stockstatus.storeId,
                                    warehouse.name as storeName,stock.unitId,stock.commission,stock.engineNumber,stock.cecishNumber,stock.imei,location.name as locationName,offer.offerType,offer.offerValue,offer.discountType,offer.discountValue, qm.stockQty as stockQty FROM StockInfo as stock 
                                LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodId = stock.prodId
                                LEFT JOIN Ecommerce ecom ON stock.prodId = ecom.prodId 
                                LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = stockstatus.storeId 
                                LEFT JOIN CategoryInfo as category ON category.Id= stock.catName 
                                LEFT JOIN SupplierInfo as supplier ON supplier.supId = stock.supCompany 
                                LEFT JOIN manufacturerInfo as manufacter ON manufacter.Id = stock.manufacturerId
                                LEFT JOIN LocationInfo as location ON location.Id = stock.locationId
                                LEFT JOIN UnitInfo as unit ON unit.Id = stock.unitId 
                                LEFT JOIN OfferInfo as offer ON offer.prodId = stock.prodId 
                                LEFT JOIN qtyManagement as qm ON qm.productId = stock.prodId 
                                WHERE stock.active='1' AND (stockstatus.prodId='" + productId + "' OR stock.prodCode='" + prodId + "') AND stockstatus.storeId = '" + HttpContext.Current.Session["storeId"] + "' AND qm.storeId = '" + HttpContext.Current.Session["storeId"] + "' ORDER BY stock.prodName DESC";
            var dtStockData = sqlOperation.getDataTable(queryAddprod);
            return commonFunction.serializeDatatableToJson(dtStockData);
        }



        public DataTable getProductDataByProductCode(string prodCode)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodCode='" + prodCode + "'" + HttpContext.Current.Session["userAccessParameters"] + "");
        }

        public DataTable getPackageDataTableModel(string prodCode)
        {
            string query = "SELECT TOP 10 packageName as prodName, qty as qty, Price as sPrice, Id as prodId, prodCode,dealerPrice FROM packageInfo WHERE Id = '" + prodCode + "'" + HttpContext.Current.Session["userAccessParameters"];

            return sqlOperation.getDataTable(query);
        }


        public DataTable CheckTransferQtyModel(string Id, string transQty, string shiftTo)
        {
            string query = "SELECT * FROM StockInfo where Id='" + Id + "'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable CheckTransferProductModel(string transToStore, string prodId)
        {
            string query = "SELECT * FROM StockInfo where storeId='" + transToStore + "' AND prodId='" + prodId + "'";
            return sqlOperation.getDataTable(query);
        }



        public bool updateOriginalStockQty(string Id, string transQty, string shiftTo)
        {
            string query = "UPDATE StockInfo SET qty=CAST(qty as decimal)-" + transQty + " where Id='" + Id +
                           "' AND roleId='" + HttpContext.Current.Session["roleId"] + "'";
            return sqlOperation.fireQuery(query);
        }

        public bool updateTransferStockQty(string Id, string transQty, string shiftTo)
        {
            string queryTrans = "UPDATE StockInfo SET qty=CAST(qty as decimal)+" + transQty + " where Id='" + Id + "' AND roleId='" + shiftTo + "' ";
            return sqlOperation.fireQuery(queryTrans);
        }

        public string getProductImeiModel()
        {
            var dtImei = sqlOperation.getDataTable("SELECT IMEI FROM StockInfo WHERE prodCode ='" + prodCode + "' ");
            if (dtImei.Rows.Count > 0)
                return dtImei.Rows[0]["imei"].ToString();
            else
                return "";
        }

        public string updateProdImeiNumber(string prodId, string imei)
        {
            return "UPDATE StockInfo SET imei='" + imei + "' where ProdId='" + prodId + "' ";
        }


        public DataTable getSearchServiceListModel(string searchVal)
        {
            string query = "SELECT name, Id as code, Id as id FROM ServiceInfo WHERE (name like N'%" + searchVal + "%')" +
                           HttpContext.Current.Session["userAccessParameters"] + " ORDER BY name ASC ";

            return sqlOperation.getDataTable(query);
        }


        public DataTable getStockQtyModel(string prodId, string status, string storeId)
        {
            return
                sqlOperation.getDataTable("SELECT SUM(CAST(qty as decimal)) as qty FROM StockStatusInfo WHERE prodID='" +
                                            prodId + "' AND status='" + status + "' AND active ='1' AND storeId='" + storeId + "'");
        }

        public DataTable getStockQtyModel(string prodId, string status)
        {
            return sqlOperation.getDataTable("SELECT SUM(CAST(qty as decimal)) as qty FROM StockStatusInfo WHERE prodID='" +
                    prodId + "' AND status='" + status + "' AND active ='1' AND storeId='" + HttpContext.Current.Session["storeId"] + "'");

        }

        public DataTable getStoreWiseQtyStoreIdModel(string prodCode, string storeId)
        {
            string q = "SELECT *," + commonFunction.getQtyQueryStock("tbl", storeId, "inquery") + " FROM StockInfo AS tbl LEFT JOIN Ecommerce AS ecom ON tbl.prodCode = ecom.prodCode WHERE tbl.active ='1' AND (tbl.prodCode = '" +
                   prodCode + "')" + commonFunction.getUserAccessParameters("tbl") + " ";

            return sqlOperation.getDataTable(q);

        }

        // Import check
        public DataTable getProductListByProdNameModel(string productName)
        {
            return
                sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodName=N'" + productName + "' " +
                                          HttpContext.Current.Session["userAccessParameters"] + "");
        }


        public DataTable getProductListByProdCodeModel(string prodCode)
        {
            return
                sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodCode=N'" + prodCode + "' " +
                                          HttpContext.Current.Session["userAccessParameters"] + "");
        }





        public bool restoreStockProduct(string prodId)
        {
            sqlOperation.fireQuery("Update stockInfo SET active='1' where prodId='" + prodId + "'");
            return sqlOperation.fireQuery("Update stockStatusInfo SET active='1' where prodId='" + prodId + "'");
        }

        public DataTable getStcokQtyByUnitModel(string status, string prodCode, string storeId)
        {
            return
                sqlOperation.getDataTable("SELECT qty FROM StockStatusInfo WHERE status='" + status + "' AND prodCode='" + prodCode +
                                          "' AND storeId='" + storeId + "'");
        }

        public DataTable getAttrValue(string attrId)
        {
            return sqlOperation.getDataTable("SELECT * FROM AttributeInfo WHERE Id='" + attrId + "'");
        }

        public DataTable getProductIdByProductCodeModel(string productCode)
        {
            return sqlOperation.getDataTable("SELECT prodId FROM StockInfo WHERE prodCode='" + productCode + "'");
        }


        public DataTable getProductDataByParentProductId(string prodID)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE parentId='" + prodID + "'" + HttpContext.Current.Session["userAccessParameters"] + "");
        }



        public DataTable getProductDataWithoutAnyAuth(string prodID)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodId='" + prodID + "'");
        }

        public bool checkUnitIdExist(string _unitId)
        {
            var dtUnit = sqlOperation.getDataTable("SELECT * FROM UnitInfo WHERE Id='" + _unitId + "'");
            if (dtUnit.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }








        public void UpdateStockBuyPriceForSupplierCommissionModel()
        {
            sqlOperation.executeQuery("UPDATE StockInfo SET bPrice ='" + bPrice + "'  WHERE prodId='" + prodId + "'");
        }





        public bool UpdateStockQtyCalculateAutomatic()
        {
            return sqlOperation.fireQuery("UPDATE StockInfo SET qty ='" + qty + "'  WHERE prodId='" + prodId + "' AND warehouse='" + storeId + "'");

        }

        public bool createStockQtyManagement()
        {
            string query = "INSERT INTO QtyManagement (productId,stockQty,storeId,roleId,branchId,groupId,entryDate,updateDate) VALUES ('" + prodId + "','" + qty + "','" + storeId + "','" + roleId + "','" + branchId + "','" + groupId + "','" + entryDate + "','" + updateDate + "')";
            return sqlOperation.fireQuery(query);
        }

        public bool updateStockQty()
        {
            string query = "UPDATE QtyManagement SET stockQty='" + qty + "' where productId='" + prodId + "' AND storeId='" + storeId + "'";
            return sqlOperation.fireQuery(query);
        }

        public DataTable getStockQtyManagement()
        {
            string query = "SELECT * FROM QtyManagement where productId='" + prodId + "' AND storeId='" + storeId + "'";
            return sqlOperation.getDataTable(query);
        }



        public string updateStockQtyWihoutExecute()
        {
            string query = "UPDATE QtyManagement SET stockQty='" + qty + "' where productId='" + prodId + "' AND storeId='" + storeId + "' ";
            return query;
        }


        public DataTable getStockQtyModel(string productId)
        {
            string query = "SELECT qm.stockQty as stockQty FROM  StockInfo as stock LEFT JOIN QtyManagement as qm ON stock.prodId = qm.productId where qm.productId='" + productId + "' AND qm.storeId='" + HttpContext.Current.Session["storeId"].ToString() + "'";
            return sqlOperation.getDataTable(query);
        }


        //public void updateStockStatus()
        //{
        //    query = "INSERT INTO StockStatusInfo VALUES('" +
        //            prodId + "','" +
        //            prodCode + "',N'" +
        //            prodName + "',N'" +
        //            prodDescr + "',N'" +
        //            supCompany + "',N'" +
        //            catName + "','" +
        //            lastQty + "','" +
        //            bPrice + "','" +
        //            sPrice + "','" +
        //            weight + "','" +
        //            size + "','" +
        //            discount + "','" +
        //            stockTotal + "','" +
        //            Status + "','" +
        //            commonFunction.GetCurrentTime() + "','" +
        //            commonFunction.GetCurrentTime() + "','" +
        //            entryQty + "','" +
        //            title + "','" +
        //            HttpContext.Current.Session["roleId"] + "','" +
        //            billNo + "', '" +
        //            HttpContext.Current.Session["branchId"] + "','" +
        //            HttpContext.Current.Session["groupId"] + "','" +
        //            fieldAttribute + "','" +
        //            tax + "','" +
        //            sku + "','" +
        //            lastQty + "','" +
        //            productSource + "','" +
        //            prodCode + "','" +
        //            imei + "','" +
        //            fieldId + "','" +
        //            attributeId + "','" +
        //            SupCommission + "','" +
        //            dealerPrice + "')";
        //    sqlOperation.executeQuery(query);
        //}






        //public string updateStockQtyModelForSave(string prodId, string qty)
        //{
        //    return "UPDATE StockInfo SET qty=" + qty + " WHERE prodId='" + prodId + "' ";
        //}


    }


}
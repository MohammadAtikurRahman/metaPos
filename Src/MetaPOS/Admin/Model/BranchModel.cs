using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using System.Data;


namespace MetaPOS.Admin.Model
{

    public class BranchModel
    {
        SqlOperation sqlOperation = new SqlOperation();
        CommonFunction commonFunction = new CommonFunction();

        public string Id { get; set; }
        public string warehouseId { get; set; }




        public string insertBranchInfo()
        {
            return sqlOperation.executeQuery(
                "INSERT INTO [BranchInfo] (Id, entryDate, updateDate, userRight,storeId) VALUES ('" +
                Id + "', '" + commonFunction.GetCurrentTime().ToShortDateString() + "', '" +
                commonFunction.GetCurrentTime().ToShortDateString() + "', 'Branch','" + warehouseId + "')");
        }





        public DataTable getBranchInfoOrderByDesc()
        {
            return sqlOperation.getDataTable("SELECT * FROM branchInfo ORDER BY Id DESC");
        }



        public string syncBranchAllDataModel(string syncFrom, string syncTo)
        {

            string query = @"Merge Category as target
                            using Category  as source
                            on
                            target.Id=source.Id where target.roleId = '16' AND source.roleID='3' 
                            When matched 
                            Then
                            update 
                            set target.catName=source.catName
                            When not matched by Target Then
                            INSERT (catName,roleId,refId) VALUES (catName,'" + syncTo + "',Id)";

            return sqlOperation.executeQuery(query);

        }

        public bool checkItemExist(string Id, string syncTo, string tableName)
        {
            string query = "SELECT * FROM " + tableName + " where roleId='" + syncTo + "' AND refId='" + Id + "'";
            var dtItems = sqlOperation.getDataTable(query);
            if (dtItems.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public DataTable getSyncItemList(string syncFrom, string tableName)
        {
            string query = "SELECT * FROM " + tableName + " where " + getBranchAllRoleID(syncFrom);
            return sqlOperation.getDataTable(query);
        }

        public bool updateBranchItem(string tableName, string syncFrom, string syncTo, string itemId, string updateStatement)
        {
            string query =
                "UPDATE " + tableName + " SET " + updateStatement + " WHERE refId='" + itemId + "' AND roleId ='" + syncTo + "'";
            return sqlOperation.fireQuery(query);
        }

        public bool insertBranchItem(string syncFrom, string syncTo, string itemId)
        {
            string query = "INSERT CategoryInfo (catName,entryDate, updateDate,roleID,active,refId) ( SELECT catName,entryDate,updateDate,'" +
                syncTo + "',active,Id FROM CategoryInfo Where Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        public DataTable getSyncFrom(string refId, string tableName)
        {
            return sqlOperation.getDataTable("SELECT * FROM " + tableName + " WHERE Id='" + refId + "'");
        }

        public bool insertBranchUnitInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT UnitInfo (unitName,unitRatio,entryDate, updateDate,roleID,active,refId) ( SELECT unitName,unitRatio,entryDate,updateDate,'" +
                syncTo + "',active,Id FROM UnitInfo Where Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchWarehouseInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
               "INSERT WarehouseInfo (name,entryDate, updateDate,roleID,active,refId) ( SELECT name,entryDate,updateDate,'" +
               syncTo + "',active,Id FROM WarehouseInfo Where Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchManufacturerInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
               "INSERT ManufacturerInfo (manufacturerName,entryDate, updateDate,roleID,active,refId) ( SELECT manufacturerName,entryDate,updateDate,'" +
               syncTo + "',active,Id FROM ManufacturerInfo Where Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchSupplierInfo(string syncFrom, string syncTo, string itemId, string supId)
        {
            string query =
               "INSERT SupplierInfo (supID,supCompany, supPhone,conName,conTitle,conPhone,address,fax,paymethod_,discount,supCode,entryDate, updateDate,roleID,active,refId) ( SELECT Id,supCompany, supPhone,conName,conTitle,conPhone,address,fax,paymethod_,discount,supCode,entryDate,updateDate,'" +
               syncTo + "',active,supID FROM SupplierInfo Where Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        internal DataTable getItemIDByRefIDModel(string refId, string tableName, string syncTo)
        {
            return sqlOperation.getDataTable("SELECT * FROM " + tableName + " WHERE refId = " + refId + " AND roleId='" + syncTo + "'");
        }

        public DataTable getStockData(string syncFrom)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE roleId='" + syncFrom + "' OR branchId='" + syncFrom + "'");
        }

        public bool insertSyncStockData(string syncTo, string Id, string catId, string supId, string manufactureId, string warehouseId, string unitId)
        {
            DateTime defaultDate = DateTime.MinValue;

            string query =
                        "INSERT INTO StockInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,entryDate,entryQty,title,branchName,roleId,branchId,groupId,fieldAttribute,tax,sku,lastQty,warranty,imei,warningQty,dealerPrice,createdFor,receivedDate,expiryDate,batchNo,serialNo,shipmentStatus,manufacturerId,notes,unitId,warehouse,commission,purchaseDate,imeiStatus,refId) " +
                        " (SELECT '" + GenerateProductID() + "', prodCode,prodName,prodDescr,'" + supId + "','" + catId + "','0',bPrice,sPrice,weight,size,discount,'0','" + commonFunction.GetCurrentTime() + "','0',title,branchName,'" + syncTo + "','0',groupId,fieldAttribute,tax,sku,'0',warranty,'',warningQty,dealerPrice,'" + syncTo + "','" + commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "',batchNo,serialNo,shipmentStatus,'" + manufactureId + "',notes,'" + unitId + "','" + warehouseId + "',commission,'" + commonFunction.GetCurrentTime() + "','0',Id FROM StockInfo where Id='" +
                        Id + "')";

            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchStaffInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT INTO StaffInfo (staffID,name,phone,dob,address,sex,entryDate,updateDate,roleID,active) (SELECT staffID,name,phone,dob,address,sex,entryDate,updateDate,'" +
                syncTo + "',active FROM StaffInfo WHERE Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchCashCatInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT INTO CashCatInfo (cashType,descr,entryDate,updateDate,roleId,active) (SELECT CashType,descr,entryDate,updateDate,'" +
                syncTo + "',active FROM CashCatInfo where Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchFieldInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT INTO FieldInfo (field,visible,active,entryDate,updateDate,roleID,refId) (SELECT field,visible,active,entryDate,updateDate,'" +
                syncTo + "',Id FROM FieldInfo WHERE Id='" + itemId + "')";
            return sqlOperation.fireQuery(query);

        }

        internal bool insertBranchCardTypeInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT INTO CardTypeInfo (cardName,bankId,cardDisc,entryDate,updateDate,roleID,active) (SELECT cardName,bankId,cardDisc,entryDate,updateDate,'" +
                syncTo + "',active FROM CardTypeInfo where id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchBankNameInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT INTO BankNameInfo (bankName,descr,entryDate,updateDate,roleID,active) (SELECT bankName,descr,entryDate,updateDate,'" +
                syncTo + "',active FROM  BankNameInfo WHERE id='" + itemId + "' )";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchAttributeInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT INTO AttributeInfo (fieldId, attributeName,active,entryDate,updateDate,roleID,refId) (SELECT fieldId, attributeName,active,entryDate,updateDate,'" +
                syncTo + "',Id FROM AttributeInfo WHERE Id='" + itemId + "'  ) ";
            return sqlOperation.fireQuery(query);
        }

        internal bool insertBranchAttributeDetailInfo(string syncFrom, string syncTo, string itemId)
        {
            string query =
                "INSERT INTO AttributeDetailInfo (fieldId,attributeId,productId,soldQty,stockQty,entryDate,updateDate,roleId,refId) (SELECT fieldId,attributeId,productId,soldQty,stockQty,entryDate,updateDate,'" +
                syncTo + "',Id FROM AttributeDetailInfo WHERE id='" + itemId + "')";
            return sqlOperation.fireQuery(query);
        }


        public int GenerateProductID()
        {
            int prodId = commonFunction.nextIdPlusOne("SELECT MAX(prodID) FROM [StockInfo]");
            int prodId2 = commonFunction.nextIdPlusOne("SELECT MAX(prodID) FROM [StockStatusInfo]");

            if (prodId2 > prodId)
                prodId = prodId2;

            prodId++;
            return prodId;
        }



        public string BarCodeGeneratorForBulkStock()
        {
            string prodCode = commonFunction.barcodeGenerator();

            return prodCode;
        }



        public string nextPurchaseCode()
        {
            DataSet ds;
            string purchaseCode;
            ds = sqlOperation.getDataSet("SELECT  purchaseCode FROM [StockStatusInfo] where purchaseCode LIKE 'P%' AND   purchaseCode != '' ORDER BY purchaseCode DESC");

            try
            {
                string pCode = ds.Tables[0].Rows[0][0].ToString();
                purchaseCode = commonFunction.nextPurchaeCodeGenerator(pCode);
            }
            catch
            {
                purchaseCode = "P000001";
            }

            return purchaseCode;
        }

        internal object getSupplierIDByRefIDModel(string refId, string tableName)
        {
            throw new NotImplementedException();
        }





        public string getBranchAllRoleID(string roleId)
        {
            string strRoleId = "";
            DataTable dtTable =
                sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleId + "' OR branchId ='" + roleId +
                                          "'");
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                strRoleId += " roleId =" + dtTable.Rows[i]["roleID"] + " OR";
            }

            strRoleId = strRoleId.Substring(0, strRoleId.Length - 2);

            return strRoleId;
        }


        public bool UpdateBranchSupplierID(string syncFrom, string syncTo, string itemId, string getSupId)
        {
            DataTable dtTable =
                sqlOperation.getDataTable("SELECT * FROM SupplierInfo WHERE roleId='" + syncTo + "'");
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                sqlOperation.fireQuery("UPDATE SupplierInfo SET supID='" + dtTable.Rows[i]["Id"] + "' WHERE Id='" +
                                          dtTable.Rows[i]["Id"] + "' ");
            }
            return true;
        }

        public DataTable dtStockFromByRoleID(string Id, string table, string syncFrom)
        {
            return
                sqlOperation.getDataTable("SELECT * FROM " + table + " where Id ='" + Id + "' AND roleId ='" + syncFrom + "'");
        }

        public DataSet getBranchListModel()
        {
            var roleId = HttpContext.Current.Session["roleId"];
            return
                sqlOperation.getDataSet("SELECT * FROM RoleInfo WHERE groupId='" + roleId + "' and userRight='Branch'");
        }
    }
}
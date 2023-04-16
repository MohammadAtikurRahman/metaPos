using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using System.Data;


namespace MetaPOS.Admin.Model
{
    public class ImportModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        public string supplierName { get; set; }
        public string supId { get; set; }


        // Category
        public string catName { get; set; }

        public bool saveImportedSupplier()
        {
            return sqlOperation.fireQuery("INSERT INTO SupplierInfo (supID,supCompany,roleId,active,entryDate,updateDate) VALUES('" + supId + "','" +
                                   supplierName + "','" + HttpContext.Current.Session["roleId"] + "','1','" + commonFunction.GetCurrentTime() + "','" +
                                   commonFunction.GetCurrentTime() + "')");
        }



        public bool saveImportedCategory()
        {
            string query = "INSERT INTO CategoryInfo (catName,entryDate,updateDate,active,roleId) VALUES ('" + catName +
                           "','" + commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "','1','" +
                           HttpContext.Current.Session["roleId"] + "')";
            return sqlOperation.fireQuery(query);
        }

        public DataTable importExistValueCheckerModel(string value, string column, string table)
        {
            string query = "SELECT * FROM " + table + " WHERE " + column + " = '" + value + "' AND roleId='" + HttpContext.Current.Session["roleId"] + "'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getIdByValueModel(string value, string column, string columnSearch, string table)
        {
            string query = "SELECT " + column + " FROM " + table + " WHERE " + columnSearch + " LIKE '%" + value + "%'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getDataDictImportModel(string key, string value, string table)
        {
            string query = "SELECT " + key + " as dictKey ," + value + " as distValue FROM " + table + " where roleId='"+HttpContext.Current.Session["roleid"]+"'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable checkSupplierID(string suplierId)
        {
            return sqlOperation.getDataTable("SELECT supID FROM SupplierInfo where supID='" + suplierId + "'");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.Model
{


    public class WarehouseModel
    {


        public int Id { get; set; }
        public string name { get; set; }
        public DateTime entryDate { get; set; }

        public DateTime updateDate { get; set; }
        public string roleId { get; set; }

        public char active { get; set; }


        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction commonFunction = new DataAccess.CommonFunction();





        //public bool saveWarehouseInfoModel(string name)
        //{
        //    bool exists = false;
        //    string query = "INSERT INTO WarehouseInfo VALUES ('" + name + "','" + commonFunction.GetCurrentTime() + "','" +
        //                   commonFunction.GetCurrentTime() + "','" + HttpContext.Current.Session["roleId"] + "','1')";
        //    try
        //    {
        //        objSql.executeQuery(query);
        //        exists = true;
        //    }
        //    catch (Exception)
        //    {
        //        exists = false;
        //    }
        //    return exists;
        //}





        public bool updateWarehouseInfoModel(string warehouseId, string name)
        {
            bool isUpdate = false;
            try
            {
                string query = "UPDATE WarehouseInfo SET name='" + name + "',updateDate= '" + commonFunction.GetCurrentTime() +
                               "' WHERE Id ='" + warehouseId + "'";
                objSql.executeQuery(query);
            }
            catch (Exception)
            {
                isUpdate = true;
            }

            return isUpdate;
        }





        public DataTable getWarehouseListOderByDesc()
        {
            return objSql.getDataTable("SELECT * FROM WarehouseInfo ORDER BY Id DESC");
        }





        //public string saveAndGetWarehouseModel()
        //{
        //    string queryMainStore =
        //                "INSERT INTO WarehouseInfo (name, entryDate,updateDate,roleID,active,type) VALUES ('Main Store','" +
        //                commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "','" + roleId +
        //                "','1','main'); SELECT SCOPE_IDENTITY() as newId";
        //    return objSql.executeQueryScalar(queryMainStore);
        //}

    }


}
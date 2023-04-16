using System.Data;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{


    public class RecordModel
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        public string select { get; set; }
        public string from { get; set; }
        public string where { get; set; }
        public string values { get; set; }
        public string set { get; set; }
        public string column { get; set; }
        public string dir { get; set; }
        public string status { get; set; }





        public DataTable getRecordInfoListModel()
        {
            return sqlOperation.getDataTable("SELECT " + select + " FROM " + from + " WHERE " + where + HttpContext.Current.Session["userAccessParameters"] + " ORDER BY " + column + " " + dir);
        }


        public DataTable getBranchInfoListModel()
        {
            return sqlOperation.getDataTable("SELECT " + select + " FROM " + from + " WHERE " + where + "  ORDER BY " + column + " " + dir);
        }




        public int findRecordInfoModel()
        {
            return sqlOperation.countDataRows("SELECT " + select + " FROM " + from + " WHERE " + where + HttpContext.Current.Session["userAccessParameters"]);
        }





        public bool saveRecordInfoModel()
        {
            return sqlOperation.fireQuery("INSERT INTO " + from + " VALUES (" + values + ")");
        }





        public bool updateRecordInfoModel()
        {
            return sqlOperation.fireQuery("UPDATE " + from + " SET " + set + " WHERE " + where);
        }





        public bool delteRecordInfoModel()
        {
            return sqlOperation.fireQuery("UPDATE " + from + " SET " + set + " WHERE " + where);
        }





        public bool restoreRecordInfoModel()
        {
            return sqlOperation.fireQuery("UPDATE " + from + " SET " + set + " WHERE " + where);
        }



        public DataSet getWarehouseListForBranch()
        {
            return
                sqlOperation.getDataSet("SELECT * FROM WarehouseInfo WHERE roleID='" + HttpContext.Current.Session["roleID"] + "' AND active='" + status +
                                        "'");
        }

        public DataSet getWarehouseListForStore()
        {
            return sqlOperation.getDataSet("select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' AND role.storeId=" + HttpContext.Current.Session["storeId"] + " ORDER BY warehouse.Id ASC");
        }
    }


}
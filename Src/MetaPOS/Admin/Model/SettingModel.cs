using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{


    public class SettingModel
    {

        private CommonFunction commonFunction = new CommonFunction();
        private SqlOperation sqlOperation = new SqlOperation();

        public string column { get; set; }
        public string value { get; set; }
        public string roleId { get; set; }





        public string updateSettingInfoModel()
        {
            var query = "UPDATE [SettingInfo] SET " + column + " = '" + value + "' WHERE id='" + roleId + "' ";

            return sqlOperation.executeQuery(query);
        }



        public string initializeConfigModel()
        {
            string querySetting = "SELECT * FROM SettingInfo as setting LEFT JOIN RoleInfo as role ON role.roleID = setting.Id LEFT JOIN BranchInfo as branch ON  branch.storeId = role.storeId LEFT JOIN WarehouseInfo as store ON branch.storeId = store.Id "
                                  + "WHERE branch.storeId ='" + HttpContext.Current.Session["storeId"] + "'";
            var dt = sqlOperation.getDataTable(querySetting); ;
            return commonFunction.serializeDatatableToJson(dt);
        }
    }


}
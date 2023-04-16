using System.Web;
using MetaPOS.Admin.DataAccess;
using DataTable = System.Data.DataTable;


namespace MetaPOS.Admin.Model
{


    public class StaffModel
    {
        private SqlOperation objSqlOperation = new SqlOperation();
        private string query = "";

        // Get Staff data for API
        public DataTable getStaffApiDataListModel(string getConditionalParameter) 
        {
            query = "SELECT * FROM StaffInfo WHERE " + getConditionalParameter;
            var dt = objSqlOperation.getDataTable(query);
            return dt;
        }


        public DataTable getStaffListModel()
        {
            return objSqlOperation.getDataTable("SELECT staffID as Id, name FROM staffInfo where active='1' AND storeID = " + HttpContext.Current.Session["storeId"] + "");
        }
    }


}
using System.Data;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{


    public class PackageModel
    {


        private SqlOperation sqlOperation = new SqlOperation();


       

        public DataTable getSearchPackageListModel(string searchVal)
        {
            string query = "SELECT packageName as name, Id as code, Id as id FROM PackageInfo WHERE active = '1' AND (packageName like N'%" + searchVal + "%')" + HttpContext.Current.Session["storeAccessParameters"] + " ORDER BY packageName ASC ";

            return sqlOperation.getDataTable(query);
        }



        public DataTable getPackageDataListModelByPackId(string prodId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM PackageInfo WHERE Id = '" + prodId + "'");
            return dt;
        }


        public DataTable getPackageDataListModel()
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM PackageInfo where active='1'");
            return dt;
        }

    }


}
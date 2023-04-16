using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AppBundle.Service
{
    public class ModelService
    {
        private FireService fireService = new FireService();
        private CommonFunction commonFunction = new CommonFunction();



        public string select { get; set; }
        public string from { get; set; }
        public string where { get; set; }
        public string values { get; set; }
        public string set { get; set; }
        public string column { get; set; }
        public string dir { get; set; }





        public string getDataListModel()
        {
            var sessionConditon = " AND storeID = " + HttpContext.Current.Session["storeId"];
            if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
            {
                if (column.Contains("."))
                {
                    var table = column.Split('.')[0];
                    sessionConditon = commonFunction.getUserAccessParameters(table);
                }
                else
                {
                    sessionConditon = HttpContext.Current.Session["userAccessParameters"].ToString();
                }
            }

            return fireService.getDataTable("SELECT " + select + " FROM " + from + " WHERE " + where + sessionConditon + " ORDER BY " + column + " " + dir);
        }






        public string getDataModel()
        {
            return fireService.getDataTable("SELECT " + select + " FROM " + from + " WHERE " + where + HttpContext.Current.Session["userAccessParameters"]);
        }





        public string findDataModel()
        {
            return fireService.countDataRows("SELECT " + select + " FROM " + from + " WHERE " + where + HttpContext.Current.Session["userAccessParameters"]);
        }





        public string saveDataModel()
        {
            return fireService.executeQuery("INSERT INTO " + from + " " + values);
        }




        public string updateDataModel()
        {
            return fireService.executeQuery("UPDATE " + from + " SET " + set + " WHERE " + where);
        }





        public string deleteDataModel()
        {
            return fireService.executeQuery("UPDATE " + from + " SET " + set + " WHERE " + where);
        }





        public string restoreDataModel()
        {
            return fireService.executeQuery("UPDATE " + from + " SET " + set + " WHERE " + where);
        }


        public string getDataJoinListModel()
        {
            return fireService.getDataTable("SELECT " + select + " FROM " + from + " WHERE " + where + commonFunction.getUserAccessParameters("tbl1") + " ORDER BY " + column + " " + dir);
        }
    }
}
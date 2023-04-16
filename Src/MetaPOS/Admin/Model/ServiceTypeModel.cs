using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class ServiceTypeModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        public List<ListItem> getServiceTypeListModel()
        {
            string query = "SELECT Id,name FROM ServiceTypeInfo WHERE active='1' " + HttpContext.Current.Session["userAccessParameters"] + " ";

            var serviceTypeList = new List<ListItem>();
            var dtServiceType = sqlOperation.getDataTable(query);

            foreach (DataRow row in dtServiceType.Rows)
            {
                serviceTypeList.Add(new ListItem(row["name"].ToString(),row["Id"].ToString()));
            }
            return serviceTypeList;
        }
    }
}
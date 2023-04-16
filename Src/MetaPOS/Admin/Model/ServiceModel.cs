using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.AppBundle.Service;
using MetaPOS.Admin.DataAccess;
using System.Data;


namespace MetaPOS.Admin.Model
{
    public class ServiceModel
    {
        private  SqlOperation sqlOperation = new SqlOperation();

        public int Id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public decimal retailPrice { get; set; }
        public decimal wholePrice { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }
        public int roleId { get; set; }

        public char active { get; set; }


        public ServiceModel()
        {
            retailPrice = 0;
            wholePrice = 0;
            entryDate = DateTime.Now;
            updateDate = DateTime.Now;
            roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"]);
            active = '1';
        }





        public bool saveServiceModel()
        {
            string query =
                "INSERT INTO ServiceInfo (type,name,retailPrice,wholePrice,entryDate,updateDate,roleId,active) VALUES ('" +
                type + "','" + name + "','" + retailPrice + "','" + wholePrice + "','" + entryDate + "','" + updateDate +
                "','" + roleId + "','" + active + "')";

            return sqlOperation.fireQuery(query);
        }

        public DataTable getServiceDataTableModel(string prodCode)
        {
            return sqlOperation.getDataTable("SELECT * FROM ServiceInfo WHERE Id='" + prodCode + "'");
        }

        public string getServiceDataListModel(string active)
        {
            var fireService = new FireService();
            return fireService.getDataTable("SELECT service.id,type,service.name,service.retailPrice,service.wholePrice,type.name as typeName FROM ServiceInfo as service LEFT JOIN ServiceTypeInfo as type ON service.type = type.Id  WHERE service.active='" + active + "'");
        }

        public bool updateServiceDataModel()
        {
            return
                sqlOperation.fireQuery("UPDATE ServiceInfo SET type='" + type + "',name='" + name + "',retailPrice='" +
                                          retailPrice + "',wholePrice ='" + wholePrice + "',updateDate='" + updateDate +
                                          "' WHERE Id='" + Id + "'");
        }

        public DataTable getServiceDataListModelByID(string Id)
        {
            return sqlOperation.getDataTable("SELECT * FROM ServiceInfo WHERE Id='" + Id + "'");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class ServicingModel
    {

        SqlOperation sqlOperation = new SqlOperation();


        public string cusId { get; set; }
        public string serviceId { get; set; }
        public string prodId { get; set; }
        public string prodName { get; set; }
        public string imei { get; set; }
        public string supplier { get; set; }
        public string supplierId { get; set; }
        public string deliveryDate { get; set; }

        public string description { get; set; }
        public decimal paidAmt { get; set; }
        public decimal totalAmt { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }

        public string roleId { get; set; }
        public string active { get; set; }

        public string saveServiceInfoDataModel(string serviceData)
        {
            string query = "INSERT INTO ServicingInfo(serviceId,customerId,prodId,prodName,imei,supplier,deliveryDate,description,paidAmt,totalAmt,entryDate,updateDate,roleId,active,supplierId) VALUES ('" +
                serviceId + "','" + cusId + "','" + prodId + "','" + prodName + "','" + imei + "','" + supplier + "','" +
                deliveryDate + "','" + description + "','" + paidAmt + "','" + totalAmt + "','" + entryDate + "','" + updateDate + "','" + roleId + "','" +
                active + "', '" + supplierId + "')";

            return sqlOperation.executeQuery(query);

        }





        public string updateServiceInfoDataModel()
        {
            string query = "UPDATE ServicingInfo SET paidAmt = paidAmt +'" + paidAmt + "',updateDate='" + updateDate +
                           "' WHERE serviceId = '" + serviceId + "'";
            return sqlOperation.executeQuery(query);
        }

        public DataTable generateNextServiceIdModel()
        {
            return sqlOperation.getDataTable("SELECT serviceId FROM [ServicingInfo] ORDER BY Id DESC");
        }
    }
}
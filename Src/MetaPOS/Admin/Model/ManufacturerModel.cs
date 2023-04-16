using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class ManufacturerModel
    {
        private  SqlOperation sqlOperation = new SqlOperation();
        private  CommonFunction commonFunction = new CommonFunction();

        public string manufacturerName { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }
        public string roleId { get; set; }
        public string active { get; set; }





        public ManufacturerModel()
        {
            entryDate = updateDate = commonFunction.GetCurrentTime();
        }



        public bool createManufacturerModel()
        {
            string query = "INSERT INTO ManufacturerInfo (manufacturerName,entryDate,updateDate,roleId) VALUES ('" +
                           manufacturerName + "','" + entryDate + "','" + updateDate + "','" + roleId + "')";
            return sqlOperation.fireQuery(query);
        }
    }
}
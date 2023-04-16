using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{

    public class CustomerPointModel
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public int cusId { get; set; }
        public decimal point { get; set; }
        public string source { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }

        public char active { get; set; }


        public string saveCustomerPointModel()
        {
            string query = "INSERT CustomerPointInfo (cusId,point,source,entryDate,updateDate,active) VALUES('" +
                           cusId + "','" + point + "','" + source + "','" + entryDate + "','" + updateDate +
                           "','" + active + "') ";
            return query;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaPOS.Api.Common;
//using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Api.Models
{
    public class InventoryModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string storeAccessParameters { get; set; }
        public string shopName { get; set; }

        public DataTable InventoryData()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopName;
            return sqlOperation.getDataTable("SELECT * FROM StockInfo where active='1'");
        }
    }
}

using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Infrastructure.Data
{
    public class SupplierModel : SupplierDto
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public DataTable GetSupplierRecivedAmount()
        {
            //string query = "SELECT SUM(cashIn) as cashin FROM CashReportInfo WHERE status='3' AND entryDate >= '1/9/2020' AND entryDate <= '1/9/2020'  AND storeId ='74'";
            string query = "SELECT SUM(cashIn) as cashIn FROM CashReportInfo WHERE status='3' AND entryDate >= '" + From.ToShortDateString() + "' AND entryDate <= '" + To.ToShortDateString() + "' " + storeAccessParameter + " ";
            return sqlOperation.getDataTable(query);
        }
    }
}

using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Infrastructure.Data
{
    public class ExpenseModel : SearchDto
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public DataTable GetData()
        {
            string query = "SELECT SUM(cashOut) as cashOut FROM CashReportInfo WHERE status='2' AND entryDate >= '" + From + "' AND entryDate <= '" + To + "' " + storeAccessParameter + " ";
            return sqlOperation.getDataTable(query);
        }
    }
}

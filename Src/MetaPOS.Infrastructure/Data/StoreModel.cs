using MetaPOS.Entities.RecordAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Infrastructure.Data
{
    public class StoreModel : Store
    {

        SqlOperation sqlOperation = new SqlOperation();

        public DataTable getStore()
        {
            string query = "SELECT * FROM WarehouseInfo WHERE active='1'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getBranchRoleList()
        {
            string query = "SELECT * FROM WarehouseInfo WHERE active='1'";
            return sqlOperation.getDataTable(query);
        }

        public DataTable getStoreListDataCondition(string condition)
        {
            string query = "SELECT * FROM WarehouseInfo WHERE active='1' " + condition + "";
            return sqlOperation.getDataTable(query);
        }
    }
}

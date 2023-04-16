using MetaPOS.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Infrastructure.Data
{
    public class RoleModel : Role
    {
        SqlOperation sqlOperation = new SqlOperation();

        public DataTable getBranchRoleList()
        {
            var role = new Role();
            string query = "SELECT * FROM RoleInfo WHERE roleId='" + RoleId + "' OR branchId='" + RoleId + "'";
            return sqlOperation.getDataTable(query);
        }
    }
}

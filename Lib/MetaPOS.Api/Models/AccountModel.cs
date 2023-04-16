using System.Data;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;

namespace MetaPOS.Api.Models
{
    public class AccountModel : Account
    {
        public new string shopname { get; set; }
        public DataTable getLoginData()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.conString = shopname;
            return
                sqlOperation.getDataTable("SELECT role.roleId,role.title,role.userRight,role.branchId,role.email,role.monthlyfee,role.activedate,role.expiryDate,role.storeId,branch.branchWebsite FROM RoleInfo as role LEFT JOIN BranchInfo as branch ON role.storeId = branch.storeId WHERE role.email='" + email + "' AND role.password='" +
                                          password + "' AND role.active='1'");
        }
    }
}

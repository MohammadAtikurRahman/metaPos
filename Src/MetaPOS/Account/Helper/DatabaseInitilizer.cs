using MetaPOS.Admin.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Account.Helper
{
    public class DatabaseInitilizer
    {
        public void Initilize()
        {
            var version = 1.0M;
            var sqlOperation = new SqlOperation();
            var versionControl = new Version();

            try
            {
                var dtVersion = sqlOperation.getDataTable("SELECT version FROM [RoleInfo] WHERE roleID = '" + HttpContext.Current.Session["roleId"] + "' ");

                if (dtVersion.Rows.Count > 0)
                    version = Convert.ToDecimal(dtVersion.Rows[0]["version"].ToString());
                

                // Version Upgrading
                version = versionControl.UpgradeVersion(version);



                // FOR SUPER ADMIN
                const string accessPageList =
                "Stock; UnitStock; BulkStock; Package; Return; Damage; Cancel; Warning; Product; Warranty; Expiry; Purchase; " +
                "Sale; Invoice; Slip; Customer; Quotation; Servicing; DueReminder; Token; Service; " +
                "Supply; Salary; Expense; Receive; Banking; " +
                "Dashboard; Transaction; Summary; PurchaseReport; InventoryReport; StockReport; ProfitLoss; SupplierCommission; " +
                "Ecommerce; Offer; SMS; Import; " +
                "Manufacturer; Supplier; Category; Particular; Staff; Bank; Card; UnitMeasurement; Store; Field; Attribute; Location; " +
                "Sync; SmsConfig; Web; " +
                "User; Branch; Profile; Security; Setting; Support; " +
                "Docs; Size; Version; ServiceType; Subscription; Offline; " +
                "Add; Edit; Delete;";

                sqlOperation.executeQueryWithoutAuth("UPDATE RoleInfo SET accessPage = '" + accessPageList + "' WHERE userRight = 'Super'");

                // Update command for last version
                sqlOperation.executeQueryWithoutAuth("UPDATE RoleInfo SET version = '" + version + "'");
            }
            catch (Exception)
            {
                sqlOperation.executeQueryWithoutAuth("UPDATE RoleInfo SET version = '" + version + "'");
            }
        }
        
    }
}
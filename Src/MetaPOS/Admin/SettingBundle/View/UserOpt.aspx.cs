using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.SettingBundle.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MetaPOS.Admin.SettingBundle.View
{
    public partial class UserOpt : BasePage
    {
        private CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("User"))
                {
                    commonFunction.pageout();
                }

                checkVisiableContent();

                if (Request["id"] != null)
                {
                    var roleId = Request["id"].ToString();
                    setUserData(roleId);

                    divConfirmPassword.Visible = false;
                }
                else
                {
                    string accessPage = "";
                    setAccessPage(accessPage);

                }

                checkUserMode();


            }

        }


        private void checkVisiableContent()
        {
            var roleId = "0";
            var userRight = Session["userRight"].ToString();
            if (userRight == "Super")
            {
                divBranchLimit.Visible = true;
                divUserLimit.Visible = true;
                divExpiryDate.Visible = true;
                divSubscriptionFee.Visible = true;
                divStoreList.Visible = false;
            }
            else if (userRight == "Group")
            {
                divUserLimit.Visible = true;
                divExpiryDate.Visible = true;
                divSubscriptionFee.Visible = true;
                divStoreList.Visible = true;
                btnUserLogs.Visible = true;
                ddlStoreList.Attributes.Add("disabled", "disabled");

                if (Request["id"] != null)
                    roleId = Request["id"].ToString();
            }
            else if (userRight == "Branch")
            {
                roleId = HttpContext.Current.Session["roleId"].ToString();
                divStoreList.Visible = true;
                btnUserLogs.Visible = false;

            }
            else if (userRight == "Regular")
            {


            }


            commonFunction.fillAllDdl(ddlStoreList, "SELECT id,name FROM WarehouseInfo WHERE active='1' and roleId = '" + roleId + "'", "name", "id");
            ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_addUser_select, "0"));
        }



        private void checkUserMode()
        {
            if (Request["mode"] != null)
            {
                if (Request["mode"] == "edit")
                {

                }
                else
                {
                    txtTitle.Enabled = false;
                    txtEmail.Enabled = false;
                    txtPassword.Enabled = false;
                    txtBranchLimit.Enabled = false;
                    txtUserLimit.Enabled = false;
                    txtSubscriptionFee.Enabled = false;
                    txtExpiryDate.Enabled = false;
                    ddlStoreList.Enabled = false;

                    chkPurchase.Enabled = false;

                    btnSave.Visible = false;
                }
            }
        }

        private void setUserData(string roleId)
        {
            var userService = new UserService();
            var dtRole = userService.GetUserData(roleId);
            if (dtRole.Rows.Count > 0)
            {
                txtTitle.Text = dtRole.Rows[0]["title"].ToString();
                txtEmail.Text = dtRole.Rows[0]["email"].ToString();
                txtPassword.Text = commonFunction.Decrypt(dtRole.Rows[0]["password"].ToString());
                txtBranchLimit.Text = dtRole.Rows[0]["branchLimit"].ToString();
                txtUserLimit.Text = dtRole.Rows[0]["userLimit"].ToString();
                txtSubscriptionFee.Text = dtRole.Rows[0]["monthlyFee"].ToString();
                txtExpiryDate.Text = Convert.ToDateTime(dtRole.Rows[0]["expirydate"]).ToString("dd-MMM-yyyy");
                var storeId = dtRole.Rows[0]["storeId"].ToString();
                ddlStoreList.SelectedValue = storeId;

                if (Request["mode"] == "view")
                {
                    txtPassword.TextMode = TextBoxMode.SingleLine;
                    txtPassword.Text = commonFunction.Decrypt(dtRole.Rows[0]["password"].ToString());
                }

                var accessPage = dtRole.Rows[0]["accessPage"].ToString();
                setAccessPage(accessPage);

                btnSave.Text = Resources.Language.Btn_addUser_update;//Update

            }
        }



        private void setAccessPage(string accessPage)
        {

            var currentRoleId = "0";
            var userRight = Session["userRight"].ToString();

            if (userRight == "Group" || userRight == "Super")
            {
                currentRoleId = Session["roleId"].ToString();
            }
            else if (userRight == "Branch")
            {
                currentRoleId = Session["roleId"].ToString();
            }
  
            var userService = new UserService();
            var dtRole = userService.GetUserData(currentRoleId);
            if (dtRole.Rows.Count <= 0)
                return;
            var roleBaseAccessPage = dtRole.Rows[0]["accessPage"].ToString();

            // Inventory
            if (accessPage.Contains("Purchase;") && roleBaseAccessPage.Contains("Purchase;"))
            {
                chkPurchase.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Purchase;"))
                    chkPurchase.Visible = false;

            }

            if (accessPage.Contains("Stock;") && roleBaseAccessPage.Contains("Stock;"))
            {
                chkStock.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Stock;"))
                    chkStock.Visible = false;
            }

            if (accessPage.Contains("Package;") && roleBaseAccessPage.Contains("Package;"))
            {
                chkPackage.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Package;"))
                    chkPackage.Visible = false;
            }

            if (accessPage.Contains("Warranty;") && roleBaseAccessPage.Contains("Warranty;"))
            {
                chkWarranty.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Warranty;"))
                    chkWarranty.Visible = false;
            }

            if (accessPage.Contains("Return;") && roleBaseAccessPage.Contains("Return;"))
            {
                chkReturn.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Return;"))
                    chkReturn.Visible = false;
            }

            if (accessPage.Contains("Damage;") && roleBaseAccessPage.Contains("Damage;"))
            {
                chkDamage.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Damage;"))
                    chkDamage.Visible = false;
            }

            if (accessPage.Contains("Cancel;") && roleBaseAccessPage.Contains("Cancel;"))
            {
                chkCancel.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Cancel;"))
                    chkCancel.Visible = false;
            }

            if (accessPage.Contains("Warning;") && roleBaseAccessPage.Contains("Warning;"))
            {
                chkWarning.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Warning;"))
                    chkWarning.Visible = false;
            }

            if (accessPage.Contains("Expiry;") && roleBaseAccessPage.Contains("Expiry;"))
            {
                chkExpiry.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Expiry;"))
                    chkExpiry.Visible = false;
            }

            if (accessPage.Contains("Import;") && roleBaseAccessPage.Contains("Import;"))
            {
                chkImport.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Import;"))
                    chkImport.Visible = false;
            }



            // Sales
            if (accessPage.Contains("Invoice;") && roleBaseAccessPage.Contains("Invoice;"))
            {
                chkInvoice.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Invoice;"))
                    chkInvoice.Visible = false;
            }

            if (accessPage.Contains("Customer;") && roleBaseAccessPage.Contains("Customer;"))
            {
                chkCustomer.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Customer;"))
                    chkCustomer.Visible = false;
            }

            if (accessPage.Contains("Quotation;") && roleBaseAccessPage.Contains("Quotation;"))
            {
                chkQuotation.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Quotation;"))
                    chkQuotation.Visible = false;
            }

            if (accessPage.Contains("Servicing;") && roleBaseAccessPage.Contains("Servicing;"))
            {
                chkServicing.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Servicing;"))
                    chkServicing.Visible = false;
            }

            if (accessPage.Contains("DueReminder;") && roleBaseAccessPage.Contains("DueReminder;"))
            {
                chkDueReminder.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("DueReminder;"))
                    chkDueReminder.Visible = false;
            }

            if (accessPage.Contains("Service;") && roleBaseAccessPage.Contains("Service;"))
            {
                chkService.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Service;"))
                    chkService.Visible = false;
            }

            if (accessPage.Contains("Token;") && roleBaseAccessPage.Contains("Token;"))
            {
                chkToken.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Token;"))
                    chkToken.Visible = false;
            }

            // Accounting
            if (accessPage.Contains("Supply;") && roleBaseAccessPage.Contains("Supply;"))
            {
                chkSupply.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Supply;"))
                    chkSupply.Visible = false;
            }

            if (accessPage.Contains("Receive;") && roleBaseAccessPage.Contains("Receive;"))
            {
                chkReceive.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Receive;"))
                    chkReceive.Visible = false;
            }

            if (accessPage.Contains("Salary;") && roleBaseAccessPage.Contains("Salary;"))
            {
                chkSalary.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Salary;"))
                    chkSalary.Visible = false;
            }

            if (accessPage.Contains("Salary;") && roleBaseAccessPage.Contains("Salary;"))
            {
                chkSalary.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Salary;"))
                    chkSalary.Visible = false;
            }

            if (accessPage.Contains("Expense;") && roleBaseAccessPage.Contains("Expense;"))
            {
                chkExpense.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Expense;"))
                    chkExpense.Visible = false;
            }

            if (accessPage.Contains("Banking;") && roleBaseAccessPage.Contains("Banking;"))
            {
                chkBanking.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Banking;"))
                    chkBanking.Visible = false;
            }

            if (accessPage.Contains("Offer;") && roleBaseAccessPage.Contains("Offer;"))
            {
                chkOffer.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Offer;"))
                    chkOffer.Visible = false;
            }

            // Promotion
            if (accessPage.Contains("Offer;") && roleBaseAccessPage.Contains("Offer;"))
            {
                chkOffer.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Offer;"))
                    chkOffer.Visible = false;
            }

            if (accessPage.Contains("SMS;") && roleBaseAccessPage.Contains("SMS;"))
            {
                chkSMS.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("SMS;"))
                    chkSMS.Visible = false;
            }

            // Reports
            if (accessPage.Contains("Dashboard;") && roleBaseAccessPage.Contains("Dashboard;"))
            {
                chkDashboard.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Dashboard;"))
                    chkDashboard.Visible = false;
            }
            if (accessPage.Contains("Slip;") && roleBaseAccessPage.Contains("Slip;"))
            {
                chkSlip.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Slip;"))
                    chkSlip.Visible = false;
            }


            if (accessPage.Contains("Slip;") && roleBaseAccessPage.Contains("Slip;"))
            {
                chkSlip.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Slip;"))
                    chkSlip.Visible = false;
            }
            if (accessPage.Contains("StockReport;") && roleBaseAccessPage.Contains("StockReport;"))
            {
                chkStockReport.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("StockReport;"))
                    chkStockReport.Visible = false;
            }

            if (accessPage.Contains("PurchaseReport;") && roleBaseAccessPage.Contains("PurchaseReport;"))
            {
                chkPurchaseReport.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("PurchaseReport;"))
                    chkPurchaseReport.Visible = false;
            }

            if (accessPage.Contains("InventoryReport;") && roleBaseAccessPage.Contains("InventoryReport;"))
            {
                chkInventoryReport.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("InventoryReport;"))
                    chkInventoryReport.Visible = false;
            }

            if (accessPage.Contains("SupplierCommission;") && roleBaseAccessPage.Contains("SupplierCommission;"))
            {
                chkSupplierCommission.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("SupplierCommission;"))
                    chkSupplierCommission.Visible = false;
            }

            if (accessPage.Contains("Transaction;") && roleBaseAccessPage.Contains("Transaction;"))
            {
                chkTransaction.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Transaction;"))
                    chkTransaction.Visible = false;
            }

            if (accessPage.Contains("ProfitLoss;") && roleBaseAccessPage.Contains("ProfitLoss;"))
            {
                chkProfitLoss.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("ProfitLoss;"))
                    chkProfitLoss.Visible = false;
            }


            if (accessPage.Contains("Analytic;") && roleBaseAccessPage.Contains("Analytic;"))
            {
                chkAnalytic.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Analytic;"))
                    chkAnalytic.Visible = false;
            }

            if (accessPage.Contains("Summary;") && roleBaseAccessPage.Contains("Summary;"))
            {
                chkSummary.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Summary;"))
                    chkSummary.Visible = false;
            }


            // Website
            if (accessPage.Contains("Ecommerce;") && roleBaseAccessPage.Contains("Ecommerce;"))
            {
                chkEcommerce.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Ecommerce;"))
                    chkEcommerce.Visible = false;
            }


            // Records
            if (accessPage.Contains("Store;") && roleBaseAccessPage.Contains("Store;"))
            {
                chkStore.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Store;"))
                    chkStore.Visible = false;
            }

            if (accessPage.Contains("Manufacturer;") && roleBaseAccessPage.Contains("Manufacturer;"))
            {
                chkManufacturer.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Manufacturer;"))
                    chkManufacturer.Visible = false;
            }

            if (accessPage.Contains("Location;") && roleBaseAccessPage.Contains("Location;"))
            {
                chkLocation.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Location;"))
                    chkLocation.Visible = false;
            }


            if (accessPage.Contains("Supplier;") && roleBaseAccessPage.Contains("Supplier;"))
            {
                chkSupplier.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Supplier;"))
                    chkSupplier.Visible = false;
            }

            if (accessPage.Contains("Category;") && roleBaseAccessPage.Contains("Category;"))
            {
                chkCategory.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Category;"))
                    chkCategory.Visible = false;
            }

            if (accessPage.Contains("UnitMeasurement;") && roleBaseAccessPage.Contains("UnitMeasurement;"))
            {
                chkUnitMeasurement.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("UnitMeasurement;"))
                    chkUnitMeasurement.Visible = false;
            }

            if (accessPage.Contains("Field;") && roleBaseAccessPage.Contains("Field;"))
            {
                chkField.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Field;"))
                    chkField.Visible = false;
            }

            if (accessPage.Contains("Attribute;") && roleBaseAccessPage.Contains("Attribute;"))
            {
                chkAttribute.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Attribute;"))
                    chkAttribute.Visible = false;
            }

            if (accessPage.Contains("Bank;") && roleBaseAccessPage.Contains("Bank;"))
            {
                chkBank.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Bank;"))
                    chkBank.Visible = false;
            }

            if (accessPage.Contains("Card;") && roleBaseAccessPage.Contains("Card;"))
            {
                chkCard.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Card;"))
                    chkCard.Visible = false;
            }


            if (accessPage.Contains("Particular;") && roleBaseAccessPage.Contains("Particular;"))
            {
                chkParticular.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Particular;"))
                    chkParticular.Visible = false;
            }

            if (accessPage.Contains("Staff;") && roleBaseAccessPage.Contains("Staff;"))
            {
                chkStaff.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Staff;"))
                    chkStaff.Visible = false;
            }

            if (accessPage.Contains("ServiceType;") && roleBaseAccessPage.Contains("ServiceType;"))
            {
                chkServiceType.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("ServiceType;"))
                    chkServiceType.Visible = false;
            }


            // Setting
            if (accessPage.Contains("Offline;") && roleBaseAccessPage.Contains("Offline;"))
            {
                chkServiceType.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Offline;"))
                    chkServiceType.Visible = false;
            }


            // Operation
            if (accessPage.Contains("Add;") && roleBaseAccessPage.Contains("Add;"))
            {
                chkAdd.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Add;"))
                    chkAdd.Visible = false;
            }

            if (accessPage.Contains("Edit;") && roleBaseAccessPage.Contains("Edit;"))
            {
                chkEdit.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Edit;"))
                    chkEdit.Visible = false;
            }

            if (accessPage.Contains("Delete;") && roleBaseAccessPage.Contains("Delete;"))
            {
                chkDelete.Checked = true;
            }
            else
            {
                if (!roleBaseAccessPage.Contains("Delete;"))
                    chkDelete.Visible = false;
            }
        }

        private string GetAccessPage()
        {
            string accessPage = "";


            // Inventory
            if (chkPurchase.Checked)
                accessPage += "Purchase; ";

            if (chkStock.Checked)
                accessPage += "Stock; ";

            if (chkPackage.Checked)
                accessPage += "Package; ";

            if (chkWarranty.Checked)
                accessPage += "Warranty; ";

            if (chkReturn.Checked)
                accessPage += "Return; ";

            if (chkDamage.Checked)
                accessPage += "Damage; ";

            if (chkCancel.Checked)
                accessPage += "Cancel; ";

            if (chkWarning.Checked)
                accessPage += "Warning; ";

            if (chkExpiry.Checked)
                accessPage += "Expiry; ";

            if (chkImport.Checked)
                accessPage += "Import; ";


            // Sales
            if (chkInvoice.Checked)
                accessPage += "Invoice; ";

            if (chkCustomer.Checked)
                accessPage += "Customer; ";

            if (chkQuotation.Checked)
                accessPage += "Quotation; ";

            if (chkServicing.Checked)
                accessPage += "Servicing; ";

            if (chkDueReminder.Checked)
                accessPage += "DueReminder; ";

            if (chkService.Checked)
                accessPage += "Service; ";

            if (chkToken.Checked)
                accessPage += "Token; ";


            // Accounting
            if (chkSupply.Checked)
                accessPage += "Supply; ";

            if (chkReceive.Checked)
                accessPage += "Receive; ";

            if (chkSalary.Checked)
                accessPage += "Salary; ";

            if (chkExpense.Checked)
                accessPage += "Expense; ";

            if (chkBanking.Checked)
                accessPage += "Banking; ";


            // Promotion
            if (chkOffer.Checked)
                accessPage += "Offer; ";

            if (chkSMS.Checked)
                accessPage += "SMS; ";


            // Reports
            if (chkDashboard.Checked)
                accessPage += "Dashboard; ";

            if (chkSlip.Checked)
                accessPage += "Slip; ";

            if (chkStockReport.Checked)
                accessPage += "StockReport; ";

            if (chkPurchaseReport.Checked)
                accessPage += "PurchaseReport; ";

            if (chkSlip.Checked)
                accessPage += "Slip; ";

            if (chkInventoryReport.Checked)
                accessPage += "InventoryReport; ";

            if (chkSupplierCommission.Checked)
                accessPage += "SupplierCommission; ";

            if (chkTransaction.Checked)
                accessPage += "Transaction; ";

            if (chkProfitLoss.Checked)
                accessPage += "ProfitLoss; ";

            if (chkAnalytic.Checked)
                accessPage += "Analytic; ";

            if (chkSummary.Checked)
                accessPage += "Summary; ";


            // Website
            if (chkEcommerce.Checked)
                accessPage += "Ecommerce; ";


            // Records
            if (chkStore.Checked)
                accessPage += "Store; ";

            if (chkManufacturer.Checked)
                accessPage += "Manufacturer; ";

            if (chkLocation.Checked)
                accessPage += "Location; ";

            if (chkSupplier.Checked)
                accessPage += "Supplier; ";

            if (chkCategory.Checked)
                accessPage += "Category; ";

            if (chkUnitMeasurement.Checked)
                accessPage += "UnitMeasurement; ";

            if (chkField.Checked)
                accessPage += "Field; ";

            if (chkAttribute.Checked)
                accessPage += "Attribute; ";

            if (chkBank.Checked)
                accessPage += "Bank; ";

            if (chkCard.Checked)
                accessPage += "Card; ";

            if (chkParticular.Checked)
                accessPage += "Particular; ";

            if (chkStaff.Checked)
                accessPage += "Staff; ";

            if (chkServiceType.Checked)
                accessPage += "ServiceType; ";


            // Setting
            if (chkOffline.Checked)
                accessPage += "Offline; ";


            // Operation
            if (chkAdd.Checked)
                accessPage += "Add; ";

            if (chkEdit.Checked)
                accessPage += "Edit; ";

            if (chkDelete.Checked)
                accessPage += "Delete; ";

            // 
            if (Session["userRight"].ToString() != "Branch")
            {
                accessPage += "User; ";
            }

            accessPage += "Subscription; Profile; Security; Version; Docs; ";

            return accessPage;
        }




        private void checkAccessPageRight()
        {
            var userRight = Session["userRight"].ToString();
            if (userRight == "Group")
            {

            }
            else if (userRight == "Branch")
            {

            }
            else if (userRight == "Regular")
            {

            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                scriptMessage("Name is Required.", MessageType.Warning);
                return;
            }


            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                scriptMessage("Email is Required.", MessageType.Warning);
                return;
            }


            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                scriptMessage("Password is Required.", MessageType.Warning);
                return;
            }

            if (Request["id"] == null)
            {
                if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    scriptMessage("Password is Required.", MessageType.Warning);
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    scriptMessage("Password is not match.", MessageType.Warning);
                    return;
                }
            }


            if (string.IsNullOrWhiteSpace(txtBranchLimit.Text))
            {
                txtBranchLimit.Text = "1";
            }

            if (string.IsNullOrWhiteSpace(txtUserLimit.Text))
            {
                txtUserLimit.Text = "1";
            }

            if (string.IsNullOrWhiteSpace(txtSubscriptionFee.Text))
            {
                txtSubscriptionFee.Text = "0.00";
            }

            if (string.IsNullOrWhiteSpace(txtExpiryDate.Text))
            {
                txtExpiryDate.Text = commonFunction.GetCurrentTime().AddDays(30).ToString("dd-MMM-yyyy");
            }

            string msg = "";
            var userService = new UserService();
            if (Request["id"] != null)
            {
                var roleId = Request["id"].ToString();

                // Update
                userService.title = txtTitle.Text;
                userService.email = txtEmail.Text;
                userService.password = commonFunction.Encrypt(txtPassword.Text);
                userService.branchLimit = txtBranchLimit.Text;
                userService.userLimit = txtUserLimit.Text;
                userService.subscriptionFee = Convert.ToDecimal(txtSubscriptionFee.Text);
                userService.expiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                userService.accessPage = GetAccessPage();
                userService.roleId = Convert.ToInt32(roleId);
                userService.storeId = Convert.ToInt32(ddlStoreList.SelectedValue);
                msg = userService.UpdateUserData();

            }
            else
            {
                // generate roleId
                var roleId = commonFunction.getLastRoleIdIncrement();

                // Set user right
                var storeId = "0";
                var userRight = Session["userRight"].ToString();
                var createdUserRight = "";
                if (userRight == "Super")
                {
                    createdUserRight = "Group";
                    userService.groupId = 0;
                    userService.branchId = 0;
                }
                else if (userRight == "Group")
                {
                    createdUserRight = "Branch";
                    // create store and get storeId
                    var sqlOperation = new SqlOperation();
                    string queryMainStore = "INSERT INTO WarehouseInfo (name, entryDate,updateDate,roleID,active,type) VALUES ('Main Store','" + commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "','" + roleId + "','1','main'); SELECT SCOPE_IDENTITY() as newId";
                    storeId = sqlOperation.executeQueryScalar(queryMainStore);

                    userService.groupId = Convert.ToInt32(Session["roleId"].ToString());
                    userService.branchId = 0;
                }
                else if (userRight == "Branch")
                {
                    createdUserRight = "Regular";
                    storeId = ddlStoreList.SelectedValue;

                    userService.groupId = Convert.ToInt32(Session["groupId"].ToString());
                    userService.branchId = Convert.ToInt32(Session["roleId"].ToString());

                    if (ddlStoreList.SelectedValue == "0")
                    {
                        scriptMessage("Select a store", MessageType.Warning);
                        return;
                    }
                }

                // Insert
                userService.version = Convert.ToDecimal(Session["version"]);
                userService.userRight = createdUserRight;
                userService.title = txtTitle.Text;
                userService.email = txtEmail.Text;
                userService.password = commonFunction.Encrypt(txtPassword.Text);
                userService.branchLimit = txtBranchLimit.Text;
                userService.userLimit = txtUserLimit.Text;
                userService.subscriptionFee = Convert.ToDecimal(txtSubscriptionFee.Text);
                userService.expiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                userService.accessPage = GetAccessPage();
                userService.storeId = Convert.ToInt32(storeId);
                userService.roleId = Convert.ToInt32(roleId);

                msg = userService.SaveUserData();
            }

            if (msg.Contains("false"))
            {
                scriptMessage("Something is wrong", MessageType.Warning);
            }
            else
            {
                scriptMessage("Saved Successfully", MessageType.Success);
                reset();

                System.Threading.Thread.Sleep(1000);
                Response.Redirect("/admin/user?msg=success");
            }
        }



        private void reset()
        {
            txtTitle.Text = "";
            txtEmail.Text = "";
            txtBranchLimit.Text = "";
            txtUserLimit.Text = "";
            txtPassword.Text = "";
            txtSubscriptionFee.Text = "";
            txtExpiryDate.Text = "";

            chkPurchase.Checked = false;
            chkStock.Checked = false;
            chkPackage.Checked = false;
            chkWarranty.Checked = false;
            chkReturn.Checked = false;
            chkDamage.Checked = false;
            chkCancel.Checked = false;
            chkWarning.Checked = false;
            chkExpiry.Checked = false;
            chkImport.Checked = false;

            chkInvoice.Checked = false;
            chkCustomer.Checked = false;
            chkQuotation.Checked = false;
            chkServicing.Checked = false;
            chkDueReminder.Checked = false;
            chkService.Checked = false;
            chkToken.Checked = false;

            chkSupply.Checked = false;
            chkReceive.Checked = false;
            chkSalary.Checked = false;
            chkExpense.Checked = false;
            chkBanking.Checked = false;

            chkOffer.Checked = false;
            chkSMS.Checked = false;

            chkDashboard.Checked = false;
            chkSlip.Checked = false;
            chkStockReport.Checked = false;
            chkPurchaseReport.Checked = false;
            chkInventoryReport.Checked = false;
            chkSupplierCommission.Checked = false;
            chkTransaction.Checked = false;
            chkProfitLoss.Checked = false;
            chkAnalytic.Checked = false;
            chkSummary.Checked = false;

            chkEcommerce.Checked = false;

            chkStore.Checked = false;
            chkManufacturer.Checked = false;
            chkLocation.Checked = false;
            chkSupplier.Checked = false;
            chkCategory.Checked = false;
            chkUnitMeasurement.Checked = false;
            chkField.Checked = false;
            chkAttribute.Checked = false;
            chkBank.Checked = false;
            chkCard.Checked = false;
            chkParticular.Checked = false;
            chkStaff.Checked = false;
            chkServiceType.Checked = false;

            chkOffline.Checked = false;

            chkAdd.Checked = false;
            chkEdit.Checked = false;
            chkDelete.Checked = false;

        }



        public void scriptMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + message + "','" + type + "');", true);

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "scrollKey",
            //    "scrollTo(0, 0);", true);
        }

        public enum MessageType
        {
            Success,
            Error,
            Info,
            Warning
        };





       

    }
}
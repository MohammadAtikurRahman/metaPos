using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.SettingBundle.Service;


namespace MetaPOS.Admin.SettingBundle.View
{


    public partial class Support :BasePage
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Support"))
                {
                    commonFunction.pageout();
                }

                checkSetting(Session["roleId"].ToString());
            }

           
        }





        public void scriptMessage(string msg)
        {
            string title = "Notification Area";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), title, "alert('" + msg + "');", true);
        }




        protected void btnDeleteData_Click(object sender, EventArgs e)
        {


            string query = clearDataFromDatabase();
            var isExecute = sqlOperation.fireQuery(query);
            ResetDeletedCheckBox();
            if (isExecute)
                scriptMessage("Full Data Cleared Successfully.");
            else
                scriptMessage("Your Data is not clear.");
        }

        private string clearDataFromDatabase()
        {
            string query = "BEGIN TRANSACTION ";

            if (chkStock.Checked)
            {
                query += "BEGIN Delete from StockInfo END ";
                query += "BEGIN Delete from QtyManagement END ";
            }

            if (chkStockStatus.Checked)
                query += "BEGIN Delete from StockStatusInfo END ";

            if (chkPackage.Checked)
                query += "BEGIN Delete from PackageInfo END ";

            if (chkReturn.Checked)
                query += "BEGIN Delete from StockStatusInfo where status='stockReturn' END ";

            if (chkDamage.Checked)
                query += "BEGIN Delete from StockStatusInfo where status='Damage' OR status='damage' END ";

            if (chkInvoice.Checked)
                query += "BEGIN Delete from SaleInfo END ";

            if (chkCustomer.Checked)
                query += "BEGIN Delete from CustomerInfo END ";

            if (chkService.Checked)
                query += "BEGIN Delete from ServiceInfo END ";

            if (chkQuotation.Checked)
                query += "BEGIN Delete from QuotationInfo END ";

            if (chkServcing.Checked)
                query += "BEGIN Delete from ServicingInfo END ";

            if (chkInstallment.Checked)
                query += "BEGIN Delete from CustomerReminderInfo END ";

            if (chkToken.Checked)
                query += "BEGIN update SaleInfo set token = '' END ";

            if (chkSupplier.Checked)
                query += "BEGIN Delete from SupplierInfo END ";

            if (chkReceive.Checked)
                query += "BEGIN Delete from CashReportInfo where status='3' END ";

            if (chkExpense.Checked)
                query += "BEGIN delete CashReportInfo where status='2' END ";

            if (chkSalary.Checked)
                query += "BEGIN delete CashReportInfo where status='1' END ";

            if (chkBanking.Checked)
                query += "BEGIN delete CashReportInfo where status='4' END ";

            //Report
            if (chkPurchaseReport.Checked)
                query += "BEGIN delete StockStatusInfo where searchType='product' and purchaseCode !='' END ";

            if (chkSlip.Checked)
                query += "BEGIN delete slipInfo END ";

            if (chkInventoryReport.Checked)
                query += "BEGIN delete stockstatusinfo where status='stock' END ";

            if (chkTransactionReport.Checked)
                query += "BEGIN delete cashReportInfo END ";


            // Records
            if (chkStore.Checked)
                query += "BEGIN delete warehouseInfo where type !='main' END ";

            if (chkLocation.Checked)
                query += "BEGIN delete locationInfo END ";

            if (chkManufacturer.Checked)
                query += "BEGIN delete ManufacturerInfo END ";

            if (chkSupplier.Checked)
                query += "BEGIN delete Supplierinfo END ";

            if (chkCategory.Checked)
                query += "BEGIN delete CategoryInfo END ";

            if (chkMeasurement.Checked)
                query += "BEGIN delete UnitInfo END ";

            if (chkField.Checked)
                query += "BEGIN delete FieldInfo END ";

            if (chkAttribute.Checked)
                query += "BEGIN delete AttributeInfo END BEGIN delete AttributeDetailInfo END ";

            if (chkBank.Checked)
                query += "BEGIN delete BankNameInfo END ";

            if (chkCard.Checked)
                query += "BEGIN delete cardTypeInfo END ";

            if (chkParticular.Checked)
                query += "BEGIN delete CashCatInfo END ";

            if (chkStaff.Checked)
                query += "BEGIN delete StaffInfo END ";

            if (chkServiceType.Checked)
                query += "BEGIN delete ServiceInfo END ";

            // User
            if (chkBranch.Checked)
                query += "BEGIN Delete BranchInfo END BEGIN Delete roleInfo where userRight='Group' AND userRight='Super' END ";
            if (chkSubscription.Checked)
                query += "BEGIN Delete SubscriptionInfo END ";
            if (chkSmsConfig.Checked)
                query += "BEGIN delete SmsConfigInfo END ";


            query += " COMMIT";


            return query;
        }



        private void ResetDeletedCheckBox()
        {
            chkStock.Checked = false;
            chkStockStatus.Checked = false;
            chkPackage.Checked = false;
            chkReturn.Checked = false;
            chkDamage.Checked = false;
            chkInvoice.Checked = false;
            chkInvoice.Checked = false;
            chkService.Checked = false;
            chkQuotation.Checked = false;
            chkServcing.Checked = false;
            chkInstallment.Checked = false;
            chkToken.Checked = false;
            chkSupply.Checked = false;
            chkReceive.Checked = false;
            chkExpense.Checked = false;
            chkSalary.Checked = false;
            chkBanking.Checked = false;
            chkPurchaseReport.Checked = false;
            chkSlip.Checked = false;
            chkInventoryReport.Checked = false;
            chkTransactionReport.Checked = false;
            chkStore.Checked = false;
            chkLocation.Checked = false;
            chkManufacturer.Checked = false;
            chkSupplier.Checked = false;
            chkMeasurement.Checked = false;
            chkField.Checked = false;
            chkAttribute.Checked = false;
            chkBank.Checked = false;
            chkCard.Checked = false;
            chkParticular.Checked = false;
            chkStaff.Checked = false;
            chkCustomer.Checked = false;
            chkCategory.Checked = false;
            chkServiceType.Checked = false;
            chkInventoryAll.Checked = false;
            chkSalesAll.Checked = false;
            chkAccountingAll.Checked = false;
            chkReportAll.Checked = false;
            chkRecordAll.Checked = false;
            
        }


        protected void chkInventoryAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkInventoryAll.Checked)
            {
                chkStock.Checked = true;
                chkStockStatus.Checked = true;
                chkPackage.Checked = true;
                chkReturn.Checked = true;
                chkDamage.Checked = true;
            }
            else
            {
                chkStock.Checked = false;
                chkStockStatus.Checked = false;
                chkPackage.Checked = false;
                chkReturn.Checked = false;
                chkDamage.Checked = false;
            }

        }



        protected void chkSalesAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSalesAll.Checked)
            {
                chkInvoice.Checked = true;
                chkCustomer.Checked = true;
                chkService.Checked = true;
                chkQuotation.Checked = true;
                chkServcing.Checked = true;
                chkInstallment.Checked = true;
                chkToken.Checked = true;
            }
            else
            {
                chkInvoice.Checked = false;
                chkCustomer.Checked = false;
                chkService.Checked = false;
                chkQuotation.Checked = false;
                chkServcing.Checked = false;
                chkInstallment.Checked = false;
                chkToken.Checked = false;
            }
        }

        protected void chkAccountingAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAccountingAll.Checked)
            {
                chkSupply.Checked = true;
                chkReceive.Checked = true;
                chkExpense.Checked = true;
                chkSalary.Checked = true;
                chkBanking.Checked = true;
            }
            else
            {
                chkSupply.Checked = false;
                chkReceive.Checked = false;
                chkExpense.Checked = false;
                chkSalary.Checked = false;
                chkBanking.Checked = false;
            }
        }

        protected void chkReportAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkReportAll.Checked)
            {
                chkPurchaseReport.Checked = true;
                chkSlip.Checked = true;
                chkInventoryReport.Checked = true;
                chkTransactionReport.Checked = true;
            }
            else
            {
                chkPurchaseReport.Checked = false;
                chkSlip.Checked = false;
                chkInventoryReport.Checked = false;
                chkTransactionReport.Checked = false;
            }
        }

        protected void chkRecordAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkRecordAll.Checked)
            {
                chkStore.Checked = true;
                chkLocation.Checked = true;
                chkManufacturer.Checked = true;
                chkSupplier.Checked = true;
                chkCategory.Checked = true;
                chkMeasurement.Checked = true;
                chkField.Checked = true;
                chkAttribute.Checked = true;
                chkBank.Checked = true;
                chkCard.Checked = true;
                chkParticular.Checked = true;
                chkStaff.Checked = true;
                chkServiceType.Checked = true;
            }
            else
            {
                chkStore.Checked = false;
                chkLocation.Checked = false;
                chkManufacturer.Checked = false;
                chkSupplier.Checked = false;
                chkCategory.Checked = false;
                chkMeasurement.Checked = false;
                chkField.Checked = false;
                chkAttribute.Checked = false;
                chkBank.Checked = false;
                chkCard.Checked = false;
                chkParticular.Checked = false;
                chkStaff.Checked = false;
                chkServiceType.Checked = false;
            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtQuery.Text))
            {
                lblMessage.Text = "Query execute sucessfully.";
                return;
            }

            try
            {
                string query = txtQuery.Text;
                SqlDataSource dsUser = new SqlDataSource();
                dsUser.ID = "dsUser";
                this.Page.Controls.Add(dsUser);
                var constr = GlobalVariable.getConnectionStringName();
                dsUser.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
                dsUser.SelectCommand = query;
                grdDeveloper.DataSource = dsUser;
                grdDeveloper.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "error|" + ex.Message;
            }
            
        }

        private void checkSetting(string groupId)
        {

            try
            {
                var branchId = "";
                var dtBranch = sqlOperation.getDataTable("SELECT * FROM [roleInfo] WHERE groupId='" + groupId + "' ");
                if (dtBranch.Rows.Count > 0)
                    branchId = dtBranch.Rows[0]["roleId"].ToString();

                var dtSetting = sqlOperation.getDataTable("SELECT * FROM [SettingInfo] WHERE id='" + branchId + "' ");

                if (dtSetting.Rows.Count <= 0)
                    return;

                // Set data to variable
                paymentMode.SelectedValue = dtSetting.Rows[0]["paymentMode"].ToString();

            }
            catch (Exception)
            {
                 
            }
            
        }

        protected void paymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var groupId = Session["roleId"].ToString();

            RadioButtonList rbl = sender as RadioButtonList;
            string value = rbl.SelectedValue;

            var  supportService = new SupportService();
            supportService.groupId = groupId;
            supportService.value = value;
            supportService.changePaymentMode();
        }


    }


    //BEGIN Delete from StockInfo END
    //BEGIN Delete from StockStatusInfo END
    //BEGIN Delete from PackageInfo END
    //BEGIN Delete from StockStatusInfo where status='stockReturn' END
    //BEGIN Delete from StockStatusInfo where status='Damage' OR status = 'damage' END
    //BEGIN Delete from SaleInfo END
    //BEGIN Delete from CustomerInfo END
    //BEGIN Delete from ServiceInfo END
    //BEGIN Delete from QuotationInfo END
    //BEGIN Delete from ServicingInfo END
    //BEGIN Delete from CustomerReminderInfo END
    //BEGIN update SaleInfo set token = '' END
    //BEGIN Delete from SupplierInfo END
    //BEGIN Delete from CashReportInfo where status = '3' END
    //BEGIN delete CashReportInfo where status = '2' END
    //BEGIN delete CashReportInfo where status = '1' END
    //BEGIN delete CashReportInfo where status = '4' END
    //BEGIN delete StockStatusInfo where searchType = 'product' and purchaseCode !='' END
    //BEGIN delete slipInfo END
    //BEGIN delete stockstatusinfo where status = 'stock' END
    //BEGIN delete cashReportInfo END
    //BEGIN delete warehouseInfo where type !='main' END
    //BEGIN delete locationInfo END
    //BEGIN delete ManufacturerInfo END
    //BEGIN delete Supplierinfo END
    //BEGIN delete CategoryInfo END
    //BEGIN delete UnitInfo END
    //BEGIN delete FieldInfo END
    //BEGIN delete AttributeInfo END BEGIN delete AttributeDetailInfo END
    //BEGIN delete bankInfo END
    //BEGIN delete cardInfo END
    //BEGIN delete CashCatInfo END
    //BEGIN delete StaffInfo END
    //BEGIN delete ServiceInfo END
    //BEGIN Delete SubscriptionInfo END
    //BEGIN delete SmsConfigInfo END



}
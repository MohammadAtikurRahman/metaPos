using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
//using AttributeRouting.Helpers;
using MetaPOS.Admin.SettingBundle.Service;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.SettingBundle.View
{


    public partial class Setting : BasePage
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        private string settingBranchId = "0";




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Setting"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }
                //commonFunction.fillAllDdl(ddlBranchSetting,
                //"SELECT * FROM RoleInfo where groupId='" + Session["roleId"] + "' and branchId ='0' " + Session["userAccessParameters"] + " AND active=1 ", "title", "roleId");
                //ddlBranchSetting.Items.Insert(0, new ListItem(" -- SELECT -- ", ""));

                if (Session["userRight"].ToString() == "Branch")
                {
                    settingBranchId = Session["roleId"].ToString();
                }
                else if (Session["userRight"].ToString() == "Regular")
                {
                    settingBranchId = Session["branchId"].ToString();
                }
                lblHiddenBranchId.Value = settingBranchId;

                checkSetting(settingBranchId);
            }


        }





        private void checkSetting(string branchId)
        {

            
            var dtSetting = sqlOperation.getDataTable("SELECT * FROM [SettingInfo] WHERE id='" + branchId + "' ");

            if (dtSetting.Rows.Count <= 0)
                return;


            //Id,SalesPrintingTime,SalesPrintPageType,barCodeType,updateDate,salesProductEditable,stockBarCodeScanning,isProductSizeVisible,cardDiscount,offer,sku,tax,receipt,sMail,warranty,imei,billDateEdit,token,isPaidWatermark,isCategoryProduct,isWarningQty,isWholeSeller,isAdvanced,searchBy,printWithAutoSave,quotation,apiEcomm,bloodGroup,Phone2,membershipId,quotationNotification,cusDateWiseSearch,printPayHistory,isUnicode,isExistProductName,isOpeningDue,isImport,customField,manufacturer,upload,shipment,size,paydate,timeZone,isUnit,isWarehouse,isRecivedDate,isExpiryDate,isBatchNo,isSerialNo,isSupplierCom,isPurchaseDate,isSearchQty,isMiscCost,isGodownInvoice,isCartCategory,isCartSupplier,displayCartTotalQty,displaySchedulePayment,displaySupplierReceived,displayDiscountInSale,generateBardCodeType,isPrintImei,isPrintWarranty,isGoAction,isPrintSupplier,isPrintCategory,isDueAdjustment,isPurchasePayment,displayBuyPrice,autoSalesPerson,displayCartBuyPrice,displayCartRetailPrice,displayCartWholesalePrice,printChangeAmt,printReturnQty,isInvoiceWord,isCustomerRequired,isVatSale,isVatPrint,displayPaperSizeSelectOption,isGasCylinderAvail,sendInvoiceBySms,isEngineNumber,isCecishNumber,isBetaInvoicePrint,isCustomerReminder,displayInterest,displayExtraDiscount,displayTransfer,displayGuarantor,displayInstalment,displaySearchCustomer,displayAddCustomer,printBarcodeFor,displayService,displayComPrice,displayLocation,dbranding,displayBuyPriceForUser,isDeliveryStatus,displayLogoPrint,displayBrandNamePrint,displayPrintAllStock,noBarcode,displayDiscountPrint,displayDeliveryStatus,displayInstallmentMessage,invoiceWiseSearchProduct,smallPrintPaperWidth,saveMiscCostToExpense,stayAfterSearchProduct,displayVariant,isMultipay


            
            // Set data to variable
            SalesPrintingTime.Checked = dtSetting.Rows[0]["SalesPrintingTime"].ToString() == "1" ? true : false;
            SalesPrintPageType.SelectedValue = dtSetting.Rows[0]["SalesPrintPageType"].ToString();
            barCodeType.SelectedValue = dtSetting.Rows[0]["barCodeType"].ToString();
            salesProductEditable.Checked = dtSetting.Rows[0]["salesProductEditable"].ToString() == "1" ? true : false;
            stockBarCodeScanning.Checked = dtSetting.Rows[0]["stockBarCodeScanning"].ToString() == "1" ? true : false;
            cardDiscount.SelectedValue = dtSetting.Rows[0][8].ToString();
            offer.Checked = dtSetting.Rows[0]["offer"].ToString() == "1" ? true : false;
            sku.Checked = dtSetting.Rows[0]["sku"].ToString() == "1" ? true : false;
            tax.Checked = dtSetting.Rows[0]["tax"].ToString() == "1" ? true : false;
            receipt.Checked = dtSetting.Rows[0]["receipt"].ToString() == "1" ? true : false;
            tokenRcpt.Checked = dtSetting.Rows[0]["tokenRcpt"].ToString() == "1" ? true : false;
            sMail.Checked = dtSetting.Rows[0]["sMail"].ToString() == "1" ? true : false;
            warranty.Checked = dtSetting.Rows[0]["warranty"].ToString() == "1" ? true : false;
            isDisplaySerialNo.Checked = dtSetting.Rows[0]["isDisplaySerialNo"].ToString() == "1" ? true : false;
            monthWaseSalesReport.Text = dtSetting.Rows[0]["monthWaseSalesReport"].ToString();
            imei.Checked = dtSetting.Rows[0]["imei"].ToString() == "1" ? true : false;
            billDateEdit.Checked = dtSetting.Rows[0]["billDateEdit"].ToString() == "1" ? true : false;
            token.Checked = dtSetting.Rows[0]["token"].ToString() == "1" ? true : false;
            isPaidWatermark.Checked = dtSetting.Rows[0]["isPaidWatermark"].ToString() == "1" ? true : false;
            isCategoryProduct.SelectedValue = dtSetting.Rows[0]["isCategoryProduct"].ToString();
            isWarningQty.Checked = dtSetting.Rows[0]["isWarningQty"].ToString() == "1" ? true : false;
            isWholeSeller.Checked = dtSetting.Rows[0]["isWholeSeller"].ToString() == "1" ? true : false;
            isAdvanced.Checked = dtSetting.Rows[0]["isAdvanced"].ToString() == "1" ? true : false;
            searchBy.Checked = dtSetting.Rows[0]["searchBy"].ToString() == "1" ? true : false; 
            printWithAutoSave.Checked = dtSetting.Rows[0]["printWithAutoSave"].ToString() == "1" ? true : false;
            quotation.Checked = dtSetting.Rows[0]["quotation"].ToString() == "1" ? true : false;
            apiEcomm.Checked = dtSetting.Rows[0]["apiEcomm"].ToString() == "1" ? true : false;
            bloodGroup.Checked = dtSetting.Rows[0]["bloodGroup"].ToString() == "1" ? true : false;
            phone2.Checked = dtSetting.Rows[0]["phone2"].ToString() == "1" ? true : false;
            membershipId.Checked = dtSetting.Rows[0]["membershipId"].ToString() == "1" ? true : false;
            quotationNotification.Checked = dtSetting.Rows[0]["quotationNotification"].ToString() == "1" ? true : false;
            cusDateWiseSearch.Checked = dtSetting.Rows[0]["cusDateWiseSearch"].ToString() == "1" ? true : false; 
            printPayHistory.Checked = dtSetting.Rows[0]["printPayHistory"].ToString() == "1" ? true : false;
            isUnicode.SelectedValue = dtSetting.Rows[0]["isUnicode"].ToString();
            isExistProductName.Checked = dtSetting.Rows[0]["isExistProductName"].ToString() == "1" ? true : false;
            isOpeningDue.Checked = dtSetting.Rows[0]["isOpeningDue"].ToString() == "1" ? true : false;
            isImport.Checked = dtSetting.Rows[0]["isImport"].ToString() == "1" ? true : false;
            customField.Checked = dtSetting.Rows[0]["customField"].ToString() == "1" ? true : false;
            manufacturer.Checked = dtSetting.Rows[0]["manufacturer"].ToString() == "1" ? true : false;
            upload.Checked = dtSetting.Rows[0]["upload"].ToString() == "1" ? true : false;
            shipment.Checked = dtSetting.Rows[0]["shipment"].ToString() == "1" ? true : false;
            size.Checked = dtSetting.Rows[0]["size"].ToString() == "1" ? true : false;
            paydate.Checked = dtSetting.Rows[0]["paydate"].ToString() == "1" ? true : false; 
            timeZone.SelectedValue = dtSetting.Rows[0]["timeZone"].ToString();
            isUnit.Checked = dtSetting.Rows[0]["isUnit"].ToString() == "1" ? true : false;
            isWarehouse.Checked = dtSetting.Rows[0]["isWarehouse"].ToString() == "1" ? true : false;
            isRecivedDate.Checked = dtSetting.Rows[0]["isRecivedDate"].ToString() == "1" ? true : false;
            isExpiryDate.Checked = dtSetting.Rows[0]["isExpiryDate"].ToString() == "1" ? true : false;
            isBatchNo.Checked = dtSetting.Rows[0]["isBatchNo"].ToString() == "1" ? true : false;
            isSerialNo.Checked = dtSetting.Rows[0]["isSerialNo"].ToString() == "1" ? true : false;
            isSupplierCom.Checked = dtSetting.Rows[0]["isSupplierCom"].ToString() == "1" ? true : false;
            isSearchQty.Checked = dtSetting.Rows[0]["isSearchQty"].ToString() == "1" ? true : false; 
            isMiscCost.Checked = dtSetting.Rows[0]["isMiscCost"].ToString() == "1" ? true : false; 
            isGodownInvoice.SelectedValue = dtSetting.Rows[0]["isGodownInvoice"].ToString();
            isCartCategory.Checked = dtSetting.Rows[0]["isCartCategory"].ToString() == "1" ? true : false; 
            isCartSupplier.Checked = dtSetting.Rows[0]["isCartSupplier"].ToString() == "1" ? true : false;
            isPrintImei.Checked = dtSetting.Rows[0]["isPrintImei"].ToString() == "1" ? true : false; ;
            isPrintWarranty.Checked = dtSetting.Rows[0]["isPrintWarranty"].ToString() == "1" ? true : false; ;
            isGoAction.Checked = dtSetting.Rows[0]["isGoAction"].ToString() == "1" ? true : false;
            isPrintSupplier.Checked = dtSetting.Rows[0]["isPrintSupplier"].ToString() == "1" ? true : false; ;
            isPrintCategory.Checked = dtSetting.Rows[0]["isPrintCategory"].ToString() == "1" ? true : false; ;
            isDueAdjustment.Checked = dtSetting.Rows[0]["isDueAdjustment"].ToString() == "1" ? true : false;
            isPurchasePayment.Checked = dtSetting.Rows[0]["isPurchasePayment"].ToString() == "1" ? true : false;
            displayBuyPrice.Checked = dtSetting.Rows[0]["displayBuyPrice"].ToString() == "1" ? true : false;
            autoSalesPerson.Checked = dtSetting.Rows[0]["autoSalesPerson"].ToString() == "1" ? true : false; ;
            isCartCategory.Checked = dtSetting.Rows[0]["isCartCategory"].ToString() == "1" ? true : false; ;
            displayCartBuyPrice.Checked = dtSetting.Rows[0]["displayCartBuyPrice"].ToString() == "1" ? true : false; ;
            displayCartRetailPrice.Checked = dtSetting.Rows[0]["displayCartRetailPrice"].ToString() == "1" ? true : false; ;
            displayCartWholesalePrice.Checked = dtSetting.Rows[0]["displayCartWholesalePrice"].ToString() == "1" ? true : false; ;
            displayDiscountInSale.Checked = dtSetting.Rows[0]["displayDiscountInSale"].ToString() == "1" ? true : false; ;
            displayCartTotalQty.Checked = dtSetting.Rows[0]["displayCartTotalQty"].ToString() == "1" ? true : false; ;
            displaySchedulePayment.Checked = dtSetting.Rows[0]["displaySchedulePayment"].ToString() == "1" ? true : false;
            displaySupplierReceived.Checked = dtSetting.Rows[0]["displaySupplierReceived"].ToString() == "1" ? true : false;
            generateBardCodeType.Checked = dtSetting.Rows[0]["generateBardCodeType"].ToString() == "1" ? true : false;
            printChangeAmt.Checked = dtSetting.Rows[0]["printChangeAmt"].ToString() == "1" ? true : false; ;
            printReturnQty.Checked = dtSetting.Rows[0]["printReturnQty"].ToString() == "1" ? true : false; ;
            isInvoiceWord.Checked = dtSetting.Rows[0]["isInvoiceWord"].ToString() == "1" ? true : false; ;
            isCustomerRequired.Checked = dtSetting.Rows[0]["isCustomerRequired"].ToString() == "1" ? true : false;
            isVatSale.Checked = dtSetting.Rows[0]["isVatSale"].ToString() == "1" ? true : false;
            isVatPrint.Checked = dtSetting.Rows[0]["isVatPrint"].ToString() == "1" ? true : false; ;
            displayPaperSizeSelectOption.Checked = dtSetting.Rows[0]["displayPaperSizeSelectOption"].ToString() == "1" ? true : false;
            isGasCylinderAvail.Checked = dtSetting.Rows[0]["isGasCylinderAvail"].ToString() == "1" ? true : false;
            sendInvoiceBySms.Checked = dtSetting.Rows[0]["sendInvoiceBySms"].ToString() == "1" ? true : false;
            isEngineNumber.Checked = dtSetting.Rows[0]["isEngineNumber"].ToString() == "1" ? true : false;
            isCecishNumber.Checked = dtSetting.Rows[0]["isCecishNumber"].ToString() == "1" ? true : false;
            isBetaInvoicePrint.Checked = dtSetting.Rows[0]["isBetaInvoicePrint"].ToString() == "1" ? true : false; ;
            displayInterest.Checked = dtSetting.Rows[0]["displayInterest"].ToString() == "1" ? true : false;
            displayExtraDiscount.Checked = dtSetting.Rows[0]["displayExtraDiscount"].ToString() == "1" ? true : false;
            displayTransfer.Checked = dtSetting.Rows[0]["displayTransfer"].ToString() == "1" ? true : false;
            displayInstalment.Checked = dtSetting.Rows[0]["displayInstalment"].ToString() == "1" ? true : false;
            displayGuarantor.Checked = dtSetting.Rows[0]["displayGuarantor"].ToString() == "1" ? true : false;
            displaySearchCustomer.Checked = dtSetting.Rows[0]["displaySearchCustomer"].ToString() == "1" ? true : false;
            displayAddCustomer.Checked = dtSetting.Rows[0]["displayAddCustomer"].ToString() == "1" ? true : false;
            printBarcodeFor.SelectedValue = dtSetting.Rows[0]["printBarcodeFor"].ToString();
            displayService.Checked = dtSetting.Rows[0]["displayService"].ToString() == "1" ? true : false;
            displayComPrice.Checked = dtSetting.Rows[0]["displayComPrice"].ToString() == "1" ? true : false;
            displayLocation.Checked = dtSetting.Rows[0]["displayLocation"].ToString() == "1" ? true : false;
            dbranding.Checked = dtSetting.Rows[0]["dbranding"].ToString() == "1" ? true : false;
            displayBuyPriceForUser.Checked = dtSetting.Rows[0]["displayBuyPriceForUser"].ToString() == "1" ? true : false;
            isDeliveryStatus.Checked = dtSetting.Rows[0]["isDeliveryStatus"].ToString() == "1" ? true : false;
            displayLogoPrint.Checked = dtSetting.Rows[0]["displayLogoPrint"].ToString() == "1" ? true : false; ;
            displayBrandNamePrint.Checked = dtSetting.Rows[0]["displayBrandNamePrint"].ToString() == "1" ? true : false; ;
            displayPrintAllStock.Checked = dtSetting.Rows[0]["displayPrintAllStock"].ToString() == "1" ? true : false;
            noBarcode.Checked = dtSetting.Rows[0]["noBarcode"].ToString() == "1" ? true : false;
            displayDiscountPrint.Checked = dtSetting.Rows[0]["displayDiscountPrint"].ToString() == "1" ? true : false; ;
            displayDeliveryStatus.Checked = dtSetting.Rows[0]["displayDeliveryStatus"].ToString() == "1" ? true : false;
            displayInstallmentMessage.Checked = dtSetting.Rows[0]["displayInstallmentMessage"].ToString() == "1" ? true : false;
            invoiceWiseSearchProduct.Checked = dtSetting.Rows[0]["invoiceWiseSearchProduct"].ToString() == "1" ? true : false;
            saveMiscCostToExpense.Checked = dtSetting.Rows[0]["saveMiscCostToExpense"].ToString() == "1" ? true : false;
            smallPrintPaperWidth.Text = dtSetting.Rows[0]["smallPrintPaperWidth"].ToString();
            stayAfterSearchProduct.Checked = dtSetting.Rows[0]["stayAfterSearchProduct"].ToString() == "1" ? true : false;
            displayVariant.Checked = dtSetting.Rows[0]["displayVariant"].ToString() == "1" ? true : false;
            isMultipay.Checked = dtSetting.Rows[0]["isMultipay"].ToString() == "1" ? true : false;
            isSeparateStore.Checked = dtSetting.Rows[0]["isSeparateStore"].ToString() == "1" ? true : false;
            isCusDesignation.Checked = dtSetting.Rows[0]["isCusDesignation"].ToString() == "1" ? true : false;
            isSendSmsOwnerNumber.Checked = dtSetting.Rows[0]["isSendSmsOwnerNumber"].ToString() == "1" ? true : false;
            isLastQty.Checked = dtSetting.Rows[0]["isLastQty"].ToString() == "1" ? true : false;
            isBalanceQty.Checked = dtSetting.Rows[0]["isBalanceQty"].ToString() == "1" ? true : false;
            isBalanceQty.Checked = dtSetting.Rows[0]["isBalanceQty"].ToString() == "1" ? true : false;
            isReferredBy.Checked = dtSetting.Rows[0]["isReferredBy"].ToString() == "1" ? true : false;
            isCustomerSummaryExport.Checked = dtSetting.Rows[0]["isCustomerSummaryExport"].ToString() == "1" ? true : false;
            autoSelectSearchBy.SelectedValue = dtSetting.Rows[0]["autoSelectSearchBy"].ToString();
            CartDefaultProduct.Text = dtSetting.Rows[0]["CartDefaultProduct"].ToString();
            language.SelectedValue = dtSetting.Rows[0]["language"].ToString();
            businessType.SelectedValue = dtSetting.Rows[0]["businessType"].ToString().Trim();
            challanPaperSize.SelectedValue = dtSetting.Rows[0]["challanPaperSize"].ToString().Trim();
            displayChallanAddress.Checked = dtSetting.Rows[0]["displayChallanAddress"].ToString() == "1" ? true : false;
            displayChallanLocation.Checked = dtSetting.Rows[0]["displayChallanLocation"].ToString() == "1" ? true : false;
            
        }







        [WebMethod]
        public static string updateSettingInfoAction(string jsonStrData)
        {
            var settingService = new SettingService();
            return settingService.updateSettingInfo(jsonStrData);
        }





        protected void ddlBranchSetting_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            checkSetting(settingBranchId);  //  ddlBranchSetting.SelectedValue
        }



    }


}
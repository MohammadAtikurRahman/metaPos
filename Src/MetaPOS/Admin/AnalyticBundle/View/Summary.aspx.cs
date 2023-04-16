using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.AnalyticBundle.Service;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Core.Services;
using MetaPOS.Entities.Dto;
using MetaPOS.Core.Services.Summary;

namespace MetaPOS.Admin.AnalyticBundle.View
{
    public partial class Summary :BasePage //Page
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction objCommonFun = new CommonFunction();

        DateTime dateTimeTo, dateTimeFrom;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Summary"))
                    objCommonFun.pageout();

                try
                {
                    lblCompanyName.Text = Session["comName"].ToString();
                    lblCompanyAddress.Text = Session["comAddress"].ToString();
                    lblCompanyPhone.Text = Session["comPhone"].ToString();
                }
                catch (Exception)
                {

                }

                txtFrom.Text = txtTo.Text = lblDateTime.Text = objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy");

                if (txtFrom.Text == "")
                    dateTimeFrom = objCommonFun.GetCurrentTime();
                else
                    dateTimeFrom = Convert.ToDateTime(txtFrom.Text);


                if (txtTo.Text == "")
                    dateTimeTo = objCommonFun.GetCurrentTime();
                else
                    dateTimeTo = Convert.ToDateTime(txtTo.Text);

                var storeService = new Core.Services.StoreService();
                storeService.loadStoreDropdownList(ddlStoreList);

                Search();
            }
        }



        public void Search()
        {
            if (txtFrom.Text == "")
                dateTimeFrom = objCommonFun.GetCurrentTime();
            else
                dateTimeFrom = Convert.ToDateTime(txtFrom.Text);

            if (txtTo.Text == "")
                dateTimeTo = objCommonFun.GetCurrentTime();
            else
                dateTimeTo = Convert.ToDateTime(txtTo.Text);

            if (chkAnyDate.Checked)
            {
                dateTimeFrom = objCommonFun.GetCurrentTime().AddYears(-10);
                dateTimeTo = objCommonFun.GetCurrentTime().AddYears(2);

                txtTo.Enabled = false;
                txtFrom.Enabled = false;
            }
            else
            {
                txtTo.Enabled = true;
                txtFrom.Enabled = true;
            }

            saleSummery();
            cashSummery();
            AccountSummery();
        }






        public void saleSummery()
        {
            var storeAccessParameters = Session["storeAccessParameters"].ToString();
            if (ddlStoreList.SelectedValue != "0")
                storeAccessParameters = " AND storeId='" + ddlStoreList.SelectedValue + "'";

            var summaryDto = new SearchDto();
            summaryDto.storeAccessParameter = storeAccessParameters;
            summaryDto.From = dateTimeFrom;
            summaryDto.To = dateTimeTo;
            var summaryService = new SalesSummaryService(summaryDto);

            lblInvoiceQty.Text = summaryService.InvoiceTotal();
            lblSalesQtyTotal.Text = summaryService.SalesQtyTotal();
            lblNetAmount.Text = summaryService.NetAmountTotal();

            lblSalesDisc.Text = summaryService.DiscountAmountTotal();
            lblGrossAmt.Text = summaryService.GrossAmountTotal();
            lblRecivedAmt.Text = summaryService.RecivedAmountTotal();
            lblSaleReturnAmt.Text = summaryService.ReturnAmountTotal();
            lblDueAmt.Text = summaryService.DueAmountTotal();
            lblTotalMiscCost.Text = summaryService.MiscellaneousCostTotal();

            //lblSalesQty.Text = summaryService.ItemTotalQty();
            //string storeAccessParameters = "";

            //storeAccessParameters = Session["storeAccessParameters"].ToString();
            //if (ddlStoreList.SelectedValue != "0")
            //    storeAccessParameters = " AND storeId='" + ddlStoreList.SelectedValue + "'";

            //var saleService = new SaleService();
            //var dtSaleSummery = saleService.getSaleSummery(storeAccessParameters, dateTimeFrom, dateTimeTo);



            //string qty = "0", netAmt = "0", discountAmt = "0", grossAmt = "0", recivedAmt = "0", dueAmt = "0";


            //if (dtSaleSummery.Rows[0]["netAmt"].ToString() != "")
            //    netAmt = dtSaleSummery.Rows[0]["netAmt"].ToString();

            //if (dtSaleSummery.Rows[0]["discAmt"].ToString() != "")
            //    discountAmt = dtSaleSummery.Rows[0]["discAmt"].ToString();

            //if (dtSaleSummery.Rows[0]["grossAmt"].ToString() != "")
            //    grossAmt = dtSaleSummery.Rows[0]["grossAmt"].ToString();

            //if (dtSaleSummery.Rows[0]["giftAmt"].ToString() != "")
            //    dueAmt = dtSaleSummery.Rows[0]["giftAmt"].ToString();

            ////loadingCost,caryingCost,uploadingCost,shippingCost,serviceCharge
            //var loadingCost = dtSaleSummery.Rows[0]["loadingCost"].ToString();
            //var carryingCost = dtSaleSummery.Rows[0]["carryingCost"].ToString();
            //var unloadingCost = dtSaleSummery.Rows[0]["unloadingCost"].ToString();
            //var shippingCost = dtSaleSummery.Rows[0]["shippingCost"].ToString();
            //var serviceCharge = dtSaleSummery.Rows[0]["serviceCharge"].ToString();

            //var totalMiscCost = Convert.ToDecimal(loadingCost == "" ? "0" : loadingCost) +
            //                    Convert.ToDecimal(carryingCost == "" ? "0" : carryingCost) +
            //                    Convert.ToDecimal(unloadingCost == "" ? "0" : unloadingCost) +
            //                    Convert.ToDecimal(shippingCost == "" ? "0" : shippingCost) +
            //                    Convert.ToDecimal(serviceCharge == "" ? "0" : serviceCharge);


            //lblNetAmount.Text = netAmt;
            //lblSalesDisc.Text = discountAmt;
            //lblGrossAmt.Text = grossAmt;
            //lblTotalMiscCost.Text = totalMiscCost.ToString("0.00");


            //// Receive amt
            //var receivedAmt = 0M;
            //var cashService = new CashService();
            //var dtCashReceive = cashService.getCashReportSaleRecord(storeAccessParameters, dateTimeFrom, dateTimeTo);
            //if (dtCashReceive.Rows[0]["cashIn"].ToString() != "")
            //{
            //    receivedAmt = Convert.ToDecimal(dtCashReceive.Rows[0]["cashIn"].ToString());
            //}

            //// Sales Return 
            //var returnAmt = 0M;
            //var dtSaleReturn = cashService.getCashReportSaleRecordByCashType(storeAccessParameters, dateTimeFrom,
            //    dateTimeTo, "Sales Return");

            //if (dtSaleReturn.Rows[0]["cashOut"].ToString() != "")
            //    returnAmt = Convert.ToDecimal(dtSaleReturn.Rows[0]["cashOut"].ToString());


            //// Due Amt
            //var openingDueAmt = 0M;
            //var dtDueAmt = cashService.getCashReportSaleRecordByCashType(storeAccessParameters, dateTimeFrom, dateTimeTo, "Opening due");

            //if (dtDueAmt.Rows[0]["cashOut"].ToString() != "")
            //    openingDueAmt = Convert.ToDecimal(dtDueAmt.Rows[0]["cashOut"].ToString());

            //var totalReceivedAmt = receivedAmt - returnAmt;

            ////lblSalesPaid.Text = totalReceivedAmt.ToString();


            //totalReceivedAmt -= openingDueAmt;

            //// Due Amt
            ////lblSalesDue.Text = (Convert.ToDecimal(grossAmt) - totalReceivedAmt).ToString("0.00");

            //// Total Invoice
            //var dtSale = saleService.getSaleRecordInfo(storeAccessParameters, dateTimeFrom, dateTimeTo);

            //string invoiceCounter = "0";
            //if (dtSale.Rows.Count > 0)
            //{
            //    invoiceCounter = dtSale.Rows.Count.ToString();
            //}
            //lblInvoiceQty.Text = invoiceCounter;

            //decimal saleQty = 0M;
            //if (dtSale.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtSale.Rows.Count; i++)
            //    {
            //        saleQty += Convert.ToDecimal(dtSale.Rows[i]["qty"].ToString());
            //    }
            //}

            //lblSalesQty.Text = saleQty.ToString();
        }





        public void cashSummery()
        {
            string storeAccessParameters = "";

            storeAccessParameters = objCommonFun.getStoreAccessParameters("cashReport");
            if (ddlStoreList.SelectedValue != "0")
                storeAccessParameters = " AND cashReport.storeId='" + ddlStoreList.SelectedValue + "'";

            var cashService = new CashService();
            var summaryCashService = new CashSummaryService();
            summaryCashService.To = dateTimeTo;
            summaryCashService.From = dateTimeFrom;
            summaryCashService.PayMethod = 0;
            lblCashinHand.Text = summaryCashService.CashPaymethod(summaryCashService);

            summaryCashService.PayMethod = 1;
            lblCashinCard.Text = summaryCashService.CardPaymethod(summaryCashService);

            summaryCashService.PayMethod = 2;
            lblCashinCheck.Text = summaryCashService.CheckPaymethod(summaryCashService);

            summaryCashService.PayMethod = 3;
            lblCashinbKash.Text = summaryCashService.MobileBankingPaymethod(summaryCashService);

            //summaryCashService.PayMethod = 4;
            //lblDeposit.Text = summaryCashService.cashReportSummaryPayMethod(storeAccessParameters, 4, dateTimeFrom, dateTimeTo);

            summaryCashService.PayMethod = 4;
            lblCashOnDelivery.Text = summaryCashService.CODPaymethod(summaryCashService);
        }





        public void AccountSummery()
        {

            string storeAccessParameters = Session["storeAccessParameters"].ToString();
            if (ddlStoreList.SelectedValue != "0")
                storeAccessParameters = " AND storeId='" + ddlStoreList.SelectedValue + "'";

            var summaryDto = new SearchDto();
            summaryDto.From = Convert.ToDateTime(txtFrom.Text);
            summaryDto.To = Convert.ToDateTime(txtTo.Text);
            summaryDto.storeAccessParameter = storeAccessParameters;

            var salesProfitService = new SalesProfitService();

            var salesProfit = salesProfitService.SalesTotalProfitAmount(summaryDto);
            lblSalesProfit.Text = salesProfit.ToString("0.00");

            var supplierCommission = salesProfitService.GetSupplierCommission(summaryDto);
            lblSupplierCommission.Text = supplierCommission.ToString("0.00");

            var discountAmount = salesProfitService.GetDiscountAmount(summaryDto);
            lblDiscountAmt.Text = discountAmount.ToString("0.00");

            var returnAmount = salesProfitService.GetReturnAmount(summaryDto);
            lblReturnAmt.Text = returnAmount.ToString("0.00");


            var expenseDto = new ExpenseDto();
            expenseDto.From = Convert.ToDateTime(txtFrom.Text);
            expenseDto.To = Convert.ToDateTime(txtTo.Text);
            expenseDto.storeAccessParameter = storeAccessParameters;

            var expenseService = new ExpenseService();
            var totalExpenseAmount = expenseService.GetExpenseAmount(expenseDto);

            var salaryDto = new SalaryDto();
            salaryDto.From = Convert.ToDateTime(txtFrom.Text);
            salaryDto.To = Convert.ToDateTime(txtTo.Text);
            salaryDto.storeAccessParameter = storeAccessParameters;

            var salaryService = new SalaryService();
            var totalSalaryAmount = salaryService.GetSalaryData(salaryDto);


            var supplierDto = new SupplierDto();
            supplierDto.From = Convert.ToDateTime(txtFrom.Text);
            supplierDto.To = Convert.ToDateTime(txtTo.Text);
            supplierDto.storeAccessParameter = storeAccessParameters;

            var supplierService = new SupplierService();
            var supplierRecivedAmount = supplierService.GetSupplierRecivedAmountData(supplierDto);
            
            salesProfit += supplierRecivedAmount;

            // deduct misc cost
            var miscellaneousCost = lblTotalMiscCost.Text == "" ? "0" : lblTotalMiscCost.Text;
            salesProfit -= Convert.ToDecimal(miscellaneousCost);
            
            //var supplierCommissionAmt = getSupplierCommissionAmount();
            //saleProfit += supplierCommissionAmt;

            lblGrossProfit.Text = salesProfit.ToString("0.00");
            lblGrossProfitCollapseFooter.Text = salesProfit.ToString("0.00");
            lblTotalExpense.Text = totalExpenseAmount.ToString("0.00");
            lblTotalSalary.Text = totalSalaryAmount.ToString("0.00");
            lblNetProfit.Text = (salesProfit - (totalExpenseAmount + totalSalaryAmount)).ToString("0.00");
        }



        private decimal getSupplierCommissionAmount()
        {
            var supplierCommission = new SupplierCommission();
            return supplierCommission.getSupplierCommission(ddlStoreList.SelectedValue, dateTimeTo, dateTimeFrom);
        }






        /* old */
        public void scriptMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Area", "alert('" + msg + "');", true);
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {

            Search();
        }






    }


}
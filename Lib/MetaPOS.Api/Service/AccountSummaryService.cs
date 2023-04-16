using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;
using MetaPOS.Api.Models;

namespace MetaPOS.Api.Service
{
    public class AccountSummaryService : AccountSummary
    {
        private CommonFunction commonFunction = new CommonFunction();
        private SummaryModel summaryModel = new SummaryModel();
        private static string storeAccessParameters = "";
        public AccountSummaryService()
        {

        }

        public List<DataStatus> getAccountSummary()
        {
            var dataStatus = new List<DataStatus>();

            if (!commonFunction.CheckConnectionString(shopname))
            {
                dataStatus.Add(new DataStatus() {status = "404"});
                return dataStatus;
            }

            try
            {
                var summaryModel = new SummaryModel();
                summaryModel.storeid = storeid;
                summaryModel.startdate = startdate;
                summaryModel.enddate = enddate;
                summaryModel.shopname = shopname;

                // Sales Profit
                decimal totalSalesProfit = SalesProfit();

                // Commission Amount
                decimal totalCommissionAmount = CommisionAmount();

                // Discount Amount
                decimal totalDiscountAmount = DiscountAmount();

                // Return Amount
                decimal totalReturnAmount = ReturnAmount();


                // Total Profit
                var totalProfit = (totalSalesProfit + totalCommissionAmount) - (totalDiscountAmount + totalDiscountAmount + totalReturnAmount);

                // Total Expenses
                var totalExpense = TotalExpense();

                // Net Income
                var totalNetIncome = totalProfit - totalExpense;

                var accountSummary = new List<object>();
                accountSummary.Add(new Summary() { title = "মোট বিক্রয়", amount = totalSalesProfit.ToString("0.00"), imageurl = "/img/appicon/icon1.svg" });
                accountSummary.Add(new Summary() { title =  "মোট খরচ", amount = totalExpense.ToString("0.00"), imageurl = "/img/appicon/icon1.svg" });
                accountSummary.Add(new Summary() { title = "সর্বমোট আয়", amount = totalNetIncome.ToString("0.00"), imageurl = "/img/appicon/icon1.svg" });

                dataStatus.Add(new DataStatus() { status = "200", data = accountSummary });
            }
            catch (Exception)
            {
                dataStatus.Add(new DataStatus() { status = "404" });
            }
            return dataStatus;
        }


        private decimal TotalExpense()
        {
            var totalExpense = 0M;
            var dtExpense = summaryModel.getExpensiveModel();
            summaryModel.shopname = shopname;

            for (int i = 0; i < dtExpense.Rows.Count; i++)
            {
                totalExpense += Convert.ToDecimal(dtExpense.Rows[i]["cashOut"].ToString() == "" ? "0" : dtExpense.Rows[i]["cashOut"].ToString());
            }

            return totalExpense;
        }

        private decimal ReturnAmount()
        {
            storeAccessParameters = " AND storeId='" + storeid + "'"; //commonFunction.getStoreListbyRoleId(storeid);
            summaryModel.startDate = startdate;
            summaryModel.endDate = enddate;
            summaryModel.storeAccessParameters = storeAccessParameters;
            summaryModel.shopname = shopname;

            decimal totalReturnAmt = 0M;
            var dtReturnAmt = summaryModel.getSalesReturnModel();
            for (int i = 0; i < dtReturnAmt.Rows.Count; i++)
            {
                totalReturnAmt += Convert.ToDecimal(dtReturnAmt.Rows[i]["balance"].ToString());
            }
            return totalReturnAmt;
        }




        private decimal DiscountAmount()
        {
            storeAccessParameters = " AND storeId='" + storeid + "'"; //commonFunction.getStoreListbyRoleId(storeid);
            summaryModel.startDate = startdate;
            summaryModel.endDate = enddate;
            summaryModel.storeAccessParameters = storeAccessParameters;
            summaryModel.shopname = shopname;

            decimal totalDiscountAmt = 0M;
            var dtSalesDiscount = summaryModel.getSalesDiscountModel();
            for (int i = 0; i < dtSalesDiscount.Rows.Count; i++)
            {
                totalDiscountAmt += Convert.ToDecimal(dtSalesDiscount.Rows[i]["discAmt"].ToString());
            }
            return totalDiscountAmt;
        }




        private decimal CommisionAmount()
        {
            var totalCommission = 0M;
            storeAccessParameters = " AND storeId='" + storeid + "'"; //commonFunction.getStoreListbyRoleId(storeid);
            summaryModel.startDate = startdate;
            summaryModel.endDate = enddate;
            summaryModel.storeAccessParameters = storeAccessParameters;
            summaryModel.shopname = shopname;

            var dtCommissionAmount = summaryModel.getSalesProfitModel();

            if (dtCommissionAmount.Rows.Count > 0)
            {

                for (int i = 0; i < dtCommissionAmount.Rows.Count; i++)
                {
                    var bPrice = Convert.ToDecimal(dtCommissionAmount.Rows[i]["bPrice"].ToString());
                    var commission = Convert.ToDecimal(dtCommissionAmount.Rows[i]["commission"].ToString());
                    totalCommission += (commission * bPrice) / 100;
                }
            }

            return totalCommission;
        }




        private decimal SalesProfit()
        {
            storeAccessParameters = " AND storeId='" + storeid + "'";
            summaryModel.startDate = startdate;
            summaryModel.endDate = enddate;
            summaryModel.storeAccessParameters = storeAccessParameters;
            summaryModel.shopname = shopname;

            var dtSaleProfit = summaryModel.getSalesProfitModel();

            var totalSalesProfit = 0M;
            for (int i = 0; i < dtSaleProfit.Rows.Count; i++)
            {
                totalSalesProfit += Convert.ToDecimal(dtSaleProfit.Rows[i]["balance"].ToString());
            }
            return totalSalesProfit;
        }
    }
}

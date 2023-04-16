using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.InventoryBundle.Service;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.AnalyticBundle.Service
{
    public class AccountService
    {

        public decimal getGrossProfit(string storeAccessParameters, string from, string to)
        {
            if (from == "")
                from = "01/01/2010";
            if (to == "")
                to = DateTime.MaxValue.ToShortDateString();

            var stockStatusModel = new StockStatusModel();
            decimal totalProfitAmt = 0M, totalReturnAmt = 0M;
            var totalDiscountAmt = 0M;

            totalDiscountAmt = getSummarySalesDiscountAmount(storeAccessParameters, from, to);

            totalProfitAmt = getSummaryTotalProfit(storeAccessParameters, from, to);

            var totalSupplierCommission = getSummarySupplierCommission(storeAccessParameters, from, to);

            // Sales Return total
            totalReturnAmt = getSummaryReturnAmount(storeAccessParameters, from, to);


            var grossProfit = (totalProfitAmt + totalSupplierCommission) - (totalReturnAmt + totalDiscountAmt);

            return grossProfit;
        }

        public decimal getSummaryTotalProfit(string storeAccessParameters, string from, string to)
        {
            decimal totalBal = 0;

            var stockStatusModel = new StockStatusModel();
            var dtStockStatusSalesProfit = stockStatusModel.getSalesProfitModel(storeAccessParameters, from, to);
            if (dtStockStatusSalesProfit.Rows.Count > 0)
            {
                for (int i = 0; i < dtStockStatusSalesProfit.Rows.Count; i++)
                {
                    totalBal += Convert.ToDecimal(dtStockStatusSalesProfit.Rows[i]["balance"].ToString());
                }
            }

            return totalBal;
        }

        public decimal getSummarySupplierCommission(string storeAccessParameters, string from, string to)
        {
            decimal totalCommission = 0, bPrice = 0, commission = 0;

            var stockStatusModel = new StockStatusModel();
            var dtStockStatusSalesProfit = stockStatusModel.getSalesProfitModel(storeAccessParameters, from, to);
            if (dtStockStatusSalesProfit.Rows.Count > 0)
            {

                for (int i = 0; i < dtStockStatusSalesProfit.Rows.Count; i++)
                {
                    bPrice = Convert.ToDecimal(dtStockStatusSalesProfit.Rows[i]["bPrice"].ToString());
                    commission = Convert.ToDecimal(dtStockStatusSalesProfit.Rows[i]["commission"].ToString());
                    totalCommission += getCommissionForSummary(bPrice, commission);
                }
            }

            return totalCommission;
        }




        public decimal getSummarySalesDiscountAmount(string storeAccessParameters, string from, string to)
        {
            decimal totalDiscountAmt = 0;
            var stockStatusModel = new StockStatusModel();
            var dtStockStatusSalesDiscount = stockStatusModel.getSalesDiscountModel(storeAccessParameters, from, to);
            if (dtStockStatusSalesDiscount.Rows.Count > 0)
            {
                for (int i = 0; i < dtStockStatusSalesDiscount.Rows.Count; i++)
                {
                    totalDiscountAmt += Convert.ToDecimal(dtStockStatusSalesDiscount.Rows[i]["discAmt"].ToString());
                }
            }

            return totalDiscountAmt;
        }





        public decimal getSummaryReturnAmount(string storeAccessParameters, string from, string to)
        {
            var totalReturnAmt = 0M;
            var stockStatusModel = new StockStatusModel();
            var dtStockStatusSalesReturn = stockStatusModel.getSalesReturnModel(storeAccessParameters, from, to);
            if (dtStockStatusSalesReturn.Rows.Count > 0)
            {
                for (int i = 0; i < dtStockStatusSalesReturn.Rows.Count; i++)
                {
                    totalReturnAmt += Convert.ToDecimal(dtStockStatusSalesReturn.Rows[i]["balance"].ToString());
                }
            }

            return totalReturnAmt;
        }


        private decimal getCommissionForSummary(decimal bPrice, decimal commission)
        {
            return (commission * bPrice) / 100;
        }

        public decimal getTotalExpensive(string storeAccessParameters, string form, string to)
        {
            var stockStatusModel = new StockStatusModel();
            var dtStockStatusExpense = stockStatusModel.getTotalExpensiveExpensceModel(storeAccessParameters, form, to);

            var totalExpense = 0M;
            if (dtStockStatusExpense.Rows[0][0].ToString() != "")
                totalExpense = Convert.ToDecimal(dtStockStatusExpense.Rows[0][0].ToString());
            return totalExpense;
        }

        public decimal getSupplierRecivedAmt(string storeAccessParameters, string form, string to)
        {
            var stockStatusModel = new StockStatusModel();
            var dtStockStatusSupplierAmt = stockStatusModel.getTotalSupplierRecivedAmtModel(storeAccessParameters, form, to);
            var totalSupplierRecivedAmt = 0M;
            if (dtStockStatusSupplierAmt.Rows.Count > 0 && dtStockStatusSupplierAmt.Rows[0][0].ToString() != "")
            {
                totalSupplierRecivedAmt = Convert.ToDecimal(dtStockStatusSupplierAmt.Rows[0][0].ToString());
            }
            return totalSupplierRecivedAmt;
        }

        public decimal getTotalSalary(string storeAccessParameters, string form, string to)
        {
            var stockStatusModel = new StockStatusModel();
            var dtTotalSalary = stockStatusModel.getTotalSalaryModel(storeAccessParameters, form, to);
            var totalSalaryAmt = 0M;
            if (dtTotalSalary.Rows.Count > 0 && dtTotalSalary.Rows[0][0].ToString() != "")
            {
                totalSalaryAmt = Convert.ToDecimal(dtTotalSalary.Rows[0][0].ToString());
            }
            return totalSalaryAmt;
        }
    }
}
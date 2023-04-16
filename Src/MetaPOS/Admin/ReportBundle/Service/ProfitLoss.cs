using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.ReportBundle.Service
{
    public class ProfitLoss
    {


        //private decimal totalCashin = 0, totalCashout = 0;
        public DataTable getProfitLossCalculation(string query)
        {
            var profitLossModel = new ProfitLossModel();
            return profitLossModel.getProfitLossCalculationModel(query);

        }

        public string getSupplierDueAmt(DateTime searchFrom, DateTime searchTo)
        {
            var profitLossModel = new ProfitLossModel();
            profitLossModel.searchFrom = searchFrom;
            profitLossModel.searchTo = searchTo;

            decimal totalPaidAmt = 0, totalPayableAmt = 0;

            var dtSupplierPayableAmt = profitLossModel.getSupllierPayableAmountModel();
            if (dtSupplierPayableAmt.Rows[0][0].ToString() != "")
            {
                totalPayableAmt = Convert.ToDecimal(dtSupplierPayableAmt.Rows[0][0].ToString());
            }

            var dtSupplierPaidAmt = profitLossModel.getSupllierPaidAmountModel();
            if (dtSupplierPaidAmt.Rows[0][0].ToString() != "")
            {
                totalPaidAmt = Convert.ToDecimal(dtSupplierPaidAmt.Rows[0][0].ToString());
            }

            return (totalPayableAmt - totalPaidAmt).ToString("0.00");


        }

        public string getRevenueAmount()
        {
            var profitLoss = new ProfitLossModel();
            var dtRevenue = profitLoss.getRevenueAmountModel();

            if (dtRevenue.Rows[0][0].ToString() != "")
                return dtRevenue.Rows[0][0].ToString();
            else
                return "0";

        }

        

        public string getExpenseAmount()
        {
            var profitLoss = new ProfitLossModel();
            var dtRevenue = profitLoss.getExpenseAmountModel();

            if (dtRevenue.Rows[0][0].ToString() != "")
                return dtRevenue.Rows[0][0].ToString();
            else
                return "0";
        }
    }
}
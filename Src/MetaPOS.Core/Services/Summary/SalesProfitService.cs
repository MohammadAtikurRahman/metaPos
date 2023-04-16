using MetaPOS.Core.Repositories;
using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services.Summary
{
    public class SalesProfitService
    {
        public decimal ProfitAmount { get; set; }
        public decimal SupplierCommissionAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public SalesProfitService()
        {
            ProfitAmount = 0;
            SupplierCommissionAmount = 0;
            ReturnAmount = 0;
            DiscountAmount = 0;
        }


        public decimal SalesTotalProfitAmount(SearchDto summary)
        {
            // Profit Amount
            ProfitAmount = GetProfitAmount(summary);

            // Supplier Commision
            SupplierCommissionAmount = GetSupplierCommission(summary);

            // Return Amount
            ReturnAmount = GetReturnAmount(summary);

            // Discount Amount
            DiscountAmount = GetDiscountAmount(summary);

            var totalProfitAmount = GetSalesProfitAmount();

            return totalProfitAmount;
        }

        

        public decimal GetProfitAmount(SearchDto summary)
        {
            var salesProfit = new SalesProfitRepository();
            var dtProfit = salesProfit.ProfitAmount(summary);
            var totaProfit = 0M;
            for (int i = 0; i < dtProfit.Rows.Count; i++)
            {
                totaProfit += Convert.ToDecimal(dtProfit.Rows[i]["balance"].ToString());
            }
            return totaProfit;
        }


        public decimal GetSupplierCommission(SearchDto summary)
        {
            var salesProfit = new SalesProfitRepository();
            var dtSupplierCommission = salesProfit.SupplierCommission(summary);
            decimal totalCommission = 0M, bPrice = 0M, commission = 0M;
            for (int i = 0; i < dtSupplierCommission.Rows.Count; i++)
            {
                bPrice = Convert.ToDecimal(dtSupplierCommission.Rows[i]["bPrice"].ToString());
                commission = Convert.ToDecimal(dtSupplierCommission.Rows[i]["commission"].ToString());
                totalCommission = CalculateCommission(commission, bPrice);
            }
            return totalCommission;
        }


        public decimal GetReturnAmount(SearchDto summary)
        {
            var salesProfit = new SalesProfitRepository();
            var dtReturn = salesProfit.ReturnAmount(summary);
            var totalReturnAmount = 0M;
            for (int i = 0; i < dtReturn.Rows.Count; i++)
            {
                totalReturnAmount += Convert.ToDecimal(dtReturn.Rows[i]["balance"].ToString());
            }
            return totalReturnAmount;
        }



        public decimal GetDiscountAmount(SearchDto summary)
        {
            var salesProfit = new SalesProfitRepository();
            var dtDiscount = salesProfit.DiscountAmount(summary);
            var totalDiscountAmount = 0M;
            for (int i = 0; i < dtDiscount.Rows.Count; i++)
            {
                totalDiscountAmount += Convert.ToDecimal(dtDiscount.Rows[i]["discAmt"].ToString());
            }
            return totalDiscountAmount;
        }


        public decimal CalculateCommission(decimal commission, decimal bPrice)
        {
            return (commission * bPrice) / 100;
        }


        public decimal GetSalesProfitAmount()
        {
            return (ProfitAmount + SupplierCommissionAmount) - (ReturnAmount + DiscountAmount);
        }
    }
}

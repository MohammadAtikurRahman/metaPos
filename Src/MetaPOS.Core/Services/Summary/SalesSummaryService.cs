using MetaPOS.Core.Repositories;
using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services.Summary
{
    public class SalesSummaryService : SearchDto
    {
        DataTable dtSale, dtSaleRecord, dtSaleReturn, dtSaleRecived;
        private SummaryRepository summaryRepository = new SummaryRepository();

        public SalesSummaryService(SearchDto summaryDto)
        {
            dtSale = summaryRepository.SaleSummary(summaryDto);
            dtSaleRecord = summaryRepository.SalesQtyTotal(summaryDto);
            dtSaleReturn = summaryRepository.SaleReturnTotal(summaryDto);
            dtSaleRecived = summaryRepository.SaleRecivedRecord(summaryDto);
        }

        public string InvoiceTotal()
        {
            if (dtSaleRecord.Rows.Count == 0)
                return "0";
            var hasData = dtSaleRecord.Rows[0][0].ToString() == "" ? false : true;
            return hasData ? dtSaleRecord.Rows.Count.ToString() : "0";
        }

        public string SalesQtyTotal()
        {
            if (dtSaleRecord.Rows.Count <= 0)
                return "0";
            var saleQty = 0M;
            
            for (int i = 0; i < dtSaleRecord.Rows.Count; i++)
            {
                var qty = dtSaleRecord.Rows[i]["qty"].ToString() == "" ? "0" : dtSaleRecord.Rows[i]["qty"].ToString();
                saleQty += Convert.ToDecimal(qty);
            }

            return saleQty.ToString();
        }

        public string NetAmountTotal()
        {
            if (dtSale.Rows.Count == 0)
                return "0";

            var netAmt = dtSale.Rows[0]["netAmt"].ToString();
            return netAmt == "" ? "0.00" : netAmt;
        }

        public string DiscountAmountTotal()
        {
            if (dtSale.Rows.Count == 0)
                return "0";

            var discAmt = dtSale.Rows[0]["discAmt"].ToString();
            return discAmt == "" ? "0.00" : discAmt;
        }

        public string MiscellaneousCostTotal()
        {
            if (dtSale.Rows.Count == 0)
                return "0";

            var miscCostService = new MiscCostService();
            miscCostService.LoadingCost = dtSale.Rows[0]["loadingCost"].ToString();
            miscCostService.CarryingCost = dtSale.Rows[0]["carryingCost"].ToString();
            miscCostService.UnloadingCost = dtSale.Rows[0]["unloadingCost"].ToString();
            miscCostService.ShippingCost = dtSale.Rows[0]["shippingCost"].ToString();
            miscCostService.ServiceCharge = dtSale.Rows[0]["serviceCharge"].ToString();

            var totalMiscCost = miscCostService.TotalMiscCostAmount();
            return totalMiscCost.ToString("0.00");
        }

        public string GrossAmountTotal()
        {
            if (dtSale.Rows.Count == 0)
                return "0.00";

            var grossAmt = dtSale.Rows[0]["grossAmt"].ToString();
            return grossAmt == "" ? "0.00" : grossAmt;
        }

        public string RecivedAmountTotal()
        {
            if (dtSaleRecived.Rows.Count == 0)
                return "0.00";

            var hasData = dtSaleRecived.Rows[0]["cashIn"].ToString() == "" ? true : false;
            return hasData ? "0.00" : dtSaleRecived.Rows[0]["cashIn"].ToString();
        }

        public string ReturnAmountTotal()
        {
            if (dtSaleReturn.Rows.Count == 0)
                return "0.00";

            if (dtSaleReturn.Rows[0][0].ToString() == "")
                return "0.00";

            var returnAmt = 0M;
            for (int i = 0; i < dtSaleReturn.Rows.Count; i++)
            {
                returnAmt += Convert.ToDecimal(dtSaleReturn.Rows[i]["balance"].ToString());
            }
            return returnAmt.ToString() == "" ? "0" : returnAmt.ToString("0.00");
        }

        public string DueAmountTotal()
        {
            if (dtSale.Rows.Count == 0)
                return "0.00";

            var giftAmt = dtSale.Rows[0]["giftAmt"].ToString();
            return giftAmt == "" ? "0.00" : giftAmt;
        }
    }
}

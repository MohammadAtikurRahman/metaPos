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
    public class CashSummaryService : SearchDto
    {
        DataTable dtPayment;
        SummaryRepository summaryRepository = new SummaryRepository();

        public string CashPaymethod(SearchDto summaryDto)
        {
            dtPayment = summaryRepository.CashSummary(summaryDto);
            if (dtPayment.Rows.Count == 0)
                return "0.00";
            var cash = dtPayment.Rows[0][0].ToString();
            return cash == "" ? "0.00" : cash;
        }

        public string CheckPaymethod(SearchDto summaryDto)
        {
            dtPayment = summaryRepository.CashSummary(summaryDto);
            if (dtPayment.Rows.Count == 0)
                return "0.00";
            var cash = dtPayment.Rows[0][0].ToString();
            return cash == "" ? "0.00" : cash;
        }


        public string CardPaymethod(SearchDto summaryDto)
        {
            dtPayment = summaryRepository.CashSummary(summaryDto);
            if (dtPayment.Rows.Count == 0)
                return "0.00";
            var cash = dtPayment.Rows[0][0].ToString();
            return cash == "" ? "0.00" : cash;
        }

        public string MobileBankingPaymethod(SearchDto summaryDto)
        {
            dtPayment = summaryRepository.CashSummary(summaryDto);
            if (dtPayment.Rows.Count == 0)
                return "0.00";
            var cash = dtPayment.Rows[0][0].ToString();
            return cash == "" ? "0.00" : cash;
        }

        public string CODPaymethod(SearchDto summaryDto)
        {
            dtPayment = summaryRepository.CashSummary(summaryDto);
            if (dtPayment.Rows.Count == 0)
                return "0.00";
            var cash = dtPayment.Rows[0][0].ToString();
            return cash == "" ? "0.00" : cash;
        }


    }
}

using MetaPOS.Core.Interfaces;
using MetaPOS.Entities.Dto;
using MetaPOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Repositories
{
    public class SummaryRepository : ISummary
    {
        public DataTable AccountsSummary(SearchDto summary)
        {
            var summaryModel = new SummaryModel();
            summaryModel.storeAccessParameter = summary.storeAccessParameter;
            summaryModel.From = summary.From;
            summaryModel.To = summary.To;
            return summaryModel.AccountsSummary();
        }

        public DataTable CashSummary(SearchDto summary)
        {
            var summaryModel = new SummaryModel();
            summaryModel.storeAccessParameter = summary.storeAccessParameter;
            summaryModel.From = summary.From;
            summaryModel.To = summary.To;
            summaryModel.PayMethod = summary.PayMethod;
            return summaryModel.CashSummary();
        }

        public DataTable SaleRecivedRecord(SearchDto summary)
        {
            var summaryModel = new SummaryModel();
            summaryModel.storeAccessParameter = summary.storeAccessParameter;
            summaryModel.From = summary.From;
            summaryModel.To = summary.To;
            return summaryModel.CashReportSaleRecord();
        }

        public DataTable SaleSummary(SearchDto summary)
        {
            var summaryModel = new SummaryModel();
            summaryModel.storeAccessParameter = summary.storeAccessParameter;
            summaryModel.From = summary.From;
            summaryModel.To = summary.To;
            return summaryModel.SaleSummary();
        }

        public DataTable SalesQtyTotal(SearchDto summary)
        {
            var summaryModel = new SummaryModel();
            summaryModel.storeAccessParameter = summary.storeAccessParameter;
            summaryModel.From = summary.From;
            summaryModel.To = summary.To;
            return summaryModel.SaleRecord();
        }


        public DataTable SaleReturnTotal(SearchDto summary)
        {
            var summaryModel = new SummaryModel();
            summaryModel.storeAccessParameter = summary.storeAccessParameter;
            summaryModel.From = summary.From;
            summaryModel.To = summary.To;
            return summaryModel.SaleReturnRecord();
        }

        
    }
}

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
    public class SalesProfitRepository : ISalesProfit
    {
        public DataTable DiscountAmount(SearchDto summary)
        {
            var salesProfitModel = new SalesProfitModel();
            salesProfitModel.From = summary.From;
            salesProfitModel.To = summary.To;
            salesProfitModel.storeAccessParameter = summary.storeAccessParameter;
            return salesProfitModel.DiscountAmount();
        }

        public DataTable ProfitAmount(SearchDto summary)
        {
            var salesProfitModel = new SalesProfitModel();
            salesProfitModel.From = summary.From;
            salesProfitModel.To = summary.To;
            salesProfitModel.storeAccessParameter = summary.storeAccessParameter;
            return salesProfitModel.ProfitAmount();
        }

        public DataTable ReturnAmount(SearchDto summary)
        {
            var salesProfitModel = new SalesProfitModel();
            salesProfitModel.From = summary.From;
            salesProfitModel.To = summary.To;
            salesProfitModel.storeAccessParameter = summary.storeAccessParameter;
            return salesProfitModel.ReturnAmount();
        }

        public DataTable SupplierCommission(SearchDto summary)
        {
            var salesProfitModel = new SalesProfitModel();
            salesProfitModel.From = summary.From;
            salesProfitModel.To = summary.To;
            salesProfitModel.storeAccessParameter = summary.storeAccessParameter;
            return salesProfitModel.SupplierCommission();
        }
    }
}

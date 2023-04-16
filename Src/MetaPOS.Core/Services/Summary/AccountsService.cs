using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services.Summary
{
    public class AccountsService
    {
        public string TotalProfit(SearchDto summary)
        {
            var salesProfitService = new SalesProfitService();
            var totalProfit = salesProfitService.GetProfitAmount(summary);
            return totalProfit.ToString("0.00");
        }


        public string TotalExpense()
        {
            throw new NotImplementedException();
        }


        public string NetIncome()
        {
            throw new NotImplementedException();
        }
    }
}

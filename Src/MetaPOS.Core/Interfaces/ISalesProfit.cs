using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface ISalesProfit
    {
        DataTable ProfitAmount(SearchDto summary);
        DataTable SupplierCommission(SearchDto summary);
        DataTable ReturnAmount(SearchDto summary);
        DataTable DiscountAmount(SearchDto summary);

    }
}

using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface ISummary
    {
        DataTable SaleSummary(SearchDto summary);
        DataTable CashSummary(SearchDto summary);
        DataTable AccountsSummary(SearchDto summary);
    }
}

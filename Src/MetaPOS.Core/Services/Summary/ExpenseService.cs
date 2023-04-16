using MetaPOS.Core.Interfaces;
using MetaPOS.Core.Repositories;
using MetaPOS.Entities.Dto;
using MetaPOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services.Summary
{
    public class ExpenseService
    {
        public decimal GetExpenseAmount(SearchDto summary)
        {
            var expenseRepository = new ExpenseRepository();
            var dtExpense = expenseRepository.GetData(summary);
            
            var totalExpense = 0M;
            if (dtExpense.Rows.Count == 0)
                return totalExpense;

            if (dtExpense.Rows[0][0].ToString() != "")
                totalExpense = Convert.ToDecimal(dtExpense.Rows[0][0].ToString());
            return totalExpense;
        }
    }
}

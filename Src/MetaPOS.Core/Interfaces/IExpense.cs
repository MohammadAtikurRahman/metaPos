using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface IExpense
    {
        bool GetById(int Id);
        DataTable GetData();
        DataTable GetDataCondition(string condition);
        DataTable GetData(SearchDto summary);
        string Add(ExpenseDto expense);
        string Update(ExpenseDto expense);
        string Delete(ExpenseDto expense);
    }
}

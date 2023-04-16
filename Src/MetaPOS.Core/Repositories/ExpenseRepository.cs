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
    public class ExpenseRepository : IExpense
    {
        public string Add(ExpenseDto expense)
        {
            throw new NotImplementedException();
        }

        public string Delete(ExpenseDto expense)
        {
            throw new NotImplementedException();
        }

        public bool GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetData()
        {
            throw new NotImplementedException();
        }

        public DataTable GetData(SearchDto summary)
        {
            var expenseModel = new ExpenseModel();
            expenseModel.From = summary.From;
            expenseModel.To = summary.To;
            expenseModel.storeAccessParameter = summary.storeAccessParameter;
            return expenseModel.GetData();
        }

        public DataTable GetDataCondition(string condition)
        {
            throw new NotImplementedException();
        }

        public string Update(ExpenseDto expense)
        {
            throw new NotImplementedException();
        }
    }
}

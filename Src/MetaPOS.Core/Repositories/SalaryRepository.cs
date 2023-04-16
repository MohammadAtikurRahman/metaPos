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
    public class SalaryRepository : ISalary
    {
        public string Add(SalaryDto expense)
        {
            throw new NotImplementedException();
        }

        public string Delete(SalaryDto expense)
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

        public DataTable GetData(SalaryDto salary)
        {
            var salaryModel = new SalaryModel();
            salaryModel.From = salary.From;
            salaryModel.To = salary.To;
            salaryModel.storeAccessParameter = salary.storeAccessParameter;
            return salaryModel.GetSalaryData();
        }
        

        public DataTable GetDataCondition(string condition)
        {
            throw new NotImplementedException();
        }

        public string Update(SalaryDto expense)
        {
            throw new NotImplementedException();
        }
    }
}

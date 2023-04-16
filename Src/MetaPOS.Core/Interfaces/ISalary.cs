using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface ISalary
    {
        bool GetById(int Id);
        DataTable GetData();
        DataTable GetDataCondition(string condition);
        DataTable GetData(SalaryDto summary);
        string Add(SalaryDto expense);
        string Update(SalaryDto expense);
        string Delete(SalaryDto expense);
    }
}

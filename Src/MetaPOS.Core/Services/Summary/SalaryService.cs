using MetaPOS.Core.Repositories;
using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services.Summary
{
    public class SalaryService
    {
        public decimal GetSalaryData(SalaryDto salary)
        {
            var salaryRepository = new SalaryRepository();
            var dtSalary = salaryRepository.GetData(salary);
            var totalSalaryAmt = 0M;
            if (dtSalary.Rows.Count > 0 && dtSalary.Rows[0][0].ToString() != "")
            {
                totalSalaryAmt = Convert.ToDecimal(dtSalary.Rows[0][0].ToString());
            }
            return totalSalaryAmt;
        }
    }
}

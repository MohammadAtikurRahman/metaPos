using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Entities.Dto
{
    public class SalaryDto : SearchDto
    {
        public int Id { get; set; }
        public string StaffId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }

    }
}

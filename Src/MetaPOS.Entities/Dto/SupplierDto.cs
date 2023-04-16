using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Entities.Dto
{
    public class SupplierDto : SearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SupplierCode { get; set; }
        public string ContactName { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Discount { get; set; }
        public string Address { get; set; }
        public SqlConnection connectionStr { get; set; }
    }
}

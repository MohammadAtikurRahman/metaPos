using MetaPOS.Entities.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Entities.ProductAggregate
{
    public class Category: BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EnteryDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }
    }
}

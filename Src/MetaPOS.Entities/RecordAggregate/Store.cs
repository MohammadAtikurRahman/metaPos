using MetaPOS.Entities.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Entities.RecordAggregate
{
    public class Store: BaseEntity
    {
        public string Name { get; set; }
        public string RoleId { get; set; }
    }
}

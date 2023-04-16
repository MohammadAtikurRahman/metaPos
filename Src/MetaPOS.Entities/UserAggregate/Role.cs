using MetaPOS.Entities.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Entities.UserAggregate
{
    public class Role : BaseEntity
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StoreId { get; set; }
    }
}

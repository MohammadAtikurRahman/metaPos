using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Api.Entity
{
    public class Account
    {
        public string shopname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string roleId { get; set; }
        public string storeId { get; set; }
        public string title { get; set; }
        public string branchId { get; set; }
        public string activedate { get; set; }
        public string expirydate { get; set; }
        public string monthlyfee { get; set; }
        public string baseurl { get; set; }
    }
}

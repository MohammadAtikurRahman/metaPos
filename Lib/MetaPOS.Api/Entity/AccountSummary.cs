using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Api.Entity
{
    public class AccountSummary
    {
        public string storeid { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string shopname { get; set; }
    }
}

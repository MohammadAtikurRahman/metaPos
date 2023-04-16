using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPay.PortWallet.Entities
{
    public class Order
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Redirect_url { get; set; }
        public string Ipn_url { get; set; }
        public string Reference { get; set; }
    }
}

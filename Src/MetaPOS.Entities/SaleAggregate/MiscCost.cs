using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Entities.SaleAggregate
{
    public class MiscCost
    {
        public string LoadingCost { get; set; }
        public string CarryingCost { get; set; }
        public string UnloadingCost { get; set; }
        public string ShippingCost { get; set; }
        public string ServiceCharge { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Api.Entity
{
    public class SoldProduct
    {
        public string productId { get; set; }
        public string prodName { get; set; }
        public string salePrice { get; set; }
        public string qty { get; set; }

    }
}

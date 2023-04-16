using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services
{
    public class CommonService
    {
        public decimal ValidDecimal(string amount)
        {
            try
            {
                return Convert.ToDecimal(amount);
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}

using MetaPOS.Entities.SaleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services.Summary
{
    public class MiscCostService : MiscCost
    {
        public decimal TotalMiscCostAmount()
        {
            var commonService = new CommonService();

            return commonService.ValidDecimal(LoadingCost) + commonService.ValidDecimal(CarryingCost) + commonService.ValidDecimal(UnloadingCost) + commonService.ValidDecimal(ShippingCost) + commonService.ValidDecimal(ServiceCharge);
        }

        
    }
}

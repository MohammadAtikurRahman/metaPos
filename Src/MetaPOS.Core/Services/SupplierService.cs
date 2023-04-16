using MetaPOS.Core.Repositories;
using MetaPOS.Entities.Dto;
using MetaPOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services
{
    public class SupplierService
    {
        public decimal GetSupplierRecivedAmountData(SupplierDto supplier)
        {
            var supplierReposity = new SupplierRepository();
            var dtSupplierRecivedAmt = supplierReposity.GetData(supplier);
            if (dtSupplierRecivedAmt.Rows.Count == 0)
                return 0M;

            var amount = dtSupplierRecivedAmt.Rows[0][0].ToString();
            if (!HasSupplierRecivedAmount(amount))
                return 0M;

            var totalSupplierRecivedAmt = ConvertToDecimalSupplierRecivedAmount(amount);

            return totalSupplierRecivedAmt;
        }


        public bool HasSupplierRecivedAmount(string amount)
        {
            if (amount != "")
                return true;
            return false;
        }


        public decimal ConvertToDecimalSupplierRecivedAmount(string amount)
        {
            try
            {
                return Convert.ToDecimal(amount);
            }
            catch (Exception)
            {
                return -1M;
            }
        }
    }
}

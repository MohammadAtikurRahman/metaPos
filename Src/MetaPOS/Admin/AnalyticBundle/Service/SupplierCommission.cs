using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.AnalyticBundle.Service
{
    public class SupplierCommission
    {

        public decimal getSupplierCommission(string storeId, DateTime to, DateTime from)
        {

            var supplierCommission = new SupplierCommisionModel();
            supplierCommission.storeId = storeId;
            supplierCommission.To = to;
            supplierCommission.From = from;
            var dtSupplierCommission = supplierCommission.getSupplierCommissionForSummary();

            decimal supCommissionAmt = 0;
            if (dtSupplierCommission.Rows[0][0].ToString() != "")
            {
                supCommissionAmt = Convert.ToDecimal(dtSupplierCommission.Rows[0][0].ToString());
            }

            return supCommissionAmt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.InventoryBundle.Service
{
    public class SupplierCommision : Entities.Stock
    {
        public string getSupplierCommision(string supId)
        {
            var supplierCommisionModel = new SupplierCommisionModel();
            var dtSupplierCommision = supplierCommisionModel.getSupplierCommisionModel(supId);
            if (dtSupplierCommision.Rows.Count > 0)
                return dtSupplierCommision.Rows[0]["discount"].ToString();
            return "0";
        }





        public void UpdateStockBuyPriceForSupplierCommission()
        {
            var stockModel = new StockModel();
            stockModel.prodId = prodId;
            stockModel.bPrice = bPrice;
            stockModel.UpdateStockBuyPriceForSupplierCommissionModel();
        }
    }
}
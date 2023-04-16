using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.InventoryBundle.Service
{
    public class StockStatus
    {
        private  CommonFunction commonFunction = new CommonFunction();

        public bool upsertStockstatusTransfer(string TransId, string TransProdId, string transQty, string status)
        {
            var stockstatusModel = new StockStatusModel();
            var lastQty = commonFunction.getLastStockQty(TransProdId, TransId);
            var sign = "+";
            if (status == "stockTransfer")
                sign = "-";
            var balanceQty = commonFunction.calculateQty(TransProdId, lastQty, transQty, sign);
            stockstatusModel.lastQty = lastQty;
            stockstatusModel.balanceQty = balanceQty;

            return stockstatusModel.upsertStockstatusTransferModel(TransId, TransProdId, transQty, status);
        }

        public bool changeStatusData(string deliveryId)
        {
            var inventoryModel = new InventoryModel();
            return inventoryModel.changeStatusModel(deliveryId);
        }

        public bool changeDeliveryStatusData(string deliveryId)
        {
            var inventoryModel = new InventoryModel();
            return inventoryModel.changeDeliveryStatusModel(deliveryId);
        }
    }
}
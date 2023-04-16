using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.InventoryBundle.Service
{
    public class StockBarcode
    {

        public DataTable getProductData(string prodCode)
        {
            var stockModel = new StockModel();
           return stockModel.getProductDataByProductCode(prodCode);

        }
    }
}
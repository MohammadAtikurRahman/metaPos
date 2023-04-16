using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleImei
    {

        public string getProductImeiList(string prodCode)
        {
            var stockModel = new StockModel();

            stockModel.prodCode = prodCode;
             return stockModel.getProductImeiModel();
        }
    }
}
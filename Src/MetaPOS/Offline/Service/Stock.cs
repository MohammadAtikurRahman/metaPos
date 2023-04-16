using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Offline.Service
{
    public class Stock
    {
        public string initializeProduct()
        {
            var stockModel = new StockModel();
            var dtSock = stockModel.getStockListSeriliaze();
            return dtSock;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SalePrint
    {
        CommonFunction commonFunction = new CommonFunction();

        public string getInvoicePrintData(string billNo)
        {
            var saleModel = new SaleModel();
            string storeId = HttpContext.Current.Session["storeId"].ToString();
            
            var dtInvoiceData = saleModel.getInvoicePrintDataModel(billNo, storeId); ;
            return commonFunction.serializeDatatableToJson(dtInvoiceData);
        }
    }


}
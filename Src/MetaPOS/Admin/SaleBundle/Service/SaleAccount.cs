using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SaleAccount
    {


        public void calDiscount()
        {
        }





        public string getSupplierCommission(string prodCode)
        {
            var supCommModel = new StockModel();
            return supCommModel.getSupCommissionModel(prodCode);
        }


    }


}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.AnalyticBundle.Service
{
    public class SaleService
    {


        public DataTable getSaleSummery(string storeAccessParameters, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var saleModel = new SaleModel();
            saleModel.storeAccessParameters = storeAccessParameters;
            saleModel.From = dateTimeFrom;
            saleModel.To = dateTimeTo;
            return saleModel.getsaleSummeryStoreWiseModel();
        }



        public DataTable getSaleRecordInfo(string storeAccessParameters, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var saleModel = new SaleModel();
            saleModel.storeAccessParameters = storeAccessParameters;
            saleModel.From = dateTimeFrom;
            saleModel.To = dateTimeTo;
            return saleModel.getSaleRecordInfoModel();
        }
    }
}
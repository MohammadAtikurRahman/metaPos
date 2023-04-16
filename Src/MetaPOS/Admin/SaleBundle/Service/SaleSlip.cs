using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleSlip
    {
        public string UpdateSlipInfo(string billNo)
        {
            var transactionQuery = "";

            var slipModel = new SlipModel();
            transactionQuery += "BEGIN ";
            transactionQuery += slipModel.updateSlipInfoDataClearModel(billNo);
            transactionQuery += "END ";

            return transactionQuery;
        }
    }
}
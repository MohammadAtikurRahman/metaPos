using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SalePoint
    {
        private CommonFunction commonFunction = new CommonFunction();

        public string saveCustomerPoint(dynamic data)
        {
            var transactionQuery = "";

            var customerPointModel = new CustomerPointModel();
            customerPointModel.cusId = Convert.ToInt32(data["cusId"]);
            customerPointModel.point = Convert.ToInt32(data["offer"]);
            customerPointModel.source = data["source"].ToString();
            customerPointModel.entryDate = commonFunction.GetCurrentTime();
            customerPointModel.updateDate = commonFunction.GetCurrentTime();
            customerPointModel.active = '1';

            transactionQuery += "BEGIN ";
            customerPointModel.saveCustomerPointModel();
            transactionQuery += "END ";
            return transactionQuery;
        }

        public string suspendCustomerPoint(int cusId, decimal offer)
        {
            var transactionQuery = "";
            var customerPointModel = new CustomerPointModel();
            customerPointModel.cusId = cusId;
            customerPointModel.point = -offer;
            customerPointModel.source = "Invoice Suspend";
            customerPointModel.entryDate = commonFunction.GetCurrentTime();
            customerPointModel.updateDate = commonFunction.GetCurrentTime();
            customerPointModel.active = '1';

            transactionQuery += "BEGIN ";
            transactionQuery += customerPointModel.saveCustomerPointModel();
            transactionQuery += "END ";

            return transactionQuery;

        }
    }
}
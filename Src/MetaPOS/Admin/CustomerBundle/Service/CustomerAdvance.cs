using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.CustomerBundle.Service
{
    public class CustomerAdvance
    {
        private CommonFunction commonFunction = new CommonFunction();
        public string saveCustomerAdvanceAmount(string jsonData)
        {
            var transactionQuery = "";
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);

            var cashReportModel = new CashReportModel();
            cashReportModel.descr = data["id"].Value<string>();
            cashReportModel.mainDescr = data["id"].Value<string>();
            cashReportModel.entryDate = data["date"].Value<string>() == ""
                ? commonFunction.GetCurrentTime()
                : Convert.ToDateTime(data["date"].Value<string>());

            /* Customer */
            cashReportModel.cashIn = data["amount"].Value<decimal>();
            cashReportModel.cashType = "Customer Advance";
            cashReportModel.status = '6';
            cashReportModel.payMethod = "0";
            transactionQuery += "BEGIN ";
            transactionQuery += cashReportModel.saveCustomerCashData();
            transactionQuery += "END ";

            /* Transection*/
            cashReportModel.cashIn = data["amount"].Value<decimal>();
            cashReportModel.cashType = "Advance Payment";
            cashReportModel.status = '5';
            transactionQuery += "BEGIN ";
            transactionQuery += cashReportModel.saveCustomerCashData();
            transactionQuery += "END ";
            return transactionQuery;
        }
    }
}
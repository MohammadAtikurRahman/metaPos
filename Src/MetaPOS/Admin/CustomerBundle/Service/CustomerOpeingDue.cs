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
    public class CustomerOpeingDue
    {
        private CommonFunction commonFunction = new CommonFunction();
        public string saveCustomerOpeingDueAmount(string jsonData)
        {
            var transactionQuery = "";
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);

            var cashReportModel = new CashReportModel();
            cashReportModel.descr = data["id"].Value<string>();
            cashReportModel.mainDescr = data["id"].Value<string>();
            cashReportModel.cashOut = data["amount"].Value<decimal>();
            cashReportModel.entryDate = data["date"].Value<string>() == ""
                ? commonFunction.GetCurrentTime()
                : Convert.ToDateTime(data["date"].Value<string>());
            cashReportModel.status = '6';
            cashReportModel.payMethod = "0";
            cashReportModel.cashType = "Opening due";
            transactionQuery += "BEGIN ";
            transactionQuery += cashReportModel.saveCustomerCashData();
            transactionQuery += "END ";
            return transactionQuery;
        }




        public string saveCustomerPaymentAmount(string jsonData)
        {
            var transactionQuery = "";
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);

            var cashReportModel = new CashReportModel();
            cashReportModel.descr = data["id"].Value<string>();
            cashReportModel.mainDescr = data["id"].Value<string>();
            cashReportModel.entryDate = data["date"].Value<string>() == ""
               ? commonFunction.GetCurrentTime()
               : Convert.ToDateTime(data["date"].Value<string>());
            /* Customer payment */
            cashReportModel.cashIn = data["payment"].Value<decimal>();
            cashReportModel.cashType = "Customer Payment";
            cashReportModel.status = '6';
            cashReportModel.payMethod = "0";
            transactionQuery += "BEGIN ";
            transactionQuery += cashReportModel.saveCustomerCashData();
            transactionQuery += "END ";

            // Transection payment
            cashReportModel.cashIn = data["payment"].Value<decimal>();
            cashReportModel.cashType = "Payment";
            cashReportModel.status = '5';
            cashReportModel.payMethod = "0";

            transactionQuery += "BEGIN ";
            transactionQuery += cashReportModel.saveCustomerCashData();
            transactionQuery += "END ";
            return transactionQuery;
        }
    }
}
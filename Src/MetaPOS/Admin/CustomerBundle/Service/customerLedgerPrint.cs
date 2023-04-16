using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MetaPOS.Admin.ApiBundle.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.CustomerBundle.Service
{
    public class customerLedgerPrint
    {
        public string fullCustomerLedgerPrint(string jsonData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            var cusId = data["id"].Value<string>();
            string query = "select cash.cashType,cash.descr,cash.cashIn,cash.cashOut,cash.entryDate,cash.billNo,cash.status,cus.name,cus.phone,cus.address,cus.mailInfo,cus.AccountNo,cus.installmentStatus from cashreportinfo as cash LEFT JOIN CustomerInfo as cus ON cash.descr= cus.cusID  where cash.descr =" + cusId + "'";

            HttpContext.Current.Session["pageName"] = "CustomerLedgerReport";
            HttpContext.Current.Session["reportName"] = "Customer Ledger";
            HttpContext.Current.Session["reportQury"] = query;


            HttpContext.Current.Response.Redirect("~/Admin/Print/LoadQuery.aspx", false);

            return "0";

        }
    }
}
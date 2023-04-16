using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.AnalyticBundle.Service
{
    public class CashService
    {
        public DataTable getCashReportSaleRecord(string storeAccessParameters, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var cashModel = new CashReportModel();
            return cashModel.getCashReportSaleRecordModel(storeAccessParameters, dateTimeFrom, dateTimeTo);
        }




        public DataTable getCashReportSaleRecordByCashType(string storeAccessParameters, DateTime dateTimeFrom, DateTime dateTimeTo, string cashType)
        {
            var cashModel = new CashReportModel();
            return cashModel.getCashReportSaleRecordByCashTypeModel(storeAccessParameters, dateTimeFrom, dateTimeTo, cashType);
        }

        public string cashReportSummaryPayMethod(string storeAccessParameters, int payMethod, DateTime dateFrom, DateTime dateTo)
        {
            var cashModel = new CashReportModel();
            var dtCashReportPayMethod = cashModel.getCashReportByPayMethodModel(storeAccessParameters, payMethod, dateFrom, dateTo);
            if (dtCashReportPayMethod.Rows[0][0].ToString() == "")
                return "0";
            else
                return dtCashReportPayMethod.Rows[0][0].ToString();
        }
    }


}
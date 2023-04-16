using System;
using System.Collections.Generic;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.Controller
{


    public class CashReportController
    {


        private CashReportModel objCashReportModelModel = new CashReportModel();
        private decimal cashIn, cashInHand;
        private char adjust; //status;
        private string billNo;





        public CashReportController()
        {
            cashIn = 0;
            cashInHand = 0;
            billNo = "";
            //status = '2';
            adjust = '0';
        }





        public void createCashReport(Dictionary<string, string> dicData)
        {
            objCashReportModelModel.cashType = dicData["cashType"];
            objCashReportModelModel.descr = dicData["descr"];
            objCashReportModelModel.cashIn = cashIn;
            objCashReportModelModel.cashOut = Convert.ToDecimal(dicData["cashOut"]);
            objCashReportModelModel.cashInHand = cashInHand;
            objCashReportModelModel.billNo = billNo;
            objCashReportModelModel.mainDescr = "";
            objCashReportModelModel.status = '6';
            objCashReportModelModel.adjust = adjust;
            objCashReportModelModel.createCashReport();
        }


    }


}
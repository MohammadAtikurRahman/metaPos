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
    public class SaleCashReport
    {


        public string cashType { get; set; }
        public string descr { get; set; }
        public decimal cashIn { get; set; }
        public decimal cashOut { get; set; }
        public decimal cashInHand { get; set; }
        public DateTime entryDate { get; set; }
        public string billNo { get; set; }
        public string mainDescr { get; set; }
        public int roleId { get; set; }
        public int branchId { get; set; }
        public int groupId { get; set; }
        public char status { get; set; }
        public char adjust { get; set; }
        public bool isSchedulePayment { get; set; }
        public bool isScheduled { get; set; }
        public bool isReceived { get; set; }
        public decimal trackAmt { get; set; }
        public int storeId { get; set; }
        public string payMethod { get; set; }
        public string purchaseCode { get; set; }
        public string cardType { get; set; }
        public DateTime maturityDate { get; set; }
        public string bankName { get; set; }
        public string checkNo { get; set; }
        // work here
        public string payType { get; set; }
        public string payDescr { get; set; }


        private SqlOperation sqlOperation = new SqlOperation();


        public string cashTransactionSales()
        {
            string query = "INSERT INTO [CashReportInfo] (cashType," +
                                                "descr," +
                                                "cashIn," +
                                                "cashOut," +
                                                "cashInHand," +
                                                "entryDate," +
                                                "billNo," +
                                                "mainDescr," +
                                                "roleID," +
                                                "branchId," +
                                                "groupId," +
                                                "status," +
                                                "adjust," +
                                                "isSchedulePayment," +
                                                "isScheduled," +
                                                "isReceived," +
                                                "trackAmt," +
                                                "storeId," +
                                                "payMethod," +
                                                "purchaseCode," +
                                                "cardType," +
                                                "MaturityDate," + 
                                                "BankName," + 
                                                "checkNo," + 
                                                "payType," + 
                                                "payDescr) VALUES ('" +
                                                cashType + "', '" +
                                                descr + "', '" +
                                                cashIn + "', '" +
                                                cashOut + "', '" +
                                                cashInHand + "', '" +
                                                entryDate + "', '" +
                                                billNo + "', '" +
                                                mainDescr + "', '" +
                                                HttpContext.Current.Session["roleId"] + "', '" +
                                                HttpContext.Current.Session["branchId"] + "', '" +
                                                HttpContext.Current.Session["groupId"] + "', '" +
                                                status + "', '" +
                                                adjust + "','" + 
                                                isSchedulePayment + "','" +
                                                isScheduled + "','" +
                                                isReceived + "','" +
                                                trackAmt + "','" +
                                                HttpContext.Current.Session["storeId"] + "','" +
                                                payMethod + "','" +
                                                purchaseCode + "','" +
                                                cardType + "','" +
                                                maturityDate + "','" +
                                                bankName + "','" +
                                                checkNo + "','" +
                                                payType + "','" +
                                                payDescr + "')";

            return query;
        }


        public void saleCashReportInfo(string billNo, string returnAmt)
        {
            var saleModel = new SaleModel();
            var dtSaleInfo = saleModel.getSaleInfoDataListModel(billNo);

            var totalPaid = Convert.ToDecimal(dtSaleInfo.Rows[0]["balance"].ToString());

            if (totalPaid > 0)
            {
                var commonFunction = new CommonFunction();

                commonFunction.cashTransactionSales(0, Convert.ToDecimal(returnAmt), "Sales Return", billNo, billNo, "0", "5", "0",
                    commonFunction.GetCurrentTime().ToString());
            }
        }


        public string saleCashReportInfoData(string billNo, string returnAmt)
        {
            var transactionQuery = "";
            var saleModel = new SaleModel();
            var dtSaleInfo = saleModel.getSaleInfoDataListModel(billNo);

            var totalPaid = Convert.ToDecimal(dtSaleInfo.Rows[0]["balance"].ToString());

            if (totalPaid > 0)
            {
                var commonFunction = new CommonFunction();

                
                transactionQuery += "BEGIN ";
                transactionQuery += commonFunction.cashTransactionSalesData(0, Convert.ToDecimal(returnAmt), "Sales Return", billNo, billNo, "0", "5", "0",
                    commonFunction.GetCurrentTime().ToString(), payType, payDescr);
                transactionQuery += "END ";
            }

            return transactionQuery;
        }





        public void getSaleCashReport(string billNo)
        {
            var cashReportModel = new CashReportModel();
            var dtCashReport = cashReportModel.getCashReportDataModel(billNo);

        }



        public string saveMiscCostToExpense(dynamic data)
        {
            var transactionQuery = "";
            var miscCost = Convert.ToDecimal(data["loadingCost"]) + Convert.ToDecimal(data["unloadingCost"]) +
                           Convert.ToDecimal(data["serviceCharge"]) + Convert.ToDecimal(data["shippingCost"]) +
                           Convert.ToDecimal(data["carryingCost"]);
            if (miscCost > 0)
            {
                var cashReportModel = new CashReportModel();
                cashReportModel.cashOut = miscCost;
                cashReportModel.billNo = data["billNo"].ToString();
                cashReportModel.cashType = "Invoice misc. cost";
                cashReportModel.status = '2';
                cashReportModel.payMethod = "0";
                cashReportModel.entryDate = Convert.ToDateTime(data["entryDate"]);
                cashReportModel.payType = payType;
                cashReportModel.payDescr = payDescr;

                transactionQuery += "BEGIN ";
                transactionQuery += cashReportModel.saveCustomerCashData();
                transactionQuery += "END ";
            }

            return transactionQuery;
        }


    }
}
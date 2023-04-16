using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.CustomerBundle.Service
{
    public class CustomerPayment
    {

        SqlOperation sqlOperation = new SqlOperation();
        public bool advancePayment(string cusid, string advancePayAmt)
        {

            return sqlOperation.fireQuery("UPDATE CustomerInfo SET advance = advance +" + advancePayAmt + " where cusId='" + cusid + "'");
        }

        public bool advanceCashReportInfo(string cusid, decimal advancePay)
        {
            CommonFunction commonFunction = new CommonFunction();
            commonFunction.cashTransactionSales(advancePay, 0, "Advance Payment", "",
                cusid, "", "3", "0", commonFunction.GetCurrentTime().ToString());
            return true;
        }

        public bool updateSaleInfoByDirectCustomer(string billNo, string cusId, string preRoleId, string pay)
        {
            var saleModel = new SaleModel();
            return saleModel.updateSaleInfoByDirectCustomerModel(billNo, cusId, preRoleId, pay);
        }

        public bool updateSlipInfoByDirectCustomer(string billNo, string cusId, string preRoleId, string pay)
        {
            var slipModel = new SlipModel();
            return slipModel.updateSlipInfoByDirectCustomerModel(billNo, cusId, preRoleId, pay);
        }

        public bool saveStockStatusInfoByDirectCustomer(string billNo, string cusId, string preRoleId, string pay)
        {
            var stockStatus = new StockStatusModel();
            return stockStatus.saveStockStatusInfoByDirectCustomerModel(billNo, cusId, preRoleId, pay);
        }

        public bool updateCustomer(string billNo, string cusId, string preRoleId, string pay)
        {
            var customer = new CustomerModel();
            return customer.updateCustomerModel(cusId, pay);
        }

        public bool saveCashReportInfoByDirectCustomer(string billNo, string cusId, string preRoleId, string pay)
        {
            var cashReport = new CashReportModel();
            return cashReport.saveCashReportInfoByDirectCustomerModel(billNo, cusId, preRoleId, pay);
        }
    }
}
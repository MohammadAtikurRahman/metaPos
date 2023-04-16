using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.SaleBundle.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SaleCustomer : Customers
    {
        private CommonFunction commonFunction = new CommonFunction();
        private SqlOperation objSqlOperation = new SqlOperation();
        //private DataSet ds;
        //private string query = "";

        public string initializeCustomer()
        {
            var commonFunction = new CommonFunction();
            var customerModel = new CustomerModel();
            customerModel.active = "1";
            customerModel.CusType = "All";


            return customerModel.getCustomerListOfflineSerializeModel();
        }


        public string saleSuspendCustomerInfo(string billNo, string returnAmt, decimal balance)
        {
            var transactionQuery = "";
            var saleModel = new SaleModel();
            var saleCustomer = new CustomerModel();
            var customerModel = new CustomerModel();

            string cusId = "";
            decimal invoiceDueAmt = 0, totalPaid = 0, grossAmt = 0;

            //get sale data
            var dtSale = saleModel.getSaleInfoDataListModel(billNo);
            if (dtSale.Rows.Count > 0)
            {
                cusId = dtSale.Rows[0]["cusId"].ToString();
                invoiceDueAmt = Convert.ToDecimal(dtSale.Rows[0]["giftAmt"]);
                totalPaid = Convert.ToDecimal(dtSale.Rows[0]["balance"]);
                grossAmt = Convert.ToDecimal(dtSale.Rows[0]["grossAmt"]);
            }

            transactionQuery += "BEGIN ";
            transactionQuery += commonFunction.cashTransactionSalesData(grossAmt, 0, "Suspended ", cusId, billNo, "", "6", "0", commonFunction.GetCurrentTime().ToString(),null,null);
            transactionQuery += "END ";

            transactionQuery += "BEGIN ";
            transactionQuery += commonFunction.cashTransactionSalesData(0, Convert.ToDecimal(returnAmt), "Cash Return", cusId, billNo, "", "6", "0", commonFunction.GetCurrentTime().ToString(),null,null);
            transactionQuery += "END ";

            return transactionQuery;
        }


        





        public DataTable getCustomerData(string cusId)
        {
            var customerModel = new CustomerModel();
            customerModel.cusId = cusId;
            return customerModel.getCustomerDataModel();
        }


        public bool existsCustomer()
        {
            var customerModel = new CustomerModel();
            customerModel.cusId = cusId;
            var dtCustomer = customerModel.getCustomerDataModel();
            if (dtCustomer.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public string saveCustomerData(dynamic data)
        {
            var customerModel = new CustomerModel();
            customerModel.name = data["name"].ToString();
            customerModel.phone = data["phone"].ToString();
            customerModel.address = data["address"].ToString();
            customerModel.mailInfo = data["email"].ToString();
            customerModel.notes = data["notes"].ToString();
            customerModel.CusType = data["cusType"].ToString();
            customerModel.accountNo = data["accountNo"].ToString();
            customerModel.installmentStatus = Convert.ToBoolean(data["installmentStatus"]);
            customerModel.designation = "";

            return customerModel.saveCustomerInfoModel();
        }


        public string updateCustomerDataForBatchQuery(dynamic data)
        {
            var customerModel = new CustomerModel();
            customerModel.cusId = data["cusId"].ToString();
            customerModel.name = data["name"].ToString();
            customerModel.phone = data["phone"].ToString();
            customerModel.address = data["address"].ToString();
            customerModel.notes = data["notes"].ToString();
            customerModel.CusType = data["cusType"].ToString();
            customerModel.mailInfo = data["email"].ToString();
            customerModel.accountNo = data["accountNo"].ToString();

            return customerModel.updateCustomerDataModelForBatchQuery();
        }



    }


}
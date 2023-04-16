using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleInstallment
    {

        private CommonFunction commonFunction = new CommonFunction();

        public string suspendInstallment(string billNo)
        {
            var transactionQuery = "";

            var installmentModel = new InstallmentModel();
            installmentModel.billNo = billNo;
            transactionQuery += "BEGIN ";
            transactionQuery += installmentModel.suspendInstallmentModel();
            transactionQuery += "END ";

            return transactionQuery;
        }



        public string CreateInstallment(dynamic data)
        {
            var transactionQuery = "";

            int instalmentNumber = Convert.ToInt32(data["instalmentNumber"]);
            if (instalmentNumber <= 0)
                return "";

            string cusId = data["cusId"].ToString();
            var nextPayDate = Convert.ToDateTime(data["nextPayDate"]);
            string billNo = data["billNo"].ToString();

            decimal downPayment = Convert.ToDecimal(data["downPayment"]);
            decimal totalAmt = Convert.ToDecimal(data["grossAmt"]);
            decimal payCash = Convert.ToDecimal(data["payCash"]);
            string instalmentIncrement = data["instalmentIncrement"].ToString();

            if (nextPayDate.ToString() == "")
            {
                nextPayDate = DateTime.MinValue;
            }


            if (nextPayDate < Convert.ToDateTime("01/01/2017"))
                return " ";

            var customerRemainderModel = new InstallmentModel();
            DataTable dtCustomer = customerRemainderModel.getCustomerReminderByBillNoModel(billNo);
            //bool isSaved = true;
            if (dtCustomer.Rows.Count > 0)
            {
                //customerRemainderModel.CustomerId = cusId;
                //customerRemainderModel.nextPayDate = nextPayDate;
                //customerRemainderModel.billNo = billNo;

                //isSaved= customerRemainderModel.updateCustomerRemainderNextPayDate();
            }
            else
            {
                for (int i = 0; i < instalmentNumber; i++)
                {
                    customerRemainderModel.CustomerId = cusId;
                    customerRemainderModel.nextPayDate = nextPayDate;
                    customerRemainderModel.actve = "1";
                    customerRemainderModel.status = "0";
                    customerRemainderModel.billNo = billNo;
                    customerRemainderModel.instalmentNumber = instalmentNumber;
                    customerRemainderModel.downPayment = downPayment;
                    customerRemainderModel.paidAmt = 0M;

                    transactionQuery += "BEGIN ";
                    transactionQuery += customerRemainderModel.saveCustomerRemainderNextPayDate();
                    transactionQuery += "END ";

                    if (instalmentIncrement == "1m")
                        nextPayDate = nextPayDate.AddMonths(1);
                    else if (instalmentIncrement == "3m")
                        nextPayDate = nextPayDate.AddMonths(3);
                    else if (instalmentIncrement == "6m")
                        nextPayDate = nextPayDate.AddMonths(6);
                    else if (instalmentIncrement == "1y")
                        nextPayDate = nextPayDate.AddYears(1);
                    else if (instalmentIncrement == "7")
                        nextPayDate = nextPayDate.AddDays(7);
                    else if (instalmentIncrement == "15")
                        nextPayDate = nextPayDate.AddDays(15);

                }

                decimal extraReminder = totalAmt - ((downPayment * instalmentNumber) + payCash);

                if (extraReminder > 0)
                {
                    customerRemainderModel.CustomerId = cusId;
                    customerRemainderModel.nextPayDate = nextPayDate;
                    customerRemainderModel.actve = "1";
                    customerRemainderModel.status = "0";
                    customerRemainderModel.billNo = billNo;
                    customerRemainderModel.instalmentNumber = instalmentNumber;
                    customerRemainderModel.downPayment = extraReminder;
                    transactionQuery += "BEGIN ";
                    transactionQuery += customerRemainderModel.saveCustomerRemainderNextPayDate();
                    transactionQuery += "END ";
                }


            }

            return transactionQuery;

        }
    }
}
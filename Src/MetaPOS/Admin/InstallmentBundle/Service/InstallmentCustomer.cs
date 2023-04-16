using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.InstallmentBundle.Service
{
    public class InstallmentCustomer
    {
        SqlOperation sqlOperation = new SqlOperation();
        public string getCustomerReminderInfoByBillNo(string billNo)
        {
            var commonFunction = new CommonFunction();
            var customerRemainderModel = new InstallmentModel();
            var dt = customerRemainderModel.getCustomerReminderByBillNoModel(billNo);
            if (dt.Rows.Count > 0)
                return commonFunction.serializeDatatableToJson(dt);
            else
                return "";
        }

        public string ChangePayStatus(string billNo, decimal payCash, string reminderId)
        {
            var transactionQuery = "";


            var reminderModel = new ReminderModel();
            var dtReminder = reminderModel.getCustomerReminderDataListModel(billNo);

            decimal downPayment = 0, paidAmt = 0;
            if (dtReminder.Rows.Count > 0)
            {
                downPayment = Convert.ToDecimal(dtReminder.Rows[0]["downPayment"]);
                paidAmt = Convert.ToDecimal(Convert.ToDecimal(dtReminder.Rows[0]["paidAmt"].ToString()));

            }

            if (payCash == downPayment && paidAmt == 0)
            {
                transactionQuery += "BEGIN ";
                transactionQuery += reminderModel.changePayStatusModel(reminderId);
                transactionQuery += "END ";

                transactionQuery += "BEGIN ";
                transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payCash);
                transactionQuery += "END ";
            }
            else
            {

                if (paidAmt + payCash > downPayment)
                {
                    var payable = downPayment - paidAmt;

                    transactionQuery += "BEGIN ";
                    transactionQuery += reminderModel.changePayStatusModel(reminderId);
                    transactionQuery += "END ";

                    transactionQuery += "BEGIN ";
                    transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payable);
                    transactionQuery += "END ";

                    payCash -= payable;
                }
                else
                {
                    transactionQuery += "BEGIN ";
                    transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payCash);
                    transactionQuery += "END ";

                    payCash = 0;
                }

                // Additional Payment
                if (payCash > 0)
                {
                    dtReminder = reminderModel.getCustomerReminderDataListModelOrderByDesc(billNo);
                    for (int i = 0; i < dtReminder.Rows.Count; i++)
                    {
                        reminderId = dtReminder.Rows[i][0].ToString();
                        paidAmt = Convert.ToDecimal(Convert.ToDecimal(dtReminder.Rows[i]["paidAmt"].ToString()));

                        if (payCash + paidAmt >= downPayment)
                        {
                            var payableAmt = downPayment - paidAmt;
                            transactionQuery += "BEGIN ";
                            transactionQuery += reminderModel.changePayStatusModel(reminderId);
                            transactionQuery += "END ";

                            transactionQuery += "BEGIN ";
                            transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payableAmt);
                            transactionQuery += "END ";

                            payCash -= payableAmt;
                        }
                        else
                        {
                            if (payCash <= 0)
                                break;

                            transactionQuery += "BEGIN ";
                            transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payCash);
                            transactionQuery += "END ";
                            break;
                        }

                    }
                }

            }





            // check due installment
            //for (int i = 0; i < dtReminder.Rows.Count; i++)
            //{
            //    var installmentDueAmt = Convert.ToDecimal(dtReminder.Rows[i]["paidAmt"].ToString());
            //    if (installmentDueAmt < downPayment)
            //    {
            //        transactionQuery += "BEGIN ";
            //        transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payCash);
            //        transactionQuery += "END ";
            //    }
            //}


            //if (payCash == downPayment)
            //{

            //    transactionQuery += "BEGIN ";
            //    transactionQuery += reminderModel.changePayStatusModel(reminderId);
            //    transactionQuery += "END ";

            //    transactionQuery += "BEGIN ";
            //    transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payCash);
            //    transactionQuery += "END ";
            //    reminderModel.allChangePayStatusModel(billNo);
            //}
            //else if (payCash < downPayment)
            //{
            //    transactionQuery += "BEGIN ";
            //    transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payCash);
            //    transactionQuery += "END ";
            //    reminderModel.allChangePayStatusModel(billNo);

            //}
            //else
            //{
            //    decimal dp = downPayment;
            //    for (int i = 0; i < dtReminder.Rows.Count; i++)
            //    {
            //        if (payCash > dp)
            //        {
            //            reminderId = dtReminder.Rows[i][0].ToString();
            //            transactionQuery += "BEGIN ";
            //            transactionQuery += reminderModel.changePayStatusModel(reminderId);
            //            transactionQuery += "END ";

            //            payCash -= downPayment;

            //            transactionQuery += "BEGIN ";
            //            transactionQuery += reminderModel.UpdateCustomerRemainderPaidAmountModel(reminderId, payCash);
            //            transactionQuery += "END ";
            //            reminderModel.allChangePayStatusModel(billNo);
            //        }
            //    }
            //}

            return transactionQuery;
        }

        public string keepInstallmentDueAfterPayment(string jsonData)
        {
            var returnData = "";
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            /* Customer payment */
            var payment = data["payment"].Value<decimal>();
            var cusId = data["id"].Value<string>();

            var installmentModel = new InstallmentModel();
            var dtInstallment = installmentModel.getInstallmentByCustomer(cusId);
            var installmentDueAmt = 0M;
            if (dtInstallment.Rows[0][0].ToString() != "")
                installmentDueAmt = Convert.ToDecimal(dtInstallment.Rows[0][0].ToString());

            var customerTotalDue = 0M;
            var dtCustomer = installmentModel.getCustomerTotalDue(cusId);
            string fvv = dtCustomer.Rows[0][0].ToString();
            if (dtCustomer.Rows[0][0] != null)
                customerTotalDue = Convert.ToDecimal(dtCustomer.Rows[0][0].ToString());

            var dueWithoutInstallment = customerTotalDue - installmentDueAmt;

            if (payment > dueWithoutInstallment)
            {
                dueWithoutInstallment = Convert.ToDecimal(dueWithoutInstallment) < 0M ? 0 : dueWithoutInstallment;
                returnData = "{'status': false,'message':'You can not payment more then " + dueWithoutInstallment +
                             " '}";

                return returnData;
            }
            returnData = "{'status': true,'message':'You can payment'}"; ;

            return returnData;

        }
    }
}
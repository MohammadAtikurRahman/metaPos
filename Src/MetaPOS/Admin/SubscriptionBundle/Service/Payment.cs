using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SubscriptionBundle.Service
{
    public class Payment
    {
        private CommonFunction commonFunction = new CommonFunction();

        public bool confirmPayment(string paymentId, string status, int numberOfMonth, string totalFee, string payment, string branchId)
        {
            bool msg = false;
            if (status == "2")
            {
                var paymentModel = new PaymentModel();
                paymentModel.Id = Convert.ToInt32(paymentId);
                paymentModel.Status = status;
                paymentModel.UpdateDate = commonFunction.GetCurrentTime();
                paymentModel.ChangeStatusModel();

                HttpContext.Current.Response.Redirect("~/admin/payment?msg=rejected");
            }
            else if (status == "1")
            {
                if (Convert.ToDecimal(payment) <= 0M)
                {
                    HttpContext.Current.Response.Redirect("~/admin/payment?msg=zero");
                }

                try
                {
                    var paymentModel = new PaymentModel();
                    paymentModel.Id = Convert.ToInt32(paymentId);
                    paymentModel.NumberOfMonth = numberOfMonth;
                    paymentModel.TotalFee = totalFee;
                    paymentModel.Payment = payment;
                    paymentModel.Status = status;
                    paymentModel.UpdateDate = commonFunction.GetCurrentTime();
                    paymentModel.confirmPaymentModel();

                    msg = true;
                }
                catch (Exception)
                {
                    msg = false;
                }
            }

            return msg;

        }
    }
}
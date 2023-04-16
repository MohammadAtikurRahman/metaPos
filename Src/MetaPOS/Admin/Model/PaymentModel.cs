using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class PaymentModel
    {
        private  SqlOperation sqlOperation  = new SqlOperation();

        public int  Id { get; set; }
        public int NumberOfMonth { get; set; }
        public string TotalFee { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
        public DateTime UpdateDate { get; set; }



        public bool confirmPaymentModel()
        {
            return
                sqlOperation.fireQuery("UPDATE SubscriptionInfo SET status='"+Status+"',amount='" + Payment + "', updateDate='"+UpdateDate+"' WHERE Id='" + Id + "'");
        }

        public bool ChangeStatusModel()
        {
            return
               sqlOperation.fireQuery("UPDATE SubscriptionInfo SET status='" + Status + "', updateDate='" + UpdateDate + "' WHERE Id='" + Id + "'");
        }
    }
}
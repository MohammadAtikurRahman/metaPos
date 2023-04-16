using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.SaleBundle.Entity
{
    public class Customers
    {
        // declaration
        public string nextCusID { get; set; }
        public string cusId { get; set; }

        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string mailInfo { get; set; }

        //entry date
        public DateTime entryDate { get; set; }

        //update date
        //total paid
        public decimal totalPaid { get; set; }
        //role id
        //total due
        public decimal totalDue { get; set; }
        public string refName { get; set; }
        public string refPhone { get; set; }
        public string refAddress { get; set; }
        public int branchId { get; set; }
        public int groupId { get; set; }
        public string phone2 { get; set; }
        public string CusType { get; set; }
        public string bloodGroup { get; set; }
        public string orderId { get; set; }
        public string memberId { get; set; }
        public decimal openingDue { get; set; }
        public string notes { get; set; }

        public string accountNo { get; set; }
        public bool installmentStatus { get; set; }

        public string PayStatus { get; set; }
        public string active { get; set; }
        public decimal advanceAmt { get; set; }
        public string designation { get; set; }
        public string sex { get; set; }
        public int age { get; set; }

        public string parameterAccess { get; set; }
    }
}
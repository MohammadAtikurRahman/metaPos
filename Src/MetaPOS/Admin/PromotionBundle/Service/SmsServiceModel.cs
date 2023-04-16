using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.PromotionBundle.Service
{
    public class SmsServiceModel
    {
        public string phoneList { set; get; }

        public string message { set; get; }
        public string medium { set; get; }
        public string apiKey { set; get; }
        public string senderId { set; get; }
        public string username { set; get; }
        public string password { set; get; }
  
    }
}
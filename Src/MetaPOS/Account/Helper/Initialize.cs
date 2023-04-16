using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Account.Helper
{
    public class Initialize
    {
        public void SessionInitlize()
        {
            HttpContext.Current.Session["SessionAccessPage"] = "";
            HttpContext.Current.Session["pageName"] = "";
            HttpContext.Current.Session["reportQury"] = "";
            HttpContext.Current.Session["expiryDate"] = DateTime.Now.AddDays(30);
            HttpContext.Current.Session["roleId"] = "";
        }
    }
}
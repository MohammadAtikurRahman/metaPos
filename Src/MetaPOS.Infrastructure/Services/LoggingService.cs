using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MetaPOS.Infrastructure.Services
{
    public class LoggingService
    {
        public bool isLoggedIn()
        {
            if (HttpContext.Current.Session["email"] != null)
                return true;
            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;


namespace MetaPOS.Admin.DataAccess
{
    public static class GlobalVariable
    {

        public static string connectionString;
        private static CommonFunction commonFunction = new CommonFunction();




        public static string getConnectionStringName()
        {
          
            string conString = "localhost";
            /*try
            {
                conString = HttpContext.Current.Session["conString"].ToString();
            }
            catch (Exception)
            {
                conString = "localhost";
            }*/
            return conString;
        }

    }
}
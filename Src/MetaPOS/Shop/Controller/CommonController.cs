using System;
using System.Web;


namespace MetaPOS.Shop.Controller
{


    public class CommonController
    {


        public string getDomainPartOnly()
        {
            string url = HttpContext.Current.Request.Url.Authority;

            try
            {
                if (url.Substring(0, 11) == "http://www.")
                {
                    url = url.Substring(11, url.Length - 11);
                }
                else if (url.Substring(0, 4) == "www.")
                {
                    url = url.Substring(4, url.Length - 4);
                }
                else
                {
                    url = HttpContext.Current.Request.Url.Host;
                }
            }
            catch (Exception)
            {
                url = HttpContext.Current.Request.Url.Host;
            }

            return url;
        }


    }


}
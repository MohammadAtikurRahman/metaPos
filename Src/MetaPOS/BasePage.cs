using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using DocumentFormat.OpenXml.Math;
using  MetaPOS.Admin.AppBundle;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            //GET current language fro 
            var commonFunction = new CommonFunction();

            string language = "en";
            string al = commonFunction.findSettingItemValueDataTable("language");
            if (commonFunction.findSettingItemValueDataTable("language") == "bn")
                language = "en-US";

            //Set the Culture.
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            //System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);
            //customCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy h:mm tt";
        }

    }
}
using System;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.PromotionBundle.View
{


    
    public partial class SmsConfig : System.Web.UI.Page
    {

        CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(!commonFunction.accessChecker("SmsConfig"))
                {
                    commonFunction.pageout();
                }
            }

        }

        
    }
}
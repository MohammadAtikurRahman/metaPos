using System;
using System.Web.UI;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.SMSBundle.Service;


namespace MetaPOS.Admin.SMSBundle.View
{


    public partial class SMS: Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var objCommonFun = new CommonFunction();

                if(!objCommonFun.accessChecker("SMS"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }

                redirectSMSPanel();
            }
        }





        private void redirectSMSPanel()
        {
            var objSMSService = new SMSService();
            dynamic objSMSEntity = objSMSService.findSMSConfigInfo();

            var objCommonController = new Shop.Controller.CommonController();
            var url = objCommonController.getDomainPartOnly();
           

            Response.Redirect("http://sms." + url + "/login/vendor?username=" + objSMSEntity.username + "&password=" + objSMSEntity.password + "&apiKey=" + objSMSEntity.apiKey);
        }


    }
}
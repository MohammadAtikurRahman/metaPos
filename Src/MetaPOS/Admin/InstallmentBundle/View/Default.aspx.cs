using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.AppBundle.Service;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.InstallmentBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.InstallmentBundle.View
{
    public partial class Default :BasePage
    {
        private CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblHiddenCompanyName.Value = Session["comName"].ToString();
                lblHiddenCompanyAddress.Value = Session["comAddress"].ToString();
                lblHiddenCompanyPhone.Value = Session["comPhone"].ToString();
            }
            catch (Exception)
            {
                commonFunction.pageout();
            }
        }





        [WebMethod]
        public static string updateCustomerReminderDataAction(string jsonDataCustomer, string jsonDataReminder)
        {
            var dataService = new DataService();
            dataService.updateData(jsonDataCustomer);

            return dataService.updateData(jsonDataReminder);
        }

        [WebMethod]
        public static string getReminderInfoAction(string billNo)
        {
            var reminderCustomer = new InstallmentCustomer();
            return reminderCustomer.getCustomerReminderInfoByBillNo(billNo);

        }




    }
}
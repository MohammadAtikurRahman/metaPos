using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Offline.Service;


namespace MetaPOS.Offline
{
    public partial class Offline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }





        [WebMethod]
        public static string initializeCustomerAction()
        {
            var customer = new Service.Customer();
            return customer.initializeCustomer();
        }



        [WebMethod]
        public static string initializeProductAction()
        {
            var stockProduct = new Stock();
            return stockProduct.initializeProduct();
        }
    }
}
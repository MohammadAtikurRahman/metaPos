using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.InventoryBundle.Service;


namespace MetaPOS.Admin.InventoryBundle.View
{
    public partial class StockOpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }





        [WebMethod]
        public static string getSupplierCommisionAction(string supId)
        {
            var supplierCommision = new SupplierCommision();
            return supplierCommision.getSupplierCommision(supId);
        }
    }
}
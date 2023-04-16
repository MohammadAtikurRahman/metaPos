using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Print
{


    public partial class GodownInvoiceLoader : System.Web.UI.Page
    {


        private CommonFunction objCommonFun = new CommonFunction();
        private GodownInvoice objGodownInvoice = new GodownInvoice();






        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }


    }


}
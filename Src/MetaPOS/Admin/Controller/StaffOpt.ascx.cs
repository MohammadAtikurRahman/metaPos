using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Controller
{
    public partial class StaffOpt : System.Web.UI.UserControl
    {
        private  CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblStoreId.Text = Session["storeId"].ToString();
                }
                catch (Exception)
                {
                    commonFunction.pageout();
                }
                
            }
        }
    }
}
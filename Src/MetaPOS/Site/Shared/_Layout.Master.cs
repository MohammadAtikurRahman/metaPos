using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.ApiBundle.Controllers;
using MetaPOS.Admin.Controller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Site.Shared
{
    public partial class _Layout : System.Web.UI.MasterPage
    {
        private CommonController objCommonController = new CommonController();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string url = objCommonController.getDomainPartOnly();

            if (url == "web.metaposbd.com" || url == "www.web.metaposbd.com")
            {
                Response.Redirect("http://web.metaposbd.com/login");
            }
            else if(url == "metaposbd.com" || url == "www.metaposbd.com")
            {
                Response.Redirect("http://app.metaposbd.com/login");
            }
            else
            {
                Response.Redirect("/login");
            }
        }
    }
}
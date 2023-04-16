using System;
using System.IO;
using System.Web;
using MetaPOS.Admin.Model;
using System.Data;


namespace MetaPOS
{


    public partial class Default : System.Web.UI.Page
    {


        private Shop.Controller.CommonController objCommonController = new Shop.Controller.CommonController();
        //RoleModel roleModel = new RoleModel();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblRedirection.Text = "1";
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                string host = HttpContext.Current.Request.Url.Host;
                string url = objCommonController.getDomainPartOnly();

                if (host == "localhost")
                {
                    Response.Redirect("login");
                    //Response.Redirect("account/login?domain=" + path.Replace("/", ""));

                }
                else if ((url == "www.metaposbd.com" || url == "metaposbd.com" || url == "web.metaposbd.com" || url == "www.metaposbd.com"))
                {
                    Response.Redirect("/web");
                }

                else if (Directory.Exists(Server.MapPath("Shop")))
                {
                    Response.Redirect("shop");
                }
                else if (Directory.Exists(Server.MapPath("Site")))
                {
                    Response.Redirect("site");
                }
                else
                {
                    Response.Redirect("login");
                    // Response.Redirect("account/login?domain=" + path.Replace("/", ""));
                }

                Response.Redirect("login");
            }
        }


    }


}
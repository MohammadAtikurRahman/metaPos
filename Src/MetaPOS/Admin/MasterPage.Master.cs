using System;
using System.Globalization;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin
{


    public partial class MasterPage : System.Web.UI.MasterPage
    {


        private CommonFunction commonFunction = new CommonFunction();





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                addOptGlobal.Value = commonFunction.accessChecker("Add").ToString();

                editOptGlobal.Value = commonFunction.accessChecker("Edit").ToString();

                deleteOptGlobal.Value = commonFunction.accessChecker("Delete").ToString();

                roleIdGlobal.Value = HttpContext.Current.Session["roleID"].ToString();

                branchIdGlobal.Value = HttpContext.Current.Session["branchID"].ToString();

                userRightGlobal.Value = HttpContext.Current.Session["userRight"].ToString();

                currentDatetimeGlobal.Value = commonFunction.GetCurrentTime().ToString();

                accessPagesGlobal.Value = HttpContext.Current.Session["accessPage"].ToString();

                storeIdGlobal.Value = HttpContext.Current.Session["storeId"].ToString();

                storeExpiryDate.Value = HttpContext.Current.Session["expiryDate"].ToString();

                expiryNotification.Value = HttpContext.Current.Session["expiryNotification"].ToString();


                companyName.Value = HttpContext.Current.Session["comName"].ToString();

                companyEmail.Value = HttpContext.Current.Session["email"].ToString();

                companyDomain.Value = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).AbsoluteUri;

            }
        }

        


    }


}
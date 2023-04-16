using System;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.RecordBundle.View
{
    public partial class Manufacturer : BasePage//System.Web.UI.Page
    {
        protected void Page_load(object sender,EventArgs e)
        {
            try
            {
                lblHiddenCompanyName.Value = Session["comName"].ToString();
                lblHiddenCompanyAddress.Value = Session["comAddress"].ToString();
                lblHiddenCompanyPhone.Value = Session["comPhone"].ToString();
            }
            catch (Exception )
            {
               
            }
        }

    }
}
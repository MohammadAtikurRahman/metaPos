using System;


namespace MetaPOS.Admin.RecordBundle.View
{
    public partial class Supplier : BasePage//System.Web.UI.Page
    {


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
                
            }
        }


    }
}
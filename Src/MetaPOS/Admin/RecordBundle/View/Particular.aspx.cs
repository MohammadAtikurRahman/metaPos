using System;


namespace MetaPOS.Admin.RecordBundle.View
{
    public partial class Particular :BasePage ///System.Web.UI.Page
    {

        protected void Page_load(object Sender,EventArgs e)
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
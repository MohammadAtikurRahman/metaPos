using System;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.RecordBundle.View
{


    public partial class Staff :BasePage //System.Web.UI.Page
    {
        private  CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblHiddenCompanyName.Value = Session["comName"].ToString();
                    lblHiddenCompanyAddress.Value = Session["comAddress"].ToString();
                    lblHiddenCompanyPhone.Value = Session["comPhone"].ToString();
                    lblUserRight.Text = Session["userRight"].ToString();
                }
                catch (Exception)
                {

                }
                
            }
        }


    }


}
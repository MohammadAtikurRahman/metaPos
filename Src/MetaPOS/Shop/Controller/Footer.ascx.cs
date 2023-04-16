using System;
using System.Data;


namespace MetaPOS.Controller
{


    public partial class Footer : System.Web.UI.UserControl
    {


        private Shop.Model.Shop objWebModel = new Shop.Model.Shop();
        private DataSet ds;





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getHeaderInfo();
                copyRightYear.InnerText = DateTime.Now.Year.ToString();
            }
        }





        public void getHeaderInfo()
        {
            ds = objWebModel.getWeb();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblContact.Text = ds.Tables[0].Rows[0][4].ToString();
                lblAddress.Text = ds.Tables[0].Rows[0][5].ToString();
                lblEmailAddress.Text = ds.Tables[0].Rows[0][6].ToString();
                lblCompanyName.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                FooterControl.Visible = false;
            }
        }


    }


}
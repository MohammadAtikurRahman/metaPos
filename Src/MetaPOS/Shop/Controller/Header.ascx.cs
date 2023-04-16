using System;
using System.Data;


namespace MetaPOS.Shop.Controller
{


    public partial class Header : System.Web.UI.UserControl
    {


        private Model.Shop objWebModel = new Model.Shop();
        private DataSet ds;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getHeaderInfo();
            }
        }



        public void getHeaderInfo()
        {
            ds = objWebModel.getWeb();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblSiteTitle.Text = ds.Tables[0].Rows[0][0].ToString();
                lblSiteSlogan.Text = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                headerControl.Visible = false;
            }
        }


    }


}
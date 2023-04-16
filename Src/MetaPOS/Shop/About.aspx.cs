using System;
using System.Data;


namespace MetaPOS.Shop
{


    public partial class About : System.Web.UI.Page
    {


        private Model.Shop objWebModel = new Model.Shop();
        private DataSet ds;





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WebInfo();
            }
        }





        public void WebInfo()
        {
            ds = objWebModel.getWeb();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblAboutText.Text = ds.Tables[0].Rows[0][7].ToString();
            }
        }


    }


}
using System;
using System.Data;
using System.Web.UI;


namespace MetaPOS.Shop
{


    public partial class Default : Page
    {

        private Model.Shop objWebModel = new Model.Shop();

        private string displayFeatured = "0", displayNew = "0";

        private DataSet ds;





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get Header
                getHeaderInfo();

                // Featured Product
                getFeaturedInfo();

                // New Product
                getNewProductInfo();

                // Contact to order mobile number
                lblContactMobile.Text = objWebModel.getContact();
                callToNumber.HRef = "callto:" + lblContactMobile.Text;
                if (lblContactMobile.Text == "")
                {
                    lblContactMobile.Text = "No contact available";
                    callToNumber.HRef = "javascript:void(0);";
                }
            }
        }





        public void getHeaderInfo()
        {
            ds = objWebModel.getWeb();

            if (ds.Tables[0].Rows.Count > 0)
            {
                displayFeatured = ds.Tables[0].Rows[0][12].ToString();
                displayNew = ds.Tables[0].Rows[0][13].ToString();
                lblTitle.InnerText = "Home Page || " + ds.Tables[0].Rows[0][0];
            }
        }





        public void getFeaturedInfo()
        {
            objWebModel.displayFeatured = displayFeatured;

            ds = objWebModel.getFeaturedProduct();
            dlFeaturedProduct.DataSource = ds;
            dlFeaturedProduct.DataBind();
        }





        public void getNewProductInfo()
        {
            objWebModel.displayNew = displayNew;
            ds = objWebModel.getNewProduct();
            dlNewProduct.DataSource = ds;
            dlNewProduct.DataBind();
        }





        //
        //public void databind()
        //{
        //    string query = "SELECT TOP 4 * FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId WHERE RoleInfo.domainName='" + Request.Url.Host.ToString() + "'";
        //    ds = objSqlOperation.getDataSet(query);

        //    //SqlCommand cmd = new SqlCommand(query);
        //    //dadapter = new SqlDataAdapter();
        //    //cmd.Connection = vcon;
        //    //dadapter.SelectCommand = cmd;
        //    //dset = new DataSet();
        //    //adsource = new PagedDataSource();
        //    //dadapter.Fill(dset);
        //    //adsource.DataSource = dset.Tables[0].DefaultView;
        //    //adsource.PageSize = 8;
        //    //adsource.AllowPaging = true;
        //    //adsource.CurrentPageIndex = pos;
        //    dlSingleProduct.DataSource = ds;
        //    dlSingleProduct.DataBind();

        //}

        protected void btnViewDetailsImage_Click(object sender, ImageClickEventArgs e)
        {
        }





        protected void addToCart_Click(object sender, EventArgs e)
        {
        }


    }


}
using System;
using System.Data;
using System.Web.UI.HtmlControls;


namespace MetaPOS.Shop
{


    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private Model.Shop objWebModel = new Model.Shop();
        private DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getHeaderInfo();
            }
        }

        public void getHeaderInfo()
        {
            ds = objWebModel.getWeb();

            if(ds.Tables[0].Rows.Count > 0)
            {
                //lblTitle.Text = ds.Tables[0].Rows[0][0].ToString();

                //HtmlMeta tagTitle = new HtmlMeta();
                //tagTitle.Attributes.Add("property", "og:title");
                //tagTitle.Content = ds.Tables[0].Rows[0][0].ToString();
                //Page.Header.Controls.Add(tagTitle);

                //HtmlMeta tagDescr = new HtmlMeta();
                //tagDescr.Attributes.Add("property", "og:description");
                //tagDescr.Content = ds.Tables[0].Rows[0][7].ToString();
                //Page.Header.Controls.Add(tagDescr);

                //var tag = new HtmlMeta
                //{
                //    Name = ds.Tables[0].Rows[0][0].ToString(),
                //    Content = ds.Tables[0].Rows[0][7].ToString()
                //};
                //Page.Header.Controls.Add(tag);
            }
        }

    }


}
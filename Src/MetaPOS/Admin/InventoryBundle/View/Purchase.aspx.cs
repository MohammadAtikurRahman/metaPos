using MetaPOS.Admin.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MetaPOS.Admin.InventoryBundle.View
{
    public partial class Purchase : System.Web.UI.Page
    {


        private string whichPage = null;

        private CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (whichPage == "unit")
                {
                    if (!commonFunction.accessChecker("Stock"))
                    {
                        commonFunction.pageout();
                    }
                }
                else
                {
                    if (!commonFunction.accessChecker("Purchase"))
                    {
                        commonFunction.pageout();
                    }
                }

                MainTab.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
            }
        }



        protected void btnLoadProductDetails_Click(object sender, ImageClickEventArgs e)
        {

        }


        protected void Tab1_Click(object sender, EventArgs e)
        {
            MainTab.CssClass = "Clicked";
            OthersTab.CssClass = "Initial";
            DynamicTab.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            MainTab.CssClass = "Initial";
            OthersTab.CssClass = "Clicked";
            DynamicTab.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            MainTab.CssClass = "Initial";
            OthersTab.CssClass = "Initial";
            DynamicTab.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
        }

        protected void MainTab_Click(object sender, EventArgs e)
        {

        }

        protected void OthersTab_Click(object sender, EventArgs e)
        {

        }

        protected void DynamicTab_Click(object sender, EventArgs e)
        {

        }
    }
}
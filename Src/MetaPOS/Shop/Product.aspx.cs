using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MetaPOS.Shop
{


    public partial class Product : Page
    {


        private DataSet ds;

        private PagedDataSource adsource = null;
        private PagedDataSource pgsource = new PagedDataSource();

        private int findex, lindex;
        private int pos = 0;
        private static string CatSearch = "";

        // Initialize Model
        private Model.Shop objWebModel = new Model.Shop();







        protected void Page_Load(object sender, EventArgs e)
        {
            // LoadData();
            if (!IsPostBack)
            {
                this.ViewState["vs"] = 0;
                //ddlCatLoad();

                // content display
                //isContentDisplay();

                // get category search
                CatSearch = Request["cat"];

                //getAllProductInfo();

                //databind();

                //
                BindDataList();
            }

            lblContactMobile.Text = objWebModel.getContact();
            callToNumber.HRef = "callto:" + lblContactMobile.Text;
            if (lblContactMobile.Text == "")
            {
                lblContactMobile.Text = "No Number available";
                callToNumber.HRef = "javascript:void(0);";
            }

            LoadCategory();

            pos = (int) this.ViewState["vs"];

            //not found or paging check
            if (lvCatagory.Items.Count != 0)
            {
                lblMsg.Visible = false;
            }
        }





        // Get Category


        private void LoadCategory()
        {
            ds = objWebModel.getCategory();
            lvCatagory.DataSource = ds;
            lvCatagory.DataBind();

            //lvCatagory.Items.Insert(0,"All Category");
        }





        //Category Load
        //private void ddlCatLoad()
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["dbPOS"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SELECT catName FROM CategoryInfo"))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Connection = con;
        //            con.Open();
        //            lvCatagory.DataSource = cmd.ExecuteReader();
        //            lvCatagory.DataBind();
        //            con.Close();
        //        }
        //    }
        //    //ddlCatSearch.Items.Insert(0, new ListItem("--Select Category--", "0"));

        //}

        //DataBind Datalist
        //public void databind()
        //{
        //    query = "SELECT Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE (StockInfo.catName = '" + CatSearch + "' OR '" + CatSearch + "' = '') AND (prodTitle LIKE '%" + txtSearch.Text + "%' OR '" + txtSearch.Text + "' = '')  AND RoleInfo.domainName='" + Request.Url.Authority.ToString() + "'";
        //    //query = "SELECT Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE RoleInfo.domainName='" + Request.Url.Host.ToString() + "'";
        //    SqlCommand cmd = new SqlCommand(query);
        //    //cmd.Parameters.AddWithValue("@count", Convert.ToInt32(ddlSearch.SelectedValue));
        //    dadapter = new SqlDataAdapter();
        //    cmd.Connection = vcon;
        //    dadapter.SelectCommand = cmd;
        //    dset = new DataSet();
        //    adsource = new PagedDataSource();
        //    dadapter.Fill(dset);
        //    adsource.DataSource = dset.Tables[0].DefaultView;
        //    adsource.PageSize = 8;
        //    adsource.AllowPaging = true;
        //    adsource.CurrentPageIndex = pos;
        //    btnFirst.Enabled = !adsource.IsFirstPage;
        //    btnPrevious.Enabled = !adsource.IsFirstPage;
        //    btnNext.Enabled = !adsource.IsLastPage;
        //    btnLast.Enabled = !adsource.IsLastPage;
        //    lvProduct.DataSource = adsource;
        //    lvProduct.DataBind();

        //}

        // Get all product
        public void getAllProductInfo()
        {
            ds = objWebModel.getAllProduct();

            lvProduct.DataSource = ds;
            lvProduct.DataBind();

            //lvProduct.DataSource = ds.Tables[0];
            //ds = new DataSet();
            //adsource = new PagedDataSource();
            //adsource.PageSize = 8;
            //adsource.AllowPaging = true;
            //adsource.CurrentPageIndex = pos;
            //btnFirst.Enabled = !adsource.IsFirstPage;
            //btnPrevious.Enabled = !adsource.IsFirstPage;
            //btnNext.Enabled = !adsource.IsLastPage;
            //btnLast.Enabled = !adsource.IsLastPage;

            //lvProduct.DataSource = adsource;
            //lvProduct.DataBind();

            //btnFirst.Enabled = !adsource.IsFirstPage;
            //btnPrevious.Enabled = !adsource.IsFirstPage;
            //btnNext.Enabled = !adsource.IsLastPage;
            //btnLast.Enabled = !adsource.IsLastPage;
            //lvProduct.DataSource = adsource;
            //lvProduct.DataBind();
        }





        // Get Contact information 


        protected void btnFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            getAllProductInfo();
        }





        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            pos = (int) this.ViewState["vs"];
            pos -= 1;
            this.ViewState["vs"] = pos;
            getAllProductInfo();
        }





        protected void btnNext_Click(object sender, EventArgs e)
        {
            pos = (int) this.ViewState["vs"];
            pos += 1;
            this.ViewState["vs"] = pos;
            getAllProductInfo();
        }





        protected void btnLast_Click(object sender, EventArgs e)
        {
            pos = adsource.PageCount - 1;
            getAllProductInfo();
        }





        protected void btnViewDetailsImage_Click(object sender, ImageClickEventArgs e)
        {
        }





        protected void addToCart_Click(object sender, EventArgs e)
        {
            //i++;
            //Session["Product" + i] = ProductName.InnerText;
            //Session["Qty" + i] = txtQtyAddToCart.Text;
            //Session["counter"] = i.ToString();
            //Response.Redirect("~/Web/metaposbd/Product.aspx");
        }





        protected void Button1_Click(object sender, EventArgs e)
        {
        }





        // check content display
        //public void isContentDisplay()
        //{
        //    ds = objWebModel.getHeader();
        //    if (ds.Tables[0].Rows.Count <= 0)
        //    {
        //        phContentControl.Visible = false;
        //    }
        //}
        protected void btnViewDetailsImage_OnClick(object sender, ImageClickEventArgs e)
        {
            throw new NotImplementedException();
        }





        protected void lnkFirst_Click(object sender, EventArgs e)
        {
            //If user click First Link button assign current index as Zero "0" then refresh DataList "DlistEmp" Data.
            CurrentPage = 0;
            BindDataList();
        }





        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            //If user click Previous Link button assign current index as -1 it reduce existing page index.
            CurrentPage -= 1;
            //refresh DataList "DlistEmp" Data
            BindDataList();
        }





        protected void DataListPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("newpage"))
            {
                //Assign CurrentPage number when user click on the page number in the Paging DataList
                CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
                //Refresh DataList "DlistEmp" Data once user change page
                BindDataList();
            }
        }





        protected void lnkNext_Click(object sender, EventArgs e)
        {
            //If user click Next Link button assign current index as +1 it add one value to existing page index.
            CurrentPage += 1;

            //refresh DataList "DlistEmp" Data
            BindDataList();
        }





        protected void lnkLast_Click(object sender, EventArgs e)
        {
            //If user click Last Link button assign current index as totalpage then refresh DataList "DlistEmp" Data.
            CurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
            BindDataList();
        }





        //
        //DataTable GetData()
        //{
        //    DataTable dtable = new DataTable();

        //    //query = "SELECT Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE RoleInfo.domainName='" + url + "'";

        //    query = "SELECT Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE (StockInfo.catName = '" + CatSearch + "' OR '" + CatSearch + "' = '') AND (prodTitle LIKE '%" + txtSearch.Text + "%' OR '" + txtSearch.Text + "' = '')  AND RoleInfo.domainName='" + Request.Url.Authority.ToString() + "'";

        //    dtable = objSqlOperation.getDataTable(query);

        //    //Create column names for Datatable "dtable" 
        //    //dtable.Columns.Add("eno");
        //    //dtable.Columns.Add("empname");
        //    //dtable.Columns.Add("dept");
        //    //for (int i = 1; i <= 100; i++)
        //    //{
        //    //    //Create rows for DataTable "dtable" and assign values for each column
        //    //    dr = dtable.NewRow();
        //    //    dr["eno"] = i;
        //    //    dr["empname"] = "Empname " + i;
        //    //    dr["dept"] = "Dept. Name ";
        //    //    dtable.Rows.Add(dr);
        //    //}
        //    //Return DataTable "dtable" with its data
        //    return dtable;
        //}

        private void BindDataList()
        {
            //Create new DataTable dt
            DataTable dt = objWebModel.getEcommerce(CatSearch, txtSearch.Text);
            pgsource.DataSource = dt.DefaultView;

            //Set PageDataSource paging 
            pgsource.AllowPaging = true;

            //Set number of items to be displayed in the DataList using drop down list
            if (ddlSearch.SelectedIndex == -1 || ddlSearch.SelectedIndex == 0)
            {
                pgsource.PageSize = 6;
            }
            else
            {
                pgsource.PageSize = Convert.ToInt32(ddlSearch.SelectedItem.Value);
            }


            //Get Current Page Index
            pgsource.CurrentPageIndex = CurrentPage;

            //Store it Total pages value in View state
            ViewState["totpage"] = pgsource.PageCount;

            //Below line is used to show page number based on selection like "Page 1 of 20"
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + pgsource.PageCount;

            //Enabled true Link button previous when current page is not equal first page 
            //Enabled false Link button previous when current page is first page
            lnkPrevious.Enabled = !pgsource.IsFirstPage;

            //Enabled true Link button Next when current page is not equal last page 
            //Enabled false Link button Next when current page is last page
            lnkNext.Enabled = !pgsource.IsLastPage;

            //Enabled true Link button First when current page is not equal first page 
            //Enabled false Link button First when current page is first page
            lnkFirst.Enabled = !pgsource.IsFirstPage;

            //Enabled true Link button Last when current page is not equal last page 
            //Enabled false Link button Last when current page is last page
            lnkLast.Enabled = !pgsource.IsLastPage;

            //Bind resulted PageSource into the DataList
            lvProduct.DataSource = pgsource;
            lvProduct.DataBind();

            //Create Paging in the second DataList "DataListPaging"
            doPaging();
        }





        private void doPaging()
        {
            DataTable dt = new DataTable();
            //Add two column into the DataTable "dt" 
            //First Column store page index default it start from "0"
            //Second Column store page index default it start from "1"
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");

            //Assign First Index starts from which number in paging data list
            findex = CurrentPage - 5;

            //Set Last index value if current page less than 5 then last index added "5" values to the Current page else it set "10" for last page number
            if (CurrentPage > 5)
            {
                lindex = CurrentPage + 5;
            }
            else
            {
                lindex = 10;
            }

            //Check last page is greater than total page then reduced it to total no. of page is last index
            if (lindex > Convert.ToInt32(ViewState["totpage"]))
            {
                lindex = Convert.ToInt32(ViewState["totpage"]);
                findex = lindex - 10;
            }

            if (findex < 0)
            {
                findex = 0;
            }

            //Now creating page number based on above first and last page index
            for (int i = findex; i < lindex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            //Finally bind it page numbers in to the Paging DataList
            DataListPaging.DataSource = dt;
            DataListPaging.DataBind();
        }





        private int CurrentPage
        {
            get
            {
                //Check view state is null if null then return current index value as "0" else return specific page viewstate value
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int) ViewState["CurrentPage"]);
                }
            }
            set
            {
                //Set View statevalue when page is changed through Paging DataList
                ViewState["CurrentPage"] = value;
            }
        }





        protected void DataListPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //Enabled False for current selected Page index
            LinkButton lnkPage = (LinkButton) e.Item.FindControl("Pagingbtn");
            if (lnkPage.CommandArgument.ToString() == CurrentPage.ToString())
            {
                lnkPage.Enabled = false;
            }
        }





        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataList();
        }





        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindDataList();
        }


    }


}
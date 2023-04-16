using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.SaleBundle.View
{


    public partial class Token :BasePage
    {


        private SqlConnection vcon = new SqlConnection(ConfigurationManager.ConnectionStrings["localhost"].ToString());


        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();

        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();
        private string query = "";
        private decimal TotalDisc = 0.0M;
        //private DataSet ds;





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Token"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }

                txtSearchDateFrom.Text = txtSearchDateTo.Text = objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy");
            }
            searchResult();
        }





        private void refreshGrd(string query)
        {
            SqlDataSource dsToken = new SqlDataSource();
            dsToken.ID = "dsToken";
            this.Page.Controls.Add(dsToken);
            var constr = GlobalVariable.getConnectionStringName();
            dsToken.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsToken.SelectCommand = query;
            grdTokenInfo.DataSource = dsToken;
            grdTokenInfo.DataBind();
        }





        private void searchResult()
        {
            DateTime searchFrom, searchTo;
            try
            {
                searchFrom = Convert.ToDateTime(txtSearchDateFrom.Text);
            }
            catch
            {
                searchFrom = Convert.ToDateTime("01-jan-2000");
            }
            try
            {
                searchTo = Convert.ToDateTime(txtSearchDateTo.Text);
            }
            catch
            {
                searchTo = Convert.ToDateTime("01-jan-2090");
            }

            query =
                "SELECT [billNo], [cusID], [token], [discAmt], [entryDate] FROM [SaleInfo] WHERE (billNo LIKE IsNULL('%" +
                txtSearch.Text + "%',billNo) OR cusID LIKE IsNULL('%" + txtSearch.Text +
                "%',cusID)) AND (entryDate BETWEEN '" + searchFrom.ToShortDateString() + "' " + "AND DATEADD(d, 1, '" +
                searchTo.ToShortDateString() + "') AND token !='')  ORDER BY billNo DESC ";

            //query = "SELECT [billNo], [cusID], [token], [discAmt], [entryDate] FROM [SaleInfo] WHERE (billNo LIKE IsNULL('%" + txtSearch.Text + "%',billNo) OR cusID LIKE IsNULL('%" + txtSearch.Text + "%',cusID)) AND (entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  AND (entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '' ) AND token !=''  ORDER BY billNo DESC ";

            refreshGrd(query);
        }





        protected void grdTokenInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalDisc += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "discAmt"));
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = (Label) e.Row.FindControl("lblTotal");

                lblTotal.Text = TotalDisc.ToString();
            }
        }





        protected void grdTokenInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }





        protected void grdTokenInfo_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
        }





        protected void grdTokenInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }




        protected void ddlUserList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void grdTokenInfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTokenInfo.PageIndex = e.NewPageIndex;
            searchResult();
        }


    }


}
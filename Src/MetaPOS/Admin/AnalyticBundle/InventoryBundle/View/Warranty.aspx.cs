using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.InventoryBundle.View
{


    public partial class Warranty : System.Web.UI.Page
    {


        private string query = "", WarrantyDate = "", ExpiredDate = "", status = "";
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();
        SqlDataSource dsWarranty = new SqlDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Warranty"))
                {
                    commonFunction.pageout();
                }

                // DataSet 
                dsWarranty.ID = "dsWarranty";
                this.Page.Controls.Add(dsWarranty);
            }

            SearchResult();
        }





        private void refreshGrd(string query)
        {
            var constr = GlobalVariable.getConnectionStringName();
            dsWarranty.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsWarranty.SelectCommand = query;
            //grdWarranty.PageIndex = 0;
            grdWarranty.DataSource = dsWarranty;
            grdWarranty.DataBind();
        }





        private void SearchResult()
        {
            query =
                "SELECT tbl.billNo, sale.cusID, Cus.name, Cus.phone, Cus.mailInfo, tbl.prodID, stock.prodName, tbl.entryDate AS PurchaseDate, stock.warranty,sale.entryDate as SaleDate, sale.imei,sup.supID, sup.supCompany FROM stockStatusInfo AS tbl " +
                "LEFT OUTER JOIN saleInfo AS sale ON sale.billNo = tbl.billNO AND sale.prodID= tbl.prodID " +
                "LEFT OUTER JOIN slipinfo AS slip ON slip.billNo = tbl.billNO AND slip.prodID= tbl.prodID " +
                "LEFT OUTER JOIN CustomerInfo AS Cus ON Cus.cusID = sale.cusID  " +
                "LEFT OUTER JOIN StockInfo AS stock ON stock.prodID = tbl.prodID " +
                "LEFT OUTER JOIN SupplierInfo AS sup ON sup.supID = tbl.supCompany " +
                "WHERE (sale.warranty != '0') " +
                "AND (sale.warranty != '0-0-0') " +
                "AND (tbl.billNo LIKE '%" + txtSearch.Text + "%' " +
                "OR Cus.cusID LIKE '%" + txtSearch.Text +
                "%' OR Cus.name LIKE '%" + txtSearch.Text + "%' " +
                "OR Cus.name LIKE '%" + txtSearch.Text + "%' " +
                "OR cus.Phone LIKE '%" + txtSearch.Text + "%' " +
                "OR tbl.imei LIKE '%" + txtSearch.Text + "%' " +
                "OR stock.prodName LIKE '%" + txtSearch.Text + "%' " +
                "OR tbl.ProdId LIKE '%" + txtSearch.Text + "%') " +
                "AND tbl.status !='saleReturn' AND tbl.isSuspend='0' " +
                "AND stock.warranty != '' AND stock.warranty != '0-0-0' AND stock.warranty != '0' " + 
                commonFunction.getUserAccessParameters("tbl") + " ORDER BY tbl.billNo DESC ";
            refreshGrd(query);
        }





        protected void grdWarranty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string invoiceId = (grdWarranty.SelectedRow.FindControl("lblInvoice") as Label).Text;
            string CustomerId = (grdWarranty.SelectedRow.FindControl("lblCusID") as Label).Text;
            string prodID = (grdWarranty.SelectedRow.FindControl("prodID") as Label).Text;
            string prodName = (grdWarranty.SelectedRow.FindControl("prodName") as Label).Text;
            string supCompany = (grdWarranty.SelectedRow.FindControl("supCompany") as Label).Text;
            string imei = (grdWarranty.SelectedRow.FindControl("imei") as Label).Text;
            string supID = (grdWarranty.SelectedRow.FindControl("supID") as Label).Text;
            Response.Redirect("Servicing?cusid=" + CustomerId + "&prodID=" + prodID + "&prodName=" + prodName +
                              "&supCompany=" + supCompany + "&imei=" + imei + "&supID=" + supID);

            Response.Redirect("Service");
        }





        protected void grdWarranty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Change griview text
                TableCell PurchaseCell = e.Row.Cells[10];
                TableCell expiredCell = e.Row.Cells[13];
                TableCell statusCell = e.Row.Cells[14];


                //Warranty Data edit
                WarrantyDate = e.Row.Cells[11].Text;

                if (WarrantyDate == "0" || WarrantyDate == "")
                {
                    WarrantyDate = "0-0-0";
                }

                string[] wDates = WarrantyDate.Split('-');
                ArrayList wDateList = new ArrayList();
                foreach (string wDate in wDates)
                {
                    wDateList.Add(wDate);
                }

                //Expired Date 
                ExpiredDate = e.Row.Cells[13].Text;
                if (ExpiredDate == "0" || ExpiredDate == "")
                {
                    ExpiredDate = "0-0-0";
                }


                int year = Convert.ToInt32(wDateList[0]);
                int month = Convert.ToInt32(wDateList[1]);
                int day = Convert.ToInt32(wDateList[2]);

                DateTime saleDate = Convert.ToDateTime(e.Row.Cells[12].Text);


                DateTime expireDate = saleDate.AddYears(year).AddMonths(month).AddDays(day);

                //status Check
                string status = "";
                DateTime today = DateTime.Now;

                if (today.Date < expireDate.Date.AddDays(1))
                {
                    status = "Running";
                    statusCell.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    status = "Expired";
                    statusCell.ForeColor = System.Drawing.Color.Red;
                }


                //Change griview text
                if (PurchaseCell.Text != WarrantyDate)
                {
                    PurchaseCell.Text = wDateList[0] + "y " + wDateList[1] + "m " + wDateList[2] + "d ";
                    expiredCell.Text = expireDate.ToString("dd-MMM-yyyy");
                    statusCell.Text = status;
                }


                //lblTest.Text = PurshaseDate.ToShortDateString();
            }
        }




        protected void ddlStore_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SearchResult();
        }




        protected void grdWarranty_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdWarranty.PageIndex = e.NewPageIndex;
            SearchResult();
        }




        protected void btnPrintWarranty_OnClick(object sender, EventArgs e)
        {
            Session["pageName"] = "WarrantyReport";
            Session["reportQury"] = query;
            Response.Redirect("Print/LoadQuery.aspx");
        }


    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


/***
0	Opening Balance
0	Purchase Amount
0	Return
0	Supplier Payment
0	Supplier Purchase
1	Staff Salary
3	Receive
4	Bank Withdraw
5	Due Payment
5	Sales Payment
5	Sales Return
6	Cash Return
6	Discount
6	Invoice
6	Payment
6	Product Return
7	Service payment
8	Damage
 * */

namespace MetaPOS.Admin.ReportBundle.View
{
    public partial class ProfitLoss :BasePage //System.Web.UI.Page
    {
        private CommonFunction commonFunction = new CommonFunction();
        private decimal totalCashin = 0, totalCashout = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("ProfitLoss"))
                {
                    commonFunction.pageout();
                }

                txtSearchDateFrom.Text = txtSearchDateTo.Text = commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy");
            }


            if (commonFunction.findSettingItemValueDataTable("language") == "bn")
            {
                chkAllProfitLoss.Text = "সকল লাভ/লোকসান";
            }
            else
            {
                chkAllProfitLoss.Text = "All Profit/Loss";
            }

            
            searchResult();

            getRevenueAmount();
            getExpenseAoumt();
            getProfitAmount();

        }

        private void getProfitAmount()
        {
            var revenueAmt = lblRevenue.InnerText;
            var expenseAmt = lblExpense.InnerText;
            var supplierDueAmt = lblSupplierDue.InnerText;

            var profitLossAmt = (Convert.ToDecimal(revenueAmt) - (Convert.ToDecimal(expenseAmt) + Convert.ToDecimal(supplierDueAmt)));
            lblProfitLoss.InnerText = profitLossAmt.ToString();

            if (profitLossAmt < 0)
            {
                lblProfitLossTxt.InnerText = Resources.Language.Lbl_profitLoss_loss_amount;
            }
            else
            {
                lblProfitLossTxt.InnerText = Resources.Language.Lbl_profitLoss_profit_amount;
            }

        }

        private void getExpenseAoumt()
        {
            var profitLoss = new Service.ProfitLoss();
            var expenseAmt = profitLoss.getExpenseAmount();
            lblExpense.InnerText = expenseAmt;
        }

        private void getRevenueAmount()
        {
            var profitLoss = new Service.ProfitLoss();
            var revenueAmt = profitLoss.getRevenueAmount();
            lblRevenue.InnerText = revenueAmt;
        }




        private void getSupplierDueAmount(DateTime searchFrom, DateTime searchTo)
        {
            var profitLoss = new Service.ProfitLoss();
            var SupplierAmt = profitLoss.getSupplierDueAmt(searchFrom, searchTo);
            lblSupplierDue.InnerText = SupplierAmt;
        }


        private void refreshGrd(string query)
        {
            // get total 
            totalCashout = totalCashin = 0;
            var profitLossService = new Service.ProfitLoss();
            var dtProfitLoss = profitLossService.getProfitLossCalculation(query);

            for (int i = 0; i < dtProfitLoss.Rows.Count; i++)
            {
                totalCashout += Convert.ToDecimal(dtProfitLoss.Rows[i]["cashout"].ToString());
                totalCashin += Convert.ToDecimal(dtProfitLoss.Rows[i]["cashin"].ToString());
            }

            SqlDataSource dsProfitLoss = new SqlDataSource();
            dsProfitLoss.ID = "dsProfitLoss";
            this.Page.Controls.Add(dsProfitLoss);
            var constr = GlobalVariable.getConnectionStringName();
            dsProfitLoss.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsProfitLoss.SelectCommand = query;
            grdProfitLoss.DataSource = dsProfitLoss;
            grdProfitLoss.DataBind();
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

            if (chkAllProfitLoss.Checked)
            {
                searchFrom = Convert.ToDateTime("01-jan-2000");
                txtSearchDateFrom.Enabled = false;
                txtSearchDateTo.Enabled = false;
            }
            else
            {
                txtSearchDateFrom.Enabled = true;
                txtSearchDateTo.Enabled = true;
            }

            getSupplierDueAmount(searchFrom, searchTo);


            string query = "SELECT * FROM CashReportinfo WHERE entryDate >= '" + searchFrom + "' AND entryDate <= '" + searchTo + "' AND status !='6' " + Session["userAccessParameters"] + " ";

            refreshGrd(query);
        }





        protected void txtSearch_OnTextChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void grdProfitLoss_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblFooterCashin")).Text = totalCashin.ToString("0.00");
                ((Label)e.Row.FindControl("lblFooterCashout")).Text = totalCashout.ToString("0.00");
                var profitLossAmt = totalCashin - totalCashout;
                lblProfitLoss.InnerText = profitLossAmt.ToString("0.00");
            }

        }


    }
}
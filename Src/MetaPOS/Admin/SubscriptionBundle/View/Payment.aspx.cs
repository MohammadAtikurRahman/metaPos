using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPay;
using MetaPay.SSLCommerz;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.SaleBundle.View;


namespace MetaPOS.Admin
{
    public partial class Payment : BasePage
    {
        private CommonFunction objCommonFun = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Payment"))
                {
                    objCommonFun.pageout();
                }
            }

            if (Request["msg"] == "success")
            {
                ScriptMessage("Payment added successfully.", MessageType.Success);
            }
            else if (Request["msg"] == "warning")
            {
                ScriptMessage("payment not completed.", MessageType.Warning);
            }
            else if (Request["msg"] == "rejected")
            {
                ScriptMessage("Payment rejected.", MessageType.Warning);
            }
            else if (Request["msg"] == "zero")
            {
                ScriptMessage("payment can not be zero", MessageType.Warning);
            }



            search();
        }



        private void RefreshGrd(string queryGrid)
        {
            dsPayment.SelectCommand = queryGrid;
            grdPayment.PageIndex = 0;
            grdPayment.DataBind();
            ViewState["saveQuery"] = queryGrid;

        }


        public void search()
        {
            string query = "SELECT * FROM SubscriptionInfo as subscrip LEFT JOIN BranchInfo as branch ON branch.Id=subscrip.roleId " +
                           "WHERE subscrip.active='1' and subscrip.type='0' and (subscrip.status='" + ddlPaymentStatus.SelectedValue + "' OR '" + ddlPaymentStatus.SelectedValue + "'='') AND (subscrip.invoiceNo like '%" + txtSearch.Text + "%' OR '" + txtSearch.Text + "'='' ) ORDER BY subscrip.createDate DESC";
            RefreshGrd(query);
        }


        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }


        protected void grdPayment_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var changeStatus = "";
                var Status = ((Label)e.Row.FindControl("lblStatus")).Text;

                if (Status == "1")
                {
                    changeStatus = "Accepted";
                    ((Label)e.Row.FindControl("lblStatus")).ForeColor = Color.SeaGreen;
                }
                else if (Status == "2")
                {
                    changeStatus = "Rejected";
                    ((Label)e.Row.FindControl("lblStatus")).ForeColor = Color.Red;
                }
                else
                {
                    changeStatus = "Pending";
                    ((Label)e.Row.FindControl("lblStatus")).ForeColor = Color.RoyalBlue;
                }

                if (Status != "0")
                {
                    ((LinkButton)e.Row.FindControl("btnGrdView")).Visible = false;
                    ((Label)e.Row.FindControl("lblStatus")).Visible = true;
                    ((Label)e.Row.FindControl("lblStatus")).Text = changeStatus;
                }
            }
        }





        protected void grdPayment_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "payment")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdPayment.Rows[index];

                Label lblId = (Label)row.FindControl("lblId");
                lblPaymentId.Text = lblId.Text;


                Label getlblTransectionId = (Label)row.FindControl("lblTransectionId");
                lblTransectionID.Text = getlblTransectionId.Text;

                Label lblStatusOriginal = (Label)row.FindControl("lblStatus");
                ddlStatus.SelectedValue = lblStatusOriginal.Text;

                Label getlblBranchName = (Label)row.FindControl("lblBranchName");
                lblBranchName.Text = getlblBranchName.Text;

                Label getlblBranchId = (Label)row.FindControl("lblBranchId");
                lblBranchId.Text = getlblBranchId.Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                         "<script>$('#ConfirmPayment').modal('show');</script>", false);
            }
        }





        protected void btnConfirmPayment_OnClick(object sender, EventArgs e)
        {
            var paymentId = lblPaymentId.Text;

            var payment = txtPayment.Text == "" ? "0" : txtPayment.Text;
            var status = ddlStatus.SelectedValue;
            var branchId = lblBranchId.Text;
            var numberOfMonth = 1;
            var totalFee = "0";


            var serPayment = new SubscriptionBundle.Service.Payment();
            var msg = serPayment.confirmPayment(paymentId, status, Convert.ToInt32(numberOfMonth), totalFee, payment, branchId);


            if (msg)
            {
                Response.Redirect("~/admin/payment?msg=success");
            }
            else
            {
                Response.Redirect("~/admin/payment?msg=warning");
            }
        }





        protected void txtSearch_OnTextChanged(object sender, EventArgs e)
        {
            search();
        }



        
    }
}
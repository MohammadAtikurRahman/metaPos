using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.SaleBundle.Service;


namespace MetaPOS.Admin.SaleBundle.View
{
    public partial class Servicing :BasePage
    {

        private SqlOperation objSql = new SqlOperation();
        private CommonFunction objCommonFun = new CommonFunction();
        SqlDataSource dsServiceing = new SqlDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Servicing"))
                {
                    objCommonFun.pageout();
                }

             
                string message = Request["msg"];
                if (message == "success")
                {
                    scriptMessage("Operation successful.", MessageType.Success);
                }
                else if (message == "fail")
                {
                    scriptMessage("Sorry! Operation Failed.", MessageType.Warning);
                }
                else if (message == "overdue")
                {
                    scriptMessage("You can not pay mutch more than service due.", MessageType.Warning);
                }



                txtFrom.Text = txtTo.Text = txtDeliveryDate.Text = objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy");

                searchResult();
            }
        }

        private void refreshGrd(string query)
        {

            dsServiceing.ID = "dsServiceing";
            this.Page.Controls.Add(dsServiceing);
            var constr = GlobalVariable.getConnectionStringName();
            dsServiceing.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsServiceing.SelectCommand = query;
            grdServiceing.DataSource = dsServiceing;
            grdServiceing.DataBind();
        }


        public void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);

        }



        private void refreshDtl(string @serviceId)
        {
            string query = "SELECT * FROM [ServicingInfo] WHERE ([serviceId] = '" + @serviceId + "')";

            SqlDataSource dsServiceDetailsView = new SqlDataSource();
            dsServiceDetailsView.ID = "dsServiceDetailsView";
            this.Page.Controls.Add(dsServiceDetailsView);
            var constr = GlobalVariable.getConnectionStringName();
            dsServiceDetailsView.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsServiceDetailsView.SelectCommand = query;
            dltServiceView.DataSource = dsServiceDetailsView;
            dltServiceView.DataBind();
        }









        private void searchResult()
        {
            DateTime searchFrom, searchTo;
            try
            {
                searchFrom = Convert.ToDateTime(txtFrom.Text);
            }
            catch
            {
                searchFrom = Convert.ToDateTime("01-jan-2000");
            }
            try
            {
                searchTo = Convert.ToDateTime(txtTo.Text);
            }
            catch
            {
                searchTo = Convert.ToDateTime("01-jan-2090");
            }


            //query = "SELECT cat.Id, cat.cashType, cash.Id, cash.cashType, cash.descr, cash.cashIn, cash.cashout, cash.cashInHand, cash.entryDate, cash.billNo, cash.mainDescr, cash.roleID, cash.branchId, cash.groupId FROM CashCatInfo AS cat INNER JOIN CashReportInfo AS cash ON cat.cashType = cash.cashType WHERE (cash.cashType = '" + ddlCashSearchType.Text + "' OR '" + ddlCashSearchType.Text + "' = 'Search All') AND (cash.entryDate >= '" + searchFrom.ToShortDateString() + "' OR '" + txtSearchDateFrom.Text + "' = '')  AND (cash.entryDate <= '" + searchTo.ToShortDateString() + "' OR '" + txtSearchDateTo.Text + "' = '') AND (cash.roleId = '" + Session["roleId"] + "' OR cash.branchId = '" + Session["roleId"] + "' OR cash.groupId = '" + Session["roleId"] + "') ORDER BY cash.Id DESC";

            string query =
                "SELECT tbl.Id, tbl.serviceId,tbl.customerId,tbl.prodId,tbl.prodName,sale.imei,tbl.supplier,tbl.deliveryDate,tbl.description,tbl.paidAmt,tbl.totalAmt,tbl.entryDate,tbl.updateDate,tbl.roleId,tbl.active, cus.name as cusName FROM ServicingInfo as tbl " +
                "LEFT JOIN CustomerInfo as cus ON tbl.customerId= cus.cusID LEFT JOIN SaleInfo as sale ON sale.prodId= tbl.prodId " +
                "WHERE ([ServiceId] LIKE IsNull('%" + txtSearch.Text + "%', ServiceId) OR [customerId] LIKE IsNull('%" + txtSearch.Text + "%', customerId) OR sale.imei LIKE IsNull('%" + txtSearch.Text + "%', sale.imei))  AND (tbl.entryDate >= '" + searchFrom.ToShortDateString() +
                "' OR '" + searchFrom + "' = '0')  AND (tbl.entryDate <= '" + searchTo.ToShortDateString() +
                "' OR '" + searchTo + "' = '0') " + objCommonFun.getUserAccessParameters("tbl") +
                " ORDER BY tbl.Id DESC";

            refreshGrd(query);
        }








        [WebMethod]
        public static string saveServiceInfoDataAction(string serviceData)
        {
            SaleServicing saleService = new SaleServicing();
            return saleService.saveServiceInfoData(serviceData);
        }


        [WebMethod]
        public static string genarateNextServiceIDAction()
        {
            var saleService = new SaleServicing();
            return saleService.genreateNextServiceID();
        }




        protected void txtSearch_OnTextChanged(object sender, EventArgs e)
        {
            searchResult();
        }





        protected void grdService_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "ReceiptPrint")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdServiceing.Rows[index];

                Label serviceId = (Label)row.FindControl("serviceId");

                string query = "SELECT serviceId,customerId,prodId,prodName,imei,supplier,deliveryDate,description,paidAmt,totalAmt,entryDate,updateDate,roleId,active FROM ServicingInfo WHERE serviceId = '" + serviceId.Text + "'";

                Session["pageName"] = "serviceReport";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
            else if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdServiceing.Rows[index];

                Label lblAdvCusId = (Label)row.FindControl("serviceId");
                lblServceId.Text = lblAdvCusId.Text;
                refreshDtl(lblServceId.Text);


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#serviceDetailView').modal('show');</script>", false);
            }
            else if (e.CommandName == "StatusChange")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdServiceing.Rows[index];

                Label ServiceIdStatus = (Label)row.FindControl("serviceId");
                lblServiceIdStatus.Text = ServiceIdStatus.Text;


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#serviceStatusChage').modal('show');</script>", false);
            }
            else if (e.CommandName == "ServicingPay")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdServiceing.Rows[index];

                Label CusServicePay = (Label)row.FindControl("customerId");
                lblCusServicePay.Text = CusServicePay.Text;

                Label ServiceIdStatus = (Label)row.FindControl("serviceId");
                lblCusServiceId.Text = ServiceIdStatus.Text;

                Label TotalAmt = (Label)row.FindControl("lblTotalAmt");
                Label paidAmt = (Label)row.FindControl("lblPaidAmt");

                decimal cusDue = Convert.ToDecimal(TotalAmt.Text) - Convert.ToDecimal(paidAmt.Text);
                lblDueAmt.Text = cusDue.ToString();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                    "<script>$('#servicePayModal').modal('show');</script>", false);
            }
        }




        protected void ddlUserList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            searchResult();

        }



        protected void grdServiceing_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdServiceing.PageIndex = e.NewPageIndex;
            searchResult();
            
        }





        protected void btnStatusChage_OnClick(object sender, EventArgs e)
        {
            string query = "UPDATE ServicingInfo SET active = '" + ddlServiceStatus.SelectedValue +
                           "' where serviceId = '" + lblServiceIdStatus.Text + "'";
            objSql.executeQuery(query);

            Response.Redirect("~/admin/servicing?msg=success");
        }




        protected void btnServicePay_OnClick(object sender, EventArgs e)
        {
            CommonFunction commonFunction = new CommonFunction();

            if (txtServicePayAmt.Text == "")
                txtServicePayAmt.Text = "0";

            decimal paidAmt = Convert.ToDecimal(txtServicePayAmt.Text);

            if (paidAmt <= 0)
            {
                return;
            }

            if (Convert.ToDecimal(lblDueAmt.Text) < paidAmt)
            {
                Response.Redirect("Servicing?msg=overdue");
            }

            string cusID = lblCusServicePay.Text;
            commonFunction.cashTransactionSales(paidAmt, 0, "Servicing payment", cusID, cusID, lblCusServiceId.Text, "7", "0",
                commonFunction.GetCurrentTime().ToString());

            // custoer adjustment
            CustomerModel customerModel = new CustomerModel();
            ServicingModel serviceModel = new ServicingModel();

            // get customer due
            var dsCus = customerModel.getCustomerByCondition(" cusID='" + cusID + "'");
            decimal dbCusDue = Convert.ToDecimal(dsCus.Tables[0].Rows[0][10]);
            decimal openingDue = Convert.ToDecimal(dsCus.Tables[0].Rows[0][22]);

            decimal serviceDue = dbCusDue - paidAmt;
            if (serviceDue < 0)
            {
                serviceDue = 0;
            }

            customerModel.totalPaid = paidAmt;
            customerModel.totalDue = serviceDue;
            customerModel.openingDue = openingDue;
            customerModel.cusId = cusID;
            customerModel.updateCustomerInfoModel();

            // update serviceinfo
            serviceModel.paidAmt = paidAmt;
            serviceModel.serviceId = lblCusServiceId.Text;
            serviceModel.updateDate = objCommonFun.GetCurrentTime();
            serviceModel.updateServiceInfoDataModel();

            Response.Redirect("Servicing?msg=success");
        }


    }




    public enum MessageType
    {


        Success,
        Error,
        Info,
        Warning


    };
}
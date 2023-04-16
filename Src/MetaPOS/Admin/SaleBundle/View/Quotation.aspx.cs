using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace MetaPOS.Admin.SaleBundle.View
{


    public partial class Quotation :BasePage
    {


        private Controller.SaleController objSaleController = new Controller.SaleController();
        private Controller.CommonController objCommonController = new Controller.CommonController();
        private Controller.SlipController objSlipController = new Controller.SlipController();
        private Controller.StockStatusController objStockStatusController = new Controller.StockStatusController();
        private Controller.QuotationController objQuotationController = new Controller.QuotationController();
        private Controller.StockController objStockController = new Controller.StockController();

        private Model.QuotationModel objQuotationModelModel = new Model.QuotationModel();
        private Model.CustomerModel objCustomerModelModel = new Model.CustomerModel();

        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();

        public Dictionary<string, string> dicSale = new Dictionary<string, string>();

        public Dictionary<string, Dictionary<int, object>> dicQuotation =
            new Dictionary<string, Dictionary<int, object>>();


        public enum MessageType
        {


            Success,
            Error,
            Info,
            Warning


        };





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Quotation"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
            }
        }





        public void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        protected void gvQuotation_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "confirm")
            {
                //Global 
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvQuotation.Rows[index];

                Label lblSKU = (Label) row.FindControl("lblSKU");
                Label lblOrderID = (Label) row.FindControl("lblOrderID");

                // Branch Dropdownlist load
                //DropDownList ddlBranch = (DropDownList)row.FindControl("ddlBrachToQuotation");
                //objCommonFun.fillAllDdl(ddlBranch, "SELECT roleId,title FROM RoleInfo WHERE active='1'", "title", "roleId");

                // Set dictionary data
                dicSale.Add("orderId", lblOrderID.Text);
                dicSale.Add("sku", lblSKU.Text);

                objQuotationModelModel.dicData = dicSale;

                DataSet dsQuatation = objQuotationModelModel.getQuotationJoinCustomer();

                for (int i = 0; i < dsQuatation.Tables[0].Rows.Count; i++)
                {
                    dicQuotation.Add("sku" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][1].ToString()}
                    });
                    dicQuotation.Add("qty" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][2].ToString()}
                    });
                    dicQuotation.Add("cusId" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][3].ToString()}
                    });
                    dicQuotation.Add("orderId" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][4].ToString()}
                    });
                    dicQuotation.Add("Code" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][5].ToString()}
                    });
                    dicQuotation.Add("prodId" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][6].ToString()}
                    });
                    dicQuotation.Add("sPrice" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][7].ToString()}
                    });
                    dicQuotation.Add("prodName" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][8].ToString()}
                    });
                    dicQuotation.Add("supCompany" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][9].ToString()}
                    });
                    dicQuotation.Add("catName" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][10].ToString()}
                    });
                    dicQuotation.Add("bPrice" + i, new Dictionary<int, object>()
                    {
                        {i, dsQuatation.Tables[0].Rows[i][11].ToString()}
                    });
                    dicQuotation.Add("status" + i, new Dictionary<int, object>()
                    {
                        {i, "sale"}
                    });
                }

                //Count Passing
                objSaleController.count = dsQuatation.Tables[0].Rows.Count;
                objSlipController.count = dsQuatation.Tables[0].Rows.Count;
                objQuotationController.count = dsQuatation.Tables[0].Rows.Count;
                objStockController.count = dsQuatation.Tables[0].Rows.Count;
                objStockStatusController.count = dsQuatation.Tables[0].Rows.Count;


                // Insert data StockStatusInfo Table
                objStockStatusController.createListStockStatusInfo(dicQuotation);

                // Insert data into SlipInfo Table
                objSlipController.createSlipInfo(dicQuotation);

                // Insert data into SalesInfo Table
                objSaleController.createSaleInfo(dicQuotation);

                // Updata data into StockInfo Table
                //objSaleController.updateStockInfo(dicQuotation);

                // Insert Data into TempPrintSaleInfo 
                //objSaleController.createTempPrintSaleInfo(dicQuotation);


                // Insert data TempSaleInfo table
                //var msg = objSaleController.createTempSaleInfo(dicQuotation);
                //lblTest.Text = msg.ToString();
                //return;


                // Customer data into CustomerInfo Table
                Label lblCusId = (Label) row.FindControl("lblCusId");

                // Set customer id 
                Dictionary<string, string> dicGetCustomer = new Dictionary<string, string>();
                dicGetCustomer.Add("cusID", lblCusId.Text);

                var getConditinalParameter = objCommonController.getConditinalParameter(dicGetCustomer);
                DataSet dsCustomer = objCustomerModelModel.getCustomerByCondition(getConditinalParameter);

                // Get Due / gift Amount
                decimal giftAmt = objQuotationController.GiftAmt(dicQuotation);

                // Set customer updated data
                Dictionary<string, string> dicCustomerUpdateData = new Dictionary<string, string>();
                dicCustomerUpdateData.Add("roleID", HttpContext.Current.Session["roleId"].ToString());
                dicCustomerUpdateData.Add("branchId", HttpContext.Current.Session["branchId"].ToString());
                dicCustomerUpdateData.Add("groupId", HttpContext.Current.Session["groupId"].ToString());
                dicCustomerUpdateData.Add("totalDue", giftAmt.ToString("0.00"));

                // Set customer conditional data
                Dictionary<string, string> dicCustomerConditionalData = new Dictionary<string, string>();
                dicCustomerConditionalData.Add("cusId", lblCusId.Text);

                var getFormatUpdateItemData = objCommonController.getUpdateParameter(dicCustomerUpdateData);
                var getFormatedConditinalParameter =
                    objCommonController.getConditinalParameter(dicCustomerConditionalData);

                objCustomerModelModel.cusId = lblCusId.Text;
                objCustomerModelModel.updateCustomer(getFormatUpdateItemData, getFormatedConditinalParameter);

                // Update  quotation info Table
                objQuotationController.updateQuotation(dicQuotation, "1");

                scriptMessage("An order confirmed successfully.", MessageType.Success);

                gvQuotation.DataBind();
            }
            else if (e.CommandName == "cancel")
            {
                //Global 
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvQuotation.Rows[index];

                Label lblSKU = (Label) row.FindControl("lblSKU");
                Label lblOrderID = (Label) row.FindControl("lblOrderID");

                // Set dictionary data
                dicSale.Add("orderId", lblOrderID.Text);
                dicSale.Add("sku", lblSKU.Text);

                objQuotationModelModel.dicData = dicSale;

                DataSet dsQuatation = objQuotationModelModel.getQuotationJoinCustomer();

                for (int j = 0; j < dsQuatation.Tables[0].Rows.Count; j++)
                {
                    dicQuotation.Add("sku" + j, new Dictionary<int, object>()
                    {
                        {j, dsQuatation.Tables[0].Rows[j][1].ToString()}
                    });
                    dicQuotation.Add("qty" + j, new Dictionary<int, object>()
                    {
                        {j, dsQuatation.Tables[0].Rows[j][2].ToString()}
                    });
                    dicQuotation.Add("cusId" + j, new Dictionary<int, object>()
                    {
                        {j, dsQuatation.Tables[0].Rows[j][3].ToString()}
                    });
                    dicQuotation.Add("orderId" + j, new Dictionary<int, object>()
                    {
                        {j, dsQuatation.Tables[0].Rows[j][4].ToString()}
                    });
                }

                //Count Passing
                objSaleController.count = dsQuatation.Tables[0].Rows.Count;
                objSlipController.count = dsQuatation.Tables[0].Rows.Count;
                objQuotationController.count = dsQuatation.Tables[0].Rows.Count;
                objStockController.count = dsQuatation.Tables[0].Rows.Count;

                // Update  quotation (cancel) info Table
                objQuotationController.updateQuotation(dicQuotation, "2");

                // Stock update
                objStockController.updateListStockInfo(dicQuotation);

                scriptMessage("An Order Canceled.", MessageType.Warning);

                gvQuotation.DataBind();
            }
        }





        // Cancel product with branchId
        private void CancelQuotationProduct()
        {
        }





        protected void ddlBrachToQuotation_DataBinding(object sender, EventArgs e)
        {
            var ddl = sender as DropDownList;
            if (ddl != null)
            {
            }
        }





        protected void gvQuotation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["userRight"].ToString() == "Branch")
                {
                    var ddlBranch = (DropDownList) e.Row.FindControl("ddlBrachToQuotation");
                    objCommonFun.fillAllDdl(ddlBranch,
                        "SELECT roleId,title FROM RoleInfo WHERE active='1'" + HttpContext.Current.Session["userAccessParameters"] +
                        " AND branchId = '0'", "title", "roleId");
                }
                else if (Session["userRight"].ToString() == "Group")
                {
                    var ddlBranch = (DropDownList) e.Row.FindControl("ddlBrachToQuotation");
                    objCommonFun.fillAllDdl(ddlBranch,
                        "SELECT roleId,title FROM RoleInfo WHERE active='1'" + HttpContext.Current.Session["userAccessParameters"] +
                        " AND branchId = '0' AND groupId !='0'", "title", "roleId");
                }
            }
        }


    }


}
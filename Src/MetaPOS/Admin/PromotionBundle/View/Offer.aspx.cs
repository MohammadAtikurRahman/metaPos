using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.PromotionBundle.Service;


namespace MetaPOS.Admin.PromotionBundle.View
{


    public partial class Offer :BasePage //System.Web.UI.Page
    {


        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();
        private DataSet ds;
        private string query = "";


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
                if (!objCommonFun.accessChecker("Offer"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }

                txtOfferStart.Text = txtOfferEnd.Text = objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy");


                if (Request["msg"] == "success")
                {
                    ScriptMessage("Offer added successfully.", MessageType.Success);
                }
                else if (Request["msg"] == "warning")
                {
                    ScriptMessage("Offer is not added.", MessageType.Warning);
                }

                
            }


            searchResult();
           
        }





        private void refreshGrd(string query)
        {
            SqlDataSource dsOffer = new SqlDataSource();
            dsOffer.ID = "dsOffer";
            this.Page.Controls.Add(dsOffer);
            var constr = GlobalVariable.getConnectionStringName();
            dsOffer.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsOffer.SelectCommand = query;
            grdOffer.DataSource = dsOffer;
            grdOffer.DataBind();
        }





        private void searchResult()
        {
            if (Session["userRight"].ToString() != "Regular")
            {
                Session["queryOfLoadingCustomData"] = "(offer.roleId = '" + Session["roleId"] + "' OR " + "offer.branchId = '" +
                                                      Session["roleId"] + "' OR " + "offer.groupId = '" + Session["roleId"] +
                                                      "') ";
            }
            else
            {
                Session["queryOfLoadingCustomData"] = "(offer.roleId = '" + Session["branchId"] + "' OR offer.branchId = '" +
                                                      Session["branchId"] + "') ";
            }

            query =
                "SELECT *,CONCAT(stock.prodName,pack.packageName) as prodPack FROM OfferInfo as offer LEFT JOIN StockInfo stock ON offer.prodId = stock.prodId AND offer.type='0' LEFT JOIN PackageInfo pack ON offer.prodId=pack.Id AND offer.type='1' WHERE (stock.prodName LIKE IsNull('%" +
                txtSearch.Text + "%',stock.prodName) OR offer.prodId LIKE IsNull('%" + txtSearch.Text + "%',offer.prodId) OR pack.packageName LIKE IsNull('%" + txtSearch.Text + "%',pack.packageName)) AND CAST(offerEnd as date) >= '" + objCommonFun.GetCurrentTime().ToShortDateString() + "' AND " + Session["queryOfLoadingCustomData"] + " ORDER BY offer.ID DESC";

            refreshGrd(query);
        }





        protected void btnLoadProductDetails_Click(object sender, ImageClickEventArgs e)
        {
            var dsStock = objCommonFun.LoadProductDetails(txtSearchNameCode.Text);

            if (dsStock.Tables.Count > 0 && dsStock.Tables[0].Rows.Count > 0)
            {
                txtProductName.Text = dsStock.Tables[0].Rows[0][2].ToString();
                txtProductID.Text = dsStock.Tables[0].Rows[0][0].ToString();


                var dtOfferStock =
                    objSql.getDataTable("SELECT offerStart, offerEnd, offerType, offerValue, discountType, discountValue FROM [OfferInfo] WHERE prodID = '" + txtProductID.Text + "' AND type='" + rblSearchIn.SelectedValue + "' AND CAST(offerEnd as date) >= '" + objCommonFun.GetCurrentTime().ToShortDateString() + "'");

                if (dtOfferStock.Rows.Count > 0)
                {
                    int offerType = Convert.ToInt16(dtOfferStock.Rows[0][2].ToString());
                    int discountType = Convert.ToInt16(dtOfferStock.Rows[0][4].ToString());
                    string input;

                    txtOfferStart.Text = Convert.ToDateTime(dtOfferStock.Rows[0][0]).ToString("dd-MMM-yyyy");
                    txtOfferEnd.Text = Convert.ToDateTime(dtOfferStock.Rows[0][1]).ToString("dd-MMM-yyyy");
                    ddlOfferOn.SelectedValue = offerType.ToString();
                    ddlDiscountOn.SelectedValue = discountType.ToString();

                    input = dtOfferStock.Rows[0][3].ToString();
                    if (offerType == 0)
                        txtOfferValue.Text = input.Substring(0, input.Length - 3);
                    else
                        txtOfferValue.Text = input;

                    input = dtOfferStock.Rows[0][5].ToString();
                    //if (discountType == 0)
                    //    txtDiscountValue.Text = input.Substring(0, input.Length - 3);
                    //else
                    //    txtDiscountValue.Text = input;
                    txtDiscountValue.Text = input;
                }
                else
                {
                    ddlOfferOn.SelectedIndex = 0;
                    txtOfferValue.Text = "";
                    ddlDiscountOn.SelectedIndex = 0;
                    txtDiscountValue.Text = "";
                    txtOfferStart.Text = txtOfferEnd.Text = objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy");
                }
            }
        }





        protected void ddlOfferOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOfferOn.Text = ddlOfferOn.SelectedValue == "1" ? "Enter Amount" : "Enter Qty";
        }





        protected void ddlDiscountOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDiscountOn.SelectedValue == "1")
                lblDiscountOn.Text = "Enter Amount";
            else if (ddlDiscountOn.SelectedValue == "2")
                lblDiscountOn.Text = "Enter Point";
            else
                lblDiscountOn.Text = "Enter Qty";

        }





        protected void btnSetOffer_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Length == 0 || txtProductID.Text.Length == 0)
            {
                ScriptMessage("Search product to set offer!", MessageType.Warning);
                return;
            }

            try
            {
                var offerValue = txtOfferValue.Text == "" ? "0" : txtOfferValue.Text;
                var offerDiscount = txtDiscountValue.Text == "" ? "0" : txtDiscountValue.Text;

                if (Convert.ToDecimal(offerValue) <= 0)
                {
                     ScriptMessage("Offer value is required", MessageType.Warning);
                    return;
                }

                if (Convert.ToDecimal(offerDiscount) <= 0)
                {
                    ScriptMessage("Discount value is required", MessageType.Warning);
                    return;
                }

            }
            catch
            {
                ScriptMessage("Fields required Number!", MessageType.Warning);
                return;
            }

            if (txtOfferValue.Text.Contains(".") && ddlOfferOn.SelectedValue != "1")
            {
                ScriptMessage("Offer is not supported point number", MessageType.Warning);
                return;
            }

            // check ratio 
            if (txtDiscountValue.Text.Contains("."))
            {
                query = "SELECT unitRatio FROM UnitInfo as unit LEFT JOIN StockInfo as stock ON stock.unitId=unit.Id WHERE stock.prodId='" +
                    txtProductID.Text + "'";
                var dtStore = objSql.getDataTable(query);
                var ratio = 1;
                if (dtStore.Rows.Count > 0)
                {
                    ratio = Convert.ToInt32(dtStore.Rows[0]["unitRatio"].ToString());

                }

                var piece = txtDiscountValue.Text.Split('.')[1];
                if (Convert.ToInt32(piece) >= ratio)
                {
                    ScriptMessage("invalid! can not input then ratio", MessageType.Warning);
                    return;
                }

            }

            ds = objSql.getDataSet("SELECT prodCode FROM [OfferInfo] WHERE prodId = '" + txtProductID.Text + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    objSql.executeQuery("UPDATE [OfferInfo] SET offerStart = '" + txtOfferStart.Text +
                                        "', offerEnd = '" + txtOfferEnd.Text +
                                        "', offerType = '" + ddlOfferOn.SelectedValue +
                                        "', offerValue = '" + txtOfferValue.Text +
                                        "', discountType = '" + ddlDiscountOn.SelectedValue +
                                        "', discountValue = '" + txtDiscountValue.Text +
                                        "', active = '1' WHERE prodId = '" + txtProductID.Text + "'");

                    reset();

                    Response.Redirect("~/admin/offer?msg=success");
                }
                catch (Exception)
                {
                    Response.Redirect("~/admin/offer?msg=success");
                }
            }
            else
            {
                try
                {
                    objSql.executeQuery(
                        "INSERT INTO [OfferInfo] (prodCode, prodId, offerStart, offerEnd, offerType, offerValue, discountType, discountValue, entryDate, roleId,                        branchId,                 groupId, type) VALUES ('" +
                        txtProductID.Text + "','" + txtProductID.Text + "', '" + txtOfferStart.Text + "', '" + txtOfferEnd.Text + "', '" +
                        ddlOfferOn.SelectedValue + "', '" + txtOfferValue.Text + "', '" + ddlDiscountOn.SelectedValue +
                        "', '" + txtDiscountValue.Text + "', '" + objCommonFun.GetCurrentTime().ToShortDateString() +
                        "', '" + Session["roleId"] + "', '" + Session["branchId"] + "', '" + Session["groupId"] + "','" + rblSearchIn.SelectedValue + "')");

                    //ScriptMessage("Operation Successful.", MessageType.Success);
                }
                catch (Exception)
                {
                    //ScriptMessage(ex.ToString(), MessageType.Warning);
                }
            }


        }





        protected void grdOffer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var setColorClass = string.Empty;

                TableCell offerType = e.Row.Cells[6],
                    offerValue = e.Row.Cells[7],
                    discountType = e.Row.Cells[8],
                    discountVlue = e.Row.Cells[9],
                    activate = e.Row.Cells[10];

                if (offerType.Text == "0")
                {
                    offerType.Text = "Qty";
                    offerValue.Text = offerValue.Text.Substring(0, offerValue.Text.Length - 3);
                }
                else
                {
                    offerType.Text = "Amount";
                }

                if (discountType.Text == "1")
                {
                    discountType.Text = "Amount";
                    if (discountVlue.Text.Length > 3)
                        discountVlue.Text = discountVlue.Text.Substring(0, discountVlue.Text.Length - 3);
                }
                else if (discountType.Text == "2")
                {
                    discountType.Text = "Point";
                }
                else
                {
                    discountType.Text = "Qty";
                }

                activate.Text = activate.Text == "True" ? "Yes" : "No";
            }
        }





        protected void grdOffer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var active = grdOffer.SelectedRow.Cells[10].Text;

            if (active == "Yes")
            {
                objSql.executeQuery("UPDATE [OfferInfo] SET active = '0' WHERE prodCode = '"
                                    + (grdOffer.SelectedRow.Cells[3].Text) + "' ");
                ScriptMessage("Successfully Deactivated.", MessageType.Success);
            }
            else
            {
                objSql.executeQuery("UPDATE [OfferInfo] SET active = '1' WHERE prodCode = '"
                                    + (grdOffer.SelectedRow.Cells[3].Text) + "' ");
                ScriptMessage("Successfully Activated.", MessageType.Success);
            }
        }





        protected void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }





        private void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void reset()
        {
            txtProductName.Text = "";
            txtProductID.Text = "";
            ddlOfferOn.SelectedIndex = 0;
            txtOfferValue.Text = "";
            ddlDiscountOn.SelectedIndex = 0;
            txtDiscountValue.Text = "";
            txtOfferStart.Text = txtOfferEnd.Text = objCommonFun.GetCurrentTime().ToString("dd-MMM-yyyy");
        }





        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (grdOffer.Rows.Count <= 0)
            {
                ScriptMessage("There are no data records to Print", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "OfferList";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }






        [WebMethod]
        public static string getValidOfferAction(string jsonData)
        {
            var offerService = new OfferService();
            return offerService.getValidOffer(jsonData);
        }


    }


}
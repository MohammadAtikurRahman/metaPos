using System;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;


namespace MetaPOS.Admin.ShopBundle.View
{


    public partial class Ecommerce : BasePage
    {


        private Admin.DataAccess.SqlOperation objSql = new Admin.DataAccess.SqlOperation();
        private Admin.DataAccess.CommonFunction objCommonFun = new Admin.DataAccess.CommonFunction();
        private Admin.Model.eCommerceModel objEcommerce = new Model.eCommerceModel();
        private DataSet ds, dseCom;
        private string query = "";
        private int featured = 0, countFeatured = 0;
        private bool checkFeatured;


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
                if (!objCommonFun.accessChecker("Ecommerce"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
            }

            searchResult();
        }





        private void refreshGrd(string query)
        {
            dsEcommerce.SelectCommand = query;
            grdEcommerce.PageIndex = 0;
            grdEcommerce.DataBind();
        }





        private void searchResult()
        {
            if (Session["userRight"].ToString() != "Regular")
            {
                Session["queryOfLoadingCustomData"] = "(ecom.roleId = '" + Session["roleId"] + "' OR " +
                                                      "ecom.branchId = '" + Session["roleId"] + "' OR " +
                                                      "ecom.groupId = '" + Session["roleId"] + "') ";
            }
            else
            {
                Session["queryOfLoadingCustomData"] = "(ecom.roleId = '" + Session["branchId"] +
                                                      "' OR ecom.branchId = '" + Session["branchId"] + "') ";
            }

            query =
                "SELECT * FROM Ecommerce as ecom LEFT JOIN StockInfo as si ON ecom.prodCode = si.prodCode WHERE (ecom.prodTitle LIKE IsNull('%" +
                txtSearch.Text + "%', ecom.prodTitle) OR ecom.prodCode LIKE IsNull('%" + txtSearch.Text +
                "%', ecom.prodCode) OR si.prodName LIKE IsNull('%" + txtSearch.Text + "%', si.prodName))  AND " +
                Session["queryOfLoadingCustomData"] + " ORDER BY ecom.ID DESC";

            refreshGrd(query);
        }





        protected void btnLoadProductDetails_Click(object sender, ImageClickEventArgs e)
        {
            ds = objCommonFun.LoadProductDetails(txtSearchNameCode.Text);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtProductName.Text = ds.Tables[0].Rows[0][3].ToString();
                txtProductCode.Text = ds.Tables[0].Rows[0][2].ToString();

                ds.Clear();

                ds =
                    objSql.getDataSet(
                        "SELECT prodTitle, shortDescr, longDescr, image, isFeatured FROM [Ecommerce] WHERE prodCode = '" +
                        txtProductCode.Text + "'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTitle.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtFeatures.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtDescription.Text = ds.Tables[0].Rows[0][2].ToString();
                    lblFileName.Text = ds.Tables[0].Rows[0][3].ToString();
                    checkFeatured = Convert.ToBoolean(ds.Tables[0].Rows[0][4]);
                    if (checkFeatured)
                        chkFeatured.Checked = true;
                }
                else
                {
                    txtTitle.Text = "";
                    txtFeatures.Text = "";
                    txtDescription.Text = "";
                    lblFileName.Text = "";
                }
            }
        }





        protected void btnSet_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Length == 0)
            {
                scriptMessage("Product title is required!", MessageType.Warning);
                return;
            }

            if (txtProductName.Text.Length == 0 || txtProductCode.Text.Length == 0)
            {
                scriptMessage("Search product to set offer!", MessageType.Warning);
                return;
            }

            // check featured product 
            if (chkFeatured.Checked)
            {
                objEcommerce.EprodCode = txtProductCode.Text;
                objEcommerce.isFeatured = true;
                countFeatured = objEcommerce.countFeatured();
                featured = objEcommerce.getDisplayNumber();
                if (featured < countFeatured)
                {
                    scriptMessage("Featured Product List Full.. please update", MessageType.Warning);
                    return;
                }
            }

            string folderPath = Server.MapPath("~/Img/Product/");
            string fileName = lblFileName.Text;

            if (fuImage.HasFile)
            {
                if ((File.Exists(folderPath + fileName)))
                    File.Delete(folderPath + fileName);

                fileName = txtProductCode.Text + Path.GetExtension(fuImage.PostedFile.FileName);
                fuImage.SaveAs(folderPath + Path.GetFileName(fileName));
            }

            dseCom = objSql.getDataSet("SELECT prodCode FROM [Ecommerce] WHERE prodCode = '" + txtProductCode.Text + "'");

            //
            if (chkFeatured.Checked)
                checkFeatured = true;
            else
                checkFeatured = false;

            if (dseCom.Tables[0].Rows.Count > 0)
            {
                try
                {
                    objSql.executeQuery("UPDATE [Ecommerce] SET prodTitle = N'" + txtTitle.Text +
                                        "', shortDescr = N'" + txtFeatures.Text +
                                        "', longDescr = N'" + txtDescription.Text +
                                        "', image = '" + fileName +
                                        "', isFeatured = '" + checkFeatured +
                                        "', active = '1' WHERE prodCode = '" + txtProductCode.Text + "'");
                    scriptMessage("Operation Successful.", MessageType.Success);
                    reset();
                }
                catch (Exception ex)
                {
                    scriptMessage(ex.ToString(), MessageType.Warning);
                }
            }
            else
            {
                try
                {
                    objSql.executeQuery(
                        "INSERT INTO [Ecommerce] (prodCode,                         prodTitle,              shortDescr,                     longDescr,                  image,      entryDate, roleId,                        branchId,                 groupId,       isFeatured ) VALUES ('" +
                        txtProductCode.Text + "', N'" + txtTitle.Text + "', N'" + txtFeatures.Text + "', N'" +
                        txtDescription.Text + "', '" + fileName +
                        "', '" + objCommonFun.GetCurrentTime().ToShortDateString() + "', '" + Session["roleId"] + "', '" +
                        Session["branchId"] + "', '" + Session["groupId"] + "', '" + checkFeatured + "')");

                    scriptMessage("Operation Successful.", MessageType.Success);
                    grdEcommerce.DataBind();
                    reset();
                }
                catch (Exception ex)
                {
                    scriptMessage(ex.ToString(), MessageType.Warning);
                }
            }
        }





        protected void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }





        private void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        private void reset()
        {
            txtProductName.Text = "";
            txtProductCode.Text = "";
            txtTitle.Text = "";
            txtFeatures.Text = "";
            txtDescription.Text = "";
            chkFeatured.Checked = false;
        }





        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (grdEcommerce.Rows.Count <= 0)
            {
                scriptMessage("There are no data records to Print", MessageType.Warning);
            }
            else
            {
                Session["pageName"] = "EcommerceList";
                Session["reportQury"] = query;
                Response.Redirect("Print/LoadQuery.aspx");
            }
        }





        protected void grdEcommerce_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            scriptMessage("Operation Successfully", MessageType.Success);
        }


    }


}
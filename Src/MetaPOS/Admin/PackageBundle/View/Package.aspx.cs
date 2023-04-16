using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.SaleBundle.Service;
using Resources;


namespace MetaPOS.Admin.PackageBundle.View
{


    public partial class Package : BasePage
    {


        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();
        private DataSet ds;//, dsTempSaleInfo, dsTempSalePriceTrack, dsUpdateSaleInfo;

        private static Label[] lblSL = new Label[100];
        private static Label[] lblProdName = new Label[100];
        private static Label[] lblProdCode = new Label[100];
        private static Label[] lblSPrice = new Label[100];

        private static TextBox[] txtQty = new TextBox[100];

        private static Button[] btnCancel = new Button[100];

        //ProdCode Array
        private static ArrayList ProdCodeArr = new ArrayList();

        private static int counter = 0;

        private string query = "", qtyPack = "0";
        private static bool barcodeScan;


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
                if (!objCommonFun.accessChecker("Package"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }


                clearProductArr();
                btnUpdate.Visible = false;
                //btnCancel.Visible = false;

                DataVisibilityControl();
            }
            //load gridview
            SearchResult();

        }





        private void clearProductArr()
        {
            // initilize
            counter = 0;

            // Clear array
            Array.Clear(lblSL, 0, lblSL.Length);
            Array.Clear(lblProdName, 0, lblProdName.Length);
            Array.Clear(lblProdCode, 0, lblProdCode.Length);
            Array.Clear(lblSPrice, 0, lblSPrice.Length);
            Array.Clear(btnCancel, 0, btnCancel.Length);
            ProdCodeArr.Clear();

            // clear div
            divProdPakageList.Controls.Clear();
        }





        private void refreshGrd(string query)
        {
            SqlDataSource dsPackage = new SqlDataSource();
            dsPackage.ID = "dsPackage";
            this.Page.Controls.Add(dsPackage);
            var constr = GlobalVariable.getConnectionStringName();
            dsPackage.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsPackage.SelectCommand = query;
            grdPackage.DataSource = dsPackage;
            grdPackage.DataBind();
        }





        public void SearchResult()
        {
            query = "SELECT [Id], [packageName], [prodCode], [price] FROM PackageInfo WHERE storeId='" + Session["storeId"] + "' AND (packageName LIKE IsNull('%" +
                txtSearch.Text + "%',packageName) OR Id LIKE IsNull('%" + txtSearch.Text + "%',Id)) AND active = '" +
                ddlActiveStatus.SelectedValue + "' " + Session["userAccessParameters"] + " ORDER BY Id DESC";
            //lblTest.Text = query;
            //query = "SELECT pack.Id, pack.packageName, pack.prodCode, pack.price, min(stock.qty) FROM PackageInfo AS pack LEFT JOIN StockInfo AS stock ON pack.prodCode = stock.prodCode  WHERE (pack.packageName LIKE IsNull('%" + txtSearch.Text + "%',pack.packageName) OR pack.Id LIKE IsNull('%" + txtSearch.Text + "%',pack.Id)) AND pack.active = '" + ddlActiveStatus.SelectedValue + "' ";
            refreshGrd(query);
        }





        private bool isExistsPackage()
        {
            ds = objSql.getDataSet("SELECT * FROM PackageInfo WHERE packageName = '" + txtPackageName.Text + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }





        protected void txtBarcodeScaner_TextChanged(object sender, EventArgs e)
        {
            barcodeScan = true;
        }





        protected void btnLoadProductDetails_Click(object sender, ImageClickEventArgs e)
        {
            ds = objCommonFun.LoadProductDetails(txtSearchNameCode.Text);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtBarcodeScaner.Text = ds.Tables[0].Rows[0][1].ToString();
                barcodeScan = false;
                btnPAdd_Click(null, null);
            }
            else
            {
                txtBarcodeScaner.Text = "";
                scriptMessage("Product is not found!", MessageType.Warning);
                barcodeScan = false;

                ShowShoppingList();
            }
        }





        protected void btnPAdd_Click(object sender, EventArgs e)
        {
            if (txtBarcodeScaner.Text == "")
            {
                scriptMessage("Stock Unavailable to Search!", MessageType.Warning);
                return;
            }


            //txtBarcodeScaner.Text = txtBarcodeScaner.Text.Substring(txtBarcodeScaner.Text.Length - 12, 12);
            if (barcodeScan)
            {
                txtBarcodeScaner.Text = txtBarcodeScaner.Text.Remove(0, 1);
                barcodeScan = false;
            }

            if (ProdCodeArr.Contains(txtBarcodeScaner.Text))
            {
                scriptMessage("Item already added.", MessageType.Warning);
                ShowShoppingList();
                return;
            }

            addNewItem(txtBarcodeScaner.Text, "0");
            txtSearchNameCode.Text = "";
            txtBarcodeScaner.Text = "";
        }





        protected bool checkingProduct()
        {
            string[] splitText = new string[] { };
            string code = txtSearchNameCode.Text;
            string finalText = code;

            if (code.Contains('['))
            {
                splitText = code.Split(new string[] { " [ " }, StringSplitOptions.None);
                finalText = splitText[1].Substring(0, splitText[1].Length - 1);
            }


            foreach (string product in ProdCodeArr)
            {
                if (finalText == product)
                {
                    scriptMessage("Product is already added.", MessageType.Warning);
                    return true;
                }
            }

            // 

            return false;
        }





        protected void addNewItem(string barcode, string isUpdate)
        {
            string query = "SELECT * FROM [StockInfo] WHERE prodCode = '" + barcode + "'";
            if (isUpdate == "1")
            {
                query = "SELECT * FROM [StockInfo] WHERE prodId = '" + barcode + "'";
            }

            ds = objSql.getDataSet(query);

            if (ds.Tables[0].Rows.Count == 0)
            {
                scriptMessage("Stock unavailable!", MessageType.Warning);
                return;
            }

            lblSL[counter] = new Label { Text = (counter + 1).ToString() };

            lblProdName[counter] = new Label { Text = ds.Tables[0].Rows[0][3].ToString() };

            //txtQty[counter] = new TextBox { Text = "1", AutoPostBack = true };
            //txtQty[counter].TextChanged += new EventHandler(txtChangeQty);

            lblSPrice[counter] = new Label { Text = ds.Tables[0].Rows[0][9].ToString() };

            btnCancel[counter] = new Button { Text = "   X   ", ID = counter.ToString() };
            btnCancel[counter].Click += new EventHandler(btnCancelClick);
            btnCancel[counter].CssClass = "btnCross";

            // Product Code
            ProdCodeArr.Add(ds.Tables[0].Rows[0][1].ToString());

            ds.Clear();

            counter++;
            //}

            ShowShoppingList();
        }





        protected void ShowShoppingList()
        {
            divProdPakageList.Controls.Clear();
            for (int i = 0; i < counter; i++)
            {
                // start 
                divProdPakageList.Controls.Add(new LiteralControl("<div class = dynamicContentUnit>"));

                // 0
                divProdPakageList.Controls.Add(lblSL[i]);

                // 1
                divProdPakageList.Controls.Add(lblProdName[i]);

                // 2
                //divProdPakageList.Controls.Add(txtQty[i]);

                divProdPakageList.Controls.Add(lblSPrice[i]);

                // 3
                divProdPakageList.Controls.Add(btnCancel[i]);

                // end 
                divProdPakageList.Controls.Add(new LiteralControl("</div>"));
            }
        }



        protected void txtChangeQty(object sender, EventArgs e)
        {
            var qty = txtQty[counter].Text;
        }





        protected void btnCancelClick(object sender, EventArgs e)
        {
            Button dynamicBtnClick = (Button)sender;
            counter--;
        }





        private void DataVisibilityControl()
        {

            // Gridview 
            List<DataControlField> columns = grdPackage.Columns.Cast<DataControlField>().ToList();
            var isGasCylinderAvail = objCommonFun.findSettingItemValueDataTable("isGasCylinderAvail");
            if (isGasCylinderAvail == "0")
            {
                columns.Find(col => col.SortExpression == "emptyQty").Visible = false;
            }
        }





        protected void btnPackageSave_Click(object sender, EventArgs e)
        {
            string PackBranchId = "";

            if (Session["userRight"].ToString() == "Group")
            {
                PackBranchId = Session["branchId"].ToString();
            }
            else if (Session["userRight"].ToString() == "Branch")
            {
                PackBranchId = Session["roleId"].ToString();
            }
            else if (Session["userRight"].ToString() == "Regular")
            {
                PackBranchId = Session["branchId"].ToString();
            }

            string prodCode = "";

            if (isExistsPackage())
            {
                scriptMessage("Package Name Exists.", MessageType.Warning);
                ShowShoppingList();
                return;
            }

            if (txtDealerPrice.Text == "")
                txtDealerPrice.Text = "0";

            if (txtPackagePrice.Text == "")
                txtPackagePrice.Text = "0";


            for (int i = 0; i < counter; i++)
            {
                prodCode += ProdCodeArr[i].ToString() + ";";
                //qtyPack += txtQty[i].Text + ";";
                qtyPack = "1";
            }

            if (prodCode == "")
            {
                scriptMessage("Package product is empty",MessageType.Warning);
                return;
            }

            if (txtPackageName.Text.IndexOf('[') > 0 || txtPackageName.Text.IndexOf(']') > 0)
            {
                scriptMessage("[ or ] is not support in package name.", MessageType.Warning);
                return;
            }

            query = "INSERT INTO [PackageInfo] (" +
                    "packageName," +
                    "prodCode," +
                    "price," +
                    "entryDate," +
                    "roleID, " +
                    "branchId," +
                    "groupId," +
                    "createdFor, " +
                    "qty," +
                    "dealerPrice," +
                    "storeId ) VALUES " +
                    " ('" + txtPackageName.Text + "', '" +
                    prodCode + "', '" +
                    txtPackagePrice.Text + "','" +
                    objCommonFun.GetCurrentTime().ToShortDateString() + "','" +
                    Session["roleId"] + "', '" +
                    Session["branchId"] + "', '" +
                    Session["groupId"] + "', '" +
                    PackBranchId + "','" +
                    qtyPack + "','" +
                    txtDealerPrice.Text + "','" +
                    Session["storeId"] + "')";

            objSql.executeQuery(query);
            scriptMessage("Package Save Successfully.", MessageType.Success);
            grdPackage.DataBind();
            clearProductArr();
            txtPackageName.Text = "";
            txtPackagePrice.Text = "";
            txtDealerPrice.Text = "";
        }





        protected void grdPackage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ddlActiveStatus.SelectedValue == "1")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[6].Text = Resources.Language.Lbl_package_edit;
                }
            }
            else if (ddlActiveStatus.SelectedValue == "0")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[6].Text = "Restore";
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    (e.Row.Cells[6].FindControl("btnGrdDelete") as LinkButton).Text =
                        "<span class='glyphicon glyphicon-retweet'></span>";
                }
            }

            //
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProdIDs = (Label)e.Row.FindControl("lblProductCode");

                CommonFunction commonFunction = new CommonFunction();
                SalePackage salePackage = new SalePackage();

                var minQty = "0";
                var getEmptyCylinder = "0";

                if (lblProdIDs.Text != "")
                {
                    minQty = commonFunction.getPackageQty(lblProdIDs.Text);
                    getEmptyCylinder = salePackage.getEmptyCylinderQty(lblProdIDs.Text);
                }

                // Set value
                ((Label)e.Row.FindControl("lblEmptyQty")).Text = getEmptyCylinder;
                //
                Label lblMinQty = ((Label)e.Row.FindControl("lblminQty"));

                if (lblMinQty.Text == "")
                {
                    lblMinQty.Text = minQty;
                }
            }
        }





        protected void grdPackage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = grdPackage.Rows[e.RowIndex];
            Label lblAttribute = (Label)row.FindControl("lblID");
            string lblName = lblAttribute.Text;

            if (ddlActiveStatus.SelectedValue == "1")
            {
                query = "UPDATE [PackageInfo] SET active = '0' WHERE Id = '" + lblName + "' ";
                objSql.executeQuery(query);
                scriptMessage("Package Removed Successfully.", MessageType.Success);
            }
            else if (ddlActiveStatus.SelectedValue == "0")
            {
                query = "UPDATE [PackageInfo] SET active = '1' WHERE Id = '" + lblName + "' ";
                objSql.executeQuery(query);
                scriptMessage("Package Restored Successfully.", MessageType.Success);
            }
        }





        protected void grdPackage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string currentTime = objCommonFun.GetCurrentTime().ToShortDateString();
            string PackageId = (grdPackage.Rows[e.NewEditIndex].FindControl("lblID") as Label).Text;

            ds = objSql.getDataSet("SELECT * FROM PackageInfo WHERE Id = '" + PackageId + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblPackageId.Text = ds.Tables[0].Rows[0][0].ToString();
                txtPackageName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtPackagePrice.Text = ds.Tables[0].Rows[0][3].ToString();
                txtDealerPrice.Text = ds.Tables[0].Rows[0][12].ToString();
            }

            string[] splitText = new string[] { };
            string code = ds.Tables[0].Rows[0][2].ToString();
            //string finalText = code;

            splitText = code.Split(';');
            int arrCount = splitText.Length - 1;

            clearProductArr();

            for (int i = 0; i < arrCount; i++)
            {
                ds = objSql.getDataSet("SELECT * FROM [StockInfo] WHERE prodId = '" + splitText[i] + "'");

                addNewItem(splitText[i], "1");
            }

            btnPackageSave.Visible = false;
            btnUpdate.Visible = true;
        }





        protected void grdPackage_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }





        protected void grdPackage_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (e.Exception == null)
            {
                if (e.AffectedRows == 1)
                    scriptMessage("Operation Successful.", MessageType.Success);
                else
                    scriptMessage("Sorry, Operation Failed.", MessageType.Warning);
            }
            else
                scriptMessage("Sorry, Operation Failed.", MessageType.Warning);
        }





        protected void grdPackage_DataBound(object sender, EventArgs e)
        {
        }





        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string prodCode = "";

            //if (isExistsPackage())
            //{
            //    scriptMessage("Package Name Exists.", MessageType.Warning);
            //    ShowShoppingList();
            //    return;
            //}

            for (int i = 0; i < counter; i++)
            {
                prodCode += ProdCodeArr[i].ToString() + ";";
            }

            query = "UPDATE [PackageInfo] SET packageName ='" + txtPackageName.Text + "',prodCode = '" + prodCode +
                    "',price = '" + txtPackagePrice.Text + "',dealerPrice='" + txtDealerPrice.Text + "' WHERE Id = '" + lblPackageId.Text + "'";

            objSql.executeQuery(query);
            scriptMessage("Package Update Successfully.", MessageType.Success);

            clearProductArr();
            //
            txtPackageName.Text = "";
            txtPackagePrice.Text = "";
            txtDealerPrice.Text = "";
            //
        }




        protected void ddlStore_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            SearchResult();
        }




        protected void grdPackage_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPackage.PageIndex = e.NewPageIndex;
            SearchResult();
        }





        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["pageName"] = "Package";
            Session["reportQury"] = query;
            Response.Redirect("Print/LoadQuery.aspx");
        }











        [WebMethod]
        public static string getPackageDataListAddToCartAction(string prodCode)
        {
            var salePackage = new SalePackage();
            return salePackage.getPackageDataListAddToCart(prodCode);
        }






    }


}
using System.Drawing.Text;
using MetaPOS.Account.Helper;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MetaPOS.Admin.ImportBundle.View
{
    public partial class ImportProductFromApi : BasePage
    {
        private CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    LoadCategory();
                    LoadBrand();
                }
                catch (Exception)
                {
                    this.ClientScript.RegisterClientScriptBlock(typeof(string), "alert", "alert('No Data! Please contact support team.')", true);
                    btnApplyFilter.Attributes.Add("disabled", "disabled");
                    return;
                }

                ddlCategoryList.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlCompanyList.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlBrandList.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlSubCategoryList.Items.Insert(0, new ListItem("-- Select --", "0"));


                //user create category,supplier,store
                commonFunction.fillAllDdl(ddlCategory, "SELECT catName,Id FROM [CategoryInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY catName ASC", "catName",
                  "Id");
                commonFunction.fillAllDdl(ddlSupplier, "SELECT supCompany,SupID FROM [SupplierInfo] WHERE active='1' " + Session["userAccessParameters"] + " ORDER BY supCompany ASC",
                        "supCompany", "SupID");
                commonFunction.fillAllDdl(ddlStore, "SELECT name,Id FROM [WarehouseInfo] WHERE active='1' AND name !='' " + Session["userAccessParameters"] + " ORDER BY name ASC", "name",
                        "Id");
                

                ddlCategory.Items.Insert(0, new ListItem(Resources.Language.Lbl_importProductFromApi_select_category, "0"));
                ddlSupplier.Items.Insert(0, new ListItem(Resources.Language.Lbl_importProductFromApi_select_supplier, "0"));
                ddlStore.Items.Insert(0, new ListItem(Resources.Language.Lbl_importProductFromApi_select_store, "0"));


                //preBarcode = generateBarcodePreNumber();
            }
            
           
        }


        public void ScriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }


        protected void btnApplyFilter_onclick(object sender, EventArgs e)
        {


            if (ddlCompanyList.SelectedValue == "0")
            {
                ScriptMessage("Please Select a Company", MessageType.Warning);
                return;
            }

            string args = "";
            if (ddlCompanyList.SelectedValue != "0")
                args += "companyId=" + ddlCompanyList.SelectedValue;
            if (ddlCategoryList.SelectedValue != "0")
                args += "&categoryId=" + ddlCategoryList.SelectedValue;
            if (ddlBrandList.SelectedValue != "0")
                args += "&brandId=" + ddlBrandList.SelectedValue;
            if (ddlSubCategoryList.SelectedValue != "0")
                args += "&subcategoryId=" + ddlSubCategoryList.SelectedValue;

            panelProductData.Visible = true;

            LoadProductDataAfterFilter(args);
        }


        private void LoadCategory()
        {
            var url = "https://api.robiamarhishab.xyz/api/ProductCategories/TopList";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("AuthCode", "ASDfghJKL098)(*");

            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                object result = JsonConvert.DeserializeObject<object>(responseFromServer);
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < jArray.Length; i++)
                {
                    var text = jArray[i]["name"].ToString();
                    var value = jArray[i]["id"].ToString();
                    items.Add(new ListItem { Text = text, Value = value });
                }
                ddlCategoryList.DataSource = items;
                ddlCategoryList.DataTextField = "Text";
                ddlCategoryList.DataValueField = "Value";
                ddlCategoryList.DataBind();
            }
            LoadCompay();
        }

        private void LoadSubCategory(string Id)
        {
            var url = "https://api.robiamarhishab.xyz/api/ProductCategories/SubList?id=" + Id;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("AuthCode", "ASDfghJKL098)(*");

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                object result = JsonConvert.DeserializeObject<object>(responseFromServer);
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < jArray.Length; i++)
                {
                    var text = jArray[i]["name"].ToString();
                    var value = jArray[i]["id"].ToString();
                    items.Add(new ListItem { Text = text, Value = value });
                }
                ddlSubCategoryList.DataSource = items;
                ddlSubCategoryList.DataTextField = "Text";
                ddlSubCategoryList.DataValueField = "Value";
                ddlSubCategoryList.DataBind();
            }
        }

        private void LoadChildCategoryFirst(string Id)
        {
            var url = "https://api.robiamarhishab.xyz/api/ProductCategories/SubList?id=" + Id;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("AuthCode", "ASDfghJKL098)(*");

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                object result = JsonConvert.DeserializeObject<object>(responseFromServer);
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < jArray.Length; i++)
                {
                    var text = jArray[i]["name"].ToString();
                    var value = jArray[i]["id"].ToString();
                    items.Add(new ListItem { Text = text, Value = value });
                }
                ddlChildCategoryList.DataSource = items;
                ddlChildCategoryList.DataTextField = "Text";
                ddlChildCategoryList.DataValueField = "Value";
                ddlChildCategoryList.DataBind();

                divChildCategory.Visible = true;
                ddlChildCategoryList.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }


        private void LoadChildCategoryTwo(string Id)
        {
            var url = "https://api.robiamarhishab.xyz/api/ProductCategories/SubList?id=" + Id;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("AuthCode", "ASDfghJKL098)(*");

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                object result = JsonConvert.DeserializeObject<object>(responseFromServer);
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < jArray.Length; i++)
                {
                    var text = jArray[i]["name"].ToString();
                    var value = jArray[i]["id"].ToString();
                    items.Add(new ListItem { Text = text, Value = value });
                }
                ddlChildCategoryListTwo.DataSource = items;
                ddlChildCategoryListTwo.DataTextField = "Text";
                ddlChildCategoryListTwo.DataValueField = "Value";
                ddlChildCategoryListTwo.DataBind();

                divChildCategoryTwo.Visible = true;
                ddlChildCategoryListTwo.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void LoadCompay()
        {
            var url = "https://api.robiamarhishab.xyz/api/Companies/AllList";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("AuthCode", "ASDfghJKL098)(*");

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                object result = JsonConvert.DeserializeObject<object>(responseFromServer);
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < jArray.Length; i++)
                {
                    var text = jArray[i]["name"].ToString();
                    var value = jArray[i]["id"].ToString();
                    items.Add(new ListItem { Text = text, Value = value });
                }
                ddlCompanyList.DataSource = items;
                ddlCompanyList.DataTextField = "Text";
                ddlCompanyList.DataValueField = "Value";
                ddlCompanyList.DataBind();
            }
        }

        private void LoadBrand()
        {
            var url = "https://api.robiamarhishab.xyz/api/Brands/AllList";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("AuthCode", "ASDfghJKL098)(*");

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                object result = JsonConvert.DeserializeObject<object>(responseFromServer);
                JToken[] jArray = ((result as JArray) as JToken).ToArray();
                List<ListItem> items = new List<ListItem>();
                for (int i = 0; i < jArray.Length; i++)
                {
                    var text = jArray[i]["name"].ToString();
                    var value = jArray[i]["id"].ToString();
                    items.Add(new ListItem { Text = text, Value = value });
                }
                ddlBrandList.DataSource = items;
                ddlBrandList.DataTextField = "Text";
                ddlBrandList.DataValueField = "Value";
                ddlBrandList.DataBind();
            }
        }


        private void LoadProductDataAfterFilter(string args)
        {
            var url = "https://api.robiamarhishab.xyz/api/Products/List?" + args;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Headers.Add("AuthCode", "ASDfghJKL098)(*");

            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(responseFromServer);

                if (dataTable.Columns.Count <= 0)
                {
                    ScriptMessage("Data is Empty! Select another company.", MessageType.Warning);
                    return;
                }

                // Create the DataView of the DataTable
                DataView view = new DataView(dataTable);
                // Create a new DataTable from the DataView with just the columns desired - and in the order desired
                DataTable selectedColumn = view.ToTable("Selected", false, "id", "title", "sku", "code", "unit", "purchasePerUnit", "salePerUnit", "updatedAt");
                selectedColumn.Columns["id"].ColumnName = "Id";
                selectedColumn.Columns["title"].ColumnName = "Title";
                selectedColumn.Columns["sku"].ColumnName = "Sku";
                selectedColumn.Columns["code"].ColumnName = "Code";
                selectedColumn.Columns["unit"].ColumnName = "Unit";
                selectedColumn.Columns["purchasePerUnit"].ColumnName = "Purchase Price";
                selectedColumn.Columns["salePerUnit"].ColumnName = "Sale Price";
                selectedColumn.Columns["updatedAt"].ColumnName = "Update Date";


                grdProductDataFilter.DataSource = selectedColumn;
                grdProductDataFilter.DataBind();
            }
        }

        protected void ddlCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = ddlCategoryList.SelectedValue;
            LoadSubCategory(selected);

            ddlSubCategoryList.Items.Insert(0, new ListItem("-- Select --", "0"));
        }


        protected void ddlSubCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {

            var selectedId = ddlSubCategoryList.SelectedValue;
            LoadChildCategoryFirst(selectedId);
        }

        protected void ddlChildCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedId = ddlChildCategoryList.SelectedValue;
            LoadChildCategoryTwo(selectedId);
        }

        protected void grdProductDataFilter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    CheckBox checkbox = new CheckBox();
            //    checkbox.ID = "chkChecked";
            //    checkbox.CheckedChanged += CheckedChaged;
            //    e.Row.Cells[0].Controls.Add(checkbox);
            //}
        }

        protected void CheckedChaged(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(typeof(string), "alert", "alert('Hi')", true);

        }

        protected void btnImportProductData_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in grdProductDataFilter.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)(row.FindControl("chkGrdSelect"));
                    if (chk.Checked)
                    {



                        // category.
                        var categoryId = ddlCategory.SelectedValue;
                        var supplierId = ddlSupplier.SelectedValue;
                        var ddlStoreId = ddlStore.SelectedValue;


                        if (ddlStoreId == "0")
                        {
                            ScriptMessage("Please Select your store", MessageType.Warning);
                            return;
                        }

                        if (supplierId == "0")
                        {
                            ScriptMessage("Please Select your supplier", MessageType.Warning);
                            return;
                        }

                        if (categoryId == "0")
                        {
                            ScriptMessage("Please Select your category", MessageType.Warning);
                            return;
                        }

                        

                        // category.
                        //var category = "";
                        //var subChildCategoryTwoIndex = ddlChildCategoryListTwo.SelectedIndex;
                        //var subChildCategoryOneIndex = ddlChildCategoryList.SelectedIndex;
                        //var subCategoryIndex = ddlSubCategoryList.SelectedIndex;
                        //var categoryitemIndex = ddlCategoryList.SelectedIndex;

                        //if (subChildCategoryTwoIndex != 0)
                        //    category = ddlChildCategoryList.SelectedItem.Text;
                        //else if (subChildCategoryOneIndex != 0)
                        //    category = ddlSubCategoryList.SelectedItem.Text;
                        //else if (subCategoryIndex != 0)
                        //    category = ddlSubCategoryList.SelectedItem.Text;
                        //else
                        //    category = ddlChildCategoryListTwo.SelectedItem.Text;


                        //var sqlOperation = new SqlOperation();

                        //var categoryId = "";
                        //string query = "SELECT * FROM CategoryInfo WHERE catName='" + category + "'";
                        //var dt = sqlOperation.getDataTable(query);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    categoryId = dt.Rows[0]["Id"].ToString();
                        //}
                        //else
                        //{
                        //    query = "INSERT INTO CategoryInfo (catName, entryDate, updateDate, roleId) VALUES ('" + category + "', '" + commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "','" + Session["roleId"].ToString() + "')";
                        //    categoryId = sqlOperation.executeQueryScalar(query);
                        //}


                        //// brand 

                        //// Company
                        ////var supplier = ddlCompanyList.SelectedItem.Text;
                        //string supplierId = "", supplierTmpId = "";
                        //query = "SELECT * FROM SupplierInfo WHERE supCompany='" + supplier + "'";
                        //var dtCat = sqlOperation.getDataTable(query);
                        //if (dtCat.Rows.Count > 0)
                        //{
                        //    supplierId = dtCat.Rows[0]["supId"].ToString();
                        //}
                        //else
                        //{
                        //    Random _random = new Random();
                        //    int supID = _random.Next();

                        //    query = "INSERT INTO SuplierInfo (supId, supCompany, entryDate, updateDate, roleId) VALUES ('"+supID+"','" + supplier + "', '" + commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "','" + Session["roleId"].ToString() + "')";
                        //    supplierTmpId = sqlOperation.executeQueryScalar(query);

                        //    query = "SELECT * FROM SupplierInfo WHERE Id='" + supplierTmpId + "'";
                        //    var dtSupTmp = sqlOperation.getDataTable(query);
                        //    if (dtSupTmp.Rows.Count > 0)
                        //    {
                        //        supplierId = dtSupTmp.Rows[0]["supId"].ToString();
                        //    }
                        //}

// DataTable selected = view.ToTable("Selected", false, "Id", "title", "sku", "code", "unit", "purchasePerUnit", "salePerUnit", "updatedAt");

                        // Product
                        var stockModel = new StockModel();
                        var barCode = commonFunction.barcodeGenerator();
                        string id = row.Cells[1].Text;
                        string name = row.Cells[2].Text;
                        string sku = row.Cells[3].Text == "&nbsp;" ? "0" : row.Cells[3].Text; 
                        string pCode = row.Cells[4].Text;
                        string unit = row.Cells[5].Text;
                        decimal buyPrice = Convert.ToDecimal(row.Cells[6].Text);
                        decimal salePrice = Convert.ToDecimal(row.Cells[7].Text);
                        string updateDate = row.Cells[8].Text;
                        var isExsited = isProductExsit(name);
                        if (!isExsited)
                        {
                            return;
                        }

                        var newProductId = commonFunction.GenerateNewProductId();
                        stockModel.prodId = newProductId;
                        stockModel.prodName = name;
                        stockModel.prodCode = barCode;//assign product code
                        stockModel.pCode = pCode;
                        stockModel.prodDescr = "";
                        stockModel.supCompany = supplierId;
                        stockModel.catName = categoryId;
                        stockModel.qty = "0.0";
                        stockModel.bPrice = buyPrice;
                        stockModel.sPrice = salePrice;
                        stockModel.weight = 0;
                        stockModel.size = "";
                        stockModel.discount = 0;
                        stockModel.stockTotal = 0;
                        stockModel.entryQty = "0.0";
                        stockModel.title = "";
                        stockModel.branchName = "";
                        stockModel.fieldAttribute = "";
                        stockModel.tax = "";
                        stockModel.sku = sku;
                        stockModel.lastQty = "0.0";
                        stockModel.warningQty = "0";
                        stockModel.imei = "";
                        stockModel.warranty = "0-0-0";
                        stockModel.dealerPrice = 0;
                        stockModel.purchaseCode = commonFunction.nextPurchaseCode();
                        stockModel.createdFor = Session["roleId"].ToString();
                        stockModel.receivedDate = commonFunction.GetCurrentTime();
                        stockModel.expiryDate = DateTime.Now;
                        stockModel.batchNo = "";
                        stockModel.serialNo = "";
                        stockModel.ShipmentStatus = 0;
                        stockModel.manufacturerId = 0;
                        stockModel.notes = "";
                        stockModel.unitId = 0;
                        stockModel.storeId = Convert.ToInt32(ddlStoreId);
                        stockModel.SupCommission = 0;
                        stockModel.purchaseDate = commonFunction.GetCurrentTime();
                        stockModel.imeiStatus = '0';
                        stockModel.engineNumber = "";
                        stockModel.cecishNumber = "";
                        stockModel.comPrice = 0;
                        stockModel.locationId = 0;
                        stockModel.freeQty = "0";
                        stockModel.billNo = "0";
                        stockModel.fieldRecord = "0";
                        stockModel.attributeRecord = "0";
                        stockModel.parentId = "0";
                        stockModel.balanceQty = "0.0";
                        stockModel.Status = "stock";
                        stockModel.roleId = Session["roleId"].ToString();
                        stockModel.branchId = Session["branchId"].ToString();
                        stockModel.entryDate = commonFunction.GetCurrentTime();
                        stockModel.updateDate = Convert.ToDateTime(updateDate);
                        stockModel.searchType = "product";
                        



                        // Create stock
                        var q = stockModel.createStock();

                        var qa = stockModel.createStockQtyManagement();

                        // Create StockStatusInfo
                        stockModel.createStockStatus();

                    }
                }
            }

            //success msg 
            ScriptMessage("Product imported successfully", MessageType.Success);
        }

       protected bool isProductExsit(string prodName)
        {
            SqlOperation objSql = new SqlOperation();
            DataTable dtProductTable = objSql.getDataTable("SELECT prodID FROM StockInfo WHERE prodName = '" + prodName.Trim() + "'");
            if (dtProductTable.Rows.Count > 0)
            {
                ScriptMessage("This product name already exists!" + prodName + "", MessageType.Warning);
                return false;
            }
            return true;
        }


        private void AddCheckBox()
        {

            foreach (GridViewRow row in grdProductDataFilter.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cb = new CheckBox();
                    cb.Checked = false;
                    cb.ID = "chkChecked";
                    row.Cells[0].Controls.Add(cb);
                }
            }
        }

    }
}
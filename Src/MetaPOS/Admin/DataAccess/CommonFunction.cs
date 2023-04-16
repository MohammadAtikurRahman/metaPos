using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using MetaPOS.Admin.InventoryBundle.Service;
using MetaPOS.Admin.InventoryBundle.View;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.PromotionBundle.Service;
using MetaPOS.Admin.SaleBundle.View;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Configuration;
using MetaPOS.Account.Helper;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace MetaPOS.Admin.DataAccess
{


    public class CommonFunction
    {


        // Initialize Controller
        //Sale objSaleModel = new Sale();

        private SqlOperation sqlOperation = new SqlOperation();
        //private DataTable dt;

        private string query = "";//, isPaidWatermark = "";
        private const string initVector = "tu89geji340t89u2", Key = "emergersIT.com";
        private const int keysize = 256;
        public static int msgCode;
        public static decimal cashDue = 0, balance = 0;
        public static string isCatagoryProduct = "", isPaymentHistory = "", isUnicode = "", printBillNo = "";
        private static bool isProdCode = false, accessProduct = false;



        public void pageout()
        {
            if (HttpContext.Current.Session["email"] == null)
            {
                logout();
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Admin/404");
            }
        }





        public void stockProductsQtyCalculateAdjustment()
        {

        }


        public void calculateToSaveStockQty(string prodId, string storeId, string unitId)
        {
            query = @"
                    Select 
                    ((Select  (
                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,2) as INT) END),0)) -
                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,2) as INT) END),0)+
                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,2) as INT) END),0))
                    ) AS qty
                    FROM StockStatusInfo as stockstatus
                    WHERE stockstatus.prodId = stock.prodID AND stockstatus.storeId = warehouse.Id  )) AS qty,
                    ((Select  (
                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,1) as INT) END),0)) -
                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,1) as INT) END),0)+
                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,1) as INT) END),0))
                    ) AS qty
                    FROM StockStatusInfo as stockstatus
                    WHERE stockstatus.prodId = stock.prodID   AND stockstatus.storeId = warehouse.Id  )) AS piece
                    FROM StockInfo stock
                    LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodId = stock.prodId 
                    LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = stockstatus.storeId 
                    WHERE stockstatus.prodId='" + prodId
                    + "' AND stockstatus.storeId='" + storeId + "' GROUP BY stock.prodId  ,stockstatus.storeId,warehouse.Id";

            var dtBalQty = sqlOperation.getDataTable(query);

            var qty = 0;
            var piece = 0;

            for (int i = 0; i < dtBalQty.Rows.Count; i++)
            {
                qty += Convert.ToInt32(dtBalQty.Rows[i]["qty"]);
                piece += Convert.ToInt32(dtBalQty.Rows[i]["piece"]);
            }


            var dtRatio = sqlOperation.getDataTable("SELECT unitRatio FROM unitInfo WHERE Id='" + unitId + "'");
            var ratio = 1;
            var originalQty = qty;
            var originalPiece = 0;
            if (dtRatio.Rows.Count > 0)
            {
                ratio = Convert.ToInt32(dtRatio.Rows[0][0].ToString());
                if (piece < 0)
                {
                    var pieceToQty = piece / ratio;
                    var exitingPiece = piece % ratio;

                    originalQty -= pieceToQty + 1;
                    originalPiece = ratio - Math.Abs(exitingPiece);
                }
                else
                {
                    var pieceToQty = piece / ratio;
                    var exitingPiece = piece % ratio;

                    originalQty += pieceToQty;
                    originalPiece = exitingPiece;
                }
            }
            else
            {
                originalPiece = piece;
            }


            string originalQtyWithPiece = originalQty + "." + originalPiece;


            // save to stockinfo
            var stockModel = new StockModel();
            stockModel.prodId = Convert.ToInt32(prodId);
            stockModel.storeId = Convert.ToInt32(storeId);
            stockModel.qty = originalQtyWithPiece;
            stockModel.UpdateStockQtyCalculateAutomatic();

        }

        public string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }




        public bool CheckConnectionString(string subdomain)
        {
            Configuration Config1 = WebConfigurationManager.OpenWebConfiguration("~");
            var conSetting = (ConnectionStringsSection)Config1.GetSection("connectionStrings");


            var conStringCount = conSetting.ConnectionStrings.Count;
            for (int i = 0; i < conStringCount; i++)
            {
                var conStringDomain = conSetting.ConnectionStrings[i].Name;
                if (conStringDomain.ToLower() == subdomain.ToLower())
                {
                    return true;
                }
            }

            return false;
        }








        public void LoginExecutable(DataTable dtRoleAcess, string conString)
        {
            //
            HttpContext.Current.Session["stockProblmemId"] = "";
            var monthlyFee = 0M;
            DataTable dtBranch;

            HttpContext.Current.Session["roleId"] = dtRoleAcess.Rows[0]["roleId"].ToString();
            HttpContext.Current.Session["title"] = dtRoleAcess.Rows[0]["title"].ToString();
            HttpContext.Current.Session["password"] = dtRoleAcess.Rows[0]["password"].ToString();
            HttpContext.Current.Session["accessPage"] = dtRoleAcess.Rows[0]["accessPage"].ToString();
            HttpContext.Current.Session["userRight"] = dtRoleAcess.Rows[0]["userRight"].ToString();
            HttpContext.Current.Session["branchId"] = dtRoleAcess.Rows[0]["branchId"].ToString();
            HttpContext.Current.Session["email"] = dtRoleAcess.Rows[0]["email"].ToString();
            HttpContext.Current.Session["groupId"] = dtRoleAcess.Rows[0]["groupId"].ToString();
            HttpContext.Current.Session["version"] = dtRoleAcess.Rows[0]["version"].ToString();
            HttpContext.Current.Session["accessTo"] = dtRoleAcess.Rows[0]["accessTo"].ToString();
            HttpContext.Current.Session["storeId"] = dtRoleAcess.Rows[0]["storeId"].ToString();
            HttpContext.Current.Session["SessionAccessPage"] = dtRoleAcess.Rows[0]["accessPage"].ToString();
            //HttpContext.Current.Session["isSecureAccount"] = dtRoleAcess.Rows[0]["isSecureAccount"].ToString();

            if (dtRoleAcess.Columns.Contains("ExpiryDate"))
                HttpContext.Current.Session["expiryDate"] = dtRoleAcess.Rows[0]["expiryDate"].ToString();

            if (dtRoleAcess.Columns.Contains("monthlyFee"))
                monthlyFee = Convert.ToDecimal(dtRoleAcess.Rows[0]["monthlyFee"].ToString());

            if (HttpContext.Current.Session["userRight"].ToString() == "Group")
            {
                dtBranch = sqlOperation.getDataTable("SELECT branchName, branchWebsite,branchAddress,branchPhone FROM [BranchInfo] WHERE Id = '" +
                                HttpContext.Current.Session["roleId"] + "' ");
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
            {
                dtBranch = sqlOperation.getDataTable("SELECT branchName, branchWebsite,branchAddress,branchPhone FROM [BranchInfo] WHERE storeId = '" +
                                HttpContext.Current.Session["storeId"] + "' ");
            }
            else // if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
            {
                dtBranch = sqlOperation.getDataTable("SELECT branchName, branchWebsite,branchAddress,branchPhone FROM [BranchInfo] WHERE storeId = '" +
                                HttpContext.Current.Session["storeId"] + "'");

                var dtRoleAcessExpiry = sqlOperation.getDataTable("SELECT * FROM [RoleInfo] WHERE roleId = '" + HttpContext.Current.Session["branchId"] + "'");
                if (dtRoleAcessExpiry.Rows.Count > 0)
                {
                    HttpContext.Current.Session["expiryDate"] = dtRoleAcessExpiry.Rows[0]["expiryDate"].ToString();
                    monthlyFee = Convert.ToDecimal(dtRoleAcess.Rows[0]["monthlyFee"].ToString());
                }
            }




            try
            {
                HttpContext.Current.Session["comName"] = "Your Company";
                HttpContext.Current.Session["urlPath"] = "";

                if (dtBranch.Rows.Count > 0)
                {
                    HttpContext.Current.Session["comName"] = dtBranch.Rows[0]["branchName"].ToString();
                    HttpContext.Current.Session["urlPath"] = dtBranch.Rows[0]["branchWebsite"].ToString();
                    HttpContext.Current.Session["comAddress"] = dtBranch.Rows[0]["branchAddress"].ToString();
                    HttpContext.Current.Session["comPhone"] = dtBranch.Rows[0]["branchPhone"].ToString();
                }
            }
            catch (Exception)
            {
                HttpContext.Current.Session["comName"] = "Your Company";
                HttpContext.Current.Session["urlPath"] = "";
                HttpContext.Current.Session["expiryDate"] = GetCurrentTime();
            }



            // Get User Access Parameters
            HttpContext.Current.Session["userAccessParameters"] = "";
            var branchRoleId = "";
            DataSet dsUserAccessParameters = sqlOperation.getDataSet("SELECT * FROM RoleInfo");

            if (HttpContext.Current.Session["userRight"].ToString() == "Group")
            {
                dsUserAccessParameters = sqlOperation.getDataSet("SELECT * FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["roleId"] + "' OR groupId ='" + HttpContext.Current.Session["roleId"] + "' ");
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
            {
                dsUserAccessParameters = sqlOperation.getDataSet("SELECT * FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["roleId"] + "' OR branchId ='" + HttpContext.Current.Session["roleId"] + "' ");
                branchRoleId = HttpContext.Current.Session["roleId"].ToString();
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
            {
                dsUserAccessParameters = sqlOperation.getDataSet("SELECT * FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["branchId"] + "' OR branchId ='" + HttpContext.Current.Session["branchId"] + "' ");
                branchRoleId = HttpContext.Current.Session["branchId"].ToString();
            }

            for (int count = 0; count < dsUserAccessParameters.Tables[0].Rows.Count; count++)
            {
                if (count == 0)
                    HttpContext.Current.Session["userAccessParameters"] = " AND ( ";

                if (count == dsUserAccessParameters.Tables[0].Rows.Count - 1)
                    HttpContext.Current.Session["userAccessParameters"] += "roleId = '" + dsUserAccessParameters.Tables[0].Rows[count][0] + "' ) ";
                else
                    HttpContext.Current.Session["userAccessParameters"] += "roleId = '" + dsUserAccessParameters.Tables[0].Rows[count][0] + "' OR ";
            }


            bool isCreatedThisMonth = false;
            var subsModel = new SubscriptionModel();
            try
            {
                var dtSubscription = subsModel.getSubscribeCreatedInfo();
                if (dtSubscription.Rows.Count > 0)
                {
                    isCreatedThisMonth = true;
                    DateTime sessiponDateTime = Convert.ToDateTime(HttpContext.Current.Session["expiryDate"].ToString());
                    DateTime nowDateTime = Convert.ToDateTime(GetCurrentTime().ToString("dd-MMM-yyyy"));
                    if (sessiponDateTime.Date <= nowDateTime.Date.AddDays(2))
                    {
                        isCreatedThisMonth = false;
                    }

                    if (dtSubscription.Rows[0]["status"].ToString() == "0")
                    {
                        isCreatedThisMonth = true;
                    }
                }
                
            }
            catch (Exception)
            {

            }
            HttpContext.Current.Session["expiryNotification"] = "0";
            var expiryDate = Convert.ToDateTime(HttpContext.Current.Session["ExpiryDate"].ToString());
            if (Convert.ToDateTime(HttpContext.Current.Session["ExpiryDate"].ToString()) <= GetCurrentTime())
            {
                HttpContext.Current.Session["expiryNotification"] = "2";
            }
            else if (expiryDate.AddDays(-5) < GetCurrentTime())
            {
                HttpContext.Current.Session["expiryNotification"] = "1";
            }

            query = "BEGIN TRANSACTION "

                        + "IF OBJECT_ID('dbo.SubscriptionInfo','U') IS NULL "
                        + "BEGIN CREATE TABLE SubscriptionInfo(Id INT IDENTITY (1, 1) NOT NULL, "
                        + "[roleId]             INT            DEFAULT ((0)) NOT NULL, "
                        + "[storeId]            INT            DEFAULT ((0)) NOT NULL, "
                        + "[invoiceNo]          NVARCHAR (50) NULL, "
                        + "[name]               NVARCHAR (100) NULL, "
                        + "[description]        NVARCHAR (MAX) NULL, "
                        + "[cashin]             DECIMAL (12,2) DEFAULT ((0)) NOT NULL, "
                        + "[cashout]            DECIMAL (12,2) DEFAULT ((0)) NOT NULL, "
                        + "[status]             NVARCHAR (25) NULL, "
                        + "[paymentMode]        NVARCHAR (25) NULL, "
                        + "[type]               NVARCHAR (25) NULL, "
                        + "createDate            DATETIME DEFAULT ((getdate())) NOT NULL , "
                        + "updateDate            DATETIME DEFAULT ((getdate())) NOT NULL, "
                        + "PRIMARY KEY CLUSTERED (Id ASC)) END "

                        + "COMMIT";
            sqlOperation.executeQueryWithoutAuth(query);

            // Check expiry date
            if (!isCreatedThisMonth)
            {

                var commonFunction = new CommonFunction();

                if (monthlyFee > 0)
                {
                    subsModel.roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"].ToString());
                    subsModel.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
                    subsModel.invoiceNo = GetCurrentTime().Ticks.ToString();
                    subsModel.name = "Subscription";
                    subsModel.description = "Due date is " + expiryDate.ToString("dd-MMM-yyyy");
                    subsModel.paymentMode = "";
                    subsModel.cashin = 0M;
                    subsModel.cashout = monthlyFee;
                    subsModel.type = "Billed";
                    subsModel.status = "0";
                    subsModel.createDate = commonFunction.GetCurrentTime();
                    subsModel.updateDate = commonFunction.GetCurrentTime();
                    subsModel.saveSubscriptionModel();

                }
            }



            // Store parameters
            HttpContext.Current.Session["storeAccessParameters"] = "";

            query = "BEGIN TRANSACTION "
                    + " IF COL_LENGTH('RoleInfo','storeId') IS NULL "
                    + " BEGIN ALTER TABLE RoleInfo ADD storeId INT DEFAULT(('0')) NOT NULL END "

                    + "COMMIT";
            sqlOperation.executeQueryWithoutAuth(query);

            var dtRoleSotreAccess = sqlOperation.getDataTable("SELECT storeId FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["roleId"] + "' OR branchId='" + HttpContext.Current.Session["roleId"] + "'");
            for (int i = 0; i < dtRoleSotreAccess.Rows.Count; i++)
            {
                if (i == 0)
                    HttpContext.Current.Session["storeAccessParameters"] += " AND (storeId ='" + dtRoleSotreAccess.Rows[i]["storeId"] + "'";

                HttpContext.Current.Session["storeAccessParameters"] += " OR storeId ='" + dtRoleSotreAccess.Rows[i]["storeId"] + "'";

                if (i == dtRoleSotreAccess.Rows.Count - 1)
                    HttpContext.Current.Session["storeAccessParameters"] += ")";
            }


            HttpContext.Current.Session["storeId"] = "";
            try
            {
                var dtRoleStoreId = sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["roleId"] + "'");
                if (dtRoleStoreId.Rows.Count > 0)
                {
                    HttpContext.Current.Session["storeId"] = dtRoleStoreId.Rows[0]["storeId"].ToString();
                }
            }
            catch (Exception)
            {

            }



            // StoreWise get roleId
            HttpContext.Current.Session["roleIdAccessStoreWise"] = "";

            var dtRoleIDStoreWise = sqlOperation.getDataTable("SELECT roleId FROM RoleInfo WHERE storeId='" + HttpContext.Current.Session["storeId"] + "'");
            for (int i = 0; i < dtRoleIDStoreWise.Rows.Count; i++)
            {
                if (i == 0)
                    HttpContext.Current.Session["roleIdAccessStoreWise"] += " AND (roleId ='" + dtRoleIDStoreWise.Rows[i]["roleId"] + "'";

                HttpContext.Current.Session["roleIdAccessStoreWise"] += " OR roleId ='" + dtRoleIDStoreWise.Rows[i]["roleId"] + "'";

                if (i == dtRoleIDStoreWise.Rows.Count - 1)
                    HttpContext.Current.Session["roleIdAccessStoreWise"] += ")";
            }

            HttpContext.Current.Session["mainStoreId"] = "";
            HttpContext.Current.Session["lang"] = "bn";


            var inilitalizeDatabase = new DatabaseInitilizer();
            inilitalizeDatabase.Initilize();

            //Thread upgradingVersion = new Thread(delegate() { Login.Default.CheckDbColumns(); });
            //upgradingVersion.IsBackground = true;
            //upgradingVersion.Start();

            // Connection name set
            HttpContext.Current.Session["connectionStringName"] = conString;

        }


        public void isLoggedIn()
        {
            if (HttpContext.Current.Session["email"] != null)
            {
                var SessionAccessPage = HttpContext.Current.Session["SessionAccessPage"].ToString();
                if (HttpContext.Current.Session["userRight"].ToString() == "Super" ||
                HttpContext.Current.Session["userRight"].ToString() == "Group")
                {
                    HttpContext.Current.Response.Redirect("~/admin/user");
                }
                //else if (Convert.ToBoolean(HttpContext.Current.Session["isSecureAccount"]) == false)
                //{
                //    HttpContext.Current.Response.Redirect("~/admin/security");
                //}
                else if (SessionAccessPage.Contains("Dashboard;"))
                {
                    HttpContext.Current.Response.Redirect("~/admin/dashboard");
                }
                else if (SessionAccessPage.Contains("Invoice;"))
                {
                    HttpContext.Current.Response.Redirect("~/admin/invoice-next");
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/admin/security");
                }
            }
        }


        public void logout()
        {
            var db = "";
            try
            {
                db = HttpContext.Current.Session["conString"].ToString();
                //Login.Default.varSessionAccessPage = "";
                //HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                //System.Web.Security.FormsAuthentication.SignOut();
            }
            catch (Exception)
            {
                db = "error";
            }

            HttpContext.Current.Response.Redirect("~/login");
            //HttpContext.Current.Response.Redirect("~/account/login?domain=" + db);
        }





        public DateTime GetCurrentTime()
        {
            string host = HttpContext.Current.Request.Url.Host;
            DateTime exactTime = DateTime.Now;

            //string timezone = findSettingItemValue(43);

            if (host != "localhost")
                exactTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(exactTime, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");

            return exactTime;
        }





        public string pageVerificationSignal()
        {
            return ("You dont have permit access this page.");
        }






        public void fillAllDdl(DropDownList ddlName, string query, string fieldName, string fieldValue)
        {
            try
            {
                var dsField = sqlOperation.getDataSet(query);
                ddlName.DataSource = dsField;
                ddlName.DataTextField = fieldName;
                ddlName.DataValueField = fieldValue;
                //if (ddlName.SelectedItem != null)
                //    ddlName.SelectedItem.Selected = false;

                ddlName.SelectedIndex = -1;
                if (ddlName.SelectedValue.Length > 0)
                {
                    ddlName.SelectedValue.Remove(0);
                }

                ddlName.DataBind();
            }
            catch (Exception)
            {
                pageout();
            }

        }




        public void fillAllDdl(DropDownList ddlName, string tblName, string status)
        {
            if (tblName == "WarehouseInfo")
            {
                try
                {
                    var recordModel = new RecordModel();
                    recordModel.status = status;

                    if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
                    {
                        ddlName.DataSource = recordModel.getWarehouseListForBranch();
                    }
                    else
                    {
                        ddlName.DataSource = recordModel.getWarehouseListForStore();
                    }

                    ddlName.DataTextField = "name";
                    ddlName.DataValueField = "Id";
                    ddlName.DataBind();
                }
                catch (Exception)
                {
                    pageout();
                }
            }
        }





        public void fillDdlCatt(DropDownList ddlCatt, string ddlSupplierTxt)
        {
            try
            {
                ddlCatt.DataSource = sqlOperation.getDataSet("SELECT DISTINCT catName FROM [ProductInfo] WHERE supCompany = '" + ddlSupplierTxt + "'");
                ddlCatt.DataTextField = "catName";
                ddlCatt.DataValueField = "catName";
                ddlCatt.DataBind();
                ddlCatt.Items.Insert(0, "Select All");
            }
            catch (Exception)
            {
                pageout();
            }
        }





        public bool accessChecker(string pageName)
        {
            if (HttpContext.Current.Session["accessPage"] == null)
                pageout();

            var accessPageList = "";

            if (HttpContext.Current.Session["userRight"].ToString() == "Super")
            {
                accessPageList =
                    "Dashboard; User; Security; Version; Docs; Add; edit; Support; ";

                if (accessPageList.Contains(pageName + ";"))
                    return true;
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Group")
            {
                accessPageList =
                    "Dashboard; User; Security; Version; Support; Profile; Payment;";

                var pages = HttpContext.Current.Session["accessPage"];
                if (pages != null && pages.ToString().Contains(pageName + ";"))
                {
                    if ("Add" == pageName || "Edit" == pageName)
                        return true;
                }

                if (accessPageList.Contains(pageName + ";"))
                    return true;
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Branch" ||
                     HttpContext.Current.Session["userRight"].ToString() == "Regular")
            {
                var pages = HttpContext.Current.Session["accessPage"] + "Setting;  SmsConfig;";


                if (pages != null && pages.ToString().Contains(pageName + ";"))
                    return true;
            }

            return false;
        }


        public bool accessToChecker(string functionName)
        {
            if (HttpContext.Current.Session["userRight"].ToString() == "Branch" ||
                HttpContext.Current.Session["userRight"].ToString() == "Regular")
            {
                var functions = HttpContext.Current.Session["accessTo"];



                if (functions != null && functions.ToString().Contains(functionName + ";"))
                    return true;
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Super" ||
                     HttpContext.Current.Session["userRight"].ToString() == "Group")
            {
                return true;
            }

            return false;
        }


        public bool accessCheckerCreatingUser(string pageName)
        {
            if (HttpContext.Current.Session["accessPage"] == null)
                pageout();

            if (HttpContext.Current.Session["userRight"].ToString() == "Super")
                return true;

            var pages = HttpContext.Current.Session["accessPage"];

            if (pages != null && pages.ToString().Contains(pageName + ";"))
                return true;

            return false;
        }


        public bool accessToCreatingUser(string function)
        {
            if (HttpContext.Current.Session["accessTo"] == null)
                pageout();

            if (HttpContext.Current.Session["userRight"].ToString() == "Super")
                return true;

            var functions = HttpContext.Current.Session["accessTo"];

            if (functions != null && functions.ToString().Contains(function + ";"))
                return true;

            return false;
        }




        public bool numberValidation(string num)
        {
            try
            {
                decimal test = Convert.ToDecimal(num);
                return true;
            }
            catch
            {
                return false;
            }
        }





        public string Encrypt(string toEncrypt)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(toEncrypt);
            var password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Encrypted = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(Encrypted);
        }





        public string Decrypt(string toDecrypt)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] DeEncryptedText = Convert.FromBase64String(toDecrypt);
            var password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(DeEncryptedText);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[DeEncryptedText.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }





        public int nextIdPlusOne(string query)
        {
            try
            {
                var dtNextId = sqlOperation.getDataTable(query);
                return (Convert.ToInt32(dtNextId.Rows[0][0].ToString()) + 1);
            }
            catch
            {
                return 1;
            }
        }


        public string getSaleLastID()
        {
            var dtSaleId = sqlOperation.getDataTable("SELECT billNo FROM [SaleInfo] ORDER BY billNo DESC");
            if (dtSaleId.Rows.Count > 0)
                return dtSaleId.Rows[0][0].ToString();
            else
                return "";
        }


        public string nextIdGenerator(string currentId)
        {
            //scriptMessage(objCommonFun.nextIdGenerator(txtCategory.Text));
            var currentCode = new StringBuilder(currentId.Substring(0, 2));
            int currentInt = Convert.ToInt32(currentId.Substring(2, 5));
            ++currentInt;
            if (currentInt == 100000)
            {
                currentInt = 0;
                ++currentCode[1];
                if (currentCode[1] > 'Z')
                {
                    currentCode[1] = 'A';
                    ++currentCode[0];
                    if (currentCode[0] > 'Z')
                    {
                        currentCode[0] = 'A';
                    }
                }
            }
            return currentCode + "" + currentInt.ToString("00000");
        }





        public string nextCusIdGenerator(string currentCusId)
        {
            int currentInt = Convert.ToInt32(currentCusId);
            ++currentInt;
            return currentInt.ToString("0000000");
        }





        public string nextPurchaeCodeGenerator(string currentId)
        {
            int currentInt = Convert.ToInt32(currentId.Substring(1, currentId.Length - 1));
            ++currentInt;
            string Id = "P" + currentInt.ToString("000000");
            return Id;
        }





        public string barcodeGenerator()
        {
            int barcodeInt1 = 0, barcodeInt2 = 0, digit = 0;
            string barcodeStr1 = "", barcodeStr2 = "";
            string digitValue = "";
            string itemValue = findSettingItemValue(3);

            if (itemValue == "0")
                digit = 11;
            else if (itemValue == "1")
                digit = 10;
            else if (itemValue == "2")
                digit = 9;

            var dtBarcodeStandard =
                sqlOperation.getDataTable(
                    "SELECT TOP 1 prodCode FROM [StockInfo] WHERE prodCode LIKE 'ZZ-%[0-9]' AND LEN(prodCode)='" + digit +
                    "' ORDER BY prodCode DESC");
            var dtBarcodeExtend =
                sqlOperation.getDataTable(
                    "SELECT TOP 1 prodCode FROM [StockStatusInfo] WHERE prodCode LIKE 'ZZ-%[0-9]' AND LEN(prodCode)='" +
                    digit + "' ORDER BY prodCode DESC");

            try
            {
                barcodeStr1 = dtBarcodeStandard.Rows[0][0].ToString();
                barcodeInt1 = Convert.ToInt32(barcodeStr1.Remove(0, 3)) + 1;
            }
            catch
            {
                barcodeInt1 = 1;
            }
            try
            {
                barcodeStr2 = dtBarcodeExtend.Rows[0][0].ToString();
                barcodeInt2 = Convert.ToInt32(barcodeStr2.Remove(0, 3)) + 1;
            }
            catch
            {
                barcodeInt2 = 1;
            }

            if (itemValue == "0")
                digitValue = "00000000";
            else if (itemValue == "1")
                digitValue = "0000000";
            else if (itemValue == "2")
                digitValue = "000000";

            if (barcodeInt1 > barcodeInt2)
                return barcodeInt1.ToString(digitValue).Insert(0, "ZZ-"); //11
            else
                return barcodeInt2.ToString(digitValue).Insert(0, "ZZ-"); //11
        }



        public string barcodeGeneratorImport(int preBarcode)
        {
            string digitValue = "00000000";
            return preBarcode.ToString(digitValue).Insert(0, "ZZ-"); //11
        }




        //0 = Supplier, 1 = Staff, 2 = Expense, 3 = Receive, 4 = Bank, 5 = Sales, 6 =avance, 7 = service, 8 = Damage

        public string cashTransaction(decimal cashIn, decimal cashOut, string cashType, string descr, string billNo,
            string mainDescr, string status, string adjust)
        {
            decimal cashInHand = 0;

            query = "INSERT INTO [CashReportInfo] (cashType,            descr,           cashIn,           cashOut,           cashInHand,                    entryDate,                         billNo,           mainDescr,                        roleID,                                                   branchId,                                      groupId,                                    status,         adjust,            storeId         ) VALUES ('" +
                cashType + "', '" + descr + "', '" + cashIn + "', '" + cashOut + "', '" + cashInHand + "', '" +
                GetCurrentTime().ToShortDateString() + "', '" + billNo + "', '" + mainDescr + "', '" +
                HttpContext.Current.Session["roleId"] + "',          '" + HttpContext.Current.Session["branchId"] +
                "', '" + HttpContext.Current.Session["groupId"] + "', '" + status + "', '" + adjust + "','" + HttpContext.Current.Session["storeId"] + "')";

            return sqlOperation.executeQuery(query);
        }


        public string cashTransactionWithDate(decimal cashIn, decimal cashOut, string cashType, string descr, string billNo,
            string mainDescr, string status, string adjust, string entryDate)
        {
            decimal cashInHand = 0;
            if (entryDate == "")
                entryDate = GetCurrentTime().ToShortDateString();

            query = "INSERT INTO [CashReportInfo] (cashType,            descr,           cashIn,           cashOut,           cashInHand,                    entryDate,                         billNo,           mainDescr,                        roleID,                                                   branchId,                                      groupId,                                    status,         adjust ,       storeId        ) VALUES ('" +
                cashType + "', '" + descr + "', '" + cashIn + "', '" + cashOut + "', '" + cashInHand + "', '" +
                entryDate + "', '" + billNo + "', '" + mainDescr + "', '" +
                HttpContext.Current.Session["roleId"] + "',          '" + HttpContext.Current.Session["branchId"] +
                "', '" + HttpContext.Current.Session["groupId"] + "', '" + status + "', '" + adjust + "','" + HttpContext.Current.Session["storeId"] + "')";

            return sqlOperation.executeQuery(query);
        }




        // For Purchase Code
        public string cashTransactionFormPurchase(decimal cashIn, decimal cashOut, string cashType, string descr,
            string billNo, string mainDescr, string status, string adjust, string purchaseCode, string purchaseDate,
            bool isScheduled, bool isReceived, decimal trackAmt)
        {
            decimal cashInHand = 0;
            //ds = sqlOperation.getDataSet("SELECT * FROM [CashReportInfo] ORDER BY ID DESC");

            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    if (Convert.ToDateTime(ds.Tables[0].Rows[0][6].ToString()).ToShortDateString() == GetCurrentTime().ToShortDateString())
            //        cashInHand = Convert.ToDecimal(ds.Tables[0].Rows[0][5].ToString());
            //}
            //cashInHand = cashInHand + cashIn - cashOut;

            if (cashIn == 0 && cashOut == 0 && trackAmt == 0)
                return "";

            query = "INSERT INTO [CashReportInfo] (cashType,            descr,           cashIn,           cashOut,           cashInHand,                    entryDate,                         billNo,           mainDescr,                        roleID,                                                   branchId,                                      groupId,                                    status,         adjust,            purchaseCode,       isScheduled, isReceived, trackAmt, storeId) VALUES ('" +
                cashType + "', '" + descr + "', '" + cashIn + "', '" + cashOut + "', '" + cashInHand + "', '" +
                purchaseDate + "', '" + billNo + "', '" + mainDescr + "', '" + HttpContext.Current.Session["roleId"] +
                "',          '" + HttpContext.Current.Session["branchId"] + "', '" +
                HttpContext.Current.Session["groupId"] + "', '" + status + "', '" + adjust + "', '" + purchaseCode +
                "','" + isScheduled + "','" + isReceived + "', '" + trackAmt + "','" + HttpContext.Current.Session["storeId"] + "')";


            return sqlOperation.executeQuery(query);
        }





        public string cashTransactionSales(decimal cashIn, decimal cashOut, string cashType, string descr, string billNo,
            string mainDescr, string status, string adjust, string dateTime)
        {
            decimal cashInHand = 0;

            query =
                "INSERT INTO [CashReportInfo] (cashType,            descr,           cashIn,           cashOut,           cashInHand,                    entryDate,                         billNo,           mainDescr,                        roleID,                                                   branchId,                                      groupId,                                    status,         adjust, storeId,payMethod) VALUES ('" +
                cashType + "', '" + descr + "', '" + cashIn + "', '" + cashOut + "', '" + cashInHand + "', '" + dateTime +
                "', '" + billNo + "', '" + mainDescr + "', '" + HttpContext.Current.Session["roleId"] + "',          '" +
                HttpContext.Current.Session["branchId"] + "', '" + HttpContext.Current.Session["groupId"] + "', '" +
                status + "', '" + adjust + "','" + HttpContext.Current.Session["storeId"] + "','0')";

            return sqlOperation.executeQuery(query);
        }

        public string cashTransactionSalesData(decimal cashIn, decimal cashOut, string cashType, string payMethod, string billNo,
            string mainDescr, string status, string adjust, string dateTime, string payType, string payDescr)
        {
            decimal cashInHand = 0;

            query = "INSERT INTO [CashReportInfo] (cashType,descr,cashIn,cashOut,cashInHand,entryDate,billNo,mainDescr,roleID,branchId,groupId,status,adjust, storeId,payMethod,payType,payDescr) VALUES ('" +
                                                    cashType + "', '" + payMethod + "', '" + cashIn + "', '" + cashOut + "', '" + cashInHand + "', '" + dateTime +
                "', '" + billNo + "', '" + mainDescr + "', '" + HttpContext.Current.Session["roleId"] + "',          '" +
                HttpContext.Current.Session["branchId"] + "', '" + HttpContext.Current.Session["groupId"] + "', '" +
                status + "', '" + adjust + "','" + HttpContext.Current.Session["storeId"] + "','" + payMethod + "','" + payType + "','" + payDescr + "')";

            return query;
        }





        public void cashDueAndBalanceValue(string supCompany)
        {
            try
            {
                var dtSupplier =
                    sqlOperation.getDataTable("SELECT TOP 1 cashDue, balance FROM [SupplierStatus] WHERE supCompany = '" +
                                      supCompany + "' ORDER BY id DESC");
                cashDue = Convert.ToDecimal(dtSupplier.Rows[0][0].ToString());
                balance = Convert.ToDecimal(dtSupplier.Rows[0][1].ToString());
            }
            catch
            {
                cashDue = 0;
                balance = 0;
            }
        }





        public string findSettingItemValue(int index)
        {
            try
            {
                string adminId = HttpContext.Current.Session["roleId"].ToString();
                if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                {
                    adminId = HttpContext.Current.Session["branchId"].ToString();
                }

                var dsPrint = sqlOperation.getDataSet("SELECT * FROM [SettingInfo] WHERE id='" + adminId + "' ");

                string itemVaue = "";
                if (dsPrint.Tables[0].Rows.Count > 0)
                {
                    itemVaue = dsPrint.Tables[0].Rows[0][index].ToString();
                }

                return itemVaue;
            }
            catch (Exception)
            {
                return "";
            }
        }





        public string findSettingItemValueDataTable(string columnName)
        {
            string itemVaue = "";
            try
            {
                
                string adminId = HttpContext.Current.Session["roleId"].ToString();

                if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                {
                    adminId = HttpContext.Current.Session["branchId"].ToString();
                }

                DataTable dtPrint = sqlOperation.getDataTable("SELECT * FROM [SettingInfo] WHERE id='" + adminId + "' ");

                if (dtPrint.Rows.Count > 0)
                {
                    itemVaue = dtPrint.Rows[0][columnName].ToString().Trim();
                }

                
            }
            catch (Exception)
            {
                pageout();
            }
            return itemVaue;
        }





        public DataTable LoadProductDetail(string prouctId, string storeId)
        {
            var dtTemp = new DataTable();

            try
            {
                var msg = checkProductQtyManagement(prouctId, storeId);
                if (!msg)
                {
                    return new DataTable();
                }

                //string queryStock = "SELECT *," + getQtyQueryStock("tbl", storeId, "inquery") + " FROM StockInfo AS tbl LEFT JOIN Ecommerce AS ecom ON tbl.prodCode = ecom.prodCode WHERE tbl.active ='1' AND (tbl.prodId = '" + prouctId + "')" + getUserAccessParameters("tbl") + " ";
                string queryStock = "SELECT * FROM StockInfo AS tbl LEFT JOIN QtyManagement as qm ON qm.productId = tbl.prodId LEFT JOIN Ecommerce AS ecom ON tbl.prodCode = ecom.prodCode WHERE tbl.active ='1' AND qm.storeId='" + storeId + "' AND (tbl.prodId = '" + prouctId + "')" + getUserAccessParameters("tbl") + " ";
                dtTemp = sqlOperation.getDataTable(queryStock);

                if (dtTemp.Rows.Count == 0)
                {
                    dtTemp = sqlOperation.getDataTable("SELECT * FROM StockInfo as stock LEFT JOIN QtyManagement as qm ON qm.productId = stock.prodId WHERE stock.active ='1' AND (stock.prodId = '" + prouctId + "') " + getUserAccessParameters("stock") + " ");
                }

                if (dtTemp.Rows.Count == 0)
                {
                    dtTemp = sqlOperation.getDataTable("SELECT packageName, prodCode, Id FROM PackageInfo WHERE (Id = '" + prouctId + "') " + HttpContext.Current.Session["userAccessParameters"] + " ");
                    if (dtTemp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                        {
                            dtTemp.Rows[i][0] = "Package";
                        }
                    }
                }

                return dtTemp;
            }
            catch (Exception)
            {
                return dtTemp;
            }
        }







        public DataSet LoadProductDetails(string code)
        {
            var dsTemp = new DataSet();

            try
            {
                string[] splitText = new string[] { };
                string finalText = code;

                if (!isProdCode)
                {
                    int i = 0;
                    while ((i = code.IndexOf('[', i)) != -1)
                    {
                        string codeWithBrakets = code.Substring(i).Trim();
                        finalText = codeWithBrakets.Substring(1, codeWithBrakets.Length - 2);
                        finalText = finalText.Trim();
                        i++;
                    }
                }

                // Using eCommerce part
                string tmpQuery = "SELECT tbl.prodId,tbl.prodCode,tbl.prodName FROM StockInfo AS tbl LEFT JOIN Ecommerce AS ecom ON tbl.prodCode = ecom.prodCode WHERE (tbl.prodId = '" + finalText + "')" + getUserAccessParameters("tbl") + "";
                dsTemp = sqlOperation.getDataSet(tmpQuery);
                accessProduct = false;
                StockBulkOpt.accessProduct = false;

                if (dsTemp.Tables[0].Rows.Count == 0)
                {
                    dsTemp =
                        sqlOperation.getDataSet("SELECT prodId,prodCode,prodName FROM StockInfo WHERE (prodId = '" + finalText + "') " +
                                          HttpContext.Current.Session["userAccessParameters"]);
                    accessProduct = true;
                    StockBulkOpt.accessProduct = true;
                }

                if (dsTemp.Tables[0].Rows.Count == 0)
                {
                    dsTemp = sqlOperation.getDataSet("SELECT Id, prodCode,packageName FROM PackageInfo WHERE (Id = '" +
                        finalText + "')" + HttpContext.Current.Session["userAccessParameters"]);

                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                        {
                            dsTemp.Tables[0].Rows[i][0] = "Package";
                        }
                    }
                }

                return dsTemp;
            }
            catch (Exception)
            {
                return dsTemp;
            }
        }





        public bool isAdancedCheck(decimal grossAmt, decimal payCash, decimal paidAmt)
        {
            if (grossAmt < (payCash + paidAmt) || grossAmt == 0)
                return false;

            return true;
        }





        public bool IsEmpty(string value)
        {
            if (value.Length > 0)
                return false;
            else
                return true;
        }





        public string ConvertToUnicode(object number)
        {
            string bengali_text, output;
            bengali_text = string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
            output = bengali_text.Replace("৤", ".").Replace("৷", "A").Replace("Cash", "ক্যাশ").Replace("ৣ", "-");
            return output;
        }





        public dynamic DisplayPercentange(string printBillNo)
        {
            string percentance = "";
            query = "SELECT * FROM SaleInfo WHERE billNo = '" + printBillNo + "'";
            var dtSale = sqlOperation.getDataTable(query);
            if (dtSale.Rows.Count > 0)
            {
                string dicType = dtSale.Rows[0][18].ToString();
                decimal disAmt = Convert.ToDecimal(dtSale.Rows[0][7]);
                decimal netAmt = Convert.ToDecimal(dtSale.Rows[0][6]);
                if (dicType == "%")
                {
                    percentance = "(" + ((disAmt * 100) / netAmt).ToString() + "%" + ")";
                }
                else if (disAmt.ToString() != "" && dicType != "%")
                {
                    percentance = "";
                    //percentance = disAmt.ToString();
                }
            }
            return percentance;
        }





        // Prevoius billNo display 
        public DataSet prevoiusBillNo(string billNo)
        {
            var dtSlip =
                sqlOperation.getDataSet("SELECT Id,billNo,payCash,entryDate FROM slipInfo WHERE billNo = '" + billNo +
                                  "' ORDER BY Id ASC");
            return dtSlip;
        }





        public string SupplierID(string supplierName)
        {
            var dtSupplier = sqlOperation.getDataTable("SELECT * FROM SupplierInfo WHERE supCompany = '" + supplierName + "'");

            return dtSupplier.Rows[0][0].ToString();
        }





        // Delete tmpUserInfo 
        private void deleteTempSaleInfo(string tmpBillNo)
        {
            sqlOperation.executeQuery("DELETE FROM [TempSaleInfo] WHERE billNo='" + tmpBillNo + "'");
        }





        public string getProductId(string code, bool isProdCode)
        {
            string productId = "";
            if (isProdCode)
            {
                string[] splitText = new string[] { };
                string finalText = code;

                if (code.Contains('['))
                {
                    splitText = code.Split(new string[] { " ( " }, StringSplitOptions.None);
                    finalText = splitText[1].Substring(0, splitText[1].Length - 1);
                }

                code = finalText;
            }

            DataSet dsProd = sqlOperation.getDataSet("SELECT prodID FROM StockInfo WHERE prodCode = '" + code + "'");
            if (dsProd.Tables[0].Rows.Count > 0)
                productId = dsProd.Tables[0].Rows[0][0].ToString();

            return productId;
        }





        public string serializeDatatableToJson(DataTable dt)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var rows = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }


        public string serializeDictionayToJson(Dictionary<string, string> dic)
        {
            var serializer = new JavaScriptSerializer();
            var rows = new List<Dictionary<string, string>>();

            foreach (KeyValuePair<string, string> entry in dic)
            {
                var row = new Dictionary<string, string>();

                row.Add(entry.Key, entry.Value);

                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }


        public JObject deSerializeJsonToObject(string jsonData)
        {
            jsonData = jsonData.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
            if (jsonData == "")
                return JObject.Parse(null);
            else
                return JObject.Parse(jsonData);

        }




        public string formatWhereInQuery(string jsonValues)
        {
            var splitValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonValues);

            var formatWhere = "";

            foreach (var item in splitValues)
            {
                formatWhere += item.Key + "='" + item.Value + "' AND ";
            }

            return formatWhere.Substring(0, formatWhere.Length - 4);
        }





        public string formatSetInQuery(string jsonValues)
        {
            var splitValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonValues);

            var formatWhere = "";

            foreach (var item in splitValues)
            {
                formatWhere += item.Key + "='" + item.Value + "',";
            }

            return formatWhere.TrimEnd(',');
        }





        public string formatValuesInQuery(JToken arrValues)
        {
            var formatValues = "";

            foreach (string item in arrValues)
            {
                formatValues += "'" + item + "',";
            }

            return formatValues.TrimEnd(',');
        }





        public string getUserAccessParameters(string tblName)
        {
            // Get User Access Parameters
            var userAccessParameters = "";
            DataSet dsUserAccessParameters = sqlOperation.getDataSet("SELECT * FROM RoleInfo");

            if (HttpContext.Current.Session["userRight"].ToString() == "Group")
            {
                dsUserAccessParameters =
                    sqlOperation.getDataSet("SELECT * FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["roleId"] +
                                      "' OR groupId ='" + HttpContext.Current.Session["roleId"] + "' ");
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
            {
                dsUserAccessParameters =
                    sqlOperation.getDataSet("SELECT * FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["roleId"] +
                                      "' OR branchId ='" + HttpContext.Current.Session["roleId"] + "' ");
            }
            else if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
            {
                dsUserAccessParameters =
                    sqlOperation.getDataSet("SELECT * FROM RoleInfo WHERE roleId='" + HttpContext.Current.Session["branchId"] +
                                      "' OR branchId ='" + HttpContext.Current.Session["branchId"] + "' ");
            }

            for (int count = 0; count < dsUserAccessParameters.Tables[0].Rows.Count; count++)
            {
                if (count == 0)
                    userAccessParameters = " AND ( ";

                if (count == dsUserAccessParameters.Tables[0].Rows.Count - 1)
                    userAccessParameters += tblName + ".roleId = '" + dsUserAccessParameters.Tables[0].Rows[count][0] +
                                            "' ) ";
                else
                    userAccessParameters += tblName + ".roleId = '" + dsUserAccessParameters.Tables[0].Rows[count][0] +
                                            "' OR ";
            }

            return userAccessParameters;
        }





        public string getStoreAccessParameters(string tblName)
        {
            // Get User Access Parameters
            string userAccessParameters = "";
            string roleSession = HttpContext.Current.Session["roleId"].ToString();


            var dtRole =
                sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleSession + "' OR branchId='" +
                                    roleSession + "' ");

            string storeWhere = "";
            if (dtRole.Rows.Count > 0)
            {
                storeWhere = " WHERE ( ";
                for (int i = 0; i < dtRole.Rows.Count; i++)
                {
                    storeWhere += "roleId='" + dtRole.Rows[i]["roleId"] + "'";

                    if (i != dtRole.Rows.Count - 1)
                        storeWhere += " OR ";

                }
                storeWhere += ")";

            }

            // var dtRoleSotreAccess = sqlOperation.getDataTable("SELECT storeId FROM RoleInfo WHERE roleId='" + roleSession + "' OR branchId='" + roleSession + "'");

            var dtRoleSotreAccess = sqlOperation.getDataTable("SELECT Id FROM warehouseInfo " + storeWhere + "");

            for (int i = 0; i < dtRoleSotreAccess.Rows.Count; i++)
            {
                if (i == 0)
                    userAccessParameters += " AND (" + tblName + ".storeId ='" + dtRoleSotreAccess.Rows[i]["Id"] + "'";
                else
                    userAccessParameters += " OR " + tblName + ".storeId ='" + dtRoleSotreAccess.Rows[i]["Id"] + "'";

                if (i == dtRoleSotreAccess.Rows.Count - 1)
                    userAccessParameters += " OR " + tblName + ".storeId IS NULL)";
            }


            return userAccessParameters;
        }




        public string getStoreAccessParametersWithSelectedValue(string tblName, string selectedStore)
        {

            if (selectedStore != "0")
                return " AND " + tblName + ".storeId='" + selectedStore + "'";


            // Get User Access Parameters
            string userAccessParameters = "";
            string roleSession = HttpContext.Current.Session["roleId"].ToString();

            var dtRole =
                sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleSession + "' OR branchId='" +
                                    roleSession + "' ");

            string storeWhere = "";
            if (dtRole.Rows.Count > 0)
            {
                storeWhere = " WHERE ( ";
                for (int i = 0; i < dtRole.Rows.Count; i++)
                {
                    storeWhere += "roleId='" + dtRole.Rows[i]["roleId"] + "'";

                    if (i != dtRole.Rows.Count - 1)
                        storeWhere += " OR ";

                }
                storeWhere += ")";

            }


            // var dtRoleSotreAccess = sqlOperation.getDataTable("SELECT storeId FROM RoleInfo WHERE roleId='" + roleSession + "' OR branchId='" + roleSession + "'");

            var dtRoleSotreAccess = sqlOperation.getDataTable("SELECT Id FROM warehouseInfo " + storeWhere + "");

            for (int i = 0; i < dtRoleSotreAccess.Rows.Count; i++)
            {
                if (i == 0)
                    userAccessParameters += " AND (" + tblName + ".storeId ='" + dtRoleSotreAccess.Rows[i]["Id"] + "'";
                else
                    userAccessParameters += " OR " + tblName + ".storeId ='" + dtRoleSotreAccess.Rows[i]["Id"] + "'";

                if (i == dtRoleSotreAccess.Rows.Count - 1)
                    userAccessParameters += " OR " + tblName + ".storeId IS NULL)";
            }


            return userAccessParameters;
        }






        public string calculateQty(string prodId, string inputTotalQty, string dbSoldTotalQty, string sign, string dbReturnTotalQty)
        {
            var stockModel = new StockModel();

            int dbRatio = 1;
            var dtStock = stockModel.getProductRatioModelByProductId(prodId);
            if (dtStock.Rows.Count > 0)
                dbRatio = Convert.ToInt32(dtStock.Rows[0]["unitRatio"]);
            else
                dbRatio = 1;

            dtStock = stockModel.getStockDataListModelByProdID(prodId);
            // stock Qty and Pieces split
            string dbStockTotalQty = getLastStockQty(prodId, HttpContext.Current.Session["storeId"].ToString());
            bool containsStockQty = dbStockTotalQty.Contains(".");
            int dbStockQty = 0, dbStockPiece = 0;
            if (containsStockQty)
            {
                string[] splitDbQty = dbStockTotalQty.Split('.');
                if (splitDbQty.Length > 1)
                {
                    dbStockQty = Convert.ToInt32(splitDbQty[0]);
                    dbStockPiece = Convert.ToInt32(splitDbQty[1]);
                }
            }
            else
            {
                dbStockQty = Convert.ToInt32(dbStockTotalQty);
            }

            // Sold qty and price
            bool containsSoldQty = dbSoldTotalQty.Contains(".");
            int dbSoldQty = 0, dbSoldPiece = 0;
            if (containsSoldQty)
            {
                string[] splitDbSoldQty = dbSoldTotalQty.Split('.');
                if (splitDbSoldQty.Length > 1)
                {
                    dbSoldQty = Convert.ToInt32(splitDbSoldQty[0]);
                    dbSoldPiece = Convert.ToInt32(splitDbSoldQty[1]);
                }
            }
            else
            {
                dbSoldQty = Convert.ToInt32(dbSoldTotalQty);
            }
            int totalQty = dbStockQty + dbSoldQty;
            int totalPiece = dbStockPiece + dbSoldPiece;



            // Return Qty 
            bool containsReturnQty = dbReturnTotalQty.Contains(".");
            int dbReturnQty = 0, dbReturnPiece = 0;
            if (containsReturnQty)
            {
                string[] splitDbReturnQty = dbReturnTotalQty.Split('.');
                if (splitDbReturnQty.Length > 1)
                {
                    dbReturnQty = Convert.ToInt32(splitDbReturnQty[0]);
                    dbReturnPiece = Convert.ToInt32(splitDbReturnQty[1]);
                }
            }
            else
            {
                dbReturnQty = Convert.ToInt32(dbReturnTotalQty);
                dbReturnPiece = 0;
            }

            //int qtyAfterReturn = dbSoldQty - dbReturnQty;
            //int pieceAfterReturn = dbSoldPiece - dbReturnPiece;

            totalQty = totalQty - dbReturnQty;
            totalPiece = totalPiece - dbReturnPiece;



            // Input qty and piece split
            int inputQty = 0, inputPiece = 0;
            bool containsInput = inputTotalQty.Contains(".");
            if (containsInput)
            {
                string[] splitQty = inputTotalQty.Split('.');
                if (splitQty.Length > 1)
                {
                    inputQty = Convert.ToInt32(splitQty[0]);
                    inputPiece = Convert.ToInt32(splitQty[1]);
                }
            }
            else
            {
                inputQty = Convert.ToInt32(inputTotalQty);
            }

            // Check total piece and ratio
            if (totalPiece > dbRatio)
            {
                totalPiece = totalPiece - dbRatio;
                totalQty = totalQty + 1;
            }


            // Check input and total piece
            if (inputPiece > totalPiece)
            {
                totalQty -= 1;
                totalPiece = (dbRatio + totalPiece) - inputPiece;
            }
            else
            {
                totalPiece = totalPiece - inputPiece;
            }

            if (sign == "+")
            {
                totalQty = totalQty + inputQty;
            }
            else
            {
                totalQty = totalQty - inputQty;
            }

            if (totalQty <= 0)
                totalQty = 0;
            if (totalPiece <= 0)
                totalPiece = 0;

            if (dbRatio > 1)
                return totalQty + "." + totalPiece;
            else
                return totalQty.ToString();
        }





        public string calculateQty(string prodId, string baseQty, string additionQty, string sign)
        {
            var stockModel = new StockModel();

            int dbRatio = 1;
            var dtStock = stockModel.getProductRatioModelByProductId(prodId);
            if (dtStock.Rows.Count > 0)
                dbRatio = Convert.ToInt32(dtStock.Rows[0]["unitRatio"]);
            else
                dbRatio = 1;

            // stock Qty and Pieces split
            bool containsBaseQty = baseQty.Contains(".");
            int dbBaseQty = 0, dbBasePiece = 0;
            if (containsBaseQty)
            {
                string[] splitDbQty = baseQty.Split('.');
                if (splitDbQty.Length > 1)
                {
                    dbBaseQty = Convert.ToInt32(splitDbQty[0]);
                    dbBasePiece = Convert.ToInt32(splitDbQty[1]);
                }
            }
            else
            {
                dbBaseQty = Convert.ToInt32(baseQty);
            }

            // Sold qty and price
            bool containsAdditionQty = additionQty.Contains(".");
            int dbAdditionQty = 0, dbAdditionPiece = 0;
            if (containsAdditionQty)
            {
                string[] splitDbSoldQty = additionQty.Split('.');
                if (splitDbSoldQty.Length > 1)
                {
                    dbAdditionQty = Convert.ToInt32(splitDbSoldQty[0]);
                    dbAdditionPiece = Convert.ToInt32(splitDbSoldQty[1]);
                }
            }
            else
            {
                dbAdditionQty = Convert.ToInt32(additionQty);
            }


            var totalQty = 0;
            var totalPiece = 0;

            if (sign == "+")
            {
                totalQty = dbBaseQty + dbAdditionQty;

                // Piece
                totalPiece = dbBasePiece + dbAdditionPiece;
                if (totalPiece > dbRatio)
                {
                    totalPiece = totalPiece - dbRatio;
                    totalQty = totalQty + 1;
                }
            }
            else
            {
                totalQty = dbBaseQty - dbAdditionQty;

                if (dbBasePiece > dbAdditionPiece)
                {
                    totalPiece = dbBasePiece - dbAdditionPiece;
                }
                else if (dbBasePiece < dbAdditionPiece)
                {
                    totalQty -= 1;
                    dbBasePiece += dbRatio;
                    totalPiece = dbBasePiece - dbAdditionPiece;
                }
            }

            if (totalQty <= 0)
                totalQty = 0;
            if (totalPiece <= 0)
                totalPiece = 0;

            return totalQty + "." + totalPiece;
        }


        public string nextServiceId()
        {
            CommonFunction objCommonFun = new CommonFunction();
            SqlOperation sqlOperation = new SqlOperation();

            string nextBillNoRequire = "";
            DataSet ds = sqlOperation.getDataSet("SELECT serviceId FROM [ServicingInfo] ORDER BY Id DESC");

            try
            {
                nextBillNoRequire = objCommonFun.nextIdGenerator(ds.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                nextBillNoRequire = "SS00001";
            }

            return nextBillNoRequire;
        }



        public void CheckBarcodeImageCreated(string prodCode)
        {
            // Check barcode image create or not
            if (!File.Exists(HttpContext.Current.Server.MapPath("BarcodeTool/images/" + prodCode + ".png")))
            {
                Process process = Process.Start(HttpContext.Current.Server.MapPath("BarcodeTool/BarCodeGenerate.exe"));
                if (process != null) process.WaitForExit();
            }
        }



        public string getPackageQty(string code)
        {
            string[] splitText = new string[] { };
            string prodCodes = "";
            string packageQty = "0";

            var conditionExtension = "";
            splitText = code.Split(';');
            if (splitText.Length > 0)
            {
                int arrayCount = splitText.Length - 1;

                for (int i = 0; i < arrayCount; i++)
                {
                    prodCodes += "CAST(prodId as nvarchar) = " + "'" + splitText[i] + "'";

                    if (i != arrayCount - 1)
                        prodCodes += " OR ";
                }

                conditionExtension = " WHERE " + prodCodes;
            }

            query = @"SELECT (Select  (
                            (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(qty as decimal) END),0) +
                            COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(qty as decimal) END),0) +
                            COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(qty as decimal) END),0)) -
                            (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(qty as decimal) END),0) +
                            COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(qty as decimal) END),0)+
                            COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(qty as decimal) END),0) +
                            COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(qty as decimal) END),0) +
                            COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(qty as decimal) END),0))
                    ) AS qty
                    FROM StockStatusInfo as stockstatus
                    WHERE stockstatus.prodId = stock.prodID AND stockstatus.storeId='" + HttpContext.Current.Session["storeId"] + "') as qty  FROM StockInfo as stock  " + conditionExtension;
            var dtStockQty = sqlOperation.getDataTable(query);

            if (dtStockQty.Rows.Count > 0 && dtStockQty.Rows[0][0].ToString() != "")
            {
                //packageQty = dsQtyInfo.Tables[0].Rows[0][0].ToString();
                decimal packStoreQty = Convert.ToDecimal(dtStockQty.Rows[0][0].ToString());
                for (int i = 0; i < dtStockQty.Rows.Count; i++)
                {
                    if (packStoreQty > Convert.ToDecimal(dtStockQty.Rows[i][0].ToString()))
                    {
                        packStoreQty = Convert.ToDecimal(dtStockQty.Rows[i][0].ToString());
                    }
                }

                packageQty = packStoreQty.ToString();
            }

            else
                packageQty = "0";

            return packageQty;
        }


        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero".ToUpper();

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number)).ToUpper();

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.ToUpper();
        }





        public string getBranchID(string roleId)
        {
            DataTable dtRoleInfo = sqlOperation.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + roleId + "'");
            if (dtRoleInfo.Rows.Count > 0)
            {
                string userRight = dtRoleInfo.Rows[0]["userRight"].ToString();
                if (userRight == "Group")
                {
                    return dtRoleInfo.Rows[0]["branchId"].ToString();
                }
                else if (userRight == "Branch")
                {
                    return dtRoleInfo.Rows[0]["roleId"].ToString();
                }
                else if (userRight == "Regular")
                {
                    return dtRoleInfo.Rows[0]["branchId"].ToString();
                }
                else
                {
                    return dtRoleInfo.Rows[0]["roleID"].ToString();
                }

            }
            return "";
        }





        public string getCurrentStockQty(string prodId)
        {
            decimal stocktotal = 0,
                saletotal = 0,
                saleReturnTotal = 0,
                stockReturnTotal = 0, damageTotal = 0, cancelTotal = 0
                ;

            var stockModel = new StockModel();
            var dtStock = stockModel.getStockQtyModel(prodId, "stock");
            if (dtStock.Rows.Count > 0 && dtStock.Rows[0]["qty"].ToString() != "")
                stocktotal = Convert.ToDecimal(dtStock.Rows[0]["qty"].ToString());

            var dtStockReturn = stockModel.getStockQtyModel(prodId, "stockReturn");
            if (dtStockReturn.Rows.Count > 0 && dtStockReturn.Rows[0]["qty"].ToString() != "")
                stockReturnTotal = Convert.ToDecimal(dtStockReturn.Rows[0]["qty"].ToString());

            var dtSale = stockModel.getStockQtyModel(prodId, "sale");
            if (dtSale.Rows.Count > 0 && dtSale.Rows[0]["qty"].ToString() != "")
                saletotal = Convert.ToDecimal(dtSale.Rows[0]["qty"].ToString());

            var dtSaleReturn = stockModel.getStockQtyModel(prodId, "saleReturn");
            if (dtSaleReturn.Rows.Count > 0 && dtSaleReturn.Rows[0]["qty"].ToString() != "")
                saleReturnTotal = Convert.ToDecimal(dtSaleReturn.Rows[0]["qty"].ToString());

            var dtDamage = stockModel.getStockQtyModel(prodId, "damage");
            if (dtDamage.Rows.Count > 0 && dtDamage.Rows[0]["qty"].ToString() != "")
                damageTotal = Convert.ToDecimal(dtDamage.Rows[0]["qty"].ToString());


            var dtCancel = stockModel.getStockQtyModel(prodId, "cancel");
            if (dtCancel.Rows.Count > 0 && dtCancel.Rows[0]["qty"].ToString() != "")
                cancelTotal = Convert.ToDecimal(dtCancel.Rows[0]["qty"].ToString());


            return ((stocktotal + saleReturnTotal) - (saletotal + stockReturnTotal + damageTotal + cancelTotal)).ToString();

        }



        public string getCurrentStockQtyForStoreID(string prodId, string storeId)
        {
            decimal stocktotal = 0,
                saletotal = 0,
                saleReturnTotal = 0,
                stockReturnTotal = 0, damageTotal = 0, cancelTotal = 0
                ;

            var stockModel = new StockModel();
            var dtStock = stockModel.getStockQtyModel(prodId, "stock", storeId);
            if (dtStock.Rows.Count > 0 && dtStock.Rows[0]["qty"].ToString() != "")
                stocktotal = Convert.ToDecimal(dtStock.Rows[0]["qty"].ToString());

            var dtStockReturn = stockModel.getStockQtyModel(prodId, "stockReturn", storeId);
            if (dtStockReturn.Rows.Count > 0 && dtStockReturn.Rows[0]["qty"].ToString() != "")
                stockReturnTotal = Convert.ToDecimal(dtStockReturn.Rows[0]["qty"].ToString());

            var dtSale = stockModel.getStockQtyModel(prodId, "sale", storeId);
            if (dtSale.Rows.Count > 0 && dtSale.Rows[0]["qty"].ToString() != "")
                saletotal = Convert.ToDecimal(dtSale.Rows[0]["qty"].ToString());

            var dtSaleReturn = stockModel.getStockQtyModel(prodId, "saleReturn", storeId);
            if (dtSaleReturn.Rows.Count > 0 && dtSaleReturn.Rows[0]["qty"].ToString() != "")
                saleReturnTotal = Convert.ToDecimal(dtSaleReturn.Rows[0]["qty"].ToString());

            var dtDamage = stockModel.getStockQtyModel(prodId, "damage", storeId);
            if (dtDamage.Rows.Count > 0 && dtDamage.Rows[0]["qty"].ToString() != "")
                damageTotal = Convert.ToDecimal(dtDamage.Rows[0]["qty"].ToString());


            var dtCancel = stockModel.getStockQtyModel(prodId, "cancel", storeId);
            if (dtCancel.Rows.Count > 0 && dtCancel.Rows[0]["qty"].ToString() != "")
                cancelTotal = Convert.ToDecimal(dtCancel.Rows[0]["qty"].ToString());


            return ((stocktotal + saleReturnTotal) - (saletotal + stockReturnTotal + damageTotal + cancelTotal)).ToString();

        }




        public string getCurrentStockQtyWihStoreID(string prodId, string storeId)
        {
            decimal stocktotal = 0,
                saletotal = 0,
                saleReturnTotal = 0,
                stockReturnTotal = 0, damageTotal = 0, cancelTotal = 0
                ;

            var stockModel = new StockModel();
            var dtStock = stockModel.getStockQtyModel(prodId, "stock", storeId);
            if (dtStock.Rows.Count > 0 && dtStock.Rows[0]["qty"].ToString() != "")
                stocktotal = Convert.ToDecimal(dtStock.Rows[0]["qty"].ToString());

            var dtStockReturn = stockModel.getStockQtyModel(prodId, "stockReturn", storeId);
            if (dtStockReturn.Rows.Count > 0 && dtStockReturn.Rows[0]["qty"].ToString() != "")
                stockReturnTotal = Convert.ToDecimal(dtStockReturn.Rows[0]["qty"].ToString());

            var dtSale = stockModel.getStockQtyModel(prodId, "sale", storeId);
            if (dtSale.Rows.Count > 0 && dtSale.Rows[0]["qty"].ToString() != "")
                saletotal = Convert.ToDecimal(dtSale.Rows[0]["qty"].ToString());

            var dtSaleReturn = stockModel.getStockQtyModel(prodId, "saleReturn", storeId);
            if (dtSaleReturn.Rows.Count > 0 && dtSaleReturn.Rows[0]["qty"].ToString() != "")
                saleReturnTotal = Convert.ToDecimal(dtSaleReturn.Rows[0]["qty"].ToString());

            var dtDamage = stockModel.getStockQtyModel(prodId, "damage", storeId);
            if (dtDamage.Rows.Count > 0 && dtDamage.Rows[0]["qty"].ToString() != "")
                damageTotal = Convert.ToDecimal(dtDamage.Rows[0]["qty"].ToString());


            var dtCancel = stockModel.getStockQtyModel(prodId, "cancel", storeId);
            if (dtCancel.Rows.Count > 0 && dtCancel.Rows[0]["qty"].ToString() != "")
                cancelTotal = Convert.ToDecimal(dtCancel.Rows[0]["qty"].ToString());


            return ((stocktotal + saleReturnTotal) - (saletotal + stockReturnTotal + damageTotal + cancelTotal)).ToString();

        }

        public void getUserListByStore(DropDownList ddlUserList, DropDownList ddlStoreList)
        {
            fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE active='1' AND (storeId='" + ddlStoreList.SelectedValue + "' OR '" + ddlStoreList.SelectedValue + "'='0') AND storeId !='0'", "title", "roleId");
            ddlUserList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Search All User", "0"));
        }





        public string getQtyQueryStock(string status, string storeId, string type)
        {
            string conditionalState = "";
            if (type == "inquery")
                conditionalState = " AS stockQty";

            return @"((Select  (
                                (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(qty as decimal(10,2)) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(qty as decimal(10,2)) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(qty as decimal(10,2)) END),0)) -
                                (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(qty as decimal(10,2)) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(qty as decimal(10,2)) END),0)+
                                COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(qty as decimal(10,2)) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(qty as decimal(10,2)) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(qty as decimal(10,2)) END),0))
                        ) AS qty
                        FROM StockStatusInfo as stockstatus
                        WHERE stockstatus.prodId = " + status + ".prodID AND stockstatus.storeId='" +
                   storeId + "' )) " + conditionalState;

        }



        public string getQtyQueryStockAllStore(string status)
        {
            return @"((Select  (
                                (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(qty as decimal) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(qty as decimal) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(qty as decimal) END),0)) -
                                (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(qty as decimal) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(qty as decimal) END),0)+
                                COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(qty as decimal) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(qty as decimal) END),0) +
                                COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(qty as decimal) END),0))
                        ) AS qty
                        FROM StockStatusInfo as stockstatus
                        WHERE stockstatus.prodId = " + status + ".prodID)) AS qty ";

        }





        public string createBranchInfo(bool isWarehouseOrderBy, string storeId)
        {
            var newBranchId = 1;
            var branchModel = new BranchModel();
            var dtBranchInfo = branchModel.getBranchInfoOrderByDesc();
            if (dtBranchInfo.Rows.Count > 0)
            {
                newBranchId = Convert.ToInt32(dtBranchInfo.Rows[0]["Id"].ToString());
                newBranchId++;
            }

            // get last warehouseinfo
            var warehouseId = "";
            if (!isWarehouseOrderBy)
            {
                var warehouseModel = new WarehouseModel();
                var dtWarehouse = warehouseModel.getWarehouseListOderByDesc();
                if (dtWarehouse.Rows.Count > 0)
                    warehouseId = dtWarehouse.Rows[0]["Id"].ToString();
            }
            else
            {
                warehouseId = storeId;
            }

            if (warehouseId == "")
                return "fail";

            branchModel.Id = newBranchId.ToString();
            branchModel.warehouseId = warehouseId;
            return branchModel.insertBranchInfo();
        }


        public string getBranchAllUserRoleId(string roleId, string branchId)
        {
            // Branch all user roleid
            var branchAllUserRoleId = "";

            var roleModel = new RoleModel();

            var dtRole = roleModel.getBranchAllUserRoleIdModel(roleId, branchId);
            for (int k = 0; k < dtRole.Rows.Count; k++)
            {
                if (k == 0)
                    branchAllUserRoleId += " AND(";

                branchAllUserRoleId += " roleId='" + dtRole.Rows[k]["roleId"] + "'";

                if (k != dtRole.Rows.Count - 1)
                    branchAllUserRoleId += " OR ";

                if (k == dtRole.Rows.Count - 1)
                    branchAllUserRoleId += ")";
            }

            return branchAllUserRoleId;
        }



        public bool productExists(string productName)
        {
            var stockModel = new StockModel();
            var dtStock = stockModel.getProductListByProdNameModel(productName);
            if (dtStock.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool prodCodeExists(string _prodCode)
        {
            var stockModel = new StockModel();
            var dtStock = stockModel.getProductListByProdCodeModel(_prodCode);
            if (dtStock.Rows.Count > 0)
                return true;
            else
                return false;
        }





        public string getIMEIStoreWise(string storeId, string prodID)
        {
            string productId = prodID.Length > 5 ? prodID.Substring(0, 5) : prodID;

            string proCode = prodID;

            string totalStock = "", saleReturn = "", stockRecive = "", sale = "", stockTransfer = "", stockReturn = "", damage = "", cancel = "", stockRemove = "";
            string queryIMEI = @"SELECT 
                                CASE WHEN status = 'stock' THEN imei END stockImei,
                                CASE WHEN status = 'stockTransfer' THEN imei END stockTransferImei,
                                CASE WHEN status = 'stockReceive' THEN imei END stockReceiveImei,
                                CASE WHEN status = 'sale' THEN imei END saleImei,
                                CASE WHEN status = 'saleReturn' THEN imei END saleReturnImei,
                                CASE WHEN status = 'stockReturn' THEN imei END stockReturnImei,
                                CASE WHEN status = 'damage' THEN imei END damageImei,
                                CASE WHEN status = 'stockRemove' THEN imei END stockRemove,
                                CASE WHEN status = 'cancel' THEN imei END cancelImei
                                FROM StockStatusInfo where storeId='" + storeId + "' AND (prodID='" + productId + "' OR prodCode = '" + proCode + "')";

            var dtStore = sqlOperation.getDataTable(queryIMEI);

            //string totalImei = "";
            if (dtStore.Rows.Count > 0)
            {
                for (int i = 0; i < dtStore.Rows.Count; i++)
                {
                    string dbStock = dtStore.Rows[i]["stockImei"].ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                    totalStock += totalStock == "" ? dbStock : "," + dbStock;

                    sale = dtStore.Rows[i]["saleImei"].ToString().TrimEnd(',').TrimStart(',');
                    stockTransfer = dtStore.Rows[i]["stockTransferImei"].ToString().TrimEnd(',').TrimStart(',');
                    stockReturn = dtStore.Rows[i]["stockReturnImei"].ToString().TrimEnd(',').TrimStart(',');
                    damage = dtStore.Rows[i]["damageImei"].ToString().TrimEnd(',').TrimStart(',');
                    cancel = dtStore.Rows[i]["cancelImei"].ToString().TrimEnd(',').TrimStart(',');
                    stockRemove = dtStore.Rows[i]["stockRemove"].ToString().TrimEnd(',').TrimStart(',');

                    var totalSubstractImei = sale == "" ? sale : "," + sale;
                    totalSubstractImei += stockTransfer == "" ? stockTransfer : "," + stockTransfer;
                    totalSubstractImei += stockReturn == "" ? stockReturn : "," + stockReturn;
                    totalSubstractImei += damage == "" ? damage : "," + damage;
                    totalSubstractImei += cancel == "" ? cancel : "," + cancel;
                    totalSubstractImei += stockRemove == "" ? stockRemove : "," + stockRemove;

                    string[] spliptTotalSubsImei = totalSubstractImei.TrimEnd(',').Split(',');
                    for (int j = 0; j < spliptTotalSubsImei.Length; j++)
                    {
                        if (spliptTotalSubsImei[j].Trim() != "")
                        {
                            totalStock = totalStock.Replace(spliptTotalSubsImei[j].Trim() + ",", "");
                            totalStock = totalStock.Replace("," + spliptTotalSubsImei[j].Trim(), "");
                        }
                    }
                    totalStock = totalStock.TrimEnd(',');

                    // 
                    string dbSaleReturn = dtStore.Rows[i]["saleReturnImei"].ToString().TrimEnd(',').TrimStart(',');
                    if (saleReturn == "")
                        saleReturn = dbSaleReturn;
                    else
                        saleReturn = "," + dbSaleReturn;
                    saleReturn = saleReturn.TrimEnd(',');

                    string dbSaleRecive = dtStore.Rows[i]["stockReceiveImei"].ToString().TrimEnd(',').TrimStart(',');
                    if (stockRecive == "")
                        stockRecive = dbSaleRecive;
                    else
                        stockRecive = "," + dbSaleRecive;
                    stockRecive = stockRecive.TrimEnd(',');

                    saleReturn = saleReturn.TrimStart(',').TrimEnd(',');
                    totalStock += saleReturn != "" ? "," + saleReturn : saleReturn;

                    stockRecive = stockRecive.TrimStart(',').TrimEnd(',');
                    totalStock += stockRecive != "" ? "," + stockRecive : stockRecive;
                }
            }

            return totalStock.TrimEnd(',').TrimStart(',').Replace(",,", ",");
        }





        public string getStockQtyByUnitMeasurement(string prodId, string storeId)
        {
            var stockUnit = new InventoryBundle.Service.Stock();
            return stockUnit.getStockQtyByUnitService(prodId, storeId);
        }


        public int getRatioByProductId(string productId)
        {
            var stockModel = new StockModel();
            var dtRatio = stockModel.getProductRatioModelByProductId(productId);
            int ratio = 1;
            if (dtRatio.Rows.Count > 0)
            {
                ratio = Convert.ToInt32(dtRatio.Rows[0]["unitRatio"]);
            }
            return ratio;
        }




        public bool Sendmail(string url)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                HttpClient httpClient = new HttpClient(handler);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                HttpResponseMessage response = httpClient.SendAsync(request).Result;

                return response.IsSuccessStatusCode;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string responseContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    throw new System.Exception("response :{responseContent}", ex);


                }
                return false;
            }
        }







        public string getLastStockQty(string prodId, string storeId)
        {
            try
            {       
                //string query = "SELECT stock.prodId, qm.stockQty as stockQty FROM [StockInfo] as stock LEFT JOIN [qtyManagement] as qm ON stock.prodId=qm.productId WHERE qm.productId = '" + prodId + "' AND qm.storeId='" + storeId + "'";
                string query = @"
                    Select 
                    ((Select  (
                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,2) as INT) END),0)) -
                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,2) as INT) END),0)+
                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,2) as INT) END),0))
                    ) AS qty
                    FROM StockStatusInfo as stockstatus
                    WHERE stockstatus.prodId = stock.prodID AND stockstatus.storeId = warehouse.Id  )) AS qty,
                    ((Select  (
                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,1) as INT) END),0)) -
                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,1) as INT) END),0)+
                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,1) as INT) END),0))
                    ) AS qty
                    FROM StockStatusInfo as stockstatus
                    WHERE stockstatus.prodId = stock.prodID   AND stockstatus.storeId = warehouse.Id  )) AS piece
                    FROM StockInfo stock
                    LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodId = stock.prodId 
                    LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = stockstatus.storeId 
                    WHERE stockstatus.prodId='" + prodId
                    + "' AND stockstatus.storeId='" + storeId + "' GROUP BY stock.prodId  ,stockstatus.storeId,warehouse.Id";

                var dtStock = sqlOperation.getDataTable(query);

                if (dtStock.Rows.Count > 0)
                {
                    return dtStock.Rows[0]["qty"].ToString();//stockQty
                }
                else
                {
                    return "0.0";
                }
            }
            catch (Exception)
            {
                return "0.0";
            }
        }


        public int GenerateNewProductId()
        {
            int prodId = nextIdPlusOne("SELECT MAX(prodID) FROM [StockInfo]");
            int prodId2 = nextIdPlusOne("SELECT MAX(prodID) FROM [StockStatusInfo]");

            if (prodId2 > prodId)
                prodId = prodId2;

            return prodId;
        }


        public string nextPurchaseCode()
        {
            DataSet ds;
            string purchaseCode;
            ds = sqlOperation.getDataSet("SELECT top 1 purchaseCode FROM [StockStatusInfo] where purchaseCode LIKE 'P%' ORDER BY purchaseCode DESC");

            try
            {
                string pCode = ds.Tables[0].Rows[0][0].ToString();
                purchaseCode = nextPurchaeCodeGenerator(pCode);
            }
            catch
            {
                purchaseCode = "P000001";
            }

            return DateTime.Now.Ticks.ToString();
        }





        //public SqlConnection GetSqlConnection(string connectionName)
        //{

        //}

        public SqlConnection getMasterConnection()
        {
            var vconMater = new SqlConnection(ConfigurationManager.ConnectionStrings["dbMaster"].ToString());
            return vconMater;
        }


        public SqlConnection getPOSConnection()
        {
            var vconMater = new SqlConnection(ConfigurationManager.ConnectionStrings["dbPOS"].ToString());
            return vconMater;
        }



        public string getLastRoleIdIncrement()
        {
            var dtRoleID = sqlOperation.getDataTable("SELECT MAX(roleID) as maxRoleId FROM [RoleInfo]");
            var dtBranchID = sqlOperation.getDataTable("SELECT MAX(Id) as maxRoleId FROM [BranchInfo]");

            var lastBranchID = 1;

            if (dtBranchID.Rows[0][0].ToString() != "")
            {
                lastBranchID = Convert.ToInt32(dtBranchID.Rows[0][0].ToString());
            }

            if (dtRoleID.Rows[0][0].ToString() != "")
            {
                var lastRoleID = Convert.ToInt32(dtRoleID.Rows[0][0].ToString());


                if (lastRoleID < lastBranchID)
                    lastRoleID = lastBranchID;

                lastRoleID++;

                return lastRoleID.ToString();
            }
            else
            {
                return "1";

            }
        }


        public DataTable loadStoreData()
        {
            return sqlOperation.getDataTable("SELECT * FROM warehouseinfo where active='1' " + HttpContext.Current.Session["userAccessParameters"] + "");

        }





        public string balanceQtyOperation(string prodId, string storeId, string unitId)
        {
            string query = @"
                    Select 
                    ((Select  (
                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,2) as INT) END),0)) -
                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,2) as INT) END),0)+
                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,2) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,2) as INT) END),0))
                    ) AS qty
                    FROM StockStatusInfo as stockstatus
                    WHERE stockstatus.prodId = stock.prodID AND stockstatus.storeId = warehouse.Id  )) AS qty,
                    ((Select  (
                        (COALESCE(SUM(CASE WHEN status = 'stock' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'saleReturn' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockReceive' THEN CAST(parsename(qty,1) as INT) END),0)) -
                        (COALESCE(SUM(CASE WHEN status = 'sale' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'stockTransfer' THEN CAST(parsename(qty,1) as INT) END),0)+
                        COALESCE(SUM(CASE WHEN status = 'stockReturn' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'damage' THEN CAST(parsename(qty,1) as INT) END),0) +
                        COALESCE(SUM(CASE WHEN status = 'cancel' THEN CAST(parsename(qty,1) as INT) END),0))
                    ) AS qty
                    FROM StockStatusInfo as stockstatus
                    WHERE stockstatus.prodId = stock.prodID   AND stockstatus.storeId = warehouse.Id  )) AS piece
                    FROM StockInfo stock
                    LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodId = stock.prodId 
                    LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = stockstatus.storeId 
                    WHERE stockstatus.prodId='" + prodId
                    + "' AND stockstatus.storeId='" + storeId + "' GROUP BY stock.prodId  ,stockstatus.storeId,warehouse.Id";

            var dtBalQty = sqlOperation.getDataTable(query);

            var qty = 0;
            var piece = 0;

            for (int i = 0; i < dtBalQty.Rows.Count; i++)
            {
                qty += Convert.ToInt32(dtBalQty.Rows[i]["qty"]);
                piece += Convert.ToInt32(dtBalQty.Rows[i]["piece"]);
            }


            var dtRatio = sqlOperation.getDataTable("SELECT unitRatio FROM unitInfo WHERE Id='" + unitId + "'");
            var ratio = 1;
            var originalQty = qty;
            var originalPiece = 0;
            if (dtRatio.Rows.Count > 0)
            {
                ratio = Convert.ToInt32(dtRatio.Rows[0][0].ToString());
                if (piece < 0)
                {
                    var pieceToQty = piece / ratio;
                    var exitingPiece = piece % ratio;

                    originalQty -= pieceToQty + 1;
                    originalPiece = ratio - Math.Abs(exitingPiece);
                }
                else
                {
                    var pieceToQty = piece / ratio;
                    var exitingPiece = piece % ratio;

                    originalQty += pieceToQty;
                    originalPiece = exitingPiece;
                }
            }
            else
            {
                originalPiece = piece;
            }


            string originalQtyWithPiece = originalQty + "." + originalPiece;

            // stock
            //            var dtStock = sqlOperation.getDataTable("SELECT qty FROM StockInfo where prodId='" + prodId + "'");
            //            if (dtStock.Rows.Count > 0)
            //            {
            //                var stockTotalQty = Convert.ToDecimal(dtStock.Rows[0][0].ToString());
            //                var ajustQty = stockTotalQty - Convert.ToDecimal(originalQtyWithPiece);
            //                if (ajustQty > 0)
            //                {
            //                    string queryAjustQty = @"BEGIN TRANSACTION INSERT StockStatusInfo (prodID,prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleID,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId,isPackage,engineNumber,cecishNumber,transceiverId,searchType,purchaseCode,storeId) 
            //                                            (SELECT DISTINCT stockstatus.prodID, stock.prodCode, stock.prodName, stock.prodDescr, stock.supCompany, stock.catName,
            //                                            '" + stockTotalQty + @"', stock.bPrice, stock.sPrice, stock.weight, stock.size, stock.discount, stock.stockTotal, 'stock', getdate(), getdate(), stock.entryQty, stock.title,
            //                                            stock.roleId, '', stock.branchId, stock.groupId, stock.fieldAttribute, stock.tax, stock.sku, stock.lastQty, '', stock.prodCode, '', '', '', stock.commission, stock.dealerPrice, stock.createdFor, stock.unitId,
            //                                            'false', stock.engineNumber, stock.cecishNumber, '', stockstatus.searchType, stock.purchaseDate, stockstatus.storeId FROM StockStatusInfo stockstatus LEFT JOIN StockInfo stock ON stockstatus.prodID = stock.prodID WHERE stock.prodID = '" + prodId + "') COMMIT";
            //                    sqlOperation.executeQuery(queryAjustQty);
            //                }
            //            }


            return originalQtyWithPiece;
        }





        public string getUserDomain()
        {
            try
            {
                var url = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                string domainUrl = url.ToString();
                var splitUl = domainUrl.Split('?');

                var userDomain = splitUl[splitUl.Length - 1].Split('=')[1];

                return userDomain;
            }
            catch (Exception)
            {
                return "";
            }
        }


        public string GetSubDomain()
        {
            var url = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);


            if (url.HostNameType == UriHostNameType.Dns)
            {

                string host = url.Host;

                var nodes = host.Split('.');
                int startNode = 0;
                if (nodes[0] == "www") startNode = 1;

                //return string.Format("{0}.{1}", nodes[startNode], nodes[startNode + 1]);
                return string.Format("{0}", nodes[startNode]);

            }

            return null;
        }



        public string GetDomain()
        {
            var url = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string domain_name = "";

            if (url.HostNameType == UriHostNameType.Dns)
            {

                string host = url.Host;

                var nodes = host.Split('.');
                int startNode = 0;
                if (nodes[0] == "www") startNode = 1;

                if (nodes.Length > 1)
                    domain_name = host;
                else
                    domain_name = string.Format("{0}", nodes[startNode]);

            }

            if(domain_name == "localhost")
            {
                return "http://localhost:4355";
            }
            else
            {
                return "http://" + domain_name;
            }
        }


        public bool isSaaS()
        {
            Configuration Config1 = WebConfigurationManager.OpenWebConfiguration("~");
            var conSetting = (ConnectionStringsSection)Config1.GetSection("connectionStrings");
            var conStringCount = conSetting.ConnectionStrings.Count;
            if (conStringCount > 5)
                return true;

            return false;
        }



        public bool checkProductQtyManagement(string productId, string storeId)
        {
            var qtyManagement = new QtyManagement();
            var hasProduct = qtyManagement.hasProductStockQty(productId, storeId);
            if (hasProduct)
            {
                return true;
            }
            else
            {
                var stockModel = new StockModel();
                var dtStock = stockModel.getProductDataWithoutAnyAuth(productId);
                if (dtStock.Rows.Count > 0)
                {
                    stockModel.prodId = Convert.ToInt32(productId);
                    stockModel.storeId = Convert.ToInt32(storeId);
                    stockModel.qty = "0";
                    stockModel.roleId = HttpContext.Current.Session["roleId"].ToString();
                    stockModel.branchId = HttpContext.Current.Session["branchId"].ToString();
                    stockModel.groupId = HttpContext.Current.Session["groupId"].ToString();
                    stockModel.entryDate = GetCurrentTime();
                    stockModel.updateDate = GetCurrentTime();
                    return stockModel.createStockQtyManagement();
                }
            }
            return false;
        }


        public void generatePDF()
        {
            try
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                Paragraph Text = new Paragraph("This is test file");
                pdfDoc.Add(Text);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Example.pdf");
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            { 
                HttpContext.Current.Response.Write(ex.Message); 
            }  

           
        }

    }

}







using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Script.Serialization;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Models;

namespace MetaPOS.Api.Common
{
    public class CommonFunction
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public string getStoreListbyRoleId(string roleId, string shopname)
        {
            string storeList = "";
            var roleModel = new RoleModel();
            var dtRole = roleModel.getRoleDataModel(roleId, shopname);
            for (int i = 0; i < dtRole.Rows.Count; i++)
            {
                storeList += "storeId='" + dtRole.Rows[i]["storeId"] + "' ";
                if (i != dtRole.Rows.Count - 1)
                    storeList += "OR ";
                else
                    storeList += ")";
            }

            if (storeList == "")
                return "";

            return "AND (" + storeList;
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


        public string Encrypt(string toEncrypt)
        {
            string initVector = "tu89geji340t89u2", Key = "emergersIT.com";
            int keysize = 256;

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




        public string getRoleIdByBranchID(string roleId, string shopname)
        {
            var roleModel = new RoleModel();
            var dtRole = roleModel.getRoleIdByBranchId(roleId, shopname);

            string roleIdList = "";
            for (int i = 0; i < dtRole.Rows.Count; i++)
            {
                roleIdList += "roleId=" + dtRole.Rows[i]["roleId"] + " ";

                if (i == dtRole.Rows.Count - 1)
                {
                    roleIdList += ")";
                }
                else
                {
                    roleIdList += "OR ";
                }
            }

            if (roleIdList == "")
                return "";

            roleIdList = "AND ( " + roleIdList;

            return roleIdList;
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




        //***********************below function For post api write function**********************************
        public DateTime GetCurrentTime()
        {
            string host = HttpContext.Current.Request.Url.Host;
            DateTime exactTime = DateTime.Now;

            //string timezone = findSettingItemValue(43);

            if (host != "localhost")
                exactTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(exactTime, TimeZoneInfo.Local.Id, "Bangladesh Standard Time");

            return exactTime;
        }





        public string getLastStockQty(string prodId, int storeId)
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




        public string calculateQty(string prodId, string baseQty, string additionQty, string sign)
        {
            var productModel = new ProductModel();

            int dbRatio = 1;
            var dtStock = productModel.getProductRatioModelByProductId(prodId);
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




        public string getSaleLastID(string shopname)
        {
            sqlOperation.conString = shopname;
            var dtSaleId = sqlOperation.getDataTable("SELECT billNo, cusID FROM [SaleInfo] ORDER BY billNo DESC");
            if (dtSaleId.Rows.Count > 0)
                return dtSaleId.Rows[0][0].ToString();
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



    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using MetaPOS.Admin.AppBundle.Service;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.PromotionBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.AppBundle.View
{
    public partial class Operation : System.Web.UI.Page
    {




        [WebMethod]
        public static string getDataListAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.getDataList(jsonStrData);
        }





        [WebMethod]
        public static string getDataJoinListAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.getDataJoinList(jsonStrData);
        }




        [WebMethod]
        public static string getDataAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.getData(jsonStrData);
        }





        [WebMethod]
        public static string findDataAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.findData(jsonStrData);
        }





        [WebMethod]
        public static string saveDataAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.saveData(jsonStrData);
        }





        [WebMethod]
        public static string updateDataAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.updateData(jsonStrData);
        }





        [WebMethod]
        public static string deleteDataAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.deleteData(jsonStrData);
        }





        [WebMethod]
        public static string restoreDataAction(string jsonStrData)
        {
            var dataService = new DataService();
            return dataService.restoreData(jsonStrData);
        }





        [WebMethod]
        public static string loadCustomerBalanceDataAction(string cusId)
        {
            var dataService = new DataService();
            return dataService.loadCustomerBalanceData(cusId);
        }





        [WebMethod]
        public static string getCurrentStockTotalQtyAction(string prodId)
        {
            var commonFunction = new CommonFunction();
            return commonFunction.getCurrentStockQty(prodId);
        }


        [WebMethod]
        public static List<ListItem> loadStoreDataAction()
        {
            var commonFunction = new CommonFunction();
            var dtStore= commonFunction.loadStoreData();

            var storeList = new List<ListItem>();
            foreach (DataRow row in dtStore.Rows)
            {
                storeList.Add(new ListItem(row["name"].ToString(), row["Id"].ToString()));
            }

            return storeList;
        }


        [WebMethod]
        public static List<ListItem> loadStaffDataAction()
        {
            var staffModel = new StaffModel();
            var dtStaff=staffModel.getStaffListModel();

            var staffList = new List<ListItem>();
            foreach (DataRow row in dtStaff.Rows)
            {
                staffList.Add(new ListItem(row["name"].ToString(), row["Id"].ToString()));
            }

            return staffList;
        }


        [WebMethod]
        public static bool checkPagePermissionAction(string pageName)
        {
            var objCommonFun = new CommonFunction();
            return objCommonFun.accessChecker(pageName);
        }





        [WebMethod]
        public static string[] GetProducts(string prefix)
        {
            var products = new List<string>();
            string query = "";
            using (var conn = new SqlConnection())
            {
                var constr = GlobalVariable.getConnectionStringName();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;
                using (var cmd = new SqlCommand())
                {
                    query =
                        "SELECT DISTINCT prodName, prodCode FROM StockInfo WHERE (prodName like '%' + @SearchText + '%') " +
                        HttpContext.Current.Session["userAccessParameters"] + " ORDER BY prodName DESC  ";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            products.Add(string.Format("{0}, {1}", sdr["prodName"], sdr["prodCode"]));
                            //products.Add(string.Format("{0}, {1}, {2}", sdr["prodName"], sdr["prodCode"], sdr["prodID"]));
                        }
                    }
                    conn.Close();
                }
            }
            return products.ToArray();
        }





        [WebMethod]
        public static string[] GetCustomerInvoice(string prefix, string searchOption)
        {
            var products = new List<string>();
            string query = "";

            using (var conn = new SqlConnection())
            {
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings["dbPOS"].ConnectionString;
                var constr = GlobalVariable.getConnectionStringName();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;


                using (var cmd = new SqlCommand())
                {
                    if (searchOption == "customer")
                        query =
                            "SELECT DISTINCT name, phone, cusID FROM CustomerInfo WHERE (name like '%' + @SearchText + '%' OR phone like '%' + @SearchPhone + '%' OR cusID like '%' + @searchCusID + '%'  ) " +
                            HttpContext.Current.Session["userAccessParameters"];
                    else
                        query =
                            "SELECT DISTINCT billNo, cusID FROM SaleInfo WHERE (billNo like '%' + @SearchText + '%') " +
                            HttpContext.Current.Session["userAccessParameters"];


                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Parameters.AddWithValue("@SearchPhone", prefix);
                    cmd.Parameters.AddWithValue("@searchCusID", prefix);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (searchOption == "customer")
                                products.Add(string.Format("{0}, {1}, {2}", sdr["name"], sdr["phone"], sdr["cusID"]));
                            //products.Add(string.Format("{0}, {1}, {2}", sdr["prodName"], sdr["prodCode"], sdr["prodID"]));
                            //else
                            //products.Add(string.Format("{0}, {1}", sdr["billNo"], sdr["cusID"]));
                        }
                    }
                    conn.Close();
                }
            }
            return products.ToArray();
        }





        [WebMethod]
        public static dynamic uploadDoc(HttpPostedFileBase file)
        {
            try
            {
                return JObject.Parse("Hello mr.");

                //if (file.ContentLength > 0)
                //{
                //    string folderPath = HttpContext.Current.Server.MapPath("~/Img/Product");

                //    //Check whether Directory (Folder) exists.
                //    if (!Directory.Exists(folderPath))
                //    {
                //        //If Directory (Folder) does not exists. Create it.
                //        Directory.CreateDirectory(folderPath);
                //    }

                //    //Save the File to the Directory (Folder).
                //    file.SaveAs(folderPath + Path.GetFileName(file.FileName));

                //    return JObject.Parse("{'msg':'success'}");
                //}
            }
            catch (Exception)
            {
                return JObject.Parse("{'msg':'error'}");
            }
        }





        [WebMethod]
        public static string getSettingAccessValueAction()
        {
            try
            {
                var sessionId = "";
                if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                    sessionId = HttpContext.Current.Session["branchId"].ToString();
                else
                    sessionId = HttpContext.Current.Session["roleId"].ToString();

                var connectionStr = GlobalVariable.getConnectionStringName();
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStr].ConnectionString);
                string queryString = "SELECT * FROM SettingInfo WHERE Id='" +
                                     sessionId + "'";

                var adapter = new SqlDataAdapter(queryString, connection);
                var dt = new DataTable();
                adapter.Fill(dt);

                var commFunc = new CommonFunction();
                var data = commFunc.serializeDatatableToJson(dt);

                return data;
            }
            catch (Exception)
            {
                return "status:error";
            }
        }





        [WebMethod]
        public static string sendSmsDataAction(string jsonStrData)
        {
            var smsConfigModel = new SmsConfigModel();
            var dataList = smsConfigModel.getSmsConfigDataByBranchId();

            var smsService = new SmsService();
            if (dataList.Rows.Count > 0)
            {
                smsService.medium = "default";
                smsService.senderKey = dataList.Rows[0]["senderKey"].ToString();
            }
            else
            {
                return "Please set SMS configuration properly.";
            }

            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);
            var phoneList = data["phoneList"].Value<string>();
            var message = data["message"].Value<string>();
            var messageCount = data["messageCount"].Value<int>();
            var customer = data["customer"].Value<string>();


            var output = smsService.sendSmsService(phoneList, message, 0, messageCount, customer);

            return output;
        }

    }
}
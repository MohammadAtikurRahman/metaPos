using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Collections;
using System.IO;
using System.Diagnostics;
using MetaPOS.Admin.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Resources;


namespace MetaPOS.Admin.AnalyticBundle.View
{


    public partial class Analytic :BasePage //Page
    {


        //https://www.aspsnippets.com/Articles/Google-Chart-APIs-Google-Pie-Doughnut-Chart-example-with-database-in-ASPNet.aspx
        //http://www.c-sharpcorner.com/uploadfile/ca9151/google-charts-api-using-database-in-asp-net/
        //http://www.c-sharpcorner.com/uploadfile/4d9083/how-toString-create-google-charts-in-asp-net-with-json879/

        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        private static DateTime dateTo, dateFrom;
        private static string toString, fromString;





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Analytic"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }

                commonFunction.fillAllDdl(ddlCategory, "Select Id, catName FROM CategoryInfo WHERE active='1' AND (roleId='" + Session["roleId"] + "' OR roleId='" + Session["branchId"] + "')", "catName",
                    "Id");
                ddlCategory.Items.Insert(0, new ListItem("-- All --", "0"));

                commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");

                dateFrom = dateTo = Convert.ToDateTime(commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy"));
                dateFrom = dateFrom.AddDays(-7);

                txtFrom.Text = dateFrom.ToString("dd-MMM-yyyy");
                txtTo.Text = dateTo.ToString("dd-MMM-yyyy");

                DataVisibilityControl();
            }

            toString = Convert.ToDateTime(txtTo.Text).ToShortDateString();
            fromString = Convert.ToDateTime(txtFrom.Text).ToShortDateString();
        }

        



        [WebMethod]
        public static List<object> getSaleData(string analyticObj)
        {
            var data = (JObject)JsonConvert.DeserializeObject(analyticObj);
            var searchType = "day";
            try
            {
                searchType = data["dateSearchType"].Value<String>();
            }
            catch (Exception)
            {
                searchType = "day";
            }
            var dateForm = data["dateForm"].Value<String>();
            var dateTo = data["dateTo"].Value<String>();
            var storeId = data["storeId"].Value<String>();

            var storeAccessParameters = HttpContext.Current.Session["storeAccessParameters"].ToString();
            if (storeId != "0")
                storeAccessParameters = " AND storeId = '" + storeId + "'";

            string query = "", dateFormat = "dd-MMM-yy";
            
            if (searchType == "week")
            {
                query = @"SELECT DATEADD(WEEK, DATEDIFF(WEEK, 0, entryDate), 0) AS entryDate, SUM(grossAmt) as totalSaleAmt, SUM(payCash) AS payCash FROM SaleInfo 
                             WHERE entryDate between '" + dateForm + "' AND '" + dateTo + "' " + storeAccessParameters + " " +
                        "GROUP BY DATEADD(WEEK, DATEDIFF(WEEK, 0, entryDate), 0) ORDER BY entryDate DESC";

            }
            else if (searchType == "month")
            {
                dateFormat = "MMM-yy";

                if ((Convert.ToDateTime(toString) - Convert.ToDateTime(fromString)).TotalDays < 32)
                {

                    fromString = Convert.ToDateTime(fromString).AddDays(-60).ToShortDateString();
                }

                query = @"SELECT DATEADD(MONTH, DATEDIFF(MONTH, 0, entryDate), 0) AS entryDate, SUM(grossAmt) as totalSaleAmt, SUM(payCash) AS payCash FROM SaleInfo 
                             WHERE entryDate between '" + dateForm + "' AND '" + dateTo + "' " + storeAccessParameters + " " +
                        "GROUP BY DATEADD(MONTH, DATEDIFF(MONTH, 0, entryDate), 0) ORDER BY entryDate DESC";
            }
            else if (searchType == "year")
            {
                dateFormat = "yyyy";

                if ((Convert.ToDateTime(toString) - Convert.ToDateTime(fromString)).TotalDays < 370)
                {

                    fromString = Convert.ToDateTime(fromString).AddDays(-700).ToShortDateString();
                }

                query = @"SELECT DATEADD(YEAR, DATEDIFF(YEAR, 0, entryDate), 0) AS entryDate, SUM(grossAmt) as totalSaleAmt, SUM(payCash) AS payCash FROM SaleInfo 
                             WHERE entryDate between '" + dateForm + "' AND '" + dateTo + "' " + storeAccessParameters + " " +
                        "GROUP BY DATEADD(YEAR, DATEDIFF(YEAR, 0, entryDate), 0) ORDER BY entryDate DESC";

            }
            else
            {
                // day

                query = @"SELECT DATEADD(DAY, DATEDIFF(day, 0, entryDate), 0) AS entryDate, SUM(grossAmt) as totalSaleAmt, SUM(payCash) AS payCash FROM SaleInfo 
                             WHERE entryDate between '" + dateForm + "' AND '" + dateTo + "' " + storeAccessParameters + " " +
                        "GROUP BY DATEADD(DAY, DATEDIFF(day, 0, entryDate), 0) ORDER BY entryDate DESC";
            }

            //            string query = @"SELECT DATEADD(DAY, DATEDIFF(day, 0, entryDate), 0) AS entryDate, SUM(grossAmt) as totalSaleAmt, SUM(payCash) AS payCash FROM SaleInfo 
            //                            WHERE entryDate BETWEEN " + toString + " AND " + fromString + " GROUP BY DATEADD(DAY, DATEDIFF(day, 0, entryDate), 0) ORDER BY entryDate DESC";

            //            string query = @"SELECT DATEADD(Week, DATEDIFF(week, 0, entryDate), 0) [week] ,DATEADD(Week, DATEDIFF(week, 0, entryDate), 0) AS entryDate ,  SUM(grossAmt) as totalSaleAmt, SUM(payCash) AS payCash FROM SaleInfo 
            //                            WHERE entryDate BETWEEN '12/15/2016' AND '05/17/2017' GROUP BY DATEADD(Week, DATEDIFF(week, 0, entryDate), 0) ORDER BY [week] DESC";

            //            string query = @"SELECT DATEPART(Year, entryDate) Year, DATEPART(Month, entryDate) Month, CONCAT(DATEPART(Month, entryDate),'/', DATEPART(Year, entryDate)) AS entryDate ,  SUM(grossAmt) as totalSaleAmt, SUM(payCash) AS payCash FROM SaleInfo 
            //                            WHERE entryDate BETWEEN '12/15/2016' AND '05/17/2017' GROUP BY DATEPART(Year, entryDate), DATEPART(Month, entryDate) ORDER BY Month DESC";

            string conString = GlobalVariable.getConnectionStringName();
            string constr = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            List<object> chartData = new List<object>();

            chartData.Add(new object[]
            {
                "Date", "Sale Amount", "Paid Amount"
            });
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                                //sdr["entryDate"], sdr["totalSaleAmt"], sdr["payCash"]
                                Convert.ToDateTime(sdr["entryDate"]).ToString(dateFormat), sdr["totalSaleAmt"], sdr["payCash"]
                            });
                        }
                    }
                    con.Close();
                }
            }

            return chartData;
        }


        private void DataVisibilityControl()
        {
            if (Session["userRight"].ToString() == "Branch")
            {
                ddlStoreList.Items.Insert(0, new ListItem(Language.Lbl_analyticReport_store, "0"));
            }
            else if (Session["userRight"].ToString() == "Regular")
            {
                ddlStoreList.SelectedValue = Session["storeId"].ToString();
            }
        }

    }


}
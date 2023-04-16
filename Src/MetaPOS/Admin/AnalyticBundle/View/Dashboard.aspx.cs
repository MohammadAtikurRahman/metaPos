using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using MetaPOS.Admin.AnalyticBundle.Service;
using MetaPOS.Admin.DataAccess;
using Resources;


namespace MetaPOS.Admin.AnalyticBundle.View
{


    public partial class Dashboard : BasePage
    {


        //https://www.aspsnippets.com/Articles/Google-Chart-APIs-Google-Pie-Doughnut-Chart-example-with-database-in-ASPNet.aspx
        //http://www.c-sharpcorner.com/uploadfile/ca9151/google-charts-api-using-database-in-asp-net/
        //http://www.c-sharpcorner.com/uploadfile/4d9083/how-to-create-google-charts-in-asp-net-with-json879/

        private CommonFunction commonFunction = new CommonFunction();





        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Dashboard"))
                {
                    var obj = new CommonFunction();
                    obj.pageout();
                }
            }



            try
            {

                //pie Chart title date
                lblProductDate.InnerText = commonFunction.GetCurrentTime().ToString("MMMM, yyy");

                // Total Sale Amount
                var totalSaleAmt = "0";
                var saleService = new SaleService();
                var dtSaleService = saleService.getSaleSummery(Session["storeAccessParameters"].ToString(),
                    commonFunction.GetCurrentTime(), commonFunction.GetCurrentTime());

                if (dtSaleService.Rows[0]["netAmt"].ToString() != "")
                {
                    totalSaleAmt = dtSaleService.Rows[0]["netAmt"].ToString();
                }
                lblTotalSaleAmt.Text = totalSaleAmt;

                // Total Paid Amount
                var totalBillNo = "0";
                var dtSale = saleService.getSaleRecordInfo(Session["storeAccessParameters"].ToString(),
                    commonFunction.GetCurrentTime(), commonFunction.GetCurrentTime());


                if (dtSale.Rows.Count > 0)
                {
                    totalBillNo = dtSale.Rows.Count.ToString();
                }
                lblNewInvoice.Text = totalBillNo;


                // Show qty
                decimal saleQty = 0M;
                if (dtSale.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSale.Rows.Count; i++)
                    {
                        saleQty += Convert.ToDecimal(dtSale.Rows[i]["qty"].ToString());
                    }
                }
                lblProdQty.Text = saleQty.ToString();
               


                // New Customer 
                var customerService = new CustomerService();
                var dtCustomerList = customerService.getCustomerListByDate(commonFunction.GetCurrentTime(),
                    commonFunction.GetCurrentTime());

                lblNewCustomer.Text = dtCustomerList.Rows.Count.ToString();


                //Month set in deashboard
                int monthCount = getSaleMonthCount();
                lblMonthCount.Text = monthCount.ToString();

                
            }
            catch (Exception)
            {
                Response.Redirect("/error");
            }

        }





        /* Sales Reports */





        [WebMethod]
        public static List<object> getSaleData()
        {
            string conString = GlobalVariable.getConnectionStringName();
            string constr = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
        {
            "Date", "Sale Amount"
        });

            int monthCount = getSaleMonthCount();

            // get last 6 month list
            var lastSixMonths = Enumerable.Range(0, monthCount).Select(i => DateTime.Now.AddMonths(i - monthCount).ToString("MM/yyyy"));
            foreach (var monthAndYear in lastSixMonths)
            {
                char splitBy = '/';

                if (monthAndYear.Contains('-'))
                {
                    splitBy = '-';
                }


                string[] words = monthAndYear.Split(splitBy);

                string qStr =
                    "SELECT SUM(grossAmt) as totalSaleAmt, SUM(payCash) as payCash from SaleInfo WHERE MONTH(entryDate) = " +
                    words[0] + "  AND YEAR(entryDate) = " + words[1] + HttpContext.Current.Session["userAccessParameters"];

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(qStr))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();

                            var saleCount = sdr["totalSaleAmt"];

                            if (saleCount == DBNull.Value)
                            {
                                saleCount = 0;
                            }

                            chartData.Add(new object[]
                        {
                            Convert.ToDateTime(monthAndYear).ToString("MM/yy"), saleCount
                        });
                            sdr.Close();
                        }
                        con.Close();
                    }
                }
            }
            return chartData;
        }




        [WebMethod]
        public static int getSaleMonthCount()
        {
            int monthCount = 6;


            var sessionId = "";
            if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                sessionId = HttpContext.Current.Session["branchId"].ToString();
            else
                sessionId = HttpContext.Current.Session["roleId"].ToString();

            string conString = GlobalVariable.getConnectionStringName();
            string constr = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            List<object> chartData = new List<object>();

            // get setting data for sale month count
            string monthData = "SELECT monthWaseSalesReport FROM SettingInfo WHERE id='" + sessionId + "'";

            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(monthData))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            monthCount = Convert.ToInt32(sdr["monthWaseSalesReport"]);
                            sdr.Close();
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
            }
            if (monthCount == 0)
            {
                monthCount = 6;
            }
            return monthCount;
        }



        //public static List<object> getSaleData()
        //{
        //    //select top 6  (CAST(MONTH(entryDate) as varchar(20)) + '/' +  CAST(YEAR(entryDate) as nvarchar(20))) as Dated, SUM(grossAmt) as totalSale, SUM(payCash) as totalPay from SlipInfo where Id IN (SELECT MAX(temp.Id) FROM SlipInfo as temp group by billNo) group by MONTH(entryDate),YEAR(entryDate) ORDER BY YEAR(entryDate) DESC

        //    string query =
        //        "select top 4  (CAST(MONTH(entryDate) as varchar(20)) + '/' +  CAST(YEAR(entryDate) as nvarchar(20))) as entryDate, SUM(grossAmt) as totalSaleAmt, SUM(payCash) as payCash from SlipInfo where Id IN (SELECT MAX(temp.Id) FROM SlipInfo as temp group by billNo)" + HttpContext.Current.Session["userAccessParameters"] +
        //        " group by MONTH(entryDate),YEAR(entryDate) ORDER BY YEAR(entryDate) DESC";

        //    string conString = GlobalVariable.getConnectionStringName();
        //    string constr = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
        //    List<object> chartData = new List<object>();

        //    chartData.Add(new object[]
        //    {
        //        "Date", "Sale Amount"
        //    });
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    chartData.Add(new object[]
        //                    {
        //                        Convert.ToDateTime(sdr["entryDate"]).ToString("MM/yy"), sdr["totalSaleAmt"]
        //                    });
        //                }
        //            }
        //            con.Close();
        //        }
        //    }

        //    return chartData;
        //}




        [WebMethod]
        public static List<object> getTopCategory()
        {
            var commonFunction = new CommonFunction();

            string query = @"select top 5 tbl.prodID, stock.prodName as productName, count(tbl.prodID) Total
                            from SaleInfo tbl LEFT JOIN StockInfo stock  ON tbl.prodId = stock.prodId
							WHERE stock.prodName != '' AND DATEPART(MM, tbl.entryDate) = DATEPART(MM, getdate())" +
                           commonFunction.getUserAccessParameters("tbl") +
                           " group by tbl.prodID,stock.prodName order by tbl.prodID desc";

            string conString = GlobalVariable.getConnectionStringName();
            string constr = ConfigurationManager.ConnectionStrings[conString].ConnectionString;
            List<object> chartData = new List<object>();

            chartData.Add(new object[]
            {
                "productName", "Total"
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
                                sdr["productName"].ToString(), sdr["Total"] });
                        }
                    }
                    con.Close();
                }
            }

            return chartData;
        }



    }


}
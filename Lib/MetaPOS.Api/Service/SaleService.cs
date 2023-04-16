using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;
using MetaPOS.Api.Models;
using System.Web;

namespace MetaPOS.Api.Service
{
    public class SaleService
    {
        //private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        public List<DataStatus> SaleSummary(DateTime startdate, DateTime enddate, string storeid, string shopname)
        {

            var statusData = new List<DataStatus>();


            if (!commonFunction.CheckConnectionString(shopname))
            {
                statusData.Add(new DataStatus() { status = "404" });
                return statusData;
            }

            SaleModel saleModel = new SaleModel();
            saleModel.shopName = shopname;

            var tableData = saleModel.SaleAmountModel(startdate, enddate, storeid);
            if (tableData.Rows.Count > 0)
            {
                var netAmt = tableData.Rows[0]["netAmt"].ToString();
                netAmt = netAmt == "" ? "0" : netAmt;

                var discAmt = tableData.Rows[0]["discAmt"].ToString();
                discAmt = discAmt == "" ? "0" : discAmt;

                var grossAmt = tableData.Rows[0]["grossAmt"].ToString();
                grossAmt = grossAmt == "" ? "0" : grossAmt;

                var giftAmt = tableData.Rows[0]["giftAmt"].ToString();
                giftAmt = giftAmt == "" ? "0" : giftAmt;

                var totalInvoice = InvoiceCounter(startdate, enddate, storeid, shopname);

                var totalSaleQty = TotalSaleQty(startdate, enddate, storeid, shopname);

                // Misc cost
                var miscCost =
                    Convert.ToDecimal(tableData.Rows[0]["loadingCost"].ToString() == ""
                        ? "0"
                        : tableData.Rows[0]["loadingCost"].ToString()) +
                    Convert.ToDecimal(tableData.Rows[0]["carryingCost"].ToString() == ""
                        ? "0"
                        : tableData.Rows[0]["carryingCost"].ToString()) +
                    Convert.ToDecimal(tableData.Rows[0]["unloadingCost"].ToString() == ""
                        ? "0"
                        : tableData.Rows[0]["unloadingCost"].ToString()) +
                    Convert.ToDecimal(tableData.Rows[0]["shippingCost"].ToString() == ""
                        ? "0"
                        : tableData.Rows[0]["shippingCost"].ToString()) +
                    Convert.ToDecimal(tableData.Rows[0]["serviceCharge"].ToString() == ""
                        ? "0"
                        : tableData.Rows[0]["serviceCharge"].ToString()).ToString("0.00");



                var saleSummary = new List<object>();
                saleSummary.Add(new Summary()
                {
                    title = "মোট বিক্রয়",
                    amount = netAmt,
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট ডিস্কাউন্ট",
                    amount = discAmt,
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট পরিমাণ",
                    amount = grossAmt,
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট বাকি",
                    amount = giftAmt,
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট বিবিধ ব্যয়",
                    amount = miscCost,
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট ইনভয়েজ",
                    amount = totalInvoice,
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট বিক্রয় পরিমাণ",
                    amount = totalSaleQty,
                    imageurl = "/img/appicon/icon1.svg"
                });

                statusData.Add(new DataStatus() { status = "200", data = saleSummary });
            }
            else
            {
                statusData.Add(new DataStatus() { status = "403" });
            }

            return statusData;
        }



        public string InvoiceCounter(DateTime startDate, DateTime endDate, string storeId, string shopname)
        {
            var storeAccessParameters = " AND storeId='" + storeId + "'";

            var saleModel = new SaleModel();
            saleModel.shopName = shopname;
            saleModel.startDate = startDate;
            saleModel.endDate = endDate;
            saleModel.storeAccessParameters = storeAccessParameters;
            var dtSale = saleModel.getSaleRecordModel();
            return dtSale.Rows.Count.ToString();
        }




        public string TotalSaleQty(DateTime startDate, DateTime endDate, string storeId, string shopname)
        {
            //var storeAccessParameters = commonFunction.getStoreListbyRoleId(roleId);
            var storeAccessParameters = " AND storeId='" + storeId + "'";

            var saleModel = new SaleModel();
            saleModel.startDate = startDate;
            saleModel.endDate = endDate;
            saleModel.shopName = shopname;
            saleModel.storeAccessParameters = storeAccessParameters;
            var dtSale = saleModel.getSaleRecordModel();
            var totalSaleQty = 0M;
            for (int i = 0; i < dtSale.Rows.Count; i++)
            {
                totalSaleQty += Convert.ToDecimal(dtSale.Rows[i]["qty"].ToString());
            }
            return totalSaleQty.ToString();
        }



        public string NewCustomer(DateTime startDate, DateTime endDate, string storeId)
        {
            return "";
        }




        public List<DataStatus> SaleAreaChart(string storeId, string shopname)
        {
            //var listOfStore = commonFunction.getStoreListbyRoleId(roleId);
            //if (listOfStore == "")
            //    return new List<object>();
            //select top 6  (CAST(MONTH(entryDate) as varchar(20)) + '/' +  CAST(YEAR(entryDate) as nvarchar(20))) as Dated, SUM(grossAmt) as totalSale, SUM(payCash) as totalPay from SlipInfo where Id IN (SELECT MAX(temp.Id) FROM SlipInfo as temp group by billNo) group by MONTH(entryDate),YEAR(entryDate) ORDER BY YEAR(entryDate) DESC
            var statusData = new List<DataStatus>();
            try
            {
                string query =
                "select top 6 entryDate, SUM(grossAmt) as totalSaleAmt, SUM(payCash) as payCash from saleinfo where storeId='" + storeId + "' " +
                " group by entryDate ORDER BY entryDate DESC";
                var sqlOperation = new SqlOperation();
                string conString = shopname;
                string constr = ConfigurationManager.ConnectionStrings[conString].ConnectionString;

                var listData = new List<object>();

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            int i = 1;
                            while (sdr.Read())
                            {
                                var strDate = Convert.ToDateTime(sdr["entryDate"]).ToShortDateString();
                                string[] splitDate = strDate.Split('/');

                                listData.Add(new SaleGraphChart() { id = i.ToString(), date = splitDate[1] + "/" + splitDate[2], saleamount = sdr["totalSaleAmt"].ToString() });
                                i++;
                            }
                        }
                        con.Close();
                    }
                }
                statusData.Add(new DataStatus() { status = "200", data = listData });
            }
            catch (Exception)
            {
                statusData.Add(new DataStatus() { status = "404" });
            }

            return statusData;
        }




        public List<DataStatus> SalePieChart(string storeId, string shopname)
        {
            //var listOfStore = commonFunction.getStoreListbyRoleId(roleId);
            //if (listOfStore == "")
            //    return new List<object>();
            var statusData = new List<DataStatus>();
            try
            {
                // AND DATEPART(MM, tbl.entryDate) = DATEPART(MM, getdate())
                string query = @"select top 5 tbl.prodID, stock.prodName as productName, count(tbl.prodID) Total
                            from SaleInfo tbl LEFT JOIN StockInfo stock  ON tbl.prodId = stock.prodId
							WHERE stock.prodName != ''  AND storeId='" + storeId + "'" +
                              " group by tbl.prodID,stock.prodName order by tbl.prodID desc";
                string conString = shopname;
                string constr = ConfigurationManager.ConnectionStrings[conString].ConnectionString;

                var listData = new List<object>();

                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            int i = 1;
                            while (sdr.Read())
                            {
                                listData.Add(new SalePieChart() { id = i.ToString(), productname = sdr["productName"].ToString(), totalamount = sdr["Total"].ToString() });
                                i++;
                            }
                        }
                        con.Close();
                    }
                }

                statusData.Add(new DataStatus() { status = "200", data = listData });
            }
            catch (Exception)
            {
                statusData.Add(new DataStatus() { status = "404" });
            }

            return statusData;
        }












        public string SalesQty(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
           var saleModel = new SaleModel();
            saleModel.startDate = startDate;
            saleModel.endDate = endDate;
            saleModel.storeAccessParameters = storeAccessParameters;
            var dtItemQty = saleModel.SaleQtyModel();
            return dtItemQty.Rows.Count.ToString();
        }



        public string ItemQty(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            var saleModel = new SaleModel();
            saleModel.startDate = startDate;
            saleModel.endDate = endDate;
            saleModel.storeAccessParameters = storeAccessParameters;
            var dtItemQty = saleModel.SaleQtyModel();
            var totalItemQty = 0M;
            for (int i = 0; i < dtItemQty.Rows.Count; i++)
            {
                totalItemQty += Convert.ToDecimal(dtItemQty.Rows[i]["qty"].ToString());
            }
            return totalItemQty.ToString("0.00");
        }


        public decimal SalesNetAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            var saleModel = new SaleModel();
            saleModel.startDate = startDate;
            saleModel.endDate = endDate;
            saleModel.storeAccessParameters = storeAccessParameters;
            var dtSalesNetAmount = saleModel.SaleNetAmountModel();
            return dtSalesNetAmount.Rows[0]["totalNetAmount"].ToString() == "" ? 0M : Convert.ToDecimal(dtSalesNetAmount.Rows[0]["totalNetAmount"].ToString());
        }



        public decimal SalesProfit(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSalesProfit = saleModel.SaleProfitModel();
                decimal totalSalePrice = 0M, totalBuyPrice = 0M;
                for (int i = 0; i < dtSalesProfit.Rows.Count; i++)
                {
                    totalSalePrice += Convert.ToDecimal(dtSalesProfit.Rows[i]["salePrice"].ToString()) *
                                      Convert.ToDecimal(dtSalesProfit.Rows[i]["qty"].ToString());
                    totalBuyPrice += Convert.ToDecimal(dtSalesProfit.Rows[i]["buyPrice"].ToString()) *
                                     Convert.ToDecimal(dtSalesProfit.Rows[i]["qty"].ToString());
                }
                return totalSalePrice - totalBuyPrice;
            }
            catch (Exception)
            {
                return -1M;
            }
        }

        public decimal CommissionAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {

            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtCommissionAmount = saleModel.CommissionAmountModel();
                var totalCommissionAmt = 0M;
                for (int i = 0; i < dtCommissionAmount.Rows.Count; i++)
                {
                    var bPrice = Convert.ToDecimal(dtCommissionAmount.Rows[i]["bPrice"].ToString());
                    var commission = Convert.ToDecimal(dtCommissionAmount.Rows[i]["commission"].ToString());
                    var qty = Convert.ToDecimal(dtCommissionAmount.Rows[i]["qty"].ToString());
                    var commissionAmt = (commission * bPrice) / 100;
                    totalCommissionAmt += commissionAmt * qty;
                }
                return totalCommissionAmt;
            }
            catch (Exception)
            {
                return -1M;
            }
        }

        public decimal DiscountAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtCommissionAmount = saleModel.DiscountAmountModel();
                var totalCommissionAmt = 0M;
                for (int i = 0; i < dtCommissionAmount.Rows.Count; i++)
                {
                    totalCommissionAmt += Convert.ToDecimal(dtCommissionAmount.Rows[i]["discAmt"].ToString());
                }

                return totalCommissionAmt;
            }
            catch (Exception)
            {
                return -1M;
            }
        }



        public decimal ReturnAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtReturnAmount = saleModel.ReturnAmountModel();
                var totalCommissionAmt = 0M;
                for (int i = 0; i < dtReturnAmount.Rows.Count; i++)
                {
                    totalCommissionAmt += Convert.ToDecimal(dtReturnAmount.Rows[i]["balance"].ToString());
                }
                return totalCommissionAmt;
            }
            catch (Exception)
            {
                return -1M;
            }
        }




        public decimal MiscCostAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtMiscCostAmount = saleModel.MiscCostAmountModel();
                var totalMiscCost = 0M;

                var loadingCost = dtMiscCostAmount.Rows[0]["loadingCost"].ToString() == "" ? 0M : Convert.ToDecimal(dtMiscCostAmount.Rows[0]["loadingCost"].ToString());
                var carryingCost = dtMiscCostAmount.Rows[0]["carryingCost"].ToString() == "" ? 0M : Convert.ToDecimal(dtMiscCostAmount.Rows[0]["carryingCost"].ToString());
                var unloadingCost = dtMiscCostAmount.Rows[0]["unloadingCost"].ToString() == "" ? 0M : Convert.ToDecimal(dtMiscCostAmount.Rows[0]["unloadingCost"].ToString());
                var shippingCost = dtMiscCostAmount.Rows[0]["shippingCost"].ToString() == "" ? 0M : Convert.ToDecimal(dtMiscCostAmount.Rows[0]["shippingCost"].ToString());
                var serviceCharge = dtMiscCostAmount.Rows[0]["serviceCharge"].ToString() == "" ? 0M : Convert.ToDecimal(dtMiscCostAmount.Rows[0]["serviceCharge"].ToString());

                totalMiscCost = loadingCost + carryingCost + unloadingCost + shippingCost + serviceCharge;

                return totalMiscCost;
            }
            catch (Exception)
            {
                return -1M;
            }
        }




        public decimal SupplierRecivedAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSupplierRecivedAmount = saleModel.SupplierRecivedAmountModel();
                return dtSupplierRecivedAmount.Rows[0]["cashout"].ToString() == "" ? 0M : Convert.ToDecimal(dtSupplierRecivedAmount.Rows[0]["cashout"].ToString());
            }
            catch (Exception)
            {
                return -1M;
            }
        }

        public decimal SalaryAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSalaryAmount = saleModel.SalaryAmountModel();

                return dtSalaryAmount.Rows[0]["totalSalary"].ToString() == "" ? 0M : Convert.ToDecimal(dtSalaryAmount.Rows[0]["totalSalary"].ToString());
            }
            catch (Exception)
            {
                return -1M;
            }
        }

        public decimal ExpenseAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSupplierRecivedAmount = saleModel.ExpenceAmountModel();
                return dtSupplierRecivedAmount.Rows[0]["cashOut"].ToString() == "" ? 0M : Convert.ToDecimal(dtSupplierRecivedAmount.Rows[0]["cashOut"].ToString());
            }
            catch (Exception)
            {
                return -1M;
            }
        }

        public decimal salesRecivedAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSupplierRecivedAmount = saleModel.SalesReceivedAmountModel();
                return dtSupplierRecivedAmount.Rows[0]["cashIn"].ToString() == "" ? 0M : Convert.ToDecimal(dtSupplierRecivedAmount.Rows[0]["cashIn"].ToString());
            }
            catch (Exception)
            {
                return -1M;
            }
        }

        public decimal TaxAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            return 0M;
        }

        public decimal SaleGrossAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSupplierRecivedAmount = saleModel.SaleGrossAmountModel();
                return dtSupplierRecivedAmount.Rows[0]["grossAmt"].ToString() == "" ? 0M : Convert.ToDecimal(dtSupplierRecivedAmount.Rows[0]["grossAmt"].ToString());
            }
            catch (Exception)
            {
                return -1M;
            }
        }




        public string cashReportSummaryPayMethod(DateTime startDate, DateTime endDate, string storeAccessParameters, int payMethod)
        {
            try
            {
                var saleModel = new SaleModel();
                var dtCashReportPayMethod = saleModel.getCashReportByPayMethodModel(startDate, endDate, storeAccessParameters, payMethod);
                if (dtCashReportPayMethod.Rows[0][0].ToString() == "")
                    return "0";
                else
                    return dtCashReportPayMethod.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "-1";
            }
        }


        public decimal recivedAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSupplierRecivedAmount = saleModel.ReceivedAmountModel();
                return dtSupplierRecivedAmount.Rows[0]["cashIn"].ToString() == "" ? 0M : Convert.ToDecimal(dtSupplierRecivedAmount.Rows[0]["cashIn"].ToString());
            }
            catch (Exception)
            {
                return -1M;
            }
        }

        public decimal DueAmount(DateTime startDate, DateTime endDate, string storeAccessParameters)
        {
            try
            {
                var saleModel = new SaleModel();
                saleModel.startDate = startDate;
                saleModel.endDate = endDate;
                saleModel.storeAccessParameters = storeAccessParameters;
                var dtSupplierRecivedAmount = saleModel.DueAmountModel();
                return dtSupplierRecivedAmount.Rows[0]["totalDueAmt"].ToString() == "" ? 0M : Convert.ToDecimal(dtSupplierRecivedAmount.Rows[0]["totalDueAmt"].ToString());
            }
            catch (Exception)
            {
                return -1M;
            }
        }
    }
}

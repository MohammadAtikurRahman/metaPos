using System;
using System.Data;
using System.Web;


namespace MetaPOS.Admin.AnalyticBundle.Service
{


    public class SummaryModel
    {


        private static string query = "";
        private DataSet ds;

        private DataAccess.SqlOperation objSql = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction commFun = new DataAccess.CommonFunction();

        // Total Sale Amt 
        public decimal totalSaleAmt()
        {
            decimal totalSaleAmt;
            try
            {
                totalSaleAmt = 0;

                //query = "SELECT tbl.billNo, tbl.qty, StockInfo.bPrice*tbl.qty, tbl.sPrice*tbl.qty FROM SaleInfo as tbl JOIN StockInfo ON tbl.prodID=StockInfo.prodID WHERE CAST(tbl.entryDate AS date)= '" + commFun.GetCurrentTime().ToString("dd/MMM/yyyy") + "'" + commFun.getUserAccessParameters("tbl");

                query = "SELECT distinct billNo, grossAmt FROM SaleInfo  WHERE CAST(entryDate AS date)= '" +
                        commFun.GetCurrentTime().ToString("dd/MMM/yyyy") + "'" +
                        HttpContext.Current.Session["userAccessParameters"];

                ds = objSql.getDataSet(query);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    totalSaleAmt += Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                }
            }
            catch
            {
                totalSaleAmt = 0;
            }

            return totalSaleAmt;
        }





        // Total Paid Amt
        public decimal totalProfitAmt()
        {
            decimal totalProfitAmt = 0, totalDiscAmt = 0;

            try
            {
                query = "SELECT tbl.billNo, tbl.qty, StockInfo.bPrice*tbl.qty, tbl.sPrice*tbl.qty FROM SaleInfo as tbl JOIN StockInfo ON tbl.prodID=StockInfo.prodID  WHERE CAST(tbl.entryDate AS date)= '" +
                    commFun.GetCurrentTime().ToString("dd/MMM/yyyy") + "'" +
                    commFun.getUserAccessParameters("tbl");

                ds = objSql.getDataSet(query);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    totalProfitAmt += Convert.ToDecimal(ds.Tables[0].Rows[i][3].ToString()) -
                                      Convert.ToDecimal(ds.Tables[0].Rows[i][2].ToString());
                }

                totalDiscAmt = totalDiscountAmt();
                totalProfitAmt = totalProfitAmt - totalDiscAmt;
            }
            catch (Exception)
            {
                totalProfitAmt = 0;
            }

            return totalProfitAmt;
        }





        public decimal totalDiscountAmt()
        {
            decimal totalDiscountAmt = 0;
            try
            {
                query = "SELECT distinct billNo, discAmt FROM SaleInfo  WHERE CAST(entryDate AS date)= '" +
                        commFun.GetCurrentTime().ToString("dd/MMM/yyyy") + "'" +
                        HttpContext.Current.Session["userAccessParameters"];
                ds = objSql.getDataSet(query);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    totalDiscountAmt += Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                }
            }
            catch (Exception)
            {
                totalDiscountAmt = 0;
            }
            return totalDiscountAmt;
        }





        // Total Product Qty
        public int totalProductQty()
        {
            int totalProdQty = 0;

            try
            {
                query = "SELECT distinct billNo, qty FROM SaleInfo  WHERE CAST(entryDate AS date)= '" +
                        commFun.GetCurrentTime().ToString("dd/MMM/yyyy") + "'" +
                        HttpContext.Current.Session["userAccessParameters"];
                ds = objSql.getDataSet(query);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    totalProdQty += Convert.ToInt32(ds.Tables[0].Rows[i][1]);
                }
            }
            catch (Exception)
            {
                totalProdQty = 0;
            }

            return totalProdQty;
        }





        // Total New customer
        public int totalNewCustomer()
        {
            int totalNewCus = 0;

            try
            {
                query = "SELECT cusId FROM CustomerInfo WHERE CAST(entryDate AS date)= '" +
                        commFun.GetCurrentTime().ToString("dd/MMM/yyyy") + "'" +
                        HttpContext.Current.Session["userAccessParameters"];
                ds = objSql.getDataSet(query);
                totalNewCus = Convert.ToInt32(ds.Tables[0].Rows.Count);
            }
            catch (Exception)
            {
                totalNewCus = 0;
            }

            return totalNewCus;
        }





        public int totalNewInvoice()
        {
            int totalNewInvoice = 0;

            try
            {
                query = "Select billNo FROM SaleInfo WHERE CAST(entryDate AS date)= '" +
                        commFun.GetCurrentTime().ToString("dd/MMM/yyyy") + "'" +
                        HttpContext.Current.Session["userAccessParameters"];
                ds = objSql.getDataSet(query);
                totalNewInvoice = Convert.ToInt32(ds.Tables[0].Rows.Count);
            }
            catch (Exception)
            {
                totalNewInvoice = 0;
            }

            return totalNewInvoice;
        }


    }


}
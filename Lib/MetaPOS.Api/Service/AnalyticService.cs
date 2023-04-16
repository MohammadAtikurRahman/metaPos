using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;
using MetaPOS.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Api.Service
{


    public class AnalyticService
    {
        private CommonFunction commonFunction = new CommonFunction();

        public List<DataStatus> AnalyticReport(DateTime startdate, DateTime enddate, string storeid, string shopname)
        {

            var statusData = new List<DataStatus>();


            if (!commonFunction.CheckConnectionString(shopname))
            {
                statusData.Add(new DataStatus() { status = "404" });
                return statusData;
            }

            try
            {
                SaleModel saleModel = new SaleModel();
                
                saleModel.shopName = shopname;

                var tableData = saleModel.SaleAmountModel(startdate, enddate, storeid);

                saleModel.startDate = startdate;
                saleModel.endDate = enddate;
                saleModel.storeAccessParameters = " AND storeId='" + storeid + "'";
                var saleData = saleModel.SaleData();

                var inventoryModel = new InventoryModel();
                inventoryModel.shopName = shopname;
                DataTable inventoryData = inventoryModel.InventoryData();

                // number of store
                // number of product
                // number of invoice
                // sale amount 
                var totalStore = 3;
                var totalProducts = inventoryData.Rows.Count;
                var totalInvoice = saleData.Rows.Count;
                var saleAmount = tableData.Rows[0]["netAmt"].ToString() == "" ? "0" : tableData.Rows[0]["netAmt"].ToString();
                var totalSaleAmount = Convert.ToDecimal(saleAmount);

                var saleSummary = new List<object>();
                //saleSummary.Add(new Summary()
                //{
                //    title = "মোট স্টোর",
                //    amount = totalStore.ToString(),
                //    imageurl = "/img/appicon/icon1.svg"
                //});
                saleSummary.Add(new Summary()
                {
                    title = "মোট প্রোডাক্ট",
                    amount = totalProducts.ToString(),
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট ইনভয়েজ",
                    amount = totalInvoice.ToString(),
                    imageurl = "/img/appicon/icon1.svg"
                });
                saleSummary.Add(new Summary()
                {
                    title = "মোট বিক্রির টাকা",
                    amount = totalSaleAmount.ToString(),
                    imageurl = "/img/appicon/icon1.svg"
                });


                statusData.Add(new DataStatus() { status = "200", data = saleSummary });
            }
            catch (Exception)
            {
                statusData.Add(new DataStatus() { status = "403" });
            }

            return statusData;
        }
    }
}

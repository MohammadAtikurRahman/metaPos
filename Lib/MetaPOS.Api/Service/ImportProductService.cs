using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;
using MetaPOS.Api.Models;


namespace MetaPOS.Api.Service
{
     public class ImportProductService
    {
        private CommonFunction commonFunction = new CommonFunction();

        public List<DataStatus> importProductApiReport(string prodID, string apiKey, string shopName)
        {

            // var statusData = new List<DataStatus>();
            var data = new List<DataStatus>();


            //if (!commonFunction.CheckConnectionString(shopName))
            //{
            //    statusData.Add(new DataStatus() { status = "404" });
            //    return statusData;
            //}

            try
            {
                SaleModel saleModel = new SaleModel();

                //saleModel.pCode = pCode;

                var tableData = saleModel.SaleAmountTotalQtyModel(prodID);//startdate, enddate, ata  done

                //saleModel.startDate = startdate;
                //saleModel.endDate = enddate;
                //saleModel.storeAccessParameters = " AND storeId='" + storeid + "'";
                var saleData = saleModel.SaleData();

                //var inventoryModel = new InventoryModel();
                //inventoryModel.shopName = code;
                //DataTable inventoryData = inventoryModel.InventoryData();

                // number of store
                // number of product
                // number of invoice
                // sale amount 

                // var totalStore = 3;

                //var totalProducts = inventoryData.Rows.Count;
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
                    //title = "মোট প্রোডাক্ট",
                    // amount = totalProducts.ToString(),
                    //imageurl = "/img/appicon/icon1.svg"
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


                data.Add(new DataStatus() { status = "200", data = saleSummary });
            }
            catch (Exception)
            {
                data.Add(new DataStatus() { status = "403" });
            }
            return data;
        }


    }
}

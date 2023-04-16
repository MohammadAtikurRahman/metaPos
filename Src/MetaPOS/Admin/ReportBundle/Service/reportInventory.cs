using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.AnalyticBundle.Service
{
    public class ReportInventory
    {
        public List<ListItem> getCategoryDataList(string searchType)
        {
            var categoryModel = new CategoryModel();
            return categoryModel.getCategoryDataListModel(searchType);
        }

        public string  getInventoryReportDataList(string jsonData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonData);
            var inventoryReportModel = new InventoryModel();
            inventoryReportModel.searchType = data["searchType"].Value<string>();
            inventoryReportModel.category = data["category"].Value<string>();
            inventoryReportModel.datetFrom = data["dateFrom"].Value<string>() == "" ? DateTime.Now : data["dateFrom"].Value<DateTime>();
            inventoryReportModel.dateTo = data["dateTo"].Value<string>() == "" ? DateTime.Now : data["dateTo"].Value<DateTime>();
            inventoryReportModel.prodId = data["prodId"].Value<string>();
            inventoryReportModel.status = data["status"].Value<string>();
            inventoryReportModel.userId = data["userId"].Value<string>();


            string storeId = data["storeId"].Value<string>();
            if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                storeId = HttpContext.Current.Session["storeId"].ToString();

            inventoryReportModel.storeId = storeId;
            
            return inventoryReportModel.getInventoryReportModel();
        }
    }
}
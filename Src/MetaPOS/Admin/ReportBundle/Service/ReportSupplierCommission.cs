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
    public class ReportSupplierCommission
    {
        public List<ListItem> getCategoryDataList(string searchType)
        {
            var categoryModel = new CategoryModel();
            return categoryModel.getCategoryDataListModel(searchType);
        }

        public string  getSupplierCommissionReportDataList(string jsonData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonData);
            var supplierCommissionModel = new SupplierCommisionModel();
            supplierCommissionModel.category = data["category"].Value<string>();
            supplierCommissionModel.datetFrom = data["dateFrom"].Value<string>() == "" ? DateTime.Now : data["dateFrom"].Value<DateTime>();
            supplierCommissionModel.dateTo = data["dateTo"].Value<string>() == "" ? DateTime.Now : data["dateTo"].Value<DateTime>();
            supplierCommissionModel.prodId = data["prodId"].Value<string>();
            supplierCommissionModel.userId = data["userId"].Value<string>();

            string storeId = data["storeId"].Value<string>();
            if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                storeId = HttpContext.Current.Session["storeId"].ToString();

            supplierCommissionModel.storeId = storeId;

            return supplierCommissionModel.getSupplierCommissionReportModel();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleService
    {

        private CommonFunction commonFunction = new CommonFunction();
        public bool saveServiceData(string jsonStrData)
        {
            var serviceModel = new ServiceModel();
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);

            serviceModel.name = data["name"].Value<string>();
            serviceModel.type = data["type"].Value<string>();
            serviceModel.retailPrice = data["retailPrice"].Value<decimal>();
            serviceModel.wholePrice = data["wholePrice"].Value<decimal>();
            serviceModel.entryDate = commonFunction.GetCurrentTime();
            serviceModel.updateDate = commonFunction.GetCurrentTime();
            serviceModel.active = '1';
            return serviceModel.saveServiceModel();
        }

        public string getServiceDataListAddToCart(string prodCode)
        {
            var serviceModel = new ServiceModel();

            var dtStock = serviceModel.getServiceDataTableModel(prodCode);
            return commonFunction.serializeDatatableToJson(dtStock);

        }

        public string getServiceDataList(string active)
        {
            var serviceModel = new ServiceModel();
            return serviceModel.getServiceDataListModel(active);
        }

        public bool updateServiceData(string jsonStrData)
        {
            var serviceModel = new ServiceModel();
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);
            serviceModel.name = data["name"].Value<string>();
            serviceModel.type = data["type"].Value<string>();
            serviceModel.retailPrice = data["retailPrice"].Value<decimal>();
            serviceModel.wholePrice = data["wholePrice"].Value<decimal>();
            serviceModel.Id = data["id"].Value<int>();
            return serviceModel.updateServiceDataModel();

        }

        public string searchServiceDataListAddToCart(string billNo, string Id)
        {
            var stockStatusModel = new StockStatusModel();

            var dtService = stockStatusModel.getStockStatusDataListModelByBilNoAndProdCode(billNo, Id);
            return commonFunction.serializeDatatableToJson(dtService);
        }

        public void deleteDraft(dynamic billNo)
        {
            var saleModel = new SaleModel();
            saleModel.billNo = billNo;
            saleModel.deleteDraftModel();
        }
    }
}
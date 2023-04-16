using System;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;


namespace MetaPOS.Admin.AppBundle.Service
{
    public class DataService
    {
        private ModelService modelService = new ModelService();
        private CommonService commonService = new CommonService();
        private CommonFunction commonFunction= new CommonFunction();





        public string getDataList(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            modelService.select = data["select"].Value<string>();
            modelService.from = data["from"].Value<string>();
            modelService.where = commonService.formatWhereInQuery(data["where"].ToString());
            modelService.column = data["column"].Value<string>();
            modelService.dir = data["dir"].Value<string>();

            return modelService.getDataListModel();
        }






        public string getData(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            modelService.select = data["select"].Value<string>();
            modelService.from = data["from"].Value<string>();
            modelService.where = commonService.formatWhereInQuery(data["where"].ToString());

            return modelService.getDataModel();
        }






        public string findData(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            modelService.select = data["select"].Value<string>();
            modelService.from = data["from"].Value<string>();
            modelService.where = commonService.formatWhereInQuery(data["where"].ToString());

            return modelService.findDataModel();
        }

        public string RemoveHTMLTags(string str)
        {
            System.Text.RegularExpressions.Regex rx =
            new System.Text.RegularExpressions.Regex("<[^>]*>");
            str = rx.Replace(str, "");
            return str;
        }



        public string saveData(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);
            var query = data["values"].ToString();
            var querystr = RemoveHTMLTags(query);
            modelService.from = data["from"].Value<string>();
            modelService.values = commonService.formatValuesInQuery(querystr);
            
            var msg = modelService.saveDataModel();

            if (data["from"].Value<string>() == "WarehouseInfo")
            {
                commonFunction.createBranchInfo(false,"");
            }


            return (string) msg;
        }


        public string updateData(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);
            var query = data["set"].ToString();
            var querystr = RemoveHTMLTags(query);
            modelService.from = data["from"].Value<string>();
            modelService.where = commonService.formatWhereInQuery(data["where"].ToString());
            modelService.set = commonService.formatSetInQuery(querystr);

            return modelService.updateDataModel();
        }





        public string deleteData(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            modelService.from = data["from"].Value<string>();
            modelService.where = commonService.formatWhereInQuery(data["where"].ToString());
            modelService.set = commonService.formatSetInQuery(data["set"].ToString());

            return modelService.deleteDataModel();
        }





        public string restoreData(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            modelService.from = data["from"].Value<string>();
            modelService.where = commonService.formatWhereInQuery(data["where"].ToString());
            modelService.set = commonService.formatSetInQuery(data["set"].ToString());

            return modelService.restoreDataModel();
        }




        public string getDataJoinList(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            modelService.select = data["select"].Value<string>();
            modelService.from = data["from"].Value<string>();
            modelService.where = commonService.formatWhereInQuery(data["where"].ToString());
            modelService.column = data["column"].Value<string>();
            modelService.dir = data["dir"].Value<string>();

            return modelService.getDataJoinListModel();
        }

        public string loadCustomerBalanceData(string cusId)
        {
            var cashReportModal = new CashReportModel();
            cashReportModal.descr = cusId;
            return cashReportModal.loadCustomerBalanceDataModel();
        }
    }
}
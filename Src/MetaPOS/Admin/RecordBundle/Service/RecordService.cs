using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.RecordBundle.Service
{


    public class RecordService
    {

        RecordModel recordModel = new RecordModel();
        CommonFunction commonFunction = new CommonFunction();




        public string getRecordInfoList(string jsonStrData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);

            recordModel.select = data["select"].Value<string>();
            recordModel.from = data["from"].Value<string>();
            recordModel.where = commonFunction.formatWhereInQuery(data["where"].ToString());
            recordModel.column = data["column"].Value<string>();
            recordModel.dir = data["dir"].Value<string>();

            var dt = recordModel.getRecordInfoListModel();

            return commonFunction.serializeDatatableToJson(dt);
        }

        public string getBranchInfoList(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            recordModel.select = data["select"].Value<string>();
            recordModel.from = data["from"].Value<string>();
            recordModel.where = commonFunction.formatWhereInQuery(data["where"].ToString());
            recordModel.column = data["column"].Value<string>();
            recordModel.dir = data["dir"].Value<string>();

            var dt = recordModel.getBranchInfoListModel();

            return commonFunction.serializeDatatableToJson(dt);
        }





        public int findRecordInfo(string jsonStrData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);

            recordModel.select = data["select"].Value<string>();
            recordModel.from = data["from"].Value<string>();
            recordModel.where = commonFunction.formatWhereInQuery(data["where"].ToString());

            return recordModel.findRecordInfoModel();
        }





        public bool saveRecordInfo(string jsonStrData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);

            recordModel.from = data["from"].Value<string>();
            recordModel.values = commonFunction.formatValuesInQuery(data["values"]);

            return recordModel.saveRecordInfoModel();
        }





        public bool updateRecordInfo(string jsonStrData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);

            recordModel.from = data["from"].Value<string>();
            recordModel.where = commonFunction.formatWhereInQuery(data["where"].ToString());
            recordModel.set = commonFunction.formatSetInQuery(data["set"].ToString());

            return recordModel.updateRecordInfoModel();
        }





        public bool deleteRecordInfo(string jsonStrData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);

            recordModel.from = data["from"].Value<string>();
            recordModel.where = commonFunction.formatWhereInQuery(data["where"].ToString());
            recordModel.set = commonFunction.formatSetInQuery(data["set"].ToString());

            return recordModel.delteRecordInfoModel();
        }





        public bool restoreRecordInfo(string jsonStrData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonStrData);

            recordModel.from = data["from"].Value<string>();
            recordModel.where = commonFunction.formatWhereInQuery(data["where"].ToString());
            recordModel.set = commonFunction.formatSetInQuery(data["set"].ToString());

            return recordModel.restoreRecordInfoModel();
        }





        public bool printRecordInfo(string jsonStrData)
        {
            return false;
        }


    }


}
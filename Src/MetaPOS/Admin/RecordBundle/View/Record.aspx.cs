using System.Web.Services;
using MetaPOS.Admin.RecordBundle.Service;


namespace MetaPOS.Admin.RecordBundle.View
{


    public partial class Record : System.Web.UI.Page
    {


        [WebMethod]
        public static string getRecordInfoListAction(string jsonStrData)
        {
            var recordService = new RecordService();
            return recordService.getRecordInfoList(jsonStrData);
        }

        [WebMethod]
        public static string getBranchInfoAction(string jsonStrData)
        {
            var recordService = new RecordService();
            return recordService.getBranchInfoList(jsonStrData);
        }

        



        [WebMethod]
        public static int findRecordInfoAction(string jsonStrData)
        {
            var recordService = new RecordService();
            return recordService.findRecordInfo(jsonStrData);
        }





        [WebMethod]
        public static bool saveRecordInfoAction(string jsonStrData)
        {
            var recordService = new RecordService();
            return recordService.saveRecordInfo(jsonStrData);
        }





        [WebMethod]
        public static bool updateRecordInfoAction(string jsonStrData)
        {
            var recordService = new RecordService();
            return recordService.updateRecordInfo(jsonStrData);
        }





        [WebMethod]
        public static bool deleteRecordInfoAction(string jsonStrData)
        {
            var recordService = new RecordService();
            return recordService.deleteRecordInfo(jsonStrData);
        }





        [WebMethod]
        public static bool restoreRecordInfoAction(string jsonStrData)
        {
            var recordUnit = new RecordService();
            return recordUnit.restoreRecordInfo(jsonStrData);
        }





        [WebMethod]
        public static bool printRecordInfoAction(string jsonStrData)
        {
            var recordUnit = new RecordService();
            return recordUnit.printRecordInfo(jsonStrData);
        }


    }


}
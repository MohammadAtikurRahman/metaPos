using System;
using MetaPOS.Admin.DataAccess;
using DataTable = System.Data.DataTable;


namespace MetaPOS.Admin.Model
{


    public class SupplierModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();


        public string supId { get; set; }
        public string supComapny { get; set; }
        public string supPhone { get; set; }
        public string conName { get; set; }
        public string conTitle { get; set; }
        public string conPhone { get; set; }
        public string address { get; set; }
        public string fax { get; set; }
        public string mailInfo { get; set; }
        public string payMethod_ { get; set; }
        public string discount { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }
        public string roleId { get; set; }
        public string active { get; set; }
        public string supCode { get; set; }
        public string refId { get; set; }





        public SupplierModel()
        {
           updateDate = entryDate = commonFunction.GetCurrentTime();
            active = "1";
            discount = "0";
        }


        public DataTable getSupplierApiDataListModel(string getConditionalParameter)
        {
            var query = "SELECT * FROM SupplierInfo WHERE " + getConditionalParameter;
            var dt = sqlOperation.getDataTable(query);
            return dt;
        }



        public bool createSupplierModel()
        {
            string query =
                "INSERT INTO SupplierInfo (supId,supCompany,entryDate,updateDate,roleId) VALUES ('" +
                supId + "','" + supComapny + "','" + entryDate + "','" + updateDate + "','" + roleId + "')";
            return sqlOperation.fireQuery(query);
        }
    }


}
using System;
using System.Data;
using System.Web;
using MetaPOS.Admin.RecordBundle.View;


namespace MetaPOS.Admin.Model
{


    public class SlipModel
    {


        private DataAccess.SqlOperation sqlOperation = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction commonFunction = new DataAccess.CommonFunction();

        // Global perameter
        private string query = "";
        //private DataSet ds;

        // Initialize Database Perameter
        public string billNo { get; set; }
        public string roleId { get; set; }
        public string cusId { get; set; }
        public string prodId { get; set; }
        public string qty { get; set; }
        public decimal netAmt { get; set; }
        public decimal discAmt { get; set; }
        public decimal vatAmt { get; set; }
        public decimal grossAmt { get; set; }
        public string payMethod { get; set; }
        public decimal payCash { get; set; }
        public string payCard { get; set; }
        public decimal giftAmt { get; set; }
        public decimal retuen_ { get; set; }
        public decimal balance { get; set; }
        public string status { get; set; }
        public string branchId { get; set; }
        public string groupId { get; set; }
        public string cusID { get; set; }
        public int prodID { get; set; }
        public decimal return_ { get; set; }
        public DateTime entryDate { get; set; }

        public string storeId { get; set; }
        public decimal miscCost { get; set; }
        public string referredBy { get; set; }




        public SlipModel()
        {
            //roleId = HttpContext.Current.Session["roleId"].ToString();
            //branchId = HttpContext.Current.Session["branchId"].ToString();
            //groupId = HttpContext.Current.Session["groupId"].ToString();
            entryDate = commonFunction.GetCurrentTime();
        }


        public string saveSlipDataModel()
        {

            string query = "INSERT INTO SlipInfo (" +
                                            "billNo, roleId, cusId, prodId, qty, netAmt, discAmt, vatAmt, grossAmt, payMethod, payCash, payCard, giftAmt, return_, balance, entryDate, status, branchId, groupId,storeId,salesPersonId,cusType,isAutoSalesPerson,miscCost,referredBy) " +
                           " SELECT TOP 1 '" + billNo + "', roleID, cusID,prodID, qty, netAmt, discAmt, vatAmt, grossAmt, payMethod, payCash, payCard, giftAmt, return_, balance, '" + entryDate + "', '" + status + "', branchId, groupId,storeId,salesPersonId,cusType,isAutoSalesPerson,'" + miscCost + "','" + referredBy + "' FROM [SaleInfo] WHERE billNo = '" + billNo + "'";

            return query;
        }

        // Create Data SlipInfo Table
        public void createSlip()
        {
            query = "INSERT INTO SlipInfo VALUES ('" +
                    billNo + "','" +
                    HttpContext.Current.Session["roleId"] + "','" +
                    cusId + "','" +
                    prodId + "','" +
                    qty + "','" +
                    netAmt + "','" +
                    discAmt + "','" +
                    vatAmt + "','" +
                    grossAmt + "','" +
                    payMethod + "','" +
                    payCash + "','" +
                    payCard + "','" +
                    giftAmt + "','" +
                    retuen_ + "','" +
                    balance + "','" +
                    commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy") + "','" +
                    status + "','" +
                    HttpContext.Current.Session["branchId"] + "','" +
                    HttpContext.Current.Session["groupId"] + "')";

            sqlOperation.executeQuery(query);
        }





        // Update Data SlipInfo Table
        public void updateSlip()
        {
        }





        // Delete Data SlipInfo Table
        public void deleteSlip()
        {
        }



        public string updateSlipInfoDataClearModel(string billNo)
        {
            string query =
                "UPDATE SlipInfo SET qty='0',netAmt='0',discAmt='0',vatAmt='0',grossAmt='0',payMethod='0'," +
                "payCash='0',giftAmt='0',return_='0',balance='0',status='Suspended' " +
                "WHERE billNo='" + billNo + "'";
            return query;
        }

        public DataTable getSlipInfoTopOneModel(string billNo)
        {
            string query = "SELECT TOP 1 * FROM SlipInfo WHERE billNo = '" + billNo + "' ORDER BY Id DESC";

            return sqlOperation.getDataTable(query);
        }

        public DataTable getSlipInfoModel(string billNo)
        {
            string query = "SELECT * FROM SlipInfo WHERE billNo = '" + billNo + "'";

            return sqlOperation.getDataTable(query);
        }

        public bool updateSlipInfoByDirectCustomerModel(string billNo, string cusId, string preRoleId, string pay)
        {
            string query = "INSERT INTO SlipInfo (" +
                           "billNo," +
                           "roleId," +
                           "cusId," +
                           "prodId," +
                           "qty," +
                           "netAmt," +
                           "discAmt," +
                           "vatAmt," +
                           "grossAmt," +
                           "payMethod," +
                           "payCash," +
                           "payCard," +
                           "giftAmt," +
                           "return_," +
                           "balance," +
                           "entryDate," +
                           "status," +
                           "branchId," +
                           "groupId) " +

                           "SELECT " +
                           "billNo," +
                           "roleId," +
                           "cusId," +
                           "prodId," +
                           "qty," +
                           "netAmt," +
                           "discAmt," +
                           "vatAmt," +
                           "grossAmt," +
                           "payMethod," +
                           "" + pay + "," +
                           "payCard," +
                           "giftAmt-" + pay + "," +
                           "return_," +
                           "balance + " + pay + "," +
                           "entryDate," +
                           "status," +
                           "branchId," +
                           "groupId FROM SlipInfo where billNo='" + billNo + "'";
            return sqlOperation.fireQuery(query);
        }
    }


}
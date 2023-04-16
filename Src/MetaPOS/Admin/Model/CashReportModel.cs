using System;
using System.Data;
using System.Web;


namespace MetaPOS.Admin.Model
{


    public class CashReportModel
    {


        private DataAccess.SqlOperation objSqlOperation = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();

        //Global Variable
        private string query = "";
        //private DataSet ds;

        // Initialize Database Perameter
        public string cashType { get; set; }
        public string descr { get; set; }
        public decimal cashIn { get; set; }
        public decimal cashOut { get; set; }

        public decimal cashInHand { get; set; }
        //entry date
        public string billNo { get; set; }
        public string mainDescr { get; set; }
        //roleID
        //branchId
        //group Id
        public char status { get; set; }
        public char adjust { get; set; }
        public DateTime entryDate { get; set; }

        public string payMethod { get; set; }
        public string payType { get; set; }
        public string payDescr { get; set; }




        public CashReportModel()
        {
            cashType = "";
            descr = "";
            cashIn = 0;
            cashOut = 0;
            cashInHand = 0;
            billNo = "";
            mainDescr = "";
            status = '0';
            adjust = '0';
            entryDate = objCommonFun.GetCurrentTime();
            payMethod = "";
            payType = "";
            payDescr = "";
        }

        public string saveCustomerCashData()
        {
            string queryCustCash =
                "INSERT INTO CashReportInfo (cashType,descr,cashIn,cashOut,entryDate,billNo,status,roleId,branchId,groupId,storeId,payMethod,payType,payDescr) VALUES ('" +
                cashType + "','" + descr + "','" + cashIn + "','" + cashOut + "','" + entryDate + "','" + billNo + "','" +
                status + "','" + HttpContext.Current.Session["roleId"] + "','" + HttpContext.Current.Session["branchId"] +
                "','" + HttpContext.Current.Session["groupId"] + "','" + HttpContext.Current.Session["storeId"] + "','" + payMethod + "','" + payType + "','" + payDescr + "') ";
            return queryCustCash;
        }



        // Insert Data CashReprotInfo Table
        public void createCashReport()
        {
            query = "INSERT INTO CashReportInfo VALUES('N" +
                    cashType + "','" +
                    descr + "','" +
                    cashIn + "','" +
                    cashOut + "','" +
                    cashInHand + "','" +
                    objCommonFun.GetCurrentTime().ToString("MM/dd/yyyy") + "','" +
                    billNo + "','" +
                    mainDescr + "','" +
                    HttpContext.Current.Session["roleId"] + "','" +
                    HttpContext.Current.Session["branchId"] + "','" +
                    HttpContext.Current.Session["groupId"] + "','" +
                    status + "','" +
                    adjust + "')";


            objSqlOperation.executeQuery(query);
        }


        

        public DataTable getCashReportDataModel(string billNo)
        {
            string query = "SELECT * FROM CashReportInfo WHERE billNo='" + billNo + "'";
            return objSqlOperation.getDataTable(query);
        }

        public bool saveCashReportInfoByDirectCustomerModel(string billNo, string cusId, string preRoleId, string pay)
        {
            string query =
                "INSERT INTO CashReportInfo (cashType,descr,cashIn,cashout,cashInHand,entrydate,billNo,roleId,branchId,status,storeId) VALUES ('Sales Cash', '" +
                billNo + "'," + pay + ",'0','0','" + objCommonFun.GetCurrentTime() + "','" + billNo + "','" +
                HttpContext.Current.Session["roleId"] + "','" + HttpContext.Current.Session["branchId"] + "','5','" + HttpContext.Current.Session["storeId"] + "')";
            return objSqlOperation.fireQuery(query);
        }





        public string saveCustomerCashHistory()
        {
            string query =
                "INSERT INTO CashReportInfo (cashType,descr,cashIn,cashOut,entryDate,billNo,status,roleId,branchId,groupId,storeId,payMethod) VALUES ('" +
                cashType + "','" + descr + "','" + cashIn + "','" + cashOut + "','" + entryDate + "','" + billNo + "','" +
                status + "','" + HttpContext.Current.Session["roleId"] + "','" + HttpContext.Current.Session["branchId"] +
                "','" + HttpContext.Current.Session["groupId"] + "','" + HttpContext.Current.Session["storeId"] + "','" + payMethod + "') ";
            return objSqlOperation.executeQuery(query);
        }

        public string loadCustomerBalanceDataModel()
        {
            string query = "SELECT SUM(cashOut) - SUM(cashIn) as balance FROM CashReportInfo WHERE descr = '" + descr + "' AND status ='6'";
            var dtBalance = objSqlOperation.getDataTable(query);
            return dtBalance.Rows[0][0].ToString();
        }

        public DataTable getCashReportSaleRecordModel(string storeAccessParameters, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return objSqlOperation.getDataTable("SELECT SUM(cashIn) as cashIn, SUM(cashout) as cashout FROM CashReportInfo WHERE status='5' AND (entryDate BETWEEN '" + dateTimeFrom.ToShortDateString() + "' AND '" + dateTimeTo.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }



        public DataTable getCashReportSaleRecordByCashTypeModel(string storeAccessParameters, DateTime dateTimeFrom, DateTime dateTimeTo, string cashType)
        {
            return objSqlOperation.getDataTable("SELECT SUM(cashIn) as cashIn, SUM(cashout) as cashout FROM CashReportInfo WHERE cashType ='" + cashType + "' AND (entryDate BETWEEN '" + dateTimeFrom.ToShortDateString() + "' AND '" + dateTimeTo.AddDays(1).ToShortDateString() + "') " + storeAccessParameters + "");
        }

        public DataTable getCashReportByPayMethodModel(string storeAccessParameters, int payMethod, DateTime dateFrom, DateTime dateTo)
        {
            //query =
            //    "SELECT SUM(cashIn) as balance FROM (select DISTINCT cashReport.BillNo,cashReport.cashIn from CashReportInfo cashReport LEFT JOIN SaleInfo sale ON sale.billNo=cashReport.billNo where cashReport.payMethod='" +
            //    payMethod + "' AND cashReport.status='5' AND (CAST(cashReport.entryDate as Date) BETWEEN '" +
            //    dateFrom.ToShortDateString() + "' AND '" + dateTo.AddDays(1).ToShortDateString() + "') " +
            //    storeAccessParameters + ") as sale";

            query = "SELECT SUM(cashin)-SUM(cashout) as balance FROM CashReportInfo cashReport where cashReport.payMethod='" + payMethod + "' AND cashReport.status='5' AND cashReport.cashType !='Discount' AND cashType !='Invoice' AND cashType !='Product Return' AND cashtype!='Suspended' AND (CAST(cashReport.entryDate as Date) BETWEEN '" +
               dateFrom.ToShortDateString() + "' AND '" + dateTo.AddDays(1).ToShortDateString() + "') " +
               storeAccessParameters + "";

            return objSqlOperation.getDataTable(query);
        }
    }


}
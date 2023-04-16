using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using System.Web.Services;
using MetaPOS.Admin.ApiBundle.Controllers;
using MetaPOS.Admin.CustomerBundle.Service;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.View;
using MetaPOS.Admin.SaleBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ClosedXML.Excel;


namespace MetaPOS.Admin
{

    public partial class Customer : BasePage
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();



        //private DataSet ds;

        //private static string selectedRowID = "",
        //    query = "",
        //    isBoolGroup = "",
        //    isMemberId = "",
        //    isAdvanced = "",
        //    isPayOpeningDue = "",
        //    temp;

        //private static string isDateWiseSearch = "", CustomerId = "", cusType = "0";


        public enum MessageType
        {


            Success,
            Error,
            Info,
            Warning


        };





        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                lblHiddenCompanyName.Value = Session["comName"].ToString();
                lblHiddenCompanyAddress.Value = Session["comAddress"].ToString();
                lblHiddenCompanyPhone.Value = Session["comPhone"].ToString();
            }
            catch (Exception)
            {
                
            }

            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("Customer"))
                {
                    commonFunction.pageout();
                }
            }

            var isCustomerSummaryExport = commonFunction.findSettingItemValueDataTable("isCustomerSummaryExport");
            if (isCustomerSummaryExport == "0")
                btnCustomerSummary.Visible = false;
        }



        [WebMethod]
        public static string SaveCustomerDataAction(string jsonStrData)
        {
            var customerUpsert = new CustomerUpsert();
            return customerUpsert.saveCustomerInfo(jsonStrData);
        }





        [WebMethod]
        public static bool updateCustomerInfoDataAction(string jsonData)
        {
            var customerUpsert = new CustomerUpsert();
            return customerUpsert.updateCustomerInfoData(jsonData);

        }




        [WebMethod]
        public static string UpdateCustomerDataAction(string jsonStrData)
        {
            var customerUpsert = new CustomerUpsert();
            return customerUpsert.updateCustomerInfo(jsonStrData);
        }





        [WebMethod]
        public static string changeCustomerUpdateInfoAction(string cusId)
        {
            var objCustomerUpsert = new CustomerUpsert();
            return objCustomerUpsert.changeCustomerUpdateInfo(cusId);
        }


        [WebMethod]
        public static List<ListItem> getCustomerListAction()
        {
            var objCustomerUpsert = new CustomerUpsert();
            return objCustomerUpsert.getCustomerList();
        }

        //invoice page customer search
        [WebMethod]
        public static List<ListItem> searchCustomerListForLoadAction(string searchTxt)
        {
            var objCustomerUpsert = new CustomerUpsert();
            return objCustomerUpsert.searchCustomerList(searchTxt);
        }




        //show customer in customer page
        [WebMethod]
        public static string getCustomerDataListAction(string jsonData)
        {
            var customerSelectData = new CustomerSelectData();
            return customerSelectData.getCustomerDataList(jsonData);
        }





        [WebMethod]
        public static string loadViewCustomerDataAction(string jsonData)
        {
            var customerSelectData = new CustomerSelectData();
            return customerSelectData.getCustomerDataList(jsonData);
        }





        [WebMethod]
        public static bool saveCustomerAdvanceAmountAction(string jsonData)
        {
            var customerAdvance = new CustomerAdvance();
            var executeableQuery = "BEGIN TRANSACTION ";
            executeableQuery += customerAdvance.saveCustomerAdvanceAmount(jsonData);
            executeableQuery += "COMMIT ";

            var sqlOperation = new SqlOperation();
            return sqlOperation.fireQuery(executeableQuery);

        }





        [WebMethod]
        public static bool saveCustomerOpeingDueAmountAction(string jsonData)
        {
            var customerOpeingDue = new CustomerOpeingDue();
            var executeableQuery = "BEGIN TRANSACTION ";
            executeableQuery += customerOpeingDue.saveCustomerOpeingDueAmount(jsonData);
            executeableQuery += "COMMIT ";

            var sqlOperation = new SqlOperation();
            return sqlOperation.fireQuery(executeableQuery);

        }



        [WebMethod]
        public static string saveCustomerPaymentAmountAction(string jsonData)
        {
            var customerOpeingDue = new CustomerOpeingDue();

            var installment = new InstallmentBundle.Service.InstallmentCustomer();
            var isInstallmentDue = installment.keepInstallmentDueAfterPayment(jsonData);
            var messageJson = (JObject)JsonConvert.DeserializeObject(isInstallmentDue);
            var status = messageJson["status"].Value<string>();

            if (status == "True")
            {
                var executeableQuery = "BEGIN TRANSACTION ";
                executeableQuery += customerOpeingDue.saveCustomerPaymentAmount(jsonData);
                executeableQuery += "COMMIT ";

                var sqlOperation = new SqlOperation();
                var isSaved = sqlOperation.fireQuery(executeableQuery);
                if (isSaved)
                {
                    return "true|Payment successfully";
                }
                else
                {
                    return "false|Payment is not complete";
                }
            }
            else
            {
                return "false|" + messageJson["message"].Value<string>();
            }

        }










        [WebMethod]
        public static string fullCustomerLedgerPrintAction(string jsonData)
        {
            var customerLedger = new customerLedgerPrint();
            return customerLedger.fullCustomerLedgerPrint(jsonData);
        }

        [WebMethod]
        public static bool delRestoreCustomerDataAction(string jsonData)
        {
            var customerDelRestore = new CustomerDelete();
            return customerDelRestore.delRestoreCustomerData(jsonData);
        }


        protected void btnLedgerPrint_OnClick(object sender, EventArgs e)
        {
            string dateTo, dateForm;
            var cusId = txtLederCusId.Text;
            var dateToStr = txtDateTo.Text;
            var dateFormStr = txtDateForm.Text;

            if (dateToStr == "")
                dateTo = "2000";
            else
                dateTo = dateToStr;

            if (dateFormStr == "")
                dateForm = commonFunction.GetCurrentTime().ToShortDateString();
            else
                dateForm = dateFormStr;




            string query = "SELECT cash.cashType,cash.descr,cash.cashIn,cash.cashOut,cash.entryDate,cash.billNo,cash.status,cus.name,cus.phone,cus.address,cus.mailInfo,cus.AccountNo,cus.installmentStatus from cashreportinfo as cash LEFT JOIN CustomerInfo as cus ON cash.descr= cus.cusID  where cash.descr ='" + cusId + "' AND cash.entryDate <= '" + dateForm + "' AND cash.entryDate >='" + dateTo + "' AND status !='5' ORDER BY cash.Id ASC";

            Session["pageName"] = "CustomerLedgerReport";
            HttpContext.Current.Session["reportName"] = "Customer Ledger";
            Session["reportQury"] = query;


            Response.Redirect("~/Admin/Print/LoadQuery.aspx");
        }





        protected void btnCustomerSummary_OnClick(object sender, EventArgs e)
        {
            string role = HttpContext.Current.Session["roleId"].ToString();
            string query = "SELECT MIN(cus.cusID) as cusID,MIN(cus.name) as name,MIN(cus.phone) as phone,MIN(cus.address) as address, SUM(sale.netamt) as netAmount FROM customerinfo as cus INNER JOIN saleinfo as sale on cus.cusID=sale.cusID AND cus.roleID = '" + role + "' group by cus.cusID";
            ExportGridToExcel(query, "Customer_summary");
        }


        private void ExportGridToExcel(string queryExport, string exportFileName)
        {
            var commonFunction = new CommonFunction();

            using (SqlConnection con = new SqlConnection())
            {
                var constr = GlobalVariable.getConnectionStringName();
                con.ConnectionString = ConfigurationManager.ConnectionStrings[constr].ConnectionString;

                using (SqlCommand cmd = new SqlCommand(queryExport))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "Customer");

                                string fileName = exportFileName + "_" + commonFunction.GetCurrentTime() + ".xlsx";

                                Response.Clear();
                                Response.Buffer = true;
                                Response.ContentEncoding = System.Text.Encoding.UTF8;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

        }

    }




}
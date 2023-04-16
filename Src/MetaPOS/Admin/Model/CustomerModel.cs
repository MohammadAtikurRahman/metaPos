using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.SaleBundle.Entity;
using DataTable = System.Data.DataTable;


namespace MetaPOS.Admin.Model
{


    public class CustomerModel : Customers
    {
        private Controller.CommonController commonController = new Controller.CommonController();
        private Model.SaleModel saleModel = new Model.SaleModel();

        private CommonFunction commonFunction = new CommonFunction();
        private SqlOperation sqlOperation = new SqlOperation();
        private DataSet ds;


        // Constractor
        public CustomerModel()
        {
            totalPaid = 0;
            totalDue = 0;
            bloodGroup = "";
            memberId = "";
            openingDue = 0;
            phone = "";
            mailInfo = "";
            address = "";
            installmentStatus = false;
            designation = "";


        }






        // Generate customer new id
        public string generateCustomerId()
        {
            ds = listCustomer();

            try
            {
                int currentInt = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                ++currentInt;
                nextCusID = currentInt.ToString("0000000");
            }
            catch
            {
                nextCusID = "0000001";
            }
            return nextCusID;
        }





        //Insert customer data
        public void createCustomer()
        {
            string query = "INSERT INTO CustomerInfo VALUES ('" +
                    nextCusID + "','" +
                    name + "','" +
                    phone + "','" +
                    address + "','" +
                    mailInfo + "','" +
                    commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy") + "','" +
                    commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy") + "','" +
                    '0' + "','" +
                    HttpContext.Current.Session["roleId"] + "', '" +
                    totalDue + "','" +
                    refName + "','" +
                    refPhone + "','" +
                    refAddress + "','" +
                    HttpContext.Current.Session["branchId"] + "','" +
                    HttpContext.Current.Session["groupId"] + "','" +
                    phone2 + "','" +
                    '0' + "','" +
                    bloodGroup + "','" +
                    Convert.ToInt32(orderId) + "','" +
                    memberId + "', '" +
                    memberId + "','" + openingDue + "')";

            sqlOperation.executeQuery(query);
        }






        // Update customer data
        public void updateCustomer(string getFormatUpdateItemData, string getFormatedConditinalParameter)
        {
            string query = "UPDATE CustomerInfo SET " + getFormatUpdateItemData + " WHERE " + getFormatedConditinalParameter +
                    "";
            sqlOperation.executeQuery(query);
        }





        // Get customer data
        public DataSet getCustomerByCondition(string getConditionalParameter)
        {
            string query = "SELECT * FROM CustomerInfo WHERE " + getConditionalParameter + "";
            ds = sqlOperation.getDataSet(query);
            return ds;
        }




        // List of customer data
        public DataSet listCustomer()
        {
            string query = "SELECT cusID FROM CustomerInfo WHERE cusID !='0' ORDER BY cusID DESC";
            ds = sqlOperation.getDataSet(query);
            return ds;
        }




        //save customer data 
        public string saveCustomerInfoModel()
        {
            try
            {
                string newCusID = generateCustomerId();
                if (accountNo == "")
                    accountNo = newCusID;

                string query = "INSERT INTO CustomerInfo " +
                        "(cusId,name,phone,address,mailInfo,entryDate,updateDate,roleId,branchId,groupId,CusType,notes,accountNo,installmentStatus,designation,bloodGroup,sex,age ) VALUES ('" +
                    newCusID + "', N'" +
                    name + "','" +
                    phone + "',N'" +
                    address + "','" +
                    mailInfo + "','" +
                    commonFunction.GetCurrentTime() + "','" +
                    commonFunction.GetCurrentTime() + "','" +
                    HttpContext.Current.Session["roleId"] + "','" +
                    HttpContext.Current.Session["branchId"] + "','" +
                    HttpContext.Current.Session["groupId"] + "','" +
                    CusType + "', N'" +
                    notes + "',N'" +
                    accountNo + "','" +
                    installmentStatus + "','" +
                    designation + "','" +
                    bloodGroup + "','" +
                    sex + "','" +
                     age + "')";

                sqlOperation.executeQuery(query);
                return newCusID;
            }
            catch (Exception)
            {
                return "0";
            }
        }





        public string updateCustomerInfoModel()
        {
            try
            {
                string query = "UPDATE  CustomerInfo SET updateDate='" + commonFunction.GetCurrentTime() +
                               "',totalPaid =totalPaid+" + totalPaid +
                               ",totalDue=" + totalDue + ", openingDue =" + openingDue + ", notes=N'" + notes + "' WHERE cusId = '" + cusId +
                               "'";
                sqlOperation.executeQuery(query);
                return cusId;
            }
            catch (Exception)
            {
                return "0";
            }
        }




        public List<ListItem> getCustomerModel()
        {
            string role = HttpContext.Current.Session["branchId"].ToString();
            string query = "SELECT CusID,name,phone FROM CustomerInfo WHERE active='1' " + HttpContext.Current.Session["showroomAccessParameters"]+ " ORDER BY CusID DESC";


            var customers = new List<ListItem>();
            string txtView = "";
            var dtCustomer = sqlOperation.getDataTable(query);
            foreach (DataRow row in dtCustomer.Rows)
            {
                txtView = row["CusID"] + " (" + row["name"] + ", " + row["phone"] + ")";

                customers.Add(new ListItem(txtView, row["CusID"].ToString()));
            }

            return customers;
        }

        //invoice page customer search
        public List<ListItem> SearchCustomerModel(string searchTxt)
        {
            //string role = HttpContext.Current.Session["roleId"].ToString();

            if (commonFunction.findSettingItemValueDataTable("isSeparateStore") == "1")
                parameterAccess = HttpContext.Current.Session["roleIdAccessStoreWise"].ToString();
            else
                parameterAccess = HttpContext.Current.Session["userAccessParameters"].ToString();

            string query = "SELECT TOP 20 cusID,name,phone FROM CustomerInfo WHERE (LOWER(name) LIKE '%" + searchTxt.ToLower() + "%' OR CusID LIKE '%" + searchTxt + "%' OR phone LIKE '%" + searchTxt + "%'  )  AND (active='1' OR '1' = 'All')  AND (CusType='All' OR 'All' = 'All')  " + parameterAccess + " ORDER BY CusID DESC";//AND ( roleId = '" + role + "' OR roleId = '" + role + "' )


            var customers = new List<ListItem>();
            string txtView = "";
            var dtCustomer = sqlOperation.getDataTable(query);

            foreach (DataRow row in dtCustomer.Rows)
            {
                txtView = row["CusID"] + " (" + row["name"] + ", " + row["phone"] + ")";

                customers.Add(new ListItem(txtView, row["CusID"].ToString()));
            }

            return customers;
        }



        public bool updateCustomerAdvance(string cusId, decimal advanceAmt)
        {
            return sqlOperation.fireQuery("UPDATE CustomerInfo SET advance = '" + advanceAmt + "' where cusId='" + cusId + "'");
        }

        public string getCustomerDataSerilize(string cusId)
        {
            string query = "SELECT * FROM CustomerInfo WHERE active='1' AND cusId='" + cusId + "'";
            DataTable dt = sqlOperation.getDataTable(query); ;
            return commonFunction.serializeDatatableToJson(dt);
        }


        public bool updateCustomerModel(string cusId, string pay)
        { 
            string query = "UPDATE CustomerInfo  SET totalPaid=totalPaid+" + pay + ", totalDue=totalDue-" + pay +
                           " where cusId='" + cusId + "'";
            return sqlOperation.fireQuery(query);
        }




        // Get Customer data for API
        public DataTable getCustomerApiDataListModel(string getConditionalParameter)
        {
            string query = "SELECT * FROM CustomerInfo WHERE " + getConditionalParameter;
            var dt = sqlOperation.getDataTable(query);
            return dt;
        }

        //customer page shows data
        public string getCustomerListSerializeModel()
        {
            string query = "SELECT Id,cusID,name,phone,installmentStatus,address,mailInfo,notes,CusType,AccountNo,designation, (SELECT SUM(cashOut)-SUM(cashIn) FROM CashReportInfo where descr = cusID AND status='6' ) as balance FROM CustomerInfo WHERE (active='" + active + "' OR '" + active + "' = 'All')  AND (CusType='" + CusType + "' OR '" + CusType + "' = 'All') " + parameterAccess;
            var dt = sqlOperation.getDataTable(query);
            return commonFunction.serializeDatatableToJson(dt);
        }


        public string getCustomerListOfflineSerializeModel()
        {
            string query = "SELECT Id as id,cusID,name,phone,address,mailInfo as email, '0'  as balance,CusType as type,installmentStatus as status FROM CustomerInfo WHERE (active='" + active + "' OR '" + active + "' = 'All')  AND (CusType='" + CusType + "' OR '" + CusType + "' = 'All') " + parameterAccess;
            var dt = sqlOperation.getDataTable(query); ;
            return commonFunction.serializeDatatableToJson(dt);
        }


        public bool delRestoreCustomerDataModel()
        {
            return sqlOperation.fireQuery("UPDATE CustomerInfo SET active='" + active + "' WHERE cusID='" + cusId + "'");

        }


        public bool updateCustomerDataModel()
        {
            string query = "UPDATE CustomerInfo SET name='" + name + "',phone='" + phone + "',address='" + address +
                           "',mailInfo ='" + mailInfo + "',notes='" + notes + "',accountNo='" + accountNo + "' where cusId='" +
                           cusId + "'";
            return sqlOperation.fireQuery(query);
        }


        public DataTable getCustomerListByDateModel(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            string role = HttpContext.Current.Session["roleId"].ToString();
            return
                sqlOperation.getDataTable("SELECT * FROM CustomerInfo WHERE active='1' AND CAST(entryDate as date) >= '" + dateTimeFrom + "' AND CAST(entryDate as date) <= '" + dateTimeTo + "' AND ( roleId = '" + role + "' )");

        }


        public DataTable getCustomerDataModel()
        {
            return sqlOperation.getDataTable("SELECT * FROM CustomerInfo WHERE cusId = '" + cusId + "'");
        }


        public string updateCustomerDataModelForBatchQuery()
        {
            string query = "UPDATE CustomerInfo SET name='" + name + "',phone='" + phone + "',address='" + address +
                           "',mailInfo ='" + mailInfo + "',notes='" + notes + "',accountNo='" + accountNo + "' where cusId='" +
                           cusId + "'";
            return query;
        }

    }


}
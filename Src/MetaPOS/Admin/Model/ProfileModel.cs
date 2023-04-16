using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using System.Data;


namespace MetaPOS.Admin.Model
{
    public class ProfileModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private  CommonFunction  commonFunction = new CommonFunction();

        public string Comany { get; set; }

        public string header { get; set; }

        public string Footer { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string OwnerNumber { get; set; }

        public string Vat { get; set; }

        public string Tax { get; set; }

        public string Url { get; set; }

        public string Header { get; set; }

        public string storeId { get; set; }

        public string SaveProfileDataModel()
        {
            string query = "Update BranchInfo SET branchName='" + Comany + "', branchAddress='" + Header +
                           "',invoiceFooterNote='" + Footer + "',branchPhone='" + Phone + "',branchMobile='" + Mobile + "',ownerNumber='" + OwnerNumber + "',branchVatRegNo='" + Vat + "',branchTaxIdNo='" + Tax + "',branchWebsite='" + Url + "' where storeId='" + storeId + "'";
            return sqlOperation.executeQuery(query);
        }


        public DataTable getStoreListModel()
        {
            string condition = "";
            if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
            {
                var dtRole =
                    sqlOperation.getDataTable("Select * from RoleInfo where branchId = '" +
                                              HttpContext.Current.Session["roleId"] + "' OR roleId ='" +
                                              HttpContext.Current.Session["roleId"] + "'");
                for (int i = 0; i < dtRole.Rows.Count; i++)
                {
                    condition += " roleId ='" + dtRole.Rows[i][0] + "' OR ";
                }

                condition = condition.Substring(0, condition.Length - 3);
            }
            else
            {
                condition = " roleId =" + HttpContext.Current.Session["branchId"].ToString();
            }

            string query = "SELECT * FROM WarehouseInfo WHERE name != '' and (" + condition + ")";
            return sqlOperation.getDataTable(query);
        }

        public string loadProfileDataModel(string storeId)
        {
            var dtProfile = sqlOperation.getDataTable("SELECT * FROM BranchInfo WHERE storeId ='" + storeId + "'");
            if (dtProfile.Rows.Count > 0)
                return commonFunction.serializeDatatableToJson(dtProfile);
            else
                return "";
        }
    }
}
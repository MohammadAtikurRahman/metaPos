using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class AccountModel
    {
        private CommonFunction commonFunction = new CommonFunction();
        private SqlOperation sqlOperation = new SqlOperation();

        public string subDomain { get; set; }
        public string dbName { get; set; }
        public string dbLoginId { get; set; }
        public string dbLoginPwd { get; set; }
        public string dbServer { get; set; }
        public string dbType { get; set; }
        public char status { get; set; }
        public string connectionName { get; set; }

        public string name { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string city { get; set; }
        public string company { get; set; }
        public string address { get; set; }

        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime activeDate { get; set; }
        public DateTime expiryDate { get; set; }
        public decimal monthlyFee { get; set; }


        public string roleId { get; set; }
        public string storeId { get; set; }
        public string accessPage { get; set; }
        public string accessTo { get; set; }


        public AccountModel()
        {

            entryDate = commonFunction.GetCurrentTime();
            updateDate = commonFunction.GetCurrentTime();
        }

       

        public string saveRoleModel()
        {
            //INSERT INTO [RoleInfo] ([roleID], [title], [password], [email], [branchId], [accessPage], [accessTo], [entryDate], [updateDate], [groupId], [userRight], [version] ,[domainName],[isDomainActive],[branchLimit],[userLimit],[branchType],[inheritId],[storeId],[monthlyFee],[ExpiryDate]) VALUES (@lblTopId, @title, @lblPassword, @email, @lblSearchBranchId, @lblAccessPageList, @lblAccessToList, @lblCurrentDate, @lblCurrentDate, @lblSearchRoleId, @lblSearchUserRight, @lblVersion, @lblDomainName,@lblIsDomainActive,@lblBranchLimit, @lblUserLimit,@lblBranchType,@lblInheritId,@lblStoreId,@lblMonthlyFee,@lblExpiryDate)
            string query = "INSERT INTO [RoleInfo] ([roleID], [title], [password], [email], [branchId], [accessPage], [accessTo], [entryDate], [updateDate], [groupId], [userRight], [version] ,[domainName],[isDomainActive],[branchLimit],[userLimit],[branchType],[inheritId],[storeId],[monthlyFee],[ExpiryDate]) VALUES ('" +
                                                    roleId + "','" + company + "','" + password + "','" + email + "','0','" + accessPage + "','" + accessTo + "','" + entryDate + "','" + updateDate + "','0','Branch','5.0','','" + false + "','1','2','','','" + storeId + "','0','" + entryDate.AddDays(30) + "')";
            return sqlOperation.executeQueryWithoutAuth(query);
        }

        public bool saveBranchModel()
        {
            string query = "INSERT INTO [BranchInfo] (Id, entryDate, updateDate, userRight,storeId) VALUES ('" +
                           roleId + "', '" + commonFunction.GetCurrentTime().ToShortDateString() + "', '" +
                           commonFunction.GetCurrentTime().ToShortDateString() + "', 'Branch','" + storeId + "')";
            return sqlOperation.fireQuery(query);
        }


        public bool updateRole()
        {
            string query = "UPDATE RoleInfo SET title='" + name + "',email='" + email + "',password='" +
                commonFunction.Encrypt(password) + "',entryDate='" + entryDate + "',updateDate='" + updateDate + "',activeDate='" +
                activeDate + "',expiryDate='" + expiryDate + "',monthlyFee='" + monthlyFee + "',domainName='" + name + "' where roleId='3'";
            return sqlOperation.fireQueryWithoutAuth(query);
        }

        public bool updateBranch()
        {
            string query = "UPDATE BranchInfo SET branchName='" + company + "',branchCity='" + city + "',branchAddress='" + address + "',branchPhone='" +
                           number + "',branchMobile='" + number + "',branchWebsite='" + company + "',entryDate='" + entryDate + "',updateDate='" +
                           updateDate + "' where id='3'";
            return sqlOperation.fireQueryWithoutAuth(query);
        }


        public DataTable checkRegistationEmailModel(string email)
        {
            string query = "SELECT * FROM RoleInfo where email='" + email + "'";
            return sqlOperation.getDataTable(query);
        }





        public string defaultUserLogsModel()
        {
            string query = "INSERT INTO [UserLogsInfo] ([roleId], [branchId], [userRight], [email], [ipAddress],  [loginDate]) VALUES ('3','0','Branch','" + email + "','168.192.1.0','" + DateTime.Now + "')";
            return sqlOperation.executeQueryWithoutAuth(query);
        }


    }
}
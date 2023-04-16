using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using DataTable = System.Data.DataTable;

namespace MetaPOS.Admin.Model
{
    public class BankModel
    {
        private SqlOperation sqlOperation = new SqlOperation();


        public DataTable GetBnakNameModel(string bankId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT bankName FROM BankNameInfo WHERE Id='" + bankId + "'");
            return dt;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.DataAccess
{
    public class BatchProcess
    {
        public string addBatch(string sql)
        {
            return "BEGIN " + sql + " END ";
        }

        public string executeBatch(string sql)
        {
            string prepareStatement = "BEGIN TRANSACTION " + sql + " COMMIT ";

            var sqlOperation = new SqlOperation();
            return sqlOperation.executeQueryWithoutAuth(prepareStatement);
        }


    }
}
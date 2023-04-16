using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.DataAccess
{
    public static class DefaultSqlConnection
    {
        public static SqlConnection vcon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbPOS"].ToString());
    }
}
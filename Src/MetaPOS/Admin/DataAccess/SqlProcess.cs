using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.DataAccess
{
    public class SqlProcess
    {
        public string sqlStatement(Dictionary<string, string> fields, string table)
        {
            //Dictionary<string, string> fields = new Dictionary<string, string>();
            //fields.Add("LoginEmail", "khasan@gmail.com");
            //fields.Add("Password", "hasan");
            //fields.Add("ContactName", "kamrul");
            //fields.Add("City", "Dhaka");

            string sql = String.Format("INSERT INTO " + table + "({0}) VALUES('{1}')",
                           listKeys(fields.Keys),
                           listValues(fields.Values));
            return sql;
        }


        string listKeys<T>(IEnumerable<T> enumerable)
        {
            List<T> list = new List<T>(enumerable);
            return string.Join(",", list.ToArray());
        }

        string listValues<T>(IEnumerable<T> enumerable)
        {
            List<T> list = new List<T>(enumerable);
            return string.Join("','", list.ToArray());
        }

    }
}
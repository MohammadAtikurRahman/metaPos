using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;



namespace MetaPOS.Admin.AppBundle.Service
{
    public class CommonService
    {
        public string serializeDatatableToJson(DataTable dt)
        {
            var serializer = new JavaScriptSerializer();
            var rows = new List<Dictionary<string, object>>();

            foreach(DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();

                foreach(DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }





        public string formatWhereInQuery(string jsonValues)
        {
            var splitValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonValues);

            var formatWhere = "";

            foreach(var item in splitValues)
            {
                if(item.Key.ToLower().Trim() == "and" ||
                    item.Key.ToLower().Trim() == "or" ||
                    item.Key == "(" ||
                    item.Key == ")")
                {
                    formatWhere += item.Key + " ";
                }
                else if (item.Key == "between")
                {
                    formatWhere += item.Value;
                }
                else if (item.Value.Contains(","))
                {
                    string[] splitValue = item.Value.Split(',');

                    for (int i = 0; i < splitValue.Length; i++)
                    {
                        if (i == 0)
                            formatWhere += " (";
                        else
                            formatWhere += " OR ";
                        
                        formatWhere += item.Key + "='" + splitValue[i] + "' ";

                        if(i == splitValue.Length-1)
                            formatWhere += ") ";
                    }
                }
                else
                {
                    formatWhere += item.Key + "='" + item.Value + "' ";
                }
            }

            formatWhere = formatWhere.Replace("@", "'");

            return formatWhere;
        }





        public string formatSetInQuery(string jsonValues)
        {
            var splitValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonValues);

            var formatWhere = "";

            foreach(var item in splitValues)
            {
                formatWhere += item.Key + "='" + item.Value + "',";
            }

            return formatWhere.TrimEnd(',');
        }





        public string formatValuesInQuery(string jsonValues)
        {
            var splitValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonValues);

            var formatColumns = "(";
            var formatValues = "VALUES (";

            foreach(var item in splitValues)
            {
                formatColumns += item.Key + ",";
                formatValues += "N'" + item.Value + "',";

            }

            formatColumns = formatColumns.TrimEnd(',') + ") ";
            formatValues = formatColumns + formatValues.TrimEnd(',') + ") ";

            return formatValues.TrimEnd(',');
        }

    }
}
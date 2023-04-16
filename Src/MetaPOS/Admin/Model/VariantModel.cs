using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class VariantModel
    {
        SqlOperation sqlOperation = new SqlOperation();
        CommonFunction commonFunction = new CommonFunction();
        public List<ListItem> getVariantDataModel(string type, string value)
        {
            var query = "SELECT Id,field as name FROM FieldInfo WHERE active='1' " + HttpContext.Current.Session["userAccessParameters"] + "";
            if (type == "0")
            {
                query = "SELECT Id, attributeName as name FROM AttributeInfo WHERE active='1' AND fieldId='" + value + "' " + HttpContext.Current.Session["userAccessParameters"] + "";
            }


            var variant = new List<ListItem>();
            //string txtView = "";
            var dtVariant = sqlOperation.getDataTable(query);
            foreach (DataRow row in dtVariant.Rows)
            {
                variant.Add(new ListItem(row["name"].ToString(), row["Id"].ToString()));
            }

            return variant;
        }

        public DataTable getAttributeNameModel(string attrId)
        {
            return sqlOperation.getDataTable("SELECT * FROM AttributeInfo WHERE Id='" + attrId + "'");
        }

        public DataTable getFieldNameModel(string attrId)
        {
            return
                sqlOperation.getDataTable(
                    "SELECT field.field as fieldName FROM AttributeInfo as attr LEFT JOIN FieldInfo as field ON attr.fieldId = field.Id WHERE attr.Id='" +
                    attrId + "'");
        }
    }
}
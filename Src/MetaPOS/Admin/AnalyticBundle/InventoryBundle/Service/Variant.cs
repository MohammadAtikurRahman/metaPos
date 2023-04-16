using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.InventoryBundle.Service
{
    public class Variant
    {
        public List<ListItem> getVariantData(string type, string value)
        {
            var variantModel = new VariantModel();
            return variantModel.getVariantDataModel(type, value);
        }


        public string getAttributeNameData(string attributeRecord)
        {
            var attribute = new VariantModel();
            var attrName = "";
            if (attributeRecord.Contains(","))
            {
                var splitAttr = attributeRecord.Split(',');
                for (int i = 0; i < splitAttr.Length; i++)
                {
                    var dtAttr = attribute.getAttributeNameModel(splitAttr[i]);
                    if (dtAttr.Rows.Count > 0)
                    {
                        if (i != 0)
                            attrName += ", ";

                        var fieldName = "";
                        var dtField = attribute.getFieldNameModel(splitAttr[i]);
                        if (dtField.Rows.Count > 0)
                        {
                            fieldName = dtField.Rows[0]["fieldName"].ToString();
                        }

                        attrName += fieldName + ": " + dtAttr.Rows[0]["attributeName"];
                    }
                }
            }
            else
            {
                var dtAttr = attribute.getAttributeNameModel(attributeRecord);
                if (dtAttr.Rows.Count > 0)
                {
                    var fieldName = "";
                    var dtField = attribute.getFieldNameModel(attributeRecord);

                    if (dtField.Rows.Count > 0)
                    {
                        fieldName = dtField.Rows[0]["fieldName"].ToString();
                    }

                    attrName += fieldName + " : " + dtAttr.Rows[0]["attributeName"].ToString();
                }
            }

            return attrName;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.Model
{


    public class AttributeDetailModel
    {


        private DataAccess.SqlOperation objSqlOperation = new DataAccess.SqlOperation();
        private DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();

        public int fieldId { get; set; }
        public int attributeId { get; set; }
        public int productId { get; set; }
        public int soldQty { get; set; }
        public int stockQty { get; set; }

        // entrydate
        //upadteDate
        //roleId

        // Create Attribute Detail
        public void saveAttributeDetail()
        {
            string query =
                "INSERT INTO AttributeDetailInfo (fieldId,attributeId,productId,soldQty,stockQty,entryDate,updateDate,roleId) " +
                "VALUES ('" + fieldId + "','" + attributeId + "','" + productId + "','0','" + stockQty + "','" +
                objCommonFun.GetCurrentTime() + "','" + objCommonFun.GetCurrentTime() + "','" +
                HttpContext.Current.Session["roleId"] + "') ";

            objSqlOperation.executeQuery(query);
        }





        // Update Attribute Detail
        public void updateAttributeDetail()
        {
            string query = "UPDATE AttributeDetailInfo SET stockQty = '" + stockQty + "' WHERE productId = '" +
                           productId + "' AND fieldId='" + fieldId + "' AND attributeId = '" + attributeId + "'";

            objSqlOperation.executeQuery(query);
        }





        // Load Attribute
        public DataSet findAttributeDetail(string code, string fieldId)
        {
            var dsTemp = new DataSet();
            try
            {
                // Only one field get
                //DataSet dsField = objSqlOperation.getDataSet("SELECT * FROM FieldInfo WHERE Active = '1'");
                //string query = "SELECT * FROM AttributeDetailInfo WHERE productId = '" + code + "' AND fieldId='" + dsField.Tables[0].Rows[0][0].ToString() + "'  AND roleId ='" + HttpContext.Current.Session["roleId"] + "'";
                //dsTemp = objSqlOperation.getDataSet(query);

                string query = "SELECT * FROM AttributeDetailInfo WHERE productId = '" + code + "' AND fieldId='" +
                               fieldId + "'  AND roleId ='" + HttpContext.Current.Session["roleId"] + "'";

                dsTemp = objSqlOperation.getDataSet(query);

                return dsTemp;
            }
            catch (Exception)
            {
                return dsTemp;
            }
        }





        // Check Exists Attributes
        public DataSet ExistsAttribute()
        {
            var dsTemp = new DataSet();
            try
            {
                //string[] splitText = new string[] { };
                //string finalText = code;

                //if (code.Contains('('))
                //{
                //    splitText = code.Split(new string[] { " ( " }, StringSplitOptions.None);
                //    finalText = splitText[1].Substring(0, splitText[1].Length - 1);
                //}

                string query = "SELECT * FROM AttributeDetailInfo WHERE productId = '" + productId + "' AND fieldId='" +
                               fieldId + "' AND roleId ='" + HttpContext.Current.Session["roleId"] + "'";
                dsTemp = objSqlOperation.getDataSet(query);

                return dsTemp;
            }
            catch (Exception)
            {
                return dsTemp;
            }
        }





        public void DeleteAttribute(string fieldId, string attrId, string prodId)
        {
            string query = "DELETE FROM AttributeDetailInfo WHERE productId = '" + prodId + "' AND attributeId ='" +
                           attrId + "' AND fieldId='" + fieldId + "' AND roleId ='" +
                           HttpContext.Current.Session["roleId"] + "'";
            objSqlOperation.executeQuery(query);
        }





        public DataSet getAttributeInfo(string fieldId)
        {
            string query = "SELECT * FROM AttributeInfo WHERE fieldId='" + fieldId + "'";
            DataSet ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        public void DeleteAttributeSingleProduct(string prodId)
        {
            string query = "DELETE FROM AttributeDetailInfo WHERE productId = '" + prodId + "' AND roleId ='" +
                           HttpContext.Current.Session["roleId"] + "'";
            objSqlOperation.executeQuery(query);
        }


    }


}
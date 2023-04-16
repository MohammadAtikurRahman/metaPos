using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{


    public class CategoryModel
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        private string query = "";
        private DataSet ds;

        public static DropDownList ddlList = new DropDownList();


        public string catName { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime updateDate { get; set; }
        public string roleId { get; set; }
        public string active { get; set; }





        public CategoryModel()
        {
            entryDate = updateDate = commonFunction.GetCurrentTime();
        }


        // List
        public void listCategory()
        {
            query = "SELECT * FROM CashCatInfo";
            ds = sqlOperation.getDataSet(query);
            ddlList.DataSource = ds;
            ddlList.DataTextField = "cashType";
            ddlList.DataValueField = "Id";
            ddlList.DataBind();
            //ddlList.Items.Add(0, new ListItem("-- Select All --", 0));
        }



        public List<ListItem> getCategoryDataListModel(string searchType)
        {
            string query = "";
            if (searchType == "product")
            {
                query = "Select catName as name ,Id FROM CategoryInfo where roleId='" + HttpContext.Current.Session["roleId"] + "'";
            }
            else
            {
                query = "Select name ,Id FROM ServiceTypeInfo where roleId='" + HttpContext.Current.Session["roleId"] + "'";
            }

            var dtCategory = sqlOperation.getDataTable(query);

            var customerlist = new List<ListItem>();
            foreach (DataRow row in dtCategory.Rows)
            {
                customerlist.Add(new ListItem(row["name"].ToString(), row["Id"].ToString()));
            }

            return customerlist;
        }

        public bool createCategoryModel()
        {
            string queryCategory = "INSERT INTO CategoryInfo (catName,entryDate,updateDate,roleId) VALUES ('" + catName + "','" +
                           entryDate + "','" + updateDate + "','" + roleId + "')";
            return sqlOperation.fireQuery(queryCategory);
        }
    }


}
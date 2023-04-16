using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.Model
{


    public class WebModel
    {


        private Admin.DataAccess.SqlOperation objSqlOperation = new DataAccess.SqlOperation();
        private Admin.DataAccess.CommonFunction objCommonFun = new DataAccess.CommonFunction();

        private string query = "";
        private DataSet ds;

        public string websiteName { get; set; }
        public string websiteSlogan { get; set; }
        //slider Img Name
        public string sliderImgName { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string details { get; set; }

        public string googleMapShareLink { get; set; }

        public string displayFeatured { get; set; }

        public string displayNew { get; set; }


        //entryDate

        // UpdateDate

        // RoleID

        // create / insert
        public dynamic createWeb()
        {
            query = "INSERT INTO WebInfo VALUES(N'" +
                    websiteName + "',N'" +
                    websiteSlogan + "','" +
                    sliderImgName + "',N'" +
                    contact + "',N'" +
                    address + "','" +
                    email + "',N'" +
                    details + "','" +
                    objCommonFun.GetCurrentTime().ToString("MM/dd/yyyy") + "','" +
                    objCommonFun.GetCurrentTime().ToString("MM/dd/yyyy") + "','" +
                    HttpContext.Current.Session["roleId"] + "','" +
                    googleMapShareLink + "','" +
                    displayFeatured + "','" +
                    displayNew + "')";
            return objSqlOperation.executeQuery(query);
        }





        // Edit / Update
        public dynamic updateWeb()
        {
            query = "UPDATE WebInfo SET  websiteName=N'" +
                    websiteName + "', websiteSlogan=N'" +
                    websiteSlogan + "', contact = N'" +
                    contact + "', address = N'" +
                    address + "', email ='" +
                    email + "', details=N'" +
                    details + "', updateDate ='" +
                    objCommonFun.GetCurrentTime().ToString("MM/dd/yyyy") + "', googleMapShareLink = '" +
                    googleMapShareLink + "', displayFeatured = '" +
                    displayFeatured + "', displayNew = '" +
                    displayNew + "' WHERE roleID = '" +
                    HttpContext.Current.Session["roleId"].ToString() + "'";
            return objSqlOperation.executeQuery(query);
        }





        public DataSet getWeb()
        {
            query = "SELECT * FROM WebInfo WHERE roleID = '" + HttpContext.Current.Session["roleId"] + "'";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        public dynamic ImageUpload()
        {
            query = "UPDATE WebInfo SET sliderImgName = '" + sliderImgName + "' WHERE roleID = '" +
                    HttpContext.Current.Session["roleId"].ToString() + "'";
            return objSqlOperation.executeQuery(query);
        }





        public bool IsUpdateOrInsert()
        {
            query = "SELECT * FROM WebInfo WHERE roleID='" + HttpContext.Current.Session["roleId"].ToString() + "'";
            ds = objSqlOperation.getDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }


    }


}
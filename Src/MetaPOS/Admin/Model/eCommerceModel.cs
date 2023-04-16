using System;
using System.Data;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Shop.Controller;


namespace MetaPOS.Admin.Model
{


    public class eCommerceModel
    {
        private Shop.Model.Shop objWebModel = new Shop.Model.Shop();
        private SqlOperation objSqlOperation = new SqlOperation();
        private CommonController objCommonController = new CommonController();
        private CommonFunction objCommFun = new CommonFunction();

        private DataSet ds;

        private int featuredNumber, countNumber;
        private string query = "";

        public int ecomProdId { get; set; }

        public string EprodCode { get; set; }
        public string prodTitle { get; set; }

        public decimal oPrice { get; set; }

        // oPrice
        public string shortDescr { get; set; }
        public string longDescr { get; set; }
        public string image { get; set; }
        public char active { get; set; }
        //roelId
        //entryDate
        // updateDate
        // branchId
        //groupId

        public bool isFeatured { get; set; }





        public eCommerceModel()
        {
            active = '0';
        }





        public dynamic getDisplayNumber()
        {
            ds = objWebModel.getWeb();
            if (ds.Tables[0].Rows.Count > 0)
            {
                featuredNumber = Convert.ToInt32(ds.Tables[0].Rows[0][12]);
            }
            return featuredNumber;
        }





        public dynamic InsertEcomProducts()
        {
            query = "INSERT INTO [Ecommerce] VALUES ('" + EprodCode + "',N'" + prodTitle + "','" + oPrice + "',N'" +
                    shortDescr + "',N'" + longDescr + "','" +
                    image + "','" + active + "','" +
                    HttpContext.Current.Session["roleId"] + "','" + objCommFun.GetCurrentTime() + "','" +
                    objCommFun.GetCurrentTime() + "','" + HttpContext.Current.Session["branchId"] + "','" +
                    HttpContext.Current.Session["groupId"] + "','" + isFeatured + "','" + ecomProdId + "')";
            objSqlOperation.executeQuery(query);
            return true;
        }





        public void UpdateEcomProducts()
        {
            query = "Update [Ecommerce] SET image = '" + image + "',oPrice = '" + oPrice + "', prodTitle =N'" + prodTitle +
                    "' WHERE prodCode = '" + EprodCode + "'";
            objSqlOperation.executeQuery(query);
        }





        public dynamic countFeatured()
        {
            query =
                "SELECT * FROM Ecommerce LEFT JOIN RoleInfo AS role ON Ecommerce.roleID = role.roleID WHERE Ecommerce.isFeatured ='" +
                isFeatured + "' AND role.domainName = '" + objCommonController.getDomainPartOnly() + "'";
            ds = objSqlOperation.getDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
                countNumber = Convert.ToInt32(ds.Tables[0].Rows.Count);
            return countNumber;
        }


    }


}
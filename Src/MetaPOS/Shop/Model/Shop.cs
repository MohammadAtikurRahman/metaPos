using System.Data;


namespace MetaPOS.Shop.Model
{


    public class Shop
    {


        private Admin.DataAccess.SqlOperation objSqlOperation = new Admin.DataAccess.SqlOperation();
        private Controller.CommonController objCommonController = new Controller.CommonController();

        public string displayFeatured { get; set; }
        public string displayNew { get; set; }

        private string query;

        private DataSet ds;
        private DataTable dt;





        public DataSet getWeb()
        {
            query =
                "SELECT web.websiteName,web.websiteSlogan,web.sliderImgName,web.contact,web.contact,web.address,web.email,web.details,web.entryDate,web.updateDate,web.roleID,web.googleMapShareLink,web.displayFeatured,web.displayNew,role.domainName FROM WebInfo AS web LEFT JOIN RoleInfo AS role ON web.roleID = role.roleID  WHERE role.domainName = '" +
                objCommonController.getDomainPartOnly() + "' AND role.active='1'";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        public DataTable getWebInfo()
        {
            query =
                "SELECT web.websiteName,web.websiteSlogan,web.sliderImgName,web.contact,web.contact,web.address,web.email,web.details,web.entryDate,web.updateDate,web.roleID,web.googleMapShareLink,web.displayFeatured,web.displayNew,role.domainName FROM WebInfo AS web LEFT JOIN RoleInfo AS role ON web.roleID = role.roleID  WHERE role.domainName = '" +
                objCommonController.getDomainPartOnly() + "' AND role.active='1'";
            dt = objSqlOperation.getDataTable(query);
            return dt;
        }





        public dynamic getFeaturedProduct()
        {
            string query = "SELECT TOP " + displayFeatured +
                           "  Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE RoleInfo.domainName='" +
                           objCommonController.getDomainPartOnly() + "' AND isFeatured = '" + true +
                           "' AND RoleInfo.active='1' ORDER BY ID DESC";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        // new product
        public dynamic getNewProduct()
        {
            string query = "SELECT TOP " + displayNew +
                           " Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE RoleInfo.domainName='" +
                           objCommonController.getDomainPartOnly() + "' AND RoleInfo.active='1' ORDER BY ID DESC";
            //query = "SELECT TOP 8 * FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId WHERE RoleInfo.domainName='" + HttpContext.Current.Request.Url.Host.ToString() + "' ORDER BY Ecommerce.entryDate";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        // get all product
        public DataSet getAllProduct()
        {
            string query =
                "SELECT TOP 8 Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE RoleInfo.domainName='" +
                objCommonController.getDomainPartOnly() + "' AND RoleInfo.active='1' ORDER BY ID DESC";
            //query = "SELECT TOP 8 * FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId WHERE RoleInfo.domainName='" + HttpContext.Current.Request.Url.Host.ToString() + "' ORDER BY Ecommerce.entryDate";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        public DataSet getCategory()
        {
            string query =
                "SELECT DISTINCT CategoryInfo.catName,CategoryInfo.Id FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON StockInfo.catName = CategoryInfo.Id  WHERE RoleInfo.domainName='" +
                objCommonController.getDomainPartOnly() + "' AND CategoryInfo.active ='1' AND RoleInfo.active='1'";
            ds = objSqlOperation.getDataSet(query);
            return ds;
        }





        public DataTable getEcommerce(string CatSearch, string txtSearch)
        {
            DataTable dtable = new DataTable();

            query =
                "SELECT Ecommerce.Id,Ecommerce.prodTitle,Ecommerce.image,shortDescr,Ecommerce.longDescr,StockInfo.sPrice,StockInfo.qty,SupplierInfo.supCompany, CategoryInfo.catName FROM Ecommerce LEFT JOIN StockInfo ON StockInfo.ProdCode = Ecommerce.ProdCode LEFT JOIN RoleInfo ON Ecommerce.groupId = RoleInfo.RoleId LEFT JOIN CategoryInfo ON CategoryInfo.Id = StockInfo.catName LEFT JOIN SupplierInfo ON SupplierInfo.supID = StockInfo.supCompany WHERE (StockInfo.catName = '" +
                CatSearch + "' OR '" + CatSearch + "' = '') AND (prodTitle LIKE '%" + txtSearch + "%' OR '" + txtSearch +
                "' = '')  AND RoleInfo.domainName='" + objCommonController.getDomainPartOnly() +
                "' AND RoleInfo.active ='1'";

            dtable = objSqlOperation.getDataTable(query);

            return dtable;
        }





        public string getContact()
        {
            DataTable dt = getWebInfo();

            if(dt.Rows.Count > 0)
                return dt.Rows[0]["contact"].ToString();

            return "";
        }


    }


}
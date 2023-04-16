using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.ImportBundle.Service
{
    public class ImportService
    {
        private  CommonFunction commonFunction = new CommonFunction();

        public bool createSupplier(string suplierValue)
        {
            var supplierModel = new SupplierModel();
            supplierModel.supId = commonFunction.GenerateNewRandom();
            supplierModel.supComapny = suplierValue;
            supplierModel.roleId = HttpContext.Current.Session["roleId"].ToString();

            return supplierModel.createSupplierModel();
        }

        public bool createCategory(string catagoryValue)
        {
            var categoryModel = new CategoryModel();
            categoryModel.catName = catagoryValue;
            categoryModel.roleId = HttpContext.Current.Session["roleId"].ToString();

            return categoryModel.createCategoryModel();
        }

        internal void createManufacturer(string manufacturerValue)
        {
            var manufacturer = new ManufacturerModel();
            manufacturer.manufacturerName = manufacturerValue;
            manufacturer.roleId = HttpContext.Current.Session["roleId"].ToString();

            manufacturer.createManufacturerModel();
        }
    }
}
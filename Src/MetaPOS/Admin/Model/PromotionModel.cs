using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;

namespace MetaPOS.Admin.Model
{
    public class PromotionModel
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public DataTable GetInvoiceTemplateData()
        {
            return sqlOperation.getDataTable("SELECT * FROM BranchInfo WHERE storeId='" + HttpContext.Current.Session["storeId"].ToString() + "'");
        }
    }
}
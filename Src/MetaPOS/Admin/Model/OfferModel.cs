using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class OfferModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        public string prodId { get; set; }





        public DataTable getValidOfferModel()
        {
            return sqlOperation.getDataTable("SELECT * FROM OfferInfo WHERE prodId='" + prodId + "' AND OfferEnd >='" + commonFunction.GetCurrentTime().ToShortDateString() + "' ");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{


    public class ReturnModel
    {


        public int Id { get; set; }
        public string prodCode { get; set; }
        public string qty { get; set; }
        public string piece { get; set; }
        public string catName { get; set; }
        public string imei { get; set; }


        private SqlOperation objSql = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();





        public DataTable getItemDataModel(string productId, string storeId)
        {
            string query = "SELECT stock.prodId,stock.bPrice,stock.catName,stock.imei,stock.supCompany, qm.stockQty FROM [StockInfo] as stock LEFT JOIN QtyManagement as qm ON qm.productId=stock.prodId WHERE qm.productId = '" + productId + "' and qm.storeId = '" + storeId + "'";
            DataTable dt = objSql.getDataTable(query);
            return dt;
        }





        public int getItemRatioDataModel(string prodCode)
        {
            int ratio = 0, unitId = 0;
            DataTable dt = objSql.getDataTable("SELECT unitId FROM StockInfo WHERE prodCode = '" + prodCode + "'");
            if (dt.Rows.Count > 0)
                unitId = Convert.ToInt32(dt.Rows[0]["unitId"]);

            DataTable dtRatio = objSql.getDataTable("SELECT unitRatio FROM UnitInfo WHERE Id = '" + unitId + "'");
            if (dtRatio.Rows.Count > 0)
                ratio = Convert.ToInt32(dtRatio.Rows[0]["unitRatio"]);

            return ratio;
        }


    }


}
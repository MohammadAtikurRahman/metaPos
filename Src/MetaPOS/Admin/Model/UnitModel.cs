using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class UnitModel
    {

        SqlOperation sqlOperation = new SqlOperation();
        public DataTable getUnitRationByProdCode(string prodCode)
        {
            return sqlOperation.getDataTable(
                    "SELECT unit.unitRatio FROM UnitInfo as unit LEFT JOIN stockInfo as stock ON stock.unitId = unit.Id " +
                    "WHERE stock.prodCode='" + prodCode + "'");
        }
    }
}
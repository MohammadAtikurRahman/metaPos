using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using DataTable = System.Data.DataTable;

namespace MetaPOS.Admin.Model
{
    public class CardModel
    {
        private SqlOperation sqlOperation = new SqlOperation();


        public DataTable GetCardNameModel(string cardId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT cardName FROM CardTypeInfo WHERE Id='" + cardId + "'");
            return dt;
        }


    }
}
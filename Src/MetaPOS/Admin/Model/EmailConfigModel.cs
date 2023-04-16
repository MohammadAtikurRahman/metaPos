using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;

namespace MetaPOS.Admin.Model
{
    public class EmailConfigModel
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public DataTable getSmsCofigData()
        {
            return
                sqlOperation.getDataTable(
                    "SELECT medium,sender,apiKey,cost FROM EmailConfigInfo");
        }
    }
}
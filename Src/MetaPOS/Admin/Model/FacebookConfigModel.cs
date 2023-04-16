using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Model
{
    public class FacebookConfigModel
    {
        private SqlOperation sqlOperation = new SqlOperation();
        public string pageId { get; set; }
        public string accessToken { get; set; }


        public DataTable getFacebookCofigData()
        {
            return
                sqlOperation.getDataTable(
                    "SELECT pageId,accessToken FROM FacebookConfigInfo");
        }
      }
}
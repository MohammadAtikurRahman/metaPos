using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace MetaPOS.Admin
{


    public partial class Docs : System.Web.UI.Page
    {


        private Admin.DataAccess.SqlOperation objSql = new Admin.DataAccess.SqlOperation();
        private Admin.DataAccess.CommonFunction objCommonFun = new Admin.DataAccess.CommonFunction();





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Docs"))
                {
                    DataAccess.CommonFunction obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
            }
        }


    }


}
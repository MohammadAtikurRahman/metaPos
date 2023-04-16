using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.SettingBundle.View
{
    public partial class UserLogs : BasePage
    {
        private SqlOperation sqlOperation = new SqlOperation();
        CommonFunction commonFunction = new CommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                commonFunction.fillAllDdl(ddlUserLogsList, "SELECT DISTINCT email,userRight FROM UserLogsInfo ", "email", "userRight");
                ddlUserLogsList.Items.Insert(0, new ListItem("Select User", "0"));  
            }
           
        }



        protected void ddlUserLogsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT TOP 5 * FROM UserLogsInfo WHERE userRight='" + ddlUserLogsList.SelectedValue + "' ORDER BY Id DESC";//ORDER BY Id DESC
            refreshGrd(query);
        }



        private void refreshGrd(string query)
        {

            //var dtRole = sqlOperation.getDataTable(query);
            // var roleId = Convert.ToInt32(dtRole.Rows[0]["roleID"].ToString());

            SqlDataSource dsGrdUserLogsStatus = new SqlDataSource();
            dsGrdUserLogsStatus.ID = "dsGrdUserLogsStatus";
            this.Page.Controls.Add(dsGrdUserLogsStatus);
            var constr = GlobalVariable.getConnectionStringName();
            dsGrdUserLogsStatus.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[constr].ConnectionString;
            dsGrdUserLogsStatus.SelectCommand = query;
            grdUserLogsStatus.DataSource = dsGrdUserLogsStatus;
            grdUserLogsStatus.DataBind();
        }



    }
}
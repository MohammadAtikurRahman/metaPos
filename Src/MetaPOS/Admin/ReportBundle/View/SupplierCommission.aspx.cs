using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.AnalyticBundle.Service;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.ReportBundle.View
{
    public partial class SupplierCommission : BasePage//System.Web.UI.Page
    {
        private  CommonFunction commonFunction = new CommonFunction();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                lblHiddenCompanyName.Value = Session["comName"].ToString();
                lblHiddenCompanyAddress.Value = Session["comAddress"].ToString();
                lblHiddenCompanyPhone.Value = Session["comPhone"].ToString();
            }
            catch (Exception)
            {
                
            }


            if (!IsPostBack)
            {
                if (!commonFunction.accessChecker("SupplierCommission"))
                {
                    commonFunction.pageout();
                }

                lblCompanyName.Text = Session["comName"].ToString();
            }

            commonFunction.fillAllDdl(ddlStoreList, "select DISTINCT warehouse.Id,warehouse.name FROM RoleInfo role LEFT JOIN WarehouseInfo warehouse ON warehouse.Id = role.storeId WHERE role.active='1' AND warehouse.name !='' " + commonFunction.getStoreAccessParameters("role") + " ORDER BY warehouse.Id ASC", "name", "Id");
            if (Session["userRight"].ToString() == "Branch")
            {
                ddlStoreList.Items.Insert(0, new ListItem(Resources.Language.Lbl_supplierCommission_search_all_store, "0"));
            }
            else
            {
                ddlStoreList.Style.Add("display", "none");
            }

            // User wise filter
            if (Session["userRight"].ToString() == "Branch")
            {
                commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE (userRight='Regular' OR roleId='" + Session["roleId"] + "') AND active='1' " + Session["storeAccessParameters"] + "", "title", "roleId");
                ddlUserList.Items.Insert(0, new ListItem(Resources.Language.Lbl_supplierCommission_search_all_user, "0"));
            }
            else if (Session["userRight"].ToString() == "Regular")
            {
                commonFunction.fillAllDdl(ddlUserList, "select title,roleId FROM RoleInfo WHERE userRight='Regular' AND active='1' AND roleId='" + Session["roleId"] + "'", "title", "roleId");
                ddlUserList.Style.Add("display", "none");
            }

        }


        [WebMethod]
        public static string getSupplierCommissionReportDataListAction(string jsonData)
        {
            var reportSupplierCommission = new ReportSupplierCommission();
            return reportSupplierCommission.getSupplierCommissionReportDataList(jsonData);
        }
    }
}
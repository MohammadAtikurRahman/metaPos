using MetaPOS.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace MetaPOS.Core.Services
{
    public class StoreService
    {
        public void loadStoreDropdownList(DropDownList dropDownList)
        {
            int roleId = 0;
            if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
                roleId = Convert.ToInt32(HttpContext.Current.Session["branchId"].ToString());
            else
                roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"].ToString());


            var storeRepository = new StoreRepository();
            var dtRole = storeRepository.getBranchRoleList(roleId);
            if (dtRole.Rows.Count < 0)
                return;
            string coniditionalStorelist = getConditionalData(dtRole);

            var dtStoreData = storeRepository.GetDataCondition(coniditionalStorelist);

            dropDownList.DataSource = dtStoreData;
            dropDownList.DataTextField = "name";
            dropDownList.DataValueField = "Id";

            dropDownList.SelectedIndex = -1;
            if (dropDownList.SelectedValue.Length > 0)
            {
                dropDownList.SelectedValue.Remove(0);
            }

            dropDownList.DataBind();

            dropDownList.Items.Insert(0, new ListItem("-- Store --", "0"));
            dropDownList.SelectedIndex = 0;
        }



        public string getConditionalData(DataTable dtRole)
        {
            string condition = "";
            for (int k = 0; k < dtRole.Rows.Count; k++)
            {
                if (k == 0)
                    condition += " AND(";

                condition += " roleId='" + dtRole.Rows[k]["roleId"] + "'";

                if (k != dtRole.Rows.Count - 1)
                    condition += " OR ";

                if (k == dtRole.Rows.Count - 1)
                    condition += ")";
            }
            return condition;
        }
    }
}

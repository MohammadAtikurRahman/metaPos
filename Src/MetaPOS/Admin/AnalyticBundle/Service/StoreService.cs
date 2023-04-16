using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.AnalyticBundle.Service
{
    public class StoreService
    {

        //public void getStoreList(DropDownList ddlStoreList)
        //{
        //    if (HttpContext.Current.Session["userRight"].ToString() == "Group")
        //    {
        //        // Group Load Branch
        //        var branchModel = new BranchModel();
        //        ddlStoreList.DataSource = branchModel.getBranchListModel();
        //        ddlStoreList.DataTextField = "title";
        //        ddlStoreList.DataValueField = "roleId";
        //        ddlStoreList.DataBind();

        //        ddlStoreList.SelectedValue = HttpContext.Current.Session["roleId"].ToString();
        //    }
        //    else
        //    {
        //        string branchId = "", roleId = "";
        //        if (HttpContext.Current.Session["userRight"].ToString() == "Regular")
        //        {
        //            roleId = HttpContext.Current.Session["branchId"].ToString();
        //            branchId = HttpContext.Current.Session["branchId"].ToString();

        //        }
        //        else if (HttpContext.Current.Session["userRight"].ToString() == "Branch")
        //        {
        //            roleId = HttpContext.Current.Session["roleId"].ToString();
        //            branchId = HttpContext.Current.Session["roleId"].ToString();
        //        }


        //        var storeModel = new StoreModel();
        //        ddlStoreList.DataSource = storeModel.getStoreListModel(roleId, branchId);
        //        ddlStoreList.DataTextField = "name";
        //        ddlStoreList.DataValueField = "Id";

        //        ddlStoreList.SelectedIndex = -1;
        //        if (ddlStoreList.SelectedValue.Length > 0)
        //        {
        //            ddlStoreList.SelectedValue.Remove(0);
        //        }

        //        ddlStoreList.DataBind();
        //    }

        //}


    }
}
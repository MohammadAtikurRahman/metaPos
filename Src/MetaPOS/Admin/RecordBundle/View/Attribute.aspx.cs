using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace MetaPOS.Admin.RecordBundle.View
{


    public partial class Attribute : BasePage//System.Web.UI.Page
    {

        protected void Page_load(object Sender, EventArgs e)
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
        }

        //private Admin.DataAccess.SqlOperation objSql = new Admin.DataAccess.SqlOperation();
        //private Admin.DataAccess.CommonFunction objCommonFun = new Admin.DataAccess.CommonFunction();

        //private static string oldValue = "", query = "", temp = "", attributeId = "";
        //private DataSet ds;


        //public enum MessageType
        //{


        //    Success,
        //    Error,
        //    Info,
        //    Warning


        //};





        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        if (!objCommonFun.accessChecker("Attribute"))
        //        {
        //            var obj = new DataAccess.CommonFunction();
        //            obj.pageout();
        //        }

        //        objCommonFun.fillAllDdl(ddlFieldList,
        //            "SELECT [Id], [field], [visible], [active] FROM [FieldInfo] WHERE [active] = '1' AND [visible] = '1'",
        //            "field", "Id");

        //        ddlFieldList.Items.Insert(0, "Select a Field");
        //    }

        //    searchResult();
        //}





        //private void scriptMessage(string Message, MessageType type)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
        //        "showMessage('" + Message + "','" + type + "');", true);
        //}





        //private void refreshGrd(string query)
        //{
        //    dsgrdAttributeInfo.SelectCommand = query;
        //    grdAttributeInfo.PageIndex = 0;
        //    grdAttributeInfo.DataBind();
        //}





        //private void searchResult()
        //{
        //    query =
        //        "SELECT * FROM AttributeInfo AS attr INNER JOIN FieldInfo AS field ON attr.fieldId = field.Id WHERE ((attr.attributeName LIKE IsNull('%" +
        //        txtSearchByAttribute.Text + "%', attributeName) OR field.field LIKE IsNull('%" +
        //        txtSearchByAttribute.Text + "%',field))) AND attr.active='" + ddlActiveStatus.SelectedValue +
        //        "' ORDER BY field.field, attr.Id";

        //    refreshGrd(query);
        //}





        ////<-- Function


        //protected void btnAttribute_Click(object sender, EventArgs e)
        //{
        //    if (ddlFieldList.Text == "Select a Field")
        //    {
        //        scriptMessage("Field is Required!", MessageType.Warning);
        //        return;
        //    }

        //    if (txtAttribute.Text == "")
        //    {
        //        scriptMessage("Attribute is Required!", MessageType.Warning);
        //        return;
        //    }


        //    ds =
        //        objSql.getDataSet("SELECT * FROM [AttributeInfo] WHERE attributeName = '" + txtAttribute.Text +
        //                          "' AND fieldId = '" + ddlFieldList.SelectedValue + "'");

        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        scriptMessage("This Attribute Already Exist!", MessageType.Warning);
        //        return;
        //    }

        //    query = "INSERT INTO [AttributeInfo] ( fieldId, attributeName, entryDate,   updateDate ) VALUES ('" +
        //            ddlFieldList.SelectedValue + "', '" + txtAttribute.Text + "','" +
        //            objCommonFun.GetCurrentTime().ToShortDateString() + "','" +
        //            objCommonFun.GetCurrentTime().ToShortDateString() + "')";
        //    objSql.executeQuery(query);

        //    scriptMessage("Operation Successful.", MessageType.Success);

        //    grdAttributeInfo.DataBind();

        //    Reset();
        //}





        //protected void grdAttributeInfo_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    lblCurrentDate.Text = objCommonFun.GetCurrentTime().ToShortDateString();
        //    oldValue = (grdAttributeInfo.Rows[e.NewEditIndex].FindControl("lblName") as Label).Text.ToLower();
        //    attributeId = (grdAttributeInfo.Rows[e.NewEditIndex].FindControl("lblAttId") as Label).Text;
        //}





        //protected void grdAttributeInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow row = (GridViewRow) grdAttributeInfo.Rows[e.RowIndex];

        //    ds = objSql.getDataSet("SELECT * FROM [AttributeInfo] WHERE Id = '" + attributeId + "'");

        //    temp = (grdAttributeInfo.Rows[e.RowIndex].FindControl("txtName") as TextBox).Text.ToLower();
        //    //// string fieldName = (grdCardInfo.Rows[e.RowIndex].FindControl("txtCardName") as TextBox).Text.ToLower();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataSet dsAttrbute =
        //            objSql.getDataSet("SELECT * FROM [AttributeInfo] WHERE attributeName = '" + temp + "'");
        //        int count = dsAttrbute.Tables[0].Rows.Count;

        //        if (count == 0 || oldValue == temp)
        //        {
        //            grdAttributeInfo.EditIndex = -1;
        //        }
        //        else
        //        {
        //            e.Cancel = true;
        //            scriptMessage("This Name Already Exist!", MessageType.Warning);
        //        }
        //    }
        //}





        //protected void grdAttributeInfo_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        //{
        //    if (e.Exception == null)
        //    {
        //        if (e.AffectedRows == 1)
        //            scriptMessage("Operation Successful.", MessageType.Success);
        //        else
        //            scriptMessage("Sorry, Operation Failed.", MessageType.Warning);
        //    }
        //    else
        //        scriptMessage("Sorry, Operation Failed.", MessageType.Warning);
        //}





        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //}





        //protected void grdAttributeInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    GridViewRow row = grdAttributeInfo.Rows[e.RowIndex];
        //    Label lblAttribute = (Label) row.FindControl("lblAttId");
        //    string lblAttrId = lblAttribute.Text;

        //    if (ddlActiveStatus.SelectedValue == "1")
        //    {
        //        query = "UPDATE [AttributeInfo] SET active = '0' WHERE Id = '" + lblAttrId + "' ";
        //        objSql.executeQuery(query);
        //        scriptMessage("Staff Information Removed Successfully.", MessageType.Success);
        //    }
        //    else if (ddlActiveStatus.SelectedValue == "0")
        //    {
        //        query = "UPDATE [AttributeInfo] SET active = '1' WHERE Id = '" + lblAttrId + "' ";
        //        objSql.executeQuery(query);
        //        scriptMessage("Staff Information Restored Successfully.", MessageType.Success);
        //    }

        //    searchResult();
        //}





        //protected void grdAttributeInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (ddlActiveStatus.SelectedValue == "1")
        //    {
        //        if (e.Row.RowType == DataControlRowType.Header)
        //        {
        //            e.Row.Cells[5].Text = "Delete";
        //        }
        //    }
        //    else if (ddlActiveStatus.SelectedValue == "0")
        //    {
        //        if (e.Row.RowType == DataControlRowType.Header)
        //        {
        //            e.Row.Cells[5].Text = "Restore";
        //        }

        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            (e.Row.Cells[5].FindControl("btnGrdDelete") as LinkButton).Text =
        //                "<span class='glyphicon glyphicon-retweet'></span>";
        //        }
        //    }
        //}





        //// Reset
        //public void Reset()
        //{
        //    txtAttribute.Text = "";
        //    ddlFieldList.SelectedIndex = 0;
        //}


    }


}
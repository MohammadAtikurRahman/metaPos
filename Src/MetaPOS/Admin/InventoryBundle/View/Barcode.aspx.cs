using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.Service;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.InventoryBundle.View
{
    public partial class Barcode : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void scriptMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + Message + "','" + type + "');", true);
        }


        protected void btnBarcodePrint_OnClick(object sender, EventArgs e)
        {

            var getBarcode = txtBarcode.Text;

            var barcode = new StockBarcode();
            var dtStock = barcode.getProductData(getBarcode);

            if (dtStock.Rows.Count <= 0)
            {
                scriptMessage(Resources.Language.Lbl_barcode_product_code_is_not_exit, MessageType.Warning);
                return;
            }


            // var url = "http://localhost:4350/Admin/BarcodeTool/Barcode.html/barcode.html?name='" + lblProdName.Text + "'&code ='" +

            var name = dtStock.Rows[0]["prodName"].ToString();
            var prodCode = getBarcode;
            var price = dtStock.Rows[0]["sPrice"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "customBarcodePrint('" + name + "','" + prodCode + "','" + price + "');", true);
        }

        


    }


    public enum MessageType
        {


            Success,
            Error,
            Info,
            Warning


        };
}
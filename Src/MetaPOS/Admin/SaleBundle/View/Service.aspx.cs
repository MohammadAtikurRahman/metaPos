using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.RecordBundle.Service;
using MetaPOS.Admin.SaleBundle.Service;


namespace MetaPOS.Admin.SaleBundle.View
{
    public partial class Service : BasePage//System.Web.UI.Page
    {
        private CommonFunction commonFunction = new CommonFunction();

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
                if (!commonFunction.accessChecker("Service"))
                {
                    commonFunction.pageout();
                }
            }
        }





        [WebMethod]
        public static List<ListItem> getServiceTypeListAction()
        {
            var serviceType = new ServiceType();
            return serviceType.getServiceTypeList();
        }





        [WebMethod]
        public static bool saveServiceDataAction(string jsonStrData)
        {
            var saleService = new SaleService();
            return saleService.saveServiceData(jsonStrData);
        }




        [WebMethod]
        public static bool updateServiceDataAction(string jsonStrData)
        {
            var saleService = new SaleService();
            return saleService.updateServiceData(jsonStrData);
        }

        


        [WebMethod]
        public static string getServiceDataListAction(string active)
        {
            var saleService = new SaleService();
            return saleService.getServiceDataList(active);
        }




        [WebMethod]
        public static string getServiceDataListAddToCartAction(string prodCode)
        {
            var saleService = new SaleService();
            return saleService.getServiceDataListAddToCart(prodCode);
        }



        [WebMethod]
        public static string searchServiceDataListAddToCartAction(string billNo, string Id)
        {
            var saleService = new SaleService();
            return saleService.searchServiceDataListAddToCart(billNo, Id);
        }





        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


    }
}
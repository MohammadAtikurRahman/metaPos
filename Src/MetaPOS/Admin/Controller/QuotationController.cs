using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.Controller
{


    public class QuotationController
    {


        // Model declear
        private Model.QuotationModel objQuotationModelModel = new Model.QuotationModel();

        //controller declear
        private CommonController objCommonController = new CommonController();

        // Global quotation variable 
        private int i = 0;
        public int count = 0;
        private string[] orderId = new string[100];
        private int[] qty = new int[100];
        private decimal[] sPrice = new decimal[100];





        public void updateQuotation(Dictionary<string, Dictionary<int, object>> dicData, string num)
        {
            //var msg = "";
            var getFormatedConditinalParameter = "";
            //var getFormatUpdateItemData = "";
            for (i = 0; i < count; i++)
            {
                //orderId[i] = dicData["orderId" + i][i].ToString();
                //objQuotationModel.orderId = orderId[i];
                //objQuotationModel.status = '1';
                // Set Update paramter
                //Dictionary<string, string> dicUpdateQuotation = new Dictionary<string, string>();
                //dicUpdateQuotation.Add("status", num);

                orderId[i] = dicData["orderId" + i][i].ToString();
                orderId[i].ToString();
                //
                Dictionary<string, string> dicConditinalQuotation = new Dictionary<string, string>();
                dicConditinalQuotation.Add("orderId", orderId[i]);

                //getFormatUpdateItemData += objCommonController.getFormatedUpdateParameter(dicUpdateQuotation);
                getFormatedConditinalParameter += objCommonController.getConditinalParameter(dicConditinalQuotation);

                objQuotationModelModel.updateQuotation(getFormatedConditinalParameter, num);
            }
        }





        public decimal GiftAmt(Dictionary<string, Dictionary<int, object>> dicData)
        {
            decimal netAmt, giftAmt;//, grossAmt
            netAmt = giftAmt = 0;//grossAmt = 0;
            for (i = 0; i < count; i++)
            {
                qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
                sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);

                //Calculation with parameter
                netAmt += qty[i]*sPrice[i];
                giftAmt = netAmt;
            }

            return giftAmt;
        }


    }


}
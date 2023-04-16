using System;
using System.Collections.Generic;


namespace MetaPOS.Admin.Controller
{


    public class SlipController
    {


        //Moadal declear 
        private Model.SlipModel objSlipModel = new Model.SlipModel();
        private Model.SaleModel objSaleModel = new Model.SaleModel();
        private Model.CustomerModel objCustomerModelModel = new Model.CustomerModel();

        //Slip global variable declearation
        public int count = 0;
        private int i = 0;
        public string disType, comment, warranty;
        private string[] orderId = new string[100];
        private string[] sku = new string[100];
        public int[] qty = new int[50];
        private string[] prodCode = new string[100];
        private decimal[] sPrice = new decimal[100];
        private string[] prodName = new string[100];
        private string[] supCompany = new string[100];
        private string[] catName = new string[100];
        private decimal[] bPrice = new decimal[100];
        private decimal netAmt = 0, grossAmt = 0;
        private int prodId = 0;//, branchId = 0;
        private decimal giftAmt, balance;// currentCash, disAmt, payCash;
        //Variable Value form Model
        private static string billNo = "", Status = "Sold", cusId = "";

        // Constractor
        public SlipController()
        {
            balance = 0;
        }





        public void createSlipInfo(Dictionary<string, Dictionary<int, object>> dicData)
        {
            // Declear Perameter
            int[] _qty = new int[100];

            // BillNo Id Generate
            billNo = objSaleModel.generateSaleId().ToString();
            //Customer Id Generate
            cusId = objCustomerModelModel.generateCustomerId();

            // Assaigin Perameter with Model
            for (i = 0; i < count; i++)
            {
                qty[i] = Convert.ToInt32(dicData["qty" + i][i]);
                sPrice[i] = Convert.ToDecimal(dicData["sPrice" + i][i]);

                //Calculation with parameter
                netAmt += qty[i]*sPrice[i];
                grossAmt = netAmt;
                giftAmt = grossAmt;
                Status = "Sold";
                _qty[0] = qty[i];
            }

            // Assign properties
            objSlipModel.billNo = billNo;
            objSlipModel.cusId = cusId;
            objSlipModel.prodId = prodId.ToString();
            objSlipModel.qty = _qty[0].ToString();
            objSlipModel.netAmt = netAmt;
            objSlipModel.grossAmt = grossAmt;
            objSlipModel.giftAmt = giftAmt;
            objSlipModel.balance = balance;
            objSlipModel.status = Status;

            // Gift amout for customer model
            objCustomerModelModel.totalDue = giftAmt;

            //Insert Sale data using Model
            objSlipModel.createSlip();
        }





        // Get slipinfo
        public void getSlipInfo()
        {
        }





        // Update slipInfo
        public void updateSlipInfo()
        {
        }





        //List slipinfo
        public void listSlipInfo()
        {
        }


    }


}
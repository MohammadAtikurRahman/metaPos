using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using System.Data;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SaleSetting
    {


        private CommonFunction commonFunction = new CommonFunction();
        private SaleModel saleModel = new SaleModel();





        public string saleSettingOption()
        {
            var dt = saleModel.saleSettingOptionModel();

            return commonFunction.serializeDatatableToJson(dt);
        }



        public string initializeConfig()
        {
            var settingModel = new SettingModel();
            return settingModel.initializeConfigModel();
        }
    }


}
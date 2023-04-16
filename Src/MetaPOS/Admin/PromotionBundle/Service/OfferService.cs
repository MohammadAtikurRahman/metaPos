using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CrystalDecisions.Shared.Json;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.PromotionBundle.Service
{
    public class OfferService
    {
        private  CommonFunction commonFunction = new CommonFunction();
        public string getValidOffer(string jsonData)
        {
            var offerModel = new OfferModel();

            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            offerModel.prodId = data["prodId"].Value<string>();
            var dtOffer= offerModel.getValidOfferModel();
            return commonFunction.serializeDatatableToJson(dtOffer);
        }
    }
}
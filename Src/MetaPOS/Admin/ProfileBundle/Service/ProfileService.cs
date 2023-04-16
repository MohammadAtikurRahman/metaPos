using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CrystalDecisions.Shared.Json;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;


namespace MetaPOS.Admin.ProfileBundle.Service
{
    public class ProfileService
    {


        public string SaveProfileData(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            /*
             * "company": company,
            "phone": phone,
            "mobile": mobile,
            "ownerNumber": ownerNumber,
            "vat": vat,
            "tax": tax,
            "url":url,
            "header": header,
            "footer": footer
             * */


            var profileModel = new ProfileModel();
            profileModel.Comany = data["company"].Value<string>();
            profileModel.Phone = data["phone"].Value<string>();
            profileModel.Mobile = data["mobile"].Value<string>();
            profileModel.OwnerNumber = data["ownerNumber"].Value<string>();
            profileModel.Vat = data["vat"].Value<string>();
            profileModel.Tax = data["tax"].Value<string>();
            profileModel.Url = data["url"].Value<string>();
            profileModel.Header = data["header"].Value<string>();
            profileModel.Footer = data["footer"].Value<string>();
            profileModel.storeId = data["storeId"].Value<string>();

            return profileModel.SaveProfileDataModel();
        }


        public List<ListItem> getStoreListData()
        {
            var profileModel = new ProfileModel();
            var dtCustomer = profileModel.getStoreListModel();

            

            var store = new List<ListItem>();


            foreach (DataRow row in dtCustomer.Rows)
            {
                store.Add(new ListItem(row["name"].ToString(), row["Id"].ToString()));
            }

            return store;

        }

        public string loadProfileData(string storeId)
        {
            var profileModel = new ProfileModel();
            return profileModel.loadProfileDataModel(storeId);
        }
    }
}
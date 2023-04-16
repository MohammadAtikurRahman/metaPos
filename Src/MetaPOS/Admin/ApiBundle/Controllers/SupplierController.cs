using System;
using System.Data;
using System.Net;
using System.Web.Http;
using System.Collections.Generic;
using MetaPOS.Admin.ApiBundle.Entities;


namespace MetaPOS.Admin.ApiBundle.Controllers 
{


    public class SupplierController : ApiController 
    {


        // GET api/<controller>
        public IHttpActionResult Get(string branchId, string apiKey)
        {
            var objCommController = new Controller.CommonController();

            // 1st part: to get api key
            var dicSmsConfig = new Dictionary<string, string>
            {
                {"roleId", branchId}
            };
            var getSmsConfigConditionalParameters = objCommController.getConditinalParameter(dicSmsConfig);

            var objSmsConfigModel = new Model.SmsConfigModel();
            var dtSmsConfig = objSmsConfigModel.getSmsConfigApiDataModel(getSmsConfigConditionalParameters);

            if(dtSmsConfig.Rows.Count == 0 || dtSmsConfig.Rows[0]["apiKey"].ToString() != apiKey)
            {
                var errorResult = new Response()
                {
                    Code = 400,
                    Message = "API Key not matched!",
                };

                return Content((HttpStatusCode)400, errorResult);
            }

            // 2nd part: to get customer data
            var dicUser = new Dictionary<string, string>
            {     
                {"active", "1"},
                {"conPhone!", ""},
                {"", objCommController.getUserAccessParametersByGroupId(branchId)}, 
            };
            var getUserConditionalParameters = objCommController.getConditinalParameter(dicUser);

            var objUserModel = new Model.SupplierModel();
            var dtUsers = objUserModel.getSupplierApiDataListModel(getUserConditionalParameters);

            // 3rd part: arrange api data
            var users = new List<ContactEntity>();

            foreach(DataRow row in dtUsers.Rows)
            {
                users.Add(new ContactEntity
                {
                    Name = row["conName"].ToString(),
                    PhoneNumber = row["conPhone"].ToString()
                }
                );
            }

            var successResult = new Response()
            {
                Code = 200,
                Message = "Retrieved Successful.",
                Data = users, 
                Count = users.Count
            };

            return new ResponseController(successResult, Request);
        }
    }


}
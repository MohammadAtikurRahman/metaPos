using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.RecordBundle.Service
{
    public class ServiceType
    {
        public List<ListItem> getServiceTypeList()
        {
            var serviceTypeModel = new ServiceTypeModel();
            return serviceTypeModel.getServiceTypeListModel();
        }
    }
}
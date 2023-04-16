using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaPOS.Api.Entity;

namespace MetaPOS.Api.Models
{
    public class DataStatus
    {
        public List<object> data { get; set; }
        public string status { get; set; }
    }
}

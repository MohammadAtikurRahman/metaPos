using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Entities.Dto
{
    public class SearchDto
    {
        public string storeAccessParameter { get; set; }
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        public int PayMethod { get; set; }
    }
}

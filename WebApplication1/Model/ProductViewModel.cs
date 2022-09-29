using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Model
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime? date { get; set; }
        public int status { get; set; }
        public int masterId { get; set; }
        public string description { get; set; }
        //Multiple
        public int[] ids { get; set; }
        public string[] names { get; set; }
        public DateTime?[] dates { get; set; }
        public int[] statusIds { get; set; }
        public string[] descriptions { get; set; }
    }
}

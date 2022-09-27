using Auth.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Model
{
    public class ReturnObject
    {
        public object jwt { get; set; }
        public string message { get; set; }
        public ApplicationUser userInfo { get; set; }
    }
}

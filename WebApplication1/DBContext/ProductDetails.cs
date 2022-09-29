using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.DBContext
{
    public class ProductDetails
    {
        [Key]
        public int id { get; set; }
        public string productName { get; set; }
        public int? productMasterId { get; set; }
        public ProductMasters productMaster { get; set; }
        public DateTime? expireDate { get; set; }
    }
}

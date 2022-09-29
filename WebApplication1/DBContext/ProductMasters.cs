using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.DBContext
{
    public class ProductMasters
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public DateTime sotckDate { get; set; }
        public string details { get; set; }
        public int? status { get; set; }
    }

}

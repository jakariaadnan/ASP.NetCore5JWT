using Auth.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Model
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11)]
        [MinLength(11)]
        [Required]
        [RegularExpression(@"^[0]+[0-9]*$")]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public ApplicationUser users { get; set; }
    }
}

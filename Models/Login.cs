using System.ComponentModel.DataAnnotations;

namespace loginregistration.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string loginemail {get;set;}
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string loginpw {get;set;}
        
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginregistration.Models
{
    public class User
    {
        [Key]
        public int userID {get;set;}

        [Display(Name = "First Name")]
        [Required]
        public string fname {get;set;}

        [Display(Name = "Last Name")]
        [Required]
        public string lname {get;set;}

        [EmailAddress]
        [Required]
        [Display(Name = "Email")]
        public string email {get;set;}

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [MinLength(5, ErrorMessage="Password must be 5 characters or longer!")]
        public string pw {get;set;}

        [NotMapped]
        [Compare("pw")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string confirm {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
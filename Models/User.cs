using System;
using System.ComponentModel.DataAnnotations;

// ADD THE FOLLOWING FOR 'NotMapped' -->
using System.ComponentModel.DataAnnotations.Schema;

namespace cd_c_loginRegistration.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required(ErrorMessage = "You must enter a first name.")]
        [MinLength(2, ErrorMessage = "First name can not be less than 2 characters.")]
        [Display(Name = "First Name: ")]

        public string FirstName {get;set;}

        [Required(ErrorMessage = "You must enter a last name.")]
        [MinLength(2, ErrorMessage = "Last name can not be less than 2 characters.")]
        [Display(Name = "Last Name: ")]
        public string LastName {get;set;}

        [Required(ErrorMessage = "You must enter an email address.")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address.")]
        [Display(Name = "Email: ")]

        public string Email {get;set;}

        [Required(ErrorMessage = "You must enter a password.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer.")]
        [Display(Name = "Password: ")]
        public string Password {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password: ")]
        public string Confirm {get;set;}

    }
}
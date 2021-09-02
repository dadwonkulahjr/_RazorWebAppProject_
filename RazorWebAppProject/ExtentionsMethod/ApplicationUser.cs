using Microsoft.AspNetCore.Identity;
using RazorWebAppProject.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorWebAppProject.ExtentionsMethod
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="First name"), Required]
        public string FirstName { get; set; }
        [Display(Name = "Last name"), Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, Display(Name ="Sex")]
        public Gender? Gender { get; set; }
        [Display(Name ="Upload Image")]
        public string PhotoPath { get; set; }

        public ApplicationUser()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            Gender = null;
            PhotoPath = string.Empty;
        }
        public ApplicationUser(string firstName, string lastName,
            string address, Gender? gender, string photoPath)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Gender = gender;
            PhotoPath = photoPath;
        }

        public ApplicationUser(string firstName, string lastName,
           string address, Gender? gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Gender = gender;
        }

        public ApplicationUser(string firstName, string lastName,
           string address, string photoPath)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhotoPath = photoPath;
        }
    }
}

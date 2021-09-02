using RazorWebAppProject.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorWebAppProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required."), EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Confirm Password is required."),
            Display(Name = "Confirm Password"), Compare(nameof(Password), ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }

        [Required, Display(Name ="First name")]
        public string FirstName { get; set; }
        [Required, Display(Name ="Last name")]
        public string LastName { get; set; }
        [Display(Name ="Upload Image")]
        public string PhotoPath { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        public string Address { get; set; }

        public string UserId { get; set; }
        public RegisterViewModel(string userId, string email, string password, string confirmPassword)
        {
            UserId = userId;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        public RegisterViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            PhotoPath = string.Empty;
            Gender = null;
            Address = string.Empty;
            UserId = string.Empty;

        }

        public RegisterViewModel(string userId, string email, string password, string confirmPassword, string firstName, string lastName, string photoPath, Gender? gender, string address)
        {
            UserId = userId;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            FirstName = firstName;
            LastName = lastName;
            PhotoPath = photoPath;
            Gender = gender;
            Address = address;
        }
    }
}

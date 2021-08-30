using System.ComponentModel.DataAnnotations;

namespace RazorWebAppProject.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }
        [DataType(DataType.Password), Required]
        public string Password { get; set; }
        [Display(Name ="Remember Me?")]
        public bool RememberMe { get; set; }
        public LoginViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            RememberMe = false;
        }
        public LoginViewModel(string email, string password, bool rememberMe)
        {
            Email = email;
            Password = password;
            RememberMe = rememberMe;
        }

        public LoginViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

    }
}

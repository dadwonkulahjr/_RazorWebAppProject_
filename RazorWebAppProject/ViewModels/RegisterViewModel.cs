using System.ComponentModel.DataAnnotations;

namespace RazorWebAppProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is required."), EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage ="Password is required.")]
        public string Password { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage ="Confirm Password is required."),
            Display(Name ="Confirm Password"), Compare(nameof(Password),ErrorMessage ="Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}

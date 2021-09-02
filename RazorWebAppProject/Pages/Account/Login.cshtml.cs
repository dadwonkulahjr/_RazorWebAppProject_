using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.ExtentionsMethod;
using RazorWebAppProject.ViewModels;

namespace RazorWebAppProject.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid) { return Page(); }

            var result = await _signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password, isPersistent: LoginViewModel.RememberMe, lockoutOnFailure: false);


            if (result.Succeeded)
            {
                if(!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToPage("/Index");
            }


            ModelState.AddModelError(string.Empty, "Invalid email or password.");


            return Page();


        }

    }
}

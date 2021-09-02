using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.ExtentionsMethod;
using System.Threading.Tasks;

namespace RazorWebAppProject.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostYesLogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("login");
        }

        public IActionResult OnPostNoDontLogOutAsync()
        {
            return RedirectToPage("../Admin/SuperUser/Index");
        }
    }
}

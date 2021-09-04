using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.ExtentionsMethod;
using System.Threading.Tasks;

namespace RazorWebAppProject.Pages.Account
{
    public class ViewModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUser ApplicationUser { get; set; }
        public ViewModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            ApplicationUser = await _userManager.FindByIdAsync(id);

            if (ApplicationUser == null)
            {
                return RedirectToPage("/NotFound/Index");
            }


            return Page();
        }
    }
}

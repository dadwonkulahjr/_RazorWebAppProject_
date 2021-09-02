using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.ExtentionsMethod;

namespace RazorWebAppProject.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        public IFormFile Photo { get; set; }
        public EditModel(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            ApplicationUser = await _userManager.FindByIdAsync(id);

            if (ApplicationUser == null)
            {
                return RedirectToPage("/NotFound/Index");
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid) { return Page(); }

            if (Photo != null)
            {

            }
            else
            {


                if (ApplicationUser.PhotoPath != null)
                {
                    //AppUser has existing photo

                    var userFound = await _userManager.FindByIdAsync(ApplicationUser.Id);

                    userFound.UserName = ApplicationUser.UserName;
                    userFound.FirstName = ApplicationUser.FirstName;
                    userFound.LastName = ApplicationUser.LastName;
                    userFound.Email = ApplicationUser.Email;
                    userFound.Gender = ApplicationUser.Gender;
                    userFound.Address = ApplicationUser.Address;
                   

                    if (userFound.PhotoPath != null)
                    {
                        userFound.PhotoPath = ApplicationUser.PhotoPath;
                    }

                    var identityResult = await _userManager.UpdateAsync(userFound);

                    if (identityResult.Succeeded)
                    {
                        TempData["UpdateSuccess"] = $"Record {ApplicationUser.FirstName + " " + ApplicationUser.LastName} updated successfully!";
                        return RedirectToPage("manageuser");
                    }

                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return Page();
        }



    }
}

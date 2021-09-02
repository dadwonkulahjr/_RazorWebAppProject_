using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWebAppProject.ExtentionsMethod;

namespace RazorWebAppProject.Pages.Account
{
    public class ManageUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public List<ApplicationUser> Users { get; set; }
        public ManageUserModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public void OnGet()
        {
            Users = _userManager.Users
                                .OrderBy(s => s.FirstName)
                                .ThenBy(s => s.LastName)
                                .ThenBy(s => s.Email)
                                .ToList();

        }
    }
}

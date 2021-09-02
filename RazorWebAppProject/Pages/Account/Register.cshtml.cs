using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.ExtentionsMethod;
using RazorWebAppProject.ViewModels;

namespace RazorWebAppProject.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //[BindProperty]
        public IFormFile Photo { get; set; }

        public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public void OnGet()
        {
            RegisterViewModel = new() { PhotoPath = null };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            //Create new user
            var newUser = new ApplicationUser()
            {
                UserName = RegisterViewModel.Email,
                Email = RegisterViewModel.Email,
                FirstName = RegisterViewModel.FirstName,
                LastName = RegisterViewModel.LastName,
                Gender = RegisterViewModel.Gender,
                Address = RegisterViewModel.Address,
                PhotoPath = UploadApplicationUserImageOnTheServer()
            };

            //Sign the user in
            var result = await _userManager.CreateAsync(newUser, RegisterViewModel.Password);

            if (result.Succeeded)
            {
                //Redirect user to dashboard..
                if (_signInManager.IsSignedIn(User))
                {
                    return RedirectToPage("manageuser");
                }
                else
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToPage("../Admin/SuperUser/Index");
                }
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return Page();

        }


        public string UploadApplicationUserImageOnTheServer()
        {
            string guid = null;
            if (Photo != null)
            {
                //Photo has beeen selected...
                //for processing
                guid = Guid.NewGuid().ToString() + "__" + RegisterViewModel.FirstName + " " + RegisterViewModel.LastName + "__" + Photo.FileName;

                string webRootPath = Path.Combine(_webHostEnvironment.WebRootPath, "images",
                    "iamtuse_application_users_upload", guid);

                if (!System.IO.File.Exists(webRootPath))
                {
                    using var stream = new FileStream(webRootPath, FileMode.Create);

                    Photo.CopyTo(stream);
                }

                RegisterViewModel.PhotoPath = guid;

            }
            else
            {
                //No photo has been selected by the user
                //Photo Property is NULL

                if (RegisterViewModel.PhotoPath != null)
                {
                    //User has existing photo
                    //that needs to be deleted first before
                    //processing...
                    if (!RegisterViewModel.PhotoPath.Contains("no-image.png"))
                    {

                    }
                }
                else
                {

                    guid = null;
                }
            }





            return guid;
        }


    }
}

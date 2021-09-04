using System;
using System.IO;
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
            //User has selected a photo...
            if (Photo != null)
            {
                //User does not have an existing photo
                if (ApplicationUser.PhotoPath == null)
                {

                    var existingAppUser = await _userManager.FindByIdAsync(ApplicationUser.Id);
                    if (existingAppUser != null)
                    {
                        SettingAppUserProperties(existingAppUser);

                        var result = await _userManager.UpdateAsync(existingAppUser);

                        if (result.Succeeded)
                        {
                            TempData["UpdateSuccess"] = $"Record {existingAppUser.FirstName + " " + existingAppUser.LastName} updated successfully!";
                            return RedirectToPage("manageuser");
                        }

                        foreach (var errors in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, errors.Description);
                        }


                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/NotFound/Index");
                    }

                }
                else
                {
                    //User has an existing photo that needs to 
                    //be deleted...
                    var appUser = await _userManager.FindByIdAsync(ApplicationUser.Id);
                    if (appUser != null)
                    {
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "iamtuse_application_users_upload", ApplicationUser.PhotoPath);


                        if (System.IO.File.Exists(path))
                        {
                            if (!path.Contains("no-image.png"))
                            {
                                System.IO.File.Delete(path);
                            }
                        }

                        UploadApplicationUserImageOnTheServer(appUser: appUser);
                        var result = await _userManager.UpdateAsync(appUser);

                        if (result.Succeeded)
                        {
                            TempData["UpdateSuccess"] = $"Record {appUser.FirstName + " " + appUser.LastName} updated successfully!";
                            return RedirectToPage("manageuser");

                        }



                        foreach (var errors in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, errors.Description);
                        }



                        return Page();
                    }
                }
            }
            else
            {


                if (ApplicationUser.PhotoPath != null)
                {
                    //AppUser has existing photo
                    var userFound = await _userManager.FindByIdAsync(ApplicationUser.Id);

                    SettingInitialProperties(userFound);

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


                    return Page();


                }
                else
                {
                    var userFound = await _userManager.FindByIdAsync(ApplicationUser.Id);
                    SettingInitialProperties(userFound);

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


                    return Page();
                }
            }

            return Page();


        }
        private void SettingInitialProperties(ApplicationUser userFound)
        {
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
        }
        private void SettingAppUserProperties(ApplicationUser existingAppUser)
        {
            var webPath = Path.Combine(_webHostEnvironment.WebRootPath, "images",
                                        "iamtuse_application_users_upload");

            var newGuid = Guid.NewGuid().ToString() + "_" + existingAppUser.FirstName + "_" + existingAppUser.LastName + "_" + Photo.FileName;

            var combineFullPath = Path.Combine(webPath, newGuid);
            var stream = new FileStream(combineFullPath, FileMode.Create);
            Photo.CopyTo(stream);

            existingAppUser.FirstName = ApplicationUser.FirstName;
            existingAppUser.LastName = ApplicationUser.LastName;
            existingAppUser.Email = ApplicationUser.Email;
            existingAppUser.UserName = ApplicationUser.UserName;
            existingAppUser.Gender = ApplicationUser.Gender;
            existingAppUser.Address = ApplicationUser.Address;
            existingAppUser.PhotoPath = newGuid;
        }
        private void UploadApplicationUserImageOnTheServer(ApplicationUser appUser)
        {
            var guid = Guid.NewGuid().ToString() + "_" + appUser.FirstName + "_" + appUser.LastName + "_" + Photo.FileName;


            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "iamtuse_application_users_upload", guid);
            var stream = new FileStream(fullPath, FileMode.Create);
            Photo.CopyTo(stream);
            appUser.FirstName = ApplicationUser.FirstName;
            appUser.LastName = ApplicationUser.LastName;
            appUser.UserName = ApplicationUser.UserName;
            appUser.Gender = ApplicationUser.Gender;
            appUser.Address = ApplicationUser.Address;
            appUser.Email = ApplicationUser.Email;
            appUser.PhotoPath = guid;
        }

    }
}

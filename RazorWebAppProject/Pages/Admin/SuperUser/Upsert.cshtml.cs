using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.Models;
using RazorWebAppProject.Services;
using System;
using System.IO;

namespace RazorWebAppProject.Pages.Admin.SuperUser
{
   
    public class UpsertModel : PageModel
    {
        private readonly IDefaultRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }

        [BindProperty]
        public Employee SingleEmployee { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public UpsertModel(IDefaultRepository defaultRepository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = defaultRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public ActionResult OnGet(int? id)
        {
            if (id == null)
            {
                SingleEmployee = new()
                {
                    Image = null
                };
                return Page();
            }

            if (id.HasValue)
            {
                SingleEmployee = _repository.GetFirstOrDefaultEmployee(e => e.Id == id);

                if (SingleEmployee == null)
                {
                    return RedirectToPage("/NotFound/Index");
                }

                return Page();
            }

            return Page();

        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }

            if (SingleEmployee.Id == 0)
            {
                //Create new record....
                if (Photo == null)
                {
                    //User has not seleted a photo...
                    _repository.Add(SingleEmployee);
                    TempData["CreateMessage"] = $"New record added successful {SingleEmployee.Fullname}!";
                    return RedirectToPage("./Index");
                }
                else
                {
                    if (SingleEmployee.Image != null)
                    {
                        //Delete old image first
                        if (!SingleEmployee.Image.StartsWith("no-image.png"))
                        {
                            string fullPath = GeneratePath();

                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }

                            if (Photo != null)
                            {
                                string newPath = GeneratePath();
                                string guid = GenerateGuid();

                                string combine = Path.Combine(newPath, guid);
                                if (!System.IO.File.Exists(combine))
                                {
                                    using FileStream stream = new(combine, FileMode.Create);
                                    Photo.CopyTo(stream);
                                }
                                SingleEmployee.Image = guid;
                                _repository.Add(SingleEmployee);

                                TempData["CreateMessage"] = $"New record added successful {SingleEmployee.Fullname}";
                                return RedirectToPage("./Index");
                            }

                        }


                    }
                    else
                    {
                        string guid = GenerateGuid();

                        string combine = Path.Combine(_webHostEnvironment.WebRootPath, "images", "iamtuse_uploads", guid);

                        if (!System.IO.File.Exists(combine))
                        {
                            using FileStream stream = new(combine, FileMode.Create);
                            Photo.CopyTo(stream);
                        }
                        SingleEmployee.Image = guid;
                        _repository.Add(SingleEmployee);
                        TempData["CreateMessage"] = $"New record added successful {SingleEmployee.Fullname}";
                        return RedirectToPage("./Index");
                    }
                }

            }
            else
            {
                //Update existing record..
                if (SingleEmployee == null)
                {
                    return RedirectToPage("./NotFound/Index");
                }
                else
                {
                    if (Photo != null)
                    {
                        if (SingleEmployee.Image != null)
                        {
                            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "iamtuse_uploads", SingleEmployee.Image);
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                        }
                        UploadImage();
                    }
                    else
                    {
                        UploadImage();
                    }

                    SingleEmployee = _repository.Update(SingleEmployee);
                    TempData["UpdateMessage"] = "Update successful";
                    return RedirectToPage("./Index");
                }

            }

            return Page();
        }
        public ActionResult OnPostEmailAlertNotification()
        {
            SingleEmployee = _repository.GetFirstOrDefaultEmployee(e => e.Id == SingleEmployee.Id);

            if (Notify)
            {
                Message = $"Thank you for turning on email notification {SingleEmployee.Fullname}";
            }
            else
            {
                Message = $"You have turning off email notification service {SingleEmployee.Fullname}";
            }

            TempData["message"] = Message;
            return RedirectToPage("/Admin/SuperUser/View", new { id = SingleEmployee.Id });

        }
        private string GeneratePath()
        {
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "iamtuse_uploads", Photo.FileName);
            return fullPath;
        }

        private string GenerateGuid()
        {
            if (Photo != null)
            {
                string generateGuid = Guid.NewGuid().ToString() + "__" + SingleEmployee.FirstName + "_" + SingleEmployee.LastName + "__" + Photo.FileName;
                return generateGuid;
            }
            return null;
        }
        public void UploadImage()
        {
            if (Photo != null)
            {
                string guid = GenerateGuid();
                string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "iamtuse_uploads", guid);
                using FileStream stream = new(newPath, FileMode.Create);
                Photo.CopyTo(stream);

                SingleEmployee.Image = guid;
            }

        }
    }
}

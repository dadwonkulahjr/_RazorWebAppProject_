using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.Models;
using RazorWebAppProject.Services;

namespace RazorWebAppProject.Pages.Admin.SuperUser
{
    public class DeleteModel : PageModel
    {
        private readonly IDefaultRepository _defaultRepository;
        public Employee Employee { get; set; }
        public DeleteModel(IDefaultRepository defaultRepository)
        {
            _defaultRepository = defaultRepository;
        }
        public ActionResult OnGet(int id)
        {
            Employee = _defaultRepository.GetFirstOrDefaultEmployee(e => e.Id == id);
            if (Employee == null)
            {
                return RedirectToAction("/NotFound/Index");
            }


            return Page();
        }

        public ActionResult OnPost(int id)
        {
            Employee = _defaultRepository.GetFirstOrDefaultEmployee(e => e.Id == id);
            if (Employee == null)
            {
                return RedirectToPage("./NotFound/Index");
            }

            _defaultRepository.Delete(Employee);
            TempData["DeleteConfirmation"] = "Delete Successful " + Employee.Fullname;
            return RedirectToPage("./Index");
        }
    }
}

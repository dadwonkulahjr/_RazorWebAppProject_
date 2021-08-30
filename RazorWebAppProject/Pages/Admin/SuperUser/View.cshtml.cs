using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.Models;
using RazorWebAppProject.Services;

namespace RazorWebAppProject.Pages.Admin.SuperUser
{
    public class ViewModel : PageModel
    {
        private readonly IDefaultRepository _defaultRepository;
        [TempData]
        public string Message { get; set; }
        public Employee Employee { get; set; }
        public ViewModel(IDefaultRepository defaultRepository)
        {
            _defaultRepository = defaultRepository;
        }
        public ActionResult OnGet(int id)
        {
            Employee = _defaultRepository.GetFirstOrDefaultEmployee(e => e.Id == id);
            if (Employee == null)
            {
                return RedirectToPage("./NotFound/Index");
            }


            return Page();
        }
    }
}

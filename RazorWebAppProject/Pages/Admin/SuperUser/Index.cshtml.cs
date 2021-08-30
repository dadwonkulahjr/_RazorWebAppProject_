using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppProject.Models;
using RazorWebAppProject.Services;
using System.Collections.Generic;
using System.Linq;

namespace RazorWebAppProject.Pages.Admin.SuperUser
{
    public class IndexModel : PageModel
    {
        private readonly IDefaultRepository _defaultRepository;
        [TempData]
        public string UpdateMessage { get; set; }
        [TempData]
        public string DeleteConfirmation { get; set; }
        [TempData]
        public string CreateMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IndexModel(IDefaultRepository defaultRepository)
        {
            _defaultRepository = defaultRepository;
        }
        public IEnumerable<Employee> Employees { get; set; }
    
        public void OnGet()
        {
            Employees = _defaultRepository.SearchForEmployee(SearchTerm).ToList();                                
        }
    }
}

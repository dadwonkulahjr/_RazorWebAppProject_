using Microsoft.AspNetCore.Mvc;
using RazorWebAppProject.Models;
using RazorWebAppProject.Services;
using System.Linq;

namespace RazorWebAppProject.ViewComponents.NumberOfDepartmentHeadCount
{
    public class NumberOfDepartmentHeadCountViewComponent : ViewComponent
    {
        private readonly IDefaultRepository _defaultRepository;
        public NumberOfDepartmentHeadCountViewComponent(IDefaultRepository repository)
        {
            _defaultRepository = repository;
        }
        public IViewComponentResult Invoke(Dept? departmentName = null)
        {
            var result = _defaultRepository.DepartmentHeadCounts(departmentName).ToList();
            return View(result);
        }
    }
}

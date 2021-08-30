using Microsoft.AspNetCore.Mvc;
using RazorWebAppProject.Services;
using System.Linq;

namespace RazorWebAppProject.Controllers
{
    [ApiController]
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private readonly IDefaultRepository _repository;
        public EmployeeController(IDefaultRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public JsonResult Get()
        {
            var list = _repository.Get()
                                  .OrderBy(e => e.Fullname)
                                  .ThenBy(e => e.FirstName)
                                  .ThenBy(e => e.LastName)
                                  .ThenBy(e => e.Gender)
                                  .ThenBy(e => e.Dept)
                                  .ToList();
            return Json(new { data = list });
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class DepartmentController : Controller
    {

        public DepartmentController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PhongBan newItem){
            return View();
        }
    }
}

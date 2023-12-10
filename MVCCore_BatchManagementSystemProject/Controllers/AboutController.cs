using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        [Route("about-ciit")]
        public IActionResult CIIT()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Founder()
        {
            return View();
        }
        public IActionResult Mentors()
        {
            return View();
        }
        [Route("why-ciit")]
        public IActionResult WhyCIIT()
        {
            return View();
        }

    }
}
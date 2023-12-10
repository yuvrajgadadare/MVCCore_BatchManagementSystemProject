using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;


namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class DeveloperController : Controller
    {
        // GET: Developer
        public ActionResult Course_Modules()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class CareerController : Controller
    {
        [Route("career")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
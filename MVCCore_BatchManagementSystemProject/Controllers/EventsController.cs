using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;


namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }
        [Route("celebration-at-ciit")]
        public ActionResult Slide()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;


namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        [Route("contact-us")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
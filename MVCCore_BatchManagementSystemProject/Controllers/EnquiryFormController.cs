using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class EnquiryFormController : Controller
    {
        // GET: EnquiryForm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DemoForm()
        {
            return View();
        }

    }
}
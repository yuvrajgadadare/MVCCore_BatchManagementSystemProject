using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class DummyController : Controller
    {
        // GET: Dummy
        public ActionResult Index()
        {
            return View();
        }

        public string NewReg(StudentModel s)
        {
            return "Success";
        }
    }
}
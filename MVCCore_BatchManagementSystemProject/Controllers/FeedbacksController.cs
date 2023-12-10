using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class FeedbacksController : Controller
    {
        // GET: Feedbacks
        [Route("students-valuable-feedback")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class CourseController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Route("Android/job-oriented")]

        public ActionResult Android_Development()
        {
            return View();
        }
    }
}

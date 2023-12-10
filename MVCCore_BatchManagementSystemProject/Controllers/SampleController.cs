using Microsoft.AspNetCore.Mvc;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class SampleController : Controller
    {

        [Route("Training/Demo")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

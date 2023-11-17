using Microsoft.AspNetCore.Mvc;

namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

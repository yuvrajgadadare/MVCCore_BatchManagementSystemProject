using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Services.Implementations;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Areas.Trainer.Controllers
{
    public class TrainerProfileController : Controller
    {
        ITrainerService trainerService;
        public TrainerProfileController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }
        public IActionResult Index()
        {

            if (HttpContext.Session.GetInt32("TrainerId") != null)
            {
                int trainerId = (int)HttpContext.Session.GetInt32("TrainerId");
                Tbltrainer t = trainerService.GetTrainer(trainerId);
                return View(t);

            }
            else
            {
                return Redirect("/Account/Login");
            }
        }
         

    }
}

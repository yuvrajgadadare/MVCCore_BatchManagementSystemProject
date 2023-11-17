using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class TrainerController : Controller
    {
        ITrainerService  trainerService;
        ITopicService  topicService;
        IExtraService extraService;
        private IWebHostEnvironment environment;
        public TrainerController(ITrainerService trainerService, IExtraService extraService, IWebHostEnvironment environment,ITopicService topicService)
        {
            this.trainerService=trainerService;
            this.extraService=extraService;
            this.environment=environment;
            this.topicService=topicService;
        }
        public IActionResult Index()
        {
            ViewBag.trainers = trainerService.GetTrainers();
            List<TbltrainingTopic>lst = topicService.GetTopics();
            ViewBag.topics = lst;
            //List<TrainerTopicModel> lsttopics = trainerService.GetTrainerWiseTopics(id);
            //ViewBag.trainertopics=lsttopics;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Tbltrainer d, IFormFile photo)
        {
            string imgname = d.TrainerName + Path.GetExtension(photo.FileName);
            string imgpath = environment.WebRootPath + "/images/trainers/" + imgname;
            FileStream fs = new FileStream(imgpath, FileMode.Create, FileAccess.Write);
            photo.CopyTo(fs);
            d.ProfilePhoto = imgname;
            string password = extraService.GetRandomPassword(10);
            d.Password = password;
            trainerService.AddTrainer(d);
            string message = "<h2>Dear<br/> " + d.TrainerName + ",</h2><p>Your registration has been completed successfully.You can login by email address <b>" + d.EmailAddress + "</b> and password <b>" + password + "</b></p><br/><br/><h4>Regards,CIIT Training Institute Pvt. Ltd.</h4>";
            extraService.Send_Gmail_Email(d.EmailAddress, "Registration Confirmation", message);
            ModelState.Clear();
            ViewBag.msg = "Trainer Added Successfully";
            ViewBag.trainers = trainerService.GetTrainers();
            //List<TrainerTopicModel> lsttopics = trainerService.GetTrainerWiseTopics(id);

            //ViewBag.trainertopics = lsttopics;
            List<TbltrainingTopic> lst = topicService.GetTopics();
            ViewBag.topics = lst;
            return View();
        }
     public string AddTrainerTopics(Tbltrainer t)
        {
            foreach(TbltrainerTopic p in t.TbltrainerTopics)
            {
                p.TrainerId = t.TrainerId;
                trainerService.AddTrainerTopic(p);
            }
            return "Topics for Trainer Added Successfully";
        }
        public IActionResult TrainerTopicView()
        {
            return View();
        }

        public JsonResult GetTrainerTopics(int id)
        {
            List<TrainerTopicModel> lst = trainerService.GetTrainerWiseTopics(id);
            return Json(lst);
        }
        public IActionResult DemoView()
        {
            return View();
        }
    }
}

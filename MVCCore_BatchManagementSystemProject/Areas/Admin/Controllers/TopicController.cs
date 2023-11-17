using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class TopicController : Controller
    {
        
        ITopicService topicService;
        public TopicController(ITopicService topicService)
        {
            this.topicService = topicService;
        }

        public IActionResult Index()
        {
            ViewBag.topics = topicService.GetTopics();
            return View();
        }
        [HttpPost]
        public IActionResult Index(TbltrainingTopic topic)
        {
            topicService.AddTopic(topic);
            ViewBag.msg = "Topic Added Successfully";
            ModelState.Clear();
            ViewBag.topics = topicService.GetTopics();

            return View();
        }
        public JsonResult GetTopics()
        {
            return Json(topicService.GetTopics());
        }
    }
}

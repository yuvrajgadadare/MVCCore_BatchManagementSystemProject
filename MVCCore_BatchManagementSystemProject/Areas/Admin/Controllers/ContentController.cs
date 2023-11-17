using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        ITopicService topicService;
        ITopicContentService contentService;
        public ContentController(ITopicService topicService, ITopicContentService contentService)
        {
            this.topicService = topicService;
            this.contentService = contentService;
        }

        public IActionResult Index()
        {
            ViewBag.contents = contentService.GetTopicContents();
            ViewBag.topics = GetTopics();
            return View();
        }
        [HttpPost]
        public IActionResult Index(TbltopicContent c)
        {
            contentService.AddTopicContent(c);
            ViewBag.contents = contentService.GetTopicContents();
            ViewBag.topics = GetTopics();
            ViewBag.msg = "Content Added Successfully";
            return View();
        }

        public string AddContents(TbltrainingTopic t)
        {
            foreach(TbltopicContent c in t.TbltopicContents)
            {
                c.TopicId = t.TopicId;
                contentService.AddTopicContent(c);
            }

            return "Contents Added Successfully";
        }
        public List<SelectListItem> GetTopics()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach(TbltrainingTopic t in topicService.GetTopics())
            {
                SelectListItem s = new SelectListItem() { Text=t.TopicName, Value=t.TopicId.ToString() };
                lst.Add(s);
            }
            return lst;
        }

        public JsonResult GetContents()
        {
            return Json(contentService.GetTopicContents());
        }
        public JsonResult GetTopicWiseContents(int topic_id)
        {
            return Json(contentService.GetTopicContents().Where(e=>e.TopicId.Equals(topic_id)).ToList());
        }
        public IActionResult AddContent()
        {
            ViewBag.topics = GetTopics();
            return View();
        }

        [HttpPost]
        public string DeleteContent(int content_id)
        {
            contentService.DeleteTopicContent(content_id);
            return "Content Deleted Successfully";
        }
    }
}
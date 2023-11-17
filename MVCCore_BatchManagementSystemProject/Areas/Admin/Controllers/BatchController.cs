using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Services.Implementations;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class BatchController : Controller
    {
        IBatchService batchService;
        ITrainerService trainerService;
        ITopicService topicService;
        IStudentService studentService;
        public BatchController(IBatchService batchService, ITrainerService trainerService,ITopicService topicService, IStudentService studentService)
        {
            this.batchService = batchService;
            this.trainerService = trainerService;
            this.topicService= topicService;
            this.studentService= studentService;
        }
        public IActionResult Index()
        {
            ViewBag.topics = GetTopics();
            ViewBag.trainers = GetTrainers();
            ViewBag.batches = batchService.GetBatches();
            return View();
        }
        public JsonResult GetTopicWiseStudents(int topic_id)
        {
            List<StudentCourseModel> lst = studentService.GetAllStudents();
            lst = lst.Where(e=>e.TopicId.Equals(topic_id)).ToList();
            return Json(lst);
        }
        public JsonResult GetBatchWiseStudents(int batch_id)
        {
            List<StudentBatchModel> lst = batchService.GetBatchStudents().Where(e => e.BatchId.Equals(batch_id)).ToList();
            return Json(lst);
        }
        public JsonResult GetBatchWiseAbsentStudents(int batch_id,int topic_id)
        {
            List<StudentCourseModel> lst = new List<StudentCourseModel>();
            List<StudentCourseModel> topiclst = studentService.GetAllStudents().Where(e => e.TopicId.Equals(topic_id)).ToList();
            List<StudentBatchModel> batchlst = batchService.GetBatchStudents().Where(e => e.BatchId.Equals(batch_id)).ToList();

            if (batchlst.Count == 0)
            {
                lst = topiclst;
            }
            else
            {
                foreach (StudentCourseModel s in topiclst)
                {
                    StudentBatchModel b=batchlst.FirstOrDefault(e=>e.StudentId.Equals(s.StudentId));
                    if (b == null)
                    {
                        lst.Add(s);
                    }
               
                }
            }
            return Json(lst);
        }
        [HttpPost]
        public IActionResult Index(Tblbatch b)
        {
            batchService.AddBatch(b);
            ViewBag.msg = "Batch Created Successfully";         
            ViewBag.topics = GetTopics();
            ViewBag.trainers = GetTrainers();
            ViewBag.batches = batchService.GetBatches();
            return View();
        }
        [HttpPost]
        public string AddBatchDetails(Tblbatch b)
        {
            batchService.AddBatch(b);
            return "Batch Details Added Successfully";
        }

        [HttpPost]
        public string AddBatchStudents(Tblbatch b)
        {
            foreach(TblbatchStudent s in b.TblbatchStudents)
            {
                s.BatchId = b.BatchId;
                batchService.AddBatchStudent(s);
            }
            return "Batch Students Added Successfully";
        }
        public List<SelectListItem> GetTopics()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach(TbltrainingTopic t in topicService.GetTopics())
            {
                SelectListItem s = new SelectListItem() { Text = t.TopicName, Value = t.TopicId.ToString() };
                lst.Add(s);
            }
            return lst;
        }
        public List<SelectListItem> GetTrainers()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach (Tbltrainer t in trainerService.GetTrainers())
            {
                SelectListItem s = new SelectListItem() { Text = t.TrainerName, Value = t.TrainerId.ToString() };
                lst.Add(s);
            }
            return lst;
        }

        public ActionResult ViewBatch(int batch_id)
        {
            Tblbatch batch = batchService.GetAllBatches().FirstOrDefault(e => e.BatchId.Equals(batch_id));
            batch.TblbatchSchedules = batchService.GetBatchSchedule((int)batch.BatchId);
            return View(batch);
        }



        public IActionResult AddBatchSchedule(int batch_id)
        {
            List<TblbatchSchedule>lst=batchService.GetBatchWiseSchedule(batch_id);
            foreach(TblbatchSchedule t in lst)
            {
              
                batchService.AddBatchSchedule(t);
            }
            return Redirect("/Admin/Batch/Index");
        }
    }
}

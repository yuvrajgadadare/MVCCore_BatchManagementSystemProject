using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Areas.Trainer.Controllers
{
    public class AttendanceController : Controller
    {
        ITrainerService trainerService;
        IBatchService batchService;
        IStudentService studentService;
        public AttendanceController(ITrainerService trainerService, IBatchService batchService, IStudentService studentService)
        {
            this.trainerService = trainerService;
            this.batchService = batchService;
            this.studentService = studentService;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("TrainerId") != null)
            {
                int trainerId = (int)HttpContext.Session.GetInt32("TrainerId");
                Tbltrainer t = trainerService.GetTrainer(trainerId);
                List<Tblbatch> batches = batchService.GetTrainerWiseBatches(trainerId);
                ViewBag.batches = GetDropdownBatches(batches);
                return View();
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        public List<SelectListItem> GetDropdownBatches(List<Tblbatch> lst)
        {

            List<SelectListItem> batches = new List<SelectListItem>();
            foreach (Tblbatch batch in lst)
            {
                SelectListItem s = new SelectListItem() { Value = batch.BatchId.ToString(), Text = batch.BatchName + "(" + batch.BatchTime + ")" };
                batches.Add(s);
            }
            return batches;
        }
        public JsonResult GetBatchStudents(int batch_id)
        {
            List<StudentBatchModel> lst = batchService.GetBatchWiseStudents(batch_id);
            return Json(lst);
        }

        public JsonResult BatchSchedule(int batch_id)
        {
            List<TblbatchSchedule>lst=batchService.GetBatchSchedule(batch_id).Where(e=>e.ActualDate.Equals(null)).ToList().OrderBy(e=>e.BatchScheduleId).ToList();
            return Json(lst);
        }
        public string SubmitAttendace(TblbatchScheduleAttendance b)
        {
            batchService.AddAttendance(b);
            return "Success";
        }

        public JsonResult GetMarkedAttendance(int batch_id)
        {
            List<BatchAttendanceModel> lst = batchService.GetBatchWiseAttendance(batch_id);

            return Json(lst);
        }
    }
}

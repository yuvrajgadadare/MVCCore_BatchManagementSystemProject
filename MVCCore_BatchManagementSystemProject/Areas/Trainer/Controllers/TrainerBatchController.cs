using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Areas.Trainer.Controllers
{
    public class TrainerBatchController : Controller
    {
        ITrainerService trainerService;
        IBatchService batchService;
        IStudentService studentService;
        public TrainerBatchController(ITrainerService trainerService,IBatchService batchService,IStudentService studentService)
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
                return View(batches);
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        public string AddBatchStudent(Tblbatch b)
        {
       // batchService.AddBatchStudent(s);
       foreach(TblbatchStudent s in b.TblbatchStudents)
            {
                batchService.AddBatchStudent(s);
            }
            return "Student Added in the batch";
        }
        public JsonResult GetRegisteredStudents()
        {
            return Json(studentService.GetStudentRegistrations());
        }

        public IActionResult BatchDetails(int id)
        {
            if (HttpContext.Session.GetInt32("TrainerId") != null)
            {
                int trainerId = (int)HttpContext.Session.GetInt32("TrainerId");
                Tbltrainer t = trainerService.GetTrainer(trainerId);
                Tblbatch  batch = batchService.GetTrainerWiseBatches(trainerId).FirstOrDefault(e=>e.BatchId.Equals(id));
                batch.TblbatchSchedules = batchService.GetBatchWiseSchedule((int)batch.BatchId);
                return View(batch);
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }
        public IActionResult ShowDetails(int id)
        {
            if (HttpContext.Session.GetInt32("TrainerId") != null)
            {
                int trainerId = (int)HttpContext.Session.GetInt32("TrainerId");
                Tbltrainer t = trainerService.GetTrainer(trainerId);
                Tblbatch batch = batchService.GetTrainerWiseBatches(trainerId).FirstOrDefault(e => e.BatchId.Equals(3));
                return View(batch);
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }

        public ActionResult BatchAttendance()
        {
            if (HttpContext.Session.GetInt32("TrainerId") != null)
            {
                int trainerId = (int)HttpContext.Session.GetInt32("TrainerId");
                Tbltrainer t = trainerService.GetTrainer(trainerId);
                List<Tblbatch> batch = batchService.GetTrainerWiseBatches(trainerId);
                ViewBag.batches=GetDropdownBatches(batch);
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
                SelectListItem s = new SelectListItem() { Value=batch.BatchId.ToString(), Text=batch.BatchName+"("+batch.BatchTime+")" };
                batches.Add(s);
            }
            return batches;
        }
      

        public JsonResult BatchWiseSchedule(int batch_id)
        {
            
            //    List<TblbatchSchedule> schedule = batchService.GetBatchWiseSchedule((int)batch_id);
            List<TblbatchSchedule> lst = batchService.GetBatchSchedule(batch_id);
         
                return Json(lst);
           
        }
        public JsonResult BatchContent(string id)
        {

            string[] data = System.Text.RegularExpressions.Regex.Split(id, "_");
            int batch_id = Convert.ToInt32(data[0]);
            int content_id = Convert.ToInt32(data[1]);

            TblbatchSchedule  lst = batchService.GetBatchSchedule(batch_id).FirstOrDefault(e=>e.ContentId.Equals(content_id));


            return Json(lst);

         

        }
        public JsonResult BatchWiseStudents(int batch_id)
        {
           List<StudentBatchModel>lst=batchService.GetBatchWiseStudents(batch_id);
            return Json(lst);
        }
        public JsonResult GetStudentBatchWiseAttendance(int batch_id,int registration_id)
        {
            List<BatchStudentAttendanceModel> lst = batchService.GetStudentWiseBatchAttendance(batch_id, registration_id);
            return Json(lst);
        }
        [HttpPost]
        public string MarkAttendance(BatchAttendanceModel b)
        {

            List<StudentBatchModel> batchstudents = batchService.GetBatchWiseStudents(b.batch_id);
            List<TblscheduleAttendance> attendances = new List<TblscheduleAttendance>();
            List<int> registrationids = new List<int>();
            foreach (AttendanceStudentModel a in b.students)
            {
                registrationids.Add(a.registration_id);
            }

            foreach (StudentBatchModel a in batchstudents)
            {

                TblscheduleAttendance ad = null;
                if (registrationids.IndexOf(a.RegistrationId)>=0)
                {
                    ad = new TblscheduleAttendance() { RegistrationId = a.RegistrationId, IsPresent = 1 };
                }
                else
                {
                    ad = new TblscheduleAttendance() { RegistrationId = a.RegistrationId, IsPresent = 0};
                }
                attendances.Add(ad);
            }

            TblbatchSchedule bt=batchService.GetBatchSchedule(b.batch_id).FirstOrDefault(e=>e.ContentId.Equals(b.content_id));

            TblbatchScheduleAttendance bs = new TblbatchScheduleAttendance() { BatchScheduleId=bt.BatchScheduleId, AttendanceDate=b.actual_date, TblscheduleAttendances=attendances };
            //List<TblbatchScheduleAttendance> lst = new List<TblbatchScheduleAttendance>();
            //lst.Add(bs);
            //TblbatchSchedule batch = new TblbatchSchedule() { BatchId = (int)b.batch_id, ExpectedDate=b.expected_date, ActualDate = b.actual_date, ContentId = b.content_id,  TblbatchScheduleAttendances=lst };
            batchService.AddAttendance(bs);
            return "Success";
        }
    }
}
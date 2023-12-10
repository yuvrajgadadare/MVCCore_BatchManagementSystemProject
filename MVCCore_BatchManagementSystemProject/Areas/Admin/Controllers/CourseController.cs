using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        ITrainingCourseService courseService;
        IExtraService extraService;
        
        public CourseController(ITrainingCourseService courseService,IExtraService extraService)
        {
            this.courseService = courseService;
            this.extraService = extraService;
        }
        public IActionResult Index()
        {
            List<TbltrainingCourse> lst = courseService.GetAllCourseDetails();
            return View(lst);
        }

        public IActionResult CourseSyllabus(int course_id)
        {
            TbltrainingCourse c = courseService.GetAllCourseDetails().FirstOrDefault(e=>e.CourseId.Equals(course_id));  
            return View(c);
        }
        //[HttpPost]
        //public IActionResult Index(CourseModel c)
        //{
        //    CourseModel trc = courseService.GetCourses().FirstOrDefault(e => e.CourseName.Equals(c.CourseName));
        //    if (trc == null)
        //    {

        //        TbltrainingCourse cr = new TbltrainingCourse() { CourseName = c.CourseName };
        //        List<TbltrainingCourseFee> fees = new List<TbltrainingCourseFee>();
        //        fees.Add(new TbltrainingCourseFee() { FeeMode = c.FeeMode, FeesAmount = c.FeeAmount, Gst = c.FeeAmount });
        //        cr.TbltrainingCourseFees = fees;
        //        courseService.AddCourse(cr);
        //    }
        //    else
        //    {
        //        TbltrainingCourseFee f = new TbltrainingCourseFee() 
        //        {
        //         CourseId=trc.CourseId,
        //          FeeMode=c.FeeMode,
        //           FeesAmount=c.FeeAmount,
        //            Gst=c.Gst

        //        };
        //        courseService.AddCourseFees(f);
        //    }
        //        ViewBag.msg = "Training Course Details Added Successfully";
        //    return View();
        //}
        [HttpPost]
        public string AddCourseDetails(TbltrainingCourse c)
        {
            courseService.AddCourse(c);
            return "Course Details Added Successfully";
        }
        //public JsonResult GetCourses()
        //{
        //    List<CourseModel> lst = courseService.GetCourses();
        //    return Json(lst);
        //}

        public JsonResult GetCourseTopics(int course_id)
        {
            List<TopicCourseModel> lst = courseService.GetCourseWiseTopics(course_id);
            return Json(lst);
        }

        public string AddCourseTopics(TbltrainingCourse c)
        {
            foreach(TbltrainingCourseTopic t in c.TbltrainingCourseTopics)
            {
                t.CourseId = c.CourseId;
                courseService.AddCourseTopic(t);
            }
            return "Course Topics Added Successfully";
        }


        public IActionResult CreatePDFDocument()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDocument()
        {
            
            return View();
        }


        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string ExportData)
        //{
        //    using (MemoryStream stream = new System.IO.MemoryStream())
        //    {
        //        StringReader reader = new StringReader(ExportData);
        //        Document PdfFile = new Document(PageSize.A4);
        //        PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
        //        PdfFile.Open();
        //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
        //        PdfFile.Close();
        //        return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
        //    }
        //}        

    }
}

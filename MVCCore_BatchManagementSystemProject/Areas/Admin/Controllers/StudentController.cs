using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services;
using MVCCore_BatchManagementSystemProject.Services.Implementations;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
using Razorpay.Api;

namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class StudentController : Controller
    {
        IStudentService studentService;
        ITrainingCourseService trainingCourseService;
        IExtraService extraService;
        ITrainingCourseService courseService;
        IBatchService batchService;
   
       
        private IWebHostEnvironment environment;

        public StudentController(IStudentService studentService, ITrainingCourseService trainingCourseService, IWebHostEnvironment environment,IExtraService extraService,ITrainingCourseService courseService,IBatchService batchService)
        {
            this.studentService = studentService;
            this.trainingCourseService = trainingCourseService;
            this.environment= environment;
            this.extraService = extraService;
            this.courseService = courseService;
            this.batchService = batchService;
        } 

        public List<SelectListItem> GetCourses()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach(CourseModel r in courseService.GetCourses())
            {
                SelectListItem s = new SelectListItem() 
                {
                 Text=r.CourseName+"("+r.FeeAmount+"-"+r.FeeMode+")",
                  Value=r.FeeId.ToString()
                };
                lst.Add(s);
            }
            return lst;
        }
        public IActionResult Index()
        {
            ViewBag.courses = GetCourses();
            ViewBag.students = studentService.GetStudentRegistrations();
            return View();
        }

        [HttpPost]
        public ActionResult Index(TblstudentDetail d ,IFormFile photo,DateTime registrationdate,string feeid, string discount)
        {
            string imgname = d.StudentName + Path.GetExtension(photo.FileName);
            string imgpath = environment.WebRootPath + "/images/users/" + imgname;
            FileStream fs = new FileStream(imgpath, FileMode.Create, FileAccess.Write);
            photo.CopyTo(fs);
            d.ProfilePhoto = imgname;
            string password= extraService.GetRandomPassword(10);
            d.Password = password;
            TblstudentRegistration rs = new TblstudentRegistration()
            {
                RegistrationDate = registrationdate,
                FeeId = Convert.ToInt32(feeid),
                Discount = (float)Convert.ToDouble(discount)
            };
            d.TblstudentRegistrations.Add(rs);
            studentService.AddStudentDetails(d);
            string message = "<h2>Dear<br/> "+d.StudentName+",</h2><p>Your registration has been completed successfully.You can login by email address <b>"+d.EmailAddress+"</b> and password <b>"+password+"</b></p><br/><br/><h4>Regards,CIIT Training Institute Pvt. Ltd.</h4>";
            extraService.Send_Gmail_Email(d.EmailAddress, "Registration Confirmation", message);
            ModelState.Clear();
            ViewBag.msg = "Student Added Successfully";
            ViewBag.courses = GetCourses();
            ViewBag.students = studentService.GetStudentRegistrations();
            return View();
        }

        public ActionResult Registrations()
        {
            List<StudentRegistrationModel> lst = studentService.GetStudentRegistrations();
            return View(lst);
        }
        public IActionResult ViewStudent(int registration_id)
        {
            TblstudentDetail sd = studentService.GetStudentDetails(registration_id);
            List<StudentBatchModel> batches=batchService.GetBatchStudents().Where(e=>e.RegistrationId.Equals(registration_id)).ToList();

            return View(sd);
        }
        public JsonResult GetAllRegistrations()
        {
            List<StudentRegistrationModel> lst = studentService.GetStudentRegistrations();
            return Json(lst);
        }
        public IActionResult StudentPayment()
        {
            ViewBag.students= GetStudents();
            return View();
        }

        public List<SelectListItem> GetStudents()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            foreach (StudentCourseDetailsModel st in studentService.GetStudentPayments())
            {
                SelectListItem s = new SelectListItem() { Text = st.StudentName, Value = st.RegistrationId.ToString() };
                lst.Add(s);
            }
            return lst;
        }
        public IActionResult PayFees(int? registration_id)
        {
            StudentCourseDetailsModel sd = studentService.GetRegistartionWisePayments((int)registration_id);
            ViewBag.details = sd;
            ViewBag.modes = PaymentModes();
            ViewBag.students = GetStudents();
            TblstudentPayment p = new TblstudentPayment() { RegistrationId=sd.RegistrationId,PaymentDate=DateTime.Now};
            return View(p);
        }
        [HttpPost]
        public IActionResult PayFees(TblstudentPayment p)
        {
            studentService.AddStudentPayment(p);
            ViewBag.msg = "Fees Payment Accepted Successfully";
            ModelState.Clear();
            StudentCourseDetailsModel sd = studentService.GetRegistartionWisePayments((int)p.RegistrationId);
            ViewBag.details = sd;
            ViewBag.students = GetStudents();
            ViewBag.modes = PaymentModes();
            TblstudentPayment pm = new TblstudentPayment() { RegistrationId = sd.RegistrationId, PaymentDate = DateTime.Now };
            return View(pm);
        }
        public List<SelectListItem> PaymentModes()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text="Cash", Value= "Cash" });
            lst.Add(new SelectListItem() { Text="Cheque", Value= "Cheque" });
            lst.Add(new SelectListItem() { Text="Phone Pay", Value= "Phone Pay" });
            lst.Add(new SelectListItem() { Text="Google Pay", Value= "Google Pay" });
            lst.Add(new SelectListItem() { Text="Paytm", Value= "Paytm" });
            lst.Add(new SelectListItem() { Text="Net Banking", Value= "Net Banking" });
            lst.Add(new SelectListItem() { Text="Other", Value= "Other" });
            return lst;
        }
        public IActionResult Invoice(int payment_id)
        {
            StudentPaymentModel sp=studentService.GetStudentPayment(payment_id);
            StudentCourseDetailsModel c=studentService.GetRegistartionWisePayments((int)sp.RegistrationId);
            List<StudentPaymentModel> payments = studentService.GetRegistrationWisePayments(payment_id);
            ViewBag.payments = payments;
            ViewBag.st = c;
            return View(sp); 
        }

        public IActionResult AllInvoices()
        {
            List<StudentPaymentModel> payments = studentService.GetAllPayments();
            return View(payments);
        }

         public JsonResult GetFilteredPayments()
        {

            //if (search != null)
            //{
            //    List<StudentPaymentModel> payments = studentService.GetAllPayments().Where(e => e.student_name.ToLower().Contains(search.ToLower())).ToList();
            //    return Json(payments);
            //}
            //else
            //{
                List<StudentPaymentModel> payments = studentService.GetAllPayments();
                return Json(payments);

            //}

          
        }
        public IActionResult GetStudentdetails(int registration_id)
        {
            TblstudentRegistration sd = studentService.GetStudentRegistrationDetails(registration_id);
          //  TblstudentDetail std = studentService.GetStudentDetails((int)sd.StudentId);
            List<Tblbatch> lst = batchService.GetStudentWiseBatchdetails(1);
            StudentModel sm = new StudentModel()
            {
                //studentdetails = std,
                 registration =sd,
                  batches=lst
            };

            return View(sm);
        }
    }
}

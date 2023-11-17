using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
using Newtonsoft.Json;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class RegistrationController : Controller
    {
        IStudentService studentService;
        ITrainingCourseService trainingCourseService;
        IExtraService extraService;
        ITrainingCourseService courseService;


        private IWebHostEnvironment environment;

        public RegistrationController(IStudentService studentService, ITrainingCourseService trainingCourseService, IWebHostEnvironment environment, IExtraService extraService, ITrainingCourseService courseService)
        {
            this.studentService = studentService;
            this.trainingCourseService = trainingCourseService;
            this.environment = environment;
            this.extraService = extraService;
            this.courseService = courseService;
        }

        public List<SelectListItem> GetCourses()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach (CourseModel r in courseService.GetCourses())
            {
                SelectListItem s = new SelectListItem()
                {
                    //Text = r.CourseName + "(" + r.FeeAmount + "-" + r.FeeMode + ")",
                    Text = r.CourseName + " With "+ r.FeeMode + " Fees",
                    Value = r.FeeId.ToString()
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
        public IActionResult Index(RegistrationModel r,IFormFile photo)
        {
            string imgname = r.StudentName + Path.GetExtension(photo.FileName);
            string imgpath = environment.WebRootPath + "/images/users/" + imgname;
            FileStream fs = new FileStream(imgpath, FileMode.Create, FileAccess.Write);
            photo.CopyTo(fs);
            r.ProfilePhoto = imgname;
            string password = extraService.GetRandomPassword(10);
            r.Password = password;
            HttpContext.Session.SetString("registration", JsonConvert.SerializeObject(r));
            //ViewBag.courses = GetCourses();
            //ViewBag.students = studentService.GetStudentRegistrations();


            Random randomObj = new Random();
                string transactionId = randomObj.Next(10000000, 100000000).ToString();

                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_2YNZNBy4uflUsd", "HgPEeHOX2u1cChdZMPRHVdM4");
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", r.RegistrationAmount * 100);  // Amount will in paise
                options.Add("receipt", transactionId);
                options.Add("currency", "INR");
                options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                string orderId = orderResponse["id"].ToString();
            MerchantOrder orderModel = new MerchantOrder
            {
                OrderId = orderResponse.Attributes["id"],
                RazorpayKey = "rzp_live_2YNZNBy4uflUsd",
                    Amount = (float)r.RegistrationAmount * 100,
                    Currency = "INR",
                    Name = r.StudentName,
                    Email =r.EmailAddress,
                    PhoneNumber =r.MobileNumber,
                    Address =r.Address,
                    Description = "Registration For CIIT Training Institute"
                };
            
            //db.tblcollege_student_details.Add(st);
            //db.SaveChanges();
            //ViewBag.msg = "Thank you for submitting your information successfully.Our representative will connect you by call or email.";
            //ViewBag.topics = GetTrainings();
            //return RedirectToAction("Payment");
            return View("PaymentPage", orderModel);
        }


        [HttpPost]
        public ActionResult Complete(string rzp_paymentid, string rzp_orderid)
        {
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            //string paymentId = Request.Params["rzp_paymentid"];

            //// This is orderId
            //string orderId = Request.Params["rzp_orderid"];

            string paymentId = rzp_paymentid;

            // This is orderId
            string orderId = rzp_orderid;

            //            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_umbrFAbVJ3slyJ", "su9eXFaihGucMmKECVRcRk0Q");
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_2YNZNBy4uflUsd", "HgPEeHOX2u1cChdZMPRHVdM4");

            //            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_WG2hMQTKiy6lLZ", "WrFjnp4HDgAEu6ri8uM7qdbB");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                string data = HttpContext.Session.GetString("registration");
                RegistrationModel r = JsonConvert.DeserializeObject<RegistrationModel>(data);

                TblstudentPayment pay = new TblstudentPayment() { PaymentAmount = r.RegistrationAmount, PaymentDate = DateTime.Now, PaymentMode = "Razor Pay", PaymentDescription = "Payment for Registration" };
                List<TblstudentPayment>payments = new List<TblstudentPayment>();
                payments.Add(pay);
                TblstudentRegistration reg = new TblstudentRegistration() {
                 Discount = r.Discount,
                  FeeId = r.FeeId,
                   RegistrationDate = r.RegistrationDate,
                    TblstudentPayments = payments
                    
                     
                };
                List<TblstudentRegistration> reglist = new List<TblstudentRegistration>();
                reglist.Add(reg);
                TblstudentDetail sd = new TblstudentDetail() 
                {
                 StudentName=r.StudentName,
                  MobileNumber=r.MobileNumber,
                   EmailAddress=r.EmailAddress,
                    BirthDate=r.BirthDate,
                     Gender=r.Gender,
                      Password=r.Password,
                       ProfilePhoto=r.ProfilePhoto,
                        Qualification=r.Qualification,
                         TblstudentRegistrations=reglist
                        
                };
                studentService.AddStudentDetails(sd);
                string message = "<h2>Dear<br/> " + r.StudentName + ",</h2><p>Your registration has been completed successfully.You can login by email address <b>" + r.EmailAddress + "</b> and password <b>" + r.Password + "</b></p><br/><br/><h4>Regards,CIIT Training Institute Pvt. Ltd.</h4>";
                extraService.Send_Gmail_Email(r.EmailAddress, "Registration Confirmation", message);
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
            return View();
        }









        public JsonResult GetFee(int fee_id)
        {
            CourseModel cm = courseService.GetFeeIdWiseCourse(fee_id);
            return Json(cm);
        }
    }
}

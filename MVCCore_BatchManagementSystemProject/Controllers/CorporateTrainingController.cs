using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class CorporateTrainingController : Controller
    {
//        CIIT_CRMDBEntities db;
//        public CorporateTrainingController()
//        {
//            db = new CIIT_CRMDBEntities();

//        }

//        [Route("industry-training")]
//        public ActionResult Index()
//        {
//            ViewBag.topics = GetTrainings();
//            tblcollege_student_details t = new tblcollege_student_details() { training_amount=1};
//            return View(t);
//        }
//        [Route("industry-training")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Index(tblcollege_student_details st)
//        {
//            if (!ModelState.IsValid)
//            {
//                ViewBag.topics = GetTrainings();
//                tblcollege_student_details t = new tblcollege_student_details();
//                return View(t);
//            }
//            else
//            {
//                st.fees_paid = 0;
//                //st.training_amount = 2000;
//                st.registration_date = DateTime.Now;
//                Session["student"] = st;
//                Random randomObj = new Random();
//                string transactionId = randomObj.Next(10000000, 100000000).ToString();

//                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_2YNZNBy4uflUsd", "HgPEeHOX2u1cChdZMPRHVdM4");
//                Dictionary<string, object> options = new Dictionary<string, object>();
//                options.Add("amount", st.training_amount * 100);  // Amount will in paise
//                options.Add("receipt", transactionId);
//                options.Add("currency", "INR");
//                options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
//                Razorpay.Api.Order orderResponse = client.Order.Create(options);
//                string orderId = orderResponse["id"].ToString();
//                OrderModel orderModel = new OrderModel
//                {
//                    orderId = orderResponse.Attributes["id"],
//                    razorpayKey = "rzp_live_2YNZNBy4uflUsd",
//                    amount = (float)st.training_amount,
//                    currency = "INR",
//                    name = st.student_name,
//                    email = st.email_address,
//                    contactNumber = st.mobile_number,
//                    address = st.address,
//                    description = "Payment for Training"
//                };

//                //db.tblcollege_student_details.Add(st);
//                //db.SaveChanges();
//                //ViewBag.msg = "Thank you for submitting your information successfully.Our representative will connect you by call or email.";
//                //ViewBag.topics = GetTrainings();
//                //return RedirectToAction("Payment");
//                return View("PaymentPage", orderModel);
//            }
//        }
//        public class OrderModel
//        {
//            public string orderId { get; set; }
//            public string razorpayKey { get; set; }
//            public float amount { get; set; }
//            public string currency { get; set; }
//            public string name { get; set; }
//            public string email { get; set; }
//            public string contactNumber { get; set; }
//            public string address { get; set; }
//            public string description { get; set; }
//        }

//        [Route("Payment")]
//        public ActionResult Payment()
//        {
//            return View();
//        }




//        [HttpPost]
//        [Route("Complete")]
//        public ActionResult Complete()
//        {
//            // Payment data comes in url so we have to get it from url

//            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
//            string paymentId = Request.Params["rzp_paymentid"];

//            // This is orderId
//            string orderId = Request.Params["rzp_orderid"];

//            //            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_umbrFAbVJ3slyJ", "su9eXFaihGucMmKECVRcRk0Q");
//            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_2YNZNBy4uflUsd", "HgPEeHOX2u1cChdZMPRHVdM4");

////            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_WG2hMQTKiy6lLZ", "WrFjnp4HDgAEu6ri8uM7qdbB");

//            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

//            // This code is for capture the payment 
//            Dictionary<string, object> options = new Dictionary<string, object>();
//            options.Add("amount", payment.Attributes["amount"]);
//            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
//            string amt = paymentCaptured.Attributes["amount"];

//            //// Check payment made successfully

//            if (paymentCaptured.Attributes["status"] == "captured")
//            {
//                tblcollege_student_details st = (tblcollege_student_details)Session["student"];
//                st.fees_paid = 1;
//                st.payment_id = paymentId;
//                db.tblcollege_student_details.Add(st);
//                db.SaveChanges();
//                // Create these action method
//                return RedirectToAction("Success");
//            }
//            else
//            {
//                return RedirectToAction("Failed");
//            }
//        }

//        public ActionResult Success()
//        {
//            return View();
//        }

//        public ActionResult Failed()
//        {
//            return View();
//        }



//        public List<SelectListItem> GetTrainings()
//        {
//            List<SelectListItem> lst = new List<SelectListItem>();
//            lst.Add(new SelectListItem() { Text = "Angular", Value = "Angular" });
//            lst.Add(new SelectListItem() { Text = "React", Value = "React" });
//            return lst;
//        }


    }
}
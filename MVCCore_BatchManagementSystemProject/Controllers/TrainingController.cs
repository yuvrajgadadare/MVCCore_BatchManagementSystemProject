using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class TrainingController : Controller
    {
        // GET: Training
        [Route("Android-Development/job-oriented-android-development-training-with-live-project")]

        public ActionResult Android_Development()
        {
            return View();
        }
        [Route("Angular/job-oriented-angular-js-training-with-live-project")]
        public ActionResult Angular()
        {
            return View();
        }
        [Route("Big-Data-Hadoop/job-oriented-big-data-hadoop-training-with-live-project")]
        public ActionResult BigData_Hadoop()
        {
            return View();
        }
        [Route("Corporate-Training/corporate-training-with-live-project")]
        public ActionResult Corporate_Training()
        {
            return View();
        }
        [Route("Data-Science/job-oriented-data-science-training-with-live-project")]
        public ActionResult Data_Science()
        {
            return View();
        }
        [Route("DBA/job-oriented-dba-training-with-live-project")]
        public ActionResult DBA()
        {
            return View();
        }
        [Route("Digital-Marketing/job-oriented-digital-marketing-training-with-live-project")]
        public ActionResult Digital_Marketing()
        {
            return View();
        }
        [Route("Dot-Net/job-guaranteed-dot-net-training-with-live-project")]
        public ActionResult DotNet()
        {
            return View();
        }
        [Route("Front-End-Development/job-oriented-front-end-development-training-with-live-project")]
        public ActionResult Front_End_Development()
        {
            return View();
        }
        [Route("Graphics_Designing/job-oriented-graphics-designing-training-with-live-project")]
        public ActionResult Graphics_Designing()
        {
            return View();
        }
        [Route("Hardware-Networking/job-oriented-hardware-networking-training-with-live-project")]
        public ActionResult Hardware_Networking()
        {
            return View();

        }
        [Route("Java/job-oriented-java-training-with-live-project")]
        public ActionResult Java()
        {
            return View();
        }
        [Route("MEAN-Stack/job-oriented-mean-stack-training-with-live-project")]
        public ActionResult Mean_Stack()
        {
            return View();
        }
        [Route("NodeJs/job-oriented-node-js-training-with-live-project")]
        public ActionResult NodeJs()
        {
            return View();
        }
        [Route("Php/job-oriented-php-training-with-live-project")]
        public ActionResult Php()
        {
            return View();
        }
        [Route("Python/job-oriented-python-training-with-live-project")]
        public ActionResult Python()
        {
            return View();
        }
        [Route("React-Js/job-oriented-react-js-training-with-live-project")]
        public ActionResult ReactJs()
        {
            return View();
        }

        [Route("Software-Testing/job-oriented-software-testing-training-with-live-project")]
        public ActionResult Software_Testing()
        {
            return View();
        }
        
             [Route("MERN-Stack/job-oriented-mean-stack-training-with-live-project")]
        public ActionResult MERNStack()
        {
            return View();
        }
        [Route("Data-Analytics/Job-Oriented-Data-Analytics")]
        public ActionResult DataAnalytics()
        {
            return View();
        }
        [Route("Software-Testing/Selenium-Automation-Testing-With-Live-Project")]
        public ActionResult SeleniumWithLiveProject()
        {
            return View();
        }
        [Route("Devops/DevOps-With-Azure-With-Placement-Assurance")]
        public ActionResult AzureDeops()
        {
            return View();
        }
        [Route("Devops/DevOps-With-AWS-With-Placement-Assurance")]
        public ActionResult AwzDeops()
        {
            return View();
        }
        [Route("App-Development/Hybrid-App-Development-With-Real-Time-Project")]
        public ActionResult HybridAppDevelopment()
        {
            return View();
        }
        [Route("Industry/Live-Project-Training-By-Industry-Expert")]
        public ActionResult IndustryProject()
        {
            return View();
        }
        [Route("College-Project/IEEE-Based-project-For-College-Student")]
        public ActionResult IEEE_College_Projects()
        {
            return View();
        }
    }
}
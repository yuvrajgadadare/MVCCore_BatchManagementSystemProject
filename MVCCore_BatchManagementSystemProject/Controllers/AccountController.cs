using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class AccountController : Controller
    {
        IStudentService studentService;
        ITrainerService trainerService;
        IAdminService adminService;
        public AccountController(IStudentService studentService,ITrainerService trainerService,IAdminService adminService)
        {
            this.studentService=studentService;
            this.trainerService=trainerService;
            this.adminService=adminService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email_address,string password)
        {
            TblstudentDetail stud = studentService.CheckStudentLogin(email_address,password);
            if (stud != null)
            {
                HttpContext.Session.SetInt32("StudentId",stud.StudentId);
                HttpContext.Session.SetString("StudentName",stud.StudentName);
                return Redirect("/Student/StudentDashboard");
            }
            else
            {
                Tbltrainer t = trainerService.CheckTrainerLogin(email_address, password);
                if (t != null)
                {
                    HttpContext.Session.SetInt32("TrainerId", t.TrainerId);
                    HttpContext.Session.SetString("TrainerName", t.TrainerName);
                    return Redirect("/Trainer/TrainerDashboard");
                }
                else
                {
                    TbladminDetail ad = adminService.CheckLogin(email_address, password);
                    if (ad != null)
                    {
                        return Redirect("/Admin/Dashboard");

                    }
                    else
                    {
                        ViewBag.msg = "Invalid Email Address or Password";
                        return View();
                    }
                }
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("StudentId");
            HttpContext.Session.Remove("StudentName");
            HttpContext.Session.Remove("TrainerId");
            HttpContext.Session.Remove("TrainerName");
            return RedirectToAction("Login");
        }

    }
}

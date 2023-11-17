using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Implementations;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;

namespace MVCCore_BatchManagementSystemProject.Areas.Student.Controllers
{
    public class StudentDashboardController : Controller
    {
        IStudentService studentService;
        public StudentDashboardController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("StudentId") != null)
            {
                int studentId = (int)HttpContext.Session.GetInt32("StudentId");
                TblstudentDetail st=studentService.GetStudentDetails(studentId);    
                return View(st);

            }
            else
            {
                return Redirect("/Account/Login");
            }
        }
    }
}

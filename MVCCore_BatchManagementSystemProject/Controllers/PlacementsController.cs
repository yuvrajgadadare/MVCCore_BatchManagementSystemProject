using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using System.Diagnostics;

namespace MVCCore_BatchManagementSystemProject.Controllers
{
    public class PlacementsController : Controller
  {
    //    CIIT_CRMDBEntities db;
    //    public PlacementsController()
    //    {
    //        db = new CIIT_CRMDBEntities();
    //    }
        // GET: Placements
        [Route("our-top-placements")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Demo()
        {
            return View();
        }
        //[Route("placement-form")]
        //public ActionResult PlacementForm()
        //{
        //    ViewBag.projectstatus = GetProjectStatus();
        //    ViewBag.coursestatus = GetCourseStatus();
        //    ViewBag.experiencestatus = GetExperienceStatus();
        //    tblstudent_for_Placements p = new tblstudent_for_Placements();
        //    return View(p);
        //}
        //[Route("placement-form")]
        //[ValidateAntiForgeryToken]

        //[HttpPost]
        //public ActionResult PlacementForm(tblstudent_for_Placements sp,HttpPostedFileBase photo,HttpPostedFileBase resume)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.projectstatus = GetProjectStatus();
        //        ViewBag.coursestatus = GetCourseStatus();
        //        ViewBag.experiencestatus = GetExperienceStatus();
        //        return View();
        //    }
        //    else
        //    {
        //        if (photo == null)
        //        {
        //            ViewBag.projectstatus = GetProjectStatus();
        //            ViewBag.coursestatus = GetCourseStatus();
        //            ViewBag.experiencestatus = GetExperienceStatus();
        //            ViewBag.photomsg = "Please upload your photo";
        //            return View();
        //        }
        //        if (resume == null)
        //        {
        //            ViewBag.projectstatus = GetProjectStatus();
        //            ViewBag.coursestatus = GetCourseStatus();
        //            ViewBag.experiencestatus = GetExperienceStatus();
        //            ViewBag.resumemsg = "Please upload your upload resume";
        //            return View();
        //        }
        //        ViewBag.projectstatus = GetProjectStatus();
        //        ViewBag.coursestatus = GetCourseStatus();
        //        ViewBag.experiencestatus = GetExperienceStatus();
        //        string photoname = sp.student_name + Path.GetExtension(photo.FileName);
        //        string photopath = Server.MapPath("~/Placements/profile photos/" + photoname);
        //        photo.SaveAs(photopath);
        //        string resumename = sp.student_name + Path.GetExtension(resume.FileName);
        //        string resumepath = Server.MapPath("~/Placements/resumes/" + resumename);
        //        resume.SaveAs(resumepath);
        //        sp.profile_photo = photoname;
        //        sp.resume = resumename;
        //        db.tblstudent_for_Placements.Add(sp);
        //        db.SaveChanges();
        //        ModelState.Clear();
        //        ViewBag.msg = "Your placement details added successfully, Our Placement team will get in touch with you soon.";
        //        tblstudent_for_Placements p = new tblstudent_for_Placements();
        //        return View(p);
        //    }
        //}


        //public List<SelectListItem> GetProjectStatus()
        //{
        //    List<SelectListItem> lst = new List<SelectListItem>();
        //    lst.Add(new SelectListItem() { Text="Yes",Value="1" });     
        //    lst.Add(new SelectListItem() { Text="No",Value="0" });
        //    return lst;
        //}
        //public List<SelectListItem> GetCourseStatus()
        //{
        //    List<SelectListItem> lst = new List<SelectListItem>();
        //    lst.Add(new SelectListItem() { Text = "Yes", Value = "1" });
        //    lst.Add(new SelectListItem() { Text = "No", Value = "0" });
        //    return lst;
        //}
        //public List<SelectListItem> GetExperienceStatus()
        //{
        //    List<SelectListItem> lst = new List<SelectListItem>();
        //    lst.Add(new SelectListItem() { Text = "Fresher", Value = "Fresher" });
        //    lst.Add(new SelectListItem() { Text = "6 Months Internship", Value = "6 Months Internship" });
        //    lst.Add(new SelectListItem() { Text = "1 Year Experience", Value = "1 Year Experience" });
        //    lst.Add(new SelectListItem() { Text = "1-2 Years Experience", Value = "1-2 Years Experience" });
        //    lst.Add(new SelectListItem() { Text = "2+ Years Experience", Value = "0" });
        //    return lst;
        //}
    }
}
using Microsoft.AspNetCore.Mvc;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
namespace MVCCore_BatchManagementSystemProject.Areas.Admin.Controllers
{
    public class HolidayController : Controller
    {
        IRepository<Tblholiday> holidayrepo;
        public HolidayController(IRepository<Tblholiday> holidayrepo)
        {
            this.holidayrepo= holidayrepo;
        }
        public IActionResult Index()
        {
            List<Tblholiday> lst = holidayrepo.GetAll();
            ViewBag.holidays = lst;
            Tblholiday h = new Tblholiday();
            return View(h);
        }
        [HttpPost]
        public IActionResult Index(Tblholiday hd)
        {

            Tblholiday h = holidayrepo.GetAll().FirstOrDefault(e => e.HolidayDate.Equals(hd.HolidayDate) && e.HolidayPurpose.Equals(hd.HolidayPurpose));
            if (h == null)
            {
                holidayrepo.Create(hd);
            }
            holidayrepo.Create(hd);
            ViewBag.msg = "Holiday Added Successfully";
            List<Tblholiday> lst = holidayrepo.GetAll();
            ViewBag.holidays = lst;
            Tblholiday hl = new Tblholiday();
            return View(hl);
        }
        [HttpPost]
        public string AddCommonHolidays(int year)
        {
            DateTime dt = Convert.ToDateTime("01/01/" + year);
            DateTime dt14apr = Convert.ToDateTime("14/04/" + year);
            DateTime dt26jan = Convert.ToDateTime("26/01/" + year);
            DateTime dt15aug = Convert.ToDateTime("15/08/" + year);
            DateTime dt2oct = Convert.ToDateTime("02/10/" + year);
            DateTime dt25dec = Convert.ToDateTime("25/12/" + year);
            Dictionary<DateTime,string> dates = new Dictionary<DateTime,string>();
            dates.Add(dt26jan, "Republic Day");
            dates.Add(dt14apr, "Dr. Ambedkar Jayanti");
            dates.Add(dt15aug, "Independence Day");
            dates.Add(dt2oct, "Mahatma Gandhi Jayanti");
            dates.Add(dt25dec, "Christmas");
            int cnt = 0;
            foreach(var t in dates)
            {
                Tblholiday h =holidayrepo.GetAll().FirstOrDefault(e=>e.HolidayDate.Equals(t.Key) && e.HolidayPurpose.Equals(t.Value));
                if (h == null)
                {
                    Tblholiday hd = new Tblholiday() { HolidayDate=t.Key, HolidayPurpose=t.Value };
                    holidayrepo.Create(hd);
                    cnt++;
                }
                
            }
            if (cnt != 0)
            {
                return "Successfully added public holidays";

            }
            else
            {
                return "Common Holidays are already added";

            }
        }
        }
}

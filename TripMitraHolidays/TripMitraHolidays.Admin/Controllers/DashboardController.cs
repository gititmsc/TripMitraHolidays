using System.Web.Mvc;

namespace TripMitraHolidays.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Dashboard";
            return View();
        }
    }
}

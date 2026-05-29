using System.Threading.Tasks;
using System.Web.Mvc;
using TripMitraHolidays.BAL.Inquiries;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.Inquiries;

namespace TripMitraHolidays.Admin.Controllers
{
    [Authorize]
    public class EnquiriesController : Controller
    {
        private readonly IInquiryService _service;

        public EnquiriesController()
        {
            _service = new InquiryService(new InquiryRepository());
        }

        // GET: /Enquiries
        public async Task<ActionResult> Index(
            string search = "",
            string sort = "createddate", string dir = "desc",
            int page = 1, int pageSize = 20)
        {
            ViewBag.PageTitle = "Enquiries";

            if (page < 1) page = 1;
            var allowedSizes = new[] { 10, 20, 50, 100 };
            if (System.Array.IndexOf(allowedSizes, pageSize) < 0) pageSize = 20;

            bool descending = !string.Equals(dir, "asc", System.StringComparison.OrdinalIgnoreCase);

            var result = await _service.GetPagedAsync(search, sort, descending, page, pageSize);

            var vm = new InquiryListViewModel
            {
                Inquiries  = result.Item1,
                TotalCount = result.Item2,
                Page       = page,
                PageSize   = pageSize,
                Search     = search,
                SortColumn = sort,
                SortDir    = dir
            };

            return View(vm);
        }

        // GET: /Enquiries/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ViewBag.PageTitle = "Enquiry Details";

            var inquiry = await _service.GetByIdAsync(id);
            if (inquiry == null) return HttpNotFound();

            return View(inquiry);
        }

        // POST: /Enquiries/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Enquiry deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}

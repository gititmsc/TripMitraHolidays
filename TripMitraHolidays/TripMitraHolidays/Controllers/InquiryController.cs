using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TripMitraHolidays.BAL.Inquiries;
using TripMitraHolidays.Core.Models;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.Inquiries;

namespace TripMitraHolidays.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IInquiryService _service;

        public InquiryController()
        {
            _service = new InquiryService(new InquiryRepository());
        }

        // GET: /enquire  or  /contact
        [HttpGet]
        public ActionResult Index(string destination = "", string packageName = "")
        {
            ViewBag.MetaTitle       = "Enquire Now | TripMitra Holidays";
            ViewBag.MetaDescription = "Get a free customised travel quote from TripMitra Holidays. Our experts will contact you within 24 hours.";

            var vm = new InquiryFormViewModel
            {
                PreferredDestination = destination,
                PackageName          = packageName
            };
            return View(vm);
        }

        // POST: /enquire
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(InquiryFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MetaTitle = "Enquire Now | TripMitra Holidays";
                return View(vm);
            }

            var inquiry = new Inquiry
            {
                FullName             = vm.FullName.Trim(),
                MobileNumber         = vm.MobileNumber.Trim(),
                EmailAddress         = vm.EmailAddress.Trim().ToLowerInvariant(),
                TravelDate           = vm.TravelDate,
                NumberOfPersons      = vm.NumberOfPersons,
                PreferredDestination = vm.PreferredDestination?.Trim(),
                City                 = vm.City?.Trim(),
                Budget               = vm.Budget,
                Message              = vm.Message?.Trim(),
                CreatedDate          = DateTime.UtcNow
            };

            await _service.SubmitAsync(inquiry);

            TempData["SuccessMessage"] = "Thank you for your inquiry, " + inquiry.FullName.Split(' ')[0] +
                "! Our travel expert will contact you shortly.";

            return RedirectToAction("Index");
        }
    }
}

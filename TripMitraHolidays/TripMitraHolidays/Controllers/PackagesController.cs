using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TripMitraHolidays.BAL.Packages;
using TripMitraHolidays.Core.Models;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.Packages;

namespace TripMitraHolidays.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IPackageService _packageService;

        public PackagesController()
        {
            _packageService = new PackageService(new PackageRepository());
        }

        // GET: /packages  or  /packages/{category}  or  /packages?q=...&page=2
        public async Task<ActionResult> Index(
            string category = "", string q = "", int page = 1, int pageSize = 9)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 9;

            var result = await _packageService.GetPublicPackagesAsync(q, category, "", page, pageSize);

            var vm = new PublicPackagesViewModel
            {
                Packages   = result.Item1.Select(ToCard).ToList(),
                TotalCount = result.Item2,
                Page       = page,
                PageSize   = pageSize,
                Category   = category,
                Search     = q
            };

            string categoryTitle = string.IsNullOrEmpty(category)
                ? "Holiday Packages"
                : new CultureInfo("en-IN").TextInfo.ToTitleCase(category.ToLower()) + " Holiday Packages";

            vm.MetaTitle       = categoryTitle + " | Trip Mitra Holidays";
            vm.MetaDescription = "Browse our curated " + categoryTitle.ToLower() +
                                 ". Best prices, expert itineraries, hassle-free travel planning.";

            ViewBag.MetaTitle       = vm.MetaTitle;
            ViewBag.MetaDescription = vm.MetaDescription;

            return View(vm);
        }

        // GET: /holiday/{slug}
        public async Task<ActionResult> Detail(string slug)
        {
            if (string.IsNullOrEmpty(slug)) return HttpNotFound();

            var package = await _packageService.GetBySlugAsync(slug);
            if (package == null) return HttpNotFound();

            var related = await _packageService.GetFeaturedForHomeAsync(4);

            var vm = new PackageDetailViewModel
            {
                Package = package,
                RelatedPackages = related
                    .Where(p => p.PackageId != package.PackageId)
                    .Take(3)
                    .Select(ToCard)
                    .ToList()
            };

            ViewBag.MetaTitle       = vm.MetaTitle;
            ViewBag.MetaDescription = vm.MetaDescription;
            ViewBag.OgImage         = vm.OgImage;
            ViewBag.CanonicalUrl    = Url.RouteUrl("PackageDetail",
                new { slug = package.Slug },
                Request.Url?.Scheme ?? "https");

            return View(vm);
        }

        private static PackageCardViewModel ToCard(Package p) => new PackageCardViewModel
        {
            PackageId        = p.PackageId,
            PackageName      = p.PackageName,
            Slug             = p.Slug,
            ThumbnailImage   = p.ThumbnailImage,
            Price            = p.PackagePrice,
            DiscountPrice    = p.DiscountPrice,
            DurationDays     = p.DurationDays,
            DurationNights   = p.DurationNights,
            Destination      = p.Destination,
            Country          = p.Country,
            TourCategory     = p.TourCategory,
            PackageType      = p.PackageType,
            IsFeatured       = p.IsFeatured,
            IsPopular        = p.IsPopular,
            ShortDescription = p.ShortDescription
        };
    }
}

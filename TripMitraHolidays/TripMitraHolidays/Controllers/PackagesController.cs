using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using TripMitraHolidays.BAL.Packages;
using TripMitraHolidays.Core.Helpers;
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

            string categoryLabel = string.IsNullOrEmpty(category)
                ? null
                : new CultureInfo("en-IN").TextInfo.ToTitleCase(category.ToLower());

            // One clear search intent per page: "{Category} Packages from India" rather than a generic title.
            string categoryTitle = categoryLabel == null
                ? "Holiday Packages from India"
                : categoryLabel + " Packages from India";

            vm.MetaTitle       = categoryTitle + " | Trip Mitra Holidays";
            vm.MetaDescription = categoryLabel == null
                ? "Browse handpicked holiday packages from India — domestic and international destinations, best prices, expert itineraries, hassle-free planning."
                : $"Browse {categoryLabel.ToLower()} tour packages from India with Trip Mitra Holidays — curated itineraries, best prices and expert planning for your {categoryLabel.ToLower()} trip.";

            vm.IntroHeading = categoryLabel == null
                ? "Holiday Packages from India"
                : categoryLabel + " Packages from India";

            // Answer-first opener: a direct, real price range before the descriptive copy —
            // computed from the full matching set (not just this page), so it's accurate.
            string priceRangeSentence = "";
            var allMatching = await _packageService.GetPublicPackagesAsync(q, category, "", 1, 100000);
            var prices = allMatching.Item1
                .Select(p => p.DiscountPrice ?? p.PackagePrice)
                .Where(p => p > 0)
                .ToList();
            if (prices.Any())
            {
                string label = categoryLabel == null ? "Holiday packages" : categoryLabel + " packages";
                priceRangeSentence = prices.Min() == prices.Max()
                    ? $"{label} from India start at ₹{prices.Min():N0} per person. "
                    : $"{label} from India typically range from ₹{prices.Min():N0} to ₹{prices.Max():N0} per person. ";
            }

            vm.IntroText = priceRangeSentence + (categoryLabel == null
                ? "Explore our full range of handpicked domestic and international holiday packages, curated by travel experts and priced for Indian travellers. Every package includes a day-by-day itinerary, clear inclusions and exclusions, and support from enquiry to return."
                : $"Trip Mitra Holidays curates {categoryLabel.ToLower()} itineraries with transparent pricing, handpicked stays and dedicated support — from your first enquiry through to your trip home.");

            ViewBag.MetaTitle       = vm.MetaTitle;
            ViewBag.MetaDescription = vm.MetaDescription;
            ViewBag.CanonicalUrl    = Url.RouteUrl("PackagesByCategory",
                new { category = string.IsNullOrEmpty(category) ? (object)UrlParameter.Optional : category },
                Request.Url?.Scheme ?? "https");

            return View(vm);
        }

        // GET: /packages/{destinationSlug}-tour-packages-from-{citySlug}
        // Only ever resolves for a (Destination, StartingCity) pair that has real packages behind it —
        // built from GetAllAsync() + slug matching rather than a synthetic city×destination matrix,
        // so this never serves a thin/empty page.
        public async Task<ActionResult> ByCityDestination(string destinationSlug, string citySlug)
        {
            if (string.IsNullOrEmpty(destinationSlug) || string.IsNullOrEmpty(citySlug))
                return HttpNotFound();

            var all = await _packageService.GetAllAsync();
            var matches = all
                .Where(p => p.IsActive
                         && SlugHelper.Slugify(p.Destination) == destinationSlug
                         && SlugHelper.Slugify(p.StartingCity) == citySlug)
                .OrderBy(p => p.DisplayOrder)
                .ToList();

            if (!matches.Any()) return HttpNotFound();

            var first = matches[0];
            var vm = new DestinationCityViewModel
            {
                Destination  = first.Destination,
                Country      = first.Country,
                StartingCity = first.StartingCity,
                Packages     = matches.Select(ToCard).ToList()
            };

            ViewBag.MetaTitle       = vm.MetaTitle;
            ViewBag.MetaDescription = vm.MetaDescription;
            ViewBag.CanonicalUrl    = Url.RouteUrl("DestinationCityLanding",
                new { destinationSlug, citySlug }, Request.Url?.Scheme ?? "https");

            return View("ByCityDestination", vm);
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

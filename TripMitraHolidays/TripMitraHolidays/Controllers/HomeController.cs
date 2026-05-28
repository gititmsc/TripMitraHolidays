using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TripMitraHolidays.BAL.Packages;
using TripMitraHolidays.Core.Models;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.Packages;

namespace TripMitraHolidays.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPackageService _packageService;

        public HomeController()
        {
            _packageService = new PackageService(new PackageRepository());
        }

        // GET: /
        public async Task<ActionResult> Index()
        {
            var packages = await _packageService.GetFeaturedForHomeAsync(12);

            var vm = new HomeViewModel
            {
                MetaTitle       = "TripMitra Holidays – Explore More, Worry Less",
                MetaDescription = "Discover handpicked holiday packages for India and international destinations. Best prices, expert planning, 10,000+ happy travellers.",
                OgImage         = packages.FirstOrDefault()?.ThumbnailImage
            };

            vm.FeaturedPackages   = packages.Select(ToCard).ToList();
            vm.SliderPackages     = packages.Take(6).Select(ToCard).ToList();
            vm.PopularDestinations = BuildDestinationTiles(packages);

            SetSeoViewBag(vm.MetaTitle, vm.MetaDescription, vm.OgImage);
            return View(vm);
        }

        // GET: /about
        public ActionResult About()
        {
            ViewBag.MetaTitle       = "About Us | TripMitra Holidays";
            ViewBag.MetaDescription = "Learn about TripMitra Holidays – your trusted travel partner for crafting unforgettable journeys since 2018.";
            return View();
        }

        private void SetSeoViewBag(string title, string description, string ogImage = null)
        {
            ViewBag.MetaTitle       = title;
            ViewBag.MetaDescription = description;
            ViewBag.OgImage         = ogImage;
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

        private static List<DestinationTileViewModel> BuildDestinationTiles(List<Package> packages)
        {
            return packages
                .Where(p => !string.IsNullOrEmpty(p.Destination))
                .GroupBy(p => p.Destination)
                .Select(g => new DestinationTileViewModel
                {
                    Name         = g.Key,
                    ImageUrl     = g.OrderBy(p => p.DisplayOrder)
                                    .FirstOrDefault()?.ThumbnailImage,
                    PackageCount = g.Count(),
                    Slug         = g.Key.ToLowerInvariant()
                                    .Replace(" ", "-")
                                    .Replace(",", "")
                })
                .Take(5)
                .ToList();
        }
    }
}

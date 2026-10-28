using System;
using System.Collections.Generic;
using System.Configuration;
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
                MetaTitle       = "Holiday Packages from India | Goa, Andaman, Nepal, Bhutan - Trip Mitra Holidays",
                MetaDescription = "Discover handpicked holiday packages for India and international destinations. Best prices, expert planning, 10,000+ happy travellers.",
                OgImage         = packages.FirstOrDefault()?.ThumbnailImage
            };

            vm.FeaturedPackages   = packages.Select(ToCard).ToList();
            vm.SliderPackages     = packages.Take(6).Select(ToCard).ToList();
            vm.PopularDestinations = BuildDestinationTiles(packages);

            SetSeoViewBag(vm.MetaTitle, vm.MetaDescription, vm.OgImage);
            await SetSiteStatsViewBagAsync();
            ViewBag.CanonicalUrl = Url.RouteUrl("Default", new { controller = "Home", action = "Index" }, Request.Url?.Scheme ?? "https");
            return View(vm);
        }

        // GET: /about
        public async Task<ActionResult> About()
        {
            ViewBag.MetaTitle       = "About Us | Trip Mitra Holidays";
            ViewBag.MetaDescription = "Learn about Trip Mitra Holidays – your trusted travel partner for crafting unforgettable journeys since 2018.";
            ViewBag.CanonicalUrl    = Url.RouteUrl("About", null, Request.Url?.Scheme ?? "https");
            await SetSiteStatsViewBagAsync();
            return View();
        }

        // GET: /privacy-policy
        public ActionResult PrivacyPolicy()
        {
            ViewBag.MetaTitle       = "Privacy Policy | Trip Mitra Holidays";
            ViewBag.MetaDescription = "How Trip Mitra Holidays collects, uses and protects your personal information.";
            ViewBag.CanonicalUrl    = Url.RouteUrl("PrivacyPolicy", null, Request.Url?.Scheme ?? "https");
            return View();
        }

        // GET: /terms-conditions
        public ActionResult Terms()
        {
            ViewBag.MetaTitle       = "Terms & Conditions | Trip Mitra Holidays";
            ViewBag.MetaDescription = "The terms and conditions governing bookings made with Trip Mitra Holidays.";
            ViewBag.CanonicalUrl    = Url.RouteUrl("TermsConditions", null, Request.Url?.Scheme ?? "https");
            return View();
        }

        // GET: /cancellation-policy
        public ActionResult CancellationPolicy()
        {
            ViewBag.MetaTitle       = "Cancellation & Refund Policy | Trip Mitra Holidays";
            ViewBag.MetaDescription = "Cancellation timelines, refund rules and rescheduling policy for Trip Mitra Holidays bookings.";
            ViewBag.CanonicalUrl    = Url.RouteUrl("CancellationPolicy", null, Request.Url?.Scheme ?? "https");
            return View();
        }

        private void SetSeoViewBag(string title, string description, string ogImage = null)
        {
            ViewBag.MetaTitle       = title;
            ViewBag.MetaDescription = description;
            ViewBag.OgImage         = ogImage;
        }

        // Real, computed trust numbers instead of hardcoded marketing copy —
        // avoids claims (e.g. "500+ destinations") that contradict the actual catalog.
        private async Task SetSiteStatsViewBagAsync()
        {
            int foundedYear;
            if (!int.TryParse(ConfigurationManager.AppSettings["CompanyFoundedYear"], out foundedYear))
            {
                foundedYear = 2018;
            }
            ViewBag.YearsExperience = Math.Max(1, DateTime.Now.Year - foundedYear);

            var allActive = await _packageService.GetPublicPackagesAsync(string.Empty, string.Empty, string.Empty, 1, 100000);
            ViewBag.DestinationsCount = allActive.Item1
                .Where(p => !string.IsNullOrEmpty(p.Destination))
                .Select(p => p.Destination)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count();
            ViewBag.PackagesCount = allActive.Item2;

            ViewBag.HappyTravellers = ConfigurationManager.AppSettings["HappyTravellersCount"] ?? "10,000+";
            ViewBag.AverageRating   = ConfigurationManager.AppSettings["AverageRating"] ?? "4.9 / 5";
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

using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TripMitraHolidays.BAL.Packages;
using TripMitraHolidays.Core.Helpers;
using TripMitraHolidays.Repositories.Packages;

namespace TripMitraHolidays.Controllers
{
    public class SeoController : Controller
    {
        private readonly IPackageService _packageService;

        public SeoController()
        {
            _packageService = new PackageService(new PackageRepository());
        }

        // GET: /sitemap.xml
        public async Task<ActionResult> Sitemap()
        {
            string baseUrl = (Request.Url?.Scheme ?? "https") + "://" + Request.Url?.Authority;

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

            void AddUrl(string path, string changeFreq, string priority, DateTime? lastMod = null)
            {
                sb.AppendLine("  <url>");
                sb.AppendLine($"    <loc>{baseUrl}{path}</loc>");
                if (lastMod.HasValue)
                {
                    sb.AppendLine($"    <lastmod>{lastMod.Value:yyyy-MM-dd}</lastmod>");
                }
                sb.AppendLine($"    <changefreq>{changeFreq}</changefreq>");
                sb.AppendLine($"    <priority>{priority}</priority>");
                sb.AppendLine("  </url>");
            }

            AddUrl("/", "daily", "1.0");
            AddUrl("/packages", "daily", "0.9");
            AddUrl("/about", "monthly", "0.5");
            AddUrl("/contact", "monthly", "0.5");

            var result = await _packageService.GetPublicPackagesAsync(string.Empty, string.Empty, string.Empty, 1, 10000);
            var packages = result.Item1;

            foreach (var category in packages
                .Where(p => !string.IsNullOrEmpty(p.TourCategory))
                .Select(p => p.TourCategory)
                .Distinct(StringComparer.OrdinalIgnoreCase))
            {
                AddUrl("/packages/" + category.ToLowerInvariant().Replace(" ", "-"), "daily", "0.8");
            }

            foreach (var package in packages)
            {
                AddUrl("/holiday/" + package.Slug, "weekly", "0.8", package.UpdatedDate ?? package.CreatedDate);
            }

            // Destination + departure-city landing pages — one per (Destination, StartingCity)
            // pair that actually has packages behind it (built in PackagesController.ByCityDestination).
            foreach (var combo in packages
                .Where(p => !string.IsNullOrEmpty(p.Destination) && !string.IsNullOrEmpty(p.StartingCity))
                .GroupBy(p => new { Destination = SlugHelper.Slugify(p.Destination), City = SlugHelper.Slugify(p.StartingCity) }))
            {
                AddUrl($"/packages/{combo.Key.Destination}-tour-packages-from-{combo.Key.City}", "weekly", "0.7");
            }

            sb.AppendLine("</urlset>");

            return Content(sb.ToString(), "application/xml", Encoding.UTF8);
        }

        // GET: /llms.txt — an experimental, informal convention (not a confirmed standard
        // across AI systems) for summarizing a site for LLM crawlers/answer engines.
        // Built dynamically from real catalog data so it can't go stale like a hand-written file.
        public async Task<ActionResult> LlmsTxt()
        {
            string baseUrl = (Request.Url?.Scheme ?? "https") + "://" + Request.Url?.Authority;
            string phone   = ConfigurationManager.AppSettings["CompanyPhoneDisplay"];
            string email   = ConfigurationManager.AppSettings["CompanyEmail"];

            var result = await _packageService.GetPublicPackagesAsync(string.Empty, string.Empty, string.Empty, 1, 10000);
            var packages = result.Item1;

            var categories = packages
                .Where(p => !string.IsNullOrEmpty(p.TourCategory))
                .Select(p => p.TourCategory)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c)
                .ToList();

            var destinations = packages
                .Where(p => !string.IsNullOrEmpty(p.Destination))
                .Select(p => p.Destination)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(d => d)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("# Trip Mitra Holidays");
            sb.AppendLine();
            sb.AppendLine("> Trip Mitra Holidays is an India-based travel agency offering handpicked domestic " +
                          "and international holiday packages, with day-by-day itineraries, transparent " +
                          $"pricing and dedicated support. Catalog currently covers {packages.Count} active " +
                          $"packages across {destinations.Count} destinations.");
            sb.AppendLine();
            sb.AppendLine("## Key pages");
            sb.AppendLine($"- [All holiday packages]({baseUrl}/packages)");
            sb.AppendLine($"- [About us]({baseUrl}/about)");
            sb.AppendLine($"- [Enquire / get a quote]({baseUrl}/enquire)");
            sb.AppendLine($"- [Cancellation & refund policy]({baseUrl}/cancellation-policy)");
            sb.AppendLine();

            if (categories.Any())
            {
                sb.AppendLine("## Package categories");
                foreach (var category in categories)
                {
                    string slug = category.ToLowerInvariant().Replace(" ", "-");
                    sb.AppendLine($"- [{category} tour packages]({baseUrl}/packages/{slug})");
                }
                sb.AppendLine();
            }

            if (destinations.Any())
            {
                sb.AppendLine("## Destinations covered");
                sb.AppendLine(string.Join(", ", destinations));
                sb.AppendLine();
            }

            sb.AppendLine("## Contact");
            if (!string.IsNullOrEmpty(phone)) sb.AppendLine($"- Phone: {phone}");
            if (!string.IsNullOrEmpty(email)) sb.AppendLine($"- Email: {email}");
            sb.AppendLine($"- Sitemap: {baseUrl}/sitemap.xml");

            return Content(sb.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}

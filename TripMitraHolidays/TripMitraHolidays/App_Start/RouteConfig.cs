using System.Web.Mvc;
using System.Web.Routing;

namespace TripMitraHolidays
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // SEO: dynamic XML sitemap
            routes.MapRoute(
                name: "Sitemap",
                url: "sitemap.xml",
                defaults: new { controller = "Seo", action = "Sitemap" }
            );

            // AI-search: dynamic llms.txt summary
            routes.MapRoute(
                name: "LlmsTxt",
                url: "llms.txt",
                defaults: new { controller = "Seo", action = "LlmsTxt" }
            );

            // SEO-friendly package detail: /holiday/bali-indonesia-6n7d
            routes.MapRoute(
                name: "PackageDetail",
                url: "holiday/{slug}",
                defaults: new { controller = "Packages", action = "Detail" },
                constraints: new { slug = @"[a-z0-9\-]+" }
            );

            // SEO: destination + departure-city landing page, e.g. /packages/goa-tour-packages-from-mumbai
            // Must be registered before PackagesByCategory — otherwise that single-segment route swallows this URL too.
            routes.MapRoute(
                name: "DestinationCityLanding",
                url: "packages/{destinationSlug}-tour-packages-from-{citySlug}",
                defaults: new { controller = "Packages", action = "ByCityDestination" }
            );

            // Packages listing with optional category filter: /packages or /packages/international
            routes.MapRoute(
                name: "PackagesByCategory",
                url: "packages/{category}",
                defaults: new { controller = "Packages", action = "Index", category = UrlParameter.Optional }
            );

            // Enquiry / Contact form
            routes.MapRoute(
                name: "Enquire",
                url: "enquire",
                defaults: new { controller = "Inquiry", action = "Index" }
            );
            routes.MapRoute(
                name: "Contact",
                url: "contact",
                defaults: new { controller = "Inquiry", action = "Index" }
            );

            // About page
            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Home", action = "About" }
            );

            // Legal pages
            routes.MapRoute(
                name: "PrivacyPolicy",
                url: "privacy-policy",
                defaults: new { controller = "Home", action = "PrivacyPolicy" }
            );
            routes.MapRoute(
                name: "TermsConditions",
                url: "terms-conditions",
                defaults: new { controller = "Home", action = "Terms" }
            );
            routes.MapRoute(
                name: "CancellationPolicy",
                url: "cancellation-policy",
                defaults: new { controller = "Home", action = "CancellationPolicy" }
            );

            // Default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

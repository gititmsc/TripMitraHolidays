using System.Web.Mvc;
using System.Web.Routing;

namespace TripMitraHolidays
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // SEO-friendly package detail: /holiday/bali-indonesia-6n7d
            routes.MapRoute(
                name: "PackageDetail",
                url: "holiday/{slug}",
                defaults: new { controller = "Packages", action = "Detail" },
                constraints: new { slug = @"[a-z0-9\-]+" }
            );

            // Packages listing with optional category filter: /packages or /packages/international
            routes.MapRoute(
                name: "PackagesByCategory",
                url: "packages/{category}",
                defaults: new { controller = "Packages", action = "Index", category = UrlParameter.Optional }
            );

            // About page
            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { controller = "Home", action = "About" }
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TripMitraHolidays
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // Canonical-domain + HTTPS redirect: one URL form wins SEO authority
        // (http vs https, www vs non-www are otherwise treated as duplicate sites by Google).
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string canonicalDomain = ConfigurationManager.AppSettings["CanonicalDomain"];
            if (string.IsNullOrWhiteSpace(canonicalDomain)) return;

            var url = Request.Url;
            if (url.IsLoopback || url.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                return;

            bool hostMismatch = !url.Host.Equals(canonicalDomain, StringComparison.OrdinalIgnoreCase);
            bool needsHttps    = !Request.IsSecureConnection
                                  && !string.Equals(Request.Headers["X-Forwarded-Proto"], "https", StringComparison.OrdinalIgnoreCase);

            if (hostMismatch || needsHttps)
            {
                var builder = new UriBuilder(url) { Scheme = "https", Port = -1, Host = canonicalDomain };
                Response.RedirectPermanent(builder.Uri.ToString(), true);
            }
        }
    }
}

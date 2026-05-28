using System.Web.Optimization;

namespace TripMitraHolidays
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            // TripMitra site JS (slider, mobile nav, filter tabs, smooth scroll)
            bundles.Add(new ScriptBundle("~/bundles/tripmitra").Include(
                "~/Scripts/tripmitra.js"));

            // TripMitra main stylesheet (custom design — no Bootstrap needed)
            bundles.Add(new StyleBundle("~/Content/tripmitra").Include(
                "~/Content/tripmitra.css"));
        }
    }
}

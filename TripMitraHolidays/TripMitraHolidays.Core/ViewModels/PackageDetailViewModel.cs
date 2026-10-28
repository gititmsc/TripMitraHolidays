using System.Collections.Generic;
using System.Linq;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Core.ViewModels
{
    public class PackageDetailViewModel
    {
        private static readonly string[] AudienceTypes = { "Honeymoon", "Adventure", "Pilgrimage" };

        public Package Package { get; set; }
        public List<PackageCardViewModel> RelatedPackages { get; set; } = new List<PackageCardViewModel>();

        // "Honeymoon"/"Adventure"/"Pilgrimage" read naturally in a title ("...for couples"-style intent);
        // "Domestic"/"International" don't, so fall back to the more descriptive TourCategory (Beach, Mountains, ...).
        public string SeoTypeLabel =>
            !string.IsNullOrEmpty(Package?.PackageType) && AudienceTypes.Contains(Package.PackageType)
                ? Package.PackageType
                : (Package?.TourCategory ?? Package?.PackageType);

        // One main keyword intent per page: "{Destination} {Type} Tour Package from {City} (NxD)"
        public string SeoH1
        {
            get
            {
                if (Package == null) return string.Empty;
                var parts = new List<string>();
                if (!string.IsNullOrEmpty(Package.Destination)) parts.Add(Package.Destination);
                if (!string.IsNullOrEmpty(SeoTypeLabel)) parts.Add(SeoTypeLabel);
                parts.Add("Tour Package");
                if (!string.IsNullOrEmpty(Package.StartingCity)) parts.Add("from " + Package.StartingCity);
                string h1 = string.Join(" ", parts);
                return $"{h1} ({Package.DurationNights}N/{Package.DurationDays}D)";
            }
        }

        public string MetaTitle =>
            !string.IsNullOrEmpty(Package?.MetaTitle)
                ? Package.MetaTitle
                : SeoH1 + " | Trip Mitra Holidays";

        public string MetaDescription =>
            !string.IsNullOrEmpty(Package?.MetaDescription)
                ? Package.MetaDescription
                : Package?.ShortDescription;

        public string OgImage => Package?.BannerImage ?? Package?.ThumbnailImage;
    }
}

using System.Collections.Generic;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Core.ViewModels
{
    public class PackageDetailViewModel
    {
        public Package Package { get; set; }
        public List<PackageCardViewModel> RelatedPackages { get; set; } = new List<PackageCardViewModel>();

        public string MetaTitle =>
            !string.IsNullOrEmpty(Package?.MetaTitle)
                ? Package.MetaTitle
                : (Package?.PackageName ?? "Package") + " | Trip Mitra Holidays";

        public string MetaDescription =>
            !string.IsNullOrEmpty(Package?.MetaDescription)
                ? Package.MetaDescription
                : Package?.ShortDescription;

        public string OgImage => Package?.BannerImage ?? Package?.ThumbnailImage;
    }
}

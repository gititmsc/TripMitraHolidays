using System.Collections.Generic;

namespace TripMitraHolidays.Core.ViewModels
{
    public class PackageCardViewModel
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string Slug { get; set; }
        public string ThumbnailImage { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int DurationDays { get; set; }
        public int DurationNights { get; set; }
        public string Destination { get; set; }
        public string Country { get; set; }
        public string TourCategory { get; set; }
        public string PackageType { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsPopular { get; set; }
        public string ShortDescription { get; set; }

        public decimal DisplayPrice =>
            DiscountPrice.HasValue && DiscountPrice.Value > 0 ? DiscountPrice.Value : Price;

        public string BadgeText
        {
            get
            {
                if (IsFeatured) return "Bestseller";
                if (IsPopular) return "Popular";
                return null;
            }
        }

        // Maps PackageType + TourCategory to CSS data-cat space-separated values for JS filter tabs
        public string DataCategories
        {
            get
            {
                var cats = new List<string>();
                if (!string.IsNullOrEmpty(PackageType))
                {
                    string pt = PackageType.ToLowerInvariant();
                    if (pt.Contains("international")) cats.Add("international");
                    if (pt.Contains("domestic")) cats.Add("domestic");
                }
                if (!string.IsNullOrEmpty(TourCategory))
                {
                    string tc = TourCategory.ToLowerInvariant();
                    if (tc.Contains("honeymoon")) cats.Add("honeymoon");
                    if (tc.Contains("adventure")) cats.Add("adventure");
                    if (tc.Contains("family")) cats.Add("family");
                    if (tc.Contains("luxury")) cats.Add("luxury");
                }
                return string.Join(" ", cats);
            }
        }
    }
}

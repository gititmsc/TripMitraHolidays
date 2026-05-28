using System.Collections.Generic;

namespace TripMitraHolidays.Core.ViewModels
{
    public class HomeViewModel
    {
        public string MetaTitle { get; set; } = "TripMitra Holidays – Explore More, Worry Less";
        public string MetaDescription { get; set; } = "Discover handpicked holiday packages for India and international destinations. Best prices, expert planning, 10,000+ happy travellers.";
        public string OgImage { get; set; }

        // Top packages used for the hero slider (up to 6)
        public List<PackageCardViewModel> SliderPackages { get; set; } = new List<PackageCardViewModel>();

        // All featured/active packages shown in the home grid
        public List<PackageCardViewModel> FeaturedPackages { get; set; } = new List<PackageCardViewModel>();

        // Destination tiles built by grouping packages on Destination field
        public List<DestinationTileViewModel> PopularDestinations { get; set; } = new List<DestinationTileViewModel>();
    }
}

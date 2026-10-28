using System.Collections.Generic;

namespace TripMitraHolidays.Core.ViewModels
{
    // A programmatic "{Destination} Tour Packages from {City}" landing page —
    // only ever built for a (Destination, StartingCity) pair that has real packages behind it.
    public class DestinationCityViewModel
    {
        public string Destination { get; set; }
        public string Country { get; set; }
        public string StartingCity { get; set; }
        public List<PackageCardViewModel> Packages { get; set; } = new List<PackageCardViewModel>();

        public string MetaTitle => $"{Destination} Tour Packages from {StartingCity} | Trip Mitra Holidays";

        public string MetaDescription =>
            $"Book {Destination} tour packages from {StartingCity} with Trip Mitra Holidays — handpicked itineraries, " +
            $"transparent pricing and expert planning for travellers departing from {StartingCity}.";

        public string IntroHeading => $"{Destination} Tour Packages from {StartingCity}";

        public string IntroText =>
            $"Planning a trip to {Destination} from {StartingCity}? Trip Mitra Holidays curates {Destination} holiday " +
            $"packages designed for travellers departing from {StartingCity}, with day-by-day itineraries, clear " +
            $"inclusions and exclusions, and dedicated support from your first enquiry to your return home.";
    }
}

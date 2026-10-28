using System;
using System.Collections.Generic;

namespace TripMitraHolidays.Core.Helpers
{
    // Stop-gap only: real package photos should be uploaded via the admin panel.
    // Until then, this keeps packages from all sharing one identical stock photo
    // by picking a category-relevant image instead of a single hardcoded one.
    public static class PlaceholderImages
    {
        private static readonly Dictionary<string, string> ByCategory = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Beach",      "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=800&q=80&fit=crop" },
            { "Mountains",  "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800&q=80&fit=crop" },
            { "Historical", "https://images.unsplash.com/photo-1524492412937-b28074a5d7da?w=800&q=80&fit=crop" },
            { "Cultural",   "https://images.unsplash.com/photo-1512343879784-a960bf40e7f2?w=800&q=80&fit=crop" },
            { "Religious",  "https://images.unsplash.com/photo-1545126178-862cdb469409?w=800&q=80&fit=crop" },
            { "Wildlife",   "https://images.unsplash.com/photo-1549366021-9f761d450615?w=800&q=80&fit=crop" },
            { "Adventure",  "https://images.unsplash.com/photo-1551632811-561732d1e306?w=800&q=80&fit=crop" },
            { "Luxury",     "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=800&q=80&fit=crop" },
        };

        private const string Default = "https://images.unsplash.com/photo-1488646953014-85cb44e25828?w=800&q=80&fit=crop";

        public static string ForCategory(string tourCategory)
        {
            if (!string.IsNullOrEmpty(tourCategory) && ByCategory.TryGetValue(tourCategory.Trim(), out var url))
            {
                return url;
            }
            return Default;
        }
    }
}

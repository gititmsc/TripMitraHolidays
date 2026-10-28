namespace TripMitraHolidays.Core.Helpers
{
    public static class SlugHelper
    {
        public static string Slugify(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return value.Trim()
                .ToLowerInvariant()
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace("&", "and")
                .Replace("'", "");
        }
    }
}

using System;
using System.Collections.Generic;

namespace TripMitraHolidays.Core.ViewModels
{
    public class PublicPackagesViewModel
    {
        public List<PackageCardViewModel> Packages { get; set; } = new List<PackageCardViewModel>();
        public int TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public string Category { get; set; } = "";
        public string Search { get; set; } = "";

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
        public bool HasPrev => Page > 1;
        public bool HasNext => Page < TotalPages;
        public int StartRecord => TotalCount == 0 ? 0 : (Page - 1) * PageSize + 1;
        public int EndRecord => Math.Min(Page * PageSize, TotalCount);
    }
}

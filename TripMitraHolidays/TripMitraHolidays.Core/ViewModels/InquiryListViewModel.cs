using System;
using System.Collections.Generic;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Core.ViewModels
{
    public class InquiryListViewModel
    {
        public List<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
        public int TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string Search { get; set; } = "";
        public string SortColumn { get; set; } = "createddate";
        public string SortDir { get; set; } = "desc";

        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
        public bool HasPrev => Page > 1;
        public bool HasNext => Page < TotalPages;
        public int StartRecord => TotalCount == 0 ? 0 : (Page - 1) * PageSize + 1;
        public int EndRecord => Math.Min(Page * PageSize, TotalCount);
    }
}

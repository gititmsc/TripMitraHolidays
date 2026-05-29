using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.BAL.Inquiries
{
    public interface IInquiryService
    {
        Task<int> SubmitAsync(Inquiry inquiry);
        Task<Tuple<List<Inquiry>, int>> GetPagedAsync(string search, string sortColumn, bool descending, int page, int pageSize);
        Task<Inquiry> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}

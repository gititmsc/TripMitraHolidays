using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Repositories.Inquiries
{
    public interface IInquiryRepository
    {
        Task<int> AddAsync(Inquiry inquiry);
        Task<Tuple<List<Inquiry>, int>> GetPagedAsync(string search, string sortColumn, bool descending, int page, int pageSize);
        Task<Inquiry> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}

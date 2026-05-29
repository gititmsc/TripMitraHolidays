using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;
using TripMitraHolidays.Repositories.Inquiries;

namespace TripMitraHolidays.BAL.Inquiries
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryRepository _repo;

        public InquiryService(IInquiryRepository repo)
        {
            _repo = repo;
        }

        public Task<int> SubmitAsync(Inquiry inquiry) => _repo.AddAsync(inquiry);

        public Task<Tuple<List<Inquiry>, int>> GetPagedAsync(
            string search, string sortColumn, bool descending, int page, int pageSize)
            => _repo.GetPagedAsync(search, sortColumn, descending, page, pageSize);

        public Task<Inquiry> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}

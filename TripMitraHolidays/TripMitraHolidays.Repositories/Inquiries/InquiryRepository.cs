using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Repositories.Inquiries
{
    public class InquiryRepository : IInquiryRepository
    {
        public async Task<int> AddAsync(Inquiry inquiry)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Inquiries.Add(inquiry);
                await db.SaveChangesAsync();
                return inquiry.InquiryId;
            }
        }

        public async Task<Tuple<List<Inquiry>, int>> GetPagedAsync(
            string search, string sortColumn, bool descending, int page, int pageSize)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<Inquiry> query = db.Inquiries;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    string s = search.ToLower();
                    query = query.Where(i =>
                        i.FullName.ToLower().Contains(s) ||
                        i.MobileNumber.Contains(s) ||
                        i.EmailAddress.ToLower().Contains(s) ||
                        (i.PreferredDestination != null && i.PreferredDestination.ToLower().Contains(s)) ||
                        (i.City != null && i.City.ToLower().Contains(s)));
                }

                int total = await query.CountAsync();

                IQueryable<Inquiry> sorted;
                switch ((sortColumn ?? "createddate").ToLower())
                {
                    case "fullname":
                        sorted = descending ? query.OrderByDescending(i => i.FullName) : query.OrderBy(i => i.FullName);
                        break;
                    case "mobile":
                        sorted = descending ? query.OrderByDescending(i => i.MobileNumber) : query.OrderBy(i => i.MobileNumber);
                        break;
                    case "email":
                        sorted = descending ? query.OrderByDescending(i => i.EmailAddress) : query.OrderBy(i => i.EmailAddress);
                        break;
                    case "destination":
                        sorted = descending ? query.OrderByDescending(i => i.PreferredDestination) : query.OrderBy(i => i.PreferredDestination);
                        break;
                    case "traveldate":
                        sorted = descending ? query.OrderByDescending(i => i.TravelDate) : query.OrderBy(i => i.TravelDate);
                        break;
                    case "budget":
                        sorted = descending ? query.OrderByDescending(i => i.Budget) : query.OrderBy(i => i.Budget);
                        break;
                    default: // createddate
                        sorted = descending ? query.OrderByDescending(i => i.CreatedDate) : query.OrderBy(i => i.CreatedDate);
                        break;
                }

                var items = await sorted
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Tuple.Create(items, total);
            }
        }

        public async Task<Inquiry> GetByIdAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Inquiries.FindAsync(id);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var inquiry = await db.Inquiries.FindAsync(id);
                if (inquiry == null) return;
                db.Inquiries.Remove(inquiry);
                await db.SaveChangesAsync();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace TripMitraHolidays.Repositories.AdminUser
{
    public class AdminUserRepository : IAdminUserRepository
    {
        public Core.Models.AdminUser GetByEmail(string email)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.AdminUsers
                    .FirstOrDefault(u => u.Email == email && u.IsActive);
            }
        }

        public void UpdateLastLogin(int userId, DateTime loginTime)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.AdminUsers.Find(userId);
                if (user != null)
                {
                    user.LastLoginAt = loginTime;
                    db.SaveChanges();
                }
            }
        }

        public async Task<Tuple<List<Core.Models.AdminUser>, int>> GetPagedAsync(
            string search, string sortColumn, bool descending, int page, int pageSize)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<Core.Models.AdminUser> query = db.AdminUsers;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    string s = search.ToLower();
                    query = query.Where(u =>
                        u.FullName.ToLower().Contains(s) ||
                        u.Email.ToLower().Contains(s));
                }

                int total = await query.CountAsync();

                IQueryable<Core.Models.AdminUser> sorted;
                switch ((sortColumn ?? "createdat").ToLower())
                {
                    case "fullname":
                        sorted = descending ? query.OrderByDescending(u => u.FullName) : query.OrderBy(u => u.FullName);
                        break;
                    case "email":
                        sorted = descending ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email);
                        break;
                    case "isactive":
                        sorted = descending ? query.OrderByDescending(u => u.IsActive) : query.OrderBy(u => u.IsActive);
                        break;
                    case "lastloginat":
                        sorted = descending ? query.OrderByDescending(u => u.LastLoginAt) : query.OrderBy(u => u.LastLoginAt);
                        break;
                    default:
                        sorted = descending ? query.OrderByDescending(u => u.CreatedAt) : query.OrderBy(u => u.CreatedAt);
                        break;
                }

                var items = await sorted
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Tuple.Create(items, total);
            }
        }

        public async Task<Core.Models.AdminUser> GetByIdAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.AdminUsers.FindAsync(id);
            }
        }

        public async Task<int> CreateAsync(Core.Models.AdminUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.AdminUsers.Add(user);
                await db.SaveChangesAsync();
                return user.Id;
            }
        }

        public async Task UpdateAsync(Core.Models.AdminUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var existing = await db.AdminUsers.FindAsync(user.Id);
                if (existing == null) return;
                existing.FullName = user.FullName;
                existing.Email = user.Email;
                existing.IsActive = user.IsActive;
                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    existing.PasswordHash = user.PasswordHash;
                    existing.PasswordSalt = user.PasswordSalt;
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = await db.AdminUsers.FindAsync(id);
                if (user == null) return;
                db.AdminUsers.Remove(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> EmailExistsAsync(string email, int excludeId)
        {
            using (var db = new ApplicationDbContext())
            {
                string lower = email.ToLower();
                return await db.AdminUsers.AnyAsync(u => u.Email.ToLower() == lower && u.Id != excludeId);
            }
        }
    }
}

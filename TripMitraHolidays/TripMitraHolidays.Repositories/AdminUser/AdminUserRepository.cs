using System;
using System.Linq;

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
    }
}

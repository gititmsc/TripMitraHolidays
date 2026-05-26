using System;

namespace TripMitraHolidays.Repositories.AdminUser
{
    public interface IAdminUserRepository
    {
        Core.Models.AdminUser GetByEmail(string email);
        void UpdateLastLogin(int userId, DateTime loginTime);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripMitraHolidays.Repositories.AdminUser
{
    public interface IAdminUserRepository
    {
        Core.Models.AdminUser GetByEmail(string email);
        void UpdateLastLogin(int userId, DateTime loginTime);

        Task<Tuple<List<Core.Models.AdminUser>, int>> GetPagedAsync(string search, string sortColumn, bool descending, int page, int pageSize);
        Task<Core.Models.AdminUser> GetByIdAsync(int id);
        Task<int> CreateAsync(Core.Models.AdminUser user);
        Task UpdateAsync(Core.Models.AdminUser user);
        Task DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email, int excludeId);
    }
}

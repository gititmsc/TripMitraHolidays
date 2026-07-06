using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.BAL.Users
{
    public interface IUserService
    {
        Task<Tuple<List<AdminUser>, int>> GetPagedAsync(string search, string sortColumn, bool descending, int page, int pageSize);
        Task<AdminUser> GetByIdAsync(int id);
        Task<int> CreateAsync(Core.ViewModels.UserFormViewModel model);
        Task UpdateAsync(Core.ViewModels.UserFormViewModel model);
        Task DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email, int excludeId);
    }
}

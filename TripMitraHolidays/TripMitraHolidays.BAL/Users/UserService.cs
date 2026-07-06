using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Helpers;
using TripMitraHolidays.Core.Models;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.AdminUser;

namespace TripMitraHolidays.BAL.Users
{
    public class UserService : IUserService
    {
        private readonly IAdminUserRepository _repo;

        public UserService(IAdminUserRepository repo)
        {
            _repo = repo;
        }

        public Task<Tuple<List<AdminUser>, int>> GetPagedAsync(
            string search, string sortColumn, bool descending, int page, int pageSize)
        {
            return _repo.GetPagedAsync(search, sortColumn, descending, page, pageSize);
        }

        public Task<AdminUser> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<int> CreateAsync(UserFormViewModel model)
        {
            string salt = PasswordHelper.GenerateSalt();
            var user = new AdminUser
            {
                FullName = model.FullName.Trim(),
                Email = model.Email.Trim().ToLower(),
                PasswordSalt = salt,
                PasswordHash = PasswordHelper.HashPassword(model.Password, salt),
                IsActive = model.IsActive,
                CreatedAt = DateTime.UtcNow
            };
            return await _repo.CreateAsync(user);
        }

        public async Task UpdateAsync(UserFormViewModel model)
        {
            var user = new AdminUser
            {
                Id = model.Id,
                FullName = model.FullName.Trim(),
                Email = model.Email.Trim().ToLower(),
                IsActive = model.IsActive
            };
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                string salt = PasswordHelper.GenerateSalt();
                user.PasswordSalt = salt;
                user.PasswordHash = PasswordHelper.HashPassword(model.Password, salt);
            }
            await _repo.UpdateAsync(user);
        }

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<bool> EmailExistsAsync(string email, int excludeId) =>
            _repo.EmailExistsAsync(email, excludeId);
    }
}

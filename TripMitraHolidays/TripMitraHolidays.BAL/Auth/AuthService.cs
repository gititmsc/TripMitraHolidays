using System;
using TripMitraHolidays.Core.DTOs;
using TripMitraHolidays.Core.Helpers;
using TripMitraHolidays.Repositories.AdminUser;

namespace TripMitraHolidays.BAL.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAdminUserRepository _adminUserRepo;

        public AuthService(IAdminUserRepository adminUserRepo)
        {
            _adminUserRepo = adminUserRepo;
        }

        public LoginResult ValidateLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return LoginResult.Fail("Invalid email or password.");

            var user = _adminUserRepo.GetByEmail(email.Trim().ToLower());
            if (user == null)
                return LoginResult.Fail("Invalid email or password.");

            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return LoginResult.Fail("Invalid email or password.");

            _adminUserRepo.UpdateLastLogin(user.Id, DateTime.UtcNow);
            return LoginResult.Ok(user);
        }
    }
}

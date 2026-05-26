using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Core.DTOs
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public AdminUser User { get; set; }

        public static LoginResult Fail(string message)
            => new LoginResult { Success = false, ErrorMessage = message };

        public static LoginResult Ok(AdminUser user)
            => new LoginResult { Success = true, User = user };
    }
}

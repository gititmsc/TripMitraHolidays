using TripMitraHolidays.Core.DTOs;

namespace TripMitraHolidays.BAL.Auth
{
    public interface IAuthService
    {
        LoginResult ValidateLogin(string email, string password);
    }
}

using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.BAL.Email
{
    public interface IEmailService
    {
        Task SendInquiryEmailsAsync(Inquiry inquiry);
    }
}

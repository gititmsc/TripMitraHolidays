using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.BAL.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly int    _port;
        private readonly string _user;
        private readonly string _password;
        private readonly bool   _enableSsl;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly string _adminEmail;

        public EmailService()
        {
            var cfg    = ConfigurationManager.AppSettings;
            _host      = cfg["SmtpHost"]       ?? "";
            _port      = int.TryParse(cfg["SmtpPort"],      out int p)   ? p   : 587;
            _user      = cfg["SmtpUser"]       ?? "";
            _password  = cfg["SmtpPassword"]   ?? "";
            _enableSsl = bool.TryParse(cfg["SmtpEnableSsl"], out bool ssl) ? ssl : true;
            _fromEmail = cfg["EmailFrom"]      ?? "booking@tripmitraholidays.com";
            _fromName  = cfg["EmailFromName"]  ?? "TripMitra Holidays";
            _adminEmail = cfg["AdminEmail"]    ?? "sanket@tripmitraholidays.com";
        }

        public async Task SendInquiryEmailsAsync(Inquiry inquiry)
        {
            if (string.IsNullOrWhiteSpace(_host)) return;

            try
            {
                await SendAsync(
                    inquiry.EmailAddress,
                    inquiry.FullName,
                    "Your Travel Enquiry – TripMitra Holidays",
                    BuildCustomerEmail(inquiry));
            }
            catch { /* don't block on customer email failure */ }

            try
            {
                string firstName = inquiry.FullName.Split(' ')[0];
                string dest = string.IsNullOrEmpty(inquiry.PreferredDestination)
                    ? "Not specified" : inquiry.PreferredDestination;

                await SendAsync(
                    _adminEmail,
                    "TripMitra Admin",
                    $"New Enquiry: {inquiry.FullName} – {dest}",
                    BuildAdminEmail(inquiry));
            }
            catch { /* don't block on admin email failure */ }
        }

        // ──────────────────────────────────────────────
        // CUSTOMER CONFIRMATION EMAIL
        // ──────────────────────────────────────────────
        private string BuildCustomerEmail(Inquiry inquiry)
        {
            string firstName = inquiry.FullName.Split(' ')[0];
            var sb = new StringBuilder();

            sb.Append(@"<!DOCTYPE html>
<html lang=""en"">
<head><meta charset=""UTF-8""><meta name=""viewport"" content=""width=device-width,initial-scale=1"">
<title>Your Travel Enquiry – TripMitra Holidays</title></head>
<body style=""margin:0;padding:0;background:#f4f6ff;font-family:Arial,Helvetica,sans-serif;"">
<table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background:#f4f6ff;"">
<tr><td align=""center"" style=""padding:32px 16px;"">

  <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0""
         style=""background:#ffffff;border-radius:14px;overflow:hidden;box-shadow:0 4px 24px rgba(0,0,0,0.08);max-width:600px;"">

    <!-- HEADER -->
    <tr>
      <td style=""background:#0d1b4b;padding:28px 36px;text-align:center;"">
        <div style=""color:#c9a227;font-size:26px;font-weight:bold;letter-spacing:1px;"">TripMitra Holidays</div>
        <div style=""color:rgba(255,255,255,0.55);font-size:11px;margin-top:5px;letter-spacing:3px;text-transform:uppercase;"">Explore More, Worry Less</div>
      </td>
    </tr>

    <!-- HERO -->
    <tr>
      <td style=""padding:36px 36px 24px;"">
        <h2 style=""color:#0d1b4b;font-size:22px;margin:0 0 12px;"">Thank You, ").Append(HtmlEncode(firstName)).Append(@"!</h2>
        <p style=""color:#555;font-size:14px;line-height:1.8;margin:0 0 8px;"">
          We have received your travel enquiry and are thrilled to help you plan your perfect holiday.
        </p>
        <p style=""color:#555;font-size:14px;line-height:1.8;margin:0 0 24px;"">
          Our travel expert will review your details and get back to you within
          <strong style=""color:#0d1b4b;"">24 hours</strong>.
        </p>
      </td>
    </tr>

    <!-- ENQUIRY SUMMARY -->
    <tr>
      <td style=""padding:0 36px 28px;"">
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""
               style=""background:#f4f6ff;border-radius:10px;overflow:hidden;"">
          <tr>
            <td style=""padding:18px 22px 6px;"">
              <div style=""font-size:10px;font-weight:bold;color:#c9a227;letter-spacing:2.5px;text-transform:uppercase;"">
                &#9864; Your Enquiry Summary
              </div>
            </td>
          </tr>");

            AppendSummaryRow(sb, "Full Name",    inquiry.FullName);
            AppendSummaryRow(sb, "Mobile",       inquiry.MobileNumber);
            AppendSummaryRow(sb, "Email",        inquiry.EmailAddress);

            if (!string.IsNullOrEmpty(inquiry.PreferredDestination))
                AppendSummaryRow(sb, "Destination", inquiry.PreferredDestination, highlight: true);

            if (inquiry.TravelDate.HasValue)
                AppendSummaryRow(sb, "Travel Date", inquiry.TravelDate.Value.ToString("dd MMMM yyyy"));

            if (inquiry.NumberOfPersons.HasValue)
                AppendSummaryRow(sb, "Persons", inquiry.NumberOfPersons.Value.ToString());

            if (inquiry.Budget.HasValue)
                AppendSummaryRow(sb, "Budget", "₹" + inquiry.Budget.Value.ToString("N0"), highlight: true);

            if (!string.IsNullOrEmpty(inquiry.City))
                AppendSummaryRow(sb, "City", inquiry.City);

            sb.Append(@"
          <tr><td style=""height:12px;""></td></tr>
        </table>
      </td>
    </tr>");

            // Message block
            if (!string.IsNullOrEmpty(inquiry.Message))
            {
                sb.Append(@"
    <tr>
      <td style=""padding:0 36px 28px;"">
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""
               style=""background:#fff8e8;border-left:3px solid #c9a227;border-radius:4px;"">
          <tr>
            <td style=""padding:14px 18px;"">
              <div style=""font-size:11px;font-weight:bold;color:#c9a227;letter-spacing:1px;text-transform:uppercase;margin-bottom:6px;"">Your Message</div>
              <p style=""font-size:13px;color:#555;line-height:1.7;margin:0;"">").Append(HtmlEncode(inquiry.Message)).Append(@"</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>");
            }

            sb.Append(@"
    <!-- CTA BUTTONS -->
    <tr>
      <td style=""padding:0 36px 32px;text-align:center;"">
        <table cellpadding=""0"" cellspacing=""0"" border=""0"" style=""margin:0 auto;"">
          <tr>
            <td style=""padding:0 6px;"">
              <a href=""https://wa.me/919724895328?text=Hi%2C+I+made+an+enquiry+on+your+website""
                 style=""display:inline-block;background:#25d366;color:#ffffff;padding:12px 26px;border-radius:8px;font-size:13px;font-weight:bold;text-decoration:none;"">
                &#128172; Chat on WhatsApp
              </a>
            </td>
            <td style=""padding:0 6px;"">
              <a href=""tel:+919724895328""
                 style=""display:inline-block;background:#0d1b4b;color:#ffffff;padding:12px 26px;border-radius:8px;font-size:13px;font-weight:bold;text-decoration:none;"">
                &#128222; Call Us
              </a>
            </td>
          </tr>
        </table>
      </td>
    </tr>

    <!-- CONTACT INFO BAR -->
    <tr>
      <td style=""background:#f8f9fc;border-top:1px solid #eef0f8;padding:16px 36px;"">
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
          <tr>
            <td style=""text-align:center;"">
              <span style=""font-size:12px;color:#888;"">
                &#128222; <a href=""tel:+919724895328"" style=""color:#0d1b4b;text-decoration:none;"">+91 97248 95328</a>
                &nbsp;&bull;&nbsp;
                &#128231; <a href=""mailto:booking@tripmitraholidays.com"" style=""color:#0d1b4b;text-decoration:none;"">booking@tripmitraholidays.com</a>
              </span>
            </td>
          </tr>
        </table>
      </td>
    </tr>

    <!-- FOOTER -->
    <tr>
      <td style=""background:#0a133a;padding:14px 36px;text-align:center;"">
        <p style=""color:#3a4f7a;font-size:11px;margin:0;"">
          &copy; ").Append(DateTime.Now.Year).Append(@" TripMitra Holidays. All rights reserved.
        </p>
      </td>
    </tr>

  </table>
</td></tr>
</table>
</body></html>");

            return sb.ToString();
        }

        // ──────────────────────────────────────────────
        // ADMIN NOTIFICATION EMAIL
        // ──────────────────────────────────────────────
        private string BuildAdminEmail(Inquiry inquiry)
        {
            string dest = string.IsNullOrEmpty(inquiry.PreferredDestination) ? "Not specified" : inquiry.PreferredDestination;
            var sb = new StringBuilder();

            sb.Append(@"<!DOCTYPE html>
<html lang=""en"">
<head><meta charset=""UTF-8""><meta name=""viewport"" content=""width=device-width,initial-scale=1"">
<title>New Enquiry – TripMitra Admin</title></head>
<body style=""margin:0;padding:0;background:#f0f2f8;font-family:Arial,Helvetica,sans-serif;"">
<table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background:#f0f2f8;"">
<tr><td align=""center"" style=""padding:32px 16px;"">

  <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0""
         style=""background:#ffffff;border-radius:14px;overflow:hidden;box-shadow:0 4px 24px rgba(0,0,0,0.08);max-width:600px;"">

    <!-- HEADER -->
    <tr>
      <td style=""background:#0d1b4b;padding:22px 36px;"">
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
          <tr>
            <td>
              <div style=""color:#c9a227;font-size:18px;font-weight:bold;"">TripMitra Holidays</div>
              <div style=""color:rgba(255,255,255,0.5);font-size:11px;margin-top:2px;"">Admin Notification</div>
            </td>
            <td align=""right"">
              <span style=""background:#c9a227;color:#0d1b4b;font-size:11px;font-weight:bold;padding:5px 12px;border-radius:20px;letter-spacing:0.5px;"">
                &#128276; NEW ENQUIRY
              </span>
            </td>
          </tr>
        </table>
      </td>
    </tr>

    <!-- ALERT -->
    <tr>
      <td style=""background:#fff8e8;border-bottom:1px solid #f5e2a0;padding:16px 36px;"">
        <p style=""margin:0;font-size:14px;color:#92690a;"">
          A new travel enquiry has been submitted on <strong>").Append(inquiry.CreatedDate.ToLocalTime().ToString("dd MMM yyyy")).Append(@" at ")
                .Append(inquiry.CreatedDate.ToLocalTime().ToString("hh:mm tt")).Append(@"</strong>.
        </p>
      </td>
    </tr>

    <!-- CONTACT DETAILS -->
    <tr>
      <td style=""padding:28px 36px 8px;"">
        <div style=""font-size:11px;font-weight:bold;color:#c9a227;letter-spacing:2px;text-transform:uppercase;margin-bottom:14px;"">
          Contact Details
        </div>
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">");

            AppendAdminRow(sb, "Full Name",   inquiry.FullName, bold: true);
            AppendAdminRow(sb, "Mobile",      $"<a href=\"tel:{inquiry.MobileNumber}\" style=\"color:#0d1b4b;font-weight:bold;text-decoration:none;\">{inquiry.MobileNumber}</a>", raw: true);
            AppendAdminRow(sb, "Email",       $"<a href=\"mailto:{inquiry.EmailAddress}\" style=\"color:#0d1b4b;text-decoration:none;\">{inquiry.EmailAddress}</a>", raw: true);
            if (!string.IsNullOrEmpty(inquiry.City))
                AppendAdminRow(sb, "City", inquiry.City);

            sb.Append(@"
        </table>
      </td>
    </tr>

    <!-- TRIP DETAILS -->
    <tr>
      <td style=""padding:8px 36px 28px;"">
        <div style=""font-size:11px;font-weight:bold;color:#c9a227;letter-spacing:2px;text-transform:uppercase;margin-bottom:14px;margin-top:20px;"">
          Trip Details
        </div>
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">");

            AppendAdminRow(sb, "Destination", dest, highlight: true);
            AppendAdminRow(sb, "Travel Date",
                inquiry.TravelDate.HasValue ? inquiry.TravelDate.Value.ToString("dd MMMM yyyy") : "Not specified");
            AppendAdminRow(sb, "Persons",
                inquiry.NumberOfPersons.HasValue ? inquiry.NumberOfPersons.Value.ToString() : "Not specified");
            AppendAdminRow(sb, "Budget",
                inquiry.Budget.HasValue ? "₹" + inquiry.Budget.Value.ToString("N0") : "Not specified",
                highlight: inquiry.Budget.HasValue);

            sb.Append(@"
        </table>
      </td>
    </tr>");

            if (!string.IsNullOrEmpty(inquiry.Message))
            {
                sb.Append(@"
    <!-- MESSAGE -->
    <tr>
      <td style=""padding:0 36px 28px;"">
        <div style=""font-size:11px;font-weight:bold;color:#c9a227;letter-spacing:2px;text-transform:uppercase;margin-bottom:10px;"">
          Customer Message
        </div>
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""
               style=""background:#f4f6ff;border-left:3px solid #0d1b4b;border-radius:4px;"">
          <tr>
            <td style=""padding:14px 18px;"">
              <p style=""font-size:13px;color:#333;line-height:1.8;margin:0;"">").Append(HtmlEncode(inquiry.Message)).Append(@"</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>");
            }

            string waNumber = inquiry.MobileNumber.Replace("+", "").Replace("-", "").Replace(" ", "");
            string firstName = Uri.EscapeDataString(inquiry.FullName.Split(' ')[0]);

            sb.Append(@"
    <!-- QUICK ACTIONS -->
    <tr>
      <td style=""background:#f4f6ff;border-top:1px solid #eef0f8;padding:22px 36px;"">
        <div style=""font-size:11px;font-weight:bold;color:#888;letter-spacing:1.5px;text-transform:uppercase;margin-bottom:14px;"">Quick Actions</div>
        <table cellpadding=""0"" cellspacing=""0"" border=""0"">
          <tr>
            <td style=""padding-right:10px;"">
              <a href=""tel:").Append(inquiry.MobileNumber).Append(@"""
                 style=""display:inline-block;background:#0d1b4b;color:#ffffff;padding:11px 20px;border-radius:7px;font-size:12px;font-weight:bold;text-decoration:none;"">
                &#128222; Call ").Append(HtmlEncode(inquiry.MobileNumber)).Append(@"
              </a>
            </td>
            <td style=""padding-right:10px;"">
              <a href=""mailto:").Append(HtmlEncode(inquiry.EmailAddress)).Append(@"?subject=Re%3A%20Your%20Holiday%20Enquiry%20%E2%80%93%20TripMitra%20Holidays""
                 style=""display:inline-block;background:#6366f1;color:#ffffff;padding:11px 20px;border-radius:7px;font-size:12px;font-weight:bold;text-decoration:none;"">
                &#128231; Reply via Email
              </a>
            </td>
            <td>
              <a href=""https://wa.me/").Append(waNumber).Append(@"?text=Hi%20").Append(firstName).Append(@"%2C%20this%20is%20TripMitra%20Holidays.%20Thank%20you%20for%20your%20enquiry!""
                 style=""display:inline-block;background:#25d366;color:#ffffff;padding:11px 20px;border-radius:7px;font-size:12px;font-weight:bold;text-decoration:none;"">
                &#128172; WhatsApp
              </a>
            </td>
          </tr>
        </table>
      </td>
    </tr>

    <!-- FOOTER -->
    <tr>
      <td style=""background:#0a133a;padding:14px 36px;text-align:center;"">
        <p style=""color:#3a4f7a;font-size:11px;margin:0;"">
          &copy; ").Append(DateTime.Now.Year).Append(@" TripMitra Holidays &mdash; Admin Notification System
        </p>
      </td>
    </tr>

  </table>
</td></tr>
</table>
</body></html>");

            return sb.ToString();
        }

        // ──────────────────────────────────────────────
        // HELPERS
        // ──────────────────────────────────────────────
        private static void AppendSummaryRow(StringBuilder sb, string label, string value, bool highlight = false)
        {
            string valueStyle = highlight
                ? "font-size:13px;color:#0d1b4b;font-weight:700;"
                : "font-size:13px;color:#444;";
            sb.Append($@"
          <tr>
            <td style=""padding:7px 22px;font-size:11px;color:#888;font-weight:bold;text-transform:uppercase;letter-spacing:0.8px;width:38%;"">
              {HtmlEncode(label)}
            </td>
            <td style=""padding:7px 22px;{valueStyle}"">
              {HtmlEncode(value)}
            </td>
          </tr>
          <tr><td colspan=""2"" style=""padding:0 22px;""><div style=""height:1px;background:#e8eaf0;""></div></td></tr>");
        }

        private static void AppendAdminRow(StringBuilder sb, string label, string value,
            bool highlight = false, bool bold = false, bool raw = false)
        {
            string valueStyle = "font-size:13px;color:" + (highlight ? "#c9a227;font-weight:700;" : bold ? "#111827;font-weight:600;" : "#555;");
            string cellValue = raw ? value : HtmlEncode(value);
            sb.Append($@"
          <tr>
            <td style=""padding:8px 0;font-size:12px;color:#9ca3af;font-weight:bold;text-transform:uppercase;letter-spacing:0.8px;width:35%;vertical-align:top;"">
              {HtmlEncode(label)}
            </td>
            <td style=""padding:8px 0;{valueStyle}"">
              {cellValue}
            </td>
          </tr>");
        }

        private static string HtmlEncode(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return System.Web.HttpUtility.HtmlEncode(text);
        }

        private async Task SendAsync(string toEmail, string toName, string subject, string htmlBody)
        {
            using (var smtp = new SmtpClient(_host, _port))
            {
                smtp.EnableSsl = _enableSsl;
                if (!string.IsNullOrEmpty(_user))
                    smtp.Credentials = new NetworkCredential(_user, _password);

                using (var msg = new MailMessage())
                {
                    msg.From = new MailAddress(_fromEmail, _fromName);
                    msg.To.Add(new MailAddress(toEmail, toName));
                    msg.Subject = subject;
                    msg.Body    = htmlBody;
                    msg.IsBodyHtml = true;
                    await smtp.SendMailAsync(msg);
                }
            }
        }
    }
}

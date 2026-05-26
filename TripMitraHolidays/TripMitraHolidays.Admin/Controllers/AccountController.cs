using System.Web.Mvc;
using System.Web.Security;
using TripMitraHolidays.BAL.Auth;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.AdminUser;

namespace TripMitraHolidays.Admin.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController()
        {
            _authService = new AuthService(new AdminUserRepository());
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _authService.ValidateLogin(model.Email, model.Password);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(result.User.Email, model.RememberMe);

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}

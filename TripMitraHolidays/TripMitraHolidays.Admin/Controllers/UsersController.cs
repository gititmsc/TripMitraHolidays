using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TripMitraHolidays.BAL.Users;
using TripMitraHolidays.Core.Helpers;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.AdminUser;

namespace TripMitraHolidays.Admin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _service;

        public UsersController()
        {
            _service = new UserService(new AdminUserRepository());
        }

        // GET: /Users
        public async Task<ActionResult> Index(
            string search = "", string sort = "createdat", string dir = "desc",
            int page = 1, int pageSize = 10)
        {
            ViewBag.PageTitle = "Users";

            var allowedSizes = new[] { 10, 25, 50, 100 };
            if (Array.IndexOf(allowedSizes, pageSize) < 0) pageSize = 10;
            if (page < 1) page = 1;

            bool descending = string.Equals(dir, "desc", StringComparison.OrdinalIgnoreCase);
            var result = await _service.GetPagedAsync(search, sort, descending, page, pageSize);

            var vm = new UserListViewModel
            {
                Users      = result.Item1,
                TotalCount = result.Item2,
                Page       = page,
                PageSize   = pageSize,
                Search     = search ?? "",
                SortColumn = sort,
                SortDir    = dir
            };

            return View(vm);
        }

        // GET: /Users/Manage          → Create mode
        // GET: /Users/Manage?uid=xxx  → Edit mode
        public async Task<ActionResult> Manage(string uid = null)
        {
            bool isEdit = !string.IsNullOrEmpty(uid);
            ViewBag.PageTitle = isEdit ? "Edit User" : "Add User";

            if (!isEdit)
                return View(new UserFormViewModel());

            int? id = IdProtector.Unprotect(uid);
            if (id == null) return HttpNotFound();

            var user = await _service.GetByIdAsync(id.Value);
            if (user == null) return HttpNotFound();

            return View(new UserFormViewModel
            {
                EncryptedId = uid,
                Id          = id.Value,
                FullName    = user.FullName,
                Email       = user.Email,
                IsActive    = user.IsActive
            });
        }

        // POST: /Users/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(UserFormViewModel model)
        {
            bool isEdit = model.IsEditMode;

            // Decode the encrypted ID for edit mode
            if (isEdit)
            {
                int? id = IdProtector.Unprotect(model.EncryptedId);
                if (id == null) return HttpNotFound();
                model.Id = id.Value;
            }

            // Password required only on create
            if (!isEdit && string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", "Password is required.");

            // If a new password was supplied it must match confirm
            if (!string.IsNullOrWhiteSpace(model.Password) && model.Password != model.ConfirmPassword)
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");

            if (!ModelState.IsValid)
            {
                ViewBag.PageTitle = isEdit ? "Edit User" : "Add User";
                return View(model);
            }

            if (await _service.EmailExistsAsync(model.Email, model.Id))
            {
                ModelState.AddModelError("Email", isEdit
                    ? "This email is already in use by another user."
                    : "This email address is already in use.");
                ViewBag.PageTitle = isEdit ? "Edit User" : "Add User";
                return View(model);
            }

            if (isEdit)
            {
                await _service.UpdateAsync(model);
                TempData["Success"] = $"User \"{model.FullName}\" updated successfully.";
            }
            else
            {
                await _service.CreateAsync(model);
                TempData["Success"] = $"User \"{model.FullName}\" created successfully.";
            }

            return RedirectToAction("Index");
        }

        // POST: /Users/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string uid)
        {
            int? id = IdProtector.Unprotect(uid);
            if (id == null) return HttpNotFound();

            await _service.DeleteAsync(id.Value);
            TempData["Success"] = "User deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TripMitraHolidays.BAL.Packages;
using TripMitraHolidays.Core.DTOs;
using TripMitraHolidays.Core.Models;
using TripMitraHolidays.Core.ViewModels;
using TripMitraHolidays.Repositories.Packages;

namespace TripMitraHolidays.Admin.Controllers
{
    [Authorize]
    public class PackagesController : Controller
    {
        private readonly IPackageService _service;

        public PackagesController()
        {
            _service = new PackageService(new PackageRepository());
        }

        // GET: /Packages
        public async Task<ActionResult> Index(
            string search = "", string status = "all",
            string sort = "displayorder", string dir = "asc",
            int page = 1, int pageSize = 10)
        {
            ViewBag.PageTitle = "Packages";

            var allowedSizes = new[] { 10, 25, 50, 100 };
            if (Array.IndexOf(allowedSizes, pageSize) < 0) pageSize = 10;
            if (page < 1) page = 1;

            bool? isActive = status == "active" ? true : status == "inactive" ? (bool?)false : null;
            bool descending = string.Equals(dir, "desc", StringComparison.OrdinalIgnoreCase);

            var result = await _service.GetPagedAsync(search, isActive, sort, descending, page, pageSize);

            var vm = new PackageListViewModel
            {
                Packages     = result.Item1,
                TotalCount   = result.Item2,
                Page         = page,
                PageSize     = pageSize,
                SortColumn   = sort,
                SortDir      = dir,
                Search       = search ?? "",
                StatusFilter = status ?? "all"
            };

            return View(vm);
        }

        // GET: /Packages/Create
        public ActionResult Create()
        {
            ViewBag.PageTitle = "Add Package";
            return View(new PackageFormViewModel());
        }

        // POST: /Packages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(PackageFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PageTitle = "Add Package";
                return View(model);
            }

            string uploadPath = Server.MapPath("~/Uploads/Packages/");
            EnsureDirectory(uploadPath);

            var package = MapToEntity(model);
            package.CreatedDate = DateTime.UtcNow;

            var thumbnailFile = Request.Files["ThumbnailFile"];
            var bannerFile = Request.Files["BannerFile"];

            if (thumbnailFile != null && thumbnailFile.ContentLength > 0)
                package.ThumbnailImage = SaveUploadedFile(thumbnailFile, uploadPath);

            if (bannerFile != null && bannerFile.ContentLength > 0)
                package.BannerImage = SaveUploadedFile(bannerFile, uploadPath);

            package.Slug = await _service.MakeUniqueSlugAsync(
                string.IsNullOrWhiteSpace(model.Slug)
                    ? _service.GenerateSlug(model.PackageName)
                    : model.Slug, 0);

            int newId = await _service.CreateAsync(package);

            var galleryImages = CollectGalleryImages(newId, uploadPath);
            if (galleryImages.Any())
                await _service.AddGalleryImagesAsync(galleryImages);

            TempData["Success"] = $"Package \"{model.PackageName}\" created successfully.";
            return RedirectToAction("Index");
        }

        // GET: /Packages/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.PageTitle = "Edit Package";
            var package = await _service.GetByIdAsync(id);
            if (package == null) return HttpNotFound();

            return View(MapToViewModel(package));
        }

        // POST: /Packages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(int id, PackageFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PageTitle = "Edit Package";
                return View(model);
            }

            string uploadPath = Server.MapPath("~/Uploads/Packages/");
            EnsureDirectory(uploadPath);

            var package = MapToEntity(model);
            package.PackageId = id;
            package.UpdatedDate = DateTime.UtcNow;

            var thumbnailFile = Request.Files["ThumbnailFile"];
            var bannerFile = Request.Files["BannerFile"];

            package.ThumbnailImage = (thumbnailFile != null && thumbnailFile.ContentLength > 0)
                ? SaveUploadedFile(thumbnailFile, uploadPath)
                : model.ExistingThumbnailImage;

            package.BannerImage = (bannerFile != null && bannerFile.ContentLength > 0)
                ? SaveUploadedFile(bannerFile, uploadPath)
                : model.ExistingBannerImage;

            package.Slug = await _service.MakeUniqueSlugAsync(
                string.IsNullOrWhiteSpace(model.Slug)
                    ? _service.GenerateSlug(model.PackageName)
                    : model.Slug, id);

            await _service.UpdateAsync(package);

            var galleryImages = CollectGalleryImages(id, uploadPath);
            if (galleryImages.Any())
                await _service.AddGalleryImagesAsync(galleryImages);

            TempData["Success"] = $"Package \"{model.PackageName}\" updated successfully.";
            return RedirectToAction("Index");
        }

        // GET: /Packages/Details/5
        public async Task<ActionResult> Details(int id)
        {
            ViewBag.PageTitle = "Package Details";
            var package = await _service.GetByIdAsync(id);
            if (package == null) return HttpNotFound();
            return View(package);
        }

        // POST: /Packages/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Package deleted successfully.";
            return RedirectToAction("Index");
        }

        // POST: /Packages/ToggleActive  (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ToggleActive(int id)
        {
            bool newState = await _service.ToggleActiveAsync(id);
            return Json(new { success = true, isActive = newState });
        }

        // POST: /Packages/DeleteGalleryImage  (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteGalleryImage(int imageId)
        {
            await _service.DeleteGalleryImageAsync(imageId);
            return Json(new { success = true });
        }

        // ──────────────────────────────────────────────
        // Private helpers
        // ──────────────────────────────────────────────

        private Package MapToEntity(PackageFormViewModel m)
        {
            var package = new Package
            {
                PackageName    = m.PackageName?.Trim(),
                ShortDescription = m.ShortDescription,
                Description    = m.Description,
                PackagePrice   = m.PackagePrice,
                DiscountPrice  = m.DiscountPrice,
                DurationDays   = m.DurationDays,
                DurationNights = m.DurationNights,
                Destination    = m.Destination,
                Country        = m.Country,
                StartingCity   = m.StartingCity,
                PackageType    = m.PackageType,
                TourCategory   = m.TourCategory,
                HotelRating    = m.HotelRating,
                MealType       = m.MealType,
                Transportation = m.Transportation,
                IsFlightIncluded = m.IsFlightIncluded,
                IsVisaIncluded = m.IsVisaIncluded,
                IsFeatured     = m.IsFeatured,
                IsPopular      = m.IsPopular,
                IsActive       = m.IsActive,
                DisplayOrder   = m.DisplayOrder,
                MetaTitle      = m.MetaTitle,
                MetaKeywords   = m.MetaKeywords,
                MetaDescription = m.MetaDescription
            };

            // Parse itineraries JSON
            if (!string.IsNullOrWhiteSpace(m.ItinerariesJson))
            {
                var items = JsonConvert.DeserializeObject<List<PackageItineraryDto>>(m.ItinerariesJson)
                    ?? new List<PackageItineraryDto>();
                package.Itineraries = items
                    .Where(i => !string.IsNullOrWhiteSpace(i.Title))
                    .Select(i => new PackageItinerary
                    {
                        DayNumber   = i.DayNumber,
                        Title       = i.Title.Trim(),
                        Description = i.Description
                    }).ToList();
            }

            // Parse inclusions JSON
            if (!string.IsNullOrWhiteSpace(m.InclusionsJson))
            {
                var items = JsonConvert.DeserializeObject<List<string>>(m.InclusionsJson)
                    ?? new List<string>();
                package.Inclusions = items
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => new PackageInclusion { Item = s.Trim() })
                    .ToList();
            }

            // Parse exclusions JSON
            if (!string.IsNullOrWhiteSpace(m.ExclusionsJson))
            {
                var items = JsonConvert.DeserializeObject<List<string>>(m.ExclusionsJson)
                    ?? new List<string>();
                package.Exclusions = items
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => new PackageExclusion { Item = s.Trim() })
                    .ToList();
            }

            return package;
        }

        private static PackageFormViewModel MapToViewModel(Package p)
        {
            return new PackageFormViewModel
            {
                PackageId         = p.PackageId,
                PackageName       = p.PackageName,
                Slug              = p.Slug,
                ShortDescription  = p.ShortDescription,
                Description       = p.Description,
                PackagePrice      = p.PackagePrice,
                DiscountPrice     = p.DiscountPrice,
                DurationDays      = p.DurationDays,
                DurationNights    = p.DurationNights,
                Destination       = p.Destination,
                Country           = p.Country,
                StartingCity      = p.StartingCity,
                PackageType       = p.PackageType,
                TourCategory      = p.TourCategory,
                HotelRating       = p.HotelRating,
                MealType          = p.MealType,
                Transportation    = p.Transportation,
                IsFlightIncluded  = p.IsFlightIncluded,
                IsVisaIncluded    = p.IsVisaIncluded,
                IsFeatured        = p.IsFeatured,
                IsPopular         = p.IsPopular,
                IsActive          = p.IsActive,
                DisplayOrder      = p.DisplayOrder,
                MetaTitle         = p.MetaTitle,
                MetaKeywords      = p.MetaKeywords,
                MetaDescription   = p.MetaDescription,
                ExistingThumbnailImage = p.ThumbnailImage,
                ExistingBannerImage    = p.BannerImage,
                ExistingGalleryImages  = p.Images?
                    .OrderBy(i => i.DisplayOrder)
                    .Select(i => new GalleryImageDto { ImageId = i.ImageId, ImagePath = i.ImagePath })
                    .ToList() ?? new List<GalleryImageDto>(),
                ItinerariesJson = p.Itineraries?.Any() == true
                    ? JsonConvert.SerializeObject(
                        p.Itineraries.OrderBy(i => i.DayNumber)
                         .Select(i => new { dayNumber = i.DayNumber, title = i.Title, description = i.Description }))
                    : "[]",
                InclusionsJson = p.Inclusions?.Any() == true
                    ? JsonConvert.SerializeObject(p.Inclusions.Select(i => i.Item))
                    : "[]",
                ExclusionsJson = p.Exclusions?.Any() == true
                    ? JsonConvert.SerializeObject(p.Exclusions.Select(e => e.Item))
                    : "[]"
            };
        }

        private static string SaveUploadedFile(HttpPostedFileBase file, string uploadPath)
        {
            string ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            string fileName = Guid.NewGuid().ToString("N") + ext;
            file.SaveAs(Path.Combine(uploadPath, fileName));
            return "/Uploads/Packages/" + fileName;
        }

        private List<PackageImage> CollectGalleryImages(int packageId, string uploadPath)
        {
            var result = new List<PackageImage>();
            int order = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files.GetKey(i) == "GalleryFiles")
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        result.Add(new PackageImage
                        {
                            PackageId    = packageId,
                            ImagePath    = SaveUploadedFile(file, uploadPath),
                            DisplayOrder = order++
                        });
                    }
                }
            }
            return result;
        }

        private static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Repositories.Packages
{
    public class PackageRepository : IPackageRepository
    {
        public async Task<List<Package>> GetAllAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Packages
                    .Include(p => p.Images)
                    .OrderBy(p => p.DisplayOrder)
                    .ThenByDescending(p => p.CreatedDate)
                    .ToListAsync();
            }
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Packages
                    .Include(p => p.Images)
                    .Include(p => p.Itineraries)
                    .Include(p => p.Inclusions)
                    .Include(p => p.Exclusions)
                    .FirstOrDefaultAsync(p => p.PackageId == id);
            }
        }

        public async Task<int> AddAsync(Package package)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Packages.Add(package);
                await db.SaveChangesAsync();
                return package.PackageId;
            }
        }

        public async Task UpdateAsync(Package package)
        {
            using (var db = new ApplicationDbContext())
            {
                var existing = await db.Packages.FindAsync(package.PackageId);
                if (existing == null) return;

                // Update scalar properties
                existing.PackageName = package.PackageName;
                existing.Slug = package.Slug;
                existing.ShortDescription = package.ShortDescription;
                existing.Description = package.Description;
                existing.PackagePrice = package.PackagePrice;
                existing.DiscountPrice = package.DiscountPrice;
                existing.DurationDays = package.DurationDays;
                existing.DurationNights = package.DurationNights;
                existing.Destination = package.Destination;
                existing.Country = package.Country;
                existing.StartingCity = package.StartingCity;
                existing.PackageType = package.PackageType;
                existing.TourCategory = package.TourCategory;
                existing.ThumbnailImage = package.ThumbnailImage;
                existing.BannerImage = package.BannerImage;
                existing.HotelRating = package.HotelRating;
                existing.MealType = package.MealType;
                existing.Transportation = package.Transportation;
                existing.IsFlightIncluded = package.IsFlightIncluded;
                existing.IsVisaIncluded = package.IsVisaIncluded;
                existing.IsFeatured = package.IsFeatured;
                existing.IsPopular = package.IsPopular;
                existing.IsActive = package.IsActive;
                existing.DisplayOrder = package.DisplayOrder;
                existing.MetaTitle = package.MetaTitle;
                existing.MetaKeywords = package.MetaKeywords;
                existing.MetaDescription = package.MetaDescription;
                existing.UpdatedDate = DateTime.UtcNow;

                // Replace child collections
                var oldItineraries = await db.PackageItineraries
                    .Where(i => i.PackageId == package.PackageId).ToListAsync();
                db.PackageItineraries.RemoveRange(oldItineraries);

                var oldInclusions = await db.PackageInclusions
                    .Where(i => i.PackageId == package.PackageId).ToListAsync();
                db.PackageInclusions.RemoveRange(oldInclusions);

                var oldExclusions = await db.PackageExclusions
                    .Where(e => e.PackageId == package.PackageId).ToListAsync();
                db.PackageExclusions.RemoveRange(oldExclusions);

                if (package.Itineraries != null)
                    foreach (var item in package.Itineraries)
                    {
                        item.PackageId = package.PackageId;
                        db.PackageItineraries.Add(item);
                    }

                if (package.Inclusions != null)
                    foreach (var item in package.Inclusions)
                    {
                        item.PackageId = package.PackageId;
                        db.PackageInclusions.Add(item);
                    }

                if (package.Exclusions != null)
                    foreach (var item in package.Exclusions)
                    {
                        item.PackageId = package.PackageId;
                        db.PackageExclusions.Add(item);
                    }

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var package = await db.Packages.FindAsync(id);
                if (package == null) return;

                db.Packages.Remove(package);
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> ToggleActiveAsync(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var package = await db.Packages.FindAsync(id);
                if (package == null) return false;

                package.IsActive = !package.IsActive;
                await db.SaveChangesAsync();
                return package.IsActive;
            }
        }

        public async Task AddImagesAsync(IEnumerable<PackageImage> images)
        {
            using (var db = new ApplicationDbContext())
            {
                db.PackageImages.AddRange(images);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteImageAsync(int imageId)
        {
            using (var db = new ApplicationDbContext())
            {
                var image = await db.PackageImages.FindAsync(imageId);
                if (image == null) return;

                db.PackageImages.Remove(image);
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> SlugExistsAsync(string slug, int excludeId = 0)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Packages
                    .AnyAsync(p => p.Slug == slug && p.PackageId != excludeId);
            }
        }
    }
}

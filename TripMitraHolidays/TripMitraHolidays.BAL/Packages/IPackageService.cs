using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.BAL.Packages
{
    public interface IPackageService
    {
        Task<List<Package>> GetAllAsync();
        Task<Tuple<List<Package>, int>> GetPagedAsync(string search, bool? isActive, string sortColumn, bool descending, int page, int pageSize);
        Task<Package> GetByIdAsync(int id);
        Task<int> CreateAsync(Package package);
        Task UpdateAsync(Package package);
        Task DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(int id);
        Task AddGalleryImagesAsync(List<PackageImage> images);
        Task DeleteGalleryImageAsync(int imageId);
        Task<string> MakeUniqueSlugAsync(string slug, int excludeId = 0);
        string GenerateSlug(string name);

        // Public website methods (IsActive = true only)
        Task<Package> GetBySlugAsync(string slug);
        Task<Tuple<List<Package>, int>> GetPublicPackagesAsync(string search, string tourCategory, string packageType, int page, int pageSize);
        Task<List<Package>> GetFeaturedForHomeAsync(int maxCount);
    }
}

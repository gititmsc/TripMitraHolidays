using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.BAL.Packages
{
    public interface IPackageService
    {
        Task<List<Package>> GetAllAsync();
        Task<Package> GetByIdAsync(int id);
        Task<int> CreateAsync(Package package);
        Task UpdateAsync(Package package);
        Task DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(int id);
        Task AddGalleryImagesAsync(List<PackageImage> images);
        Task DeleteGalleryImageAsync(int imageId);
        Task<string> MakeUniqueSlugAsync(string slug, int excludeId = 0);
        string GenerateSlug(string name);
    }
}

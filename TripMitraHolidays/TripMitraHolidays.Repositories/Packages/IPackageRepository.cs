using System.Collections.Generic;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;

namespace TripMitraHolidays.Repositories.Packages
{
    public interface IPackageRepository
    {
        Task<List<Package>> GetAllAsync();
        Task<Package> GetByIdAsync(int id);
        Task<int> AddAsync(Package package);
        Task UpdateAsync(Package package);
        Task DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(int id);
        Task AddImagesAsync(IEnumerable<PackageImage> images);
        Task DeleteImageAsync(int imageId);
        Task<bool> SlugExistsAsync(string slug, int excludeId = 0);
    }
}

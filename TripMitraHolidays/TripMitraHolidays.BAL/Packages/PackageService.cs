using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TripMitraHolidays.Core.Models;
using TripMitraHolidays.Repositories.Packages;

namespace TripMitraHolidays.BAL.Packages
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _repo;

        public PackageService(IPackageRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Package>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Package> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task<int> CreateAsync(Package package) => _repo.AddAsync(package);

        public Task UpdateAsync(Package package) => _repo.UpdateAsync(package);

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<bool> ToggleActiveAsync(int id) => _repo.ToggleActiveAsync(id);

        public Task AddGalleryImagesAsync(List<PackageImage> images) => _repo.AddImagesAsync(images);

        public Task DeleteGalleryImageAsync(int imageId) => _repo.DeleteImageAsync(imageId);

        public async Task<string> MakeUniqueSlugAsync(string slug, int excludeId = 0)
        {
            string baseSlug = GenerateSlug(slug);
            if (string.IsNullOrEmpty(baseSlug)) baseSlug = "package";

            string candidate = baseSlug;
            int counter = 1;
            while (await _repo.SlugExistsAsync(candidate, excludeId))
            {
                candidate = $"{baseSlug}-{counter++}";
            }
            return candidate;
        }

        public string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;

            var slug = name.Trim().ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9\s\-]", string.Empty);
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"\-+", "-");
            return slug.Trim('-');
        }
    }
}

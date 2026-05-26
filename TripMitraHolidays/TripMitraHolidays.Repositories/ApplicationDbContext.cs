using System;
using System.Data.Entity;
using TripMitraHolidays.Core.Helpers;
using TripMitraHolidays.Core.Models;


namespace TripMitraHolidays.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=TripMitraHolidaysAdmin") { }

        public DbSet<TripMitraHolidays.Core.Models.AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripMitraHolidays.Core.Models.AdminUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbContextInitializer());
        }
    }

    internal class ApplicationDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            string salt = PasswordHelper.GenerateSalt();
            context.AdminUsers.Add(new TripMitraHolidays.Core.Models.AdminUser
            {
                FullName = "System Admin",
                Email = "admin@tripmitra.com",
                PasswordHash = PasswordHelper.HashPassword("Admin@123", salt),
                PasswordSalt = salt,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
            context.SaveChanges();
        }
    }
}

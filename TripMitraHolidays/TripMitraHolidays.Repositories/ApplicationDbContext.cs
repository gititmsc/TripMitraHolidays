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
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageImage> PackageImages { get; set; }
        public DbSet<PackageItinerary> PackageItineraries { get; set; }
        public DbSet<PackageInclusion> PackageInclusions { get; set; }
        public DbSet<PackageExclusion> PackageExclusions { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripMitraHolidays.Core.Models.AdminUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Package>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<Package>()
                .HasMany(p => p.Images)
                .WithRequired(i => i.Package)
                .HasForeignKey(i => i.PackageId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Package>()
                .HasMany(p => p.Itineraries)
                .WithRequired(i => i.Package)
                .HasForeignKey(i => i.PackageId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Package>()
                .HasMany(p => p.Inclusions)
                .WithRequired(i => i.Package)
                .HasForeignKey(i => i.PackageId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Package>()
                .HasMany(p => p.Exclusions)
                .WithRequired(e => e.Package)
                .HasForeignKey(e => e.PackageId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbContextInitializer());
        }
    }

    internal class ApplicationDbContextInitializer : IDatabaseInitializer<ApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
            // Create database + all tables if the database does not exist
            if (!context.Database.Exists())
            {
                context.Database.Create();
                SeedAdminUser(context);
            }
            else
            {
                // Database exists — ensure Package tables are present (idempotent)
                EnsurePackageTables(context);
            }
        }

        private static void SeedAdminUser(ApplicationDbContext context)
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

        private static void EnsurePackageTables(ApplicationDbContext context)
        {
            context.Database.ExecuteSqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='Packages')
                BEGIN
                    CREATE TABLE [dbo].[Packages] (
                        [PackageId]         INT             IDENTITY(1,1) NOT NULL,
                        [PackageName]       NVARCHAR(300)   NOT NULL,
                        [Slug]              NVARCHAR(300)   NOT NULL,
                        [ShortDescription]  NVARCHAR(1000)  NULL,
                        [Description]       NVARCHAR(MAX)   NULL,
                        [PackagePrice]      DECIMAL(18,2)   NOT NULL DEFAULT(0),
                        [DiscountPrice]     DECIMAL(18,2)   NULL,
                        [DurationDays]      INT             NOT NULL DEFAULT(1),
                        [DurationNights]    INT             NOT NULL DEFAULT(0),
                        [Destination]       NVARCHAR(200)   NULL,
                        [Country]           NVARCHAR(200)   NULL,
                        [StartingCity]      NVARCHAR(200)   NULL,
                        [PackageType]       NVARCHAR(100)   NULL,
                        [TourCategory]      NVARCHAR(100)   NULL,
                        [ThumbnailImage]    NVARCHAR(500)   NULL,
                        [BannerImage]       NVARCHAR(500)   NULL,
                        [HotelRating]       NVARCHAR(10)    NULL,
                        [MealType]          NVARCHAR(100)   NULL,
                        [Transportation]    NVARCHAR(100)   NULL,
                        [IsFlightIncluded]  BIT             NOT NULL DEFAULT(0),
                        [IsVisaIncluded]    BIT             NOT NULL DEFAULT(0),
                        [IsFeatured]        BIT             NOT NULL DEFAULT(0),
                        [IsPopular]         BIT             NOT NULL DEFAULT(0),
                        [IsActive]          BIT             NOT NULL DEFAULT(1),
                        [DisplayOrder]      INT             NOT NULL DEFAULT(0),
                        [MetaTitle]         NVARCHAR(300)   NULL,
                        [MetaKeywords]      NVARCHAR(500)   NULL,
                        [MetaDescription]   NVARCHAR(1000)  NULL,
                        [CreatedDate]       DATETIME        NOT NULL DEFAULT(GETUTCDATE()),
                        [UpdatedDate]       DATETIME        NULL,
                        CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED ([PackageId] ASC)
                    );
                    CREATE UNIQUE NONCLUSTERED INDEX [IX_Packages_Slug] ON [dbo].[Packages]([Slug] ASC);
                END
            ");

            context.Database.ExecuteSqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='PackageImages')
                BEGIN
                    CREATE TABLE [dbo].[PackageImages] (
                        [ImageId]       INT             IDENTITY(1,1) NOT NULL,
                        [PackageId]     INT             NOT NULL,
                        [ImagePath]     NVARCHAR(500)   NOT NULL,
                        [DisplayOrder]  INT             NOT NULL DEFAULT(0),
                        CONSTRAINT [PK_PackageImages] PRIMARY KEY CLUSTERED ([ImageId] ASC),
                        CONSTRAINT [FK_PackageImages_Packages] FOREIGN KEY ([PackageId])
                            REFERENCES [dbo].[Packages]([PackageId]) ON DELETE CASCADE
                    );
                END
            ");

            context.Database.ExecuteSqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='PackageItineraries')
                BEGIN
                    CREATE TABLE [dbo].[PackageItineraries] (
                        [ItineraryId]   INT             IDENTITY(1,1) NOT NULL,
                        [PackageId]     INT             NOT NULL,
                        [DayNumber]     INT             NOT NULL DEFAULT(1),
                        [Title]         NVARCHAR(300)   NOT NULL,
                        [Description]   NVARCHAR(MAX)   NULL,
                        CONSTRAINT [PK_PackageItineraries] PRIMARY KEY CLUSTERED ([ItineraryId] ASC),
                        CONSTRAINT [FK_PackageItineraries_Packages] FOREIGN KEY ([PackageId])
                            REFERENCES [dbo].[Packages]([PackageId]) ON DELETE CASCADE
                    );
                END
            ");

            context.Database.ExecuteSqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='PackageInclusions')
                BEGIN
                    CREATE TABLE [dbo].[PackageInclusions] (
                        [InclusionId]   INT             IDENTITY(1,1) NOT NULL,
                        [PackageId]     INT             NOT NULL,
                        [Item]          NVARCHAR(500)   NOT NULL,
                        CONSTRAINT [PK_PackageInclusions] PRIMARY KEY CLUSTERED ([InclusionId] ASC),
                        CONSTRAINT [FK_PackageInclusions_Packages] FOREIGN KEY ([PackageId])
                            REFERENCES [dbo].[Packages]([PackageId]) ON DELETE CASCADE
                    );
                END
            ");

            context.Database.ExecuteSqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='PackageExclusions')
                BEGIN
                    CREATE TABLE [dbo].[PackageExclusions] (
                        [ExclusionId]   INT             IDENTITY(1,1) NOT NULL,
                        [PackageId]     INT             NOT NULL,
                        [Item]          NVARCHAR(500)   NOT NULL,
                        CONSTRAINT [PK_PackageExclusions] PRIMARY KEY CLUSTERED ([ExclusionId] ASC),
                        CONSTRAINT [FK_PackageExclusions_Packages] FOREIGN KEY ([PackageId])
                            REFERENCES [dbo].[Packages]([PackageId]) ON DELETE CASCADE
                    );
                END
            ");

            context.Database.ExecuteSqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='Inquiries')
                BEGIN
                    CREATE TABLE [dbo].[Inquiries] (
                        [InquiryId]             INT              IDENTITY(1,1) NOT NULL,
                        [FullName]              NVARCHAR(150)    NOT NULL,
                        [MobileNumber]          NVARCHAR(20)     NOT NULL,
                        [EmailAddress]          NVARCHAR(150)    NOT NULL,
                        [TravelDate]            DATE             NULL,
                        [NumberOfPersons]       INT              NULL,
                        [PreferredDestination]  NVARCHAR(200)    NULL,
                        [City]                  NVARCHAR(100)    NULL,
                        [Budget]                DECIMAL(18,2)    NULL,
                        [Message]               NVARCHAR(MAX)    NULL,
                        [CreatedDate]           DATETIME         NOT NULL DEFAULT(GETUTCDATE()),
                        CONSTRAINT [PK_Inquiries] PRIMARY KEY CLUSTERED ([InquiryId] ASC)
                    );
                    CREATE NONCLUSTERED INDEX [IX_Inquiries_CreatedDate] ON [dbo].[Inquiries]([CreatedDate] DESC);
                END
            ");
        }
    }
}

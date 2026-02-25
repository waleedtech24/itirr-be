using ITIRR.Core;
using ITIRR.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITIRR.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Level 1 - Foundation
        public DbSet<Country> Countries { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleFeature> VehicleFeatures { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

        // Level 2 - Dependent
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        // Level 3 - Main Business
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleLocation> VehicleLocations { get; set; }
        public DbSet<VehicleMedia> VehicleMedias { get; set; }
        public DbSet<VehicleAvailability> VehicleAvailabilities { get; set; }
        public DbSet<VehicleBlockedDate> VehicleBlockedDates { get; set; }
        public DbSet<HostGoal> HostGoals { get; set; }
        public DbSet<VehicleHistory> VehicleHistories { get; set; }
        public DbSet<VehicleFeatureMapping> VehicleFeatureMappings { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<VehicleListing> VehicleListings { get; set; }
        public DbSet<VehicleListingMedia> VehicleListingMedia { get; set; }
        public DbSet<DriversLicence> DriversLicences { get; set; }
        public DbSet<PCOLicence> PCOLicences { get; set; }

        public DbSet<BoatListing> BoatListings { get; set; }
        public DbSet<BoatListingMedia> BoatListingMedia { get; set; }
        public DbSet<JetListing> JetListings { get; set; }
        public DbSet<JetListingMedia> JetListingMedia { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Disable cascade delete for specific relationships to avoid cycles

            // Vehicle relationships
            builder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany()
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(v => v.VehicleCategory)
                .WithMany(vc => vc.Vehicles)
                .HasForeignKey(v => v.VehicleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(v => v.VehicleBrand)
                .WithMany(vb => vb.Vehicles)
                .HasForeignKey(v => v.VehicleBrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(v => v.VehicleModel)
                .WithMany(vm => vm.Vehicles)
                .HasForeignKey(v => v.VehicleModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(v => v.Owner)
                .WithMany()
                .HasForeignKey(v => v.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleBrand relationships
            builder.Entity<VehicleBrand>()
                .HasOne(vb => vb.VehicleType)
                .WithMany(vt => vt.VehicleBrands)
                .HasForeignKey(vb => vb.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleCategory relationships
            builder.Entity<VehicleCategory>()
                .HasOne(vc => vc.VehicleType)
                .WithMany(vt => vt.VehicleCategories)
                .HasForeignKey(vc => vc.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleModel relationships
            builder.Entity<VehicleModel>()
                .HasOne(vm => vm.VehicleBrand)
                .WithMany(vb => vb.VehicleModels)
                .HasForeignKey(vm => vm.VehicleBrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleFeature relationships
            builder.Entity<VehicleFeature>()
                .HasOne(vf => vf.VehicleType)
                .WithMany(vt => vt.VehicleFeatures)
                .HasForeignKey(vf => vf.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // City relationships
            builder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(co => co.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<City>()
                .HasOne(c => c.State)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // State relationships
            builder.Entity<State>()
                .HasOne(s => s.Country)
                .WithMany(c => c.States)
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleLocation relationships
            builder.Entity<VehicleLocation>()
                .HasOne(vl => vl.Vehicle)
                .WithMany(v => v.VehicleLocations)
                .HasForeignKey(vl => vl.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VehicleLocation>()
                .HasOne(vl => vl.City)
                .WithMany(c => c.VehicleLocations)
                .HasForeignKey(vl => vl.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Document relationships
            builder.Entity<Document>()
                .HasOne(d => d.DocumentType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Document>()
                .HasOne(d => d.Vehicle)
                .WithMany(v => v.Documents)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Document>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message relationships
            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleBlockedDate relationships
            builder.Entity<VehicleBlockedDate>()
                .HasOne(vbd => vbd.BlockedByUser)
                .WithMany()
                .HasForeignKey(vbd => vbd.BlockedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // VehicleHistory relationships
            builder.Entity<VehicleHistory>()
                .HasOne(vh => vh.ChangedByUser)
                .WithMany()
                .HasForeignKey(vh => vh.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
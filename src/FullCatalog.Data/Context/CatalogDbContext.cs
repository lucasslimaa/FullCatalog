using FullCatalog.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FullCatalog.Data
{
    public class CatalogDbContext : DbContext
    {
        private readonly IHttpContextAccessor ContextAccessor;

        public CatalogDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)   : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

            this.ContextAccessor = httpContextAccessor;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            //OVERRIDE THE CASCADE DELETE BEHAVIOUR
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((Entity)entityEntry.Entity).CreatedBy = this.ContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((Entity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((Entity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((Entity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((Entity)entityEntry.Entity).ModifiedBy = this.ContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
            }

            // After we set all the needed properties
            // we call the base implementation of SaveChanges
            // to actually save our entities in the database
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((Entity)entityEntry.Entity).CreatedBy = this.ContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((Entity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((Entity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((Entity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                ((Entity)entityEntry.Entity).ModifiedBy = this.ContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
            }

            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}

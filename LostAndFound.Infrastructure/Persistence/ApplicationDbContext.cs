using Intent.RoslynWeaver.Attributes;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Common.Models;
using LostAndFound.Domain.Entities;
using LostAndFound.Infrastructure.Identity;
using LostAndFound.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.DbContext", Version = "1.0")]

namespace LostAndFound.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureModel(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClaimConfiguration());
            modelBuilder.ApplyConfiguration(new DisputeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        [IntentManaged(Mode.Ignore)]
        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            // Seed data
            // https://rehansaeed.com/migrating-to-entity-framework-core-seed-data/
            /* E.g.
            modelBuilder.Entity<Car>().HasData(
                new Car() { CarId = 1, Make = "Ferrari", Model = "F40" },
                new Car() { CarId = 2, Make = "Ferrari", Model = "F50" },
                new Car() { CarId = 3, Make = "Lamborghini", Model = "Countach" });
            */
        }

        async Task<OperationStatus> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var opStatus = new OperationStatus();

            try
            {
                opStatus.RecordsAffected = await SaveChangesAsync(cancellationToken);
                opStatus.Status = opStatus.RecordsAffected > 0;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                return OperationStatus.CreateFromException("An error occured while saving.", ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types

            return opStatus;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Unknown";
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "Unknown";
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }

            var recordsAffected = await base.SaveChangesAsync(cancellationToken);
            await base.SaveChangesAsync(cancellationToken);

            return recordsAffected;
        }
    }
}
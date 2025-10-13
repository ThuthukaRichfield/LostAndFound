using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Common.Interfaces;
using LostAndFound.Domain.Entities;
using LostAndFound.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.DbContext", Version = "1.0")]

namespace LostAndFound.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BaseEntity> BaseEntities { get; set; }
        public DbSet<BaseNamedEntity> BaseNamedEntities { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureModel(modelBuilder);
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BaseNamedEntityConfiguration());
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
    }
}
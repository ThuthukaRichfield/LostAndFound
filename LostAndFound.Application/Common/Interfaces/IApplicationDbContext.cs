using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;
using Microsoft.EntityFrameworkCore;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.DbContextInterface", Version = "1.0")]

namespace LostAndFound.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Claim> Claims { get; }
        DbSet<Dispute> Disputes { get; }
        DbSet<Item> Items { get; }
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
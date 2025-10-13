using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;
using LostAndFound.Domain.Repositories;
using LostAndFound.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace LostAndFound.Infrastructure.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class ItemRepository : RepositoryBase<Item, Item, ApplicationDbContext>, IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int itemId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.ItemId == itemId, cancellationToken);
        }

        public void Add(Item entity)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Items (ItemId, Title, Category, Status, UserId) VALUES({entity.ItemId}, {entity.Title}, {entity.Category}, {entity.Status}, {entity.UserId})");
        }
    }
}
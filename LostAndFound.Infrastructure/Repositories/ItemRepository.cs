using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;
using LostAndFound.Domain.Repositories;
using LostAndFound.Infrastructure.Persistence;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace LostAndFound.Infrastructure.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class ItemRepository : RepositoryBase<Item, Item, ApplicationDbContext>, IItemRepository
    {

        public ItemRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int itemId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.ItemId == itemId, cancellationToken);
        }

        public async Task<Item?> FindByIdAsync(int itemId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.ItemId == itemId, cancellationToken);
        }

        public async Task<Item?> FindByIdAsync(
            int itemId,
            Func<IQueryable<Item>, IQueryable<Item>> queryOptions,
            CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.ItemId == itemId, queryOptions, cancellationToken);
        }

        public async Task<List<Item>> FindByIdsAsync(int[] itemIds, CancellationToken cancellationToken = default)
        {
            // Force materialization - Some combinations of .net9 runtime and EF runtime crash with "Convert ReadOnlySpan to List since expression trees can't handle ref struct"
            var idList = itemIds.ToList();
            return await FindAllAsync(x => idList.Contains(x.ItemId), cancellationToken);
        }
    }
}
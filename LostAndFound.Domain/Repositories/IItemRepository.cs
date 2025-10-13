using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.Repositories.Api.EntityRepositoryInterface", Version = "1.0")]

namespace LostAndFound.Domain.Repositories
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public interface IItemRepository : IEFRepository<Item, Item>
    {
        [IntentManaged(Mode.Fully)]
        Task<TProjection?> FindByIdProjectToAsync<TProjection>(int itemId, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<Item?> FindByIdAsync(int itemId, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<Item?> FindByIdAsync(int itemId, Func<IQueryable<Item>, IQueryable<Item>> queryOptions, CancellationToken cancellationToken = default);
        [IntentManaged(Mode.Fully)]
        Task<List<Item>> FindByIdsAsync(int[] itemIds, CancellationToken cancellationToken = default);
    }
}
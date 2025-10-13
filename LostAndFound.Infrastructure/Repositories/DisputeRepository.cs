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
    public class DisputeRepository : RepositoryBase<Dispute, Dispute, ApplicationDbContext>, IDisputeRepository
    {

        public DisputeRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int disputeId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.DisputeId == disputeId, cancellationToken);
        }

        public async Task<Dispute?> FindByIdAsync(int disputeId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.DisputeId == disputeId, cancellationToken);
        }

        public async Task<Dispute?> FindByIdAsync(
            int disputeId,
            Func<IQueryable<Dispute>, IQueryable<Dispute>> queryOptions,
            CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.DisputeId == disputeId, queryOptions, cancellationToken);
        }

        public async Task<List<Dispute>> FindByIdsAsync(int[] disputeIds, CancellationToken cancellationToken = default)
        {
            // Force materialization - Some combinations of .net9 runtime and EF runtime crash with "Convert ReadOnlySpan to List since expression trees can't handle ref struct"
            var idList = disputeIds.ToList();
            return await FindAllAsync(x => idList.Contains(x.DisputeId), cancellationToken);
        }
    }
}
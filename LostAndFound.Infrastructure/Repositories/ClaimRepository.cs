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
    public class ClaimRepository : RepositoryBase<Claim, Claim, ApplicationDbContext>, IClaimRepository
    {

        public ClaimRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int claimId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.ClaimId == claimId, cancellationToken);
        }

        public async Task<Claim?> FindByIdAsync(int claimId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.ClaimId == claimId, cancellationToken);
        }

        public async Task<Claim?> FindByIdAsync(
            int claimId,
            Func<IQueryable<Claim>, IQueryable<Claim>> queryOptions,
            CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.ClaimId == claimId, queryOptions, cancellationToken);
        }

        public async Task<List<Claim>> FindByIdsAsync(int[] claimIds, CancellationToken cancellationToken = default)
        {
            // Force materialization - Some combinations of .net9 runtime and EF runtime crash with "Convert ReadOnlySpan to List since expression trees can't handle ref struct"
            var idList = claimIds.ToList();
            return await FindAllAsync(x => idList.Contains(x.ClaimId), cancellationToken);
        }
    }
}
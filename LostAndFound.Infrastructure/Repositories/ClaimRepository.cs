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
    public class ClaimRepository : RepositoryBase<Claim, Claim, ApplicationDbContext>, IClaimRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClaimRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int claimId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.ClaimId == claimId, cancellationToken);
        }

        public void Add(Claim entity)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Claims (ClaimId, ItemId, UserId) VALUES({entity.ClaimId}, {entity.ItemId}, {entity.UserId})");
        }
    }
}
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
    public class DisputeRepository : RepositoryBase<Dispute, Dispute, ApplicationDbContext>, IDisputeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DisputeRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int disputeId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.DisputeId == disputeId, cancellationToken);
        }

        public void Add(Dispute entity)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Disputes (DisputeId, Reason, Status, ClaimId) VALUES({entity.DisputeId}, {entity.Reason}, {entity.Status}, {entity.ClaimId})");
        }
    }
}
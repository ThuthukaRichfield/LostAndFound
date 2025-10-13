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
    public class UserRepository : RepositoryBase<User, User, ApplicationDbContext>, IUserRepository
    {

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int userId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.UserId == userId, cancellationToken);
        }

        public async Task<User?> FindByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<User?> FindByIdAsync(
            int userId,
            Func<IQueryable<User>, IQueryable<User>> queryOptions,
            CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.UserId == userId, queryOptions, cancellationToken);
        }

        public async Task<List<User>> FindByIdsAsync(int[] userIds, CancellationToken cancellationToken = default)
        {
            // Force materialization - Some combinations of .net9 runtime and EF runtime crash with "Convert ReadOnlySpan to List since expression trees can't handle ref struct"
            var idList = userIds.ToList();
            return await FindAllAsync(x => idList.Contains(x.UserId), cancellationToken);
        }
    }
}
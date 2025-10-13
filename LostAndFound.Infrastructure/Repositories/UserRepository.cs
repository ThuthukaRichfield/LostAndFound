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
    public class UserRepository : RepositoryBase<User, User, ApplicationDbContext>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<TProjection?> FindByIdProjectToAsync<TProjection>(
            int userId,
            CancellationToken cancellationToken = default)
        {
            return await FindProjectToAsync<TProjection>(x => x.UserId == userId, cancellationToken);
        }

        public void Add(User entity)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Users (UserId, Email, Pasword, Role) VALUES({entity.UserId}, {entity.Email}, {entity.Pasword}, {entity.Role})");
        }
    }
}
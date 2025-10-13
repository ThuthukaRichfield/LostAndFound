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
    public class BaseNamedEntityRepository : RepositoryBase<BaseNamedEntity, BaseNamedEntity, ApplicationDbContext>, IBaseNamedEntityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseNamedEntityRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public void Add(BaseNamedEntity entity)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO BaseNamedEntities (Name) VALUES({entity.Name})");
        }
    }
}
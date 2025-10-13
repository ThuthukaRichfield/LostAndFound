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
    public class BaseEntityRepository : RepositoryBase<BaseEntity, BaseEntity, ApplicationDbContext>, IBaseEntityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseEntityRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public void Add(BaseEntity entity)
        {
            _dbContext.Database.ExecuteSqlInterpolated($"INSERT INTO BaseEntities (CreatedBy, CreatedDate, LastModifiedBy, LastModifiedDate, Disabled, Deleted) VALUES({entity.CreatedBy}, {entity.CreatedDate}, {entity.LastModifiedBy}, {entity.LastModifiedDate}, {entity.Disabled}, {entity.Deleted})");
        }
    }
}
using Intent.RoslynWeaver.Attributes;
using LostAndFound.Domain.Common.Interfaces;
using LostAndFound.Domain.Repositories;
using LostAndFound.Infrastructure.Persistence;
using LostAndFound.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace LostAndFound.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase("DefaultConnection");
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<IBaseEntityRepository, BaseEntityRepository>();
            services.AddTransient<IBaseNamedEntityRepository, BaseNamedEntityRepository>();
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<IDisputeRepository, DisputeRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
using Intent.RoslynWeaver.Attributes;
using LostAndFound.Application.Common.Interfaces;
using System.Security.Claims;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Identity.CurrentUserService", Version = "1.0")]

namespace LostAndFound.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService()
        {
        }

        public string UserEmail { get { return _httpContextAccessor.HttpContext?.User?.FindFirstValue("preferred_username"); } }

        public Task<ICurrentUser?> GetAsync()
        {
            return Task.FromResult<ICurrentUser?>(null);
        }

        public async Task<bool> AuthorizeAsync(string policy)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            return await Task.FromResult(true);
        }
    }
}
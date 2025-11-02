using LostAndFound.Application.Common.Models;
using LostAndFound.Application.Services.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        // Change the return type and inputs to suit your needs
        Task<OperationStatus> CreateUserAsync(string email, string password, string role);

        // You'll need this for other use cases like GetUserById
        Task<UserDto> GetUserIdAsync(string email);
    }
}

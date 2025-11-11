using Azure.Core;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Common.Models;
using LostAndFound.Application.Services.Users.Models;
using LostAndFound.Domain.Entities;
using LostAndFound.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        async Task<OperationStatus> IIdentityService.CreateUserAsync(string email, string password, string role)
        {
            if (!email.ToLower().Contains("richfield.ac.za"))
            {
                return new OperationStatus { Status = false, Message = "This is not a valid Richfield Email" };
            }

            // 1. Create the Identity User Object
            var user = new ApplicationUser { UserName = email, Email = email };

            // 2. Add to DB using UserManager
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return OperationStatus.CreateFromException($"User creation failed: {errors}", new InvalidOperationException());
            }

            // 3. Role assignment logic
            if (await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            // 4. Return success
            return new OperationStatus { Status = true, RecordsAffected = 1 };
        }

        async public Task<UserDto> GetUserIdAsync(string email)
        {
            // Use the UserManager to find the user by their ID
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            // Map the ApplicationUser (Infrastructure type) to the DTO (Application type)
            return new UserDto
            {
                Email = user.Email,
            };
        }
    }
}
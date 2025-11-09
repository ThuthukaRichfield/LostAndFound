using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Common.Models;
using LostAndFound.Domain;
using LostAndFound.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Users
{
    public class UpdateUserRoleCommand : IRequest<OperationStatus>, ICommand
    {
        // Getters and setters
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }

    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, OperationStatus>
    {
        // Variables
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _dbContext;

        public UpdateUserRoleCommandHandler(IApplicationDbContext dbContext, IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());

                if (user == null)
                {
                    return OperationStatus.CreateFromException("User not found.", new Exception($"User {request.Email} not found."));
                }

                user.Role = request.UserRole;
                _dbContext.Users.Update(user);

                var opStatus = await _dbContext.SaveChangesAsync(cancellationToken);

                return opStatus;
            }
            catch (Exception ex)
            {
                var opStatus = OperationStatus.CreateFromException("Error creating user.", ex);
                return opStatus;
            }
        }
    }
}

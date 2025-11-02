using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Common.Models;
using LostAndFound.Domain;
using LostAndFound.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Users
{
    public class CreateUserCommand : IRequest<OperationStatus>, ICommand
    {
        // Getters and setters
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserRole UserRole { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationStatus>
    {
        // Variables
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _dbContext;

        public CreateUserCommandHandler(IApplicationDbContext dbContext, IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Delegate the infrastructure/persistence work to the IIdentityService
                var opStatus = await _identityService.CreateUserAsync(
                    request.Email,
                    request.Password,
                    request.UserRole.ToString());

                if (!opStatus.Status)
                {
                    throw new Exception("Error Saving to Identity");
                }

                var newUser = new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Pasword = "NoLongerStoredHere",
                    Role = request.UserRole
                };

                _dbContext.Users.Add(newUser);

                opStatus = await _dbContext.SaveChangesAsync(cancellationToken);

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

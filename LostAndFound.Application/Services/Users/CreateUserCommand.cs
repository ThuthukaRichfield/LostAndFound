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
        public UserRole UserRole { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationStatus>
    {
        // Variables
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        
        // Ctor
        public CreateUserCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Create Object
                var newUser = new User
                {
                    Email = request.Email,
                    Name = request.Email,
                    Pasword = request.Password,
                    Role = request.UserRole
                };

                // Add to DB
               _dbContext.Users.Add(newUser);

                // Save to DB
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

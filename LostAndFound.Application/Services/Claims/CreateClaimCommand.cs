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

namespace LostAndFound.Application.Services.Claims
{
    public class CreateClaimCommand : IRequest<OperationStatus>, ICommand
    {
        // Getters and setters
        public string UserEmail { get; set; }
        public int ItemId { get; set; }
        public string Reason { get; set; }
    }

    public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, OperationStatus>
    {
        // Variables
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        // Ctor
        public CreateClaimCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get User the Item will be linked to
                var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email.ToLower().Equals(request.UserEmail.ToLower()), cancellationToken);

                // Check if User exists
                if (user == null)
                {
                    return OperationStatus.CreateFromException("User not found.", new Exception($"User {request.UserEmail} not found."));
                }

                // Get User the Item will be linked to
                var item = await _dbContext.Items.FindAsync(request.ItemId, cancellationToken);

                // Check if User exists
                if (item == null)
                {
                    return OperationStatus.CreateFromException("Item not found.", new Exception($"Item with ID {request.ItemId} not found."));
                } 
                else if (item.Status != ItemStatus.Found)
                {
                    return OperationStatus.CreateFromException("Item is not available for claiming.", new Exception($"Item with ID {request.ItemId} is not marked as Found."));
                }

                // Create Object
                var newClaim = new Claim
                {
                    User = user,
                    Item = item,
                    FoundDescription = request.Reason,
                    Status = ClaimStatus.Pending
                };

                //item.Status = ItemStatus.Claimed;

                // Add to DB
                _dbContext.Claims.Add(newClaim);
                //_dbContext.Items.Update(item);

                // Save to DB
                var opStatus = await _dbContext.SaveChangesAsync(cancellationToken);
                return opStatus;
            }
            catch (Exception ex)
            {
                var opStatus = OperationStatus.CreateFromException("Error creating claim.", ex);
                return opStatus;
            }
        }
    }
}

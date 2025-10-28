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

namespace LostAndFound.Application.Services.Disputes
{

    public class CreateDisputeCommand : IRequest<OperationStatus>, ICommand
    {
        // Getters and setters
        public int ClaimId { get; set; }
        public string Reason { get; set; }
    }

    public class CreateDisputeCommandHandler : IRequestHandler<CreateDisputeCommand, OperationStatus>
    {
        // Variables
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        // Ctor
        public CreateDisputeCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(CreateDisputeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get Claim the Dispute will be linked to
                var claim = await _dbContext.Claims.FindAsync(request.ClaimId, cancellationToken);

                // Check if Claim exists
                if (claim == null)
                {
                    return OperationStatus.CreateFromException("Claim not found.", new Exception($"Claim with ID {request.ClaimId} not found."));
                }

                //claim = ClaimStatus.Disputed;

                // Update Claim in DB
                _dbContext.Claims.Update(claim);

                // Save to DB
                var opStatus = await _dbContext.SaveChangesAsync(cancellationToken);
                return opStatus;
            }
            catch (Exception ex)
            {
                var opStatus = OperationStatus.CreateFromException("Error creating dispute.", ex);
                return opStatus;
            }
        }
    }
}

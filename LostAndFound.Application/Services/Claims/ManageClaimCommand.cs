using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Common.Models;
using LostAndFound.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Claims
{
    public class ManageClaimCommand : IRequest<OperationStatus>, ICommand
    {
        // Getters and setters
        public int ClaimId { get; set; }
        public bool IsApproved { get; set; }
    }

    public class ManageClaimCommandHandler : IRequestHandler<ManageClaimCommand, OperationStatus>
    {
        // Variables
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        // Ctor
        public ManageClaimCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(ManageClaimCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get User the Item will be linked to
                var currentClaim = await _dbContext.Claims
                    .Include(c => c.Item)
                    .FirstOrDefaultAsync(e => e.ClaimId == request.ClaimId, cancellationToken);

                // Check if User exists
                if (currentClaim == null)
                {
                    return OperationStatus.CreateFromException("Claim not found.", new Exception($"Claim with ID {request.ClaimId} not found."));
                }

                // Get User the Item will be linked to
                var currentItem = await _dbContext.Items
                    .FirstOrDefaultAsync(e => e.ItemId == currentClaim.ItemId, cancellationToken);

                // Check if User exists
                if (currentItem == null)
                {
                    return OperationStatus.CreateFromException("Item not found.", new Exception($"Item with ID {currentClaim.ItemId} not found."));
                }

                if (request.IsApproved)
                {
                    var allLinkedClaims = await _dbContext.Claims.Where(e => e.ItemId == currentClaim.ItemId).ToListAsync(cancellationToken);

                    foreach (var claim in allLinkedClaims)
                    {
                        if (claim.ClaimId == currentClaim.ClaimId)
                        {
                            // Approve this claim
                            claim.Status = ClaimStatus.Approved;
                            currentItem.Status = ItemStatus.Claimed;
                        }
                        else
                        {
                            // Reject other claims
                            claim.Status = ClaimStatus.Rejected;
                        }
                    }

                    // Add to DB
                    _dbContext.Claims.UpdateRange(allLinkedClaims);
                    _dbContext.Items.Update(currentItem);
                }
                else
                {
                    // Reject other claims
                    currentClaim.Status = ClaimStatus.Rejected;

                    // Add to DB
                    _dbContext.Claims.Update(currentClaim);
                }

                // Save to DB
                var opStatus = await _dbContext.SaveChangesAsync(cancellationToken);
                return opStatus;
            }
            catch (Exception ex)
            {
                var opStatus = OperationStatus.CreateFromException("Error managing claim.", ex);
                return opStatus;
            }
        }
    }
}

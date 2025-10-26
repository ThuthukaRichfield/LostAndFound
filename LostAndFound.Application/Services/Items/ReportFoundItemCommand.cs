using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Common.Models;
using LostAndFound.Domain;
using MediatR;

namespace LostAndFound.Application.Services.Items
{
    public class ReportFoundItemCommand : IRequest<OperationStatus>, ICommand
    {
        // Getters and setters
        public int UserId { get; set; }
        public int ItemId { get; set; }
    }

    public class ReportFoundItemCommandHandler : IRequestHandler<ReportFoundItemCommand, OperationStatus>
    {
        // Variables
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        // Ctor
        public ReportFoundItemCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(ReportFoundItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get User the Item will be linked to
                var item = await _dbContext.Items.FindAsync(request.ItemId, cancellationToken);

                // Check if Item exists
                if (item == null)
                {
                    return OperationStatus.CreateFromException("Item not found.", new Exception($"Item with ID {request.ItemId} not found."));
                }

                item.Status = ItemStatus.Found;

                // Update Item in DB
                _dbContext.Items.Update(item);

                // Save to DB
                var opStatus = await _dbContext.SaveChangesAsync(cancellationToken);
                return opStatus;
            }
            catch (Exception ex)
            {
                var opStatus = OperationStatus.CreateFromException("Error reporting found item.", ex);
                return opStatus;
            }
        }
    }
}
